using System;
using System.Web.Mvc;
using SalesWeb.Areas.SpaMisReport.Models.BEL;
using SalesWeb.Areas.SpaMisReport.Models.DAL;
using SalesWeb.Universal.Gateway;

namespace SalesWeb.Areas.SpaMisReport.Controllers
{
    [LogInChecker]
    public class MomRetailerImsCyController : Controller
    {
        MomRetailerImsCyDAL _momRetailerImsCyDAL = new MomRetailerImsCyDAL();
        public ActionResult frmMomRetailerImsCy()
        {
            return View();
        }


        [HttpPost]
        public ActionResult GetMomRetailerImsCy(string dCode, string rCode, string aCode, string tCode, string cCode)
        {
            var listData = _momRetailerImsCyDAL.GetMomRetailerImsCy(dCode, rCode, aCode, tCode, cCode);
            var data = Json(listData, JsonRequestBehavior.AllowGet);
            data.MaxJsonLength = int.MaxValue;
            return data;

        }

    }
}