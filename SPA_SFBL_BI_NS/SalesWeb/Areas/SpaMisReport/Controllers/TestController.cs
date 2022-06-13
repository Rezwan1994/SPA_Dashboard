using System;
using System.Web.Mvc;
using SalesWeb.Areas.SpaMisReport.Models.BEL;
using SalesWeb.Areas.SpaMisReport.Models.DAL;
using SalesWeb.Universal.Gateway;
using static SalesWeb.Areas.SpaMisReport.Models.BEL.TestBEL;
using System.Collections.Generic;

namespace SalesWeb.Areas.SpaMisReport.Controllers
{
    [LogInChecker]
    public class TestController : Controller
    {
        private readonly TestDAL _testDAL = new TestDAL();

        private readonly CallTestProcedureDAL _callTestProcedureDAL = new CallTestProcedureDAL();

        public ActionResult frmTest()
        {
            return View();
        }


        [HttpGet]
        public ActionResult GetProductList()
        {
            var data = _testDAL.GetProductList();
            return Json(new { Status = _testDAL.ExceptionReturn, Data = data }, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public ActionResult GetOrderMst(string mstId)
        {
            var data = _testDAL.GetOrderMst(mstId);
            return Json(new { Status = _testDAL.ExceptionReturn, Data = data }, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public ActionResult GetOrderDtl(string mstId)
        {
            var data = _testDAL.GetOrderDtl(mstId);
            return Json(new { Status = _testDAL.ExceptionReturn, Data = data }, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult InsertOrderEntry(OrderMstBEL OrderMst, List<OrderDtlBEL> OrderDtl)
        {
            return _testDAL.InsertOrderEntry(OrderMst, OrderDtl) ? Json(new { Message = _testDAL.InsertMessage, Status = "Ok", Id = _testDAL.MaxID }) : Json(new { Status = _testDAL.ExceptionReturn });

        }

        [HttpPost]
        public ActionResult UpdateOrderEntry(OrderMstBEL OrderMst, List<OrderDtlBEL> OrderDtl)
        {
            return _testDAL.UpdateOrderEntry(OrderMst, OrderDtl) ? Json(new { Message = _testDAL.UpdateMessage, Status = "Ok", Id = _testDAL.MstID }) : Json(new { Status = _testDAL.ExceptionReturn });

        }


        [HttpPost]
        public ActionResult ExcuteTestProcedure()
        {
            return _callTestProcedureDAL.ExcuteTestProcedure() ? Json(new { Message = _callTestProcedureDAL.OutMsg }) : Json(new { Status = _callTestProcedureDAL.ExceptionReturn });

        }
    }
}