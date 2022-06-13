using System;
using System.Web.Mvc;
using SalesWeb.Areas.SpaMisReport.Models.BEL;
using SalesWeb.Areas.SpaMisReport.Models.DAL;
using SalesWeb.Universal.Gateway;

namespace SalesWeb.Areas.SpaMisReport.Controllers
{
    [LogInChecker]
    public class CatNumericSalesAnalysisController : Controller
    {
        CatNumericSalesAnalysisDAL _catNumericSalesAnalysisDAL = new CatNumericSalesAnalysisDAL();
        public ActionResult frmCatNumericSalesAnalysis()
        {
            return View();
        }



        [HttpGet]
        public ActionResult GetProductCategoryList()
        {
            try
            {
                var data = _catNumericSalesAnalysisDAL.GetProductCategoryList();
                return Json(new { Status = _catNumericSalesAnalysisDAL.ExceptionReturn, Data = data }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception e)
            {
                return Json(new { Status = e.Message, Data = " " }, JsonRequestBehavior.AllowGet);
            }
        }


        [HttpPost]
        public ActionResult GetCatNumericSalesAnalysisCMonth(string dCode, string rCode, string aCode, string tCode, string cCode, string pcCode)
        {
            var listData = _catNumericSalesAnalysisDAL.GetCatNumericSalesAnalysisCMonth(dCode, rCode, aCode, tCode, cCode, pcCode);
            var data = Json(listData, JsonRequestBehavior.AllowGet);
            data.MaxJsonLength = int.MaxValue;
            return data;
        }

        [HttpPost]
        public ActionResult GetCatNumericSalesAnalysisLMonth(string dCode, string rCode, string aCode, string tCode, string cCode, string pcCode)
        {
            var listData = _catNumericSalesAnalysisDAL.GetCatNumericSalesAnalysisLMonth(dCode, rCode, aCode, tCode, cCode, pcCode);
            var data = Json(listData, JsonRequestBehavior.AllowGet);
            data.MaxJsonLength = int.MaxValue;
            return data;
        }


    }
}