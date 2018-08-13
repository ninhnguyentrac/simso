using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SimSoDep.DataBasesManager;

namespace SimSoDep.Controllers
{
    public class BaseController : Controller
    {
        public ISimSoDepRepository SimSoDepRepository;

       public BaseController()
       {
           SimSoDepRepository=new SimSoDepRepository();
       }
       public SelectList GetDanhMucStatus(object selected = null)
       {
           var hienThi = new SelectListItem()
           {
               Value = ((int)DauSoStatus.HienThi).ToString(),
               Text = "Hiển thị"
           };
           var khongHienThi = new SelectListItem()
           {
               Value = ((int)DauSoStatus.KhongHienThi).ToString(),
               Text = "Không hiển thị"
           };
           var listItem = new List<SelectListItem> { hienThi, khongHienThi };
           var list = new SelectList(listItem, "Value", "Text", selected);
           return list;
       }
    }
}
