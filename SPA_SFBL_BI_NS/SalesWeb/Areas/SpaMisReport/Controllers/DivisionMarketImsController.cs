using System;
using System.Web.Mvc;
using SalesWeb.Areas.SpaMisReport.Models.BEL;
using SalesWeb.Areas.SpaMisReport.Models.DAL;
using SalesWeb.Universal.Gateway;

namespace SalesWeb.Areas.SpaMisReport.Controllers
{
    [LogInChecker]
    public class DivisionMarketImsController : Controller
    {
        DivisionMarketImsDAL _divisionMarketImsDAL = new DivisionMarketImsDAL();
        public ActionResult frmDivisionMarketIms()
        {
            return View();
        }

        [HttpPost]
        public ActionResult GetDivMktImsYesterday(string dCode, string rCode, string aCode, string tCode, string cCode)
        {
            var listData = _divisionMarketImsDAL.GetDivMktImsYesterday(dCode, rCode, aCode, tCode, cCode);
            var data = Json(listData, JsonRequestBehavior.AllowGet);
            data.MaxJsonLength = int.MaxValue;
            return data;
        }

        [HttpPost]
        public ActionResult GetDivMktImsDateRange(string fDate, string tDate, string dCode, string rCode, string aCode, string tCode, string cCode)
        {
            var listData = _divisionMarketImsDAL.GetDivMktImsDateRange(fDate, tDate,dCode, rCode, aCode, tCode, cCode);
            var data = Json(listData, JsonRequestBehavior.AllowGet);
            data.MaxJsonLength = int.MaxValue;
            return data;
        }


        [HttpPost]
        public ActionResult GetDivMktImsToday(string dCode, string rCode, string aCode, string tCode, string cCode)
        {
            var listData = _divisionMarketImsDAL.GetDivMktImsToday(dCode, rCode, aCode, tCode, cCode);
            var data = Json(listData, JsonRequestBehavior.AllowGet);
            data.MaxJsonLength = int.MaxValue;
            return data;
        }
        [HttpPost]
        public ActionResult GetDivMktImsLastSevendays(string dCode, string rCode, string aCode, string tCode, string cCode)
        {
            var listData = _divisionMarketImsDAL.GetDivMktImsLastSevendays(dCode, rCode, aCode, tCode, cCode);
            var data = Json(listData, JsonRequestBehavior.AllowGet);
            data.MaxJsonLength = int.MaxValue;
            return data;

        }
        [HttpPost]
        public ActionResult GetDivMktImsLastThirtydays(string dCode, string rCode, string aCode, string tCode, string cCode)
        {
            var listData = _divisionMarketImsDAL.GetDivMktImsLastThirtydays(dCode, rCode, aCode, tCode, cCode);
            var data = Json(listData, JsonRequestBehavior.AllowGet);
            data.MaxJsonLength = int.MaxValue;
            return data;

        }

        [HttpPost]
        public ActionResult GetDivMktImsCurrentMonth(string dCode, string rCode, string aCode, string tCode, string cCode)
        {
            var listData = _divisionMarketImsDAL.GetDivMktImsCurrentMonth(dCode, rCode, aCode, tCode, cCode);
            var data = Json(listData, JsonRequestBehavior.AllowGet);
            data.MaxJsonLength = int.MaxValue;
            return data;

        }

        [HttpPost]
        public ActionResult GetDivMktImsLastMonth(string dCode, string rCode, string aCode, string tCode, string cCode)
        {
            var listData = _divisionMarketImsDAL.GetDivMktImsLastMonth(dCode, rCode, aCode, tCode, cCode);
            var data = Json(listData, JsonRequestBehavior.AllowGet);
            data.MaxJsonLength = int.MaxValue;
            return data;

        }














    }
}