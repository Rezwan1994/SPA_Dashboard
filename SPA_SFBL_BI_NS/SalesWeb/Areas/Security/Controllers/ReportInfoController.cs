using System;
using System.Web.Mvc;
using SalesWeb.Areas.Security.Models.BEL;
using SalesWeb.Areas.Security.Models.DAL;
using SalesWeb.Universal.Gateway;

namespace SalesWeb.Areas.Security.Controllers
{
    [LogInChecker]
    public class ReportInfoController : Controller
    {
        private readonly ReportInfoDAL _reportInfoDAL =new ReportInfoDAL();
        public ActionResult frmReportInfo()
        {
            return View();
        }
        [HttpGet]
        public ActionResult GetReportList()
        {
            var data = _reportInfoDAL.GetReportList();
            return Json(data, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public ActionResult GetFormNameList()
        {
            var data = _reportInfoDAL.GetFormNameList();
            return Json(data, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public ActionResult InsertReportInfo(ReportInfoBEL master)
        {
            try
            {
                return _reportInfoDAL.InsertReportInfo(master) ? Json(new { Message = _reportInfoDAL.InsertMessage, Status = "Ok", Id = _reportInfoDAL.MaxID }) : Json(new { Status = _reportInfoDAL.ExceptionReturn });
            }
            catch (Exception e)
            {
                return Json(new { Status = e.Message });
            }
        }
        [HttpPost]
        public ActionResult UpdateReportInfo(ReportInfoBEL master)
        {
            try
            {
                return _reportInfoDAL.UpdateReportInfo(master) ? Json(new { Message = _reportInfoDAL.UpdateMessage, Status = "Ok", Id = _reportInfoDAL.MaxID }) : Json(new { Status = _reportInfoDAL.ExceptionReturn });
            }
            catch (Exception e)
            {
                return Json(new { Status = e.Message });
            }
        }

    }
}