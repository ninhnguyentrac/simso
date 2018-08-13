using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SimSoDep.Controllers;
using SimSoDep.Models;

namespace SimSoDep.Areas.Administrator.Controllers
{
    [Authorize]
    public class ImportExcelController : BaseController
    {
        //
        // GET: /Administrator/ImportExcel/

        public ActionResult Index()
        {
            var result = SimSoDepRepository.GetListDaiLy();
            ViewBag.DaiLy = new SelectList(result, "Id", "Ten");
            return View();
        }

        // This action handles the form POST and the upload
        [HttpPost]
        public ActionResult Import(ImportModel importModel)
        {
            // Verify that the user selected a file


            var importSuccess = SimSoDepRepository.ImportExcel(importModel);

            ViewBag.Result = importSuccess;
            return View();
        }
    }

}
