using System;
using System.Web.Mvc;
using SalesWeb.Areas.SpaMisReport.Models.BEL;
using SalesWeb.Areas.SpaMisReport.Models.DAL;
using SalesWeb.Universal.Gateway;

namespace SalesWeb.Areas.SpaMisReport.Controllers
{
    [LogInChecker]
    public class DayWiseProductImsController : Controller
    {
        DayWiseProductImsDAL _DayWiseProductImsDAL = new DayWiseProductImsDAL();
        public ActionResult frmDayWiseProductIms()
        {
            return View();
        }


        [HttpPost]
        public ActionResult GetDayWiseProductIms(string fDate, string tDate, string pCode, string dCode, string rCode, string aCode, string tCode, string cCode)
        {
            var listData = _DayWiseProductImsDAL.GetDayWiseProductIms(fDate, tDate, pCode,dCode, rCode, aCode, tCode, cCode);
            var data = Json(listData, JsonRequestBehavior.AllowGet);
            data.MaxJsonLength = int.MaxValue;
            return data;
        }

    }
}