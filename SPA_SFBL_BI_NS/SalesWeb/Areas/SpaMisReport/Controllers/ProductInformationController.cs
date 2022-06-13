using System;
using System.Web.Mvc;
using SalesWeb.Areas.SpaMisReport.Models.BEL;
using SalesWeb.Areas.SpaMisReport.Models.DAL;
using SalesWeb.Universal.Gateway;

namespace SalesWeb.Areas.SpaMisReport.Controllers
{
    [LogInChecker]
    public class ProductInformationController : Controller
    {
        ProductInformationDAL _productInformationDAL = new ProductInformationDAL();
        public ActionResult frmProductInformation()
        {
            return View();
        }

        [HttpPost]
        public ActionResult GetProductInformationList(string base_product_code, string brand_code, string product_category, string status)
        {
            var listData = _productInformationDAL.GetProductInformationList(base_product_code, brand_code, product_category, status);
            var data = Json(listData, JsonRequestBehavior.AllowGet);
            data.MaxJsonLength = int.MaxValue;
            return data;
        }


        [HttpPost]
        public ActionResult GetDeadProductInformationList(string LastInvoiceDate, string base_product_code, string brand_code, string product_category, string status)
        {
            var listData = _productInformationDAL.GetDeadProductInformationList(LastInvoiceDate,base_product_code, brand_code, product_category, status);
            var data = Json(listData, JsonRequestBehavior.AllowGet);
            data.MaxJsonLength = int.MaxValue;
            return data;
        }




        [HttpGet]
        public ActionResult GetBaseProductList()
        {
            try
            {
                var data = _productInformationDAL.GetBaseProductList();
                return Json(new { Status = _productInformationDAL.ExceptionReturn, Data = data }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception e)
            {
                return Json(new { Status = e.Message, Data = " " }, JsonRequestBehavior.AllowGet);
            }
        }

        [HttpGet]
        public ActionResult GetBrandList()
        {
            try
            {
                var data = _productInformationDAL.GetBrandList();
                return Json(new { Status = _productInformationDAL.ExceptionReturn, Data = data }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception e)
            {
                return Json(new { Status = e.Message, Data = " " }, JsonRequestBehavior.AllowGet);
            }
        }


        [HttpGet]
        public ActionResult GetProductCategoryList()
        {
            try
            {
                var data = _productInformationDAL.GetProductCategoryList();
                return Json(new { Status = _productInformationDAL.ExceptionReturn, Data = data }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception e)
            {
                return Json(new { Status = e.Message, Data = " " }, JsonRequestBehavior.AllowGet);
            }
        }









    }
}