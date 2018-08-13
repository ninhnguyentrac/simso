
namespace SimSoDep.Models
{
    public class KinhDichModel
    {
        public int IdKinhDich { get; set; }
        public int IdQueThuong { get; set; }
        public string QueThuong { get; set; }
        public int IdQueHa { get; set; }
        public string QueHa { get; set; }
        public string TenQue { get; set; }
        public string PhienAm { get; set; }
        public string LoiDich { get; set; }
        public string YNghia { get; set; }
        public string HinhQue { get; set; }
        public int? IdLoaiQue { get; set; }
        public string TenLoaiQue { get; set; }
    }
}