using System;
using System.Data;
using System.Linq;
using SalesWeb.Universal.Gateway;
using static SalesWeb.Areas.Dashboard.Models.BEL.ImsLiftingAchieveGrowthModel;

namespace SalesWeb.Areas.Dashboard.Models.DAL
{
    public class ImsLiftingAchieveGrowthDAL : ReturnData
    {
        private readonly DBHelper _dbHelper = new DBHelper();
        private readonly ErrorLogger _errorLogger = new ErrorLogger();
        private static readonly DBConnection DBConnection = new DBConnection();
        public readonly string ConnString = DBConnection.SAConnStrReader("Oracle", "PHARMAERP");
        public DashboardGraphModel GetDashboardGraphModel()
        {
            DashboardGraphModel model = new DashboardGraphModel();
            MtdGrowthStatusDash mtdModel = new MtdGrowthStatusDash();
            YtdGrowthStatusDash ytdModel = new YtdGrowthStatusDash();
            try
            {
                var rowQry = "select * from MV_MTD_IMS_ACV_GROWTH_DASH";
                var dt = _dbHelper.GetDataTable(rowQry);

                model.mtdModel = (from DataRow row in dt.Rows
                                  select new MtdGrowthStatusDash
                                  {
                                      REFRESH_DATE = Convert.ToDateTime(row["REFRESH_DATE"]),
                                      CYM_IMS_SALES_VAL = Convert.ToDouble(row["CYM_IMS_SALES_VAL"]),
                                      PYM_IMS_SALES_VAL = Convert.ToDouble(row["PYM_IMS_SALES_VAL"]),
                                      ACH_PCT = Convert.ToDouble(row["ACH_PCT"]),
                                      CYM_TARGET_VAL = Convert.ToDouble(row["CYM_TARGET_VAL"]),
                                      GROWTH = Convert.ToDouble(row["GROWTH"])
                                  }).ToList().FirstOrDefault();

                var rowQry1 = "select * from MV_YTD_IMS_ACV_GROWTH_DASH";
                var dt1 = _dbHelper.GetDataTable(rowQry1);

                model.ytdModel = (from DataRow row in dt1.Rows
                                  select new YtdGrowthStatusDash
                                  {
                                      REFRESH_DATE = Convert.ToDateTime(row["REFRESH_DATE"]),
                                      CY_IMS_SALES_VAL = Convert.ToDouble(row["CY_IMS_SALES_VAL"]),
                                      PY_IMS_SALES_VAL = Convert.ToDouble(row["PY_IMS_SALES_VAL"]),
                                      ACH_PCT = Convert.ToDouble(row["ACH_PCT"]),
                                      TARGET_VAL = Convert.ToDouble(row["TARGET_VAL"]),
                                      GROWTH = Convert.ToDouble(row["GROWTH"])
                                  }).ToList().FirstOrDefault();


                var rowQry2 = @"select * from MV_MTD_LIFTING_ACV_GROWTH_DASH";
                var dt2 = _dbHelper.GetDataTable(rowQry2);

                model.liftingMonthlyModel = (from DataRow row in dt2.Rows
                                             select new MonthlyLiftingStatusDash
                                             {
                                                 REFRESH_DATE = Convert.ToDateTime(row["REFRESH_DATE"]),
                                                 CYM_ACTUAL_LIFTING_VALUE = Convert.ToDouble(row["CYM_ACTUAL_LIFTING_VALUE"]),
                                                 PYM_ACTUAL_LIFTING_VALUE = Convert.ToDouble(row["PYM_ACTUAL_LIFTING_VALUE"]),
                                                 ACH_PCT = Convert.ToDouble(row["ACH_PCT"]),
                                                 Net_Lifting_TARGET_VALUE = Convert.ToDouble(row["Net_Lifting_TARGET_VALUE"]),
                                                 GROWTH = Convert.ToDouble(row["GROWTH"])
                                             }).ToList().FirstOrDefault();


                var rowQry3 = @"select * from MV_YTD_LIFTING_ACV_GROWTH_DASH";
                //var dt3 = _dbHelper.GetDataTable(rowQry3);
                var dt3 = _dbHelper.GetDataTableWithAuditTrial(rowQry3, "IMS & Lifting - Dashboard");

                model.liftingYearlyModel = (from DataRow row in dt3.Rows
                                            select new YearlyLiftingStatusDash
                                            {
                                                REFRESH_DATE = Convert.ToDateTime(row["REFRESH_DATE"]),
                                                CY_ACTUAL_LIFTING_VALUE = Convert.ToDouble(row["CY_ACTUAL_LIFTING_VALUE"]),
                                                PY_ACTUAL_LIFTING_VALUE = Convert.ToDouble(row["PY_ACTUAL_LIFTING_VALUE"]),
                                                ACH_PCT = Convert.ToDouble(row["ACH_PCT"]),
                                                Net_Lifting_TARGET_VALUE = Convert.ToDouble(row["Net_Lifting_TARGET_VALUE"]),
                                                GROWTH = Convert.ToDouble(row["GROWTH"])
                                            }).ToList().FirstOrDefault();
            }
            catch (Exception e)
            {
                var lineNum = e.StackTrace.Substring(e.StackTrace.LastIndexOf(' '));
                _errorLogger.GetErrorMessage(e.Message, "CatNumericSalesAnalysisDAL", lineNum);
                ExceptionReturn = e.Message;
                throw;
            }


            return model;
        }
    }
}