using System;
using System.Web.Mvc;
using SalesWeb.Areas.SpaMisReport.Models.BEL;
using SalesWeb.Areas.SpaMisReport.Models.DAL;
using SalesWeb.Universal.Gateway;

namespace SalesWeb.Areas.SpaMisReport.Controllers
{
    [LogInChecker]
    public class SalesRegisterController : Controller
    {
        SalesRegisterDAL _salesRegisterDAL = new SalesRegisterDAL();
        public ActionResult frmSalesRegister()
        {
            return View();
        }

        [HttpPost]
        public ActionResult GetSalesRegisterUptoCurMonth(string dCode, string rCode, string aCode, string tCode, string cCode)
        {
            var listData = _salesRegisterDAL.GetSalesRegisterUptoCurMonth(dCode, rCode, aCode, tCode, cCode);
            var data = Json(listData, JsonRequestBehavior.AllowGet);
            data.MaxJsonLength = int.MaxValue;
            return data;

        }
        [HttpPost]
        public ActionResult GetSalesRegisterCustomDate(string fDate, string tDate, string dCode, string rCode, string aCode, string tCode, string cCode)
        {
            var listData = _salesRegisterDAL.GetSalesRegisterCustomDate(fDate, tDate, dCode, rCode, aCode, tCode, cCode);
            var data = Json(listData, JsonRequestBehavior.AllowGet);
            data.MaxJsonLength = int.MaxValue;
            return data;
        }
    }
}