using System;
using System.Web.Mvc;
using SalesWeb.Areas.SpaMisReport.Models.BEL;
using SalesWeb.Areas.SpaMisReport.Models.DAL;
using SalesWeb.Universal.Gateway;


namespace SalesWeb.Areas.SpaMisReport.Controllers
{
    [LogInChecker]
    public class NationalStockWithValueController : Controller
    {


        NationalStockWithValueDAL _nationalStockWithValueDAL = new NationalStockWithValueDAL();


        // GET: SpaMisReport/NationalStockWithValue
        public ActionResult frmNationalStockWithValue()
        {
            return View();
        }
        public ActionResult GetNationalStockWithValue()
        {
            var listData = _nationalStockWithValueDAL.GetNationalStockWithValue();
            var data = Json(listData, JsonRequestBehavior.AllowGet);
            data.MaxJsonLength = int.MaxValue;
            return data;

        }



    }
}



