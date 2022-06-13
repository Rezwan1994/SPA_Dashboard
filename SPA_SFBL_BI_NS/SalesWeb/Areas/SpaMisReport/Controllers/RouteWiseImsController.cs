using System;
using System.Web.Mvc;
using SalesWeb.Areas.SpaMisReport.Models.BEL;
using SalesWeb.Areas.SpaMisReport.Models.DAL;
using SalesWeb.Universal.Gateway;

namespace SalesWeb.Areas.SpaMisReport.Controllers
{
    [LogInChecker]
    public class RouteWiseImsController : Controller
    {
        RouteWiseImsDAL _routeWiseImsDAL = new RouteWiseImsDAL();
        public ActionResult frmRouteWiseIms()
        {
            return View();
        }


        [HttpPost]
        public ActionResult GeRouteWiseImsToday(string dCode, string rCode, string aCode, string tCode, string cCode)
        {
            var listData = _routeWiseImsDAL.GeRouteWiseImsToday(dCode, rCode, aCode, tCode, cCode);
            var data = Json(listData, JsonRequestBehavior.AllowGet);
            data.MaxJsonLength = int.MaxValue;
            return data;
        }

        [HttpPost]
        public ActionResult GeRouteWiseImsDateRange(string fDate, string tDate, string dCode, string rCode, string aCode, string tCode, string cCode)
        {
            var listData = _routeWiseImsDAL.GeRouteWiseImsDateRange(fDate, tDate, dCode, rCode, aCode, tCode, cCode);
            var data = Json(listData, JsonRequestBehavior.AllowGet);
            data.MaxJsonLength = int.MaxValue;
            return data;
        }

        [HttpPost]
        public ActionResult GeRouteWiseImsYesterday(string dCode, string rCode, string aCode, string tCode, string cCode)
        {
            var listData = _routeWiseImsDAL.GeRouteWiseImsYesterday(dCode, rCode, aCode, tCode, cCode);
            var data = Json(listData, JsonRequestBehavior.AllowGet);
            data.MaxJsonLength = int.MaxValue;
            return data;
        }
        [HttpPost]
        public ActionResult GeRouteWiseImsLastSevendays(string dCode, string rCode, string aCode, string tCode, string cCode)
        {
            var listData = _routeWiseImsDAL.GeRouteWiseImsLastSevendays(dCode, rCode, aCode, tCode, cCode);
            var data = Json(listData, JsonRequestBehavior.AllowGet);
            data.MaxJsonLength = int.MaxValue;
            return data;

        }
        [HttpPost]
        public ActionResult GeRouteWiseImsLastThirtydays(string dCode, string rCode, string aCode, string tCode, string cCode)
        {
            var listData = _routeWiseImsDAL.GeRouteWiseImsLastThirtydays(dCode, rCode, aCode, tCode, cCode);
            var data = Json(listData, JsonRequestBehavior.AllowGet);
            data.MaxJsonLength = int.MaxValue;
            return data;

        }
        [HttpPost]
        public ActionResult GeRouteWiseImsCurrentMonth(string dCode, string rCode, string aCode, string tCode, string cCode)
        {
            var listData = _routeWiseImsDAL.GeRouteWiseImsCurrentMonth(dCode, rCode, aCode, tCode, cCode);
            var data = Json(listData, JsonRequestBehavior.AllowGet);
            data.MaxJsonLength = int.MaxValue;
            return data;

        }
        [HttpPost]
        public ActionResult GeRouteWiseImsLastMonth(string dCode, string rCode, string aCode, string tCode, string cCode)
        {
            var listData = _routeWiseImsDAL.GeRouteWiseImsLastMonth(dCode, rCode, aCode, tCode, cCode);
            var data = Json(listData, JsonRequestBehavior.AllowGet);
            data.MaxJsonLength = int.MaxValue;
            return data;

        }




    }
}