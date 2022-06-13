using System;
using System.Web.Mvc;
using SalesWeb.Areas.SpaMisReport.Models.BEL;
using SalesWeb.Areas.SpaMisReport.Models.DAL;
using SalesWeb.Universal.Gateway;

namespace SalesWeb.Areas.SpaMisReport.Controllers
{
    [LogInChecker]
    public class DistSrSalesController : Controller
    {
        DistSrSalesDAL _distSrSalesDAL = new DistSrSalesDAL();

        public ActionResult frmDistSrSales()
        {
            return View();
        }

        [HttpPost]
        public ActionResult GetSrProductImsToday(string dCode, string rCode, string aCode, string tCode, string cCode, string sCode)
        {
            var listData = _distSrSalesDAL.GetSrProductImsToday(dCode, rCode, aCode, tCode, cCode, sCode);
            var data = Json(listData, JsonRequestBehavior.AllowGet);
            data.MaxJsonLength = int.MaxValue;
            return data;
        }

        [HttpPost]
        public ActionResult GetSrProductImsDateRange(string fDate, string tDate, string dCode, string rCode, string aCode, string tCode, string cCode, string sCode)
        {
            var listData = _distSrSalesDAL.GetSrProductImsDateRange(fDate,tDate,dCode, rCode, aCode, tCode, cCode, sCode);
            var data = Json(listData, JsonRequestBehavior.AllowGet);
            data.MaxJsonLength = int.MaxValue;
            return data;
        }


        [HttpPost]
        public ActionResult GetSrProductImsYesterday(string dCode, string rCode, string aCode, string tCode, string cCode, string sCode)
        {
            var listData = _distSrSalesDAL.GetSrProductImsYesterday( dCode,  rCode,  aCode,  tCode,  cCode, sCode);
            var data = Json(listData, JsonRequestBehavior.AllowGet);
            data.MaxJsonLength = int.MaxValue;
            return data;
        }


        [HttpPost]
        public ActionResult GetSrProductImsLastSevendays(string dCode, string rCode, string aCode, string tCode, string cCode, string sCode)
        {
            var listData = _distSrSalesDAL.GetSrProductImsLastSevendays(dCode, rCode, aCode, tCode, cCode, sCode);
            var data = Json(listData, JsonRequestBehavior.AllowGet);
            data.MaxJsonLength = int.MaxValue;
            return data;
        }

        [HttpPost]
        public ActionResult GetSrProductImsLastThirtydays(string dCode, string rCode, string aCode, string tCode, string cCode, string sCode)
        {
            var listData = _distSrSalesDAL.GetSrProductImsLastThirtydays(dCode, rCode, aCode, tCode, cCode, sCode);
            var data = Json(listData, JsonRequestBehavior.AllowGet);
            data.MaxJsonLength = int.MaxValue;
            return data;
        }

        [HttpPost]
        public ActionResult GetSrProductImsCurrentMonth(string dCode, string rCode, string aCode, string tCode, string cCode, string sCode)
        {
            var listData = _distSrSalesDAL.GetSrProductImsCurrentMonth(dCode, rCode, aCode, tCode, cCode, sCode);
            var data = Json(listData, JsonRequestBehavior.AllowGet);
            data.MaxJsonLength = int.MaxValue;
            return data;
        }


        [HttpPost]
        public ActionResult GetSrProductImsLastMonth(string dCode, string rCode, string aCode, string tCode, string cCode, string sCode)
        {
            var listData = _distSrSalesDAL.GetSrProductImsLastMonth(dCode, rCode, aCode, tCode, cCode, sCode);
            var data = Json(listData, JsonRequestBehavior.AllowGet);
            data.MaxJsonLength = int.MaxValue;
            return data;
        }



        [HttpGet]
        public ActionResult GetReportTypeList(string SlNo)
        {
            try
            {
                var data = _distSrSalesDAL.GetReportTypeList(SlNo);
                return Json(new { Status = _distSrSalesDAL.ExceptionReturn, Data = data }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception e)
            {
                return Json(new { Status = e.Message, Data = " " }, JsonRequestBehavior.AllowGet);
            }
        }

        [HttpGet]
        public ActionResult GetDivisionList()
        {
            try
            {
                var data = _distSrSalesDAL.GetDivisionList();
                return Json(new { Status = _distSrSalesDAL.ExceptionReturn, Data = data }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception e)
            {
                return Json(new { Status = e.Message, Data = " " }, JsonRequestBehavior.AllowGet);
            }
        }

        [HttpGet]
        public ActionResult GetRegionList(string dCode)
        {
            try
            {
                var data = _distSrSalesDAL.GetRegionList(dCode);
                return Json(new { Status = _distSrSalesDAL.ExceptionReturn, Data = data }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception e)
            {
                return Json(new { Status = e.Message, Data = " " }, JsonRequestBehavior.AllowGet);
            }
        }

        [HttpGet]
        public ActionResult GetAreaList(string dCode,string rCode)
        {
            try
            {
                var data = _distSrSalesDAL.GetAreaList(dCode,rCode);
                return Json(new { Status = _distSrSalesDAL.ExceptionReturn, Data = data }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception e)
            {
                return Json(new { Status = e.Message, Data = " " }, JsonRequestBehavior.AllowGet);
            }
        }

        [HttpGet]
        public ActionResult GetTerritoryList(string dCode, string rCode,string aCode)
        {
            try
            {
                var data = _distSrSalesDAL.GetTerritoryList(dCode, rCode,aCode);
                return Json(new { Status = _distSrSalesDAL.ExceptionReturn, Data = data }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception e)
            {
                return Json(new { Status = e.Message, Data = " " }, JsonRequestBehavior.AllowGet);
            }
        }

        [HttpGet]
        public ActionResult GetCustomerList(string dCode, string rCode, string aCode,string tCode)
        {
            try
            {
                var data = _distSrSalesDAL.GetCustomerList(dCode, rCode, aCode,tCode);
                return Json(new { Status = _distSrSalesDAL.ExceptionReturn, Data = data }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception e)
            {
                return Json(new { Status = e.Message, Data = " " }, JsonRequestBehavior.AllowGet);
            }
        }

        [HttpGet]
        public ActionResult GetSrList(string dCode, string rCode, string aCode, string tCode,string cCode)
        {
            try
            {
                var data = _distSrSalesDAL.GetSrList(dCode, rCode, aCode, tCode,cCode);
                return Json(new { Status = _distSrSalesDAL.ExceptionReturn, Data = data }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception e)
            {
                return Json(new { Status = e.Message, Data = " " }, JsonRequestBehavior.AllowGet);
            }
        }



        [HttpGet]
        public ActionResult GetDisplayProgramList(string dCode, string rCode, string aCode, string tCode, string cCode)
        {
            try
            {
                var data = _distSrSalesDAL.GetDisplayProgramList(dCode, rCode, aCode, tCode, cCode);
                return Json(new { Status = _distSrSalesDAL.ExceptionReturn, Data = data }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception e)
            {
                return Json(new { Status = e.Message, Data = " " }, JsonRequestBehavior.AllowGet);
            }
        }


    }
}