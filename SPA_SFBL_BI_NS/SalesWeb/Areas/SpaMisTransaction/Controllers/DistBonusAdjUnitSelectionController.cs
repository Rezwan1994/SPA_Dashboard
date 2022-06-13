using System;
using System.Web.Mvc;
using SalesWeb.Areas.SpaMisTransaction.Models.BEL;
using SalesWeb.Areas.SpaMisTransaction.Models.DAL;
using SalesWeb.Universal.Gateway;

namespace SalesWeb.Areas.SpaMisTransaction.Controllers
{
    [LogInChecker]
    public class DistBonusAdjUnitSelectionController : Controller
    {
        DistBonusProcessRunDAL _distBonusProcessRunDAL = new DistBonusProcessRunDAL();
        public ActionResult frmDistBonusAdjUnitSelection()
        {
            string CurentDate = DateTime.Now.ToString("dd/MM/yyyy");
            ViewBag.CurentDate = CurentDate;
            return View();
        }
    }
}