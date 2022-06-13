using System;
using System.Web.Mvc;
using SalesWeb.Areas.SpaMisReport.Models.BEL;
using SalesWeb.Areas.SpaMisReport.Models.DAL;
using SalesWeb.Universal.Gateway;

namespace SalesWeb.Areas.SpaMisReport.Controllers
{
    [LogInChecker]
    public class DistWiseBonusAdjustmentController : Controller
    {
        DistWiseBonusAdjustmentDAL _distWiseBonusAdjustmentDAL = new DistWiseBonusAdjustmentDAL();
        public ActionResult frmDistWiseBonusAdjustment()
        {
            return View();
        }




        [HttpPost]
        public ActionResult GetDistWiseBonusAdjustmentCMonth(string dCode, string rCode, string aCode, string tCode, string cCode)
        {
            var listData = _distWiseBonusAdjustmentDAL.GetDistWiseBonusAdjustmentCMonth(dCode, rCode, aCode, tCode, cCode);
            var data = Json(listData, JsonRequestBehavior.AllowGet);
            data.MaxJsonLength = int.MaxValue;
            return data;
        }


        [HttpPost]
        public ActionResult GetDistWiseBonusAdjustmentLMonth(string dCode, string rCode, string aCode, string tCode, string cCode)
        {
            var listData = _distWiseBonusAdjustmentDAL.GetDistWiseBonusAdjustmentLMonth(dCode, rCode, aCode, tCode, cCode);
            var data = Json(listData, JsonRequestBehavior.AllowGet);
            data.MaxJsonLength = int.MaxValue;
            return data;
        }


        [HttpPost]
        public ActionResult GetDistWiseBonusAdjustmentDt(string fDate, string tDate, string dCode, string rCode, string aCode, string tCode, string cCode)
        {
            var listData = _distWiseBonusAdjustmentDAL.GetDistWiseBonusAdjustmentDt(fDate, tDate,dCode, rCode, aCode, tCode, cCode);
            var data = Json(listData, JsonRequestBehavior.AllowGet);
            data.MaxJsonLength = int.MaxValue;
            return data;
        }

    }
}