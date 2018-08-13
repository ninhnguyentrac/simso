using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SimSoDep.DataBasesManager
{
    public enum LoaiQue
    {
        Hung = 0,
        Cat = 1,
        KhongHungKhongCat = 2,
    }

    public enum LoaiAmDuong
    {
        AmDuongLechThaiCuc = 0,
        AmDuongLechNhieu = 1,
        AmDuongGanCanBang = 2,
        AmDuongCanBang = 3,
    }

    public enum NguHanh
    {
        Tho = 4,
        Moc = 2,
        Kim = 1,
        Thuy = 3,
        Hoa = 5,
    }

    public enum BatQuai
    {
        Can=1,
        Doai=2,
        Ly=3,
        Chan=4,
        Ton=5,
        Kham=6,
        Caan=7,
        Khon=8,
    }

    public enum LoaiSimSo
    {
        SimTuQuy = 1,
        SimLucQuy = 2,
        SimNguQuy = 3,
        SimTaxiLap2 = 4,
        SimTaxiLap3 = 5,
        SimTaxiLap4 = 6,
        SimTienDoi = 7,
        SimTienDon = 8,
        SimTienGiua = 9,
        SimTamHoaKep = 10,
        SimTamHoaDon = 11,
        SimKep = 12,
        SimKepKhac = 13,
        SimLap = 14,
        SimLapKhac = 15,
        SimDoi = 16,
        SimDao = 17,
        SimGanh = 18,
        SimOngDia = 19,
        SimThanTai = 20,
        SimLocPhat = 21,
        SimTuQuyGiua = 22,
        SimLucQuyGiua = 23,
        SimNguQuyGiua = 24,
        SimTamHoaGiua = 25,
        SimNamSinh = 26,
        SimNgayThangNam = 27,
        SimGiaRe = 28,
        SimDeNho = 29,
        SimDacBiet = 30,
        SimDauSoCo = 31,
        SimDepTuNhien = 32
    }

    public enum DauSo
    {
        Dau090 = 1,
        Dau091 = 2,
        Dau092 = 3,
        Dau093 = 4,
        Dau094 = 5,
        Dau095 = 6,
        Dau096 = 7,
        Dau097 = 8,
        Dau098 = 9,
        Dau099 = 10
    }

    public enum NhaMang
    {
        SimSoDepViettel = 1,
        SimSoDepMobifone = 2,
        SimSoDepVinaphone = 3,
        SimSoDepVietnamobile = 4,
        SimSoDepSfone = 5,
        SimSoDepBeeline = 6,
        SimSoDepCoDinh = 7
    }

    public enum DoDaiSo
    {
        Dai10So = 1,
        Dai11So = 2
    }

    public enum LoaiGia
    {
        Tu100TrDen1Ty = 1,
        Tu50TrDen100Tr = 2,
        Tu20TrDen50Tr = 3,
        Tu10TrDen20Tr = 4,
        Tu5TrDen10Tr = 5,
        Tu2TrDen5Tr = 6,
        Tu1TrDen2Tr = 7,
        Tu05TrDen1Tr = 8,
    }

    public enum LoaiSimSoChiTiet
    {
        TuQuy0000 = 1,
        TuQuy1111 = 2,
        TuQuy2222 = 3,
        TuQuy3333 = 4,
        TuQuy4444 = 5,
        TuQuy5555 = 6,
        TuQuy6666 = 7,
        TuQuy7777 = 8,
        TuQuy8888 = 9,
        TuQuy9999 = 10,
        TaxiABCABC = 11,
        TaxiABAABA = 12,
        TaxiAABAAB = 13,
        TaxiBAABAA = 14,
        TaxiAABCAABC = 15,
        TaxiABBCABBC = 16,
        TaxiABCCABCC = 17,
        TaxiAABBAABB = 18,
        TaxiABCDABCD = 19,
        Tieng2DuoiCuoi = 20,
        Tien3DuoiCuoi = 21,
        TienDoiKhac = 22,
        TienDacBiet = 23,
        Tien3SoCuoi = 24,
        Tien4SoCuoi = 25,
        Tien5SoCuoi = 26,
        Tien6SoCuoi = 27,
        TienDonKhac = 28,
        TienGiuaChonLoc = 29,
        TamHoa000 = 30,
        TamHoa111 = 31,
        TamHoa222 = 32,
        TamHoa333 = 33,
        TamHoa444 = 34,
        TamHoa555 = 35,
        TamHoa666 = 36,
        TamHoa777 = 37,
        TamHoa888 = 38,
        TamHoa999 = 39,
        KepAABB = 40,
        KepAABBCC = 41,
        KepAABBAA = 42,
        KepAABBCCDD = 43,
        KepAABBAACC = 44,
        KepAABBCCBB = 45,
        KepAAXAAY = 46,
        KepXAAYAA = 47,
        KepAAXYAA = 48,
        KepAAXBBY = 49,
        KepXAAYBB = 50,
        KepSaoAABBSao = 51,
        LapXABYAB = 52,
        LapABXABY = 53,
        LapAXBAYB = 54,
        LapABXYAB = 55,
        LapSaoABABSao = 56,
        LapXABCYABC = 57,
        DaoDon = 58,
        DaoKep = 59,
        TamHoaGiuaChonLoc = 60,
        SimVip = 62,
        NhatNhatKhongNhi = 63,
        MaiMaiKhongChet = 64,
        BonMuaKhongThatBat = 65,
        KhongNamNaoThatBat = 66,
        SinhTaiLocPhat = 67,
        MotNamBonMuaLocPhat = 68,
        BanBeNeSo = 69,
        SanBangTatCa = 70,
        CaoHonNguoi = 71,
        TayTrangDiLen = 72,
        KhongGapHan = 73,
        MoiNamMotPhat = 74,
        MotBuocLenTroi = 75,
        MoiNamMoiLocMoiPhat = 76
    }

    public enum DauSoStatus
    {
        HienThi = 1,
        KhongHienThi = 2
    }
}