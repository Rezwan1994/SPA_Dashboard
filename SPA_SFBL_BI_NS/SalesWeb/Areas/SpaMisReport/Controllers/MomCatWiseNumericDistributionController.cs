using System;
using System.Web.Mvc;
using SalesWeb.Areas.SpaMisReport.Models.BEL;
using SalesWeb.Areas.SpaMisReport.Models.DAL;
using SalesWeb.Universal.Gateway;

namespace SalesWeb.Areas.SpaMisReport.Controllers
{
    public class MomCatWiseNumericDistributionController : Controller
    {
        MomCatWiseNumericDistributionDAL _momCatWiseNumericDistributionDAL = new MomCatWiseNumericDistributionDAL();
        public ActionResult frmMomCatWiseNumericDistribution()
        {
            return View();
        }

        [HttpPost]
        public ActionResult GetMomCatWiseNumericDistribution(string dCode, string rCode, string aCode, string tCode, string cCode, string pcCode)
        {
            var listData = _momCatWiseNumericDistributionDAL.GetMomCatWiseNumericDistribution(dCode, rCode, aCode, tCode, cCode, pcCode);
            var data = Json(listData, JsonRequestBehavior.AllowGet);
            data.MaxJsonLength = int.MaxValue;
            return data;
        }

        [HttpPost]
        public ActionResult GetMomCatWiseNumericDistributionLy(string dCode, string rCode, string aCode, string tCode, string cCode, string pcCode)
        {
            var listData = _momCatWiseNumericDistributionDAL.GetMomCatWiseNumericDistributionLy(dCode, rCode, aCode, tCode, cCode, pcCode);
            var data = Json(listData, JsonRequestBehavior.AllowGet);
            data.MaxJsonLength = int.MaxValue;
            return data;
        }
    }
}