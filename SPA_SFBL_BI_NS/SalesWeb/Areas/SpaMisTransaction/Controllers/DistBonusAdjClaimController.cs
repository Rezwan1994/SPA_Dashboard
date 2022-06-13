using System;
using System.Web.Mvc;
using SalesWeb.Areas.SpaMisTransaction.Models.BEL;
using SalesWeb.Areas.SpaMisTransaction.Models.DAL;
using SalesWeb.Universal.Gateway;

namespace SalesWeb.Areas.SpaMisTransaction.Controllers
{
    [LogInChecker]
    public class DistBonusAdjClaimController : Controller
    {
        DistBonusAdjClaimDAL _DistBonusAdjClaimDAL = new DistBonusAdjClaimDAL();
        public ActionResult frmDistBonusAdjClaim()
        {
            return View();
        }



        [HttpGet]
        public ActionResult GetProcessList()
        {
            try
            {
                var data = _DistBonusAdjClaimDAL.GetProcessList();
                return Json(new { Status = _DistBonusAdjClaimDAL.ExceptionReturn, Data = data }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception e)
            {
                return Json(new { Status = e.Message, Data = " " }, JsonRequestBehavior.AllowGet);
            }
        }


        [HttpGet]
        public ActionResult GetCustomerList(string param)
        {
            try
            {
                var data = _DistBonusAdjClaimDAL.GetCustomerList(param);
                return Json(new { Status = _DistBonusAdjClaimDAL.ExceptionReturn, Data = data }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception e)
            {
                return Json(new { Status = e.Message, Data = " " }, JsonRequestBehavior.AllowGet);
            }
        }

        [HttpGet]
        public ActionResult GetData(string ProcessNo, string CustomerCode)
        {
            var listData = _DistBonusAdjClaimDAL.GetData(ProcessNo, CustomerCode);
            var data = Json(listData, JsonRequestBehavior.AllowGet);
            data.MaxJsonLength = int.MaxValue;
            return data;
        }
    }
}