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
    public class DaiLyManagerController : BaseController
    {
        //
        // GET: /Administrator/DaiLyManager/

        public ActionResult Index()
        {
            var result = SimSoDepRepository.GetListDaiLy();
            return View(result);
        }

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Create(DaiLyModel daiLyModel)
        {
            var result = SimSoDepRepository.AddDaiLy(daiLyModel);
            return RedirectToAction("Index");
        }

        public ActionResult Edit(int id)
        {
            var result = SimSoDepRepository.GetDaiLyById(id);
            return View(result);
        }

        [HttpPost]
        public ActionResult Edit(DaiLyModel daiLyModel)
        {
            var result = SimSoDepRepository.AddDaiLy(daiLyModel);
            return RedirectToAction("Index");
        }

        public ActionResult Delete(int id)
        {
            var result = SimSoDepRepository.GetDaiLyById(id);
            return View(result);
        }
        [HttpPost]
        public ActionResult DeletePost(int id)
        {
            var result = SimSoDepRepository.DeleteDaiLyById(id);
            return RedirectToAction("Index");
        }

        public ActionResult AddTrietKhau(int id)
        {
            ViewBag.MaDaiLy = id;
            return View();
        }

        [HttpPost]
        public ActionResult AddTrietKhau(TrietKhauModel trietKhauModel)
        {
            var result = SimSoDepRepository.SaveTrietKhauDaiLy(trietKhauModel);
            return RedirectToAction("Edit", new {id = trietKhauModel.MaDaiLy});
        }

        public ActionResult EditTrietKhau(int id)
        {
            
            var result = SimSoDepRepository.GetTrietKhauDaiLy(id);
            return View(result);
        }
        [HttpPost]
        public ActionResult EditTrietKhau(TrietKhauModel trietKhauModel)
        {

            var result = SimSoDepRepository.SaveTrietKhauDaiLy(trietKhauModel);
            return RedirectToAction("Edit", new { id = trietKhauModel.MaDaiLy });
        }

        public ActionResult DeleteTrietKhau(int id)
        {
            var result = SimSoDepRepository.GetTrietKhauDaiLy(id);
            return View(result);
        }
        [HttpPost]
        public ActionResult DeleteTrietKhauPost(int id, int MaDaiLy)
        {
            var result = SimSoDepRepository.DeleteTrietKhauDaiLy(id);
            return RedirectToAction("Edit", new { id = MaDaiLy });
        }

        public ActionResult AddTangThem(int id)
        {
            ViewBag.MaDaiLy = id;
            return View();
        }

        [HttpPost]
        public ActionResult AddTangThem(TangThemModel tangThemModel)
        {
            var result = SimSoDepRepository.SaveTangThemDaiLy(tangThemModel);
            return RedirectToAction("Edit", new { id = tangThemModel.MaDaiLy });
        }

        public ActionResult EditTangThem(int id)
        {
            var result = SimSoDepRepository.GetTangThemById(id);
            return View(result);
        }
        [HttpPost]
        public ActionResult EditTangThem(TangThemModel tangThemModel)
        {
            var result = SimSoDepRepository.SaveTangThemDaiLy(tangThemModel);
            return RedirectToAction("Edit", new { id = tangThemModel.MaDaiLy });
        }

        public ActionResult DeleteTangThem(int id)
        {
            var result = SimSoDepRepository.GetTangThemById(id);
            return View(result);
        }

        [HttpPost]
        public ActionResult DeleteTangThemPost(int id, int maDaiLy)
        {
            var result = SimSoDepRepository.DeleteTangThem(id);
            return RedirectToAction("Edit", new { id = maDaiLy });
        }
    }
}
