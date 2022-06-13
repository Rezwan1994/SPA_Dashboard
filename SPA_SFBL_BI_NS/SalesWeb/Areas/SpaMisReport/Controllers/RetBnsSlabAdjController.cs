using System;
using System.Web.Mvc;
using SalesWeb.Areas.SpaMisReport.Models.BEL;
using SalesWeb.Areas.SpaMisReport.Models.DAL;
using SalesWeb.Universal.Gateway;

namespace SalesWeb.Areas.SpaMisReport.Controllers
{
    [LogInChecker]
    public class RetBnsSlabAdjController : Controller
    {
        RetBnsSlabAdjDAL _retBnsSlabAdjDAL = new RetBnsSlabAdjDAL();
        public ActionResult frmRetBnsSlabAdj()
        {
            return View();
        }



        [HttpPost]
        public ActionResult GetRetBnsSlabAdjCustomDate(string fromDate, string toDate, string dCode, string rCode, string aCode, string tCode, string cCode,string bType)
        {
            var listData = _retBnsSlabAdjDAL.GetRetBnsSlabAdjCustomDate(fromDate, toDate,dCode, rCode, aCode, tCode, cCode, bType);
            var data = Json(listData, JsonRequestBehavior.AllowGet);
            data.MaxJsonLength = int.MaxValue;
            return data;
        }



        [HttpPost]
        public ActionResult GetRetBnsSlabAdjToday(string dCode, string rCode, string aCode, string tCode, string cCode, string bType)
        {
            var listData = _retBnsSlabAdjDAL.GetRetBnsSlabAdjToday(dCode, rCode, aCode, tCode, cCode, bType);
            var data = Json(listData, JsonRequestBehavior.AllowGet);
            data.MaxJsonLength = int.MaxValue;
            return data;
        }






    }
}