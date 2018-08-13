using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SimSoDep.Models;

namespace SimSoDep.Controllers
{
    public class PhongThuyController : BaseController
    {
        [HttpPost]
        public ActionResult Index(PhongThuyModel phongThuyModel)
        {
            var isdataTime = false;
            try
            {
                var vnCal = new VietnameseCalendar();
                var date = new DateTime(phongThuyModel.Nam, phongThuyModel.Thang, phongThuyModel.Ngay);
                isdataTime = true;
                phongThuyModel.SoDienThoai = phongThuyModel.SoDienThoai.Trim();
                int yyyy, mm, dd;
                vnCal.FromDateTime(date, out yyyy, out mm, out dd);
                var result = SimSoDepRepository.BoiPhongThuy(phongThuyModel.SoDienThoai, yyyy);
                var tenNam = vnCal.GetYearName(yyyy);
                var tenThang = vnCal.GetMonthName(yyyy, mm);
                var tenNgay = vnCal.GetDayName(date);
                ViewBag.phongThuyModel = phongThuyModel;
                return View(result);
            }
            catch (Exception)
            {
                if (!isdataTime)
                    return PartialView("NgayThangKhongDung");
                return PartialView("ExceptionBreak");

            }
        }

        public ActionResult BoiPhongThuy(PhongThuyModel phongThuyModel)
        {
            var isdataTime = false;
            try
            {
                var vnCal = new VietnameseCalendar();
                var date = new DateTime(phongThuyModel.Nam, phongThuyModel.Thang, phongThuyModel.Ngay);
                isdataTime = true;
                phongThuyModel.SoDienThoai = phongThuyModel.SoDienThoai.Trim();
                int yyyy, mm, dd;
                vnCal.FromDateTime(date, out yyyy, out mm, out dd);
                var result = SimSoDepRepository.BoiPhongThuy(phongThuyModel.SoDienThoai,yyyy);
                var tenNam = vnCal.GetYearName(yyyy);
                var tenThang = vnCal.GetMonthName(yyyy,mm);
                var tenNgay = vnCal.GetDayName(date);
                return PartialView("BoiPhongThuy");
            }
            catch (Exception)
            {
                if (!isdataTime)
                    return PartialView("NgayThangKhongDung");
                return PartialView("ExceptionBreak");

            }
        }
    }
}
