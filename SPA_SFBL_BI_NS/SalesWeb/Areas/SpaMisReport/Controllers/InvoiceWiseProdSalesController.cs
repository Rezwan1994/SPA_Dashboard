using System;
using System.Web.Mvc;
using SalesWeb.Areas.SpaMisReport.Models.BEL;
using SalesWeb.Areas.SpaMisReport.Models.DAL;
using SalesWeb.Universal.Gateway;

namespace SalesWeb.Areas.SpaMisReport.Controllers
{
    [LogInChecker]
    public class InvoiceWiseProdSalesController : Controller
    {
        InvoiceWiseProdSalesDAL _InvoiceWiseProdSalesDAL = new InvoiceWiseProdSalesDAL();
        public ActionResult frmInvoiceWiseProdSales()
        {
            return View();
        }


        [HttpPost]
        public ActionResult GetInvoiceWiseProdSales(string fDate, string tDate, string pCode, string dCode, string rCode, string aCode, string tCode, string cCode)
        {
            var listData = _InvoiceWiseProdSalesDAL.GetInvoiceWiseProdSales(fDate, tDate, pCode, dCode, rCode, aCode, tCode, cCode);
            var data = Json(listData, JsonRequestBehavior.AllowGet);
            data.MaxJsonLength = int.MaxValue;
            return data;
        }









    }
}