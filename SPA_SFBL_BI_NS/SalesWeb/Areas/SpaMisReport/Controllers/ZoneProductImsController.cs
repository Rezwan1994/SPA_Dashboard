using System;
using System.Web.Mvc;
using SalesWeb.Areas.SpaMisReport.Models.BEL;
using SalesWeb.Areas.SpaMisReport.Models.DAL;
using SalesWeb.Universal.Gateway;

namespace SalesWeb.Areas.SpaMisReport.Controllers
{
    [LogInChecker]
    public class ZoneProductImsController : Controller
    {
        ZoneProductImsDAL _zoneProductImsDALL = new ZoneProductImsDAL();
        public ActionResult frmZoneProductIms()
        {
            return View();
        }

        [HttpPost]
        public ActionResult GetZoneProductImsDateRange(string fDate, string tDate, string dCode)
        {
            var listData = _zoneProductImsDALL.GetZoneProductImsDateRange(fDate, tDate, dCode);
            var data = Json(listData, JsonRequestBehavior.AllowGet);
            data.MaxJsonLength = int.MaxValue;
            return data;

        }

    }
}