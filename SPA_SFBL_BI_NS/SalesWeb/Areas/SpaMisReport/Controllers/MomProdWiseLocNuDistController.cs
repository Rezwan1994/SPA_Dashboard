using System;
using System.Web.Mvc;
using SalesWeb.Areas.SpaMisReport.Models.BEL;
using SalesWeb.Areas.SpaMisReport.Models.DAL;
using SalesWeb.Universal.Gateway;

namespace SalesWeb.Areas.SpaMisReport.Controllers
{
    [LogInChecker]
    public class MomProdWiseLocNuDistController : Controller
    {
        MomProdWiseLocNuDistDAL _momProdWiseLocNuDistDAL = new MomProdWiseLocNuDistDAL();
        public ActionResult frmMomProdWiseLocNuDist()
        {
            return View();
        }

        [HttpPost]
        public ActionResult GetMomProdWiseLocNuDist(string dCode, string rCode, string aCode, string tCode, string cCode, string pCode)
        {
            var listData = _momProdWiseLocNuDistDAL.GetMomProdWiseLocNuDist(dCode, rCode, aCode, tCode, cCode, pCode);
            var data = Json(listData, JsonRequestBehavior.AllowGet);
            data.MaxJsonLength = int.MaxValue;
            return data;
        }


        [HttpPost]
        public ActionResult GetMomProdWiseLocNuDistLy(string dCode, string rCode, string aCode, string tCode, string cCode, string pCode)
        {
            var listData = _momProdWiseLocNuDistDAL.GetMomProdWiseLocNuDistLy(dCode, rCode, aCode, tCode, cCode, pCode);
            var data = Json(listData, JsonRequestBehavior.AllowGet);
            data.MaxJsonLength = int.MaxValue;
            return data;
        }

    }
}