using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SimSoDep.Models;

namespace SimSoDep.Controllers
{
    public class DatMuaController : BaseController
    {

        public ActionResult Index(KhachHangModel khachHang)
        {
            return View();
        }

        public ActionResult Loi(KhachHangModel khachHang)
        {

            return View();
        }

        public ActionResult DatSim(long id)
        {
            var result = SimSoDepRepository.GetSimSoById(id);
            var khachHang = new KhachHangModel {SimSoModel = result};
            return View(khachHang);
        }

        [HttpPost]
        [ValidateInput(false)]
        public ActionResult DatSim(long id,KhachHangModel khachHangModel)
        {
            var result = SimSoDepRepository.DatSim(id, khachHangModel);
            if(result)
            return RedirectToAction("Index");
            return RedirectToAction("Loi");
        }
    }
}
