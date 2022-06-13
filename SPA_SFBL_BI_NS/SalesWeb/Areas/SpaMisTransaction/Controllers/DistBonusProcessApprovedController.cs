using System;
using System.Web.Mvc;
using SalesWeb.Areas.SpaMisTransaction.Models.BEL;
using SalesWeb.Areas.SpaMisTransaction.Models.DAL;
using SalesWeb.Universal.Gateway;

namespace SalesWeb.Areas.SpaMisTransaction.Controllers
{
    [LogInChecker]
    public class DistBonusProcessApprovedController : Controller
    {
        DistBonusProcessApprovedDAL _distBonusProcessApprovedDAL = new DistBonusProcessApprovedDAL();
        public ActionResult frmDistBonusProcessApproved()
        {
            string CurentDate = DateTime.Now.ToString("dd/MM/yyyy");
            ViewBag.CurentDate = CurentDate;
            return View();
        }


        [HttpGet]
        public ActionResult GetProcessList()
        {
            try
            {
                var data = _distBonusProcessApprovedDAL.GetProcessList();
                return Json(new { Status = _distBonusProcessApprovedDAL.ExceptionReturn, Data = data }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception e)
            {
                return Json(new { Status = e.Message, Data = " " }, JsonRequestBehavior.AllowGet);
            }
        }


        [HttpGet]
        public ActionResult SearchData()
        {
            try
            {
                var data = _distBonusProcessApprovedDAL.SearchData();
                return Json(new { Status = _distBonusProcessApprovedDAL.ExceptionReturn, Data = data }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception e)
            {
                return Json(new { Status = e.Message, Data = " " }, JsonRequestBehavior.AllowGet);
            }
        }


        [HttpPost]
        public ActionResult UpdateDistBonusProcessApproved(DistBonusProcessBEL master)
        {
            try
            {
                return _distBonusProcessApprovedDAL.UpdateDistBonusProcessApproved(master) ? Json(new { Message = _distBonusProcessApprovedDAL.UpdateMessage, Status = "Ok", Id = _distBonusProcessApprovedDAL.MaxID }) : Json(new { Status = _distBonusProcessApprovedDAL.ExceptionReturn });
            }
            catch (Exception e)
            {
                return Json(new { Status = e.Message });
            }
        }




    }

}