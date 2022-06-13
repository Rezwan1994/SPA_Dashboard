using System;
using System.Web.Mvc;
using SalesWeb.Areas.SpaMisReport.Models.BEL;
using SalesWeb.Areas.SpaMisReport.Models.DAL;
using SalesWeb.Universal.Gateway;
using System.Data;
using System.Web;

namespace SalesWeb.Areas.SpaMisReport.Controllers
{
    [LogInChecker]
    public class FsoWiseImsController : Controller
    {
        FsoWiseImsDAL _fsoWiseImsDAL = new FsoWiseImsDAL();
        public ActionResult frmFsoWiseIms()
        {
            return View();
        }
        [HttpPost]
        public ActionResult GetFsoWiseImsDateRange(string fDate, string tDate, string dCode, string rCode, string aCode, string tCode, string cCode)
        {
            var listData = _fsoWiseImsDAL.GetFsoWiseImsDateRange(fDate, tDate, dCode, rCode, aCode, tCode, cCode);
            var data = Json(listData, JsonRequestBehavior.AllowGet);
            data.MaxJsonLength = int.MaxValue;
            return data;

        }
    }
}