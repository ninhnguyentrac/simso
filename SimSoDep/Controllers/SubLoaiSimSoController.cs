using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SimSoDep.Controllers
{
    public class SubLoaiSimSoController : BaseController
    {
        //
        // GET: /SubLoaiSimSo/

        public ActionResult ChiTiet(string id)
        {
            var result = SimSoDepRepository.GetListSimSoByLoaiSimChiTiet(id);
            ViewBag.DataDisplay = SimSoDepRepository.GetTenLoaiSimChiTiet(id);
            return View(result);
        }

    }
}
