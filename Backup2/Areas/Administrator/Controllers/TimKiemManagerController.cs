using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SimSoDep.Controllers;

namespace SimSoDep.Areas.Administrator.Controllers
{
    [Authorize]
    public class TimKiemManagerController : BaseController
    {
        //
        // GET: /Administrator/TimKiemManager/

        public ActionResult Index()
        {
            ViewBag.madaily = new SelectList(SimSoDepRepository.GetListDaiLy(), "Id", "Ten");
            return View();
        }


        public ActionResult TimSo(string so, int? madaily)
        {
            if (string.IsNullOrEmpty(so) && madaily == null)
                return RedirectToAction("Index");
            var result = SimSoDepRepository.TimKiemSoManager(so, madaily);
            ViewBag.SearchStr = so;
            ViewBag.madaily = new SelectList(SimSoDepRepository.GetListDaiLy(), "Id", "Ten");
            return View("Index", result);
        }

    }
}
