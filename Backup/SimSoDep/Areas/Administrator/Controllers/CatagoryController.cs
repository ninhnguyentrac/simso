using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SimSoDep.Controllers;

namespace SimSoDep.Areas.Administrator.Controllers
{
    [Authorize]
    public class CatagoryController : BaseController
    {
        //
        // GET: /Administrator/Catagory/

        public ActionResult Index()
        {
            return View();
        }
        
        public ActionResult LoaiSimSo()
        {
            var result = SimSoDepRepository.GetListLoaiSimSo();
            return View(result);
        }

        public ActionResult NhaMang()
        {
            var result = SimSoDepRepository.GetMenuNhaMang(null);
            return View(result);
        }

        public ActionResult LoaiSimSoChiTiet()
        {
            var result = SimSoDepRepository.GetListLoaiSimSoChiTiet();
            return View(result);
        }

        public ActionResult DauSo()
        {
            var result = SimSoDepRepository.GetListLoaiDauSo(true);
            return View(result);
        }

        public ActionResult LoaiGia()
        {
            var result = SimSoDepRepository.GetListLoaiGia(true);
            return View(result);
        }
    }
}
