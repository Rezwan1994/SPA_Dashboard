using System;
using System.Web.Mvc;
using SalesWeb.Areas.SpaMisReport.Models.BEL;
using SalesWeb.Areas.SpaMisReport.Models.DAL;
using SalesWeb.Universal.Gateway;

namespace SalesWeb.Areas.SpaMisReport.Controllers
{
    [LogInChecker]
    public class TradeProgramParticipationController : Controller
    {
        TradeProgramParticipationDAL _tradeProgramParticipationDAL = new TradeProgramParticipationDAL();
        public ActionResult frmTradeProgramParticipation()
        {
            return View();
        }




        [HttpGet]
        public ActionResult GetTradeProgramList(string eType)
        {
            try
            {
                var data = _tradeProgramParticipationDAL.GetTradeProgramList(eType);
                return Json(new { Status = _tradeProgramParticipationDAL.ExceptionReturn, Data = data }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception e)
            {
                return Json(new { Status = e.Message, Data = " " }, JsonRequestBehavior.AllowGet);
            }
        }


        [HttpPost]
        public ActionResult GetTradeProgramParticipationList(string dCode, string rCode, string aCode, string tCode, string cCode, string tradeNo, string eType)
        {
            var listData = _tradeProgramParticipationDAL.GetTradeProgramParticipationList(dCode, rCode, aCode, tCode, cCode, tradeNo, eType);
            var data = Json(listData, JsonRequestBehavior.AllowGet);
            data.MaxJsonLength = int.MaxValue;
            return data;
        }



    }
}