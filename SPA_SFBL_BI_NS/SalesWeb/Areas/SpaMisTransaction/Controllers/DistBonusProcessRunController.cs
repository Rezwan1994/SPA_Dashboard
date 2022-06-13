using System;
using System.Web.Mvc;
using SalesWeb.Areas.SpaMisTransaction.Models.BEL;
using SalesWeb.Areas.SpaMisTransaction.Models.DAL;
using SalesWeb.Universal.Gateway;

namespace SalesWeb.Areas.SpaMisTransaction.Controllers
{
    [LogInChecker]
    public class DistBonusProcessRunController : Controller
    {
        DistBonusProcessRunDAL _distBonusProcessRunDAL = new DistBonusProcessRunDAL();
        public ActionResult frmDistBonusProcessRun()
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
                var data = _distBonusProcessRunDAL.GetProcessList();
                return Json(new { Status = _distBonusProcessRunDAL.ExceptionReturn, Data = data }, JsonRequestBehavior.AllowGet);
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
                var data = _distBonusProcessRunDAL.SearchData();
                return Json(new { Status = _distBonusProcessRunDAL.ExceptionReturn, Data = data }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception e)
            {
                return Json(new { Status = e.Message, Data = " " }, JsonRequestBehavior.AllowGet);
            }
        }


        [HttpPost]
        public ActionResult ProcessRun(string process_slno, string process_run_date)
        {
            return _distBonusProcessRunDAL.ProcessRun(process_slno, process_run_date) ? Json(new { Message = _distBonusProcessRunDAL.ProcessRunMessage, Status = "Ok" }) : Json(new { Status = _distBonusProcessRunDAL.ExceptionReturn });

        }

    }
}