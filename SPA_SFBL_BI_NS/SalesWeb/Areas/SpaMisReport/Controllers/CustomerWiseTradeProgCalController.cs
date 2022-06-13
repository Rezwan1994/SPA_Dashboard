using System;
using System.Web.Mvc;
using SalesWeb.Areas.SpaMisReport.Models.BEL;
using SalesWeb.Areas.SpaMisReport.Models.DAL;
using SalesWeb.Universal.Gateway;


namespace SalesWeb.Areas.SpaMisReport.Controllers
{
    [LogInChecker]
    public class CustomerWiseTradeProgCalController : Controller
    {
        CustomerWiseTradeProgCalDAL _customerWiseTradeProgCalDAL = new CustomerWiseTradeProgCalDAL();
        public ActionResult frmCustomerWiseTradeProgCal()
        {
            return View();
        }

        [HttpPost]
        public ActionResult GetCustomerWiseTradeProgramCalculation(string fDate, string tDate, string dCode, string rCode, string aCode, string tCode, string cCode, string tProgramNo)
        {
            var listData = _customerWiseTradeProgCalDAL.GetCustomerWiseTradeProgramCalculation(fDate, tDate, dCode, rCode, aCode, tCode, cCode, tProgramNo);
            var data = Json(listData, JsonRequestBehavior.AllowGet);
            data.MaxJsonLength = int.MaxValue;
            return data;
        }

    }
}