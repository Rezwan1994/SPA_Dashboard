using System;
using System.Web.Mvc;
using SalesWeb.Areas.Security.Models.BEL;
using SalesWeb.Areas.Security.Models.DAL;
using SalesWeb.Universal.Gateway;

namespace SalesWeb.Areas.Security.Controllers
{
    [LogInChecker]
    public class RoleReportConfController : Controller
    {
        private readonly RoleReportConfDAL _roleReportConfDAL=new RoleReportConfDAL();
        public ActionResult frmRoleReportConf()
        {
            return View();
        }


        [HttpPost]
        public ActionResult InsertRoleReportConf(ReportConfigureBEL master)
        {
            try
            {
                if (_roleReportConfDAL.InsertRoleReportConf(master))
                {
                    return Json(new { Message = _roleReportConfDAL.InsertMessage, Status = "Ok", ID = _roleReportConfDAL.MaxID });
                }
                else
                {
                    return Json(new { Status = _roleReportConfDAL.ExceptionReturn });
                }
            }
            catch (Exception e)
            {
                return Json(new { Status = e.Message });
            }
        }
        [HttpPost]
        public ActionResult UpdateRoleReportConf(ReportConfigureBEL master)
        {
            try
            {
                if (_roleReportConfDAL.UpdateRoleReportConf(master))
                {
                    return Json(new { Message = _roleReportConfDAL.UpdateMessage, Status = "Ok", ID = _roleReportConfDAL.MaxID });
                }
                else
                {
                    return Json(new { Status = _roleReportConfDAL.ExceptionReturn });
                }

            }
            catch (Exception e)
            {
                return Json(new { Status = e.Message });
            }
        }
        [HttpPost]
        public ActionResult GetReportListByRole(string roleId)
        {
            var data = _roleReportConfDAL.GetReportListByRole(roleId);
            if (data != null && _roleReportConfDAL.ExceptionReturn == null)
            {
                return Json(data, JsonRequestBehavior.AllowGet);
            }
            else
                return Json(new { Status = _roleReportConfDAL.ExceptionReturn });

        }
        [HttpPost]
        public ActionResult GetRoleReportConfList(string param)
        {
            var data = _roleReportConfDAL.GetRoleReportConfList(param);
            if (data != null && _roleReportConfDAL.ExceptionReturn == null)
            {
                return Json(data, JsonRequestBehavior.AllowGet);
            }
            else
                return Json(new { Status = _roleReportConfDAL.ExceptionReturn });

        }
        [HttpPost]
        public ActionResult DeleteGridRMCRow(string Id)
        {
            if (_roleReportConfDAL.DeleteGridRMCRow(Id))
            {
                return Json(new { Message = _roleReportConfDAL.DeleteMessage, Status = "Ok", ID = _roleReportConfDAL.MaxID });
            }
            else
            {
                return Json(new { Status = _roleReportConfDAL.ExceptionReturn });
            }

        }
    }
}