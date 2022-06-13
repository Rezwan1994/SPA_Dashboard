using System;
using System.Web.Mvc;
using SalesWeb.Areas.SpaMisReport.Models.BEL;
using SalesWeb.Areas.SpaMisReport.Models.DAL;
using SalesWeb.Universal.Gateway;

namespace SalesWeb.Areas.SpaMisReport.Controllers
{
    [LogInChecker]
    public class MomSkuWiseNumericDistributionController : Controller
    {
        MomSkuWiseNumericDistributionDAL _momSkuWiseNumericDistributionDAL = new MomSkuWiseNumericDistributionDAL();
        public ActionResult frmMomSkuWiseNumericDistribution()
        {
            return View();
        }



        [HttpPost]
        public ActionResult GetMomSKUWiseNumericDistribution(string dCode, string rCode, string aCode, string tCode, string cCode, string pCode)
        {
            var listData = _momSkuWiseNumericDistributionDAL.GetMomSKUWiseNumericDistribution(dCode, rCode, aCode, tCode, cCode, pCode);
            var data = Json(listData, JsonRequestBehavior.AllowGet);
            data.MaxJsonLength = int.MaxValue;
            return data;
        }



        [HttpPost]
        public ActionResult GetMomSKUWiseNumericDistributionLy(string dCode, string rCode, string aCode, string tCode, string cCode, string pCode)
        {
            var listData = _momSkuWiseNumericDistributionDAL.GetMomSKUWiseNumericDistributionLy(dCode, rCode, aCode, tCode, cCode, pCode);
            var data = Json(listData, JsonRequestBehavior.AllowGet);
            data.MaxJsonLength = int.MaxValue;
            return data;
        }

    }
}