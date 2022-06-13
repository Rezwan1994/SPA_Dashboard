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
    public class FsoTimeKeepingDAL : ReturnData
    {
        private readonly DBHelper _dbHelper = new DBHelper();
        private readonly ErrorLogger _errorLogger = new ErrorLogger();
        private static readonly DBConnection DBConnection = new DBConnection();
        public readonly string ConnString = DBConnection.SAConnStrReader("Oracle", "PHARMAERP");


        public object GetFsoTimeKeepingDateRange(string fDate, string tDate, string dCode, string rCode, string aCode, string tCode, string cCode, string sCode)
        {
            try
            {
                using (OracleConnection objConn = new OracleConnection(ConnString))
                {
                    using (OracleCommand objCmd = new OracleCommand())
                    {
                        objCmd.Connection = objConn;
                        objCmd.CommandText = "FN_FSO_TIME_KEEPING_DATE_RANGE";
                        objCmd.CommandType = CommandType.StoredProcedure;
                        objCmd.Parameters.Add("pfrom_date", OracleType.VarChar).Value = fDate;
                        objCmd.Parameters.Add("pto_date", OracleType.VarChar).Value = tDate;
                        objCmd.Parameters.Add("pdivision_code", OracleType.VarChar).Value = dCode;
                        objCmd.Parameters.Add("pregion_code", OracleType.VarChar).Value = rCode;
                        objCmd.Parameters.Add("parea_code", OracleType.VarChar).Value = aCode;
                        objCmd.Parameters.Add("pterritory_code", OracleType.VarChar).Value = tCode;
                        objCmd.Parameters.Add("pcustomer_code", OracleType.VarChar).Value = cCode;
                        objCmd.Parameters.Add("psr_code", OracleType.VarChar).Value = sCode;
                        objCmd.Parameters.Add("return_value", OracleType.Cursor).Direction = ParameterDirection.ReturnValue;
                        objConn.Open();
                        objCmd.ExecuteNonQuery();
                        OracleDataReader rdr = objCmd.ExecuteReader();
                        DataTable dt = new DataTable();
                        if (rdr.HasRows)
                        {
                            dt.Load(rdr);
                        }
                        _dbHelper.InsertReportAudit("FSO wise time keeping(Custom Date)");

                        List<FsoTimeKeepingBEL> item;
                        item = (from DataRow row in dt.Rows
                                select new FsoTimeKeepingBEL
                                {
                                    DivisionCode = row["DIVISION_CODE"].ToString(),
                                    DivisionName = row["DIVISION_NAME"].ToString(),
                                    RegionCode = row["REGION_CODE"].ToString(),
                                    RegionName = row["REGION_NAME"].ToString(),
                                    AreaCode = row["AREA_CODE"].ToString(),
                                    AreaName = row["AREA_NAME"].ToString(),
                                    TerritoryCode = row["TERRITORY_CODE"].ToString(),
                                    TerritoryName = row["TERRITORY_NAME"].ToString(),
                                    DbLocation = row["DB_LOCATION"].ToString(),
                                    CustomerCode = row["CUSTOMER_CODE"].ToString(),
                                    CustomerName = row["CUSTOMER_NAME"].ToString(),
                                    EmployeeCode = row["EMPLOYEE_CODE"].ToString(),
                                    EmployeeName = row["EMPLOYEE_NAME"].ToString(),
                                    MarketCode = row["MARKET_CODE"].ToString(),
                                    MarketName = row["MARKET_NAME"].ToString(),
                                    RouteCode = row["ROUTE_CODE"].ToString(),
                                    RouteName = row["ROUTE_NAME"].ToString(),
                                    RetailerCode = row["RETAILER_CODE"].ToString(),
                                    RetailerName = row["RETAILER_NAME"].ToString(),
                                    OrderDate = row["ORDER_DATE"].ToString(),
                                    DeliveryDate = row["DELIVERY_DATE"].ToString(),
                                    OrderTime = row["ORDER_TIME"].ToString(),
                                    OrderNo = row["ORDER_NO"].ToString(),
                                    OrderType = row["ORDER_TYPE"].ToString(),
                                    InvoiceStatus = row["INVOICE_STATUS"].ToString(),
                                    NumberOfProduct = row["NUMBER_OF_PRODUCT"].ToString(),
                                    OrderValue = row["ORDER_VALUE"].ToString(),
                                    TotalRouteRetailer = row["TOTAL_ROUTE_RETAILER"].ToString(),
                                    FirstOrder = row["FIRST_ORDER"].ToString(),
                                    FirstOrderTime = row["FIRST_ORDER_TIME"].ToString(),
                                    LastOrder = row["LAST_ORDER"].ToString(),
                                    LastOrderTime = row["LAST_ORDER_TIME"].ToString(),


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