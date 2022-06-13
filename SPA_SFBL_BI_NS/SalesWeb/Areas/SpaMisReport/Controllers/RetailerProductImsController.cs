using System;
using System.Web.Mvc;
using SalesWeb.Areas.SpaMisReport.Models.BEL;
using SalesWeb.Areas.SpaMisReport.Models.DAL;
using SalesWeb.Universal.Gateway;
using System.Data;
using System.Web;



namespace SalesWeb.Areas.SpaMisReport.Controllers
{
    [LogInChecker]
    public class RetailerProductImsController : Controller
    {
        RetailerProductImsDAL _retailerProductImsDAL = new RetailerProductImsDAL();

        public ActionResult frmRetailerProductIms()
        {
            return View();
        }
        [HttpGet]
        public ActionResult GetProductList(string baseProductCode, string brandCode, string categoryCode)
        {
            try
            {
                var data = _retailerProductImsDAL.GetProductList( baseProductCode,  brandCode,  categoryCode);
                return Json(new { Status = _retailerProductImsDAL.ExceptionReturn, Data = data }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception e)
            {
                return Json(new { Status = e.Message, Data = " " }, JsonRequestBehavior.AllowGet);
            }
        }
        [HttpPost]
        public ActionResult GetRetailerProductImsToday(string dCode, string rCode, string aCode, string tCode, string cCode, string pBaseProductCode, string pBrandCode, string pCategoryCode, string pCode)
        {
            var listData = _retailerProductImsDAL.GetRetailerProductImsToday(dCode, rCode, aCode, tCode, cCode, pBaseProductCode, pBrandCode, pCategoryCode, pCode);
            var data = Json(listData, JsonRequestBehavior.AllowGet);
            data.MaxJsonLength = int.MaxValue;
            return data;
        }
        [HttpPost]
        public ActionResult GetRetailerProductImsDateRange(string fDate, string tDate, string dCode, string rCode, string aCode, string tCode, string cCode, string pBaseProductCode, string pBrandCode, string pCategoryCode, string pCode)
        {
            var listData = _retailerProductImsDAL.GetRetailerProductImsDateRange(fDate, tDate,dCode, rCode, aCode, tCode, cCode, pBaseProductCode, pBrandCode, pCategoryCode, pCode);
            var data = Json(listData, JsonRequestBehavior.AllowGet);
            data.MaxJsonLength = int.MaxValue;
            return data;
        }
        [HttpPost]
        public ActionResult GetRetailerProductImsYesterday(string dCode, string rCode, string aCode, string tCode, string cCode, string pBaseProductCode, string pBrandCode, string pCategoryCode, string pCode)
        {
            var listData = _retailerProductImsDAL.GetRetailerProductImsYesterday(dCode, rCode, aCode, tCode, cCode, pBaseProductCode, pBrandCode, pCategoryCode, pCode);
            var data = Json(listData, JsonRequestBehavior.AllowGet);
            data.MaxJsonLength = int.MaxValue;
            return data;
        }
        [HttpPost]
        public ActionResult GetRetailerProductImsLastSevendays(string dCode, string rCode, string aCode, string tCode, string cCode, string pBaseProductCode, string pBrandCode, string pCategoryCode, string pCode)
        {
            var listData = _retailerProductImsDAL.GetRetailerProductImsLastSevendays(dCode, rCode, aCode, tCode, cCode, pBaseProductCode, pBrandCode, pCategoryCode, pCode);
            var data = Json(listData, JsonRequestBehavior.AllowGet);
            data.MaxJsonLength = int.MaxValue;
            return data;

        }
        [HttpPost]
        public ActionResult GetRetailerProductImsLastThirtydays(string dCode, string rCode, string aCode, string tCode, string cCode, string pBaseProductCode, string pBrandCode, string pCategoryCode, string pCode)
        {
            var listData = _retailerProductImsDAL.GetRetailerProductImsLastThirtydays(dCode, rCode, aCode, tCode, cCode, pBaseProductCode, pBrandCode, pCategoryCode, pCode);
            var data = Json(listData, JsonRequestBehavior.AllowGet);
            data.MaxJsonLength = int.MaxValue;
            return data;

        }
        [HttpPost]
        public ActionResult GetRetailerProductImsCurrentMonth(string dCode, string rCode, string aCode, string tCode, string cCode, string pBaseProductCode, string pBrandCode, string pCategoryCode, string pCode)
        {
            var listData = _retailerProductImsDAL.GetRetailerProductImsCurrentMonth(dCode, rCode, aCode, tCode, cCode, pBaseProductCode, pBrandCode, pCategoryCode, pCode);
            var data = Json(listData, JsonRequestBehavior.AllowGet);
            data.MaxJsonLength = int.MaxValue;
            return data;

        }
        [HttpPost]
        public ActionResult GetRetailerProductImsLastMonth(string dCode, string rCode, string aCode, string tCode, string cCode, string pBaseProductCode, string pBrandCode, string pCategoryCode, string pCode)
        {
            var listData = _retailerProductImsDAL.GetRetailerProductImsLastMonth(dCode, rCode, aCode, tCode, cCode, pBaseProductCode, pBrandCode, pCategoryCode, pCode);
            var data = Json(listData, JsonRequestBehavior.AllowGet);
            data.MaxJsonLength = int.MaxValue;
            return data;

        }

    }
}