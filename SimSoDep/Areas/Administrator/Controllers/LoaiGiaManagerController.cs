using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SimSoDep.Controllers;
using SimSoDep.Models;

namespace SimSoDep.Areas.Administrator.Controllers
{
    [Authorize]
    public class LoaiGiaManagerController : BaseController
    {
        //
        // GET: /Administrator/LoaiGia/

        public ActionResult Add()
        {
            ViewBag.StatusId = GetDanhMucStatus();
            return View();
        }

        [HttpPost]
        public ActionResult Add(LoaiGiaModel loaiGiaModel)
        {

            var result = SimSoDepRepository.SaveLoaiGia(loaiGiaModel);
            return RedirectToAction("LoaiGia", "Catagory");
        }

        public ActionResult Edit(int id)
        {
            ViewBag.StatusId = GetDanhMucStatus();
            var result = SimSoDepRepository.GetLoaiGiaById(id);
            return View(result);
        }

        [HttpPost]
        public ActionResult Edit(LoaiGiaModel loaiGiaModel)
        {
            var result = SimSoDepRepository.SaveLoaiGia(loaiGiaModel);
            return RedirectToAction("LoaiGia", "Catagory");
        }

        public ActionResult Delete(int id)
        {
            var result = SimSoDepRepository.GetLoaiGiaById(id);
            return View(result);
        }

        [HttpPost]
        public ActionResult DeletePost(int id)
        {
            var result = SimSoDepRepository.DeleteLoaiGia(id);
            return RedirectToAction("LoaiGia", "Catagory");
        }
    }
}
