using System;
using System.Web.Mvc;
using SalesWeb.Areas.SpaMisReport.Models.BEL;
using SalesWeb.Areas.SpaMisReport.Models.DAL;
using SalesWeb.Universal.Gateway;

namespace SalesWeb.Areas.SpaMisReport.Controllers
{
    [LogInChecker]
    public class FsoBrandRelationController : Controller
    {
        FsoBrandRelationDAL _fsoBrandRelationDAL = new FsoBrandRelationDAL();
        public ActionResult frmFsoBrandRelation()
        {
            return View();
        }
        [HttpPost]
        public ActionResult GetFsoBrandRelation(string dCode, string rCode, string aCode, string tCode, string cCode)
        {
            var listData = _fsoBrandRelationDAL.GetFsoBrandRelation(dCode, rCode, aCode, tCode, cCode);
            var data = Json(listData, JsonRequestBehavior.AllowGet);
            data.MaxJsonLength = int.MaxValue;
            return data;

        }
    }
}