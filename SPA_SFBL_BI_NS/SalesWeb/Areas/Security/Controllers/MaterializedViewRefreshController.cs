using System;
using System.Web.Mvc;
using SalesWeb.Areas.Security.Models.BEL;
using SalesWeb.Areas.Security.Models.DAL;
using SalesWeb.Universal.Gateway;

namespace SalesWeb.Areas.Security.Controllers
{
    [LogInChecker]
    public class MaterializedViewRefreshController : Controller
    {
        MaterializedViewRefreshDAL _materializedViewRefreshDAL = new MaterializedViewRefreshDAL();
        public ActionResult frmMaterializedViewRefresh()
        {
            return View();
        }

        [HttpPost]
        public ActionResult GetMaterializedView()
        {
            var listData = _materializedViewRefreshDAL.GetMaterializedView();
            var data = Json(listData, JsonRequestBehavior.AllowGet);
            data.MaxJsonLength = int.MaxValue;
            return data;

        }


        [HttpPost]
        public ActionResult RefreshMaterializedView(string JobName)
        {
            return _materializedViewRefreshDAL.RefreshMaterializedView(JobName) ? Json(new { Message = _materializedViewRefreshDAL.RefreshMessage }) : Json(new { Status = _materializedViewRefreshDAL.ExceptionReturn });

        }
    }
}