using System;
using System.Web.Mvc;
using SalesWeb.Areas.SpaMisReport.Models.BEL;
using SalesWeb.Areas.SpaMisReport.Models.DAL;
using SalesWeb.Universal.Gateway;

namespace SalesWeb.Areas.SpaMisReport.Controllers
{
    [LogInChecker]
    public class MomProdWiseNtlNuDistController : Controller
    {
        MomProdWiseNtlNuDistDAL _momProdWiseNtlNuDistDAL = new MomProdWiseNtlNuDistDAL();
        public ActionResult frmMomProdWiseNtlNuDist()
        {
            return View();
        }

        [HttpGet]
        public ActionResult GetBaseProductList()
        {
            try
            {
                var data = _momProdWiseNtlNuDistDAL.GetBaseProductList();
                return Json(new { Status = _momProdWiseNtlNuDistDAL.ExceptionReturn, Data = data }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception e)
            {
                return Json(new { Status = e.Message, Data = " " }, JsonRequestBehavior.AllowGet);
            }
        }


        [HttpPost]
        public ActionResult GetMomProdWiseNtlNuDist(string BaseProductCode)
        {
            var listData = _momProdWiseNtlNuDistDAL.GetMomProdWiseNtlNuDist(BaseProductCode);
            var data = Json(listData, JsonRequestBehavior.AllowGet);
            data.MaxJsonLength = int.MaxValue;
            return data;
        }
        [HttpPost]
        public ActionResult GetMomProdWiseNtlNuDistLy(string BaseProductCode)
        {
            var listData = _momProdWiseNtlNuDistDAL.GetMomProdWiseNtlNuDistLy(BaseProductCode);
            var data = Json(listData, JsonRequestBehavior.AllowGet);
            data.MaxJsonLength = int.MaxValue;
            return data;
        }

    }
}