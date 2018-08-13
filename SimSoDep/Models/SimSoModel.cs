
namespace SimSoDep.Models
{
    public class SimSoModel
    {
        public long MaSoDienThoai { get; set; }
        public string SoDienThoai { get; set; }
        public string NhaMang { get; set; }
        public long GiaTien { get; set; }
    }

    public class SimSoPhongThuyModel:SimSoModel
    {
        public int Diem { get; set; }
    }
}