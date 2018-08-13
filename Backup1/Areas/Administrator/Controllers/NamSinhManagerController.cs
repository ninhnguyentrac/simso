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
    public class NamSinhManagerController : BaseController
    {
        //
        // GET: /Administrator/NamSinhManager/

        public ActionResult Index()
        {
            var result = SimSoDepRepository.GetListBanMenh();
            return View(result);
        }


        //
        // GET: /Administrator/NamSinhManager/Create

        public ActionResult Create()
        {
            ViewBag.NguHanh = new SelectList(SimSoDepRepository.GetListNguHanh(), "IdNguHanh", "Ten");

            return View();
        }

        //
        // POST: /Administrator/NamSinhManager/Create

        [HttpPost]
        public ActionResult Create(NamSinhModel namSinhModel)
        {
            try
            {
                // TODO: Add insert logic here
                var result = SimSoDepRepository.SaveNameSinh(namSinhModel);
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        //
        // GET: /Administrator/NamSinhManager/Edit/5

        public ActionResult Edit(int id)
        {
            var result = SimSoDepRepository.GetNamSinhById(id);
            ViewBag.NguHanh = new SelectList(SimSoDepRepository.GetListNguHanh(), "IdNguHanh", "Ten", result.IdNguHanh);
            return View(result);
        }

        //
        // POST: /Administrator/NamSinhManager/Edit/5

        [HttpPost]
        public ActionResult Edit(NamSinhModel namSinhModel)
        {
            try
            {
                // TODO: Add update logic here
                var result = SimSoDepRepository.SaveNameSinh(namSinhModel);
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        //
        // GET: /Administrator/NamSinhManager/Delete/5

        public ActionResult Delete(int id)
        {
            var result = SimSoDepRepository.GetNamSinhById(id);
            return View(result);
        }

        //
        // POST: /Administrator/NamSinhManager/Delete/5

        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here
                var result = SimSoDepRepository.DeleteNamSinh(id);
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}
