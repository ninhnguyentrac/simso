using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using SimSoDep.Models;
using SimSoDepStore;

namespace SimSoDep.DataBasesManager
{
    public class SimSoHelper
    {
        public static int CheckSoNuoc(string chuoi)
        {
            var splitString = chuoi.ToArray();
            var tong = 0;
            for (var i = 0; i < splitString.Length; i++)
            {
                tong = tong + int.Parse(splitString[i].ToString());
            }
            if (tong > 9)
                return tong % 10;
            return tong;
        }

        public static int? CheckQueDich(string chuoi, List<tbl_SimSoDep_KinhDich> kinhDichs)
        {
            var nuaDau = (chuoi.Length % 2 == 0)
                             ? chuoi.Substring(0, chuoi.Length / 2)
                             : chuoi.Substring(0, (chuoi.Length / 2) + 1);
            var nuaSau = (chuoi.Length % 2 == 0)
                             ? chuoi.Substring(chuoi.Length / 2, chuoi.Length / 2)
                             : chuoi.Substring((chuoi.Length / 2) + 1, chuoi.Length / 2);
            var idQueThuong = CheckBatQuai(nuaDau);
            var idQueHa = CheckBatQuai(nuaSau);
            var result = kinhDichs.Where(k => k.IdQueThuong == idQueThuong && k.IdQueHa == idQueHa).Select(k => k.IdKinhDich).FirstOrDefault();
            if (result == 0)
                return null;
            return result;
        }

        public static int CheckBatQuai(string chuoi)
        {
            var splitString = chuoi.ToArray();
            var tong = 0;
            foreach (var v in splitString)
            {
                tong = tong + int.Parse(v.ToString());
            }

            var kq = 0;

            switch (tong % 8)
            {
                case 0:
                    kq = (int)BatQuai.Khon;
                    break;
                case 1:
                    kq = (int)BatQuai.Can;
                    break;
                case 2:
                    kq = (int)BatQuai.Doai;
                    break;
                case 3:
                    kq = (int)BatQuai.Ly;
                    break;
                case 4:
                    kq = (int)BatQuai.Chan;
                    break;
                case 5:
                    kq = (int)BatQuai.Ton;
                    break;
                case 6:
                    kq = (int)BatQuai.Kham;
                    break;
                case 7:
                    kq = (int)BatQuai.Caan;
                    break;
                case 8:
                    kq = (int)BatQuai.Khon;
                    break;
                case 9:
                    kq = (int)BatQuai.Can;
                    break;
            }
            return kq;
        }

        public static int CheckAmDuong(string chuoi)
        {
            var splitString = chuoi.ToArray();
            var soAm = 0;
            var soDuong = 0;
            for (var i = 0; i < splitString.Length; i++)
            {
                if (int.Parse(splitString[i].ToString()) % 2 == 0)
                {
                    soAm++;
                }
                else
                {
                    soDuong++;
                }
            }

            var phanAm = Math.Round((double)(soAm * 100) / chuoi.Length, 2);
            var phanDuong = Math.Round((double)(soDuong * 100) / chuoi.Length, 2);
            if (phanAm.Equals(0) || phanDuong.Equals(0))
                return (int)LoaiAmDuong.AmDuongLechThaiCuc;
            if (phanAm.Equals(phanDuong))
                return (int)LoaiAmDuong.AmDuongCanBang;
            if (phanAm < 40 || phanDuong < 40)
                return (int)LoaiAmDuong.AmDuongLechNhieu;
            return (int)LoaiAmDuong.AmDuongGanCanBang;
        }

        public static void CheckSoSoAmSoSoDuong(string chuoi, KetQuaPhongThuyModel ketQuaPhongThuyModel)
        {
            var splitString = chuoi.ToArray();
            var soAm = 0;
            var soDuong = 0;
            for (var i = 0; i < splitString.Length; i++)
            {
                if (int.Parse(splitString[i].ToString()) % 2 == 0)
                {
                    soAm++;
                }
                else
                {
                    soDuong++;
                }
            }
            ketQuaPhongThuyModel.SoSoAm = soAm;
            ketQuaPhongThuyModel.SoSoDuong = soDuong;
        }

        public static int CheckNguHanh(string chuoi)
        {
            var tong = int.Parse(chuoi);
            if (tong == 0 || tong == 2 || tong == 5 || tong == 8)
                return (int)NguHanh.Tho;
            if (tong == 3 || tong == 4)
                return (int)NguHanh.Moc;
            if (tong == 6 || tong == 7)
                return (int)NguHanh.Kim;
            if (tong == 9)
                return (int)NguHanh.Hoa;
            return (int)NguHanh.Thuy;
        }

        public static bool CheckSimTuNguLucQuy(string chuoi)
        {
            var splitString = chuoi.ToArray();
            var isLike = true;
            for (var i = 0; i < splitString.Length - 1; i++)
            {
                for (var j = i + 1; j < splitString.Length; j++)
                {
                    if (splitString[i] != splitString[j])
                    {
                        isLike = false;
                        break;
                    }
                }
                if (!isLike)
                {
                    break;
                }
            }
            return isLike;
        }

        public static bool CheckSoTien(string chuoi)
        {
            var splitString = chuoi.ToArray();
            var isLike = true;
            for (var i = 1; i < splitString.Length; i++)
            {
                if ((int.Parse(splitString[i - 1].ToString()) + 1) != int.Parse(splitString[i].ToString()))
                {
                    isLike = false;
                    break;
                }
            }
            return isLike;
        }

        public static bool CheckSoTienDonDacBiet(string chuoi)
        {
            var splitString = chuoi.ToArray();
            var isLike = true;
            for (var i = 1; i < splitString.Length; i++)
            {
                if ((int.Parse(splitString[i - 1].ToString()) + 2) != int.Parse(splitString[i].ToString()))
                {
                    isLike = false;
                    break;
                }
            }
            return isLike;
        }

        public static bool CheckSoTienDonKhac(string chuoi)
        {
            var splitString = chuoi.ToArray();
            var isLike = true;
            for (var i = 1; i < splitString.Length; i++)
            {
                if (int.Parse(splitString[i - 1].ToString()) >= int.Parse(splitString[i].ToString()))
                {
                    isLike = false;
                    break;
                }
            }
            return isLike;
        }

        public static int CheckSoTienGiua(string chuoi)
        {
            var splitString = chuoi.ToArray();
            var countNum = 1;
            for (var i = 1; i < splitString.Length; i++)
            {
                if ((int.Parse(splitString[i - 1].ToString()) + 1) == int.Parse(splitString[i].ToString()))
                {
                    countNum++;
                }
                else
                {
                    countNum = 1;
                }
            }
            return countNum;
        }

        public static bool CheckSoTienDoi(string chuoi)
        {
            var soDau = int.Parse(chuoi.Substring(0, 2));
            var soCuoi = int.Parse(chuoi.Substring(2, 2));
            return soCuoi > soDau;
        }

        public static bool CheckSoTienDoi3(string chuoi)
        {
            var soDau = int.Parse(chuoi.Substring(0, 2));
            var soGiua = int.Parse(chuoi.Substring(2, 2));
            var soCuoi = int.Parse(chuoi.Substring(4, 2));
            return soCuoi > soGiua && soGiua > soDau;
        }

        public static bool CheckSoTienDoiKhac(string chuoi)
        {
            var soDau = int.Parse(chuoi.Substring(0, 2));
            var soGiua = int.Parse(chuoi.Substring(2, 2));
            return soDau == soGiua;
        }

        public static bool CheckTamHoaKep(string chuoi)
        {
            var soDau = chuoi.Substring(0, 3);
            var soGiua = chuoi.Substring(3, 3);
            var arr = soDau.ToArray();
            return soGiua.Equals(soDau) && arr[0].Equals(arr[1]);
        }

        public static bool CheckSoKepAABB(string chuoi)
        {
            var arrStr = chuoi.ToArray();
            return arrStr[0] == arrStr[1] && arrStr[2] == arrStr[3] && arrStr[1] != arrStr[2];
        }

        public static bool CheckSoKepAABBCC(string chuoi)
        {
            var arrStr = chuoi.ToArray();
            return arrStr[0] == arrStr[1] && arrStr[2] == arrStr[3] && arrStr[4] == arrStr[5]
                && arrStr[1] != arrStr[2] && arrStr[1] != arrStr[4] && arrStr[3] != arrStr[4];
        }

        public static bool CheckSoKepAABBAA(string chuoi)
        {
            var arrStr = chuoi.ToArray();
            return arrStr[0] == arrStr[1] && arrStr[1] == arrStr[4] && arrStr[2] == arrStr[3] && arrStr[4] == arrStr[5] && arrStr[1] != arrStr[2];
        }

        public static bool CheckSoKepAABBCCDD(string chuoi)
        {
            var arrStr = chuoi.ToArray();
            return arrStr[0] == arrStr[1] && arrStr[2] == arrStr[3] && arrStr[4] == arrStr[5] && arrStr[6] == arrStr[7]
                   && arrStr[1] != arrStr[2] && arrStr[1] != arrStr[4] && arrStr[1] != arrStr[6] && arrStr[3] != arrStr[4]
                   && arrStr[3] != arrStr[6] && arrStr[5] != arrStr[6];
        }

        public static bool CheckSoKepAABBAACC(string chuoi)
        {
            var arrStr = chuoi.ToArray();
            return arrStr[0] == arrStr[1] && arrStr[2] == arrStr[3] && arrStr[4] == arrStr[5] && arrStr[6] == arrStr[7]
                   && arrStr[1] != arrStr[2] && arrStr[1] == arrStr[4] && arrStr[1] != arrStr[6] && arrStr[3] != arrStr[4]
                   && arrStr[3] != arrStr[6] && arrStr[5] != arrStr[6];
        }

        public static bool CheckSoKepAABBCCBB(string chuoi)
        {
            var arrStr = chuoi.ToArray();
            return arrStr[0] == arrStr[1] && arrStr[2] == arrStr[3] && arrStr[4] == arrStr[5] && arrStr[6] == arrStr[7]
                   && arrStr[1] != arrStr[2] && arrStr[1] == arrStr[4] && arrStr[1] != arrStr[6] && arrStr[3] != arrStr[4]
                   && arrStr[3] == arrStr[6] && arrStr[5] != arrStr[6];
        }

        public static bool CheckSoKepAAXAAY(string chuoi)
        {
            var arrStr = chuoi.ToArray();
            return arrStr[0] == arrStr[1] && arrStr[3] == arrStr[4] && arrStr[1] == arrStr[3]
                   && arrStr[1] != arrStr[2] && arrStr[4] != arrStr[5] && arrStr[2] != arrStr[5];
        }

        public static bool CheckSoKepXAAYAA(string chuoi)
        {
            var arrStr = chuoi.ToArray();
            return arrStr[1] == arrStr[2] && arrStr[4] == arrStr[5] && arrStr[2] == arrStr[4]
                   && arrStr[0] != arrStr[1] && arrStr[3] != arrStr[4] && arrStr[0] != arrStr[3];
        }

        public static bool CheckSoKepAAXYAA(string chuoi)
        {
            var arrStr = chuoi.ToArray();
            return arrStr[0] == arrStr[1] && arrStr[4] == arrStr[5] && arrStr[1] == arrStr[4]
                   && arrStr[1] != arrStr[2] && arrStr[3] != arrStr[4] && arrStr[2] != arrStr[3];
        }

        public static bool CheckSoKepAAXBBY(string chuoi)
        {
            var arrStr = chuoi.ToArray();
            return arrStr[0] == arrStr[1] && arrStr[3] == arrStr[4] && arrStr[1] != arrStr[2]
                   && arrStr[2] != arrStr[3] && arrStr[4] != arrStr[5] && arrStr[1] != arrStr[3] && arrStr[2] != arrStr[5];
        }

        public static bool CheckSoKepXAAYBB(string chuoi)
        {
            var arrStr = chuoi.ToArray();
            return arrStr[1] == arrStr[2] && arrStr[4] == arrStr[5] && arrStr[0] != arrStr[1]
                   && arrStr[2] != arrStr[3] && arrStr[3] != arrStr[4] && arrStr[2] != arrStr[4] && arrStr[0] != arrStr[3];
        }

        public static bool CheckSoLapXABYAB(string chuoi)
        {
            var arrStr = chuoi.ToArray();
            return arrStr[1] == arrStr[4] && arrStr[2] == arrStr[5] && arrStr[0] != arrStr[1]
                   && arrStr[0] != arrStr[2] && arrStr[0] != arrStr[3] && arrStr[0] != arrStr[5] && arrStr[1] != arrStr[2]
                   && arrStr[1] != arrStr[3] && arrStr[2] != arrStr[3];
        }

        public static bool CheckSoLapABXABY(string chuoi)
        {
            var arrStr = chuoi.ToArray();
            return arrStr[0] == arrStr[3] && arrStr[1] == arrStr[4] && arrStr[0] != arrStr[1]
                   && arrStr[0] != arrStr[2] && arrStr[0] != arrStr[5] && arrStr[1] != arrStr[2]
                   && arrStr[1] != arrStr[5] && arrStr[2] != arrStr[5];
        }

        public static bool CheckSoLapAXBAYB(string chuoi)
        {
            var arrStr = chuoi.ToArray();
            return arrStr[0] == arrStr[3] && arrStr[2] == arrStr[5] && arrStr[0] != arrStr[1]
                   && arrStr[0] != arrStr[2] && arrStr[0] != arrStr[4] && arrStr[1] != arrStr[2]
                   && arrStr[1] != arrStr[4] && arrStr[2] != arrStr[4];
        }

        public static bool CheckSoLapABXYAB(string chuoi)
        {
            var arrStr = chuoi.ToArray();
            return arrStr[0] == arrStr[4] && arrStr[1] == arrStr[5] && arrStr[0] != arrStr[1]
                   && arrStr[0] != arrStr[2] && arrStr[0] != arrStr[3] && arrStr[1] != arrStr[2]
                   && arrStr[1] != arrStr[3] && arrStr[2] != arrStr[3];
        }

        public static bool CheckSoLapXABCYABC(string chuoi)
        {
            var arrStr = chuoi.ToArray();
            return arrStr[1] == arrStr[5] && arrStr[2] == arrStr[6] && arrStr[3] != arrStr[7]
                   && arrStr[0] != arrStr[1] && arrStr[0] != arrStr[2] && arrStr[0] != arrStr[3]
                   && arrStr[0] != arrStr[4] && arrStr[1] != arrStr[2] && arrStr[1] != arrStr[3]
                   && arrStr[1] != arrStr[4] && arrStr[2] != arrStr[3] && arrStr[2] != arrStr[4]
                   && arrStr[3] != arrStr[4];
        }

        public static bool CheckSoKepSaoAABBSao(string chuoi)
        {
            var isTrue = false;
            for (int i = 0; i < 4; i++)
            {
                var subString = chuoi.Substring(i, 4);
                if (CheckSoKepAABB(subString))
                {
                    isTrue = true;
                    break;
                }
            }
            return isTrue;
        }

        public static bool CheckSoLapABAB(string chuoi)
        {
            var arrStr = chuoi.ToArray();
            return arrStr[0] == arrStr[2] && arrStr[1] == arrStr[3] && arrStr[0] != arrStr[1];
        }

        public static bool CheckSoLapSaoABABSao(string chuoi)
        {
            var isTrue = false;
            for (int i = 0; i < 4; i++)
            {
                var subString = chuoi.Substring(i, 4);
                if (CheckSoLapABAB(subString))
                {
                    isTrue = true;
                    break;
                }
            }
            return isTrue;
        }

        public static bool CheckTaxiLap2(string chuoi)
        {
            var arrStr = chuoi.ToArray();
            return arrStr[0] == arrStr[2] && arrStr[1] == arrStr[3] && arrStr[0] == arrStr[4] && arrStr[1] == arrStr[5]
                    && arrStr[0] != arrStr[1];
        }

        public static bool CheckTaxi3ABCABC(string chuoi)
        {
            var arrStr = chuoi.ToArray();
            return arrStr[0] == arrStr[3] && arrStr[1] == arrStr[4] && arrStr[2] == arrStr[5] && arrStr[0] != arrStr[1]
                    && arrStr[0] != arrStr[2] && arrStr[1] != arrStr[2];
        }

        public static bool CheckTaxi3ABAABA(string chuoi)
        {
            var arrStr = chuoi.ToArray();
            return arrStr[0] == arrStr[2] && arrStr[1] == arrStr[4] && arrStr[2] == arrStr[3] && arrStr[3] == arrStr[5]
                    && arrStr[0] != arrStr[1];
        }

        public static bool CheckTaxi3AABAAB(string chuoi)
        {
            var arrStr = chuoi.ToArray();
            return arrStr[0] == arrStr[1] && arrStr[2] == arrStr[5] && arrStr[1] == arrStr[3] && arrStr[3] == arrStr[4]
                    && arrStr[0] != arrStr[2];
        }

        public static bool CheckTaxi3BAABAA(string chuoi)
        {
            var arrStr = chuoi.ToArray();
            return arrStr[0] == arrStr[3] && arrStr[1] == arrStr[2] && arrStr[1] == arrStr[4] && arrStr[4] == arrStr[5]
                    && arrStr[0] != arrStr[1];
        }

        public static bool CheckTaxi4AABCAABC(string chuoi)
        {
            var arrStr = chuoi.ToArray();
            return arrStr[0] == arrStr[1] && arrStr[1] == arrStr[4] && arrStr[4] == arrStr[5] && arrStr[2] == arrStr[6]
                    && arrStr[3] == arrStr[7] && arrStr[0] != arrStr[2] && arrStr[0] != arrStr[3] && arrStr[2] != arrStr[3];
        }

        public static bool CheckTaxi4ABBCABBC(string chuoi)
        {
            var arrStr = chuoi.ToArray();
            return arrStr[0] == arrStr[4] && arrStr[1] == arrStr[2] && arrStr[3] == arrStr[5] && arrStr[3] == arrStr[7]
                    && arrStr[5] == arrStr[6] && arrStr[0] != arrStr[1] && arrStr[0] != arrStr[3] && arrStr[2] != arrStr[3];
        }

        public static bool CheckTaxi4ABCCABCC(string chuoi)
        {
            var arrStr = chuoi.ToArray();
            return arrStr[0] == arrStr[4] && arrStr[1] == arrStr[5] && arrStr[2] == arrStr[3] && arrStr[3] == arrStr[7]
                    && arrStr[0] != arrStr[1] && arrStr[0] != arrStr[2] && arrStr[1] != arrStr[2];
        }

        public static bool CheckTaxi4AABBAABB(string chuoi)
        {
            var subString = chuoi.Substring(0, 4);
            var subString1 = chuoi.Substring(4, 4);
            return CheckSoKepAABB(subString) && CheckSoKepAABB(subString1);
        }

        public static bool CheckTaxi4ABCDABCD(string chuoi)
        {
            var arrStr = chuoi.ToArray();
            return arrStr[0] == arrStr[4] && arrStr[1] == arrStr[5] && arrStr[2] == arrStr[6] && arrStr[3] == arrStr[7]
                    && arrStr[0] != arrStr[1] && arrStr[0] != arrStr[2] && arrStr[0] != arrStr[3] && arrStr[1] != arrStr[2]
                    && arrStr[1] != arrStr[3] && arrStr[2] != arrStr[3];
        }

        public static bool CheckSimDaoABBA(string chuoi)
        {
            var arrStr = chuoi.ToArray();
            return arrStr[0] == arrStr[3] && arrStr[1] == arrStr[2] && arrStr[0] != arrStr[1];
        }

        public static bool CheckSimDaoKep(string chuoi)
        {
            var subString = chuoi.Substring(0, 4);
            var subString1 = chuoi.Substring(4, 4);
            return CheckSimDaoABBA(subString) && CheckSimDaoABBA(subString1);
        }

        public static bool CheckSimDoiABCCBA(string chuoi)
        {
            var arrStr = chuoi.ToArray();
            return arrStr[0] == arrStr[5] && arrStr[1] == arrStr[4] && arrStr[2] == arrStr[3]
                && arrStr[0] != arrStr[1] && arrStr[0] != arrStr[2] && arrStr[1] != arrStr[2];
        }

        public static bool CheckSimGanh(string chuoi)
        {
            var arrStr = chuoi.ToArray();
            return arrStr[0] == arrStr[2] && arrStr[0] != arrStr[1];
        }
    }
}