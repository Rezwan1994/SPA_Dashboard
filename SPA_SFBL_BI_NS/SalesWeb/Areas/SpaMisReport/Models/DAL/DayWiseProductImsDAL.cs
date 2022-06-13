using System;
using System.Data;
using System.Linq;
using System.Web;
using SalesWeb.Areas.SpaMisReport.Models.BEL;
using SalesWeb.Universal.Gateway;
using System.Collections.Generic;
using System.Data.OracleClient;

namespace SalesWeb.Areas.SpaMisReport.Models.DAL
{
    public class DayWiseProductImsDAL : ReturnData
    {



        private readonly DBHelper _dbHelper = new DBHelper();
        private readonly ErrorLogger _errorLogger = new ErrorLogger();
        private static readonly DBConnection DBConnection = new DBConnection();
        public readonly string ConnString = DBConnection.SAConnStrReader("Oracle", "PHARMAERP");


        //Function
        public object GetDayWiseProductIms(string fDate, string tDate, string pCode,string dCode, string rCode, string aCode, string tCode, string cCode)
        {
            try
            {

                int userId = Convert.ToInt32(HttpContext.Current.Session["USER_ID"]);

                using (OracleConnection objConn = new OracleConnection(ConnString))
                {
                    using (OracleCommand objCmd = new OracleCommand())
                    {
                        objCmd.Connection = objConn;
                        objCmd.CommandText = "FN_DAY_WISE_PRODUCT_IMS";
                        objCmd.CommandType = CommandType.StoredProcedure;
                        objCmd.Parameters.Add("p_from_date", OracleType.VarChar).Value = fDate;
                        objCmd.Parameters.Add("p_to_date", OracleType.VarChar).Value = tDate;
                        objCmd.Parameters.Add("p_product_code", OracleType.VarChar).Value = pCode;
                        objCmd.Parameters.Add("p_division_code", OracleType.VarChar).Value = dCode;
                        objCmd.Parameters.Add("p_region_code", OracleType.VarChar).Value = rCode;
                        objCmd.Parameters.Add("p_area_code", OracleType.VarChar).Value = aCode;
                        objCmd.Parameters.Add("p_territory_code", OracleType.VarChar).Value = tCode;
                        objCmd.Parameters.Add("p_customer_code", OracleType.VarChar).Value = cCode;
                        objCmd.Parameters.Add("puser_id", OracleType.VarChar).Value = userId;
                        objCmd.Parameters.Add("return_value", OracleType.Cursor).Direction = ParameterDirection.ReturnValue;
                        objConn.Open();
                        objCmd.ExecuteNonQuery();
                        OracleDataReader rdr = objCmd.ExecuteReader();
                        DataTable dt = new DataTable();
                        if (rdr.HasRows)
                        {
                            dt.Load(rdr);
                        }
                        _dbHelper.InsertReportAudit("Day Wise Product IMS(Custom Date)");
                        int count = 0;
                        List<DayWiseProductImsBEL> item;
                        item = (from DataRow row in dt.Rows
                                select new DayWiseProductImsBEL
                                {
                                    SlNo = ++count,
                                    DivisionCode = row["DIVISION_CODE"].ToString(),
                                    DivisionName = row["DIVISION_NAME"].ToString(),
                                    RegionCode = row["REGION_CODE"].ToString(),
                                    RegionName = row["REGION_NAME"].ToString(),
                                    AreaCode = row["AREA_CODE"].ToString(),
                                    AreaName = row["AREA_NAME"].ToString(),
                                    TerritoryCode = row["TERRITORY_CODE"].ToString(),
                                    TerritoryName = row["TERRITORY_NAME"].ToString(),                                    
                                    CustomerCode = row["CUSTOMER_CODE"].ToString(),
                                    CustomerName = row["CUSTOMER_NAME"].ToString(),
                                    DbLocation = row["DB_LOCATION"].ToString(),
                                    MarketCode = row["MARKET_CODE"].ToString(),
                                    MarketName = row["MARKET_NAME"].ToString(),
                                    ProductCode = row["PRODUCT_CODE"].ToString(),
                                    ProductName = row["PRODUCT_NAME"].ToString(),
                                    PackSize = row["PACK_SIZE"].ToString(),
                                    Day01 = row["DAY_01"].ToString(),
                                    Day02 = row["DAY_02"].ToString(),
                                    Day03 = row["DAY_03"].ToString(),
                                    Day04 = row["DAY_04"].ToString(),
                                    Day05 = row["DAY_05"].ToString(),
                                    Day06 = row["DAY_06"].ToString(),
                                    Day07 = row["DAY_07"].ToString(),
                                    Day08 = row["DAY_08"].ToString(),
                                    Day09 = row["DAY_09"].ToString(),
                                    Day10 = row["DAY_10"].ToString(),
                                    Day11 = row["DAY_11"].ToString(),
                                    Day12 = row["DAY_12"].ToString(),
                                    Day13 = row["DAY_13"].ToString(),
                                    Day14 = row["DAY_14"].ToString(),
                                    Day15 = row["DAY_15"].ToString(),
                                    Day16 = row["DAY_16"].ToString(),
                                    Day17 = row["DAY_17"].ToString(),
                                    Day18 = row["DAY_18"].ToString(),
                                    Day19 = row["DAY_19"].ToString(),
                                    Day20 = row["DAY_20"].ToString(),
                                    Day21 = row["DAY_21"].ToString(),
                                    Day22 = row["DAY_22"].ToString(),
                                    Day23 = row["DAY_23"].ToString(),
                                    Day24 = row["DAY_24"].ToString(),
                                    Day25 = row["DAY_25"].ToString(),
                                    Day26 = row["DAY_26"].ToString(),
                                    Day27 = row["DAY_27"].ToString(),
                                    Day28 = row["DAY_28"].ToString(),
                                    Day29 = row["DAY_29"].ToString(),
                                    Day30 = row["DAY_30"].ToString(),
                                    Day31 = row["DAY_31"].ToString(),
                                    TotalQty = row["TOTAL_QTY"].ToString(),
                                    TargetQty = row["TARGET_QTY"].ToString()

                                }).ToList();
                        return item;
                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return ExceptionReturn = "";
            }

        }



    }
}