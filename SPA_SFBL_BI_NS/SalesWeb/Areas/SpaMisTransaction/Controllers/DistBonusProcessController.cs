using System;
using System.Web.Mvc;
using SalesWeb.Areas.SpaMisTransaction.Models.BEL;
using SalesWeb.Areas.SpaMisTransaction.Models.DAL;
using SalesWeb.Universal.Gateway;

namespace SalesWeb.Areas.SpaMisTransaction.Controllers
{
    [LogInChecker]
    public class DistBonusProcessController : Controller
    {
        DistBonusProcessDAL _distBonusProcessDAL = new DistBonusProcessDAL();
        public ActionResult frmDistBonusProcess()
        {
            string CurentDate = DateTime.Now.ToString("dd/MM/yyyy");
            ViewBag.CurentDate = CurentDate;
            return View();
        }


        [HttpGet]
        public ActionResult SearchData()
        {
            try
            {
                var data = _distBonusProcessDAL.SearchData();
                return Json(new { Status = _distBonusProcessDAL.ExceptionReturn, Data = data }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception e)
            {
                return Json(new { Status = e.Message, Data = " " }, JsonRequestBehavior.AllowGet);
            }
        }

        [HttpPost]
        public ActionResult InsertDistBonusProcess(DistBonusProcessBEL master)
        {
            try
            {
                return _distBonusProcessDAL.InsertDistBonusProcess(master) ? Json(new { Message = _distBonusProcessDAL.InsertMessage, Status = "Ok", Id = _distBonusProcessDAL.MaxID, Code = _distBonusProcessDAL.MaxCode }) : Json(new { Status = _distBonusProcessDAL.ExceptionReturn });
            }
            catch (Exception e)
            {
                return Json(new { Status = e.Message });
            }
        }


        [HttpPost]
        public ActionResult UpdateDistBonusProcess(DistBonusProcessBEL master)
        {
            try
            {
                return _distBonusProcessDAL.UpdateDistBonusProcess(master) ? Json(new { Message = _distBonusProcessDAL.UpdateMessage, Status = "Ok", Id = _distBonusProcessDAL.MaxID }) : Json(new { Status = _distBonusProcessDAL.ExceptionReturn });
            }
            catch (Exception e)
            {
                return Json(new { Status = e.Message });
            }
        }



        [HttpPost]
        public ActionResult DeleteProcess(string processSlno)
        {
            return _distBonusProcessDAL.DeleteProcess(processSlno) ? Json(new { Message = _distBonusProcessDAL.DeleteMessage, Status = "Ok" }) : Json(new { Status = _distBonusProcessDAL.ExceptionReturn });

        }


        [HttpGet]
        public ActionResult GetLastBonusProcessData()
        {
            var data = _distBonusProcessDAL.GetLastBonusProcessData();
            return Json(new { Status = _distBonusProcessDAL.ExceptionReturn, Data = data }, JsonRequestBehavior.AllowGet);
        }




    }
}