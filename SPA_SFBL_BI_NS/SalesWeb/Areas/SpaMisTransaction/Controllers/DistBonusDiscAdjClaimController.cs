using System;
using System.Web.Mvc;
using SalesWeb.Areas.SpaMisTransaction.Models.BEL;
using SalesWeb.Areas.SpaMisTransaction.Models.DAL;
using SalesWeb.Universal.Gateway;
namespace SalesWeb.Areas.SpaMisTransaction.Controllers
{
    [LogInChecker]
    public class DistBonusDiscAdjClaimController : Controller
    {
        DistBonusDiscAdjClaimDAL _distBonusDiscAdjClaimDAL = new DistBonusDiscAdjClaimDAL();
        public ActionResult frmDistBonusDiscAdjClaim()
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
                var data = _distBonusDiscAdjClaimDAL.GetProcessList();
                return Json(new { Status = _distBonusDiscAdjClaimDAL.ExceptionReturn, Data = data }, JsonRequestBehavior.AllowGet);
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
                var data = _distBonusDiscAdjClaimDAL.GetCustomerList(param);
                return Json(new { Status = _distBonusDiscAdjClaimDAL.ExceptionReturn, Data = data }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception e)
            {
                return Json(new { Status = e.Message, Data = " " }, JsonRequestBehavior.AllowGet);
            }
        }

        [HttpGet]
        public ActionResult GetData(string ProcessNo, string CustomerCode)
        {
            var listData = _distBonusDiscAdjClaimDAL.GetData(ProcessNo, CustomerCode);
            var data = Json(listData, JsonRequestBehavior.AllowGet);
            data.MaxJsonLength = int.MaxValue;
            return data;
        }




    }
}