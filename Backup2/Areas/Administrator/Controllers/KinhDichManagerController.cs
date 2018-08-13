using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SimSoDep.Controllers;
using SimSoDep.DataBasesManager;
using SimSoDep.Models;

namespace SimSoDep.Areas.Administrator.Controllers
{
    [Authorize]
    public class KinhDichManagerController : BaseController
    {
        //
        // GET: /Administrator/KinhDichManager/

        public ActionResult Index()
        {
            var result = SimSoDepRepository.GetListKinhDich();
            return View(result);
        }

        //
        // GET: /Administrator/KinhDichManager/Details/5

        public ActionResult Details(int id)
        {
            return View();
        }

        //
        // GET: /Administrator/KinhDichManager/Create

        public ActionResult Create()
        {
            ViewBag.LoaiQue = GetLoaiQue();
            ViewBag.QueThuong = new SelectList(SimSoDepRepository.GetListBatQuai(), "IdQueBatQuai", "TenQue");
            ViewBag.QueHa = new SelectList(SimSoDepRepository.GetListBatQuai(), "IdQueBatQuai", "TenQue");
            return View();
        }

        //
        // POST: /Administrator/KinhDichManager/Create

        [HttpPost]
        [ValidateInput(false)]
        public ActionResult Create(KinhDichModel kinhDichModel)
        {
            try
            {
                // TODO: Add insert logic here
                var result = SimSoDepRepository.SaveKinhDich(kinhDichModel);

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        //
        // GET: /Administrator/KinhDichManager/Edit/5

        public ActionResult Edit(int id)
        {
            var result = SimSoDepRepository.GetKinhDichById(id);
            ViewBag.QueThuong = new SelectList(SimSoDepRepository.GetListBatQuai(), "IdQueBatQuai", "TenQue", result.IdQueThuong);
            ViewBag.QueHa = new SelectList(SimSoDepRepository.GetListBatQuai(), "IdQueBatQuai", "TenQue", result.IdQueHa);
            ViewBag.LoaiQue = GetLoaiQue(result.IdLoaiQue);
            return View(result);
        }

        //
        // POST: /Administrator/KinhDichManager/Edit/5

        [HttpPost]
        public ActionResult Edit(int id, KinhDichModel kinhDichModel)
        {
            try
            {
                // TODO: Add update logic here
                var result = SimSoDepRepository.SaveKinhDich(kinhDichModel);
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        //
        // GET: /Administrator/KinhDichManager/Delete/5

        public ActionResult Delete(int id)
        {
            var result = SimSoDepRepository.GetKinhDichById(id);
            return View(result);
        }

        //
        // POST: /Administrator/KinhDichManager/Delete/5

        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here
                var result = SimSoDepRepository.DeleteKinhDich(id);
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        public SelectList GetLoaiQue(object selected = null)
        {
            var cat = new SelectListItem()
            {
                Value = ((int)LoaiQue.Cat).ToString(),
                Text = "Cát"
            };
            var hung = new SelectListItem()
            {
                Value = ((int)LoaiQue.Hung).ToString(),
                Text = "Hung"
            };
            var koHungKoCat = new SelectListItem()
            {
                Value = ((int)LoaiQue.KhongHungKhongCat).ToString(),
                Text = "Không hung không cát"
            };
            var listItem = new List<SelectListItem> {cat, hung, koHungKoCat};
            var list = new SelectList(listItem, "Value", "Text", selected);
            return list;
        }
    }
}
