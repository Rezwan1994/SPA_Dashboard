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
    public class InvoiceProductImsController : Controller
    {
        InvoiceProductImsDAL _invoiceProductImsDAL = new InvoiceProductImsDAL();
        public ActionResult frmInvoiceProductIms()
        {
            return View();
        }
        [HttpGet]
        public ActionResult GetProductList(string baseProductCode, string brandCode, string categoryCode)
        {
            try
            {
                var data = _invoiceProductImsDAL.GetProductList(baseProductCode, brandCode, categoryCode);
                return Json(new { Status = _invoiceProductImsDAL.ExceptionReturn, Data = data }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception e)
            {
                return Json(new { Status = e.Message, Data = " " }, JsonRequestBehavior.AllowGet);
            }
        }





        [HttpPost]
        public ActionResult GetInvoiceProductImsToday(string dCode, string rCode, string aCode, string tCode, string cCode, string pBaseProductCode, string pBrandCode, string pCategoryCode, string pCode)
        {
            var listData = _invoiceProductImsDAL.GetInvoiceProductImsToday(dCode, rCode, aCode, tCode, cCode, pBaseProductCode, pBrandCode, pCategoryCode, pCode);
            var data = Json(listData, JsonRequestBehavior.AllowGet);
            data.MaxJsonLength = int.MaxValue;
            return data;
        }

        public ActionResult GetInvoiceProductImsCurrentMonth(string dCode, string rCode, string aCode, string tCode, string cCode, string pBaseProductCode, string pBrandCode, string pCategoryCode, string pCode)
        {
            var listData = _invoiceProductImsDAL.GetInvoiceProductImsCurrentMonth(dCode, rCode, aCode, tCode, cCode, pBaseProductCode, pBrandCode, pCategoryCode, pCode);
            var data = Json(listData, JsonRequestBehavior.AllowGet);
            data.MaxJsonLength = int.MaxValue;
            return data;

        }
        [HttpPost]
        public ActionResult GetInvoiceProductImsLastMonth(string dCode, string rCode, string aCode, string tCode, string cCode, string pBaseProductCode, string pBrandCode, string pCategoryCode, string pCode)
        {
            var listData = _invoiceProductImsDAL.GetInvoiceProductImsLastMonth(dCode, rCode, aCode, tCode, cCode, pBaseProductCode, pBrandCode, pCategoryCode, pCode);
            var data = Json(listData, JsonRequestBehavior.AllowGet);
            data.MaxJsonLength = int.MaxValue;
            return data;

        }

        [HttpPost]
        public ActionResult GetInvoiceProductImsDateRange(string fDate, string tDate, string dCode, string rCode, string aCode, string tCode, string cCode, string pBaseProductCode, string pBrandCode, string pCategoryCode, string pCode)
        {
            var listData = _invoiceProductImsDAL.GetInvoiceProductImsDateRange(fDate, tDate, dCode, rCode, aCode, tCode, cCode, pBaseProductCode, pBrandCode, pCategoryCode, pCode);
            var data = Json(listData, JsonRequestBehavior.AllowGet);
            data.MaxJsonLength = int.MaxValue;
            return data;
        }


    }
}