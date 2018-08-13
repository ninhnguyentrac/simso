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
    public class DauSoManagerController : BaseController
    {
        //
        // GET: /Administrator/DauSoManager/

        public ActionResult Add()
        {
            ViewBag.StatusId = GetDanhMucStatus();
            ViewBag.NhaMang = new SelectList(SimSoDepRepository.GetMenuNhaMang(null), "Id", "Ten");
            return View();
        }

        [HttpPost]
        public ActionResult Add(DauSoModel dauSoModel)
        {
            var result = SimSoDepRepository.AddDauSo(dauSoModel);
            return RedirectToAction("DauSo", "Catagory");
        }

        public ActionResult Edit(int id)
        {
            ViewBag.StatusId = GetDanhMucStatus();
            var result = SimSoDepRepository.GetDauSoById(id);
            ViewBag.NhaMang = new SelectList(SimSoDepRepository.GetMenuNhaMang(null), "Id", "Ten",result.MaNhaMang);
            return View(result);
        }

        [HttpPost]
        public ActionResult Edit(DauSoModel dauSoModel)
        {
            var result = SimSoDepRepository.SaveDauSo(dauSoModel);
            return RedirectToAction("DauSo", "Catagory");
        }

        public ActionResult Delete(int id)
        {
            var result = SimSoDepRepository.GetDauSoById(id);
            return View(result);
        }

        [HttpPost]
        public ActionResult DeletePost(int id)
        {
            var result = SimSoDepRepository.DeleteDauSo(id);
            return RedirectToAction("DauSo", "Catagory");
        }

    }
}
