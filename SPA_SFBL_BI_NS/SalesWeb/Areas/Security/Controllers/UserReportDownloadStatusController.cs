using System;
using System.Web.Mvc;
using SalesWeb.Areas.Security.Models.BEL;
using SalesWeb.Areas.Security.Models.DAL;
using SalesWeb.Universal.Gateway;

namespace SalesWeb.Areas.Security.Controllers
{
    [LogInChecker]
    public class UserReportDownloadStatusController : Controller
    {
        UserReportDownloadStatusDAL _userReportDownloadStatusDAL = new UserReportDownloadStatusDAL();
        public ActionResult frmUserReportDownloadStatus()
        {
            return View();
        }
        [HttpPost]
        public ActionResult GetUserReportList(string userId)
        {
            var listData = _userReportDownloadStatusDAL.GetUserReportList(userId);
            var data = Json(listData, JsonRequestBehavior.AllowGet);
            data.MaxJsonLength = int.MaxValue;
            return data;

        }


        [HttpPost]
        public ActionResult InsertUpdateReportDownloadStatus(string pUserId, string pMenuId, string pReportName, string pDownloadStatus)
        {

            return _userReportDownloadStatusDAL.InsertUpdateReportDownloadStatus(pUserId, pMenuId, pReportName, pDownloadStatus) ? Json(new { Message = _userReportDownloadStatusDAL.StatusChangeMessage }) : Json(new { Status = _userReportDownloadStatusDAL.ExceptionReturn });

        }
    }
}