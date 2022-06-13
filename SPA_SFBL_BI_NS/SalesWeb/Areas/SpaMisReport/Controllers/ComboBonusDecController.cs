using System;
using System.Web.Mvc;
using SalesWeb.Areas.SpaMisReport.Models.BEL;
using SalesWeb.Areas.SpaMisReport.Models.DAL;
using SalesWeb.Universal.Gateway;

namespace SalesWeb.Areas.SpaMisReport.Controllers
{
    [LogInChecker]
    public class ComboBonusDecController : Controller
    {
        ComboBonusDecDAL _comboBonusDecDAL = new ComboBonusDecDAL();
        public ActionResult frmComboBonusDec()
        {
            return View();
        }
 

        [HttpPost]
        public ActionResult GetComboBonusDeclaration(string cbNo)
        {
            var listData = _comboBonusDecDAL.GetComboBonusDeclaration(cbNo);
            var data = Json(listData, JsonRequestBehavior.AllowGet);
            data.MaxJsonLength = int.MaxValue;
            return data;
        }
    }
}