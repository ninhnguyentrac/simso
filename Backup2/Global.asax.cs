using System;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using SimSoDep.DataBasesManager;
using SimSoDep.Models;

namespace SimSoDep
{
    // Note: For instructions on enabling IIS6 or IIS7 classic mode, 
    // visit http://go.microsoft.com/?LinkId=9394801

    public class MvcApplication : System.Web.HttpApplication
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
        }

        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("favicon.ico");
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute(
                "Default", // Route name
                "{controller}/{action}/{id}", // URL with parameters
                new { controller = "Home", action = "Index", id = UrlParameter.Optional } // Parameter defaults
            );

            routes.MapRoute(
                "Default1", // Route name
                "{controller}/{action}/{id}", // URL with parameters
                new { controller = "LoaiSimSo", action = "Index", id = UrlParameter.Optional } // Parameter defaults
            );
        }

        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            RegisterGlobalFilters(GlobalFilters.Filters);
            RegisterRoutes(RouteTable.Routes);
        }

        protected void Session_Start(object sender, EventArgs e)
        {

            var ip = Request.ServerVariables["HTTP_X_FORWARDED_FOR"];
            if (!string.IsNullOrEmpty(ip))
            {
                if (ip.IndexOf(",") > 0)
                {
                    string[] ipRange = ip.Split(',');
                    int le = ipRange.Length - 1;
                    ip = ipRange[le];
                }
            }
            else
            {
                ip = Request.UserHostAddress;
            }
            var logUser = new SimSoDepRepository();
            logUser.AddLogUser(ip);
            var simSoModel = logUser.ThongKeSim();
            SessionManagerModel.ThongKeSimSession = simSoModel;
        }

        protected void Session_End(object sender, EventArgs e)
        {

        }
    }
}