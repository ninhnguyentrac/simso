using System.Web.Mvc;

namespace SimSoDep.Controllers
{
    public class NhaMangController : BaseController
    {
        //
        // GET: /NhaMang/

        public ActionResult ChiTiet(string id)
        {
            var result = SimSoDepRepository.GetListSimSoByNhaMang(id);
            ViewBag.DataDisplay = SimSoDepRepository.GetTenNhaMang(id);
            return View(result);
        }

        public ActionResult GetMenuNhaMang() {
            var result = SimSoDepRepository.GetMenuNhaMang(true);
            //ViewBag.ListLoaiSim = SimSoDepRepository.GetMenuLoaiSim();
            return PartialView(result);
        }
    }
}
