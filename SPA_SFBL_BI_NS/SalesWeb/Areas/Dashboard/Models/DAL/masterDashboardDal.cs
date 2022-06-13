using System;
using System.Collections.Generic;
using System.Data;
using System.Data.OracleClient;
using System.Linq;
using SalesWeb.Areas.Dashboard.Models.BEL;
using SalesWeb.Universal.Gateway;
using static SalesWeb.Areas.Dashboard.Models.BEL.ImsLiftingAchieveGrowthModel;

namespace SalesWeb.Areas.Dashboard.Models.DAL
{
    public class masterDashboardDal : ReturnData
    {
        private readonly DBHelper _dbHelper = new DBHelper();
        private readonly ErrorLogger _errorLogger = new ErrorLogger();
        private static readonly DBConnection DBConnection = new DBConnection();
        public readonly string ConnString = DBConnection.SAConnStrReader("Oracle", "PHARMAERP");
        public OrderModel GetOrderCount(int UserId)
        {
            DataTable dataTable = this._dbHelper.GetDataTable(string.Format("SELECT fnc_today_order_val({0}) OrderValue FROM DUAL", (object)UserId));
            OrderModel orderModel = new OrderModel();
            return dataTable.Rows.Cast<DataRow>().Select<DataRow, OrderModel>((Func<DataRow, OrderModel>)(row => new OrderModel()
            {
                OrderValue = row["ORDERVALUE"].ToString() != "" ? Math.Round(Convert.ToDouble(row["ORDERVALUE"].ToString()) / 10000000.0, 2) : 0.0
            })).ToList<OrderModel>().FirstOrDefault<OrderModel>();
        }
        public MonthlyTargetModel GetMonthlyTargetCount(int UserId)
        {
            DataTable dataTable = this._dbHelper.GetDataTable(string.Format("SELECT FNC_MTD_TARGET_VAL({0}) TargetValue FROM DUAL", (object)UserId));
            MonthlyTargetModel monthlyTargetModel = new MonthlyTargetModel();
            return dataTable.Rows.Cast<DataRow>().Select<DataRow, MonthlyTargetModel>((Func<DataRow, MonthlyTargetModel>)(row => new MonthlyTargetModel()
            {
                TargetValue = row["TargetValue"].ToString() != "" ? Math.Round(Convert.ToDouble(row["TargetValue"].ToString()) / 10000000.0, 2) : 0.0
            })).ToList<MonthlyTargetModel>().FirstOrDefault<MonthlyTargetModel>();
        }

        public YearlyTargetModel GetYearlyTargetCount(int UserId)
        {
            DataTable dataTable = this._dbHelper.GetDataTable(string.Format("SELECT FNC_YTD_TARGET_VAL({0}) TargetValue FROM DUAL", (object)UserId));
            YearlyTargetModel yearlyTargetModel = new YearlyTargetModel();
            return dataTable.Rows.Cast<DataRow>().Select<DataRow, YearlyTargetModel>((Func<DataRow, YearlyTargetModel>)(row => new YearlyTargetModel()
            {
                TargetValue = row["TargetValue"].ToString() != "" ? Math.Round(Convert.ToDouble(row["TargetValue"].ToString()) / 10000000.0, 2) : 0.0
            })).ToList<YearlyTargetModel>().FirstOrDefault<YearlyTargetModel>();
        }

        public SalesModel GetSalesCount(int UserId)
        {
            DataTable dataTable = this._dbHelper.GetDataTable(string.Format("SELECT fnc_today_sales_val({0}) SalesValue FROM DUAL", (object)UserId));
            SalesModel salesModel = new SalesModel();
            return dataTable.Rows.Cast<DataRow>().Select<DataRow, SalesModel>((Func<DataRow, SalesModel>)(row => new SalesModel()
            {
                SalesValue = row["SalesValue"].ToString() != "" ? Math.Round(Convert.ToDouble(row["SalesValue"].ToString()) / 10000000.0, 2) : 0.0
            })).ToList<SalesModel>().FirstOrDefault<SalesModel>();
        }

        public MonthlySalesModel GetMonthlySalesCount(int UserId)
        {
            DataTable dataTable = this._dbHelper.GetDataTable(string.Format("SELECT FNC_MTD_IMS_VAL({0}) SalesValue FROM DUAL", (object)UserId));
            MonthlySalesModel monthlySalesModel = new MonthlySalesModel();
            return dataTable.Rows.Cast<DataRow>().Select<DataRow, MonthlySalesModel>((Func<DataRow, MonthlySalesModel>)(row => new MonthlySalesModel()
            {
                SalesValue = row["SalesValue"].ToString() != "" ? Math.Round(Convert.ToDouble(row["SalesValue"].ToString()) / 10000000.0, 2) : 0.0
            })).ToList<MonthlySalesModel>().FirstOrDefault<MonthlySalesModel>();
        }

        public YearlySalesModel GetYearlySalesCount(int UserId)
        {
            DataTable dataTable = this._dbHelper.GetDataTable(string.Format("SELECT FNC_YTD_IMS_VAL({0}) SalesValue FROM DUAL", (object)UserId));
            YearlySalesModel yearlySalesModel = new YearlySalesModel();
            return dataTable.Rows.Cast<DataRow>().Select<DataRow, YearlySalesModel>((Func<DataRow, YearlySalesModel>)(row => new YearlySalesModel()
            {
                SalesValue = row["SalesValue"].ToString() != "" ? Math.Round(Convert.ToDouble(row["SalesValue"].ToString()) / 10000000.0, 2) : 0.0
            })).ToList<YearlySalesModel>().FirstOrDefault<YearlySalesModel>();
        }

        public List<DayWiseModel> GetIMSValue(int UserId)
        {
            try
            {
                using (OracleConnection oracleConnection = new OracleConnection(this.ConnString))
                {
                    using (OracleCommand oracleCommand = new OracleCommand())
                    {
                        oracleCommand.Connection = oracleConnection;
                        oracleCommand.CommandText = "FN_DAY_WISE_SALES";
                        oracleCommand.CommandType = CommandType.StoredProcedure;
                        oracleCommand.Parameters.Add("pUserId", OracleType.Number).Value = (object)UserId;
                        oracleCommand.Parameters.Add("return_value", OracleType.Cursor).Direction = ParameterDirection.ReturnValue;
                        oracleConnection.Open();
                        oracleCommand.ExecuteNonQuery();
                        OracleDataReader reader = oracleCommand.ExecuteReader();
                        DataTable dataTable = new DataTable();
                        if (reader.HasRows)
                            dataTable.Load((IDataReader)reader);
                        List<DayWiseModel> dayWiseModelList = new List<DayWiseModel>();
                        return dataTable.Rows.Cast<DataRow>().Select<DataRow, DayWiseModel>((Func<DataRow, DayWiseModel>)(row => new DayWiseModel()
                        {
                            YYYYMMMM = Convert.ToInt32(row["YYYYMM"].ToString()),
                            DAY_01 = Math.Round(Convert.ToDouble(row["DAY_01"].ToString()) / 10000000.0, 2),
                            DAY_02 = Math.Round(Convert.ToDouble(row["DAY_02"].ToString()) / 10000000.0, 2),
                            DAY_03 = Math.Round(Convert.ToDouble(row["DAY_03"].ToString()) / 10000000.0, 2),
                            DAY_04 = Math.Round(Convert.ToDouble(row["DAY_04"].ToString()) / 10000000.0, 2),
                            DAY_05 = Math.Round(Convert.ToDouble(row["DAY_05"].ToString()) / 10000000.0, 2),
                            DAY_06 = Math.Round(Convert.ToDouble(row["DAY_06"].ToString()) / 10000000.0, 2),
                            DAY_07 = Math.Round(Convert.ToDouble(row["DAY_07"].ToString()) / 10000000.0, 2),
                            DAY_08 = Math.Round(Convert.ToDouble(row["DAY_08"].ToString()) / 10000000.0, 2),
                            DAY_09 = Math.Round(Convert.ToDouble(row["DAY_09"].ToString()) / 10000000.0, 2),
                            DAY_10 = Math.Round(Convert.ToDouble(row["DAY_10"].ToString()) / 10000000.0, 2),
                            DAY_11 = Math.Round(Convert.ToDouble(row["DAY_11"].ToString()) / 10000000.0, 2),
                            DAY_12 = Math.Round(Convert.ToDouble(row["DAY_12"].ToString()) / 10000000.0, 2),
                            DAY_13 = Math.Round(Convert.ToDouble(row["DAY_13"].ToString()) / 10000000.0, 2),
                            DAY_14 = Math.Round(Convert.ToDouble(row["DAY_14"].ToString()) / 10000000.0, 2),
                            DAY_15 = Math.Round(Convert.ToDouble(row["DAY_15"].ToString()) / 10000000.0, 2),
                            DAY_16 = Math.Round(Convert.ToDouble(row["DAY_16"].ToString()) / 10000000.0, 2),
                            DAY_17 = Math.Round(Convert.ToDouble(row["DAY_17"].ToString()) / 10000000.0, 2),
                            DAY_18 = Math.Round(Convert.ToDouble(row["DAY_18"].ToString()) / 10000000.0, 2),
                            DAY_19 = Math.Round(Convert.ToDouble(row["DAY_19"].ToString()) / 10000000.0, 2),
                            DAY_20 = Math.Round(Convert.ToDouble(row["DAY_20"].ToString()) / 10000000.0, 2),
                            DAY_21 = Math.Round(Convert.ToDouble(row["DAY_21"].ToString()) / 10000000.0, 2),
                            DAY_22 = Math.Round(Convert.ToDouble(row["DAY_22"].ToString()) / 10000000.0, 2),
                            DAY_23 = Math.Round(Convert.ToDouble(row["DAY_23"].ToString()) / 10000000.0, 2),
                            DAY_24 = Math.Round(Convert.ToDouble(row["DAY_24"].ToString()) / 10000000.0, 2),
                            DAY_25 = Math.Round(Convert.ToDouble(row["DAY_25"].ToString()) / 10000000.0, 2),
                            DAY_26 = Math.Round(Convert.ToDouble(row["DAY_26"].ToString()) / 10000000.0, 2),
                            DAY_27 = Math.Round(Convert.ToDouble(row["DAY_27"].ToString()) / 10000000.0, 2),
                            DAY_28 = Math.Round(Convert.ToDouble(row["DAY_28"].ToString()) / 10000000.0, 2),
                            DAY_29 = Math.Round(Convert.ToDouble(row["DAY_29"].ToString()) / 10000000.0, 2),
                            DAY_30 = Math.Round(Convert.ToDouble(row["DAY_30"].ToString()) / 10000000.0, 2),
                            DAY_31 = Math.Round(Convert.ToDouble(row["DAY_31"].ToString()) / 10000000.0, 2)
                        })).ToList<DayWiseModel>();
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine((object)ex);
                return new List<DayWiseModel>();
            }
        }
        public DayWiseModel GetIMSReturnPct(int UserId)
        {
            try
            {
                using (OracleConnection oracleConnection = new OracleConnection(this.ConnString))
                {
                    using (OracleCommand oracleCommand = new OracleCommand())
                    {
                        oracleCommand.Connection = oracleConnection;
                        oracleCommand.CommandText = "FN_DAY_WISE_RETURN_PCT";
                        oracleCommand.CommandType = CommandType.StoredProcedure;
                        oracleCommand.Parameters.Add("pUserId", OracleType.Number).Value = (object)UserId;
                        oracleCommand.Parameters.Add("return_value", OracleType.Cursor).Direction = ParameterDirection.ReturnValue;
                        oracleConnection.Open();
                        oracleCommand.ExecuteNonQuery();
                        OracleDataReader reader = oracleCommand.ExecuteReader();
                        DataTable dataTable = new DataTable();
                        if (reader.HasRows)
                            dataTable.Load((IDataReader)reader);
                        DayWiseModel dayWiseModel = new DayWiseModel();
                        return dataTable.Rows.Cast<DataRow>().Select<DataRow, DayWiseModel>((Func<DataRow, DayWiseModel>)(row => new DayWiseModel()
                        {
                            YYYYMMMM = Convert.ToInt32(row["YYYYMM"].ToString()),
                            DAY_01 = Convert.ToDouble(row["DAY_01"].ToString()),
                            DAY_02 = Convert.ToDouble(row["DAY_02"].ToString()),
                            DAY_03 = Convert.ToDouble(row["DAY_03"].ToString()),
                            DAY_04 = Convert.ToDouble(row["DAY_04"].ToString()),
                            DAY_05 = Convert.ToDouble(row["DAY_05"].ToString()),
                            DAY_06 = Convert.ToDouble(row["DAY_06"].ToString()),
                            DAY_07 = Convert.ToDouble(row["DAY_07"].ToString()),
                            DAY_08 = Convert.ToDouble(row["DAY_08"].ToString()),
                            DAY_09 = Convert.ToDouble(row["DAY_09"].ToString()),
                            DAY_10 = Convert.ToDouble(row["DAY_10"].ToString()),
                            DAY_11 = Convert.ToDouble(row["DAY_11"].ToString()),
                            DAY_12 = Convert.ToDouble(row["DAY_12"].ToString()),
                            DAY_13 = Convert.ToDouble(row["DAY_13"].ToString()),
                            DAY_14 = Convert.ToDouble(row["DAY_14"].ToString()),
                            DAY_15 = Convert.ToDouble(row["DAY_15"].ToString()),
                            DAY_16 = Convert.ToDouble(row["DAY_16"].ToString()),
                            DAY_17 = Convert.ToDouble(row["DAY_17"].ToString()),
                            DAY_18 = Convert.ToDouble(row["DAY_18"].ToString()),
                            DAY_19 = Convert.ToDouble(row["DAY_19"].ToString()),
                            DAY_20 = Convert.ToDouble(row["DAY_20"].ToString()),
                            DAY_21 = Convert.ToDouble(row["DAY_21"].ToString()),
                            DAY_22 = Convert.ToDouble(row["DAY_22"].ToString()),
                            DAY_23 = Convert.ToDouble(row["DAY_23"].ToString()),
                            DAY_24 = Convert.ToDouble(row["DAY_24"].ToString()),
                            DAY_25 = Convert.ToDouble(row["DAY_25"].ToString()),
                            DAY_26 = Convert.ToDouble(row["DAY_26"].ToString()),
                            DAY_27 = Convert.ToDouble(row["DAY_27"].ToString()),
                            DAY_28 = Convert.ToDouble(row["DAY_28"].ToString()),
                            DAY_29 = Convert.ToDouble(row["DAY_29"].ToString()),
                            DAY_30 = Convert.ToDouble(row["DAY_30"].ToString()),
                            DAY_31 = Convert.ToDouble(row["DAY_31"].ToString())
                        })).ToList<DayWiseModel>().FirstOrDefault<DayWiseModel>();
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine((object)ex);
                return new DayWiseModel();
            }
        }

        public DayWiseModel GetPCTrend(int UserId, string Year)
        {
            try
            {
                using (OracleConnection oracleConnection = new OracleConnection(this.ConnString))
                {
                    using (OracleCommand oracleCommand = new OracleCommand())
                    {
                        oracleCommand.Connection = oracleConnection;
                        oracleCommand.CommandText = "FN_DAY_WISE_PC";
                        oracleCommand.CommandType = CommandType.StoredProcedure;
                        oracleCommand.Parameters.Add("pUserId", OracleType.Number).Value = (object)UserId;
                        oracleCommand.Parameters.Add("pYearMonth", OracleType.VarChar).Value = (object)Year;
                        oracleCommand.Parameters.Add("return_value", OracleType.Cursor).Direction = ParameterDirection.ReturnValue;
                        oracleConnection.Open();
                        oracleCommand.ExecuteNonQuery();
                        OracleDataReader reader = oracleCommand.ExecuteReader();
                        DataTable dataTable = new DataTable();
                        if (reader.HasRows)
                            dataTable.Load((IDataReader)reader);
                        DayWiseModel dayWiseModel = new DayWiseModel();
                        return dataTable.Rows.Cast<DataRow>().Select<DataRow, DayWiseModel>((Func<DataRow, DayWiseModel>)(row => new DayWiseModel()
                        {
                            YYYYMMMM = Convert.ToInt32(row["YYYYMM"].ToString()),
                            DAY_01 = Convert.ToDouble(row["DAY_01"].ToString()),
                            DAY_02 = Convert.ToDouble(row["DAY_02"].ToString()),
                            DAY_03 = Convert.ToDouble(row["DAY_03"].ToString()),
                            DAY_04 = Convert.ToDouble(row["DAY_04"].ToString()),
                            DAY_05 = Convert.ToDouble(row["DAY_05"].ToString()),
                            DAY_06 = Convert.ToDouble(row["DAY_06"].ToString()),
                            DAY_07 = Convert.ToDouble(row["DAY_07"].ToString()),
                            DAY_08 = Convert.ToDouble(row["DAY_08"].ToString()),
                            DAY_09 = Convert.ToDouble(row["DAY_09"].ToString()),
                            DAY_10 = Convert.ToDouble(row["DAY_10"].ToString()),
                            DAY_11 = Convert.ToDouble(row["DAY_11"].ToString()),
                            DAY_12 = Convert.ToDouble(row["DAY_12"].ToString()),
                            DAY_13 = Convert.ToDouble(row["DAY_13"].ToString()),
                            DAY_14 = Convert.ToDouble(row["DAY_14"].ToString()),
                            DAY_15 = Convert.ToDouble(row["DAY_15"].ToString()),
                            DAY_16 = Convert.ToDouble(row["DAY_16"].ToString()),
                            DAY_17 = Convert.ToDouble(row["DAY_17"].ToString()),
                            DAY_18 = Convert.ToDouble(row["DAY_18"].ToString()),
                            DAY_19 = Convert.ToDouble(row["DAY_19"].ToString()),
                            DAY_20 = Convert.ToDouble(row["DAY_20"].ToString()),
                            DAY_21 = Convert.ToDouble(row["DAY_21"].ToString()),
                            DAY_22 = Convert.ToDouble(row["DAY_22"].ToString()),
                            DAY_23 = Convert.ToDouble(row["DAY_23"].ToString()),
                            DAY_24 = Convert.ToDouble(row["DAY_24"].ToString()),
                            DAY_25 = Convert.ToDouble(row["DAY_25"].ToString()),
                            DAY_26 = Convert.ToDouble(row["DAY_26"].ToString()),
                            DAY_27 = Convert.ToDouble(row["DAY_27"].ToString()),
                            DAY_28 = Convert.ToDouble(row["DAY_28"].ToString()),
                            DAY_29 = Convert.ToDouble(row["DAY_29"].ToString()),
                            DAY_30 = Convert.ToDouble(row["DAY_30"].ToString()),
                            DAY_31 = Convert.ToDouble(row["DAY_31"].ToString())
                        })).ToList<DayWiseModel>().FirstOrDefault<DayWiseModel>();
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine((object)ex);
                return new DayWiseModel();
            }
        }

        public List<LastFiveYearSalesModel> GetfiveYearsSaleTrend()
        {
            DataTable dataTable = this._dbHelper.GetDataTable(string.Format("SELECT * from MV_L5Y_TAR_SAL_ACH"));
            List<LastFiveYearSalesModel> fiveYearSalesModelList = new List<LastFiveYearSalesModel>();
            return dataTable.Rows.Cast<DataRow>().Select<DataRow, LastFiveYearSalesModel>((Func<DataRow, LastFiveYearSalesModel>)(row => new LastFiveYearSalesModel()
            {
                Year = row["Year"].ToString(),
                IMS_VAL = row["IMS_VAL"].ToString() != "" ? Math.Round(Convert.ToDouble(row["IMS_VAL"].ToString()) / 10000000.0, 2) : 0.0,
                ACH = row["ACH"].ToString() != "" ? Math.Round(Convert.ToDouble(row["ACH"].ToString()) / 10000000.0, 2) : 0.0,
                TARGET_VAL = row["TARGET_VAL"].ToString() != "" ? Math.Round(Convert.ToDouble(row["TARGET_VAL"].ToString()) / 10000000.0, 2) : 0.0
            })).ToList<LastFiveYearSalesModel>();
        }

        public TargetSalesModel GetTargetSalesList(int UserId, string Year)
        {
            try
            {
                using (OracleConnection oracleConnection = new OracleConnection(this.ConnString))
                {
                    using (OracleCommand oracleCommand = new OracleCommand())
                    {
                        oracleCommand.Connection = oracleConnection;
                        oracleCommand.CommandText = "FN_MONTH_WISE_TAR_SAL_ACH_CY";
                        oracleCommand.CommandType = CommandType.StoredProcedure;
                        oracleCommand.Parameters.Add("pUserId", OracleType.Number).Value = (object)UserId;
                        oracleCommand.Parameters.Add("pYear", OracleType.VarChar).Value = (object)Year;
                        oracleCommand.Parameters.Add("return_value", OracleType.Cursor).Direction = ParameterDirection.ReturnValue;
                        oracleConnection.Open();
                        oracleCommand.ExecuteNonQuery();
                        OracleDataReader reader = oracleCommand.ExecuteReader();
                        DataTable dataTable = new DataTable();
                        if (reader.HasRows)
                            dataTable.Load((IDataReader)reader);
                        TargetSalesModel targetSalesModel = new TargetSalesModel();
                        return dataTable.Rows.Cast<DataRow>().Select<DataRow, TargetSalesModel>((Func<DataRow, TargetSalesModel>)(row => new TargetSalesModel()
                        {
                            JAN_ACH = Math.Round(Convert.ToDouble(row["JAN_ACH"].ToString()), 2),
                            JAN_IMS_VAL = Math.Round(Convert.ToDouble(row["JAN_IMS_VAL"].ToString()) / 10000000.0, 2),
                            JAN_TARGET_VAL = Math.Round(Convert.ToDouble(row["JAN_TARGET_VAL"].ToString()) / 10000000.0, 2),
                            FEB_ACH = Math.Round(Convert.ToDouble(row["FEB_ACH"].ToString()), 2),
                            FEB_IMS_VAL = Math.Round(Convert.ToDouble(row["FEB_IMS_VAL"].ToString()) / 10000000.0, 2),
                            FEB_TARGET_VAL = Math.Round(Convert.ToDouble(row["FEB_TARGET_VAL"].ToString()) / 10000000.0, 2),
                            MAR_ACH = Math.Round(Convert.ToDouble(row["MAR_ACH"].ToString()), 2),
                            MAR_IMS_VAL = Math.Round(Convert.ToDouble(row["MAR_IMS_VAL"].ToString()) / 10000000.0, 2),
                            MAR_TARGET_VAL = Math.Round(Convert.ToDouble(row["MAR_TARGET_VAL"].ToString()) / 10000000.0, 2),
                            APR_ACH = Math.Round(Convert.ToDouble(row["APR_ACH"].ToString()), 2),
                            APR_IMS_VAL = Math.Round(Convert.ToDouble(row["APR_IMS_VAL"].ToString()) / 10000000.0, 2),
                            APR_TARGET_VAL = Math.Round(Convert.ToDouble(row["APR_TARGET_VAL"].ToString()) / 10000000.0, 2),
                            MAY_ACH = Math.Round(Convert.ToDouble(row["MAY_ACH"].ToString()), 2),
                            MAY_IMS_VAL = Math.Round(Convert.ToDouble(row["MAY_IMS_VAL"].ToString()) / 10000000.0, 2),
                            MAY_TARGET_VAL = Math.Round(Convert.ToDouble(row["MAY_TARGET_VAL"].ToString()) / 10000000.0, 2),
                            JUN_ACH = Math.Round(Convert.ToDouble(row["JUN_ACH"].ToString()), 2),
                            JUN_IMS_VAL = Math.Round(Convert.ToDouble(row["JUN_IMS_VAL"].ToString()) / 10000000.0, 2),
                            JUN_TARGET_VAL = Math.Round(Convert.ToDouble(row["JUN_TARGET_VAL"].ToString()) / 10000000.0, 2),
                            JUL_ACH = Math.Round(Convert.ToDouble(row["JUL_ACH"].ToString()), 2),
                            JUL_IMS_VAL = Math.Round(Convert.ToDouble(row["JUL_IMS_VAL"].ToString()) / 10000000.0, 2),
                            JUL_TARGET_VAL = Math.Round(Convert.ToDouble(row["JUL_TARGET_VAL"].ToString()) / 10000000.0, 2),
                            AUG_ACH = Math.Round(Convert.ToDouble(row["AUG_ACH"].ToString()), 2),
                            AUG_IMS_VAL = Math.Round(Convert.ToDouble(row["AUG_IMS_VAL"].ToString()) / 10000000.0, 2),
                            AUG_TARGET_VAL = Math.Round(Convert.ToDouble(row["AUG_TARGET_VAL"].ToString()) / 10000000.0, 2),
                            SEP_ACH = Math.Round(Convert.ToDouble(row["SEP_ACH"].ToString()), 2),
                            SEP_IMS_VAL = Math.Round(Convert.ToDouble(row["SEP_IMS_VAL"].ToString()) / 10000000.0, 2),
                            SEP_TARGET_VAL = Math.Round(Convert.ToDouble(row["SEP_TARGET_VAL"].ToString()) / 10000000.0, 2),
                            OCT_ACH = Math.Round(Convert.ToDouble(row["OCT_ACH"].ToString()), 2),
                            OCT_IMS_VAL = Math.Round(Convert.ToDouble(row["OCT_IMS_VAL"].ToString()) / 10000000.0, 2),
                            OCT_TARGET_VAL = Math.Round(Convert.ToDouble(row["OCT_TARGET_VAL"].ToString()) / 10000000.0, 2),
                            NOV_ACH = Math.Round(Convert.ToDouble(row["NOV_ACH"].ToString()), 2),
                            NOV_IMS_VAL = Math.Round(Convert.ToDouble(row["NOV_IMS_VAL"].ToString()) / 10000000.0, 2),
                            NOV_TARGET_VAL = Math.Round(Convert.ToDouble(row["NOV_TARGET_VAL"].ToString()) / 10000000.0, 2),
                            DEC_ACH = Math.Round(Convert.ToDouble(row["DEC_ACH"].ToString()), 2),
                            DEC_IMS_VAL = Math.Round(Convert.ToDouble(row["DEC_IMS_VAL"].ToString()) / 10000000.0, 2),
                            DEC_TARGET_VAL = Math.Round(Convert.ToDouble(row["DEC_TARGET_VAL"].ToString()) / 10000000.0, 2)
                        })).ToList<TargetSalesModel>().FirstOrDefault<TargetSalesModel>();
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine((object)ex);
                return new TargetSalesModel();
            }
        }

        public TargetSalesModel GetTargetSalesListLastYear(int UserId, string Year)
        {
            try
            {
                using (OracleConnection oracleConnection = new OracleConnection(this.ConnString))
                {
                    using (OracleCommand oracleCommand = new OracleCommand())
                    {
                        oracleCommand.Connection = oracleConnection;
                        oracleCommand.CommandText = "FN_MONTH_WISE_TAR_SAL_ACH_LY";
                        oracleCommand.CommandType = CommandType.StoredProcedure;
                        oracleCommand.Parameters.Add("pUserId", OracleType.Number).Value = (object)UserId;
                        oracleCommand.Parameters.Add("pYear", OracleType.VarChar).Value = (object)Year;
                        oracleCommand.Parameters.Add("return_value", OracleType.Cursor).Direction = ParameterDirection.ReturnValue;
                        oracleConnection.Open();
                        oracleCommand.ExecuteNonQuery();
                        OracleDataReader reader = oracleCommand.ExecuteReader();
                        DataTable dataTable = new DataTable();
                        if (reader.HasRows)
                            dataTable.Load((IDataReader)reader);
                        TargetSalesModel targetSalesModel = new TargetSalesModel();
                        return dataTable.Rows.Cast<DataRow>().Select<DataRow, TargetSalesModel>((Func<DataRow, TargetSalesModel>)(row => new TargetSalesModel()
                        {
                            JAN_ACH = Math.Round(Convert.ToDouble(row["JAN_ACH"].ToString()), 2),
                            JAN_IMS_VAL = Math.Round(Convert.ToDouble(row["JAN_IMS_VAL"].ToString()) / 10000000.0, 2),
                            JAN_TARGET_VAL = Math.Round(Convert.ToDouble(row["JAN_TARGET_VAL"].ToString()) / 10000000.0, 2),
                            FEB_ACH = Math.Round(Convert.ToDouble(row["FEB_ACH"].ToString()), 2),
                            FEB_IMS_VAL = Math.Round(Convert.ToDouble(row["FEB_IMS_VAL"].ToString()) / 10000000.0, 2),
                            FEB_TARGET_VAL = Math.Round(Convert.ToDouble(row["FEB_TARGET_VAL"].ToString()) / 10000000.0, 2),
                            MAR_ACH = Math.Round(Convert.ToDouble(row["MAR_ACH"].ToString()), 2),
                            MAR_IMS_VAL = Math.Round(Convert.ToDouble(row["MAR_IMS_VAL"].ToString()) / 10000000.0, 2),
                            MAR_TARGET_VAL = Math.Round(Convert.ToDouble(row["MAR_TARGET_VAL"].ToString()) / 10000000.0, 2),
                            APR_ACH = Math.Round(Convert.ToDouble(row["APR_ACH"].ToString()), 2),
                            APR_IMS_VAL = Math.Round(Convert.ToDouble(row["APR_IMS_VAL"].ToString()) / 10000000.0, 2),
                            APR_TARGET_VAL = Math.Round(Convert.ToDouble(row["APR_TARGET_VAL"].ToString()) / 10000000.0, 2),
                            MAY_ACH = Math.Round(Convert.ToDouble(row["MAY_ACH"].ToString()), 2),
                            MAY_IMS_VAL = Math.Round(Convert.ToDouble(row["MAY_IMS_VAL"].ToString()) / 10000000.0, 2),
                            MAY_TARGET_VAL = Math.Round(Convert.ToDouble(row["MAY_TARGET_VAL"].ToString()) / 10000000.0, 2),
                            JUN_ACH = Math.Round(Convert.ToDouble(row["JUN_ACH"].ToString()), 2),
                            JUN_IMS_VAL = Math.Round(Convert.ToDouble(row["JUN_IMS_VAL"].ToString()) / 10000000.0, 2),
                            JUN_TARGET_VAL = Math.Round(Convert.ToDouble(row["JUN_TARGET_VAL"].ToString()) / 10000000.0, 2),
                            JUL_ACH = Math.Round(Convert.ToDouble(row["JUL_ACH"].ToString()), 2),
                            JUL_IMS_VAL = Math.Round(Convert.ToDouble(row["JUL_IMS_VAL"].ToString()) / 10000000.0, 2),
                            JUL_TARGET_VAL = Math.Round(Convert.ToDouble(row["JUL_TARGET_VAL"].ToString()) / 10000000.0, 2),
                            AUG_ACH = Math.Round(Convert.ToDouble(row["AUG_ACH"].ToString()), 2),
                            AUG_IMS_VAL = Math.Round(Convert.ToDouble(row["AUG_IMS_VAL"].ToString()) / 10000000.0, 2),
                            AUG_TARGET_VAL = Math.Round(Convert.ToDouble(row["AUG_TARGET_VAL"].ToString()) / 10000000.0, 2),
                            SEP_ACH = Math.Round(Convert.ToDouble(row["SEP_ACH"].ToString()), 2),
                            SEP_IMS_VAL = Math.Round(Convert.ToDouble(row["SEP_IMS_VAL"].ToString()) / 10000000.0, 2),
                            SEP_TARGET_VAL = Math.Round(Convert.ToDouble(row["SEP_TARGET_VAL"].ToString()) / 10000000.0, 2),
                            OCT_ACH = Math.Round(Convert.ToDouble(row["OCT_ACH"].ToString()), 2),
                            OCT_IMS_VAL = Math.Round(Convert.ToDouble(row["OCT_IMS_VAL"].ToString()) / 10000000.0, 2),
                            OCT_TARGET_VAL = Math.Round(Convert.ToDouble(row["OCT_TARGET_VAL"].ToString()) / 10000000.0, 2),
                            NOV_ACH = Math.Round(Convert.ToDouble(row["NOV_ACH"].ToString()), 2),
                            NOV_IMS_VAL = Math.Round(Convert.ToDouble(row["NOV_IMS_VAL"].ToString()) / 10000000.0, 2),
                            NOV_TARGET_VAL = Math.Round(Convert.ToDouble(row["NOV_TARGET_VAL"].ToString()) / 10000000.0, 2),
                            DEC_ACH = Math.Round(Convert.ToDouble(row["DEC_ACH"].ToString()), 2),
                            DEC_IMS_VAL = Math.Round(Convert.ToDouble(row["DEC_IMS_VAL"].ToString()) / 10000000.0, 2),
                            DEC_TARGET_VAL = Math.Round(Convert.ToDouble(row["DEC_TARGET_VAL"].ToString()) / 10000000.0, 2)
                        })).ToList<TargetSalesModel>().FirstOrDefault<TargetSalesModel>();
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine((object)ex);
                return new TargetSalesModel();
            }
        }

        public List<DayWiseModel> GetPhysicalAndTransitStockList(
          int UserId,
          string Year)
        {
            try
            {
                using (OracleConnection oracleConnection = new OracleConnection(this.ConnString))
                {
                    using (OracleCommand oracleCommand = new OracleCommand())
                    {
                        oracleCommand.Connection = oracleConnection;
                        oracleCommand.CommandText = "FN_DATE_WISE_STOCK_VALUE";
                        oracleCommand.CommandType = CommandType.StoredProcedure;
                        oracleCommand.Parameters.Add("pUserId", OracleType.Number).Value = (object)UserId;
                        oracleCommand.Parameters.Add("pYearMonth", OracleType.VarChar).Value = (object)Year;
                        oracleCommand.Parameters.Add("return_value", OracleType.Cursor).Direction = ParameterDirection.ReturnValue;
                        oracleConnection.Open();
                        oracleCommand.ExecuteNonQuery();
                        OracleDataReader reader = oracleCommand.ExecuteReader();
                        DataTable dataTable = new DataTable();
                        if (reader.HasRows)
                            dataTable.Load((IDataReader)reader);
                        List<DayWiseModel> dayWiseModelList = new List<DayWiseModel>();
                        return dataTable.Rows.Cast<DataRow>().Select<DataRow, DayWiseModel>((Func<DataRow, DayWiseModel>)(row => new DayWiseModel()
                        {
                            YYYYMMMM = Convert.ToInt32(row["YYYYMM"].ToString()),
                            DAY_01 = Math.Round(Convert.ToDouble(row["DAY_01"].ToString()) / 10000000.0, 2),
                            DAY_02 = Math.Round(Convert.ToDouble(row["DAY_02"].ToString()) / 10000000.0, 2),
                            DAY_03 = Math.Round(Convert.ToDouble(row["DAY_03"].ToString()) / 10000000.0, 2),
                            DAY_04 = Math.Round(Convert.ToDouble(row["DAY_04"].ToString()) / 10000000.0, 2),
                            DAY_05 = Math.Round(Convert.ToDouble(row["DAY_05"].ToString()) / 10000000.0, 2),
                            DAY_06 = Math.Round(Convert.ToDouble(row["DAY_06"].ToString()) / 10000000.0, 2),
                            DAY_07 = Math.Round(Convert.ToDouble(row["DAY_07"].ToString()) / 10000000.0, 2),
                            DAY_08 = Math.Round(Convert.ToDouble(row["DAY_08"].ToString()) / 10000000.0, 2),
                            DAY_09 = Math.Round(Convert.ToDouble(row["DAY_09"].ToString()) / 10000000.0, 2),
                            DAY_10 = Math.Round(Convert.ToDouble(row["DAY_10"].ToString()) / 10000000.0, 2),
                            DAY_11 = Math.Round(Convert.ToDouble(row["DAY_11"].ToString()) / 10000000.0, 2),
                            DAY_12 = Math.Round(Convert.ToDouble(row["DAY_12"].ToString()) / 10000000.0, 2),
                            DAY_13 = Math.Round(Convert.ToDouble(row["DAY_13"].ToString()) / 10000000.0, 2),
                            DAY_14 = Math.Round(Convert.ToDouble(row["DAY_14"].ToString()) / 10000000.0, 2),
                            DAY_15 = Math.Round(Convert.ToDouble(row["DAY_15"].ToString()) / 10000000.0, 2),
                            DAY_16 = Math.Round(Convert.ToDouble(row["DAY_16"].ToString()) / 10000000.0, 2),
                            DAY_17 = Math.Round(Convert.ToDouble(row["DAY_17"].ToString()) / 10000000.0, 2),
                            DAY_18 = Math.Round(Convert.ToDouble(row["DAY_18"].ToString()) / 10000000.0, 2),
                            DAY_19 = Math.Round(Convert.ToDouble(row["DAY_19"].ToString()) / 10000000.0, 2),
                            DAY_20 = Math.Round(Convert.ToDouble(row["DAY_20"].ToString()) / 10000000.0, 2),
                            DAY_21 = Math.Round(Convert.ToDouble(row["DAY_21"].ToString()) / 10000000.0, 2),
                            DAY_22 = Math.Round(Convert.ToDouble(row["DAY_22"].ToString()) / 10000000.0, 2),
                            DAY_23 = Math.Round(Convert.ToDouble(row["DAY_23"].ToString()) / 10000000.0, 2),
                            DAY_24 = Math.Round(Convert.ToDouble(row["DAY_24"].ToString()) / 10000000.0, 2),
                            DAY_25 = Math.Round(Convert.ToDouble(row["DAY_25"].ToString()) / 10000000.0, 2),
                            DAY_26 = Math.Round(Convert.ToDouble(row["DAY_26"].ToString()) / 10000000.0, 2),
                            DAY_27 = Math.Round(Convert.ToDouble(row["DAY_27"].ToString()) / 10000000.0, 2),
                            DAY_28 = Math.Round(Convert.ToDouble(row["DAY_28"].ToString()) / 10000000.0, 2),
                            DAY_29 = Math.Round(Convert.ToDouble(row["DAY_29"].ToString()) / 10000000.0, 2),
                            DAY_30 = Math.Round(Convert.ToDouble(row["DAY_30"].ToString()) / 10000000.0, 2),
                            DAY_31 = Math.Round(Convert.ToDouble(row["DAY_31"].ToString()) / 10000000.0, 2)
                        })).ToList<DayWiseModel>();
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine((object)ex);
                return new List<DayWiseModel>();
            }
        }

        public List<DayWiseModel> GetIMSVolumeList(int UserId)
        {
            try
            {
                using (OracleConnection oracleConnection = new OracleConnection(this.ConnString))
                {
                    using (OracleCommand oracleCommand = new OracleCommand())
                    {
                        oracleCommand.Connection = oracleConnection;
                        oracleCommand.CommandText = "FN_NATIONAL_DAY_WISE_IMS_VOL";
                        oracleCommand.CommandType = CommandType.StoredProcedure;
                        oracleCommand.Parameters.Add("pUserId", OracleType.Number).Value = (object)UserId;
                        oracleCommand.Parameters.Add("return_value", OracleType.Cursor).Direction = ParameterDirection.ReturnValue;
                        oracleConnection.Open();
                        oracleCommand.ExecuteNonQuery();
                        OracleDataReader reader = oracleCommand.ExecuteReader();
                        DataTable dataTable = new DataTable();
                        if (reader.HasRows)
                            dataTable.Load((IDataReader)reader);
                        List<DayWiseModel> dayWiseModelList = new List<DayWiseModel>();
                        return dataTable.Rows.Cast<DataRow>().Select<DataRow, DayWiseModel>((Func<DataRow, DayWiseModel>)(row => new DayWiseModel()
                        {
                            YYYYMMMM = Convert.ToInt32(row["YYYYMM"].ToString()),
                            DAY_01 = Math.Round(Convert.ToDouble(row["DAY_01"].ToString()), 2),
                            DAY_02 = Math.Round(Convert.ToDouble(row["DAY_02"].ToString()), 2),
                            DAY_03 = Math.Round(Convert.ToDouble(row["DAY_03"].ToString()), 2),
                            DAY_04 = Math.Round(Convert.ToDouble(row["DAY_04"].ToString()), 2),
                            DAY_05 = Math.Round(Convert.ToDouble(row["DAY_05"].ToString()), 2),
                            DAY_06 = Math.Round(Convert.ToDouble(row["DAY_06"].ToString()), 2),
                            DAY_07 = Math.Round(Convert.ToDouble(row["DAY_07"].ToString()), 2),
                            DAY_08 = Math.Round(Convert.ToDouble(row["DAY_08"].ToString()), 2),
                            DAY_09 = Math.Round(Convert.ToDouble(row["DAY_09"].ToString()), 2),
                            DAY_10 = Math.Round(Convert.ToDouble(row["DAY_10"].ToString()), 2),
                            DAY_11 = Math.Round(Convert.ToDouble(row["DAY_11"].ToString()), 2),
                            DAY_12 = Math.Round(Convert.ToDouble(row["DAY_12"].ToString()), 2),
                            DAY_13 = Math.Round(Convert.ToDouble(row["DAY_13"].ToString()), 2),
                            DAY_14 = Math.Round(Convert.ToDouble(row["DAY_14"].ToString()), 2),
                            DAY_15 = Math.Round(Convert.ToDouble(row["DAY_15"].ToString()), 2),
                            DAY_16 = Math.Round(Convert.ToDouble(row["DAY_16"].ToString()), 2),
                            DAY_17 = Math.Round(Convert.ToDouble(row["DAY_17"].ToString()), 2),
                            DAY_18 = Math.Round(Convert.ToDouble(row["DAY_18"].ToString()), 2),
                            DAY_19 = Math.Round(Convert.ToDouble(row["DAY_19"].ToString()), 2),
                            DAY_20 = Math.Round(Convert.ToDouble(row["DAY_20"].ToString()), 2),
                            DAY_21 = Math.Round(Convert.ToDouble(row["DAY_21"].ToString()), 2),
                            DAY_22 = Math.Round(Convert.ToDouble(row["DAY_22"].ToString()), 2),
                            DAY_23 = Math.Round(Convert.ToDouble(row["DAY_23"].ToString()), 2),
                            DAY_24 = Math.Round(Convert.ToDouble(row["DAY_24"].ToString()), 2),
                            DAY_25 = Math.Round(Convert.ToDouble(row["DAY_25"].ToString()), 2),
                            DAY_26 = Math.Round(Convert.ToDouble(row["DAY_26"].ToString()), 2),
                            DAY_27 = Math.Round(Convert.ToDouble(row["DAY_27"].ToString()), 2),
                            DAY_28 = Math.Round(Convert.ToDouble(row["DAY_28"].ToString()), 2),
                            DAY_29 = Math.Round(Convert.ToDouble(row["DAY_29"].ToString()), 2),
                            DAY_30 = Math.Round(Convert.ToDouble(row["DAY_30"].ToString()), 2),
                            DAY_31 = Math.Round(Convert.ToDouble(row["DAY_31"].ToString()), 2)
                        })).ToList<DayWiseModel>();
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine((object)ex);
                return new List<DayWiseModel>();
            }
        }

        public List<SalesWiseModel> GetProductWiseSales()
        {
            DataTable dataTable = this._dbHelper.GetDataTable(string.Format("select * from MV_PRODUCT_WISE_SALES"));
            List<SalesWiseModel> salesWiseModelList = new List<SalesWiseModel>();
            return dataTable.Rows.Cast<DataRow>().Select<DataRow, SalesWiseModel>((Func<DataRow, SalesWiseModel>)(row => new SalesWiseModel()
            {
                PRODUCT_CODE = row["PRODUCT_CODE"].ToString(),
                PRODUCT_NAME = row["PRODUCT_NAME"].ToString(),
                IMS_VALUE = row["IMS_VALUE"].ToString() != "" ? Convert.ToDouble(row["IMS_VALUE"].ToString()) : 0.0,
                RETURN_VALUE = row["RETURN_VALUE"].ToString() != "" ? Convert.ToDouble(row["RETURN_VALUE"].ToString()) : 0.0,
                SALES_VALUE = row["SALES_VALUE"].ToString() != "" ? Convert.ToDouble(row["SALES_VALUE"].ToString()) : 0.0,
                PCT_OF_TOTAL_IMS_VALUE = row["PCT_OF_TOTAL_IMS_VALUE"].ToString() != "" ? Convert.ToDouble(row["PCT_OF_TOTAL_IMS_VALUE"].ToString()) : 0.0,
                IMS_VALUE_CORE = row["IMS_VALUE_CORE"].ToString() != "" ? Convert.ToDouble(row["IMS_VALUE_CORE"].ToString()) : 0.0
            })).ToList<SalesWiseModel>();
        }

        public List<SalesWiseModel> GetCategoryWiseSales()
        {
            DataTable dataTable = this._dbHelper.GetDataTable(string.Format("select * from MV_Category_WISE_SALES"));
            List<SalesWiseModel> salesWiseModelList = new List<SalesWiseModel>();
            return dataTable.Rows.Cast<DataRow>().Select<DataRow, SalesWiseModel>((Func<DataRow, SalesWiseModel>)(row => new SalesWiseModel()
            {
                CATEGORY_CODE = row["CATEGORY_CODE"].ToString(),
                CATEGORY_NAME = row["CATEGORY_NAME"].ToString(),
                IMS_VALUE = row["IMS_VALUE"].ToString() != "" ? Convert.ToDouble(row["IMS_VALUE"].ToString()) : 0.0,
                RETURN_VALUE = row["RETURN_VALUE"].ToString() != "" ? Convert.ToDouble(row["RETURN_VALUE"].ToString()) : 0.0,
                SALES_VALUE = row["SALES_VALUE"].ToString() != "" ? Convert.ToDouble(row["SALES_VALUE"].ToString()) : 0.0,
                PCT_OF_TOTAL_IMS_VALUE = row["PCT_OF_TOTAL_IMS_VALUE"].ToString() != "" ? Convert.ToDouble(row["PCT_OF_TOTAL_IMS_VALUE"].ToString()) : 0.0,
                IMS_VALUE_CORE = row["IMS_VALUE_CORE"].ToString() != "" ? Convert.ToDouble(row["IMS_VALUE_CORE"].ToString()) : 0.0
            })).ToList<SalesWiseModel>();
        }
        public List<SalesWiseModel> GetBrandWiseSales()
        {
            DataTable dataTable = this._dbHelper.GetDataTable(string.Format("select * from MV_Brand_WISE_SALES"));
            List<SalesWiseModel> salesWiseModelList = new List<SalesWiseModel>();
            return dataTable.Rows.Cast<DataRow>().Select<DataRow, SalesWiseModel>((Func<DataRow, SalesWiseModel>)(row => new SalesWiseModel()
            {
                BRAND_CODE = row["BRAND_CODE"].ToString(),
                BRAND_NAME = row["BRAND_NAME"].ToString(),
                IMS_VALUE = row["IMS_VALUE"].ToString() != "" ? Convert.ToDouble(row["IMS_VALUE"].ToString()) : 0.0,
                RETURN_VALUE = row["RETURN_VALUE"].ToString() != "" ? Convert.ToDouble(row["RETURN_VALUE"].ToString()) : 0.0,
                SALES_VALUE = row["SALES_VALUE"].ToString() != "" ? Convert.ToDouble(row["SALES_VALUE"].ToString()) : 0.0,
                PCT_OF_TOTAL_IMS_VALUE = row["PCT_OF_TOTAL_IMS_VALUE"].ToString() != "" ? Convert.ToDouble(row["PCT_OF_TOTAL_IMS_VALUE"].ToString()) : 0.0,
                IMS_VALUE_CORE = row["IMS_VALUE_CORE"].ToString() != "" ? Convert.ToDouble(row["IMS_VALUE_CORE"].ToString()) : 0.0
            })).ToList<SalesWiseModel>();
        }
    }
}