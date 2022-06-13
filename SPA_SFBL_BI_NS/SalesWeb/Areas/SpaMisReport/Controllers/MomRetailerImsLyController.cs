using System;
using System.Web.Mvc;
using SalesWeb.Areas.SpaMisReport.Models.BEL;
using SalesWeb.Areas.SpaMisReport.Models.DAL;
using SalesWeb.Universal.Gateway;

namespace SalesWeb.Areas.SpaMisReport.Controllers
{
    [LogInChecker]
    public class MomRetailerImsLyController : Controller
    {
        MomRetailerImsLyDAL _momRetailerImsLyDAL = new MomRetailerImsLyDAL();
        public ActionResult frmMomRetailerImsLy()
        {
            return View();
        }

        [HttpPost]
        public ActionResult GetMomRetailerImsLy(string dCode, string rCode, string aCode, string tCode, string cCode)
        {
            var listData = _momRetailerImsLyDAL.GetMomRetailerImsLy(dCode, rCode, aCode, tCode, cCode);
            var data = Json(listData, JsonRequestBehavior.AllowGet);
            data.MaxJsonLength = int.MaxValue;
            return data;

        }
    }
}