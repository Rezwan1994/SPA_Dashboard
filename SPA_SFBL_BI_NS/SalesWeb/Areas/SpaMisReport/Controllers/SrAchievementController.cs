using System;
using System.Web.Mvc;
using SalesWeb.Areas.SpaMisReport.Models.BEL;
using SalesWeb.Areas.SpaMisReport.Models.DAL;
using SalesWeb.Universal.Gateway;


namespace SalesWeb.Areas.SpaMisReport.Controllers
{
    [LogInChecker]
    public class SrAchievementController : Controller
    {
        SrAchievementDAL _srAchievementDAL = new SrAchievementDAL();
        public ActionResult frmSrAchievement()
        {
            return View();
        }


        [HttpPost]
        public ActionResult GetSrAchievementCurrentMonth(string dCode, string rCode, string aCode, string tCode, string cCode, string sCode)
        {
            var listData = _srAchievementDAL.GetSrAchievementCurrentMonth(dCode, rCode, aCode, tCode, cCode, sCode);
            var data = Json(listData, JsonRequestBehavior.AllowGet);
            data.MaxJsonLength = int.MaxValue;
            return data;

        }

        [HttpPost]
        public ActionResult GetSrAchievementLastMonth(string dCode, string rCode, string aCode, string tCode, string cCode, string sCode)
        {
            var listData = _srAchievementDAL.GetSrAchievementLastMonth(dCode, rCode, aCode, tCode, cCode, sCode);
            var data = Json(listData, JsonRequestBehavior.AllowGet);
            data.MaxJsonLength = int.MaxValue;
            return data;

        }

    }
}