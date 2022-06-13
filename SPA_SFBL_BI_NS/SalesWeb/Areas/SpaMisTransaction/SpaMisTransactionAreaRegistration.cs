using System.Web.Mvc;

namespace SalesWeb.Areas.SpaMisTransaction
{
    public class SpaMisTransactionAreaRegistration : AreaRegistration 
    {
        public override string AreaName 
        {
            get 
            {
                return "SpaMisTransaction";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context) 
        {
            context.MapRoute(
                "SpaMisTransaction_default",
                "SpaMisTransaction/{controller}/{action}/{id}",
                new { action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}