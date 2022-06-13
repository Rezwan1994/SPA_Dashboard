using System.Web.Mvc;
using SalesWeb.Areas.SpaMisReport.Models.DAL;
using SalesWeb.Universal.Gateway;

namespace SalesWeb.Areas.SpaMisReport.Controllers
{
    [LogInChecker]
    public class FsoTimeKeepingController : Controller
    {
        FsoTimeKeepingDAL _fsoTimeKeepingDAL = new FsoTimeKeepingDAL();
        public ActionResult frmFsoTimeKeeping()
        {
            return View();
        }

        [HttpPost]
        public ActionResult GetFsoTimeKeepingDateRange(string fDate, string tDate, string dCode, string rCode, string aCode, string tCode, string cCode, string sCode)
        {
            var listData = _fsoTimeKeepingDAL.GetFsoTimeKeepingDateRange(fDate, tDate, dCode, rCode, aCode, tCode, cCode, sCode);
            var data = Json(listData, JsonRequestBehavior.AllowGet);
            data.MaxJsonLength = int.MaxValue;
            return data;
        }

    }
}