using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SalesWeb.Areas.Dashboard.Models.BEL
{
    public class ImsLiftingAchieveGrowthModel
    {
        public class MtdGrowthStatusDash
        {
            public DateTime REFRESH_DATE { get; set; }
            public double CYM_TARGET_VAL { get; set; }
            public double PYM_IMS_SALES_VAL { get; set; }
            public double CYM_IMS_SALES_VAL { get; set; }
            public double ACH_PCT { get; set; }
            public double GROWTH { get; set; }
        }

        public class YtdGrowthStatusDash
        {
            public DateTime REFRESH_DATE { get; set; }
            public double TARGET_VAL { get; set; }
            public double CY_IMS_SALES_VAL { get; set; }
            public double PY_IMS_SALES_VAL { get; set; }
            public double ACH_PCT { get; set; }
            public double GROWTH { get; set; }
        }


        public class MonthlyLiftingStatusDash
        {
            public DateTime REFRESH_DATE { get; set; }
            public double Net_Lifting_TARGET_VALUE { get; set; }
            public double CYM_ACTUAL_LIFTING_VALUE { get; set; }
            public double PYM_ACTUAL_LIFTING_VALUE { get; set; }
            public double ACH_PCT { get; set; }
            public double GROWTH { get; set; }
        }
        public class YearlyLiftingStatusDash
        {
            public DateTime REFRESH_DATE { get; set; }
            public double Net_Lifting_TARGET_VALUE { get; set; }
            public double CY_ACTUAL_LIFTING_VALUE { get; set; }
            public double PY_ACTUAL_LIFTING_VALUE { get; set; }
            public double ACH_PCT { get; set; }
            public double GROWTH { get; set; }
        }

        public class DashboardGraphModel
        {
            public MtdGrowthStatusDash mtdModel { get; set; }
            public YtdGrowthStatusDash ytdModel { get; set; }
            public MonthlyLiftingStatusDash liftingMonthlyModel { get; set; }
            public YearlyLiftingStatusDash liftingYearlyModel { get; set; }
        }
    }
}