using System.Collections.Generic;

namespace SimSoDep.Models
{
    public class BaseLoaiModel
    {
        public int Id { get; set; }
        public string Ten { get; set; }
        public string Alias { get; set; }
        public string EnumFixed { get; set; }
        public string MoTa { get; set; }
        public int? SapXep { get; set; }
        public int? Status { get; set; }
        public string TenStatus { get; set; }
        public string MetaDescription { get; set; }
        public string MetaKeyword { get; set; }
    }

    public class LoaiSimModel:BaseLoaiModel
    {
        public List<LoaiSimModel> ListLoaiSimChiTiet { get; set; }
    }

    public class LoaiSimChiTietModel:BaseLoaiModel
    {
        public int MaLoaiSim { get; set; }
    }


    public class NhaMangModel : BaseLoaiModel
    {
        public string TenDayDu { get; set; }
    }

    public class DauSoModel:BaseLoaiModel
    {
        public int MaNhaMang { get; set; }
        public string TenNhaMang { get; set; }
    }

    public class LoaiGiaModel:BaseLoaiModel
    {
        public long GiaTu { get; set; }
        public long Den { get; set; }
    }

    public class DaiLyModel:BaseLoaiModel
    {
        public string DiDong { get; set; }
        public string MayBan { get; set; }
        public string DiaChi { get; set; }
        public string Email { get; set; }
        public List<TrietKhauModel> TrietKhaus { get; set; }
        public List<TangThemModel> TangThems { get; set; } 
    }

}