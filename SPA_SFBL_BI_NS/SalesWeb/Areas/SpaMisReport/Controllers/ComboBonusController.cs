using System;
using System.Web.Mvc;
using SalesWeb.Areas.SpaMisReport.Models.BEL;
using SalesWeb.Areas.SpaMisReport.Models.DAL;
using SalesWeb.Universal.Gateway;


namespace SalesWeb.Areas.SpaMisReport.Controllers
{
    [LogInChecker]
    public class ComboBonusController : Controller
    {
        ComboBonusDAL _comboBonusDAL = new ComboBonusDAL();
        public ActionResult frmComboBonus()
        {
            return View();
        }


        [HttpGet]
        public ActionResult GetComboBonusList()
        {
            try
            {
                var data = _comboBonusDAL.GetComboBonusList();
                return Json(new { Status = _comboBonusDAL.ExceptionReturn, Data = data }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception e)
            {
                return Json(new { Status = e.Message, Data = " " }, JsonRequestBehavior.AllowGet);
            }
        }


        [HttpPost]
        public ActionResult GetComboBonusCustomDate(string fDate, string tDate, string dCode, string rCode, string aCode, string tCode, string cCode, string cbNo)
        {
            var listData = _comboBonusDAL.GetComboBonusCustomDate(fDate,tDate,dCode, rCode, aCode, tCode, cCode, cbNo);
            var data = Json(listData, JsonRequestBehavior.AllowGet);
            data.MaxJsonLength = int.MaxValue;
            return data;
        }



    }
}