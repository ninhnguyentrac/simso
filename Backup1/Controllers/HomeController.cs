using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SimSoDep.Controllers
{
    public class HomeController : BaseController
    {
        public ActionResult Index()
        {
            var result = SimSoDepRepository.GetListSimSo();
            return View(result);
        }

        public ActionResult About()
        {
            return View();
        }

        public ActionResult LoaiSim()
        {

            return View();
        }
    }
}
