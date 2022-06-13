using System;
using System.Web.Mvc;
using SalesWeb.Areas.SpaMisTransaction.Models.BEL;
using SalesWeb.Areas.SpaMisTransaction.Models.DAL;
using SalesWeb.Universal.Gateway;

namespace SalesWeb.Areas.SpaMisTransaction.Controllers
{
    [LogInChecker]
    public class DistBonusAdjProcessFinalizeController : Controller
    {
        DistBonusAdjProcessFinalizeDAL _distBonusAdjProcessFinalizeDAL = new DistBonusAdjProcessFinalizeDAL();
        public ActionResult frmDistBonusAdjProcessFinalize()
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
                var data = _distBonusAdjProcessFinalizeDAL.GetProcessList();
                return Json(new { Status = _distBonusAdjProcessFinalizeDAL.ExceptionReturn, Data = data }, JsonRequestBehavior.AllowGet);
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
                var data = _distBonusAdjProcessFinalizeDAL.SearchData();
                return Json(new { Status = _distBonusAdjProcessFinalizeDAL.ExceptionReturn, Data = data }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception e)
            {
                return Json(new { Status = e.Message, Data = " " }, JsonRequestBehavior.AllowGet);
            }
        }


        [HttpPost]
        public ActionResult UpdateDistBonusAdjProcessFinalize(DistBonusProcessBEL master)
        {
            try
            {
                return _distBonusAdjProcessFinalizeDAL.UpdateDistBonusAdjProcessFinalize(master) ? Json(new { Message = _distBonusAdjProcessFinalizeDAL.UpdateMessage, Status = "Ok", Id = _distBonusAdjProcessFinalizeDAL.MaxID }) : Json(new { Status = _distBonusAdjProcessFinalizeDAL.ExceptionReturn });
            }
            catch (Exception e)
            {
                return Json(new { Status = e.Message });
            }
        }



    }
}