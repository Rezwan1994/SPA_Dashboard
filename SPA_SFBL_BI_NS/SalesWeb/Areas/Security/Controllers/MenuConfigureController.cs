using System;
using System.Web.Mvc;
using SalesWeb.Areas.Security.Models.BEL;
using SalesWeb.Areas.Security.Models.DAL;
using SalesWeb.Universal.Gateway;

namespace SalesWeb.Areas.Security.Controllers
{
    [LogInChecker]
    public class MenuConfigureController : Controller
    {
        private readonly MenuConfigureDAL _menuConfigureDAL = new MenuConfigureDAL();
        public ActionResult frmMenuConfigure()
        {
            return View();
        }
        [HttpGet]
        public ActionResult GetParentMenuList()
        {
            var data = _menuConfigureDAL.GetParentMenuList();
            return Json(data, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public ActionResult GetChildMenuList(int parentMenuId)
        {
            var data = _menuConfigureDAL.GetChildMenuList(parentMenuId);
            return Json(data, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public ActionResult UpdateParent(MenuConfigureBEL menuConfigureBel)
        {
            try
            {
                return _menuConfigureDAL.UpdateParent(menuConfigureBel) ? Json(new { Message = _menuConfigureDAL.InsertMessage, Status = "Ok", Id = _menuConfigureDAL.MaxID }) : Json(new { Status = _menuConfigureDAL.ExceptionReturn });
            }
            catch (Exception e)
            {
                return Json(new { Status = e.Message });
            }
        }
        [HttpPost]
        public ActionResult InsertMenuConfigure(MenuConfigureBEL menuConfigureBel)
        {
            try
            {
                return _menuConfigureDAL.InsertMenuInfo(menuConfigureBel) ? Json(new { Message = _menuConfigureDAL.InsertMessage, Status = "Ok", Id = _menuConfigureDAL.MaxID }) : Json(new { Status = _menuConfigureDAL.ExceptionReturn });
            }
            catch (Exception e)
            {
                return Json(new { Status = e.Message });
            }
        }
        [HttpPost]
        public ActionResult UpdateMenuConfigure(MenuConfigureBEL menuConfigureBel)
        {
            try
            {
                return _menuConfigureDAL.UpdateMenuInfo(menuConfigureBel) ? Json(new { Message = _menuConfigureDAL.UpdateMessage, Status = "Ok", Id = _menuConfigureDAL.MaxID }) : Json(new { Status = _menuConfigureDAL.ExceptionReturn });
            }
            catch (Exception e)
            {
                return Json(new { Status = e.Message });
            }
        }
        [HttpPost]
        public ActionResult GetMenuConfigureList(int parentId)
        {
            var data = _menuConfigureDAL.GetMenuConfigureList(parentId);
            return Json(data, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public ActionResult IsChildSeqExist(int parentId,string parentSeq)
        {
            var data = _menuConfigureDAL.IsChildSeqExist(parentId, parentSeq);
            return Json(new{Status=_menuConfigureDAL.ExceptionReturn,Data=data}, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public ActionResult IsParentSeqExist(int childId, string childSeq)
        {
            var data = _menuConfigureDAL.IsParentSeqExist(childId, childSeq);
            return Json(new{Status=_menuConfigureDAL.ExceptionReturn,Data=data}, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public ActionResult DeleteMenuConfigure(int id)
        {
            try
            {
                return _menuConfigureDAL.DeleteMenuConfigure(id) ? Json(new { Message = _menuConfigureDAL.DeleteMessage, Status = "Ok" }) : Json(new { Status = _menuConfigureDAL.ExceptionReturn });
            }
            catch(Exception e)
            {
                return Json(new { Status = e.Message });
            }
        }
    }
}