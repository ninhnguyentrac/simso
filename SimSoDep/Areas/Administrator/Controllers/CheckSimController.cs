using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SimSoDep.Controllers;
using SimSoDep.DataBasesManager;

namespace SimSoDep.Areas.Administrator.Controllers
{
    public class CheckSimController : BaseController
    {
        //
        // GET: /Administrator/CheckSim/

        public ActionResult Index()
        {
            return View();
        }

        //
        // GET: /Administrator/CheckSim/Details/5

        public ActionResult Details(string txtString)
        {
            // Verify that the user selected a file

            var importSuccess = SimSoDepRepository.CheckSo(txtString);
            if (importSuccess != null)
                return View(importSuccess);

            // redirect back to the index action to show the form once again
            return RedirectToAction("Index");

        }

        //
        // GET: /Administrator/CheckSim/Create

        public ActionResult Create()
        {
            return View();
        }

        //
        // POST: /Administrator/CheckSim/Create

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
        // GET: /Administrator/CheckSim/Edit/5

        public ActionResult Edit(int id)
        {
            return View();
        }

        //
        // POST: /Administrator/CheckSim/Edit/5

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
        // GET: /Administrator/CheckSim/Delete/5

        public ActionResult Delete(int id)
        {
            return View();
        }

        //
        // POST: /Administrator/CheckSim/Delete/5

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
