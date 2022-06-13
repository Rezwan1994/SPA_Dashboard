using System;
using System.Web.Mvc;
using SalesWeb.Areas.Security.Models.BEL;
using SalesWeb.Areas.Security.Models.DAL;
using SalesWeb.Universal.Gateway;

namespace SalesWeb.Areas.Security.Controllers
{
    [LogInChecker]
    public class ReportViewTrackingController : Controller
    {
        ReportViewTrackingDAL _reportViewTrackingDAL = new ReportViewTrackingDAL();
        public ActionResult frmReportViewTracking()
        {
            return View();
        }

        [HttpGet]
        public ActionResult GetUserList()
        {
            try
            {
                var data = _reportViewTrackingDAL.GetUserList();
                return Json(new { Status = _reportViewTrackingDAL.ExceptionReturn, Data = data }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception e)
            {
                return Json(new { Status = e.Message, Data = " " }, JsonRequestBehavior.AllowGet);
            }
        }

        [HttpPost]
        public ActionResult GetUserViewReportList(string userId, string fDate, string tDate)
        {
            var listData = _reportViewTrackingDAL.GetUserViewReportList(userId,fDate,tDate);
            var data = Json(listData, JsonRequestBehavior.AllowGet);
            data.MaxJsonLength = int.MaxValue;
            return data;

        }

    }
}