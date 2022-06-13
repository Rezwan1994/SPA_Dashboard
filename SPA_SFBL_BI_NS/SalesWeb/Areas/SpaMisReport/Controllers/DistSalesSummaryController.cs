using System;
using System.Web.Mvc;
using SalesWeb.Areas.SpaMisReport.Models.BEL;
using SalesWeb.Areas.SpaMisReport.Models.DAL;
using SalesWeb.Universal.Gateway;

namespace SalesWeb.Areas.SpaMisReport.Controllers
{
    [LogInChecker]
    public class DistSalesSummaryController : Controller
    {
        DistSalesSummaryDAL _distSalesSummaryDAL = new DistSalesSummaryDAL();



        public ActionResult frmDistSalesSummary()
        {
            return View();
        }

        [HttpPost]
        public ActionResult GetDistProductImsToday(string dCode, string rCode, string aCode, string tCode, string cCode)
        {
            var listData = _distSalesSummaryDAL.GetDistProductImsToday(dCode, rCode, aCode, tCode, cCode);
            var data = Json(listData, JsonRequestBehavior.AllowGet);
            data.MaxJsonLength = int.MaxValue;
            return data;

        }



        [HttpPost]
        public ActionResult GetDistProductImsDateRange(string fDate, string tDate, string dCode, string rCode, string aCode, string tCode, string cCode)
        {
            var listData = _distSalesSummaryDAL.GetDistProductImsDateRange(fDate, tDate,dCode, rCode, aCode, tCode, cCode);
            var data = Json(listData, JsonRequestBehavior.AllowGet);
            data.MaxJsonLength = int.MaxValue;
            return data;

        }


        [HttpPost]
        public ActionResult GetDistProductImsYesterday(string dCode, string rCode, string aCode, string tCode, string cCode)
        {
            var listData = _distSalesSummaryDAL.GetDistProductImsYesterday(dCode, rCode, aCode, tCode, cCode);
            var data = Json(listData, JsonRequestBehavior.AllowGet);
            data.MaxJsonLength = int.MaxValue;
            return data;

        }



        [HttpPost]
        public ActionResult GetDistProductImsLastSevendays(string dCode, string rCode, string aCode, string tCode, string cCode)
        {
            var listData = _distSalesSummaryDAL.GetDistProductImsLastSevendays(dCode, rCode, aCode, tCode, cCode);
            var data = Json(listData, JsonRequestBehavior.AllowGet);
            data.MaxJsonLength = int.MaxValue;
            return data;

        }



        [HttpPost]
        public ActionResult GetDistProductImsLastThirtydays(string dCode, string rCode, string aCode, string tCode, string cCode)
        {
            var listData = _distSalesSummaryDAL.GetDistProductImsLastThirtydays(dCode, rCode, aCode, tCode, cCode);
            var data = Json(listData, JsonRequestBehavior.AllowGet);
            data.MaxJsonLength = int.MaxValue;
            return data;

        }



        [HttpPost]
        public ActionResult GetDistProductImsCurrentMonth(string dCode, string rCode, string aCode, string tCode, string cCode)
        {
            var listData = _distSalesSummaryDAL.GetDistProductImsCurrentMonth(dCode, rCode, aCode, tCode, cCode);
            var data = Json(listData, JsonRequestBehavior.AllowGet);
            data.MaxJsonLength = int.MaxValue;
            return data;

        }


        [HttpPost]
        public ActionResult GetDistProductImsLastMonth(string dCode, string rCode, string aCode, string tCode, string cCode)
        {
            var listData = _distSalesSummaryDAL.GetDistProductImsLastMonth(dCode, rCode, aCode, tCode, cCode);
            var data = Json(listData, JsonRequestBehavior.AllowGet);
            data.MaxJsonLength = int.MaxValue;
            return data;

        }










    }


}