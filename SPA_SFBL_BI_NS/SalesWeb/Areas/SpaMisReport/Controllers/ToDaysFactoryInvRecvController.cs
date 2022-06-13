using System;
using System.Web.Mvc;
using SalesWeb.Areas.SpaMisReport.Models.BEL;
using SalesWeb.Areas.SpaMisReport.Models.DAL;
using SalesWeb.Universal.Gateway;

namespace SalesWeb.Areas.SpaMisReport.Controllers
{
    [LogInChecker]
    public class ToDaysFactoryInvRecvController : Controller


    {

        ToDaysFactoryInvRecvDAL _toDaysFactoryInvRecvDAL = new ToDaysFactoryInvRecvDAL();

        // GET: SpaMisReport/ToDaysFactoryInvRecv
        public ActionResult frmToDaysFactoryInvRecv()
        {
            return View();
        }


        public ActionResult GetToDaysFactoryInvRecvCustomDate(string fromDate, string toDate, string dCode, string rCode, string aCode, string tCode, string cCode)
        {
            var listData = _toDaysFactoryInvRecvDAL.GetToDaysFactoryInvRecvCustomDate(fromDate, toDate, dCode, rCode, aCode, tCode, cCode);
            var data = Json(listData, JsonRequestBehavior.AllowGet);
            data.MaxJsonLength = int.MaxValue;
            return data;
        }




    }
}