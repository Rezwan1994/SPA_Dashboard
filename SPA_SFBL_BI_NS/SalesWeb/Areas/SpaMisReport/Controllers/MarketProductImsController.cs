using System;
using System.Web.Mvc;
using SalesWeb.Areas.SpaMisReport.Models.BEL;
using SalesWeb.Areas.SpaMisReport.Models.DAL;
using SalesWeb.Universal.Gateway;

namespace SalesWeb.Areas.SpaMisReport.Controllers
{
    [LogInChecker]
    public class MarketProductImsController : Controller
    {
        MarketProductImsDAL _marketProductImsDAL = new MarketProductImsDAL();
        public ActionResult frmMarketProductIms()
        {
            return View();
        }
        [HttpPost]
        public ActionResult GetMarketProductImsToday(string dCode, string rCode, string aCode, string tCode, string cCode)
        {
            var listData = _marketProductImsDAL.GetMarketProductImsToday(dCode, rCode, aCode, tCode, cCode);
            var data = Json(listData, JsonRequestBehavior.AllowGet);
            data.MaxJsonLength = int.MaxValue;
            return data;

        }
        [HttpPost]
        public ActionResult GetMarketProductImsDateRange(string fDate, string tDate, string dCode, string rCode, string aCode, string tCode, string cCode)
        {
            var listData = _marketProductImsDAL.GetMarketProductImsDateRange(fDate, tDate,dCode, rCode, aCode, tCode, cCode);
            var data = Json(listData, JsonRequestBehavior.AllowGet);
            data.MaxJsonLength = int.MaxValue;
            return data;

        }
        [HttpPost]
        public ActionResult GetMarketProductImsYesterday(string dCode, string rCode, string aCode, string tCode, string cCode)
        {
            var listData = _marketProductImsDAL.GetMarketProductImsYesterday(dCode, rCode, aCode, tCode, cCode);
            var data = Json(listData, JsonRequestBehavior.AllowGet);
            data.MaxJsonLength = int.MaxValue;
            return data;

        }


        [HttpPost]
        public ActionResult GetMarketProductImsLastSevendays(string dCode, string rCode, string aCode, string tCode, string cCode)
        {
            var listData = _marketProductImsDAL.GetMarketProductImsLastSevendays(dCode, rCode, aCode, tCode, cCode);
            var data = Json(listData, JsonRequestBehavior.AllowGet);
            data.MaxJsonLength = int.MaxValue;
            return data;

        }



        [HttpPost]
        public ActionResult GetMarketProductImsLastThirtydays(string dCode, string rCode, string aCode, string tCode, string cCode)
        {
            var listData = _marketProductImsDAL.GetMarketProductImsLastThirtydays(dCode, rCode, aCode, tCode, cCode);
            var data = Json(listData, JsonRequestBehavior.AllowGet);
            data.MaxJsonLength = int.MaxValue;
            return data;

        }



        [HttpPost]
        public ActionResult GetMarketProductImsCurrentMonth(string dCode, string rCode, string aCode, string tCode, string cCode)
        {
            var listData = _marketProductImsDAL.GetMarketProductImsCurrentMonth(dCode, rCode, aCode, tCode, cCode);
            var data = Json(listData, JsonRequestBehavior.AllowGet);
            data.MaxJsonLength = int.MaxValue;
            return data;

        }


        [HttpPost]
        public ActionResult GetMarketProductImsLastMonth(string dCode, string rCode, string aCode, string tCode, string cCode)
        {
            var listData = _marketProductImsDAL.GetMarketProductImsLastMonth(dCode, rCode, aCode, tCode, cCode);
            var data = Json(listData, JsonRequestBehavior.AllowGet);
            data.MaxJsonLength = int.MaxValue;
            return data;

        }













    }
}