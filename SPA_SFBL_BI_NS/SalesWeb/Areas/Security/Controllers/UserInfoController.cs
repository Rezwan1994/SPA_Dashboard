using System;
using System.Web.Mvc;
using SalesWeb.Areas.Security.Models.BEL;
using SalesWeb.Areas.Security.Models.DAL;
using SalesWeb.Universal.Gateway;

namespace SalesWeb.Areas.Security.Controllers
{
    [LogInChecker]
    public class UserInfoController : Controller
    {
        private readonly UserInfoDAL _userInfoDAL = new UserInfoDAL();
        public ActionResult frmUserInfo()
        {
            return View();
        }


        [HttpPost]
        public ActionResult InsertUserInfo(UserInfoBEL master)
        {
            try
            {
                if (_userInfoDAL.InsertUserInfo(master))
                {
                    return Json(new { Message = _userInfoDAL.InsertMessage, Status = "Ok", ID = _userInfoDAL.MaxID });
                }
                else
                {
                    return Json(new { Status = _userInfoDAL.ExceptionReturn });
                }
            }
            catch (Exception e)
            {
                return Json(new { Status = e.Message });
            }
        }
        [HttpPost]
        public ActionResult UpdateUserInfo(UserInfoBEL master)
        {
            try
            {
                if (_userInfoDAL.UpdateUserInfo(master))
                {
                    return Json(new { Message = _userInfoDAL.UpdateMessage, Status = "Ok", ID = _userInfoDAL.MaxID });
                }
                else
                {
                    return Json(new { Status = _userInfoDAL.ExceptionReturn });
                }

            }
            catch (Exception e)
            {
                return Json(new { Status = e.Message });
            }
        }



        [HttpPost]
        public ActionResult GetDepotList()
        {
            try
            {
                var data = _userInfoDAL.GetDepotList();
                return Json(new { Status = _userInfoDAL.ExceptionReturn, Data = data }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception e)
            {
                return Json(new { Status = e.Message, Data = " " }, JsonRequestBehavior.AllowGet);
            }
        }

  

        [HttpPost]
        public ActionResult GetUserList()
        {
            var listData = _userInfoDAL.GetUserList();
            if (listData != null && _userInfoDAL.ExceptionReturn == null)
            {
                var data = Json(listData, JsonRequestBehavior.AllowGet);
                data.MaxJsonLength = int.MaxValue;
                return data;
            }
            return Json(new { Status = _userInfoDAL.ExceptionReturn }, JsonRequestBehavior.AllowGet);
        }


        [HttpPost]
        public ActionResult GetEmployeeList(string param)
        {
            try
            {
                var data = _userInfoDAL.GetEmployeeList(param);
                return Json(new { Status = _userInfoDAL.ExceptionReturn, Data = data }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception e)
            {
                return Json(new { Status = e.Message, Data = " " }, JsonRequestBehavior.AllowGet);
            }
        }






        //[HttpPost]
        //public ActionResult GetZoneList()
        //{
        //    try
        //    {
        //        var data = _userInfoDAL.GetZoneList();
        //        return Json(new { Status = _userInfoDAL.ExceptionReturn, Data = data }, JsonRequestBehavior.AllowGet);
        //    }
        //    catch (Exception e)
        //    {
        //        return Json(new { Status = e.Message, Data = " " }, JsonRequestBehavior.AllowGet);
        //    }
        //}

        //[HttpPost]
        //public ActionResult GetRegionList()
        //{
        //    try
        //    {
        //        var data = _userInfoDAL.GetRegionList();
        //        return Json(new { Status = _userInfoDAL.ExceptionReturn, Data = data }, JsonRequestBehavior.AllowGet);
        //    }
        //    catch (Exception e)
        //    {
        //        return Json(new { Status = e.Message, Data = " " }, JsonRequestBehavior.AllowGet);
        //    }
        //}

        //[HttpPost]
        //public ActionResult GetAreaList()
        //{
        //    try
        //    {
        //        var data = _userInfoDAL.GetAreaList();
        //        return Json(new { Status = _userInfoDAL.ExceptionReturn, Data = data }, JsonRequestBehavior.AllowGet);
        //    }
        //    catch (Exception e)
        //    {
        //        return Json(new { Status = e.Message, Data = " " }, JsonRequestBehavior.AllowGet);
        //    }
        //}
        //[HttpPost]
        //public ActionResult GetTerritoryList()
        //{
        //    try
        //    {
        //        var data = _userInfoDAL.GetTerritoryList();
        //        return Json(new { Status = _userInfoDAL.ExceptionReturn, Data = data }, JsonRequestBehavior.AllowGet);
        //    }
        //    catch (Exception e)
        //    {
        //        return Json(new { Status = e.Message, Data = " " }, JsonRequestBehavior.AllowGet);
        //    }
        //}




    }
}