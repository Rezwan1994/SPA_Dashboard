using System;
using System.Web.Mvc;
using SalesWeb.Areas.SpaMisReport.Models.BEL;
using SalesWeb.Areas.SpaMisReport.Models.DAL;
using SalesWeb.Universal.Gateway;

namespace SalesWeb.Areas.SpaMisReport.Controllers
{
    [LogInChecker]
    public class ComboBonusValController : Controller
    {
        ComboBonusValDAL _comboBonusValDAL = new ComboBonusValDAL();
        public ActionResult frmComboBonusVal()
        {
            return View();
        }

        [HttpGet]
        public ActionResult GetComboBonusList()
        {
            try
            {
                var data = _comboBonusValDAL.GetComboBonusList();
                return Json(new { Status = _comboBonusValDAL.ExceptionReturn, Data = data }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception e)
            {
                return Json(new { Status = e.Message, Data = " " }, JsonRequestBehavior.AllowGet);
            }
        }

        [HttpPost]
        public ActionResult GetComboBonusValCustomDate(string fDate, string tDate, string dCode, string rCode, string aCode, string tCode, string cCode, string cbNo)
        {
            var listData = _comboBonusValDAL.GetComboBonusValCustomDate(fDate, tDate, dCode, rCode, aCode, tCode, cCode, cbNo);
            var data = Json(listData, JsonRequestBehavior.AllowGet);
            data.MaxJsonLength = int.MaxValue;
            return data;
        }


    }
}