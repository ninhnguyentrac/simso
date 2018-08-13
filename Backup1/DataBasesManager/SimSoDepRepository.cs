using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Web;
using LinqToExcel;
using SimSoDepStore;
using SimSoDep.Areas.Administrator.Models;
using SimSoDep.Models;
using System.Text.RegularExpressions;
namespace SimSoDep.DataBasesManager
{
    public class SimSoDepRepository : ISimSoDepRepository
    {
        private readonly SimSoDepEntities _entities;

        public SimSoDepRepository()
        {
            _entities = new SimSoDepEntities();
        }

        #region Account

        public bool ValidateUser(string userName, string passWord)
        {
            try
            {
                var passWordEncrypt = EncryptPassword.MD5Hash(passWord);
                var user = _entities.tbl_Global_User.Where(u => u.UserName == userName && u.PassWord == passWordEncrypt).FirstOrDefault();
                return user != null;
            }
            catch (Exception)
            {
                return false;
            }

        }

        public bool ChangePassword(string userName, string oldPass, string newPass)
        {
            try
            {
                var passWordEncrypt = EncryptPassword.MD5Hash(oldPass);
                var user = _entities.tbl_Global_User.Where(u => u.UserName == userName && u.PassWord == passWordEncrypt).FirstOrDefault();
                if (user == null)
                {
                    return false;
                }
                var passWordNewEncrypt = EncryptPassword.MD5Hash(newPass);
                user.PassWord = passWordNewEncrypt;
                _entities.SaveChanges();
                return true;
            }
            catch (Exception)
            {
                return false;
            }

        }

        #endregion

        #region check so
        public List<CheckSoModel> CheckSo(string path)
        {
            if (string.IsNullOrEmpty(path))
            {
                return null;
            }
            var listSo = Regex.Split(path, "\r\n");
            var listQueDich = _entities.tbl_SimSoDep_KinhDich.ToList();
            var listNguHanh = _entities.tbl_SimSoDep_NguHanh.ToList();
            var result = new List<CheckSoModel>();
            foreach (var ss in listSo)
            {
                var simso = ss.Replace(".", "").Replace(" ", "").Trim();
                if (string.IsNullOrEmpty(simso))
                    continue;
                var sodautien = simso.Substring(0, 1);
                if ((simso.Length == 9 && sodautien.Equals("9")) || (simso.Length == 10 && sodautien.Equals("1")))
                {
                    simso = simso.Insert(0, "0");
                }
                if (simso.Length != 10 && simso.Length != 11)
                {
                    continue;
                }

                if (simso.Contains("4"))
                    continue;
                var sonuoc = SimSoHelper.CheckSoNuoc(simso);
                if (sonuoc < 7)
                    continue;
                var amduong = SimSoHelper.CheckAmDuong(simso);
                if (amduong == (int)LoaiAmDuong.AmDuongLechNhieu || amduong == (int)LoaiAmDuong.AmDuongLechThaiCuc)
                    continue;
                if (!simso.Contains("8"))
                    continue;
                var que = SimSoHelper.CheckQueDich(simso, listQueDich);
                var loaique = listQueDich.Where(q => q.IdKinhDich == que).Select(q => q.LoaiQue).FirstOrDefault();
                if (loaique == (int)LoaiQue.KhongHungKhongCat || loaique == (int)LoaiQue.Hung)
                    continue;
                var socuoi = simso.Substring(simso.Length - 1, 1);
                var nguhanh = SimSoHelper.CheckNguHanh(socuoi);
                var tenNguHanh = listNguHanh.Where(h => h.IdNguHanh == nguhanh).Select(h => h.Ten).FirstOrDefault();
                var tenQue = listQueDich.Where(q => q.IdKinhDich == que).Select(q => q.TenQue).FirstOrDefault();
                var cs = new CheckSoModel
                {
                    TenQue = tenQue,
                    SoDienThoai = simso,
                    LoaiAmDuong = (amduong == (int)LoaiAmDuong.AmDuongGanCanBang) ? "Gần cân bằng" : "Cân bằng",
                    NguHanh = tenNguHanh,
                    SoNuoc = sonuoc
                };
                result.Add(cs);

            }

            return result;
        }

        public ThongKeSimModel ThongKeSim()
        {
            try
            {
                var result =
                    _entities.ThongKeSim((int)NhaMang.SimSoDepViettel, (int)NhaMang.SimSoDepVinaphone,
                                         (int)NhaMang.SimSoDepMobifone, (int)NhaMang.SimSoDepVietnamobile,
                                         (int)NhaMang.SimSoDepSfone, (int)NhaMang.SimSoDepBeeline).FirstOrDefault();
                if (result != null)
                    return new ThongKeSimModel
                               {
                                   SimBeeline = result.Numbeeline,
                                   SimMobi = result.NumMobi,
                                   SimSfone = result.NumSfone,
                                   SimVietnamMobi = result.NumVietnam,
                                   SimViettel = result.NumViettel,
                                   SimVina = result.NumVina
                               };
                return new ThongKeSimModel();
            }
            catch (Exception)
            {
                return new ThongKeSimModel();
            }
        }

        #endregion

        #region Import
        //import excel
        public int ImportExcel(ImportModel importModel)
        {
            try
            {
                if (string.IsNullOrEmpty(importModel.TxtSoDienThoai))
                {
                    return 0;
                }
                var daily = _entities.tbl_SimSoDep_DaiLy.Where(d => d.MaDaiLy == importModel.DaiLy).FirstOrDefault();
                if (daily == null || daily.MaDaiLy < 1)
                {
                    return 0;
                }
                var listSo = Regex.Split(importModel.TxtSoDienThoai, "\r\n");
                var listLoaiGia = _entities.tbl_SimSoDep_LoaiGia.ToList();
                var listDauSo = _entities.tbl_SimSoDep_DauSo.ToList();
                var listQueDich = _entities.tbl_SimSoDep_KinhDich.ToList();
                var count = 0;
                foreach (var item in listSo)
                {
                    var arrSo = item.Split('\t');
                    if (arrSo.Length != 2)
                    {
                        continue;
                    }
                    var soDt = arrSo[0];
                    var giaT = arrSo[1];
                    if (string.IsNullOrEmpty(soDt))
                    {
                        continue;
                    }
                    var soDienThoai = soDt.Replace(".", "").Replace(" ", "").Trim();
                    //Them so 0 vao dau tien neu so bi mat so 0
                    var sodautien = soDienThoai.Substring(0, 1);
                    if ((soDienThoai.Length == 9 && sodautien.Equals("9")) || (soDienThoai.Length == 10 && sodautien.Equals("1")))
                    {
                        soDienThoai = soDienThoai.Insert(0, "0");
                    }
                    if (soDienThoai.Length != 10 && soDienThoai.Length != 11)
                    {
                        continue;
                    }

                    var giaTien = long.Parse(giaT.Replace(",", "").Trim());
                    long giaBanDau = giaTien;
                    //Check do dai so
                    var soLength = soDienThoai.Length;
                    //Khoi tao so dien thoai
                    var simso = new tbl_SimSoDep_SimSo
                                    {
                                        DinhDangHienThi = soDt.Trim(),
                                        SoDienThoai = soDienThoai,
                                        tbl_SimSoDep_SimSoLoaiSim = new Collection<tbl_SimSoDep_SimSoLoaiSim>(),
                                        tbl_SimSoDep_LoaiSimChiTietSimSo = new Collection<tbl_SimSoDep_LoaiSimChiTietSimSo>()
                                    };
                    //Ma dau so
                    var dauSo = (soLength == 10) ? soDienThoai.Substring(0, 3) : soDienThoai.Substring(0, 4);
                    //Check dau so nha mang
                    var dauSoData = listDauSo.Where(d => d.MoTa == dauSo).FirstOrDefault();
                    if (dauSoData != null && (dauSoData.MaNhaMang != null && dauSoData.MaNhaMang > 0))
                    {
                        simso.MaDauSo = dauSoData.MaDauSo;
                        simso.MaNhaMang = dauSoData.MaNhaMang ?? 0;
                    }
                    else
                    {
                        continue;
                    }

                    simso.GiaBanDau = giaBanDau;
                    var socuoi = soDienThoai.Substring(soDienThoai.Length - 1, 1);
                    //Check ngu hanh
                    simso.IdNguHanh = SimSoHelper.CheckNguHanh(socuoi);
                    //Check am duong
                    simso.LoaiAmDuong = SimSoHelper.CheckAmDuong(soDienThoai);
                    //que dich
                    simso.IdKinhDich = SimSoHelper.CheckQueDich(soDienThoai, listQueDich);
                    //check so nuoc
                    simso.SoNuoc = SimSoHelper.CheckSoNuoc(soDienThoai);
                    simso.MaDaiLy = daily.MaDaiLy;

                    if (soLength == 10)
                    {
                        simso.MaLoaiDoDai = (int)DoDaiSo.Dai10So;
                    }
                    else
                    {
                        simso.MaLoaiDoDai = (int)DoDaiSo.Dai11So;
                    }

                    //substring
                    var haiSoCuoi = soDienThoai.Substring(soDienThoai.Length - 2, 2);
                    var baSoCuoi = soDienThoai.Substring(soDienThoai.Length - 3, 3);
                    var bonSoCuoi = soDienThoai.Substring(soDienThoai.Length - 4, 4);
                    var namSoCuoi = soDienThoai.Substring(soDienThoai.Length - 5, 5);
                    var sauSoCuoi = soDienThoai.Substring(soDienThoai.Length - 6, 6);
                    var tamSoCuoi = soDienThoai.Substring(soDienThoai.Length - 8, 8);
                    var soGiua = (soLength == 10) ? soDienThoai.Substring(3, 6) : soDienThoai.Substring(3, 7);

                    //Check tien giua
                    var sotienGiua = SimSoHelper.CheckSoTienGiua(soGiua);
                    if (sotienGiua > 2)
                    {
                        simso.tbl_SimSoDep_SimSoLoaiSim.Add(new tbl_SimSoDep_SimSoLoaiSim
                        {
                            MaLoaiSim = (int)LoaiSimSo.SimTienGiua
                        });
                        if (sotienGiua > 3)
                        {
                            simso.tbl_SimSoDep_LoaiSimChiTietSimSo.Add(new tbl_SimSoDep_LoaiSimChiTietSimSo
                                                                      {
                                                                          MaLoaiSimChiTiet = (int)LoaiSimSoChiTiet.TienGiuaChonLoc
                                                                      });
                        }
                    }

                    //Check tam hoa don
                    var isTamHoa = false;
                    switch (baSoCuoi)
                    {
                        case "000":
                            isTamHoa = true;
                            simso.tbl_SimSoDep_LoaiSimChiTietSimSo.Add(new tbl_SimSoDep_LoaiSimChiTietSimSo
                            {
                                MaLoaiSimChiTiet = (int)LoaiSimSoChiTiet.TamHoa000
                            });
                            break;
                        case "111":
                            isTamHoa = true;
                            simso.tbl_SimSoDep_LoaiSimChiTietSimSo.Add(new tbl_SimSoDep_LoaiSimChiTietSimSo
                            {
                                MaLoaiSimChiTiet = (int)LoaiSimSoChiTiet.TamHoa111
                            });
                            break;
                        case "222":
                            isTamHoa = true;
                            simso.tbl_SimSoDep_LoaiSimChiTietSimSo.Add(new tbl_SimSoDep_LoaiSimChiTietSimSo
                            {
                                MaLoaiSimChiTiet = (int)LoaiSimSoChiTiet.TamHoa222
                            });
                            break;
                        case "333":
                            isTamHoa = true;
                            simso.tbl_SimSoDep_LoaiSimChiTietSimSo.Add(new tbl_SimSoDep_LoaiSimChiTietSimSo
                            {
                                MaLoaiSimChiTiet = (int)LoaiSimSoChiTiet.TamHoa333
                            });
                            break;
                        case "444":
                            isTamHoa = true;
                            simso.tbl_SimSoDep_LoaiSimChiTietSimSo.Add(new tbl_SimSoDep_LoaiSimChiTietSimSo
                            {
                                MaLoaiSimChiTiet = (int)LoaiSimSoChiTiet.TamHoa444
                            });
                            break;
                        case "555":
                            isTamHoa = true;
                            simso.tbl_SimSoDep_LoaiSimChiTietSimSo.Add(new tbl_SimSoDep_LoaiSimChiTietSimSo
                            {
                                MaLoaiSimChiTiet = (int)LoaiSimSoChiTiet.TamHoa555
                            });
                            break;
                        case "666":
                            isTamHoa = true;
                            simso.tbl_SimSoDep_LoaiSimChiTietSimSo.Add(new tbl_SimSoDep_LoaiSimChiTietSimSo
                            {
                                MaLoaiSimChiTiet = (int)LoaiSimSoChiTiet.TamHoa666
                            });
                            break;
                        case "777":
                            isTamHoa = true;
                            simso.tbl_SimSoDep_LoaiSimChiTietSimSo.Add(new tbl_SimSoDep_LoaiSimChiTietSimSo
                            {
                                MaLoaiSimChiTiet = (int)LoaiSimSoChiTiet.TamHoa777
                            });
                            break;
                        case "888":
                            isTamHoa = true;
                            simso.tbl_SimSoDep_LoaiSimChiTietSimSo.Add(new tbl_SimSoDep_LoaiSimChiTietSimSo
                            {
                                MaLoaiSimChiTiet = (int)LoaiSimSoChiTiet.TamHoa888
                            });
                            break;
                        case "999":
                            isTamHoa = true;
                            simso.tbl_SimSoDep_LoaiSimChiTietSimSo.Add(new tbl_SimSoDep_LoaiSimChiTietSimSo
                            {
                                MaLoaiSimChiTiet = (int)LoaiSimSoChiTiet.TamHoa999
                            });
                            break;
                    }
                    if (isTamHoa)
                    {
                        simso.tbl_SimSoDep_SimSoLoaiSim.Add(new tbl_SimSoDep_SimSoLoaiSim
                        {
                            MaLoaiSim = (int)LoaiSimSo.SimTamHoaDon
                        });

                    }
                    var isTuQuy = false;
                    var isDacBiet = false;
                    //Check 4 so cuoi
                    switch (bonSoCuoi)
                    {
                        case "0000":
                            isTuQuy = true;
                            simso.tbl_SimSoDep_LoaiSimChiTietSimSo.Add(new tbl_SimSoDep_LoaiSimChiTietSimSo
                            {
                                MaLoaiSimChiTiet = (int)LoaiSimSoChiTiet.TuQuy0000
                            });
                            break;
                        case "1111":
                            isTuQuy = true;
                            simso.tbl_SimSoDep_LoaiSimChiTietSimSo.Add(new tbl_SimSoDep_LoaiSimChiTietSimSo
                            {
                                MaLoaiSimChiTiet = (int)LoaiSimSoChiTiet.TuQuy1111
                            });
                            break;
                        case "2222":
                            isTuQuy = true;
                            simso.tbl_SimSoDep_LoaiSimChiTietSimSo.Add(new tbl_SimSoDep_LoaiSimChiTietSimSo
                            {
                                MaLoaiSimChiTiet = (int)LoaiSimSoChiTiet.TuQuy2222
                            });
                            break;
                        case "3333":
                            isTuQuy = true;
                            simso.tbl_SimSoDep_LoaiSimChiTietSimSo.Add(new tbl_SimSoDep_LoaiSimChiTietSimSo
                            {
                                MaLoaiSimChiTiet = (int)LoaiSimSoChiTiet.TuQuy3333
                            });
                            break;
                        case "4444":
                            isTuQuy = true;
                            simso.tbl_SimSoDep_LoaiSimChiTietSimSo.Add(new tbl_SimSoDep_LoaiSimChiTietSimSo
                            {
                                MaLoaiSimChiTiet = (int)LoaiSimSoChiTiet.TuQuy4444
                            });
                            break;
                        case "5555":
                            isTuQuy = true;
                            simso.tbl_SimSoDep_LoaiSimChiTietSimSo.Add(new tbl_SimSoDep_LoaiSimChiTietSimSo
                            {
                                MaLoaiSimChiTiet = (int)LoaiSimSoChiTiet.TuQuy5555
                            });
                            break;
                        case "6666":
                            isTuQuy = true;
                            simso.tbl_SimSoDep_LoaiSimChiTietSimSo.Add(new tbl_SimSoDep_LoaiSimChiTietSimSo
                            {
                                MaLoaiSimChiTiet = (int)LoaiSimSoChiTiet.TuQuy6666
                            });
                            break;
                        case "7777":
                            isTuQuy = true;
                            simso.tbl_SimSoDep_LoaiSimChiTietSimSo.Add(new tbl_SimSoDep_LoaiSimChiTietSimSo
                            {
                                MaLoaiSimChiTiet = (int)LoaiSimSoChiTiet.TuQuy7777
                            });
                            break;
                        case "8888":
                            isTuQuy = true;
                            simso.tbl_SimSoDep_LoaiSimChiTietSimSo.Add(new tbl_SimSoDep_LoaiSimChiTietSimSo
                            {
                                MaLoaiSimChiTiet = (int)LoaiSimSoChiTiet.TuQuy8888
                            });
                            break;
                        case "9999":
                            isTuQuy = true;
                            simso.tbl_SimSoDep_LoaiSimChiTietSimSo.Add(new tbl_SimSoDep_LoaiSimChiTietSimSo
                            {
                                MaLoaiSimChiTiet = (int)LoaiSimSoChiTiet.TuQuy9999
                            });
                            break;
                        case "1102":
                            simso.tbl_SimSoDep_LoaiSimChiTietSimSo.Add(new tbl_SimSoDep_LoaiSimChiTietSimSo
                            {
                                MaLoaiSimChiTiet = (int)LoaiSimSoChiTiet.NhatNhatKhongNhi
                            });
                            isDacBiet = true;
                            break;
                        case "4078":
                            simso.tbl_SimSoDep_LoaiSimChiTietSimSo.Add(new tbl_SimSoDep_LoaiSimChiTietSimSo
                            {
                                MaLoaiSimChiTiet = (int)LoaiSimSoChiTiet.BonMuaKhongThatBat
                            });
                            isDacBiet = true;
                            break;
                        case "1486":
                            simso.tbl_SimSoDep_LoaiSimChiTietSimSo.Add(new tbl_SimSoDep_LoaiSimChiTietSimSo
                            {
                                MaLoaiSimChiTiet = (int)LoaiSimSoChiTiet.MotNamBonMuaLocPhat
                            });
                            isDacBiet = true;
                            break;
                        case "2204":
                            simso.tbl_SimSoDep_LoaiSimChiTietSimSo.Add(new tbl_SimSoDep_LoaiSimChiTietSimSo
                            {
                                MaLoaiSimChiTiet = (int)LoaiSimSoChiTiet.MaiMaiKhongChet
                            });
                            isDacBiet = true;
                            break;
                        case "0578":
                            simso.tbl_SimSoDep_LoaiSimChiTietSimSo.Add(new tbl_SimSoDep_LoaiSimChiTietSimSo
                            {
                                MaLoaiSimChiTiet = (int)LoaiSimSoChiTiet.KhongNamNaoThatBat
                            });
                            isDacBiet = true;
                            break;
                        case "1368":
                            simso.tbl_SimSoDep_LoaiSimChiTietSimSo.Add(new tbl_SimSoDep_LoaiSimChiTietSimSo
                            {
                                MaLoaiSimChiTiet = (int)LoaiSimSoChiTiet.SinhTaiLocPhat
                            });
                            isDacBiet = true;
                            break;
                        case "3456":
                            simso.tbl_SimSoDep_LoaiSimChiTietSimSo.Add(new tbl_SimSoDep_LoaiSimChiTietSimSo
                            {
                                MaLoaiSimChiTiet = (int)LoaiSimSoChiTiet.BanBeNeSo
                            });
                            isDacBiet = true;
                            break;
                        case "6789":
                            simso.tbl_SimSoDep_LoaiSimChiTietSimSo.Add(new tbl_SimSoDep_LoaiSimChiTietSimSo
                            {
                                MaLoaiSimChiTiet = (int)LoaiSimSoChiTiet.SanBangTatCa
                            });
                            isDacBiet = true;
                            break;
                        case "8910":
                            simso.tbl_SimSoDep_LoaiSimChiTietSimSo.Add(new tbl_SimSoDep_LoaiSimChiTietSimSo
                            {
                                MaLoaiSimChiTiet = (int)LoaiSimSoChiTiet.CaoHonNguoi
                            });
                            isDacBiet = true;
                            break;
                    }
                    //check dac biet
                    if (namSoCuoi == "01234")
                    {
                        simso.tbl_SimSoDep_LoaiSimChiTietSimSo.Add(new tbl_SimSoDep_LoaiSimChiTietSimSo
                        {
                            MaLoaiSimChiTiet = (int)LoaiSimSoChiTiet.TayTrangDiLen
                        });
                        isDacBiet = true;
                    }
                    //Check sau so cuoi
                    switch (sauSoCuoi)
                    {
                        case "049053":
                            simso.tbl_SimSoDep_LoaiSimChiTietSimSo.Add(new tbl_SimSoDep_LoaiSimChiTietSimSo
                            {
                                MaLoaiSimChiTiet = (int)LoaiSimSoChiTiet.KhongGapHan
                            });
                            isDacBiet = true;
                            break;
                        case "181818":
                            simso.tbl_SimSoDep_LoaiSimChiTietSimSo.Add(new tbl_SimSoDep_LoaiSimChiTietSimSo
                            {
                                MaLoaiSimChiTiet = (int)LoaiSimSoChiTiet.MoiNamMotPhat
                            });
                            isDacBiet = true;
                            break;
                        case "191919":
                            simso.tbl_SimSoDep_LoaiSimChiTietSimSo.Add(new tbl_SimSoDep_LoaiSimChiTietSimSo
                            {
                                MaLoaiSimChiTiet = (int)LoaiSimSoChiTiet.MotBuocLenTroi
                            });
                            isDacBiet = true;
                            break;
                        case "151618":
                            simso.tbl_SimSoDep_LoaiSimChiTietSimSo.Add(new tbl_SimSoDep_LoaiSimChiTietSimSo
                            {
                                MaLoaiSimChiTiet = (int)LoaiSimSoChiTiet.MoiNamMoiLocMoiPhat
                            });
                            isDacBiet = true;
                            break;
                    }
                    //Check tu quy
                    if (isTuQuy)
                    {
                        simso.tbl_SimSoDep_SimSoLoaiSim.Add(new tbl_SimSoDep_SimSoLoaiSim
                        {
                            MaLoaiSim = (int)LoaiSimSo.SimTuQuy
                        });

                    }

                    //Check ngu quy
                    if (SimSoHelper.CheckSimTuNguLucQuy(namSoCuoi))
                    {
                        simso.tbl_SimSoDep_SimSoLoaiSim.Add(new tbl_SimSoDep_SimSoLoaiSim
                        {
                            MaLoaiSim = (int)LoaiSimSo.SimNguQuy
                        });
                    }
                    //Check sim luc quy
                    if (SimSoHelper.CheckSimTuNguLucQuy(sauSoCuoi))
                    {

                        simso.tbl_SimSoDep_SimSoLoaiSim.Add(new tbl_SimSoDep_SimSoLoaiSim
                        {
                            MaLoaiSim = (int)LoaiSimSo.SimLucQuy
                        });
                    }
                    //Check sim ganh
                    if (SimSoHelper.CheckSimGanh(baSoCuoi))
                    {
                        simso.tbl_SimSoDep_SimSoLoaiSim.Add(new tbl_SimSoDep_SimSoLoaiSim
                        {
                            MaLoaiSim = (int)LoaiSimSo.SimGanh
                        });
                    }
                    //Check sim loc phat
                    if (haiSoCuoi == "68")
                    {
                        simso.tbl_SimSoDep_SimSoLoaiSim.Add(new tbl_SimSoDep_SimSoLoaiSim
                        {
                            MaLoaiSim = (int)LoaiSimSo.SimLocPhat
                        });
                    }
                    //Sim ong dia
                    if (haiSoCuoi == "38" || haiSoCuoi == "78")
                    {
                        simso.tbl_SimSoDep_SimSoLoaiSim.Add(new tbl_SimSoDep_SimSoLoaiSim
                        {
                            MaLoaiSim = (int)LoaiSimSo.SimOngDia
                        });
                    }
                    //Sim than tai
                    if (haiSoCuoi == "39" || haiSoCuoi == "79")
                    {
                        simso.tbl_SimSoDep_SimSoLoaiSim.Add(new tbl_SimSoDep_SimSoLoaiSim
                        {
                            MaLoaiSim = (int)LoaiSimSo.SimThanTai
                        });
                    }
                    //Check so tien don
                    //Check tien 6 so cuoi
                    var isAddSimTienDon = false;
                    if (SimSoHelper.CheckSoTien(sauSoCuoi))
                    {
                        simso.tbl_SimSoDep_LoaiSimChiTietSimSo.Add(new tbl_SimSoDep_LoaiSimChiTietSimSo
                        {
                            MaLoaiSimChiTiet = (int)LoaiSimSoChiTiet.Tien6SoCuoi
                        });
                        isAddSimTienDon = true;
                    }
                    //Check tien 5 so cuoi
                    if (SimSoHelper.CheckSoTien(namSoCuoi))
                    {
                        simso.tbl_SimSoDep_LoaiSimChiTietSimSo.Add(new tbl_SimSoDep_LoaiSimChiTietSimSo
                        {
                            MaLoaiSimChiTiet = (int)LoaiSimSoChiTiet.Tien5SoCuoi
                        });
                        isAddSimTienDon = true;
                    }
                    //Tien 4 so cuoi
                    if (SimSoHelper.CheckSoTien(bonSoCuoi))
                    {
                        simso.tbl_SimSoDep_LoaiSimChiTietSimSo.Add(new tbl_SimSoDep_LoaiSimChiTietSimSo
                        {
                            MaLoaiSimChiTiet = (int)LoaiSimSoChiTiet.Tien4SoCuoi
                        });
                        isAddSimTienDon = true;
                    }
                    //Tien 3 so cuoi
                    if (SimSoHelper.CheckSoTien(baSoCuoi))
                    {
                        simso.tbl_SimSoDep_LoaiSimChiTietSimSo.Add(new tbl_SimSoDep_LoaiSimChiTietSimSo
                        {
                            MaLoaiSimChiTiet = (int)LoaiSimSoChiTiet.Tien3SoCuoi
                        });
                        isAddSimTienDon = true;
                    }
                    //Check tien dac biet
                    if (SimSoHelper.CheckSoTienDonDacBiet(bonSoCuoi))
                    {
                        simso.tbl_SimSoDep_LoaiSimChiTietSimSo.Add(new tbl_SimSoDep_LoaiSimChiTietSimSo
                        {
                            MaLoaiSimChiTiet = (int)LoaiSimSoChiTiet.TienDacBiet
                        });
                        isAddSimTienDon = true;
                    }
                    //Check tien don khac
                    if (SimSoHelper.CheckSoTienDonKhac(bonSoCuoi))
                    {
                        simso.tbl_SimSoDep_LoaiSimChiTietSimSo.Add(new tbl_SimSoDep_LoaiSimChiTietSimSo
                        {
                            MaLoaiSimChiTiet = (int)LoaiSimSoChiTiet.TienDonKhac
                        });
                        isAddSimTienDon = true;
                    }
                    //Neu la tien don add parent category
                    if (isAddSimTienDon)
                    {
                        simso.tbl_SimSoDep_SimSoLoaiSim.Add(new tbl_SimSoDep_SimSoLoaiSim
                        {
                            MaLoaiSim = (int)LoaiSimSo.SimTienDon
                        });
                    }

                    //Check so tien doi
                    var isAddSimTienDoi = false;
                    if (SimSoHelper.CheckSoTienDoi3(sauSoCuoi))
                    {
                        simso.tbl_SimSoDep_LoaiSimChiTietSimSo.Add(new tbl_SimSoDep_LoaiSimChiTietSimSo
                        {
                            MaLoaiSimChiTiet = (int)LoaiSimSoChiTiet.Tien3DuoiCuoi
                        });
                        isAddSimTienDoi = true;
                    }
                    if (SimSoHelper.CheckSoTienDoi(bonSoCuoi))
                    {
                        simso.tbl_SimSoDep_LoaiSimChiTietSimSo.Add(new tbl_SimSoDep_LoaiSimChiTietSimSo
                        {
                            MaLoaiSimChiTiet = (int)LoaiSimSoChiTiet.Tieng2DuoiCuoi
                        });
                        isAddSimTienDoi = true;

                    }
                    if (SimSoHelper.CheckSoTienDoiKhac(sauSoCuoi))
                    {
                        simso.tbl_SimSoDep_LoaiSimChiTietSimSo.Add(new tbl_SimSoDep_LoaiSimChiTietSimSo
                        {
                            MaLoaiSimChiTiet = (int)LoaiSimSoChiTiet.TienDoiKhac
                        });
                        isAddSimTienDoi = true;
                    }
                    if (isAddSimTienDoi)
                    {
                        simso.tbl_SimSoDep_SimSoLoaiSim.Add(new tbl_SimSoDep_SimSoLoaiSim
                        {
                            MaLoaiSim = (int)LoaiSimSo.SimTienDoi
                        });
                    }
                    //Check tam hoa kep
                    if (SimSoHelper.CheckTamHoaKep(sauSoCuoi))
                    {
                        simso.tbl_SimSoDep_SimSoLoaiSim.Add(new tbl_SimSoDep_SimSoLoaiSim
                        {
                            MaLoaiSim = (int)LoaiSimSo.SimTamHoaKep
                        });
                    }
                    //Check so kep
                    //Check so kep AABB
                    var isAddSoKep = false;
                    if (SimSoHelper.CheckSoKepAABB(bonSoCuoi))
                    {
                        simso.tbl_SimSoDep_LoaiSimChiTietSimSo.Add(new tbl_SimSoDep_LoaiSimChiTietSimSo
                        {
                            MaLoaiSimChiTiet = (int)LoaiSimSoChiTiet.KepAABB
                        });
                        isAddSoKep = true;
                    }
                    //So kep AABBAA
                    if (SimSoHelper.CheckSoKepAABBAA(sauSoCuoi))
                    {
                        simso.tbl_SimSoDep_LoaiSimChiTietSimSo.Add(new tbl_SimSoDep_LoaiSimChiTietSimSo
                        {
                            MaLoaiSimChiTiet = (int)LoaiSimSoChiTiet.KepAABBAA
                        });
                        isAddSoKep = true;
                    }
                    //So kep AABBAACC
                    if (SimSoHelper.CheckSoKepAABBAACC(tamSoCuoi))
                    {
                        simso.tbl_SimSoDep_LoaiSimChiTietSimSo.Add(new tbl_SimSoDep_LoaiSimChiTietSimSo
                        {
                            MaLoaiSimChiTiet = (int)LoaiSimSoChiTiet.KepAABBAACC
                        });
                        isAddSoKep = true;
                    }
                    //So kep AABBCC
                    if (SimSoHelper.CheckSoKepAABBCC(sauSoCuoi))
                    {
                        simso.tbl_SimSoDep_LoaiSimChiTietSimSo.Add(new tbl_SimSoDep_LoaiSimChiTietSimSo
                        {
                            MaLoaiSimChiTiet = (int)LoaiSimSoChiTiet.KepAABBCC
                        });
                        isAddSoKep = true;
                    }
                    //So kep AABBCCBB
                    if (SimSoHelper.CheckSoKepAABBCCBB(tamSoCuoi))
                    {
                        simso.tbl_SimSoDep_LoaiSimChiTietSimSo.Add(new tbl_SimSoDep_LoaiSimChiTietSimSo
                        {
                            MaLoaiSimChiTiet = (int)LoaiSimSoChiTiet.KepAABBCCBB
                        });
                        isAddSoKep = true;
                    }
                    //So kep AABBCCDD
                    if (SimSoHelper.CheckSoKepAABBCCDD(tamSoCuoi))
                    {
                        simso.tbl_SimSoDep_LoaiSimChiTietSimSo.Add(new tbl_SimSoDep_LoaiSimChiTietSimSo
                        {
                            MaLoaiSimChiTiet = (int)LoaiSimSoChiTiet.KepAABBCCDD
                        });
                        isAddSoKep = true;
                    }
                    if (SimSoHelper.CheckSoKepSaoAABBSao(tamSoCuoi))
                    {
                        simso.tbl_SimSoDep_LoaiSimChiTietSimSo.Add(new tbl_SimSoDep_LoaiSimChiTietSimSo
                        {
                            MaLoaiSimChiTiet = (int)LoaiSimSoChiTiet.KepSaoAABBSao
                        });
                        isAddSoKep = true;
                    }
                    if (isAddSoKep)
                    {
                        simso.tbl_SimSoDep_SimSoLoaiSim.Add(new tbl_SimSoDep_SimSoLoaiSim
                        {
                            MaLoaiSim = (int)LoaiSimSo.SimKep
                        });
                    }
                    var isAddSoKepKhac = false;
                    //So kep AAXAAY
                    if (SimSoHelper.CheckSoKepAAXAAY(sauSoCuoi))
                    {
                        simso.tbl_SimSoDep_LoaiSimChiTietSimSo.Add(new tbl_SimSoDep_LoaiSimChiTietSimSo
                        {
                            MaLoaiSimChiTiet = (int)LoaiSimSoChiTiet.KepAAXAAY
                        });
                        isAddSoKepKhac = true;
                    }
                    //So kep AAXBBY
                    if (SimSoHelper.CheckSoKepAAXBBY(sauSoCuoi))
                    {
                        simso.tbl_SimSoDep_LoaiSimChiTietSimSo.Add(new tbl_SimSoDep_LoaiSimChiTietSimSo
                        {
                            MaLoaiSimChiTiet = (int)LoaiSimSoChiTiet.KepAAXBBY
                        });
                        isAddSoKepKhac = true;
                    }
                    //So kep AAXYAA
                    if (SimSoHelper.CheckSoKepAAXYAA(sauSoCuoi))
                    {
                        simso.tbl_SimSoDep_LoaiSimChiTietSimSo.Add(new tbl_SimSoDep_LoaiSimChiTietSimSo
                        {
                            MaLoaiSimChiTiet = (int)LoaiSimSoChiTiet.KepAAXYAA
                        });
                        isAddSoKepKhac = true;
                    }
                    if (isAddSoKepKhac)
                    {
                        simso.tbl_SimSoDep_SimSoLoaiSim.Add(new tbl_SimSoDep_SimSoLoaiSim
                        {
                            MaLoaiSim = (int)LoaiSimSo.SimKepKhac
                        });
                    }
                    //Checksolap
                    //Check so lap ABAB
                    if (SimSoHelper.CheckSoLapABAB(bonSoCuoi))
                    {
                        simso.tbl_SimSoDep_SimSoLoaiSim.Add(new tbl_SimSoDep_SimSoLoaiSim
                        {
                            MaLoaiSim = (int)LoaiSimSo.SimLap
                        });
                    }
                    var isSolapKhac = false;
                    //Check so lao ABXABY
                    if (SimSoHelper.CheckSoLapABXABY(sauSoCuoi))
                    {
                        simso.tbl_SimSoDep_LoaiSimChiTietSimSo.Add(new tbl_SimSoDep_LoaiSimChiTietSimSo
                        {
                            MaLoaiSimChiTiet = (int)LoaiSimSoChiTiet.LapABXABY
                        });
                        isSolapKhac = true;
                    }
                    //ABXYAB
                    if (SimSoHelper.CheckSoLapABXYAB(sauSoCuoi))
                    {
                        simso.tbl_SimSoDep_LoaiSimChiTietSimSo.Add(new tbl_SimSoDep_LoaiSimChiTietSimSo
                        {
                            MaLoaiSimChiTiet = (int)LoaiSimSoChiTiet.LapABXYAB
                        });
                        isSolapKhac = true;
                    }
                    //AXBAYB
                    if (SimSoHelper.CheckSoLapAXBAYB(sauSoCuoi))
                    {
                        simso.tbl_SimSoDep_LoaiSimChiTietSimSo.Add(new tbl_SimSoDep_LoaiSimChiTietSimSo
                        {
                            MaLoaiSimChiTiet = (int)LoaiSimSoChiTiet.LapAXBAYB
                        });
                        isSolapKhac = true;
                    }
                    //SaoABABSao
                    if (SimSoHelper.CheckSoLapSaoABABSao(tamSoCuoi))
                    {
                        simso.tbl_SimSoDep_LoaiSimChiTietSimSo.Add(new tbl_SimSoDep_LoaiSimChiTietSimSo
                        {
                            MaLoaiSimChiTiet = (int)LoaiSimSoChiTiet.LapSaoABABSao
                        });
                        isSolapKhac = true;
                    }
                    //XABCYABC
                    if (SimSoHelper.CheckSoLapXABCYABC(tamSoCuoi))
                    {
                        simso.tbl_SimSoDep_LoaiSimChiTietSimSo.Add(new tbl_SimSoDep_LoaiSimChiTietSimSo
                        {
                            MaLoaiSimChiTiet = (int)LoaiSimSoChiTiet.LapXABCYABC
                        });
                        isSolapKhac = true;
                    }
                    //XABYAB
                    if (SimSoHelper.CheckSoLapXABYAB(sauSoCuoi))
                    {
                        simso.tbl_SimSoDep_LoaiSimChiTietSimSo.Add(new tbl_SimSoDep_LoaiSimChiTietSimSo
                        {
                            MaLoaiSimChiTiet = (int)LoaiSimSoChiTiet.LapXABYAB
                        });
                        isSolapKhac = true;
                    }
                    if (isSolapKhac)
                    {
                        simso.tbl_SimSoDep_SimSoLoaiSim.Add(new tbl_SimSoDep_SimSoLoaiSim
                        {
                            MaLoaiSim = (int)LoaiSimSo.SimLapKhac
                        });
                    }
                    //Check taxi lap 2
                    if (SimSoHelper.CheckTaxiLap2(sauSoCuoi))
                    {
                        simso.tbl_SimSoDep_SimSoLoaiSim.Add(new tbl_SimSoDep_SimSoLoaiSim
                        {
                            MaLoaiSim = (int)LoaiSimSo.SimTaxiLap2
                        });
                    }
                    //Check taxiLap3
                    //AABAAB
                    var isTaxiLap3 = false;
                    if (SimSoHelper.CheckTaxi3AABAAB(sauSoCuoi))
                    {
                        simso.tbl_SimSoDep_LoaiSimChiTietSimSo.Add(new tbl_SimSoDep_LoaiSimChiTietSimSo
                        {
                            MaLoaiSimChiTiet = (int)LoaiSimSoChiTiet.TaxiAABAAB
                        });
                        isTaxiLap3 = true;
                    }
                    //ABAABA
                    if (SimSoHelper.CheckTaxi3ABAABA(sauSoCuoi))
                    {
                        simso.tbl_SimSoDep_LoaiSimChiTietSimSo.Add(new tbl_SimSoDep_LoaiSimChiTietSimSo
                        {
                            MaLoaiSimChiTiet = (int)LoaiSimSoChiTiet.TaxiABAABA
                        });
                        isTaxiLap3 = true;
                    }
                    //ABCABC
                    if (SimSoHelper.CheckTaxi3ABCABC(sauSoCuoi))
                    {
                        simso.tbl_SimSoDep_LoaiSimChiTietSimSo.Add(new tbl_SimSoDep_LoaiSimChiTietSimSo
                        {
                            MaLoaiSimChiTiet = (int)LoaiSimSoChiTiet.TaxiABCABC
                        });
                        isTaxiLap3 = true;
                    }
                    //BAABAA
                    if (SimSoHelper.CheckTaxi3BAABAA(sauSoCuoi))
                    {
                        simso.tbl_SimSoDep_LoaiSimChiTietSimSo.Add(new tbl_SimSoDep_LoaiSimChiTietSimSo
                        {
                            MaLoaiSimChiTiet = (int)LoaiSimSoChiTiet.TaxiBAABAA
                        });
                        isTaxiLap3 = true;
                    }
                    if (isTaxiLap3)
                    {
                        simso.tbl_SimSoDep_SimSoLoaiSim.Add(new tbl_SimSoDep_SimSoLoaiSim
                        {
                            MaLoaiSim = (int)LoaiSimSo.SimTaxiLap3
                        });
                    }
                    var isTaxiLap4 = false;
                    //Check taxi lap 4
                    //AABBAABB
                    if (SimSoHelper.CheckTaxi4AABBAABB(tamSoCuoi))
                    {
                        simso.tbl_SimSoDep_LoaiSimChiTietSimSo.Add(new tbl_SimSoDep_LoaiSimChiTietSimSo
                        {
                            MaLoaiSimChiTiet = (int)LoaiSimSoChiTiet.TaxiAABBAABB
                        });
                        isTaxiLap4 = true;
                    }
                    //AABCAABC
                    if (SimSoHelper.CheckTaxi4AABCAABC(tamSoCuoi))
                    {
                        simso.tbl_SimSoDep_LoaiSimChiTietSimSo.Add(new tbl_SimSoDep_LoaiSimChiTietSimSo
                        {
                            MaLoaiSimChiTiet = (int)LoaiSimSoChiTiet.TaxiAABCAABC
                        });
                        isTaxiLap4 = true;
                    }
                    //ABBCABBC
                    if (SimSoHelper.CheckTaxi4ABBCABBC(tamSoCuoi))
                    {
                        simso.tbl_SimSoDep_LoaiSimChiTietSimSo.Add(new tbl_SimSoDep_LoaiSimChiTietSimSo
                        {
                            MaLoaiSimChiTiet = (int)LoaiSimSoChiTiet.TaxiABBCABBC
                        });
                        isTaxiLap4 = true;
                    }
                    //ABCCABCC
                    if (SimSoHelper.CheckTaxi4ABCCABCC(tamSoCuoi))
                    {
                        simso.tbl_SimSoDep_LoaiSimChiTietSimSo.Add(new tbl_SimSoDep_LoaiSimChiTietSimSo
                        {
                            MaLoaiSimChiTiet = (int)LoaiSimSoChiTiet.TaxiABCCABCC
                        });
                        isTaxiLap4 = true;
                    }
                    //ABCDABCD
                    if (SimSoHelper.CheckTaxi4ABCDABCD(tamSoCuoi))
                    {
                        simso.tbl_SimSoDep_LoaiSimChiTietSimSo.Add(new tbl_SimSoDep_LoaiSimChiTietSimSo
                        {
                            MaLoaiSimChiTiet = (int)LoaiSimSoChiTiet.TaxiABCDABCD
                        });
                        isTaxiLap4 = true;
                    }
                    if (isTaxiLap4)
                    {
                        simso.tbl_SimSoDep_SimSoLoaiSim.Add(new tbl_SimSoDep_SimSoLoaiSim
                        {
                            MaLoaiSim = (int)LoaiSimSo.SimTaxiLap4
                        });
                    }
                    var isAddSimDao = false;
                    //check sim dao
                    //
                    if (SimSoHelper.CheckSimDaoABBA(bonSoCuoi))
                    {
                        simso.tbl_SimSoDep_LoaiSimChiTietSimSo.Add(new tbl_SimSoDep_LoaiSimChiTietSimSo
                        {
                            MaLoaiSimChiTiet = (int)LoaiSimSoChiTiet.DaoDon
                        });
                        isAddSimDao = true;
                    }
                    //Dao kep
                    if (SimSoHelper.CheckSimDaoKep(tamSoCuoi))
                    {
                        simso.tbl_SimSoDep_LoaiSimChiTietSimSo.Add(new tbl_SimSoDep_LoaiSimChiTietSimSo
                        {
                            MaLoaiSimChiTiet = (int)LoaiSimSoChiTiet.DaoKep
                        });
                        isAddSimDao = true;
                    }
                    if (isAddSimDao)
                    {
                        simso.tbl_SimSoDep_SimSoLoaiSim.Add(new tbl_SimSoDep_SimSoLoaiSim
                        {
                            MaLoaiSim = (int)LoaiSimSo.SimDao
                        });
                    }
                    //Check sim doi
                    if (SimSoHelper.CheckSimDoiABCCBA(sauSoCuoi))
                    {
                        simso.tbl_SimSoDep_SimSoLoaiSim.Add(new tbl_SimSoDep_SimSoLoaiSim
                        {
                            MaLoaiSim = (int)LoaiSimSo.SimDoi
                        });
                    }

                    //Tinh tien
                    if (daily != null)
                    {
                        foreach (var itemTang in daily.tbl_SimSoDep_TangThem)
                        {
                            if (giaTien >= itemTang.GiaTu && giaTien <= itemTang.Den)
                            {
                                giaTien = giaTien + ((itemTang.TangThem ?? 0) * giaTien) / 100;
                                break;
                            }
                        }
                    }
                    simso.GiaTien = giaTien;
                    if (giaTien < 1000000)
                    {
                        simso.tbl_SimSoDep_SimSoLoaiSim.Add(new tbl_SimSoDep_SimSoLoaiSim
                        {
                            MaLoaiSim = (int)LoaiSimSo.SimGiaRe
                        });
                    }
                    if (giaTien > 500000000)
                    {
                        simso.tbl_SimSoDep_LoaiSimChiTietSimSo.Add(new tbl_SimSoDep_LoaiSimChiTietSimSo
                        {
                            MaLoaiSimChiTiet = (int)LoaiSimSoChiTiet.SimVip
                        });
                        isDacBiet = true;
                    }
                    //Check sim dac biet
                    if (isDacBiet)
                    {
                        simso.tbl_SimSoDep_SimSoLoaiSim.Add(new tbl_SimSoDep_SimSoLoaiSim
                        {
                            MaLoaiSim = (int)LoaiSimSo.SimDacBiet
                        });
                    }
                    //loai gia
                    var maLoaiGia = (from lg in listLoaiGia where lg.GiaTu <= giaTien && giaTien <= lg.Den select lg.MaLoaiGia).FirstOrDefault();
                    simso.MaLoaiGia = maLoaiGia;
                    //Add sim so to entities
                    _entities.tbl_SimSoDep_SimSo.AddObject(simso);
                    count++;
                }
                _entities.SaveChanges();
                return count;
            }
            catch (Exception)
            {
                return 0;
            }
        }

        #endregion

        #region nhamang

        public List<NhaMangModel> GetMenuNhaMang(bool? onlyShowHienThi = null)
        {
            var loaiSim = (onlyShowHienThi != null && onlyShowHienThi == true) ? _entities.tbl_SimSoDep_NhaMang.Where(l => l.Status == (int)DauSoStatus.HienThi).ToList().OrderBy(l => l.SapXep) : _entities.tbl_SimSoDep_NhaMang.ToList().OrderBy(l => l.SapXep);
            var listLoaiSim = new List<NhaMangModel>();
            foreach (var loai in loaiSim)
            {
                listLoaiSim.Add(new NhaMangModel
                {
                    Id = loai.MaNhaMang,
                    Ten = loai.TenMoTaDayDu,
                    Alias = loai.Alias,
                    EnumFixed = loai.EnumFixed,
                    MoTa = loai.MoTa,
                    TenStatus = (loai.Status == (int)DauSoStatus.HienThi) ? "Hiển thị" : "Không hiển thị"
                });
            }
            return listLoaiSim;
        }

        public NhaMangModel GetNhaMang(int id)
        {
            var nhamang = (from ls in _entities.tbl_SimSoDep_NhaMang
                           where ls.MaNhaMang == id
                           select new NhaMangModel
                           {
                               Id = ls.MaNhaMang,
                               Ten = ls.TenNhaMang,
                               TenDayDu = ls.TenMoTaDayDu,
                               Alias = ls.Alias,
                               EnumFixed = ls.EnumFixed,
                               SapXep = ls.SapXep,
                               Status = ls.Status,
                               MetaKeyword = ls.MetaKeyword,
                               MetaDescription = ls.MetaDescription
                           }).FirstOrDefault();
            return nhamang;
        }

        public bool SaveNhaMang(NhaMangModel nhaMangModel)
        {
            var nhamang = _entities.tbl_SimSoDep_NhaMang.Where(l => l.MaNhaMang == nhaMangModel.Id).FirstOrDefault();
            if (nhamang == null)
            {
                return false;
            }
            nhamang.TenNhaMang = nhaMangModel.Ten;
            nhamang.TenMoTaDayDu = nhaMangModel.TenDayDu;
            nhamang.Alias = nhaMangModel.Alias;
            nhamang.EnumFixed = nhaMangModel.EnumFixed;
            nhamang.Status = nhaMangModel.Status;
            nhamang.SapXep = nhaMangModel.SapXep;
            nhamang.MetaDescription = nhaMangModel.MetaDescription;
            nhamang.MetaKeyword = nhaMangModel.MetaKeyword;
            _entities.SaveChanges();
            return true;
        }

        public bool DeleteNhaMang(int id)
        {
            var nhamang = _entities.tbl_SimSoDep_NhaMang.Where(n => n.MaNhaMang == id).FirstOrDefault();
            if (nhamang == null)
                return false;
            _entities.tbl_SimSoDep_NhaMang.DeleteObject(nhamang);
            _entities.SaveChanges();
            return true;
        }

        public bool AddNhaMang(NhaMangModel nhaMangModel)
        {
            _entities.tbl_SimSoDep_NhaMang.AddObject(new tbl_SimSoDep_NhaMang
                                                         {
                                                             EnumFixed = nhaMangModel.EnumFixed,
                                                             Alias = nhaMangModel.Alias,
                                                             TenMoTaDayDu = nhaMangModel.TenDayDu,
                                                             TenNhaMang = nhaMangModel.Ten,
                                                             Status = nhaMangModel.Status,
                                                             SapXep = nhaMangModel.SapXep,
                                                             MetaKeyword = nhaMangModel.MetaKeyword,
                                                             MetaDescription = nhaMangModel.MetaDescription
                                                         });
            _entities.SaveChanges();
            return true;
        }

        #endregion

        #region simso

        public List<SimSoModel> GetListSimSoByLoaiGia(string id)
        {
            var idLoaiNhaMang =
               _entities.tbl_SimSoDep_LoaiGia.Where(ls => ls.Alias == id).Select(ls => ls.MaLoaiGia).FirstOrDefault();
            var listSim = (from s in _entities.tbl_SimSoDep_SimSo
                           where s.MaLoaiGia == idLoaiNhaMang
                           select new SimSoModel
                           {
                               MaSoDienThoai = s.MaSoDienThoai,
                               SoDienThoai = s.DinhDangHienThi,
                               GiaTien = s.GiaTien ?? 0,
                               NhaMang = s.tbl_SimSoDep_NhaMang.TenNhaMang
                           }).ToList();
            return listSim;
        }

        public List<SimSoModel> GetListSimSoByNhaMang(string id)
        {
            var idLoaiNhaMang =
               _entities.tbl_SimSoDep_NhaMang.Where(ls => ls.Alias == id).Select(ls => ls.MaNhaMang).FirstOrDefault();
            var listSim = (from s in _entities.tbl_SimSoDep_SimSo
                           where s.MaNhaMang == idLoaiNhaMang
                           select new SimSoModel
                           {
                               MaSoDienThoai = s.MaSoDienThoai,
                               SoDienThoai = s.DinhDangHienThi,
                               GiaTien = s.GiaTien ?? 0,
                               NhaMang = s.tbl_SimSoDep_NhaMang.TenNhaMang
                           }).ToList();
            return listSim;
        }

        public List<SimSoModel> GetListSimSoByDauSo(string id)
        {
            var idLoaiDauSo =
                _entities.tbl_SimSoDep_DauSo.Where(ls => ls.Alias == id).Select(ls => ls.MaDauSo).FirstOrDefault();
            var listSim = (from s in _entities.tbl_SimSoDep_SimSo
                           where s.MaDauSo == idLoaiDauSo
                           select new SimSoModel
                           {
                               MaSoDienThoai = s.MaSoDienThoai,
                               SoDienThoai = s.DinhDangHienThi,
                               GiaTien = s.GiaTien ?? 0,
                               NhaMang = s.tbl_SimSoDep_NhaMang.TenNhaMang
                           }).ToList();
            return listSim;
        }

        public List<SimSoModel> GetListSimSoByLoaiSim(string id)
        {
            var idLoaiSim =
                _entities.tbl_SimSoDep_LoaiSim.Where(ls => ls.Alias == id).Select(ls => ls.MaLoaiSim).FirstOrDefault();
            var listSim = (from s in _entities.tbl_SimSoDep_SimSoLoaiSim
                           where s.MaLoaiSim == idLoaiSim
                           select new SimSoModel
                           {
                               MaSoDienThoai = s.tbl_SimSoDep_SimSo.MaSoDienThoai,
                               SoDienThoai = s.tbl_SimSoDep_SimSo.DinhDangHienThi,
                               GiaTien = s.tbl_SimSoDep_SimSo.GiaTien ?? 0,
                               NhaMang = s.tbl_SimSoDep_SimSo.tbl_SimSoDep_NhaMang.TenNhaMang
                           }).ToList();
            return listSim;
        }

        public List<SimSoModel> GetListSimSoByLoaiSimChiTiet(string id)
        {
            var idLoaiSim =
                _entities.tbl_SimSoDep_LoaiSimChiTiet.Where(ls => ls.Alias == id).Select(ls => ls.MaLoaiSimChiTiet).FirstOrDefault();
            var listSim = (from s in _entities.tbl_SimSoDep_LoaiSimChiTietSimSo
                           where s.MaLoaiSimChiTiet == idLoaiSim
                           select new SimSoModel
                           {
                               SoDienThoai = s.tbl_SimSoDep_SimSo.DinhDangHienThi,
                               GiaTien = s.tbl_SimSoDep_SimSo.GiaTien ?? 0,
                               NhaMang = s.tbl_SimSoDep_SimSo.tbl_SimSoDep_NhaMang.TenNhaMang
                           }).ToList();
            return listSim;
        }

        public List<SimSoModel> GetListSimSo()
        {
            var listSim = (from s in _entities.tbl_SimSoDep_SimSoLoaiSim
                           select new SimSoModel
                           {
                               MaSoDienThoai = s.tbl_SimSoDep_SimSo.MaSoDienThoai,
                               SoDienThoai = s.tbl_SimSoDep_SimSo.DinhDangHienThi,
                               GiaTien = s.tbl_SimSoDep_SimSo.GiaTien ?? 0,
                               NhaMang = s.tbl_SimSoDep_SimSo.tbl_SimSoDep_NhaMang.TenNhaMang
                           }).ToList();
            return listSim;
        }

        public ViewBagModel GetTenLoaiGia(string id)
        {
            var result = (from ls in _entities.tbl_SimSoDep_LoaiGia
                          where ls.Alias == id
                          select new ViewBagModel
                          {
                              DisplayName = ls.TenLoaiGia,
                              Title = ls.MoTa
                          }).FirstOrDefault();
            return result;
        }

        public ViewBagModel GetTenLoaiSim(string id)
        {
            var result = (from ls in _entities.tbl_SimSoDep_LoaiSim
                          where ls.Alias == id
                          select new ViewBagModel
                                     {
                                         DisplayName = ls.TenLoaiSim,
                                         Title = ls.MoTa
                                     }).FirstOrDefault();
            return result;
        }

        public ViewBagModel GetTenDauSo(string id)
        {
            var result = (from ls in _entities.tbl_SimSoDep_DauSo
                          where ls.Alias == id
                          select new ViewBagModel
                          {
                              DisplayName = ls.TenDauSo,
                              Title = ls.MoTa
                          }).FirstOrDefault();
            return result;
        }

        public ViewBagModel GetTenNhaMang(string id)
        {
            var result = (from ls in _entities.tbl_SimSoDep_NhaMang
                          where ls.Alias == id
                          select new ViewBagModel
                          {
                              DisplayName = ls.TenMoTaDayDu,
                              Title = ls.MoTa
                          }).FirstOrDefault();
            return result;
        }

        public ViewBagModel GetTenLoaiSimChiTiet(string id)
        {
            var result = (from ls in _entities.tbl_SimSoDep_LoaiSimChiTiet
                          where ls.Alias == id
                          select new ViewBagModel
                          {
                              DisplayName = ls.TenLoaiSimChiTiet,
                              Title = ls.MoTa
                          }).FirstOrDefault();
            return result;
        }

        public SimSoModel GetSimSoById(long id)
        {
            var result = (from s in _entities.tbl_SimSoDep_SimSo
                          where s.MaSoDienThoai == id
                          select new SimSoModel
                                     {
                                         MaSoDienThoai = s.MaSoDienThoai,
                                         NhaMang = s.tbl_SimSoDep_NhaMang.TenNhaMang,
                                         SoDienThoai = s.DinhDangHienThi,
                                         GiaTien = s.GiaTien ?? 0,
                                     }).FirstOrDefault();
            return result;
        }

        public bool DatSim(long id, KhachHangModel khachHangModel)
        {
            try
            {
                var khachHang = new tbl_SimSoDep_KhachHang
                                    {
                                        DiaChi = khachHangModel.DiaChi,
                                        Email = khachHangModel.Email,
                                        SoDienThoai = khachHangModel.SoDienThoai,
                                        GhiChu = khachHangModel.GhiChu,
                                        TenKhachHang = khachHangModel.TenKhachHang,
                                        tbl_SimSoDep_DatHang = new Collection<tbl_SimSoDep_DatHang> { new tbl_SimSoDep_DatHang
                                                                                                          {
                                                                                                              MaSoDienThoai = id
                                                                                                          } }
                                    };
                _entities.tbl_SimSoDep_KhachHang.AddObject(khachHang);
                _entities.SaveChanges();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public List<SimSoDaiLyModes> TimKiemSoManager(string so, int? madaily)
        {
            try
            {
                var result = new List<tbl_SimSoDep_SimSo>();
                if (string.IsNullOrEmpty(so))
                {
                    result = _entities.tbl_SimSoDep_SimSo.Where(s => s.MaDaiLy == madaily).ToList();
                }
                else
                {
                    if (madaily == null)
                        result = _entities.tbl_SimSoDep_SimSo.Where(s => s.SoDienThoai.Contains(so)).ToList();
                    else
                    {
                        result = _entities.tbl_SimSoDep_SimSo.Where(s => s.SoDienThoai.Contains(so) && s.MaDaiLy == madaily).ToList();
                    }
                }

                var lstResult = new List<SimSoDaiLyModes>();
                foreach (var r in result)
                {
                    var giaBanDau = r.GiaBanDau ?? 0;
                    //var trietKhau =
                    //    r.tbl_SimSoDep_DaiLy.tbl_SimSoDep_DaiLyTrietKhau.Where(
                    //        t => t.GiaTu <= giaBanDau && t.Den <= giaBanDau).Select(t => t.TrietKhau).FirstOrDefault();

                    var trietKhau = (from t in r.tbl_SimSoDep_DaiLy.tbl_SimSoDep_DaiLyTrietKhau
                                     where t.GiaTu <= giaBanDau && giaBanDau <= t.Den
                                     select t.TrietKhau).FirstOrDefault();
                    var tienHoaHong = 0;
                    if (trietKhau != null)
                    {
                        tienHoaHong = (int)((giaBanDau * trietKhau) / 100);
                    }
                    lstResult.Add(new SimSoDaiLyModes
                                      {
                                          SoDienThoai = r.SoDienThoai,
                                          DaiLyTrietKhau = trietKhau ?? 0,
                                          GiaTien = r.GiaTien.ToString(),
                                          TenDaiLy = r.tbl_SimSoDep_DaiLy.TenDaiLy,
                                          DaiLyThuVe = (int)(giaBanDau - tienHoaHong),
                                          GiaDaiLy = r.GiaBanDau ?? 0,
                                          MinhDuoc = tienHoaHong
                                      });
                }
                return lstResult;
            }
            catch (Exception)
            {
                return new List<SimSoDaiLyModes>();
            }
        }

        #endregion

        #region LoaiSim

        public bool AddLoaiSim(LoaiSimModel loaiSimModel)
        {
            try
            {
                _entities.tbl_SimSoDep_LoaiSim.AddObject(new tbl_SimSoDep_LoaiSim
                {
                    Alias = loaiSimModel.Alias,
                    EnumFixed = loaiSimModel.EnumFixed,
                    TenLoaiSim = loaiSimModel.Ten,
                    MoTa = loaiSimModel.MoTa,
                    Status = loaiSimModel.Status,
                    SapXep = loaiSimModel.SapXep,
                    MetaDescription = loaiSimModel.MetaDescription,
                    MetaKeyword = loaiSimModel.MetaKeyword
                });
                _entities.SaveChanges();
                return true;
            }
            catch (Exception)
            {
                return false;
            }

        }

        public bool DeleteLoaiSimById(int id)
        {
            var loaiSim = _entities.tbl_SimSoDep_LoaiSim.Where(l => l.MaLoaiSim == id).FirstOrDefault();
            if (loaiSim == null)
                return false;
            _entities.tbl_SimSoDep_LoaiSim.DeleteObject(loaiSim);
            _entities.SaveChanges();
            return true;
        }

        public List<LoaiSimModel> GetMenuLoaiSim()
        {
            var loaiSim = _entities.tbl_SimSoDep_LoaiSim.ToList();
            var listLoaiSim = new List<LoaiSimModel>();
            foreach (var loai in loaiSim)
            {
                listLoaiSim.Add(new LoaiSimModel
                {
                    Id = loai.MaLoaiSim,
                    Ten = loai.TenLoaiSim,
                    Alias = loai.Alias,
                    EnumFixed = loai.EnumFixed,
                    ListLoaiSimChiTiet = GetSubMenuLoaiSim(loai)
                });
            }
            return listLoaiSim;
        }

        public List<LoaiSimModel> GetListLoaiSimSo()
        {
            var result = (from tblSimSoDepLoaiSim in _entities.tbl_SimSoDep_LoaiSim
                          orderby tblSimSoDepLoaiSim.SapXep
                          select new LoaiSimModel
                                     {
                                         Id = tblSimSoDepLoaiSim.MaLoaiSim,
                                         Ten = tblSimSoDepLoaiSim.TenLoaiSim,
                                         Alias = tblSimSoDepLoaiSim.Alias,
                                         EnumFixed = tblSimSoDepLoaiSim.EnumFixed,
                                         MoTa = tblSimSoDepLoaiSim.MoTa,
                                         Status = tblSimSoDepLoaiSim.Status,
                                         SapXep = tblSimSoDepLoaiSim.SapXep
                                     }).ToList();
            foreach (var ls in result)
            {
                switch (ls.Status)
                {
                    case (int)DauSoStatus.HienThi:
                        ls.TenStatus = "Hiển thị";
                        break;
                    case (int)DauSoStatus.KhongHienThi:
                        ls.TenStatus = "Không hiển thị";
                        break;
                }
            }
            return result;
        }

        public bool SaveLoaiSimSo(LoaiSimModel loaiSimSoModel)
        {
            var loaisimso = _entities.tbl_SimSoDep_LoaiSim.Where(l => l.MaLoaiSim == loaiSimSoModel.Id).FirstOrDefault();
            if (loaisimso == null)
            {
                return false;
            }
            loaisimso.TenLoaiSim = loaiSimSoModel.Ten;
            loaisimso.Alias = loaiSimSoModel.Alias;
            loaisimso.EnumFixed = loaiSimSoModel.EnumFixed;
            loaisimso.MoTa = loaiSimSoModel.MoTa;
            loaisimso.SapXep = loaiSimSoModel.SapXep;
            loaisimso.Status = loaiSimSoModel.Status;
            loaisimso.MetaDescription = loaiSimSoModel.MetaDescription;
            loaisimso.MetaKeyword = loaiSimSoModel.MetaKeyword;
            _entities.SaveChanges();
            return true;
        }

        public LoaiSimModel GetLoaiSimById(int id)
        {
            var loaisimso = (from ls in _entities.tbl_SimSoDep_LoaiSim
                             where ls.MaLoaiSim == id
                             select new LoaiSimModel
                                        {
                                            Id = ls.MaLoaiSim,
                                            Ten = ls.TenLoaiSim,
                                            Alias = ls.Alias,
                                            EnumFixed = ls.EnumFixed,
                                            MoTa = ls.MoTa,
                                            Status = ls.Status,
                                            SapXep = ls.SapXep,
                                            MetaKeyword = ls.MetaKeyword,
                                            MetaDescription = ls.MetaDescription
                                        }).FirstOrDefault();
            return loaisimso;
        }

        private List<LoaiSimModel> GetSubMenuLoaiSim(tbl_SimSoDep_LoaiSim tblLoaiSim)
        {
            var listChiTietLoaiSim = tblLoaiSim.tbl_SimSoDep_LoaiSimChiTiet;
            if (listChiTietLoaiSim != null && listChiTietLoaiSim.Count > 0)
            {
                var listLoaiSim = new List<LoaiSimModel>();
                foreach (var loaiChiTiet in listChiTietLoaiSim)
                {
                    listLoaiSim.Add(new LoaiSimModel
                    {
                        Id = loaiChiTiet.MaLoaiSimChiTiet,
                        Ten = loaiChiTiet.TenLoaiSimChiTiet,
                        Alias = loaiChiTiet.Alias,
                        EnumFixed = loaiChiTiet.EnumFixed
                    });
                }
                return listLoaiSim;
            }
            return null;
        }

        #endregion

        #region loaisimchitiet

        public List<LoaiSimChiTietModel> GetListLoaiSimSoChiTiet()
        {
            var result = (from tblSimSoDepLoaiSim in _entities.tbl_SimSoDep_LoaiSimChiTiet
                          orderby tblSimSoDepLoaiSim.SapXep
                          select new LoaiSimChiTietModel
                          {
                              Id = tblSimSoDepLoaiSim.MaLoaiSimChiTiet,
                              MaLoaiSim = tblSimSoDepLoaiSim.MaLoaiSim,
                              Ten = tblSimSoDepLoaiSim.TenLoaiSimChiTiet,
                              Alias = tblSimSoDepLoaiSim.Alias,
                              EnumFixed = tblSimSoDepLoaiSim.EnumFixed,
                              Status = tblSimSoDepLoaiSim.Status,
                              SapXep = tblSimSoDepLoaiSim.SapXep
                          }).ToList();
            foreach (var ls in result)
            {
                switch (ls.Status)
                {
                    case (int)DauSoStatus.HienThi:
                        ls.TenStatus = "Hiển thị";
                        break;
                    case (int)DauSoStatus.KhongHienThi:
                        ls.TenStatus = "Không hiển thị";
                        break;
                }
            }
            return result;
        }

        public LoaiSimChiTietModel GetLoaiSimChiTietById(int id)
        {
            var loaisimso = (from ls in _entities.tbl_SimSoDep_LoaiSimChiTiet
                             where ls.MaLoaiSimChiTiet == id
                             select new LoaiSimChiTietModel
                             {
                                 Id = ls.MaLoaiSimChiTiet,
                                 MaLoaiSim = ls.MaLoaiSim,
                                 Ten = ls.TenLoaiSimChiTiet,
                                 Alias = ls.Alias,
                                 EnumFixed = ls.EnumFixed,
                                 Status = ls.Status,
                                 SapXep = ls.SapXep,
                                 MetaDescription = ls.MetaDescription,
                                 MetaKeyword = ls.MetaKeyword
                             }).FirstOrDefault();
            return loaisimso;
        }

        public bool SaveLoaiSimSoChiTiet(LoaiSimChiTietModel loaiSimSoModel)
        {
            var loaisimso = _entities.tbl_SimSoDep_LoaiSimChiTiet.Where(l => l.MaLoaiSimChiTiet == loaiSimSoModel.Id).FirstOrDefault();
            if (loaisimso == null)
            {
                return false;
            }
            loaisimso.TenLoaiSimChiTiet = loaiSimSoModel.Ten;
            loaisimso.Alias = loaiSimSoModel.Alias;
            loaisimso.EnumFixed = loaiSimSoModel.EnumFixed;
            loaisimso.MaLoaiSim = loaiSimSoModel.MaLoaiSim;
            loaisimso.SapXep = loaiSimSoModel.SapXep;
            loaisimso.Status = loaiSimSoModel.Status;
            loaisimso.MetaDescription = loaiSimSoModel.MetaDescription;
            loaisimso.MetaKeyword = loaiSimSoModel.MetaKeyword;
            _entities.SaveChanges();
            return true;
        }

        public bool DeleteLoaiSimChiTietById(int id)
        {
            var lsChitiet = _entities.tbl_SimSoDep_LoaiSimChiTiet.Where(c => c.MaLoaiSimChiTiet == id).FirstOrDefault();
            if (lsChitiet == null)
                return false;
            _entities.tbl_SimSoDep_LoaiSimChiTiet.DeleteObject(lsChitiet);
            _entities.SaveChanges();
            return true;
        }

        public bool AddLoaiSimChiTiet(LoaiSimChiTietModel loaiSimChiTietModel)
        {
            _entities.tbl_SimSoDep_LoaiSimChiTiet.AddObject(new tbl_SimSoDep_LoaiSimChiTiet
                                                                {
                                                                    MaLoaiSimChiTiet = loaiSimChiTietModel.Id,
                                                                    MaLoaiSim = loaiSimChiTietModel.MaLoaiSim,
                                                                    EnumFixed = loaiSimChiTietModel.EnumFixed,
                                                                    TenLoaiSimChiTiet = loaiSimChiTietModel.Ten,
                                                                    Alias = loaiSimChiTietModel.Alias,
                                                                    SapXep = loaiSimChiTietModel.SapXep,
                                                                    Status = loaiSimChiTietModel.Status,
                                                                    MetaKeyword = loaiSimChiTietModel.MetaKeyword,
                                                                    MetaDescription = loaiSimChiTietModel.MetaDescription
                                                                });
            _entities.SaveChanges();
            return true;
        }

        #endregion

        #region dauso

        public List<DauSoModel> GetListLoaiDauSo(bool showAll)
        {
            var result = showAll ? (from tblSimSoDepLoaiSim in _entities.tbl_SimSoDep_DauSo
                                    orderby tblSimSoDepLoaiSim.SapXep
                                    select new DauSoModel
                                    {
                                        Id = tblSimSoDepLoaiSim.MaDauSo,
                                        Ten = tblSimSoDepLoaiSim.TenDauSo,
                                        Alias = tblSimSoDepLoaiSim.Alias,
                                        EnumFixed = tblSimSoDepLoaiSim.EnumFixed,
                                        MoTa = tblSimSoDepLoaiSim.MoTa,
                                        TenNhaMang = tblSimSoDepLoaiSim.tbl_SimSoDep_NhaMang.TenNhaMang,
                                        Status = tblSimSoDepLoaiSim.Status,
                                        SapXep = tblSimSoDepLoaiSim.SapXep
                                    }).ToList() : (from tblSimSoDepLoaiSim in _entities.tbl_SimSoDep_DauSo
                                                   where tblSimSoDepLoaiSim.Status == (int)DauSoStatus.HienThi
                                                   orderby tblSimSoDepLoaiSim.SapXep
                                                   select new DauSoModel
                                                   {
                                                       Id = tblSimSoDepLoaiSim.MaDauSo,
                                                       Ten = tblSimSoDepLoaiSim.TenDauSo,
                                                       Alias = tblSimSoDepLoaiSim.Alias,
                                                       EnumFixed = tblSimSoDepLoaiSim.EnumFixed,
                                                       MoTa = tblSimSoDepLoaiSim.MoTa,
                                                       TenNhaMang = tblSimSoDepLoaiSim.tbl_SimSoDep_NhaMang.TenNhaMang,
                                                       Status = tblSimSoDepLoaiSim.Status,
                                                       SapXep = tblSimSoDepLoaiSim.SapXep
                                                   }).ToList();
            foreach (var ls in result)
            {
                switch (ls.Status)
                {
                    case (int)DauSoStatus.HienThi:
                        ls.TenStatus = "Hiển thị";
                        break;
                    case (int)DauSoStatus.KhongHienThi:
                        ls.TenStatus = "Không hiển thị";
                        break;
                }
            }
            return result;
        }

        public DauSoModel GetDauSoById(int id)
        {
            var dauso = (from ls in _entities.tbl_SimSoDep_DauSo
                         where ls.MaDauSo == id
                         select new DauSoModel
                         {
                             Id = ls.MaDauSo,
                             Ten = ls.TenDauSo,
                             Alias = ls.Alias,
                             EnumFixed = ls.EnumFixed,
                             MoTa = ls.MoTa,
                             MaNhaMang = ls.MaNhaMang ?? 0,
                             Status = ls.Status,
                             SapXep = ls.SapXep,
                             MetaDescription = ls.MetaDescription,
                             MetaKeyword = ls.MetaKeyword
                         }).FirstOrDefault();
            return dauso;
        }

        public bool SaveDauSo(DauSoModel dauSoModel)
        {
            var dauSo = _entities.tbl_SimSoDep_DauSo.Where(l => l.MaDauSo == dauSoModel.Id).FirstOrDefault();
            if (dauSo == null)
            {
                return false;
            }
            dauSo.TenDauSo = dauSoModel.Ten;
            dauSo.Alias = dauSoModel.Alias;
            dauSo.EnumFixed = dauSoModel.EnumFixed;
            dauSo.MoTa = dauSoModel.MoTa;
            dauSo.MaNhaMang = dauSoModel.MaNhaMang;
            dauSo.SapXep = dauSoModel.SapXep;
            dauSo.Status = dauSoModel.Status;
            dauSo.MetaDescription = dauSoModel.MetaDescription;
            dauSo.MetaKeyword = dauSoModel.MetaKeyword;
            _entities.SaveChanges();
            return true;
        }

        public bool DeleteDauSo(int id)
        {
            var dauso = _entities.tbl_SimSoDep_DauSo.Where(d => d.MaDauSo == id).FirstOrDefault();
            if (dauso == null)
                return false;
            _entities.tbl_SimSoDep_DauSo.DeleteObject(dauso);
            _entities.SaveChanges();
            return true;
        }

        public bool AddDauSo(DauSoModel dauSoModel)
        {
            _entities.tbl_SimSoDep_DauSo.AddObject(new tbl_SimSoDep_DauSo
                                                       {
                                                           EnumFixed = dauSoModel.EnumFixed,
                                                           Alias = dauSoModel.Alias,
                                                           TenDauSo = dauSoModel.Ten,
                                                           MoTa = dauSoModel.MoTa,
                                                           MaNhaMang = dauSoModel.MaNhaMang,
                                                           Status = dauSoModel.Status,
                                                           SapXep = dauSoModel.SapXep,
                                                           MetaDescription = dauSoModel.MetaDescription,
                                                           MetaKeyword = dauSoModel.MetaKeyword
                                                       });
            _entities.SaveChanges();
            return true;
        }

        #endregion

        #region loaigia

        public List<LoaiGiaModel> GetListLoaiGia(bool showAll)
        {
            var result = showAll ? (from tblSimSoDepLoaiSim in _entities.tbl_SimSoDep_LoaiGia
                                    orderby tblSimSoDepLoaiSim.SapXep
                                    select new LoaiGiaModel
                                    {
                                        Id = tblSimSoDepLoaiSim.MaLoaiGia,
                                        Ten = tblSimSoDepLoaiSim.TenLoaiGia,
                                        Alias = tblSimSoDepLoaiSim.Alias,
                                        EnumFixed = tblSimSoDepLoaiSim.EnumFixed,
                                        SapXep = tblSimSoDepLoaiSim.SapXep,
                                        Status = tblSimSoDepLoaiSim.Status
                                    }).ToList() : (from tblSimSoDepLoaiSim in _entities.tbl_SimSoDep_LoaiGia
                                                   where tblSimSoDepLoaiSim.Status == (int)DauSoStatus.HienThi
                                                   orderby tblSimSoDepLoaiSim.SapXep
                                                   select new LoaiGiaModel
                                                   {
                                                       Id = tblSimSoDepLoaiSim.MaLoaiGia,
                                                       Ten = tblSimSoDepLoaiSim.TenLoaiGia,
                                                       Alias = tblSimSoDepLoaiSim.Alias,
                                                       EnumFixed = tblSimSoDepLoaiSim.EnumFixed,
                                                       SapXep = tblSimSoDepLoaiSim.SapXep,
                                                       Status = tblSimSoDepLoaiSim.Status
                                                   }).ToList();
            foreach (var ls in result)
            {
                switch (ls.Status)
                {
                    case (int)DauSoStatus.HienThi:
                        ls.TenStatus = "Hiển thị";
                        break;
                    case (int)DauSoStatus.KhongHienThi:
                        ls.TenStatus = "Không hiển thị";
                        break;
                }
            }
            return result;
        }

        public bool SaveLoaiGia(LoaiGiaModel loaiGiaModel)
        {
            if (loaiGiaModel.Id > 0)
            {
                var loaigia = _entities.tbl_SimSoDep_LoaiGia.Where(l => l.MaLoaiGia == loaiGiaModel.Id).FirstOrDefault();
                if (loaigia == null)
                    return false;
                loaigia.TenLoaiGia = loaiGiaModel.Ten;
                loaigia.EnumFixed = loaiGiaModel.EnumFixed;
                loaigia.Alias = loaiGiaModel.Alias;
                loaigia.GiaTu = loaiGiaModel.GiaTu;
                loaigia.Den = loaiGiaModel.Den;
                loaigia.SapXep = loaiGiaModel.SapXep;
                loaigia.Status = loaiGiaModel.Status;
                loaigia.MetaDescription = loaiGiaModel.MetaDescription;
                loaigia.MetaKeyword = loaiGiaModel.MetaKeyword;
            }
            else
            {
                _entities.tbl_SimSoDep_LoaiGia.AddObject(new tbl_SimSoDep_LoaiGia
                                                             {
                                                                 Alias = loaiGiaModel.Alias,
                                                                 EnumFixed = loaiGiaModel.EnumFixed,
                                                                 TenLoaiGia = loaiGiaModel.Ten,
                                                                 GiaTu = loaiGiaModel.GiaTu,
                                                                 Den = loaiGiaModel.Den,
                                                                 Status = loaiGiaModel.Status,
                                                                 SapXep = loaiGiaModel.SapXep,
                                                                 MetaKeyword = loaiGiaModel.MetaKeyword,
                                                                 MetaDescription = loaiGiaModel.MetaDescription
                                                             });
            }
            _entities.SaveChanges();
            return true;
        }

        public LoaiGiaModel GetLoaiGiaById(int id)
        {
            var loaigia = (from ls in _entities.tbl_SimSoDep_LoaiGia
                           where ls.MaLoaiGia == id
                           select new LoaiGiaModel
                           {
                               Id = ls.MaLoaiGia,
                               Ten = ls.TenLoaiGia,
                               Alias = ls.Alias,
                               EnumFixed = ls.EnumFixed,
                               GiaTu = ls.GiaTu ?? 0,
                               Den = ls.Den ?? 0,
                               SapXep = ls.SapXep,
                               Status = ls.Status,
                               MetaDescription = ls.MetaDescription,
                               MetaKeyword = ls.MetaKeyword
                           }).FirstOrDefault();
            return loaigia;
        }

        public bool DeleteLoaiGia(int id)
        {
            var loaigia = _entities.tbl_SimSoDep_LoaiGia.Where(l => l.MaLoaiGia == id).FirstOrDefault();
            if (loaigia == null)
                return false;
            _entities.tbl_SimSoDep_LoaiGia.DeleteObject(loaigia);
            _entities.SaveChanges();
            return true;
        }

        #endregion

        #region daily
        public List<DaiLyModel> GetListDaiLy()
        {
            var result = (from tblSimSoDepDaiLy in _entities.tbl_SimSoDep_DaiLy
                          select new DaiLyModel
                                     {
                                         Id = tblSimSoDepDaiLy.MaDaiLy,
                                         Ten = tblSimSoDepDaiLy.TenDaiLy,
                                         DiDong = tblSimSoDepDaiLy.DiDong,
                                         DiaChi = tblSimSoDepDaiLy.DiaChi,
                                         Email = tblSimSoDepDaiLy.Email,
                                         MayBan = tblSimSoDepDaiLy.MayBan
                                     }).ToList();
            return result;
        }

        public bool AddDaiLy(DaiLyModel daiLyModel)
        {
            try
            {
                if (daiLyModel.Id > 0)
                {
                    var daily = _entities.tbl_SimSoDep_DaiLy.Where(d => d.MaDaiLy == daiLyModel.Id).FirstOrDefault();
                    if (daily == null)
                    {
                        return false;
                    }
                    daily.TenDaiLy = daiLyModel.Ten;
                    daily.MayBan = daiLyModel.MayBan;
                    daily.DiDong = daiLyModel.DiDong;
                    daily.DiaChi = daiLyModel.DiaChi;
                    daily.Email = daiLyModel.Email;
                }
                else
                {
                    _entities.tbl_SimSoDep_DaiLy.AddObject(new tbl_SimSoDep_DaiLy
                    {
                        TenDaiLy = daiLyModel.Ten,
                        DiDong = daiLyModel.DiDong,
                        DiaChi = daiLyModel.DiaChi,
                        Email = daiLyModel.Email,
                        MayBan = daiLyModel.MayBan
                    });
                }

                _entities.SaveChanges();
                return true;
            }
            catch (Exception)
            {
                return false;
            }

        }

        public DaiLyModel GetDaiLyById(int id)
        {
            var daiLy = _entities.tbl_SimSoDep_DaiLy.Where(d => d.MaDaiLy == id).FirstOrDefault();
            if (daiLy != null)
            {
                var lstTangThem = daiLy.tbl_SimSoDep_TangThem.ToList();
                var tangThem = lstTangThem.Select(tblSimSoDepTangThem => new TangThemModel
                                                                             {
                                                                                 Id = tblSimSoDepTangThem.Id,
                                                                                 GiaTu = tblSimSoDepTangThem.GiaTu ?? 0,
                                                                                 Den = tblSimSoDepTangThem.Den ?? 0,
                                                                                 MaDaiLy = tblSimSoDepTangThem.MaDaiLy ?? 0,
                                                                                 TangThem = tblSimSoDepTangThem.TangThem ?? 0
                                                                             }).ToList();
                var lstTrietKhau = daiLy.tbl_SimSoDep_DaiLyTrietKhau.ToList();
                var trietKhaus = lstTrietKhau.Select(item => new TrietKhauModel
                                                                 {
                                                                     Id = item.Id,
                                                                     GiaTu = item.GiaTu ?? 0,
                                                                     Den = item.Den ?? 0,
                                                                     MaDaiLy = item.MaDaiLy ?? 0,
                                                                     TrietKhau = item.TrietKhau ?? 0,
                                                                 }).ToList();
                var result = new DaiLyModel
                                {
                                    Id = daiLy.MaDaiLy,
                                    Ten = daiLy.TenDaiLy,
                                    DiDong = daiLy.DiDong,
                                    DiaChi = daiLy.DiaChi,
                                    Email = daiLy.Email,
                                    MayBan = daiLy.MayBan,
                                    TangThems = tangThem,
                                    TrietKhaus = trietKhaus
                                };
                return result;
            }

            return new DaiLyModel();
        }

        public bool DeleteDaiLyById(int id)
        {
            try
            {
                var daily = _entities.tbl_SimSoDep_DaiLy.Where(d => d.MaDaiLy == id).FirstOrDefault();
                if (daily != null)
                {
                    if (daily.tbl_SimSoDep_DaiLyTrietKhau != null)
                        for (var i = daily.tbl_SimSoDep_DaiLyTrietKhau.Count - 1; i >= 0; i--)
                        {
                            _entities.tbl_SimSoDep_DaiLyTrietKhau.DeleteObject(daily.tbl_SimSoDep_DaiLyTrietKhau.ElementAt(i));
                        }
                    if (daily.tbl_SimSoDep_TangThem != null)
                        for (var i = daily.tbl_SimSoDep_TangThem.Count - 1; i >= 0; i--)
                        {
                            _entities.tbl_SimSoDep_TangThem.DeleteObject(daily.tbl_SimSoDep_TangThem.ElementAt(i));
                        }
                    _entities.tbl_SimSoDep_DaiLy.DeleteObject(daily);
                }
                _entities.SaveChanges();
                return true;
            }
            catch (Exception)
            {
                return false;
            }

        }

        public bool SaveTrietKhauDaiLy(TrietKhauModel trietKhauModel)
        {
            if (trietKhauModel.Id > 0)
            {
                var trietKhau =
                    _entities.tbl_SimSoDep_DaiLyTrietKhau.Where(t => t.Id == trietKhauModel.Id).FirstOrDefault();
                if (trietKhau == null)
                    return false;
                trietKhau.Den = trietKhauModel.Den;
                trietKhau.GiaTu = trietKhauModel.GiaTu;
                trietKhau.TrietKhau = trietKhauModel.TrietKhau;
            }
            else
            {
                _entities.tbl_SimSoDep_DaiLyTrietKhau.AddObject(new tbl_SimSoDep_DaiLyTrietKhau
                                                                    {
                                                                        Den = trietKhauModel.Den,
                                                                        GiaTu = trietKhauModel.GiaTu,
                                                                        MaDaiLy = trietKhauModel.MaDaiLy,
                                                                        TrietKhau = trietKhauModel.TrietKhau
                                                                    });
            }
            _entities.SaveChanges();
            return true;
        }

        public TrietKhauModel GetTrietKhauDaiLy(int id)
        {
            return (from tblSimSoDepDaiLyTrietKhau in _entities.tbl_SimSoDep_DaiLyTrietKhau
                    where tblSimSoDepDaiLyTrietKhau.Id == id
                    select new TrietKhauModel
                               {
                                   Id = tblSimSoDepDaiLyTrietKhau.Id,
                                   Den = tblSimSoDepDaiLyTrietKhau.Den ?? 0,
                                   GiaTu = tblSimSoDepDaiLyTrietKhau.GiaTu ?? 0,
                                   MaDaiLy = tblSimSoDepDaiLyTrietKhau.MaDaiLy ?? 0,
                                   TrietKhau = tblSimSoDepDaiLyTrietKhau.TrietKhau ?? 0
                               }).FirstOrDefault();
        }

        public bool DeleteTrietKhauDaiLy(int id)
        {
            var tkhau = _entities.tbl_SimSoDep_DaiLyTrietKhau.Where(k => k.Id == id).FirstOrDefault();
            if (tkhau != null)
            {
                _entities.tbl_SimSoDep_DaiLyTrietKhau.DeleteObject(tkhau);
            }
            _entities.SaveChanges();
            return true;
        }

        public bool SaveTangThemDaiLy(TangThemModel tangThemModel)
        {
            if (tangThemModel.Id > 0)
            {
                var tangThem = _entities.tbl_SimSoDep_TangThem.Where(t => t.Id == tangThemModel.Id).FirstOrDefault();

                if (tangThem != null)
                {
                    tangThem.GiaTu = tangThemModel.GiaTu;
                    tangThem.Den = tangThemModel.Den;
                    tangThem.TangThem = tangThemModel.TangThem;
                }
            }
            else
            {
                _entities.tbl_SimSoDep_TangThem.AddObject(new tbl_SimSoDep_TangThem
                {
                    MaDaiLy = tangThemModel.MaDaiLy,
                    GiaTu = tangThemModel.GiaTu,
                    Den = tangThemModel.Den,
                    TangThem = tangThemModel.TangThem
                });
            }
            _entities.SaveChanges();
            return true;
        }

        public TangThemModel GetTangThemById(int id)
        {
            return (from t in _entities.tbl_SimSoDep_TangThem
                    where t.Id == id
                    select new TangThemModel
                               {
                                   Id = t.Id,
                                   GiaTu = t.GiaTu ?? 0,
                                   Den = t.Den ?? 0,
                                   MaDaiLy = t.MaDaiLy ?? 0,
                                   TangThem = t.TangThem ?? 0
                               }).FirstOrDefault();
        }

        public bool DeleteTangThem(int id)
        {
            var tangThem = _entities.tbl_SimSoDep_TangThem.Where(t => t.Id == id).FirstOrDefault();
            if (tangThem != null)
            {
                _entities.tbl_SimSoDep_TangThem.DeleteObject(tangThem);
            }
            _entities.SaveChanges();
            return true;
        }



        #endregion

        #region BanMenh

        public List<NamSinhModel> GetListBanMenh()
        {
            var result = (from b in _entities.tbl_SimSoDep_BanMenh
                          select new NamSinhModel
                                     {
                                         IdNamSinh = b.IdBanMenh,
                                         TenNguHanh = b.tbl_SimSoDep_NguHanh.Ten,
                                         IdNguHanh = b.IdNguHanh,
                                         MoTa = b.MoTa,
                                         Nam = b.Nam,
                                         TenBanMenh = b.TenBanMenh
                                     }).ToList();
            return result;
        }

        public bool SaveNameSinh(NamSinhModel namSinhModel)
        {
            if (namSinhModel.IdNamSinh > 0)
            {
                var namSinh =
                    _entities.tbl_SimSoDep_BanMenh.Where(b => b.IdBanMenh == namSinhModel.IdNamSinh).FirstOrDefault();
                if (namSinh == null)
                    return false;
                namSinh.TenBanMenh = namSinhModel.TenBanMenh;
                namSinh.Nam = namSinhModel.Nam;
                namSinh.IdNguHanh = namSinhModel.IdNguHanh;
                namSinh.MoTa = namSinhModel.MoTa;
            }
            else
            {
                _entities.tbl_SimSoDep_BanMenh.AddObject(new tbl_SimSoDep_BanMenh
                                                        {
                                                            IdNguHanh = namSinhModel.IdNguHanh,
                                                            Nam = namSinhModel.Nam,
                                                            TenBanMenh = namSinhModel.TenBanMenh,
                                                            MoTa = namSinhModel.MoTa
                                                        });
            }

            _entities.SaveChanges();
            return true;
        }

        public NamSinhModel GetNamSinhById(int id)
        {
            var result = (from b in _entities.tbl_SimSoDep_BanMenh
                          where b.IdBanMenh == id
                          select new NamSinhModel
                                     {
                                         IdNamSinh = b.IdBanMenh,
                                         IdNguHanh = b.IdNguHanh,
                                         MoTa = b.MoTa,
                                         Nam = b.Nam,
                                         TenBanMenh = b.TenBanMenh
                                     }).FirstOrDefault();
            return result;
        }

        public bool DeleteNamSinh(int idNamSinh)
        {
            var nn = _entities.tbl_SimSoDep_BanMenh.Where(b => b.IdBanMenh == idNamSinh).FirstOrDefault();
            if (nn == null)
            {
                return false;
            }
            _entities.tbl_SimSoDep_BanMenh.DeleteObject(nn);
            _entities.SaveChanges();
            return true;
        }

        #endregion

        #region NguHanh

        public List<NguHanhModel> GetListNguHanh()
        {
            var result = (from n in _entities.tbl_SimSoDep_NguHanh
                          select new NguHanhModel
                                     {
                                         IdNguHanh = n.IdNguHanh,
                                         Ten = n.Ten,
                                         TuongKhac = n.TuongKhac,
                                         TuongSinh = n.TuongSinh,
                                         MoTa = n.MoTa,
                                     }).ToList();
            return result;
        }
        //Lay danh sach bat quai
        public List<BatQuaiModel> GetListBatQuai()
        {
            var result = (from n in _entities.tbl_SimSoDep_BatQuai
                          select new BatQuaiModel
                                     {
                                         IdQueBatQuai = n.IdQueBatQuai,
                                         TenQue = n.TenQue,
                                         HinhQue = n.HinhQue,
                                         PhienAm = n.PhienAm,
                                         MoTa = n.MoTa
                                     }).ToList();
            return result;
        }

        public bool AddBatQuai(BatQuaiModel batQuaiModel)
        {
            _entities.tbl_SimSoDep_BatQuai.AddObject(new tbl_SimSoDep_BatQuai
            {
                IdQueBatQuai = batQuaiModel.IdQueBatQuai,
                TenQue = batQuaiModel.TenQue,
                HinhQue = batQuaiModel.HinhQue,
                MoTa = batQuaiModel.MoTa,
                PhienAm = batQuaiModel.PhienAm
            });
            _entities.SaveChanges();
            return true;
        }

        public bool SaveBatQuai(int id, BatQuaiModel batQuaiModel)
        {
            var batQuai = _entities.tbl_SimSoDep_BatQuai.Where(b => b.IdQueBatQuai == id).FirstOrDefault();
            if (batQuai == null)
                return false;
            _entities.tbl_SimSoDep_BatQuai.DeleteObject(batQuai);
            _entities.tbl_SimSoDep_BatQuai.AddObject(new tbl_SimSoDep_BatQuai
            {
                IdQueBatQuai = batQuaiModel.IdQueBatQuai,
                TenQue = batQuaiModel.TenQue,
                HinhQue = batQuaiModel.HinhQue,
                MoTa = batQuaiModel.MoTa,
                PhienAm = batQuaiModel.PhienAm
            });
            _entities.SaveChanges();
            return true;
        }

        public BatQuaiModel GetBatQuaiById(int id)
        {
            var result = (from b in _entities.tbl_SimSoDep_BatQuai
                          where b.IdQueBatQuai == id
                          select new BatQuaiModel
                                     {
                                         IdQueBatQuai = b.IdQueBatQuai,
                                         HinhQue = b.HinhQue,
                                         TenQue = b.TenQue,
                                         MoTa = b.MoTa,
                                         PhienAm = b.PhienAm
                                     }).FirstOrDefault();
            return result;
        }

        public bool DeleteBatQuai(int id)
        {
            var result = (from b in _entities.tbl_SimSoDep_BatQuai
                          where b.IdQueBatQuai == id
                          select b).FirstOrDefault();
            if (result == null)
                return false;
            _entities.tbl_SimSoDep_BatQuai.DeleteObject(result);
            _entities.SaveChanges();
            return true;
        }

        #endregion

        #region KinhDich

        public List<KinhDichModel> GetListKinhDich()
        {
            var result = (from k in _entities.tbl_SimSoDep_KinhDich
                          select new KinhDichModel
                                     {
                                         HinhQue = k.HinhQue,
                                         TenQue = k.TenQue,
                                         PhienAm = k.PhienAm,
                                         IdKinhDich = k.IdKinhDich,
                                         IdQueHa = k.IdQueHa,
                                         IdQueThuong = k.IdQueThuong,
                                         LoiDich = k.LoiDich,
                                         YNghia = k.YNghia,
                                         QueHa = k.tbl_SimSoDep_BatQuai.TenQue,
                                         QueThuong = k.tbl_SimSoDep_BatQuai1.TenQue,
                                         IdLoaiQue = k.LoaiQue
                                     }).ToList();
            foreach (var kinhDichModel in result)
            {
                switch (kinhDichModel.IdLoaiQue)
                {
                    case (int)LoaiQue.Hung:
                        kinhDichModel.TenLoaiQue = "Hung";
                        break;
                    case (int)LoaiQue.Cat:
                        kinhDichModel.TenLoaiQue = "Cát";
                        break;
                    case (int)LoaiQue.KhongHungKhongCat:
                        kinhDichModel.TenLoaiQue = "Ko Hung Ko Cát";
                        break;
                }
            }
            return result;
        }

        public bool SaveKinhDich(KinhDichModel kinhDichModel)
        {
            if (kinhDichModel.IdKinhDich > 0)
            {
                var kinhdich =
                    _entities.tbl_SimSoDep_KinhDich.Where(k => k.IdKinhDich == kinhDichModel.IdKinhDich).FirstOrDefault();
                if (kinhdich == null)
                    return false;
                kinhdich.HinhQue = kinhDichModel.HinhQue;
                kinhdich.IdQueHa = kinhDichModel.IdQueHa;
                kinhdich.IdQueThuong = kinhDichModel.IdQueThuong;
                kinhdich.LoiDich = kinhDichModel.LoiDich;
                kinhdich.PhienAm = kinhDichModel.PhienAm;
                kinhdich.TenQue = kinhDichModel.TenQue;
                kinhdich.LoaiQue = kinhDichModel.IdLoaiQue;
            }
            else
            {
                _entities.tbl_SimSoDep_KinhDich.AddObject(new tbl_SimSoDep_KinhDich
                                                              {
                                                                  TenQue = kinhDichModel.TenQue,
                                                                  HinhQue = kinhDichModel.HinhQue,
                                                                  PhienAm = kinhDichModel.PhienAm,
                                                                  YNghia = kinhDichModel.YNghia,
                                                                  LoiDich = kinhDichModel.LoiDich,
                                                                  IdQueThuong = kinhDichModel.IdQueThuong,
                                                                  IdQueHa = kinhDichModel.IdQueHa,
                                                                  LoaiQue = kinhDichModel.IdLoaiQue
                                                              });
            }
            _entities.SaveChanges();
            return true;
        }

        public KinhDichModel GetKinhDichById(int id)
        {
            var result = (from k in _entities.tbl_SimSoDep_KinhDich
                          where k.IdKinhDich == id
                          select new KinhDichModel
                          {
                              HinhQue = k.HinhQue,
                              TenQue = k.TenQue,
                              PhienAm = k.PhienAm,
                              IdKinhDich = k.IdKinhDich,
                              IdQueHa = k.IdQueHa,
                              IdQueThuong = k.IdQueThuong,
                              LoiDich = k.LoiDich,
                              YNghia = k.YNghia,
                              IdLoaiQue = k.LoaiQue
                          }).FirstOrDefault();
            return result;
        }

        public bool DeleteKinhDich(int id)
        {
            var kinhdich =
                    _entities.tbl_SimSoDep_KinhDich.Where(k => k.IdKinhDich == id).FirstOrDefault();
            if (kinhdich == null)
                return false;
            _entities.tbl_SimSoDep_KinhDich.DeleteObject(kinhdich);
            _entities.SaveChanges();
            return true;
        }

        #endregion

        #region Tim kiem

        public List<SimSoModel> TimKiemSimSo(TimKiemModel timKiemModel)
        {
            var result = _entities.SearchSimSo(timKiemModel.SearchString.Trim()).ToList();
            var listSimSo = new List<SimSoModel>();
            foreach (var ss in result)
            {
                listSimSo.Add(new SimSoModel
                                  {
                                      MaSoDienThoai = ss.MaSoDienThoai,
                                      SoDienThoai = ss.SoDienThoai,
                                      GiaTien = ss.GiaTien ?? 0,
                                      NhaMang = ss.TenNhaMang
                                  });
            }
            return listSimSo;
        }

        #endregion

        #region LogUser

        public void AddLogUser(string clientIp)
        {
            _entities.tbl_SimSoDep_ClientIp.AddObject(new tbl_SimSoDep_ClientIp
                                                          {
                                                              IpAddress = clientIp
                                                          });
            _entities.SaveChanges();
        }

        #endregion

        #region Boiphongthuy

        public KetQuaPhongThuyModel BoiPhongThuy(string soDienThoai, int yyyy)
        {
            var ketqua = new KetQuaPhongThuyModel { LoaiAmDuong = SimSoHelper.CheckAmDuong(soDienThoai) };
            SimSoHelper.CheckSoSoAmSoSoDuong(soDienThoai, ketqua);

            ketqua.SoDienThoai = soDienThoai;
            ketqua.SoNuoc = SimSoHelper.CheckSoNuoc(soDienThoai);

            var listKinhDich = _entities.tbl_SimSoDep_KinhDich.ToList();
            var idQueDich = SimSoHelper.CheckQueDich(soDienThoai, listKinhDich);
            var queDich = listKinhDich.Where(k => k.IdKinhDich == idQueDich).FirstOrDefault();
            if (queDich != null)
            {
                ketqua.LoaiQue = queDich.LoaiQue ?? 0;
                ketqua.TenQue = queDich.TenQue;
                ketqua.HinhQue = queDich.HinhQue;
                ketqua.YNghia = queDich.LoiDich;
            }

            var idNguHanh = SimSoHelper.CheckNguHanh(soDienThoai);
            ketqua.IdNguHanhSo = SimSoHelper.CheckNguHanh(soDienThoai);
            ketqua.TenNguHanhSo = _entities.tbl_SimSoDep_NguHanh.Where(n => n.IdNguHanh == idNguHanh).Select(n => n.Ten).FirstOrDefault();

            var namEntities = _entities.tbl_SimSoDep_BanMenh.Where(b => b.Nam == yyyy).FirstOrDefault();
            if (namEntities != null)
            {
                ketqua.IdNguHanhNam = namEntities.IdNguHanh;
                ketqua.TenNguHanhNam = namEntities.tbl_SimSoDep_NguHanh.Ten;
            }

            ketqua.IsContainSo8 = soDienThoai.Contains("8");

            return ketqua;
        }

        #endregion
    }
}