using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SimSoDep.Areas.Administrator.Models
{
    public class SimSoDepBaseModels 
    {
        public string SoDienThoai { get; set; }
        public string GiaTien { get; set; }
    }
    
    public class SimSoDaiLyModes:SimSoDepBaseModels
    {
        public string TenDaiLy { get; set; }
        public int DaiLyTrietKhau { get; set; }
        public long DaiLyThuVe { get; set; }
        public long MinhDuoc { get; set; }
        public long GiaDaiLy { get; set; }
    }

    public class CheckSoModel:SimSoDepBaseModels
    {
        public string TenQue { get; set; }
        public string NguHanh { get; set; }
        public int SoNuoc { get; set; }
        public string LoaiAmDuong { get; set; }
    }
}