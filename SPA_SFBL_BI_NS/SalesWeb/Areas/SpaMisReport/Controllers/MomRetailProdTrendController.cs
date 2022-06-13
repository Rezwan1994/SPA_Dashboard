using System;
using System.Web.Mvc;
using SalesWeb.Areas.SpaMisReport.Models.BEL;
using SalesWeb.Areas.SpaMisReport.Models.DAL;
using SalesWeb.Universal.Gateway;

namespace SalesWeb.Areas.SpaMisReport.Controllers
{
    [LogInChecker]
    public class MomRetailProdTrendController : Controller
    {
        MomRetailProdTrendDAL _momRetailProdTrendDAL = new MomRetailProdTrendDAL();
        public ActionResult frmMomRetailProdTrend()
        {
            return View();
        }

        [HttpGet]
        public ActionResult GetProductList(string bCode, string bpCode, string pcCode)
        {
            try
            {
                var data = _momRetailProdTrendDAL.GetProductList( bCode,  bpCode,  pcCode);
                return Json(new { Status = _momRetailProdTrendDAL.ExceptionReturn, Data = data }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception e)
            {
                return Json(new { Status = e.Message, Data = " " }, JsonRequestBehavior.AllowGet);
            }
        }

        [HttpPost]
        public ActionResult GetMomRetailProdTrend(string dCode, string rCode, string aCode, string tCode, string cCode, string bCode, string bpCode, string pcCode, string pCode)
        {
            var listData = _momRetailProdTrendDAL.GetMomRetailProdTrend(dCode, rCode, aCode, tCode, cCode, bCode, bpCode, pcCode,pCode);
            var data = Json(listData, JsonRequestBehavior.AllowGet);
            data.MaxJsonLength = int.MaxValue;
            return data;

        }
        [HttpPost]
        public ActionResult GetMomRetailProdTrendLy(string dCode, string rCode, string aCode, string tCode, string cCode, string bCode, string bpCode, string pcCode, string pCode)
        {
            var listData = _momRetailProdTrendDAL.GetMomRetailProdTrendLy(dCode, rCode, aCode, tCode, cCode, bCode, bpCode, pcCode, pCode);
            var data = Json(listData, JsonRequestBehavior.AllowGet);
            data.MaxJsonLength = int.MaxValue;
            return data;

        }

    }
}