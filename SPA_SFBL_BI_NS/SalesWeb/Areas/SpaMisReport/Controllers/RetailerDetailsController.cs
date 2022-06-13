using System;
using System.Web.Mvc;
using SalesWeb.Areas.SpaMisReport.Models.BEL;
using SalesWeb.Areas.SpaMisReport.Models.DAL;
using SalesWeb.Universal.Gateway;

namespace SalesWeb.Areas.SpaMisReport.Controllers
{
    [LogInChecker]
    public class RetailerDetailsController : Controller
    {
        RetailerDetailsDAL _retailerDetailsDAL = new RetailerDetailsDAL();
        public ActionResult frmRetailerDetails()
        {
            return View();
        }

        [HttpPost]
        public ActionResult GetRetailerDetails(string dCode, string rCode, string aCode, string tCode, string cCode, string mCode, string rCatCode, string rType, string rlocType, string status)
        {
            var listData = _retailerDetailsDAL.GetRetailerDetails(dCode, rCode, aCode, tCode, cCode, mCode, rCatCode, rType, rlocType, status);
            var data = Json(listData, JsonRequestBehavior.AllowGet);
            data.MaxJsonLength = int.MaxValue;
            return data;

        }


        /*

        var jsonResult = Json(new
        {
            draw = param.Draw,
            recordsTotal = count,
            recordsFiltered = count,
            data = result
        }, JsonRequestBehavior.AllowGet);
        jsonResult.MaxJsonLength = int.MaxValue;


            */


        [HttpPost]
        public ActionResult GetDeadRetailer(string fDate, string dCode, string rCode, string aCode, string tCode, string cCode, string mCode, string rCatCode, string rType, string rlocType, string status)
        {
            var listData = _retailerDetailsDAL.GetDeadRetailer(fDate, dCode, rCode, aCode, tCode, cCode, mCode, rCatCode, rType, rlocType, status);
            var data = Json(listData, JsonRequestBehavior.AllowGet);
            data.MaxJsonLength = int.MaxValue;
            return data;

        }

        [HttpGet]
        public ActionResult GetMarketList(string dCode, string rCode, string aCode, string tCode, string cCode)
        {
            try
            {
                var data = _retailerDetailsDAL.GetMarketList( dCode,  rCode,  aCode,  tCode,  cCode);
                return Json(new { Status = _retailerDetailsDAL.ExceptionReturn, Data = data }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception e)
            {
                return Json(new { Status = e.Message, Data = " " }, JsonRequestBehavior.AllowGet);
            }
        }



        [HttpGet]
        public ActionResult GetRouteList(string dCode, string rCode, string aCode, string tCode, string cCode, string mCode)
        {
            try
            {
                var data = _retailerDetailsDAL.GetRouteList(dCode, rCode, aCode, tCode, cCode,mCode);
                return Json(new { Status = _retailerDetailsDAL.ExceptionReturn, Data = data }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception e)
            {
                return Json(new { Status = e.Message, Data = " " }, JsonRequestBehavior.AllowGet);
            }
        }


        [HttpGet]
        public ActionResult GetRetailerCategoryList()
        {
            try
            {
                var data = _retailerDetailsDAL.GetRetailerCategoryList();
                return Json(new { Status = _retailerDetailsDAL.ExceptionReturn, Data = data }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception e)
            {
                return Json(new { Status = e.Message, Data = " " }, JsonRequestBehavior.AllowGet);
            }
        }


        


    }
}