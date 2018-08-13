using System.Web.Mvc;

namespace SimSoDep.Areas.Administrator
{
    public class AdministratorAreaRegistration : AreaRegistration
    {
        public override string AreaName
        {
            get
            {
                return "Administrator";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context)
        {
            context.MapRoute(
                "Administrator_Default",
                "Administrator/{controller}/{action}/{id}",
                new { controller = "Account", action = "LogOn", id = UrlParameter.Optional }
            );

            context.MapRoute(
                "Administrator_ImportExcel",
                "Administrator/{controller}/{action}/{id}",
                new { controller = "ImportExcel", action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}
