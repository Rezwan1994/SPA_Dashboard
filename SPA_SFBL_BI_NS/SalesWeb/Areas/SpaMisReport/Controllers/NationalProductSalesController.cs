using System;
using System.Web.Mvc;
using SalesWeb.Areas.SpaMisReport.Models.BEL;
using SalesWeb.Areas.SpaMisReport.Models.DAL;
using SalesWeb.Universal.Gateway;

namespace SalesWeb.Areas.SpaMisReport.Controllers
{
    [LogInChecker]
    public class NationalProductSalesController : Controller
    {
        NationalProductSalesDAL _nationalProductSalesDAL = new NationalProductSalesDAL();
        public ActionResult frmNationalProductSales()
        {
            return View();
        }

        public ActionResult GetNtlProductSalesCurMonth()
        {
            var listData = _nationalProductSalesDAL.GetNtlProductSalesCurMonth();
            var data = Json(listData, JsonRequestBehavior.AllowGet);
            data.MaxJsonLength = int.MaxValue;
            return data;

        }

        [HttpPost]
        public ActionResult GetNtlProductSalesDateRange(string fDate, string tDate)
        {
            var listData = _nationalProductSalesDAL.GetNtlProductSalesDateRange(fDate, tDate);
            var data = Json(listData, JsonRequestBehavior.AllowGet);
            data.MaxJsonLength = int.MaxValue;
            return data;
        }






    }
}




