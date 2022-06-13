using System;
using System.Web.Mvc;
using System.Linq;
using SalesWeb.Universal.Gateway;
using static SalesWeb.Areas.Dashboard.Models.BEL.ImsLiftingAchieveGrowthModel;
using SalesWeb.Areas.Dashboard.Models.DAL;

namespace SalesWeb.Areas.Dashboard.Controllers
{

    [LogInChecker]

    public class ImsLiftingAchieveGrowthController : Controller
    {
        // GET: Dashboard/ImsLiftingAchieveGrowth

        ImsLiftingAchieveGrowthDAL _imsLiftingAchieveGrowthDAL = new ImsLiftingAchieveGrowthDAL();
        public ActionResult frmImsLiftingAchieveGrowth()
        {
            DashboardGraphModel model = _imsLiftingAchieveGrowthDAL.GetDashboardGraphModel();
            if (model == null)
            {
                model = new DashboardGraphModel();
                if (model.liftingMonthlyModel == null)
                {
                    model.liftingMonthlyModel = new MonthlyLiftingStatusDash();
                }
                if (model.liftingYearlyModel == null)
                {
                    model.liftingYearlyModel = new YearlyLiftingStatusDash();
                }
                if (model.mtdModel == null)
                {
                    model.mtdModel = new MtdGrowthStatusDash();
                }
                if (model.ytdModel == null)
                {
                    model.ytdModel = new YtdGrowthStatusDash();
                }
            }

            ViewBag.ReportDate = DateTime.Now.AddDays(-1).ToString("dd MMM yy");
            return View(model);
        }

    }


}