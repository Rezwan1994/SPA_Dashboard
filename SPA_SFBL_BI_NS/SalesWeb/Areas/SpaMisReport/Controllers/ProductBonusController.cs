using System;
using System.Web.Mvc;
using SalesWeb.Areas.SpaMisReport.Models.BEL;
using SalesWeb.Areas.SpaMisReport.Models.DAL;
using SalesWeb.Universal.Gateway;

namespace SalesWeb.Areas.SpaMisReport.Controllers
{
    [LogInChecker]
    public class ProductBonusController : Controller
    {
        ProductBonusDAL _productBonusDAL = new ProductBonusDAL();
        public ActionResult frmProductBonus()
        {
            return View();
        }

        [HttpGet]
        public ActionResult GetProductList()
        {
            try
            {
                var data = _productBonusDAL.GetProductList();
                return Json(new { Status = _productBonusDAL.ExceptionReturn, Data = data }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception e)
            {
                return Json(new { Status = e.Message, Data = " " }, JsonRequestBehavior.AllowGet);
            }
        }

        [HttpPost]
        public ActionResult GetProductBonus(string FromDate, string ToDate, string ProductCode)
        {
            var listData = _productBonusDAL.GetProductBonus(FromDate, ToDate, ProductCode);
            var data = Json(listData, JsonRequestBehavior.AllowGet);
            data.MaxJsonLength = int.MaxValue;
            return data;
        }
    }
}