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
    public class LoaiSimSoChiTietManagerController : BaseController
    {
        //
        // GET: /Administrator/LoaiSimSoChiTietManager/

        public ActionResult Add()
        {
            ViewBag.StatusId = GetDanhMucStatus();
            var listParent = SimSoDepRepository.GetListLoaiSimSo();
            ViewBag.LoaiSimSo = new SelectList(listParent, "Id", "Ten");
            return View();
        }

        [HttpPost]
        public ActionResult Add(LoaiSimChiTietModel loaiSimChiTietModel)
        {
            var result = SimSoDepRepository.AddLoaiSimChiTiet(loaiSimChiTietModel);
            return RedirectToAction("LoaiSimSoChiTiet", "Catagory");
        }

        public ActionResult Edit(int id)
        {
            ViewBag.StatusId = GetDanhMucStatus();
            var result = SimSoDepRepository.GetLoaiSimChiTietById(id);
            var listParent = SimSoDepRepository.GetListLoaiSimSo();
            ViewBag.LoaiSimSo = new SelectList(listParent, "Id", "Ten", result.Id);
            return View(result);
        }

        [HttpPost]
        public ActionResult Edit(LoaiSimChiTietModel loaiSimSoModel)
        {
            var result = SimSoDepRepository.SaveLoaiSimSoChiTiet(loaiSimSoModel);
            return RedirectToAction("LoaiSimSoChiTiet", "Catagory");
        }

        public ActionResult Delete(int id)
        {
            var result = SimSoDepRepository.GetLoaiSimChiTietById(id);
            return View(result);
        }

        [HttpPost]
        public ActionResult DeletePost(int id)
        {
            var result = SimSoDepRepository.DeleteLoaiSimChiTietById(id);
            return RedirectToAction("LoaiSimSoChiTiet", "Catagory");
        }
    }
}
