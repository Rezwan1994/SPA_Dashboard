using System;
using System.Web.Mvc;
using SalesWeb.Areas.Security.Models.BEL;
using SalesWeb.Areas.Security.Models.DAL;
using SalesWeb.Universal.Gateway;

namespace SalesWeb.Areas.Security.Controllers
{
    [LogInChecker]
    public class RoleMenuConfController : Controller
    {
        readonly RoleMenuConfDAL _primaryDAL=new RoleMenuConfDAL();
        public ActionResult frmRoleMenuConf()
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
        public ActionResult GetChildMenuList(string roleId)
        {
            var data = _primaryDAL.GetChildMenuList(roleId);
            if (data != null && _primaryDAL.ExceptionReturn == null)
            {
                return Json(data, JsonRequestBehavior.AllowGet);
            }
            else
                return Json(new { Status = _primaryDAL.ExceptionReturn });

        }
        [HttpPost]
        public ActionResult IsParentMenuMapped(string roleId,string parentId)
        {
            
            if (_primaryDAL.IsParentMenuMapped(roleId, parentId))
            {
                return Json(new { isValid = "Yes", Status = _primaryDAL.ExceptionReturn });
            }
            else
                return Json(new { isValid = "No",Status=_primaryDAL.ExceptionReturn});

        }
        [HttpPost]
        public ActionResult InsertRoleMenuConf(RoleMenuConfigureBEL master)
        {
            try
            {
                if (_primaryDAL.InsertRoleMenuConf(master))
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
        public ActionResult UpdateRoleMenuConf(RoleMenuConfigureBEL master)
        {
            try
            {
                if (_primaryDAL.UpdateRoleMenuConf(master))
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
        public ActionResult GetRoleMenuConfList(string param)
        {
            var data = _primaryDAL.GetRoleMenuConfList(param);
            if (data != null && _primaryDAL.ExceptionReturn == null)
            {
                return Json(data, JsonRequestBehavior.AllowGet);
            }
            else
                return Json(new { Status = _primaryDAL.ExceptionReturn });

        }
        [HttpPost]
        public ActionResult DeleteGridRMCRow(string Id)
        {
            if (_primaryDAL.DeleteGridRMCRow(Id))
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