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
    public class ComboBonusValDAL : ReturnData
    {

        private readonly DBHelper _dbHelper = new DBHelper();
        private readonly ErrorLogger _errorLogger = new ErrorLogger();
        private static readonly DBConnection DBConnection = new DBConnection();
        public readonly string ConnString = DBConnection.SAConnStrReader("Oracle", "PHARMAERP");


        public object GetComboBonusList()
        {
            try
            {
                var qry = " SELECT SPECIAL_BONUS_NAME COMBO_BONUS_NAME," +
                          " SPEC_BONUS_MST_SLNO COMBO_BONUS_NO," +
                          " DECODE(STATUS,'A','Active','I','Inactive') STATUS," +
                          " TO_CHAR(BONUS_EFFECT_FROM,'DD/Mon/RR') ||' - '|| TO_CHAR(BONUS_EFFECT_TO,'DD/Mon/RR') DURATION" +
                          " FROM SPA_SFBL.SPECIAL_BONUS_MST@DL_SPASFBL.SQUAREGROUP.COM  " +
                          " WHERE 1=1 AND STATUS='A' " +
                          " ORDER BY SPEC_BONUS_MST_SLNO DESC";
                DataTable dt = _dbHelper.GetDataTable(qry);
                var item = (from DataRow row in dt.Rows
                            select new ComboBonusListBEL
                            {
                                ComboBonusNo = row["COMBO_BONUS_NO"].ToString(),
                                ComboBonusName = row["COMBO_BONUS_NAME"].ToString(),
                                Status = row["STATUS"].ToString(),
                                Duration = row["DURATION"].ToString()
                            }).ToList();
                return item;

            }
            catch (Exception e)
            {
                var lineNum = e.StackTrace.Substring(e.StackTrace.LastIndexOf(' '));
                _errorLogger.GetErrorMessage(e.Message, "DistWiseSrSalesDAL", lineNum);
                ExceptionReturn = e.Message;
                return "Not Ok";
            }
        }

        public object GetComboBonusValCustomDate(string fDate, string tDate, string dCode, string rCode, string aCode, string tCode, string cCode, string cbNo)
        {
            try
            {
                using (OracleConnection objConn = new OracleConnection(ConnString))
                {
                    using (OracleCommand objCmd = new OracleCommand())
                    {
                        objCmd.Connection = objConn;
                        objCmd.CommandText = "FN_COMBO_BONUS_DR_REPORT_VAL";
                        objCmd.CommandType = CommandType.StoredProcedure;
                        objCmd.Parameters.Add("p_from_date", OracleType.VarChar).Value = fDate;
                        objCmd.Parameters.Add("p_to_date", OracleType.VarChar).Value = tDate;
                        objCmd.Parameters.Add("p_division_code", OracleType.VarChar).Value = dCode;
                        objCmd.Parameters.Add("p_region_code", OracleType.VarChar).Value = rCode;
                        objCmd.Parameters.Add("p_area_code", OracleType.VarChar).Value = aCode;
                        objCmd.Parameters.Add("p_territory_code", OracleType.VarChar).Value = tCode;
                        objCmd.Parameters.Add("p_customer_code", OracleType.VarChar).Value = cCode;
                        objCmd.Parameters.Add("p_combo_bonus_no", OracleType.VarChar).Value = cbNo;
                        objCmd.Parameters.Add("return_value", OracleType.Cursor).Direction = ParameterDirection.ReturnValue;
                        objConn.Open();
                        objCmd.ExecuteNonQuery();
                        OracleDataReader rdr = objCmd.ExecuteReader();
                        DataTable dt = new DataTable();
                        if (rdr.HasRows)
                        {
                            dt.Load(rdr);
                        }
                        _dbHelper.InsertReportAudit("Combo Bonus Adjuestment");

                        List<ComboBonusValBEL> item;
                        item = (from DataRow row in dt.Rows
                                select new ComboBonusValBEL
                                {
                                    DivisionCode = row["DIVISION_CODE"].ToString(),
                                    DivisionName = row["DIVISION_NAME"].ToString(),
                                    RegionCode = row["REGION_CODE"].ToString(),
                                    RegionName = row["REGION_NAME"].ToString(),
                                    AreaCode = row["AREA_CODE"].ToString(),
                                    AreaName = row["AREA_NAME"].ToString(),
                                    TerritoryCode = row["TERRITORY_CODE"].ToString(),
                                    TerritoryName = row["TERRITORY_NAME"].ToString(),
                                    MarketCode = row["MARKET_CODE"].ToString(),
                                    MarketName = row["MARKET_NAME"].ToString(),
                                    CustomerCode = row["CUSTOMER_CODE"].ToString(),
                                    CustomerName = row["CUSTOMER_NAME"].ToString(),
                                    DbLocation = row["DB_LOCATION"].ToString(),
                                    RouteCode = row["ROUTE_CODE"].ToString(),
                                    RouteName = row["ROUTE_NAME"].ToString(),
                                    RetailerCode = row["RETAILER_CODE"].ToString(),
                                    RetailerName = row["RETAILER_NAME"].ToString(),
                                    InvoiceDate = row["INVOICE_DATE"].ToString(),
                                    InvoiceNo = row["INVOICE_NO"].ToString(),
                                    ComboBonusNo = row["COMBO_BONUS_NO"].ToString(),
                                    ComboBonusName = row["COMBO_BONUS_NAME"].ToString(),
                                    SalesValue = row["SALES_VALUE"].ToString(),
                                    ReturnSalesValue = row["RETURN_SALES_VALUE"].ToString(),
                                    NetImsValue = row["NET_IMS_VALUE"].ToString(),
                                    ComboBonusDisc = row["COMBO_BONUS_DISC"].ToString(),
                                    ReturnComboBonusDisc = row["RETURN_COMBO_BONUS_DISC"].ToString(),
                                    NetComboBonusDisc = row["NET_COMBO_BONUS_DISC"].ToString(),
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