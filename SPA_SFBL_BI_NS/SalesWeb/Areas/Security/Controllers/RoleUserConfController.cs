using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SalesWeb.Areas.Security.Models.BEL;
using SalesWeb.Areas.Security.Models.DAL;
using SalesWeb.Universal.Gateway;

namespace SalesWeb.Areas.Security.Controllers
{
    [LogInChecker]
    public class RoleUserConfController : Controller
    {
        readonly RoleUserConfDAL _primaryDAL = new RoleUserConfDAL();
        public ActionResult frmRoleUserConf()
        {
            return View();
        }


        [HttpGet]
        public ActionResult GetRoleList()
        {
            var data = _primaryDAL.GetRoleList();
            if (data != null && _primaryDAL.ExceptionReturn == null)
            {
                return Json(data, JsonRequestBehavior.AllowGet);
            }
            else
                return Json(new { Status = _primaryDAL.ExceptionReturn });

        }

        [HttpPost]
        public ActionResult GetUserInfoList(string roleId)
        {
            var data = _primaryDAL.GetUserInfoList(roleId);
            if (data != null && _primaryDAL.ExceptionReturn == null)
            {
                return Json(data, JsonRequestBehavior.AllowGet);
            }
            else
                return Json(new { Status = _primaryDAL.ExceptionReturn });

        }
       
        [HttpPost]
        public ActionResult InsertRoleUserConf(RoleUserConfigureBEL master)
        {
            try
            {
                if (_primaryDAL.InsertRoleUserConf(master))
                {
                    return Json(new { Message = _primaryDAL.InsertMessage, Status = "Ok", ID = _primaryDAL.MaxID });
                }
                else
                {
                    return Json(new { Status = _primaryDAL.ExceptionReturn });
                }
            }
            catch (Exception e)
            {
                return Json(new { Status = e.Message });
            }
        }
        [HttpPost]
        public ActionResult UpdateRoleUserConf(RoleUserConfigureBEL master)
        {
            try
            {
                if (_primaryDAL.UpdateRoleUserConf(master))
                {
                    return Json(new { Message = _primaryDAL.UpdateMessage, Status = "Ok", ID = _primaryDAL.MaxID });
                }
                else
                {
                    return Json(new { Status = _primaryDAL.ExceptionReturn });
                }

            }
            catch (Exception e)
            {
                return Json(new { Status = e.Message });
            }
        }
        [HttpPost]
        public ActionResult GetRoleUserConfList(string param)
        {
            var data = _primaryDAL.GetRoleUserConfList(param);
            if (data != null && _primaryDAL.ExceptionReturn == null)
            {
                return Json(data, JsonRequestBehavior.AllowGet);
            }
            else
                return Json(new { Status = _primaryDAL.ExceptionReturn });

        }
        [HttpPost]
        public ActionResult DeletegridRUCRow(string Id)
        {
            if (_primaryDAL.DeletegridRUCRow(Id))
            {
                return Json(new { Message = _primaryDAL.DeleteMessage, Status = "Ok", ID = _primaryDAL.MaxID });
            }
            else
            {
                return Json(new { Status = _primaryDAL.ExceptionReturn });
            }

        }
    }
}