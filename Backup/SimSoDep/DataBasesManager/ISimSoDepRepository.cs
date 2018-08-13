using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SimSoDep.Areas.Administrator.Models;
using SimSoDep.Models;

namespace SimSoDep.DataBasesManager
{
    public interface ISimSoDepRepository
    {
        #region Account

        bool ValidateUser(string userName, string passWord);

        bool ChangePassword(string userName, string oldPass, string newPass);

        #endregion

        #region Import

        int ImportExcel(ImportModel importModel);

        #endregion

        #region simso

        List<SimSoModel> GetListSimSoByLoaiGia(string id);

        List<SimSoModel> GetListSimSoByNhaMang(string id);

        List<SimSoModel> GetListSimSoByDauSo(string id);

        List<SimSoModel> GetListSimSoByLoaiSim(string id);

        List<SimSoModel> GetListSimSoByLoaiSimChiTiet(string id);

        List<SimSoModel> GetListSimSo();

        ViewBagModel GetTenLoaiGia(string id);

        ViewBagModel GetTenLoaiSim(string id);

        ViewBagModel GetTenDauSo(string id);

        ViewBagModel GetTenNhaMang(string id);

        ViewBagModel GetTenLoaiSimChiTiet(string id);


        SimSoModel GetSimSoById(long id);

        bool DatSim(long id, KhachHangModel khachHangModel);

        List<SimSoDaiLyModes> TimKiemSoManager(string so, int? madaily);

        #endregion

        #region LoaiSim

        List<LoaiSimModel> GetMenuLoaiSim();

        List<LoaiSimModel> GetListLoaiSimSo();

        bool SaveLoaiSimSo(LoaiSimModel loaiSimSoModel);

        LoaiSimModel GetLoaiSimById(int id);

        bool DeleteLoaiSimById(int id);

        bool AddLoaiSim(LoaiSimModel loaiSimModel);

        #endregion

        #region nhamang

        List<NhaMangModel> GetMenuNhaMang(bool? hienthi = null);

        NhaMangModel GetNhaMang(int id);

        bool SaveNhaMang(NhaMangModel loaiSimSoModel);

        bool DeleteNhaMang(int id);

        bool AddNhaMang(NhaMangModel nhaMangModel);

        #endregion

        #region loaisimchitiet

        List<LoaiSimChiTietModel> GetListLoaiSimSoChiTiet();

        LoaiSimChiTietModel GetLoaiSimChiTietById(int id);

        bool SaveLoaiSimSoChiTiet(LoaiSimChiTietModel loaiSimSoModel);

        bool DeleteLoaiSimChiTietById(int id);

        bool AddLoaiSimChiTiet(LoaiSimChiTietModel loaiSimChiTietModel);

        #endregion

        #region dauso

        List<DauSoModel> GetListLoaiDauSo(bool showAll);

        DauSoModel GetDauSoById(int id);

        bool SaveDauSo(DauSoModel dauSoModel);

        bool DeleteDauSo(int id);

        bool AddDauSo(DauSoModel dauSoModel);

        #endregion

        #region loaigia

        List<LoaiGiaModel> GetListLoaiGia(bool showAll);

        bool SaveLoaiGia(LoaiGiaModel loaiGiaModel);

        LoaiGiaModel GetLoaiGiaById(int id);

        bool DeleteLoaiGia(int id);

        #endregion

        #region daily

        List<DaiLyModel> GetListDaiLy();

        bool AddDaiLy(DaiLyModel daiLyModel);

        DaiLyModel GetDaiLyById(int id);

        bool DeleteDaiLyById(int id);

        bool SaveTrietKhauDaiLy(TrietKhauModel trietKhauModel);

        TrietKhauModel GetTrietKhauDaiLy(int id);

        bool DeleteTrietKhauDaiLy(int id);

        bool SaveTangThemDaiLy(TangThemModel tangThemModel);

        TangThemModel GetTangThemById(int id);

        bool DeleteTangThem(int id);

        #endregion

        #region BanMenh

        List<NamSinhModel> GetListBanMenh();

        bool SaveNameSinh(NamSinhModel namSinhModel);

        NamSinhModel GetNamSinhById(int id);

        bool DeleteNamSinh(int idNamSinh);

        #endregion

        #region NguHanh

        List<NguHanhModel> GetListNguHanh();

        #endregion

        #region BatQuai

        List<BatQuaiModel> GetListBatQuai();

        bool AddBatQuai(BatQuaiModel batQuaiModel);

        bool SaveBatQuai(int id, BatQuaiModel batQuaiModel);

        BatQuaiModel GetBatQuaiById(int id);

        bool DeleteBatQuai(int id);

        #endregion

        #region KinhDich

        List<KinhDichModel> GetListKinhDich();

        bool SaveKinhDich(KinhDichModel kinhDichModel);

        KinhDichModel GetKinhDichById(int id);

        bool DeleteKinhDich(int id);

        #endregion

        #region Tim kiem

        List<SimSoModel> TimKiemSimSo(TimKiemModel timKiemModel);

        #endregion

        #region LogUser

        void AddLogUser(string clientIp);

        #endregion

        #region Boiphongthuy

        KetQuaPhongThuyModel BoiPhongThuy(string soDienThoai, int yyyy);

        #endregion

        #region check so

        List<CheckSoModel> CheckSo(string path);

        #endregion

        #region

        ThongKeSimModel ThongKeSim();

        #endregion
    }
}
