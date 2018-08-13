
namespace SimSoDep.Models
{
    public class BaseTangTrietKhauModel
    {
        public int Id { get; set; }
        public int MaDaiLy { get; set; }
        public long GiaTu { get; set; }
        public long Den { get; set; }
    }

    public class TangThemModel:BaseTangTrietKhauModel
    {
        public int TangThem { get; set; }
    }

    public class TrietKhauModel:BaseTangTrietKhauModel
    {
        public int TrietKhau { get; set; }
    }

}