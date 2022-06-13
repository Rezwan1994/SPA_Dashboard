using System;
using System.Web.Mvc;
using SalesWeb.Areas.SpaMisReport.Models.BEL;
using SalesWeb.Areas.SpaMisReport.Models.DAL;
using SalesWeb.Universal.Gateway;


namespace SalesWeb.Areas.SpaMisReport.Controllers
{
    [LogInChecker]
    public class DisplayProgramParticipationController : Controller
    {
        DisplayProgramParticipationDAL _displayProgramParticipationDAL = new DisplayProgramParticipationDAL();
        public ActionResult frmDisplayProgramParticipation()
        {
            return View();
        }




        [HttpPost]
        public ActionResult GetDisplayProgramParticipation(string dCode, string rCode, string aCode, string tCode, string cCode, string dProgramNo)
        {
            var listData = _displayProgramParticipationDAL.GetDisplayProgramParticipation(dCode, rCode, aCode, tCode, cCode,dProgramNo);
            var data = Json(listData, JsonRequestBehavior.AllowGet);
            data.MaxJsonLength = int.MaxValue;
            return data;
        }




    }
}