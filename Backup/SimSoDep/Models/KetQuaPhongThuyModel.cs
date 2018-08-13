using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SimSoDep.Models
{
    public class KetQuaPhongThuyModel
    {
        public string SoDienThoai { get; set; }

        public int SoSoAm { get; set; }
        public int SoSoDuong { get; set; }
        public int LoaiAmDuong { get; set; }

        public int IdNguHanhNam { get; set; }
        public int IdNguHanhSo { get; set; }
        public string TenNguHanhNam { get; set; }
        public string TenNguHanhSo { get; set; }

        public int LoaiQue { get; set; }
        public string TenQue { get; set; }
        public string HinhQue { get; set; }
        public string YNghia { get; set; }

        public bool IsContainSo8 { get; set; }

        public int SoNuoc { get; set; }

    }
}