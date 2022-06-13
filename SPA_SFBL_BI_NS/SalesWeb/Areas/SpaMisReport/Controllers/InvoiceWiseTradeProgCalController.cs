using System;
using System.Web.Mvc;
using SalesWeb.Areas.SpaMisReport.Models.BEL;
using SalesWeb.Areas.SpaMisReport.Models.DAL;
using SalesWeb.Universal.Gateway;

namespace SalesWeb.Areas.SpaMisReport.Controllers
{
    [LogInChecker]
    public class InvoiceWiseTradeProgCalController : Controller
    {
        InvoiceWiseTradeProgCalDAL _invoiceWiseTradeProgCalDAL = new InvoiceWiseTradeProgCalDAL();
        public ActionResult frmInvoiceWiseTradeProgCal()
        {
            return View();
        }

        [HttpGet]
        public ActionResult GetTradeProgramList()
        {
            try
            {
                var data = _invoiceWiseTradeProgCalDAL.GetTradeProgramList();
                return Json(new { Status = _invoiceWiseTradeProgCalDAL.ExceptionReturn, Data = data }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception e)
            {
                return Json(new { Status = e.Message, Data = " " }, JsonRequestBehavior.AllowGet);
            }
        }


        [HttpPost]
        public ActionResult GetInvoiceWiseTradeProgramCalculation(string fDate, string tDate, string dCode, string rCode, string aCode, string tCode, string cCode, string tProgramNo)
        {
            var listData = _invoiceWiseTradeProgCalDAL.GetInvoiceWiseTradeProgramCalculation(fDate, tDate, dCode, rCode, aCode, tCode, cCode, tProgramNo);
            var data = Json(listData, JsonRequestBehavior.AllowGet);
            data.MaxJsonLength = int.MaxValue;
            return data;
        }


    }
}