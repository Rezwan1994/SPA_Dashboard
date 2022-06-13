using System;
using System.Web.Mvc;
using SalesWeb.Areas.Security.Models.BEL;
using SalesWeb.Areas.Security.Models.DAL;
using SalesWeb.Universal.Gateway;

namespace SalesWeb.Areas.Security.Controllers
{
    [LogInChecker]
    public class MenuInfoController : Controller
    {
        private readonly MenuInfoDAL menuInfoDAL = new MenuInfoDAL();
        public ActionResult frmMenuInfo()
        {
            return View();
        }
        [HttpGet]
        public ActionResult GetMenuList()
        {
            var data = menuInfoDAL.GetMenuList();
            return Json(data, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public ActionResult InsertMenuInfo(MenuInfoBEL master)
        {
            try
            {
                return menuInfoDAL.InsertMenuInfo(master) ? Json(new { Message = menuInfoDAL.InsertMessage, Status = "Ok", Id = menuInfoDAL.MaxID }) : Json(new { Status = menuInfoDAL.ExceptionReturn });
            }
            catch (Exception e)
            {
                return Json(new { Status = e.Message });
            }
        }
        [HttpPost]
        public ActionResult UpdateMenuInfo(MenuInfoBEL master)
        {
            try
            {
                return menuInfoDAL.UpdateMenuInfo(master) ? Json(new { Message = menuInfoDAL.UpdateMessage, Status = "Ok", Id = menuInfoDAL.MaxID }) : Json(new { Status = menuInfoDAL.ExceptionReturn });
            }
            catch (Exception e)
            {
                return Json(new { Status = e.Message });
            }
        }

    }
}