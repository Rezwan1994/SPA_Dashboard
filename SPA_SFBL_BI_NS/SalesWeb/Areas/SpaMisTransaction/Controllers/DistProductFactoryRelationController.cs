using System;
using System.Web.Mvc;
using SalesWeb.Areas.SpaMisTransaction.Models.BEL;
using SalesWeb.Areas.SpaMisTransaction.Models.DAL;
using SalesWeb.Universal.Gateway;
using System.Collections.Generic;
using static SalesWeb.Areas.SpaMisTransaction.Models.BEL.DistProductFactoryRelationBEL;

namespace SalesWeb.Areas.SpaMisTransaction.Controllers
{
    [LogInChecker]
    public class DistProductFactoryRelationController : Controller
    {
        DistProductFactoryRelationDAL _distProductFactoryRelationDAL = new DistProductFactoryRelationDAL();
        public ActionResult frmDistProductFactoryRelation()
        {
            return View();
        }

        [HttpGet]
        public ActionResult GetCustomerList()
        {
            try
            {
                var data = _distProductFactoryRelationDAL.GetCustomerList();
                return Json(new { Status = _distProductFactoryRelationDAL.ExceptionReturn, Data = data }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception e)
            {
                return Json(new { Status = e.Message, Data = " " }, JsonRequestBehavior.AllowGet);
            }
        }

        [HttpGet]
        public ActionResult GetProductList()
        {
            try
            {
                var data = _distProductFactoryRelationDAL.GetProductList();
                return Json(new { Status = _distProductFactoryRelationDAL.ExceptionReturn, Data = data }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception e)
            {
                return Json(new { Status = e.Message, Data = " " }, JsonRequestBehavior.AllowGet);
            }
        }

        [HttpGet]
        public ActionResult GetFactoryList()
        {
            try
            {
                var data = _distProductFactoryRelationDAL.GetFactoryList();
                return Json(new { Status = _distProductFactoryRelationDAL.ExceptionReturn, Data = data }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception e)
            {
                return Json(new { Status = e.Message, Data = " " }, JsonRequestBehavior.AllowGet);
            }
        }

        [HttpPost]
        public ActionResult InsertData(DistProductFactoryRelationMstBEL mstData, List<DistProductFactoryRelationDtlBEL> dtlData)
        {
            return _distProductFactoryRelationDAL.InsertData(mstData, dtlData) ? Json(new { Message = _distProductFactoryRelationDAL.InsertMessage, Status = "Ok", Id = _distProductFactoryRelationDAL.MstID }) : Json(new { Status = _distProductFactoryRelationDAL.ExceptionReturn });

        }


        [HttpPost]
        public ActionResult UpdateData(DistProductFactoryRelationMstBEL mstData, List<DistProductFactoryRelationDtlBEL> dtlData)
        {
            return _distProductFactoryRelationDAL.UpdateData(mstData, dtlData) ? Json(new { Message = _distProductFactoryRelationDAL.InsertMessage, Status = "Ok", Id = _distProductFactoryRelationDAL.MstID }) : Json(new { Status = _distProductFactoryRelationDAL.ExceptionReturn });

        }




        [HttpGet]
        public ActionResult GetSearchProduct(string param)
        {
            try
            {
                var data = _distProductFactoryRelationDAL.GetSearchProduct(param);
                return Json(new { Status = _distProductFactoryRelationDAL.ExceptionReturn, Data = data }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception e)
            {
                return Json(new { Status = e.Message, Data = " " }, JsonRequestBehavior.AllowGet);
            }
        }


        [HttpGet]
        public ActionResult SearchMstData()
        {
            try
            {
                var data = _distProductFactoryRelationDAL.SearchMstData();
                return Json(new { Status = _distProductFactoryRelationDAL.ExceptionReturn, Data = data }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception e)
            {
                return Json(new { Status = e.Message, Data = " " }, JsonRequestBehavior.AllowGet);
            }
        }



        [HttpPost]
        public ActionResult DeleteProduct(string DtlId)
        {
            return _distProductFactoryRelationDAL.DeleteProduct(DtlId) ? Json(new { Message = _distProductFactoryRelationDAL.DeleteMessage, Status = "Ok" }) : Json(new { Status = _distProductFactoryRelationDAL.ExceptionReturn });

        }

        [HttpPost]
        public ActionResult DeleteMstDtl(string MstId)
        {
            return _distProductFactoryRelationDAL.DeleteMstDtl(MstId) ? Json(new { Message = _distProductFactoryRelationDAL.DeleteMessage, Status = "Ok" }) : Json(new { Status = _distProductFactoryRelationDAL.ExceptionReturn });

        }



    }
}