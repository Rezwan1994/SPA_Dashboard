using System;
using System.Web.Mvc;
using SalesWeb.Areas.SpaMisReport.Models.BEL;
using SalesWeb.Areas.SpaMisReport.Models.DAL;
using SalesWeb.Universal.Gateway;

namespace SalesWeb.Areas.SpaMisReport.Controllers
{
    [LogInChecker]
    public class DistReplaceStkConsumptionController : Controller
    {

        DistReplaceStkConsumptionDAL _distReplaceStkConsumptionDAL = new DistReplaceStkConsumptionDAL();
        

        // GET: SpaMisReport/DistReplaceStkConsumption
        public ActionResult frmDistReplaceStkConsumption()
        {
            return View();
        }


        public ActionResult GetDistReplaceStkConsumption(string dCode, string rCode, string aCode, string tCode, string cCode)
        {
            var listData = _distReplaceStkConsumptionDAL.GetDistReplaceStkConsumption(dCode, rCode, aCode, tCode, cCode);
            var data = Json(listData, JsonRequestBehavior.AllowGet);
            data.MaxJsonLength = int.MaxValue;
            return data;

        }


    }
}