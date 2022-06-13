using System;
using System.Web.Mvc;
using SalesWeb.Areas.SpaMisReport.Models.BEL;
using SalesWeb.Areas.SpaMisReport.Models.DAL;
using SalesWeb.Universal.Gateway;

namespace SalesWeb.Areas.SpaMisReport.Controllers
{
    [LogInChecker]
    public class AreaProductImsController : Controller
    {
        AreaProductImsDAL _areaProductImsDAL = new AreaProductImsDAL();
        public ActionResult frmAreaProductIms()
        {
            return View();
        }

        [HttpPost]
        public ActionResult GetAreaProductImsDateRange(string fDate, string tDate, string dCode, string aCode)
        {
            var listData = _areaProductImsDAL.GetAreaProductImsDateRange(fDate, tDate, dCode, aCode);
            var data = Json(listData, JsonRequestBehavior.AllowGet);
            data.MaxJsonLength = int.MaxValue;
            return data;

        }

    }
}