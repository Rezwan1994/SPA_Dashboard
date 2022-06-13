using System;
using System.Web.Mvc;
using SalesWeb.Areas.SpaMisReport.Models.BEL;
using SalesWeb.Areas.SpaMisReport.Models.DAL;
using SalesWeb.Universal.Gateway;

namespace SalesWeb.Areas.SpaMisReport.Controllers
{
    [LogInChecker]
    public class RouteWiseOrderCountController : Controller
    {
        RouteWiseOrderCountDAL _routeWiseOrderCountDAL = new RouteWiseOrderCountDAL();
        public ActionResult frmRouteWiseOrderCount()
        {
            return View();
        }


        [HttpPost]
        public ActionResult GetRouteWiseOrderCountCurrentMonth(string dCode, string rCode, string aCode, string tCode, string cCode)
        {
            var listData = _routeWiseOrderCountDAL.GetRouteWiseOrderCountCurrentMonth(dCode, rCode, aCode, tCode, cCode);
            var data = Json(listData, JsonRequestBehavior.AllowGet);
            data.MaxJsonLength = int.MaxValue;
            return data;
        }

        [HttpPost]
        public ActionResult GetRouteWiseOrderCountLastMonth(string dCode, string rCode, string aCode, string tCode, string cCode)
        {
            var listData = _routeWiseOrderCountDAL.GetRouteWiseOrderCountLastMonth(dCode, rCode, aCode, tCode, cCode);
            var data = Json(listData, JsonRequestBehavior.AllowGet);
            data.MaxJsonLength = int.MaxValue;
            return data;
        }


        [HttpPost]
        public ActionResult GetRouteWiseOrderCountCustomDate(string fromDate, string toDate, string dCode, string rCode, string aCode, string tCode, string cCode)
        {
            var listData = _routeWiseOrderCountDAL.GetRouteWiseOrderCountCustomDate(fromDate, toDate, dCode, rCode, aCode, tCode, cCode);
            var data = Json(listData, JsonRequestBehavior.AllowGet);
            data.MaxJsonLength = int.MaxValue;
            return data;
        }

    }
}