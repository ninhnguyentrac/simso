using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SimSoDep.Controllers
{
    public class DauSoController : BaseController
    {
        //
        // GET: /DauSo/

        public ActionResult GetMenuDauSo()
        {
            var result = SimSoDepRepository.GetListLoaiDauSo(false);
            return PartialView(result);
        }

        //
        // GET: /DauSo/Details/5

        public ActionResult ChiTiet(string id)
        {
            var result = SimSoDepRepository.GetListSimSoByDauSo(id);
            ViewBag.DataDisplay = SimSoDepRepository.GetTenDauSo(id);
            return View(result);
        }

        //
        // GET: /DauSo/Create

        public ActionResult Create()
        {
            return View();
        } 

        //
        // POST: /DauSo/Create

        [HttpPost]
        public ActionResult Create(FormCollection collection)
        {
            try
            {
                // TODO: Add insert logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
        
        //
        // GET: /DauSo/Edit/5
 
        public ActionResult Edit(int id)
        {
            return View();
        }

        //
        // POST: /DauSo/Edit/5

        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add update logic here
 
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        //
        // GET: /DauSo/Delete/5
 
        public ActionResult Delete(int id)
        {
            return View();
        }

        //
        // POST: /DauSo/Delete/5

        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here
 
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}
