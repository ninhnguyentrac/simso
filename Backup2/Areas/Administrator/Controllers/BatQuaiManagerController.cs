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
    public class BatQuaiManagerController : BaseController
    {
        //
        // GET: /Administrator/BatQuaiManager/

        public ActionResult Index()
        {
            var result = SimSoDepRepository.GetListBatQuai();
            return View(result);
        }

        //
        // GET: /Administrator/BatQuaiManager/Create

        public ActionResult Create()
        {
            return View();
        } 

        //
        // POST: /Administrator/BatQuaiManager/Create

        [HttpPost]
        [ValidateInput(false)]
        public ActionResult Create(BatQuaiModel batQuaiModel)
        {
            try
            {
                // TODO: Add insert logic here
                var result = SimSoDepRepository.AddBatQuai(batQuaiModel);
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
        
        //
        // GET: /Administrator/BatQuaiManager/Edit/5
        
        public ActionResult Edit(int id)
        {
            var result = SimSoDepRepository.GetBatQuaiById(id);
            return View(result);
        }

        //
        // POST: /Administrator/BatQuaiManager/Edit/5

        [HttpPost]
        public ActionResult Edit(int id, BatQuaiModel collection)
        {
            try
            {
                // TODO: Add update logic here
                var result = SimSoDepRepository.SaveBatQuai(id,collection);
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        //
        // GET: /Administrator/BatQuaiManager/Delete/5
 
        public ActionResult Delete(int id)
        {
            var result = SimSoDepRepository.GetBatQuaiById(id);
            return View(result);
        }

        //
        // POST: /Administrator/BatQuaiManager/Delete/5

        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here
                var result = SimSoDepRepository.DeleteBatQuai(id);
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}
