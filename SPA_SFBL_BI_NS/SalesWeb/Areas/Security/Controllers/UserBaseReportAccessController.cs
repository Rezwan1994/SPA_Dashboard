using System;
using System.Web.Mvc;
using SalesWeb.Areas.Security.Models.BEL;
using SalesWeb.Areas.Security.Models.DAL;
using SalesWeb.Universal.Gateway;
using Oracle.ManagedDataAccess.Client;
using System.Collections.Generic;
using static SalesWeb.Areas.Security.Models.BEL.UserBaseReportAccessBEL;

namespace SalesWeb.Areas.Security.Controllers
{
    [LogInChecker]
    public class UserBaseReportAccessController : Controller
    {
        UserBaseReportAccessDAL _userBaseReportAccessDAL = new UserBaseReportAccessDAL();
        public ActionResult frmUserBaseReportAccess()
        {
            return View();
        }

        [HttpGet]
        public ActionResult GetUserList()
        {
            try
            {
                var data = _userBaseReportAccessDAL.GetUserList();
                return Json(new { Status = _userBaseReportAccessDAL.ExceptionReturn, Data = data }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception e)
            {
                return Json(new { Status = e.Message, Data = " " }, JsonRequestBehavior.AllowGet);
            }
        }

        [HttpGet]
        public ActionResult GetTypeList(string pType)
        {
            try
            {
                var data = _userBaseReportAccessDAL.GetTypeList(pType);
                return Json(new { Status = _userBaseReportAccessDAL.ExceptionReturn, Data = data }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception e)
            {
                return Json(new { Status = e.Message, Data = " " }, JsonRequestBehavior.AllowGet);
            }
        }


        [HttpPost]
        public ActionResult InsertUserBaseReportAccess(UserMstBEL mstData, List<UserProductTypeBEL> productTypeData)
        {
            return _userBaseReportAccessDAL.InsertUserBaseReportAccess(mstData, productTypeData) ? Json(new { Message = _userBaseReportAccessDAL.InsertMessage, Status = "Ok", Id = _userBaseReportAccessDAL.MaxID }) : Json(new { Status = _userBaseReportAccessDAL.ExceptionReturn });

        }

        [HttpPost]
        public ActionResult UpdateUserBaseReportAccess(UserMstBEL mstData, List<UserProductTypeBEL> productTypeData)
        {
            return _userBaseReportAccessDAL.UpdateUserBaseReportAccess(mstData, productTypeData) ? Json(new { Message = _userBaseReportAccessDAL.UpdateMessage, Status = "Ok", Id = _userBaseReportAccessDAL.MaxID }) : Json(new { Status = _userBaseReportAccessDAL.ExceptionReturn });

        }

        [HttpGet]
        public ActionResult SearchData()
        {
            try
            {
                var data = _userBaseReportAccessDAL.SearchData();
                return Json(new { Status = _userBaseReportAccessDAL.ExceptionReturn, Data = data }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception e)
            {
                return Json(new { Status = e.Message, Data = " " }, JsonRequestBehavior.AllowGet);
            }
        }



        [HttpGet]
        public ActionResult GetUserProductType( string param)
        {
            try
            {
                var data = _userBaseReportAccessDAL.GetUserProductType(param);
                return Json(new { Status = _userBaseReportAccessDAL.ExceptionReturn, Data = data }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception e)
            {
                return Json(new { Status = e.Message, Data = " " }, JsonRequestBehavior.AllowGet);
            }
        }


        [HttpGet]
        public ActionResult GetUserProduct(string param)
        {
            try
            {
                var data = _userBaseReportAccessDAL.GetUserProduct(param);
                return Json(new { Status = _userBaseReportAccessDAL.ExceptionReturn, Data = data }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception e)
            {
                return Json(new { Status = e.Message, Data = " " }, JsonRequestBehavior.AllowGet);
            }
        }

        [HttpPost]
        public ActionResult DeleteProduct(string productDtlId)
        {
            return _userBaseReportAccessDAL.DeleteProduct(productDtlId) ? Json(new { Message = _userBaseReportAccessDAL.DeleteMessage, Status = "Ok" }) : Json(new { Status = _userBaseReportAccessDAL.ExceptionReturn });

        }


        [HttpPost]
        public ActionResult DeleteProductType(string typeId)
        {
            return _userBaseReportAccessDAL.DeleteProductType(typeId) ? Json(new { Message = _userBaseReportAccessDAL.DeleteMessage, Status = "Ok" }) : Json(new { Status = _userBaseReportAccessDAL.ExceptionReturn });

        }


        [HttpPost]
        public ActionResult DeleteUserAccess(string UserID)
        {
            return _userBaseReportAccessDAL.DeleteUserAccess(UserID) ? Json(new { Message = _userBaseReportAccessDAL.DeleteMessage, Status = "Ok" }) : Json(new { Status = _userBaseReportAccessDAL.ExceptionReturn });

        }



    }
}