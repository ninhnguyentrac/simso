using System.Web;

namespace SimSoDep.Models
{
    public class ImportModel
    {
        public int DaiLy { get; set; }
        public HttpPostedFileBase ExcelFile { get; set; }
        public string TxtSoDienThoai { get; set; }
    }
}