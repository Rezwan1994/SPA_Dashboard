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
    public class BonusReportController : Controller
    {
        BonusReportDAL _bonusReportDALL = new BonusReportDAL();
        public ActionResult frmBonusReport()
        {
            return View();
        }

        [HttpPost]
        public ActionResult GetBonusReportCustomDate(string fDate, string tDate, string dCode, string rCode, string aCode, string tCode, string cCode)
        {
            var listData = _bonusReportDALL.GetBonusReportCustomDate(fDate, tDate, dCode, rCode, aCode, tCode, cCode);
            var data = Json(listData, JsonRequestBehavior.AllowGet);
            data.MaxJsonLength = int.MaxValue;
            return data;
        }


    }
}