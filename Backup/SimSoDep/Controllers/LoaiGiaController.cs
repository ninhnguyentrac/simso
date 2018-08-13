using System.Web.Mvc;

namespace SimSoDep.Controllers
{
    public class LoaiGiaController : BaseController
    {
        public ActionResult GetMenuLoaiGia()
        {
            var result = SimSoDepRepository.GetListLoaiGia(false);
            return PartialView(result);
        }

        public ActionResult ChiTiet(string id)
        {
            var result = SimSoDepRepository.GetListSimSoByLoaiGia(id);
            ViewBag.DataDisplay = SimSoDepRepository.GetTenLoaiGia(id);
            return View(result);
        }
    }
}
