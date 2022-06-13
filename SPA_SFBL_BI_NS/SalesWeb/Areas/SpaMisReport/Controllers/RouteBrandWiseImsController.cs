using System;
using System.Web.Mvc;
using SalesWeb.Areas.SpaMisReport.Models.BEL;
using SalesWeb.Areas.SpaMisReport.Models.DAL;
using SalesWeb.Universal.Gateway;

namespace SalesWeb.Areas.SpaMisReport.Controllers
{
    [LogInChecker]
    public class RouteBrandWiseImsController : Controller
    {
        RouteBrandWiseImsDAL _routeBrandWiseImsDAL = new RouteBrandWiseImsDAL();
        public ActionResult frmRouteBrandWiseIms()
        {
            return View();
        }

        [HttpPost]
        public ActionResult GetRouteBrandImsToday(string dCode, string rCode, string aCode, string tCode, string cCode)
        {
            var listData = _routeBrandWiseImsDAL.GetRouteBrandImsToday(dCode, rCode, aCode, tCode, cCode);
            var data = Json(listData, JsonRequestBehavior.AllowGet);
            data.MaxJsonLength = int.MaxValue;
            return data;
        }
        [HttpPost]
        public ActionResult GetRouteBrandImsDateRange(string fDate, string tDate, string dCode, string rCode, string aCode, string tCode, string cCode)
        {
            var listData = _routeBrandWiseImsDAL.GetRouteBrandImsDateRange(fDate, tDate, dCode, rCode, aCode, tCode, cCode);
            var data = Json(listData, JsonRequestBehavior.AllowGet);
            data.MaxJsonLength = int.MaxValue;
            return data;
        }

        [HttpPost]
        public ActionResult GetRouteBrandImsYesterday(string dCode, string rCode, string aCode, string tCode, string cCode)
        {
            var listData = _routeBrandWiseImsDAL.GetRouteBrandImsYesterday(dCode, rCode, aCode, tCode, cCode);
            var data = Json(listData, JsonRequestBehavior.AllowGet);
            data.MaxJsonLength = int.MaxValue;
            return data;
        }
        [HttpPost]
        public ActionResult GetRouteBrandImsLastSevendays(string dCode, string rCode, string aCode, string tCode, string cCode)
        {
            var listData = _routeBrandWiseImsDAL.GetRouteBrandImsLastSevendays(dCode, rCode, aCode, tCode, cCode);
            var data = Json(listData, JsonRequestBehavior.AllowGet);
            data.MaxJsonLength = int.MaxValue;
            return data;

        }
        [HttpPost]
        public ActionResult GetRouteBrandImsLastThirtydays(string dCode, string rCode, string aCode, string tCode, string cCode)
        {
            var listData = _routeBrandWiseImsDAL.GetRouteBrandImsLastThirtydays(dCode, rCode, aCode, tCode, cCode);
            var data = Json(listData, JsonRequestBehavior.AllowGet);
            data.MaxJsonLength = int.MaxValue;
            return data;

        }

        [HttpPost]
        public ActionResult GetRouteBrandImsCurrentMonth(string dCode, string rCode, string aCode, string tCode, string cCode)
        {
            var listData = _routeBrandWiseImsDAL.GetRouteBrandImsCurrentMonth(dCode, rCode, aCode, tCode, cCode);
            var data = Json(listData, JsonRequestBehavior.AllowGet);
            data.MaxJsonLength = int.MaxValue;
            return data;

        }

        [HttpPost]
        public ActionResult GetRouteBrandImsLastMonth(string dCode, string rCode, string aCode, string tCode, string cCode)
        {
            var listData = _routeBrandWiseImsDAL.GetRouteBrandImsLastMonth(dCode, rCode, aCode, tCode, cCode);
            var data = Json(listData, JsonRequestBehavior.AllowGet);
            data.MaxJsonLength = int.MaxValue;
            return data;

        }














    }
}