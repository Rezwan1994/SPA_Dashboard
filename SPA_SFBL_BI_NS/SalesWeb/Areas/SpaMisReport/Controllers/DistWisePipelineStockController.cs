using System;
using System.Web.Mvc;
using SalesWeb.Areas.SpaMisReport.Models.BEL;
using SalesWeb.Areas.SpaMisReport.Models.DAL;
using SalesWeb.Universal.Gateway;


namespace SalesWeb.Areas.SpaMisReport.Controllers
{
    [LogInChecker]
    public class DistWisePipelineStockController : Controller
    {

        DistWisePipelineStockDAL _distWisePipelineStockDAL = new DistWisePipelineStockDAL();

        // GET: SpaMisReport/DistWisePipelineStock
        public ActionResult frmDistWisePipelineStock()
        {
            return View();
        }



        [HttpPost]
        public ActionResult GetDistWisePipelineStock(string dCode, string rCode, string aCode, string tCode, string cCode)
        {
            var listData = _distWisePipelineStockDAL.GetDistWisePipelineStock(dCode, rCode, aCode, tCode, cCode);
            var data = Json(listData, JsonRequestBehavior.AllowGet);
            data.MaxJsonLength = int.MaxValue;
            return data;

        }


    }
}