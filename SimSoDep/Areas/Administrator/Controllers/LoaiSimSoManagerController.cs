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
    public class LoaiSimSoManagerController : BaseController
    {
        //
        // GET: /Administrator/LoaiSinSo/

        public ActionResult Add()
        {
            ViewBag.StatusId = GetDanhMucStatus();
            return View();
        }

        [HttpPost]
        public ActionResult Add(LoaiSimModel loaiSimModel)
        {
            var result = SimSoDepRepository.AddLoaiSim(loaiSimModel);
            return RedirectToAction("LoaiSimSo", "Catagory");
        }

        public ActionResult Edit(int id)
        {
            ViewBag.StatusId = GetDanhMucStatus();
            var result = SimSoDepRepository.GetLoaiSimById(id);
            return View(result);
        }

        [HttpPost]
        public ActionResult Edit(LoaiSimModel loaiSimSoModel)
        {
            var result = SimSoDepRepository.SaveLoaiSimSo(loaiSimSoModel);
            return RedirectToAction("LoaiSimSo","Catagory");
        }

        public ActionResult Delete(int id)
        {
            var result = SimSoDepRepository.GetLoaiSimById(id);
            return View(result);
        }

        [HttpPost]
        public ActionResult DeletePost(int id)
        {
            var result = SimSoDepRepository.DeleteLoaiSimById(id);
            return RedirectToAction("LoaiSimSo", "Catagory");
        }

    }
}
