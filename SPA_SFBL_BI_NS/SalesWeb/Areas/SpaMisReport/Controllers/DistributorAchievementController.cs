using System;
using System.Web.Mvc;
using SalesWeb.Areas.SpaMisReport.Models.BEL;
using SalesWeb.Areas.SpaMisReport.Models.DAL;
using SalesWeb.Universal.Gateway;

namespace SalesWeb.Areas.SpaMisReport.Controllers
{
    [LogInChecker]
    public class DistributorAchievementController : Controller
    {
        DistributorAchievementDAL _DistributorAchievementDAL = new DistributorAchievementDAL();
        public ActionResult frmDistributorAchievement()
        {
            return View();
        }


        [HttpPost]
        public ActionResult GetDistributorAchievementCurrentMonth(string dCode, string rCode, string aCode, string tCode, string cCode)
        {
            var listData = _DistributorAchievementDAL.GetDistributorAchievementCurrentMonth(dCode, rCode, aCode, tCode, cCode);
            var data = Json(listData, JsonRequestBehavior.AllowGet);
            data.MaxJsonLength = int.MaxValue;
            return data;

        }

        [HttpPost]
        public ActionResult GetDistributorAchievementLastMonth(string dCode, string rCode, string aCode, string tCode, string cCode)
        {
            var listData = _DistributorAchievementDAL.GetDistributorAchievementLastMonth(dCode, rCode, aCode, tCode, cCode);
            var data = Json(listData, JsonRequestBehavior.AllowGet);
            data.MaxJsonLength = int.MaxValue;
            return data;

        }

    }
}