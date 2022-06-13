using System;
using System.Web.Mvc;
using SalesWeb.Areas.Security.Models.BEL;
using SalesWeb.Areas.Security.Models.DAL;
using SalesWeb.Universal.Gateway;

namespace SalesWeb.Areas.Security.Controllers
{
    [LogInChecker]
    public class ChangPassController : Controller
    {
        private readonly ChangPassDAL _changPassDAL = new ChangPassDAL();
        public ActionResult frmChngPass()
        {
            return View();
        }
        [HttpPost]
        public ActionResult UpdatePassword(ChangPassBEL changPass)
        {
            try
            {
                if (_changPassDAL.UpdatePassword(changPass))
                {
                    return Json(new { Message = _changPassDAL.UpdateMessage, Status = "Ok", ID = _changPassDAL.MaxID });
                }
                else
                {
                    return Json(new { Status = _changPassDAL.ExceptionReturn });
                }

            }
            catch (Exception e)
            {
                return Json(new { Status = e.Message });
            }
        }

        [HttpGet]
        public ActionResult GetCurrentPassword()
        {
            var data = _changPassDAL.GetCurrentPassword();
            return Json(data, JsonRequestBehavior.AllowGet);
        }
    }
}