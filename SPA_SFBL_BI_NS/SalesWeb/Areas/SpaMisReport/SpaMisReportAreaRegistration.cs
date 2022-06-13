using System.Web.Mvc;

namespace SalesWeb.Areas.SpaMisReport
{
    public class SpaMisReportAreaRegistration : AreaRegistration 
    {
        public override string AreaName 
        {
            get 
            {
                return "SpaMisReport";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context) 
        {
            context.MapRoute(
                "SpaMisReport_default",
                "SpaMisReport/{controller}/{action}/{id}",
                new { action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}