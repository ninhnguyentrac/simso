using System.Web.Mvc;
using SimSoDep.Controllers;
using SimSoDep.Models;

namespace SimSoDep.Areas.Administrator.Controllers
{
    [Authorize]
    public class NhaMangManagerController : BaseController
    {
        //
        // GET: /Administrator/NhaMangManager/

        public ActionResult Add()
        {
            ViewBag.StatusId = GetDanhMucStatus();
            return View();
        }

        [HttpPost]
        public ActionResult Add(NhaMangModel nhaMangModel)
        {
            var result = SimSoDepRepository.AddNhaMang(nhaMangModel);
            return RedirectToAction("NhaMang", "Catagory");
        }

        public ActionResult Edit(int id)
        {
            ViewBag.StatusId = GetDanhMucStatus();
            var result = SimSoDepRepository.GetNhaMang(id);
            return View(result);
        }

        [HttpPost]
        public ActionResult Edit(NhaMangModel loaiSimSoModel)
        {
            var result = SimSoDepRepository.SaveNhaMang(loaiSimSoModel);
            return RedirectToAction("NhaMang", "Catagory");
        }

        public ActionResult Delete(int id)
        {
            var result = SimSoDepRepository.GetNhaMang(id);
            return View(result);
        }

        [HttpPost]
        public ActionResult DeletePost(int id)
        {
            var result = SimSoDepRepository.DeleteNhaMang(id);
            return RedirectToAction("NhaMang", "Catagory");
        }
    }
}
