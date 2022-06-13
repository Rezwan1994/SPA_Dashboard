
using System.Web.Mvc;
using SalesWeb.Areas.SpaMisReport.Models.DAL;
using SalesWeb.Universal.Gateway;


namespace SalesWeb.Areas.SpaMisReport.Controllers
{
    [LogInChecker]
    public class RetailerImsController : Controller
    {
        RetailerImsDAL _retailerImsDAL = new RetailerImsDAL();

        public ActionResult frmRetailerIms()
        {
            return View();
        }

        [HttpGet]
        public ActionResult GetReportDownLoadStatus(string url)
        {
            var listData = _retailerImsDAL.GetReportDownLoadStatus(url);
            var data = Json(listData, JsonRequestBehavior.AllowGet);
            data.MaxJsonLength = int.MaxValue;
            return data;

        }

        [HttpPost]
        public ActionResult GetRetailerImsToDay(string dCode, string rCode, string aCode, string tCode, string cCode)
        {
            var listData = _retailerImsDAL.GetRetailerImsToDay(dCode, rCode, aCode, tCode, cCode);
            var data = Json(listData, JsonRequestBehavior.AllowGet);
            data.MaxJsonLength = int.MaxValue;
            return data;

        }
        [HttpPost]
        public ActionResult GetRetailerImsUptoCurMonth(string dCode, string rCode, string aCode, string tCode, string cCode)
        {
            var listData = _retailerImsDAL.GetRetailerImsUptoCurMonth(dCode, rCode, aCode, tCode, cCode);
            var data = Json(listData, JsonRequestBehavior.AllowGet);
            data.MaxJsonLength = int.MaxValue;
            return data;

        }    
        [HttpPost]
        public ActionResult GetRetailerImsUptoPrevMonth(string dCode, string rCode, string aCode, string tCode, string cCode)
        {
            var listData = _retailerImsDAL.GetRetailerImsUptoPrevMonth(dCode, rCode, aCode, tCode, cCode);
            var data = Json(listData, JsonRequestBehavior.AllowGet);
            data.MaxJsonLength = int.MaxValue;
            return data;

        }
        [HttpPost]
        public ActionResult GetRetailerImsAnyDate(string fDate, string tDate, string dCode, string rCode, string aCode, string tCode, string cCode)
        {
            var listData = _retailerImsDAL.GetRetailerImsAnyDate(fDate, tDate, dCode, rCode, aCode, tCode, cCode);
            var data = Json(listData, JsonRequestBehavior.AllowGet);
            data.MaxJsonLength = int.MaxValue;
            return data;

        }
        [HttpPost]
        public ActionResult GeRetailerWiseImsYesterday(string dCode, string rCode, string aCode, string tCode, string cCode)
        {
            var listData = _retailerImsDAL.GeRetailerWiseImsYesterday(dCode, rCode, aCode, tCode, cCode);
            var data = Json(listData, JsonRequestBehavior.AllowGet);
            data.MaxJsonLength = int.MaxValue;
            return data;

        }
        [HttpPost]
        public ActionResult GeRetailerWiseImsLastSevendays(string dCode, string rCode, string aCode, string tCode, string cCode)
        {
            var listData = _retailerImsDAL.GeRetailerWiseImsLastSevendays(dCode, rCode, aCode, tCode, cCode);
            var data = Json(listData, JsonRequestBehavior.AllowGet);
            data.MaxJsonLength = int.MaxValue;
            return data;

        }
        [HttpPost]
        public ActionResult GeRetailerWiseImsLastThirtydays(string dCode, string rCode, string aCode, string tCode, string cCode)
        {
            var listData = _retailerImsDAL.GeRetailerWiseImsLastThirtydays(dCode, rCode, aCode, tCode, cCode);
            var data = Json(listData, JsonRequestBehavior.AllowGet);
            data.MaxJsonLength = int.MaxValue;
            return data;

        }




    }
}