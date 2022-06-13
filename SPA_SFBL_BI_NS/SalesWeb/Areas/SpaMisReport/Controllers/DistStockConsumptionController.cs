using System;
using System.Web.Mvc;
using SalesWeb.Areas.SpaMisReport.Models.BEL;
using SalesWeb.Areas.SpaMisReport.Models.DAL;
using SalesWeb.Universal.Gateway;


namespace SalesWeb.Areas.SpaMisReport.Controllers
{
    [LogInChecker]
    public class DistStockConsumptionController : Controller
    {

        DistStockConsumptionDAL _distStockConsumptionDAL = new DistStockConsumptionDAL();

        // GET: SpaMisReport/DistStockConsumption
        public ActionResult frmDistStockConsumption()
        {
            return View();
        }



        [HttpPost]
        public ActionResult GetDistStockConsumption(string dCode, string rCode, string aCode, string tCode, string cCode)
        {
            var listData = _distStockConsumptionDAL.GetDistStockConsumption(dCode, rCode, aCode, tCode, cCode);
            var data = Json(listData, JsonRequestBehavior.AllowGet);
            data.MaxJsonLength = int.MaxValue;
            return data;

        }


    }
}