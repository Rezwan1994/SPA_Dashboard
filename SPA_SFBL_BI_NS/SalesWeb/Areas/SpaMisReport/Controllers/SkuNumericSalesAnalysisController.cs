using System;
using System.Web.Mvc;
using SalesWeb.Areas.SpaMisReport.Models.BEL;
using SalesWeb.Areas.SpaMisReport.Models.DAL;
using SalesWeb.Universal.Gateway;

namespace SalesWeb.Areas.SpaMisReport.Controllers
{
    [LogInChecker]
    public class SkuNumericSalesAnalysisController : Controller
    {
        SkuNumericSalesAnalysisDAL _skuNumericSalesAnalysisDAL = new SkuNumericSalesAnalysisDAL();
        public ActionResult frmSkuNumericSalesAnalysis()
        {
            return View();
        }



        [HttpGet]
        public ActionResult GetProductCategoryList()
        {
            try
            {
                var data = _skuNumericSalesAnalysisDAL.GetProductCategoryList();
                return Json(new { Status = _skuNumericSalesAnalysisDAL.ExceptionReturn, Data = data }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception e)
            {
                return Json(new { Status = e.Message, Data = " " }, JsonRequestBehavior.AllowGet);
            }
        }


        [HttpPost]
        public ActionResult GetSkuNumericSalesAnalysisCMonth(string dCode, string rCode, string aCode, string tCode, string cCode, string pcCode)
        {
            var listData = _skuNumericSalesAnalysisDAL.GetSkuNumericSalesAnalysisCMonth(dCode, rCode, aCode, tCode, cCode, pcCode);
            var data = Json(listData, JsonRequestBehavior.AllowGet);
            data.MaxJsonLength = int.MaxValue;
            return data;
        }

        [HttpPost]
        public ActionResult GetSkuNumericSalesAnalysisLMonth(string dCode, string rCode, string aCode, string tCode, string cCode, string pcCode)
        {
            var listData = _skuNumericSalesAnalysisDAL.GetSkuNumericSalesAnalysisLMonth(dCode, rCode, aCode, tCode, cCode, pcCode);
            var data = Json(listData, JsonRequestBehavior.AllowGet);
            data.MaxJsonLength = int.MaxValue;
            return data;
        }

        [HttpPost]
        public ActionResult GetSkuNumericSalesAnalysisDateRange(string fromDate, string toDate, string dCode, string rCode, string aCode, string tCode, string cCode, string pcCode)
        {
            var listData = _skuNumericSalesAnalysisDAL.GetSkuNumericSalesAnalysisDateRange(fromDate, toDate, dCode, rCode, aCode, tCode, cCode, pcCode);
            var data = Json(listData, JsonRequestBehavior.AllowGet);
            data.MaxJsonLength = int.MaxValue;
            return data;
        }
    }
}