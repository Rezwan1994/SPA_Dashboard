using System;
using System.Web.Mvc;
using SalesWeb.Areas.SpaMisReport.Models.BEL;
using SalesWeb.Areas.SpaMisReport.Models.DAL;
using SalesWeb.Universal.Gateway;

namespace SalesWeb.Areas.SpaMisReport.Controllers
{
    [LogInChecker]
    public class TerritoryProductImsController : Controller
    {
        TerritoryProductImsDAL _territoryProductImsDAL = new TerritoryProductImsDAL();
        public ActionResult frmTerritoryProductIms()
        {
            return View();
        }


        [HttpPost]
        public ActionResult GetTerritoryProductImsDateRange(string fDate, string tDate, string dCode, string aCode, string tCode)
        {
            var listData = _territoryProductImsDAL.GetTerritoryProductImsDateRange(fDate, tDate, dCode, aCode, tCode);
            var data = Json(listData, JsonRequestBehavior.AllowGet);
            data.MaxJsonLength = int.MaxValue;
            return data;

        }

    }
}