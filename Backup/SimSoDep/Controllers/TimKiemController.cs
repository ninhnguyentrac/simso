using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SimSoDep.Models;

namespace SimSoDep.Controllers
{
    public class TimKiemController : BaseController
    {
        //
        // GET: /TimKiem/
        [ValidateInput(false)]
        public ActionResult Index(TimKiemModel timKiemModel)
        {

            timKiemModel.SearchString = (string.IsNullOrEmpty(timKiemModel.SearchString))?string.Empty:timKiemModel.SearchString.Trim();
            ViewBag.SearchStr = timKiemModel.SearchString;
            var result = SimSoDepRepository.TimKiemSimSo(timKiemModel);
            return View(result);
        }
        
    }
}
