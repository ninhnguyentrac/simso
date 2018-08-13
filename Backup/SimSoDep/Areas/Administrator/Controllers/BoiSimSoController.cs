using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SimSoDep.Controllers;

namespace SimSoDep.Areas.Administrator.Controllers
{
    [Authorize]
    public class BoiSimSoController : BaseController
    {
        //
        // GET: /Administrator/BoiSimSo/

        public ActionResult Index()
        {
            return View();
        }

        //
        // GET: /Administrator/BoiSimSo/Details/5

        public ActionResult Details(int id)
        {
            return View();
        }

        //
        // GET: /Administrator/BoiSimSo/Create

        public ActionResult Create()
        {
            return View();
        } 

        //
        // POST: /Administrator/BoiSimSo/Create

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
        // GET: /Administrator/BoiSimSo/Edit/5
 
        public ActionResult Edit(int id)
        {
            return View();
        }

        //
        // POST: /Administrator/BoiSimSo/Edit/5

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
        // GET: /Administrator/BoiSimSo/Delete/5
 
        public ActionResult Delete(int id)
        {
            return View();
        }

        //
        // POST: /Administrator/BoiSimSo/Delete/5

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
