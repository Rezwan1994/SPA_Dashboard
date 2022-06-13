using System;
using System.Web.Mvc;
using SalesWeb.Areas.SpaMisReport.Models.BEL;
using SalesWeb.Areas.SpaMisReport.Models.DAL;
using SalesWeb.Universal.Gateway;

namespace SalesWeb.Areas.SpaMisReport.Controllers
{
    [LogInChecker]
    public class MomSkuWiseNtlNumericDistributionController : Controller
    {
        MomSkuWiseNtlNumericDistributionDAL _momSkuWiseNtlNumericDistributionDAL = new MomSkuWiseNtlNumericDistributionDAL();
        public ActionResult frmMomSkuWiseNtlNumericDistribution()
        {
            return View();
        }


        [HttpPost]
        public ActionResult GetMomSkuWiseNtlNumericDistribution(string pCode)
        {
            var listData = _momSkuWiseNtlNumericDistributionDAL.GetMomSkuWiseNtlNumericDistribution(pCode);
            var data = Json(listData, JsonRequestBehavior.AllowGet);
            data.MaxJsonLength = int.MaxValue;
            return data;
        }


        [HttpPost]
        public ActionResult GetMomSkuWiseNtlNumericDistributionLy(string pCode)
        {
            var listData = _momSkuWiseNtlNumericDistributionDAL.GetMomSkuWiseNtlNumericDistributionLy(pCode);
            var data = Json(listData, JsonRequestBehavior.AllowGet);
            data.MaxJsonLength = int.MaxValue;
            return data;
        }

    }
}