using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SimSoDep.Controllers
{
    public class LoaiSimSoController : BaseController
    {
        //
        // GET: /LoaiSim/
        
        //
        // GET: /Menu/

        public ActionResult GetMenuLoaiSim()
        {
            var result = SimSoDepRepository.GetMenuLoaiSim();
            return PartialView(result);
        }

        public ActionResult ChiTiet(string id) {
            var result = SimSoDepRepository.GetListSimSoByLoaiSim(id);
            ViewBag.DataDisplay = SimSoDepRepository.GetTenLoaiSim(id);
            return View(result);
        }
    }
}
