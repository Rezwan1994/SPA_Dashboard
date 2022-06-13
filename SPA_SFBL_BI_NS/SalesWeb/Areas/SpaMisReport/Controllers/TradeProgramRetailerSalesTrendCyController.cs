using System;
using System.Web.Mvc;
using SalesWeb.Areas.SpaMisReport.Models.BEL;
using SalesWeb.Areas.SpaMisReport.Models.DAL;
using SalesWeb.Universal.Gateway;

namespace SalesWeb.Areas.SpaMisReport.Controllers
{
    [LogInChecker]
    public class TradeProgramRetailerSalesTrendCyController : Controller
    {
        TradeProgramRetailerSalesTrendCyDAL _tradeProgramRetailerSalesTrendDAL = new TradeProgramRetailerSalesTrendCyDAL();
        public ActionResult frmTradeProgramRetailerSalesTrendCy()
        {
            return View();
        }

        [HttpPost]
        public ActionResult GetTradeProgramRetailerSalesTrendCy(string dCode, string rCode, string aCode, string tCode, string cCode, string tradeNo, string eType)
        {
            var listData = _tradeProgramRetailerSalesTrendDAL.GetTradeProgramRetailerSalesTrendCy(dCode, rCode, aCode, tCode, cCode, tradeNo, eType);
            var data = Json(listData, JsonRequestBehavior.AllowGet);
            data.MaxJsonLength = int.MaxValue;
            return data;
        }
        [HttpPost]
        public ActionResult GetTradeProgramRetailerSalesTrendLy(string dCode, string rCode, string aCode, string tCode, string cCode, string tradeNo, string eType)
        {
            var listData = _tradeProgramRetailerSalesTrendDAL.GetTradeProgramRetailerSalesTrendLy(dCode, rCode, aCode, tCode, cCode, tradeNo, eType);
            var data = Json(listData, JsonRequestBehavior.AllowGet);
            data.MaxJsonLength = int.MaxValue;
            return data;
        }
    }
}