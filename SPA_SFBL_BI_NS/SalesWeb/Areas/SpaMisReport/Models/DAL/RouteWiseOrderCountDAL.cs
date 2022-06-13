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
    public class RouteWiseOrderCountDAL : ReturnData
    {

        private readonly DBHelper _dbHelper = new DBHelper();
        private readonly ErrorLogger _errorLogger = new ErrorLogger();
        private static readonly DBConnection DBConnection = new DBConnection();
        public readonly string ConnString = DBConnection.SAConnStrReader("Oracle", "PHARMAERP");

        public List<RouteWiseOrderCountBEL> GetRouteWiseOrderCountCurrentMonth(string dCode, string rCode, string aCode, string tCode, string cCode)
        {
            try
            {
                string qry = " SELECT DIVISION_CODE, DIVISION_NAME, REGION_CODE, REGION_NAME, AREA_CODE, AREA_NAME, TERRITORY_CODE, TERRITORY_NAME, MARKET_CODE, MARKET_NAME, CUSTOMER_CODE, CUSTOMER_NAME, DB_LOCATION, ROUTE_CODE, ROUTE_NAME, TOTAL_ROUTE_RETAILER, NO_OF_ROUTE_VISIT, TOTAL_VISIT_RETAILER, NO_OF_NORMAL_ORDER, NO_OF_REPLACE_ORDER, NO_OF_ORDERING_RETAILER, NO_OF_ORDERING_SKU, ORDER_VALUE, TO_CHAR(PRODUCTIVITY_CALL)PRODUCTIVITY_CALL, LPC " +
                              " FROM MV_ROUTE_ORDER_COUNT_CMONTH " +
                              " WHERE (DIVISION_CODE = '" + dCode + "' OR '" + dCode + "' = 'ALL')" +
                              " AND   (REGION_CODE='" + rCode + "' OR '" + rCode + "'='ALL')" +
                              " AND   (AREA_CODE='" + aCode + "' OR '" + aCode + "'='ALL')" +
                              " AND   (TERRITORY_CODE='" + tCode + "' OR '" + tCode + "'='ALL')" +
                              " AND   (CUSTOMER_CODE='" + cCode + "' OR '" + cCode + "'='ALL')" +
                              " ORDER BY DIVISION_CODE,REGION_CODE, AREA_CODE, TERRITORY_CODE, CUSTOMER_CODE,MARKET_CODE,ROUTE_CODE";


                //DataTable dt = _dbHelper.GetDataTable(qry);
                DataTable dt = _dbHelper.GetDataTableWithAuditTrial(qry, "Route Wise Order Count (Current Month)");

                int count = 0;
                var item = (from DataRow row in dt.Rows
                            select new RouteWiseOrderCountBEL
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
                                MarketCode = row["MARKET_CODE"].ToString(),
                                MarketName = row["MARKET_NAME"].ToString(),
                                CustomerCode = row["CUSTOMER_CODE"].ToString(),
                                CustomerName = row["CUSTOMER_NAME"].ToString(),
                                DbLocation = row["DB_LOCATION"].ToString(),
                                RouteCode = row["ROUTE_CODE"].ToString(),
                                RouteName = row["ROUTE_NAME"].ToString(),
                                TotalRouteRetailer = row["TOTAL_ROUTE_RETAILER"].ToString(),
                                NoOfRouteVisit = row["NO_OF_ROUTE_VISIT"].ToString(),
                                TotalVisitRetailer = row["TOTAL_VISIT_RETAILER"].ToString(),
                                NoOfNormalOrder = row["NO_OF_NORMAL_ORDER"].ToString(),
                                NoOfReplaceOrder = row["NO_OF_REPLACE_ORDER"].ToString(),
                                NoOfOrderingRetailer = row["NO_OF_ORDERING_RETAILER"].ToString(),
                                NoOfOrderingSku = row["NO_OF_ORDERING_SKU"].ToString(),
                                OrderValue = row["ORDER_VALUE"].ToString(),
                                ProductivityCall = row["PRODUCTIVITY_CALL"].ToString(),
                                Lpc = row["LPC"].ToString()
                            }).ToList();
                return item;
            }
            catch (Exception e)
            {
                var lineNum = e.StackTrace.Substring(e.StackTrace.LastIndexOf(' '));
                _errorLogger.GetErrorMessage(e.Message, "SkuNumericSalesAnalysisDAL", lineNum);
                ExceptionReturn = e.Message;
                throw;
            }
        }


        public List<RouteWiseOrderCountBEL> GetRouteWiseOrderCountLastMonth(string dCode, string rCode, string aCode, string tCode, string cCode)
        {
            try
            {
                string qry = " SELECT DIVISION_CODE, DIVISION_NAME, REGION_CODE, REGION_NAME, AREA_CODE, AREA_NAME, TERRITORY_CODE, TERRITORY_NAME, MARKET_CODE, MARKET_NAME, CUSTOMER_CODE, CUSTOMER_NAME, DB_LOCATION, ROUTE_CODE, ROUTE_NAME, TOTAL_ROUTE_RETAILER, NO_OF_ROUTE_VISIT, TOTAL_VISIT_RETAILER, NO_OF_NORMAL_ORDER, NO_OF_REPLACE_ORDER, NO_OF_ORDERING_RETAILER, NO_OF_ORDERING_SKU, ORDER_VALUE, TO_CHAR(PRODUCTIVITY_CALL)PRODUCTIVITY_CALL, LPC  " +
                              " FROM MV_ROUTE_ORDER_COUNT_LMONTH " +
                              " WHERE (DIVISION_CODE = '" + dCode + "' OR '" + dCode + "' = 'ALL')" +
                              " AND   (REGION_CODE='" + rCode + "' OR '" + rCode + "'='ALL')" +
                              " AND   (AREA_CODE='" + aCode + "' OR '" + aCode + "'='ALL')" +
                              " AND   (TERRITORY_CODE='" + tCode + "' OR '" + tCode + "'='ALL')" +
                              " AND   (CUSTOMER_CODE='" + cCode + "' OR '" + cCode + "'='ALL')" +
                              " ORDER BY DIVISION_CODE,REGION_CODE, AREA_CODE, TERRITORY_CODE, CUSTOMER_CODE,MARKET_CODE,ROUTE_CODE";


                //DataTable dt = _dbHelper.GetDataTable(qry);
                DataTable dt = _dbHelper.GetDataTableWithAuditTrial(qry, "Route Wise Order Count (Last Month)");

                int count = 0;
                var item = (from DataRow row in dt.Rows
                            select new RouteWiseOrderCountBEL
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
                                MarketCode = row["MARKET_CODE"].ToString(),
                                MarketName = row["MARKET_NAME"].ToString(),
                                CustomerCode = row["CUSTOMER_CODE"].ToString(),
                                CustomerName = row["CUSTOMER_NAME"].ToString(),
                                DbLocation = row["DB_LOCATION"].ToString(),
                                RouteCode = row["ROUTE_CODE"].ToString(),
                                RouteName = row["ROUTE_NAME"].ToString(),
                                TotalRouteRetailer = row["TOTAL_ROUTE_RETAILER"].ToString(),
                                NoOfRouteVisit = row["NO_OF_ROUTE_VISIT"].ToString(),
                                TotalVisitRetailer = row["TOTAL_VISIT_RETAILER"].ToString(),
                                NoOfNormalOrder = row["NO_OF_NORMAL_ORDER"].ToString(),
                                NoOfReplaceOrder = row["NO_OF_REPLACE_ORDER"].ToString(),
                                NoOfOrderingRetailer = row["NO_OF_ORDERING_RETAILER"].ToString(),
                                NoOfOrderingSku = row["NO_OF_ORDERING_SKU"].ToString(),
                                OrderValue = row["ORDER_VALUE"].ToString(),
                                ProductivityCall = row["PRODUCTIVITY_CALL"].ToString(),
                                Lpc = row["LPC"].ToString()
                            }).ToList();
                return item;
            }
            catch (Exception e)
            {
                var lineNum = e.StackTrace.Substring(e.StackTrace.LastIndexOf(' '));
                _errorLogger.GetErrorMessage(e.Message, "SkuNumericSalesAnalysisDAL", lineNum);
                ExceptionReturn = e.Message;
                throw;
            }
        }



        //  Funciton - GetDistWiseSrSales
        public object GetRouteWiseOrderCountCustomDate(string fromDate, string toDate, string dCode, string rCode, string aCode, string tCode, string cCode)
        {


            try
            {
                using (OracleConnection objConn = new OracleConnection(ConnString))
                {
                    using (OracleCommand objCmd = new OracleCommand())
                    {
                        objCmd.Connection = objConn;
                        objCmd.CommandText = "FN_ROUTE_ORDER_COUNT";
                        objCmd.CommandType = CommandType.StoredProcedure;

                        objCmd.Parameters.Add("p_from_date", OracleType.VarChar).Value = fromDate;
                        objCmd.Parameters.Add("p_to_date", OracleType.VarChar).Value = toDate;
                        objCmd.Parameters.Add("p_division_code", OracleType.VarChar).Value = dCode;
                        objCmd.Parameters.Add("p_region_code", OracleType.VarChar).Value = rCode;
                        objCmd.Parameters.Add("p_area_code", OracleType.VarChar).Value = aCode;
                        objCmd.Parameters.Add("p_territory_code", OracleType.VarChar).Value = tCode;
                        objCmd.Parameters.Add("p_customer_code", OracleType.VarChar).Value = cCode;
            

                        objCmd.Parameters.Add("return_value", OracleType.Cursor).Direction = ParameterDirection.ReturnValue;
                        objConn.Open();
                        objCmd.ExecuteNonQuery();
                        OracleDataReader rdr = objCmd.ExecuteReader();
                        DataTable dt = new DataTable();
                        if (rdr.HasRows)
                        {
                            dt.Load(rdr);
                        }
                        _dbHelper.InsertReportAudit("Route Wise Order Count (Custom Date)");

                        int count = 0;
                        List<RouteWiseOrderCountBEL> item;
                        item = (from DataRow row in dt.Rows
                                select new RouteWiseOrderCountBEL
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
                                    MarketCode = row["MARKET_CODE"].ToString(),
                                    MarketName = row["MARKET_NAME"].ToString(),
                                    CustomerCode = row["CUSTOMER_CODE"].ToString(),
                                    CustomerName = row["CUSTOMER_NAME"].ToString(),
                                    DbLocation = row["DB_LOCATION"].ToString(),
                                    RouteCode = row["ROUTE_CODE"].ToString(),
                                    RouteName = row["ROUTE_NAME"].ToString(),
                                    TotalRouteRetailer = row["TOTAL_ROUTE_RETAILER"].ToString(),
                                    NoOfRouteVisit = row["NO_OF_ROUTE_VISIT"].ToString(),
                                    TotalVisitRetailer = row["TOTAL_VISIT_RETAILER"].ToString(),
                                    NoOfNormalOrder = row["NO_OF_NORMAL_ORDER"].ToString(),
                                    NoOfReplaceOrder = row["NO_OF_REPLACE_ORDER"].ToString(),
                                    NoOfOrderingRetailer = row["NO_OF_ORDERING_RETAILER"].ToString(),
                                    NoOfOrderingSku = row["NO_OF_ORDERING_SKU"].ToString(),
                                    OrderValue = row["ORDER_VALUE"].ToString(),
                                    ProductivityCall = row["PRODUCTIVITY_CALL"].ToString(),
                                    Lpc = row["LPC"].ToString()

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