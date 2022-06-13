using System;
using System.Web.Mvc;
using SalesWeb.Areas.Security.Models.BEL;
using SalesWeb.Areas.Security.Models.DAL;
using SalesWeb.Universal.Gateway;

namespace SalesWeb.Areas.Security.Controllers
{
    [LogInChecker]
    public class RoleInfoController : Controller
    {
        private readonly RoleInfoDAL _roleInfoDAL=new RoleInfoDAL();
        public ActionResult frmRoleInfo()
        {
            return View();
        }

        [HttpGet]
        public ActionResult GetRoleList()
        {
            var data = _roleInfoDAL.GetRoleList();
            if (data != null && _roleInfoDAL.ExceptionReturn == null)
            {
                return Json(data, JsonRequestBehavior.AllowGet);
            }
            else
                return Json(new { Status = _roleInfoDAL.ExceptionReturn });

        }

        [HttpPost]
        public ActionResult InsertRoleInfo(RoleInfo master)
        {
            try
            {
                if (_roleInfoDAL.InsertRoleInfo(master))
                {
                    return Json(new {Message = _roleInfoDAL.InsertMessage, Status = "Ok", ID = _roleInfoDAL.MaxID });
                }
                else
                {
                    return Json(new { Status = _roleInfoDAL.ExceptionReturn });
                }
            }
            catch (Exception e)
            {
                return Json(new { Status = e.Message });
            }
        }
        [HttpPost]
        public ActionResult UpdateRoleInfo(RoleInfo master)
        {
            try
            {
                if (_roleInfoDAL.UpdateRoleInfo(master))
                {
                    return Json(new { Message = _roleInfoDAL.UpdateMessage, Status = "Ok", ID = _roleInfoDAL.MaxID });
                }
                else
                {
                    return Json(new { Status = _roleInfoDAL.ExceptionReturn });
                }

            }
            catch (Exception e)
            {
                return Json(new { Status = e.Message });
            }
        }
    }
}