using System;
using System.Web.Mvc;
using SalesWeb.Areas.SpaMisReport.Models.BEL;
using SalesWeb.Areas.SpaMisReport.Models.DAL;
using SalesWeb.Universal.Gateway;


namespace SalesWeb.Areas.SpaMisReport.Controllers
{
    [LogInChecker]
    public class FsoWiseFirstAndLastInvoiceController : Controller
    {
        FsoWiseFirstAndLastInvoiceDAL _fsoWiseFirstAndLastInvoiceDAL = new FsoWiseFirstAndLastInvoiceDAL();
        public ActionResult frmFsoWiseFirstAndLastInvoice()
        {
            return View();
        }


        public ActionResult GetFsoWiseFirstAndLastInvoiceRel()
        {
            var listData = _fsoWiseFirstAndLastInvoiceDAL.GetFsoWiseFirstAndLastInvoiceRel();
            var data = Json(listData, JsonRequestBehavior.AllowGet);
            data.MaxJsonLength = int.MaxValue;
            return data;

        }


        public ActionResult GetFsoWiseFirstAndLastInvoiceAll()
        {
            var listData = _fsoWiseFirstAndLastInvoiceDAL.GetFsoWiseFirstAndLastInvoiceAll();
            var data = Json(listData, JsonRequestBehavior.AllowGet);
            data.MaxJsonLength = int.MaxValue;
            return data;

        }

    }
}