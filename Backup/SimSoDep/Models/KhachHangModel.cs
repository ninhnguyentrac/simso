using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace SimSoDep.Models
{
    public class KhachHangModel
    {
        [Required(ErrorMessage = "Bạn chưa nhập tên")]
        [StringLength(250, ErrorMessage = "Độ dài tên khách hàng tối đa là 250 ký tự")]
        [Display(Name = "Tên khách hàng")]
        public string TenKhachHang { get; set; }

        [Required(ErrorMessage = "Bạn chưa nhập địa chỉ")]
        [StringLength(1000, ErrorMessage = "Độ dài địa chỉ tối đa là 1000 ký tự")]
        [Display(Name = "Địa chỉ")]
        public string DiaChi { get; set; }

        [Required(ErrorMessage = "Bạn chưa nhập số điện thoại")]
        [StringLength(11,ErrorMessage = "Độ dài số điện thoại là 10 hoặc 11 số", MinimumLength = 10)]
        [Display(Name = "Số điện thoại")]
        [RegularExpression(@"^\d+$", ErrorMessage = "Số điện thoại chứa ký tự không hợp lệ")]
        public string SoDienThoai { get; set; }

        [Display(Name = "Email")]
        public string Email { get; set; }

        [Display(Name = "Ghi chú")]
        public string GhiChu { get; set; }

        public SimSoModel SimSoModel { get; set; }
    }
}