using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace SalesWeb.Universal.Gateway
{
    public class LogInChecker : ActionFilterAttribute
    {
        private int _count = 0;
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            HttpSessionStateBase session = filterContext.HttpContext.Session;
            if (session["USER_ID"] == null)
            {
                if (filterContext.HttpContext.Request.IsAjaxRequest())
                {
                    _count += 1;
                    //filterContext.Result = new JsonResult() { Data = "Home/Login", JsonRequestBehavior = JsonRequestBehavior.AllowGet };
                    //base.OnActionExecuting(filterContext);
                    filterContext.HttpContext.Response.StatusCode = 500+_count;
                    filterContext.HttpContext.Response.StatusDescription = "Home/Login";

                    filterContext.Result = new JsonResult
                    {
                        Data = new { Redirect = "Home/Login" },
                        JsonRequestBehavior = JsonRequestBehavior.AllowGet
                    };
                }
                else
                {
                    filterContext.Result = new RedirectToRouteResult(
                        new RouteValueDictionary {
                        { "Controller", "Home" },
                        { "Action", "Login" },
                        { "Area", ""},
                        { "param", "SessionOut" }
                        //
                        });
                }
            }
            else
            {
                _count = 0;
            }
        }
    }
}