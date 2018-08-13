using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SimSoDep.Models
{
    public class PhongThuyModel
    {
        public PhongThuyModel(){}
        public PhongThuyModel(string sodienthoai, int ngay, int thang, int nam)
        {
            SoDienThoai = sodienthoai;
            Ngay = ngay;
            Thang = thang;
            Nam = nam;
        }
        public string SoDienThoai { get; set; }
        public int Ngay { get; set; }
        public int Thang { get; set; }
        public int Nam { get; set; }
    }
}