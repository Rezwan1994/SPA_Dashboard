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

    public class RouteWiseImsDAL : ReturnData
    {



        private readonly DBHelper _dbHelper = new DBHelper();
        private readonly ErrorLogger _errorLogger = new ErrorLogger();
        private static readonly DBConnection DBConnection = new DBConnection();
        public readonly string ConnString = DBConnection.SAConnStrReader("Oracle", "PHARMAERP");


        //Function 
        public object GeRouteWiseImsToday(string dCode, string rCode, string aCode, string tCode, string cCode)
        {
            try
            {
                using (OracleConnection objConn = new OracleConnection(ConnString))
                {
                    using (OracleCommand objCmd = new OracleCommand())
                    {
                        objCmd.Connection = objConn;
                        objCmd.CommandText = "FN_ROUTE_IMS_TO_DAY";
                        objCmd.CommandType = CommandType.StoredProcedure;

                        objCmd.Parameters.Add("pdivision_code", OracleType.VarChar).Value = dCode;
                        objCmd.Parameters.Add("pregion_code", OracleType.VarChar).Value = rCode;
                        objCmd.Parameters.Add("parea_code", OracleType.VarChar).Value = aCode;
                        objCmd.Parameters.Add("pterritory_code", OracleType.VarChar).Value = tCode;
                        objCmd.Parameters.Add("pcustomer_code", OracleType.VarChar).Value = cCode;


                        objCmd.Parameters.Add("return_value", OracleType.Cursor).Direction = ParameterDirection.ReturnValue;
                        objConn.Open();
                        objCmd.ExecuteNonQuery();
                        OracleDataReader rdr = objCmd.ExecuteReader();
                        DataTable dt = new DataTable();
                        if (rdr.HasRows)
                        {
                            dt.Load(rdr);
                        }
                        _dbHelper.InsertReportAudit("Route Wise IMS (Today)");


                        int count = 0;
                        List<RouteWiseImsBEL> item;
                        item = (from DataRow row in dt.Rows
                                select new RouteWiseImsBEL
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
                                    NoOfInv = Convert.ToInt32(row["NO_OF_INV"]),
                                    TotalInvAmt = Convert.ToInt32(row["TOTAL_INV_AMT"]),
                                    SlabAdjustment = Convert.ToInt32(row["SLAB_ADJUSTMENT"]),
                                    NetInvAmount = Convert.ToInt32(row["NET_INV_AMOUNT"]),
                                    ReturnValue = Convert.ToInt32(row["RETURN_VALUE"]),
                                    ReturnSlabAdjust = Convert.ToInt32(row["RETURN_SLAB_ADJUST"]),
                                    NetReturnVal = Convert.ToInt32(row["NET_RETURN_VAL"]),
                                    NetIms = Convert.ToInt32(row["NET_IMS"]),
                                    NoOfReplaceInv = Convert.ToInt32(row["NO_OF_REPLACE_INV"]),
                                    ReplaceInvAmt = Convert.ToInt32(row["REPLACE_INV_AMT"])

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

        //Function 
        public object GeRouteWiseImsDateRange(string fDate, string tDate, string dCode, string rCode, string aCode, string tCode, string cCode)
        {
            try
            {
                using (OracleConnection objConn = new OracleConnection(ConnString))
                {
                    using (OracleCommand objCmd = new OracleCommand())
                    {
                        objCmd.Connection = objConn;
                        objCmd.CommandText = "FN_ROUTE_IMS_DATE_RANGE";
                        objCmd.CommandType = CommandType.StoredProcedure;


                        objCmd.Parameters.Add("pfrom_date", OracleType.VarChar).Value = fDate;
                        objCmd.Parameters.Add("pto_date", OracleType.VarChar).Value = tDate;

                        objCmd.Parameters.Add("pdivision_code", OracleType.VarChar).Value = dCode;
                        objCmd.Parameters.Add("pregion_code", OracleType.VarChar).Value = rCode;
                        objCmd.Parameters.Add("parea_code", OracleType.VarChar).Value = aCode;
                        objCmd.Parameters.Add("pterritory_code", OracleType.VarChar).Value = tCode;
                        objCmd.Parameters.Add("pcustomer_code", OracleType.VarChar).Value = cCode;


                        objCmd.Parameters.Add("return_value", OracleType.Cursor).Direction = ParameterDirection.ReturnValue;
                        objConn.Open();
                        objCmd.ExecuteNonQuery();
                        OracleDataReader rdr = objCmd.ExecuteReader();
                        DataTable dt = new DataTable();
                        if (rdr.HasRows)
                        {
                            dt.Load(rdr);
                        }
                        _dbHelper.InsertReportAudit("Route Wise IMS (Custom Date)");

                        int count = 0;
                        List<RouteWiseImsBEL> item;
                        item = (from DataRow row in dt.Rows
                                select new RouteWiseImsBEL
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
                                    NoOfInv = Convert.ToInt32(row["NO_OF_INV"]),
                                    TotalInvAmt = Convert.ToDouble(row["TOTAL_INV_AMT"]),
                                    SlabAdjustment = Convert.ToDouble(row["SLAB_ADJUSTMENT"]),
                                    NetInvAmount = Convert.ToDouble(row["NET_INV_AMOUNT"]),
                                    ReturnValue = Convert.ToDouble(row["RETURN_VALUE"]),
                                    ReturnSlabAdjust = Convert.ToDouble(row["RETURN_SLAB_ADJUST"]),
                                    NetReturnVal = Convert.ToDouble(row["NET_RETURN_VAL"]),
                                    NetIms = Convert.ToDouble(row["NET_IMS"]),
                                    NoOfReplaceInv = Convert.ToInt32(row["NO_OF_REPLACE_INV"]),
                                    ReplaceInvAmt = Convert.ToDouble(row["REPLACE_INV_AMT"])

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



        public List<RouteWiseImsBEL> GeRouteWiseImsYesterday(string dCode, string rCode, string aCode, string tCode, string cCode)
        {
            try
            {
                string qry = " SELECT DIVISION_CODE, DIVISION_NAME, REGION_CODE, REGION_NAME, AREA_CODE, AREA_NAME, TERRITORY_CODE, TERRITORY_NAME, MARKET_CODE, MARKET_NAME, CUSTOMER_CODE, CUSTOMER_NAME, DB_LOCATION, ROUTE_CODE, ROUTE_NAME, NO_OF_INV, TOTAL_INV_AMT, SLAB_ADJUSTMENT, NET_INV_AMOUNT, RETURN_VALUE, RETURN_SLAB_ADJUST, NET_RETURN_VAL, NET_IMS, NO_OF_REPLACE_INV, REPLACE_INV_AMT " +
                              " FROM MV_ROUTE_WISE_IMS_YESTERDAY " +
                              " WHERE (DIVISION_CODE = '" + dCode + "' OR '" + dCode + "' = 'ALL')" +
                              " AND   (REGION_CODE='" + rCode + "' OR '" + rCode + "'='ALL')" +
                              " AND   (AREA_CODE='" + aCode + "' OR '" + aCode + "'='ALL')" +
                              " AND   (TERRITORY_CODE='" + tCode + "' OR '" + tCode + "'='ALL')" +
                              " AND   (CUSTOMER_CODE='" + cCode + "' OR '" + cCode + "'='ALL')" +
                              " ORDER BY DIVISION_CODE,REGION_CODE, AREA_CODE, TERRITORY_CODE, CUSTOMER_CODE,MARKET_CODE";


                //DataTable dt = _dbHelper.GetDataTable(qry);
                DataTable dt = _dbHelper.GetDataTableWithAuditTrial(qry, "Route Wise IMS (Yesterday)");

                int count = 0;
                var item = (from DataRow row in dt.Rows
                            select new RouteWiseImsBEL
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
                                NoOfInv = Convert.ToInt32(row["NO_OF_INV"]),
                                TotalInvAmt = Convert.ToDouble(row["TOTAL_INV_AMT"]),
                                SlabAdjustment = Convert.ToDouble(row["SLAB_ADJUSTMENT"]),
                                NetInvAmount = Convert.ToDouble(row["NET_INV_AMOUNT"]),
                                ReturnValue = Convert.ToDouble(row["RETURN_VALUE"]),
                                ReturnSlabAdjust = Convert.ToDouble(row["RETURN_SLAB_ADJUST"]),
                                NetReturnVal = Convert.ToDouble(row["NET_RETURN_VAL"]),
                                NetIms = Convert.ToDouble(row["NET_IMS"]),
                                NoOfReplaceInv = Convert.ToInt32(row["NO_OF_REPLACE_INV"]),
                                ReplaceInvAmt = Convert.ToDouble(row["REPLACE_INV_AMT"])
                            }).ToList();
                return item;
            }
            catch (Exception e)
            {
                var lineNum = e.StackTrace.Substring(e.StackTrace.LastIndexOf(' '));
                _errorLogger.GetErrorMessage(e.Message, "RouteWiseImsDAL", lineNum);
                ExceptionReturn = e.Message;
                throw;
            }
        }
        public List<RouteWiseImsBEL> GeRouteWiseImsLastSevendays(string dCode, string rCode, string aCode, string tCode, string cCode)
        {
            try
            {
                string qry = " SELECT DIVISION_CODE, DIVISION_NAME, REGION_CODE, REGION_NAME, AREA_CODE, AREA_NAME, TERRITORY_CODE, TERRITORY_NAME, MARKET_CODE, MARKET_NAME, CUSTOMER_CODE, CUSTOMER_NAME, DB_LOCATION, ROUTE_CODE, ROUTE_NAME, NO_OF_INV, TOTAL_INV_AMT, SLAB_ADJUSTMENT, NET_INV_AMOUNT, RETURN_VALUE, RETURN_SLAB_ADJUST, NET_RETURN_VAL, NET_IMS, NO_OF_REPLACE_INV, REPLACE_INV_AMT " +
                              " FROM MV_ROUTE_WISE_IMS_LAST_7DAYS " +
                              " WHERE (DIVISION_CODE = '" + dCode + "' OR '" + dCode + "' = 'ALL')" +
                              " AND   (REGION_CODE='" + rCode + "' OR '" + rCode + "'='ALL')" +
                              " AND   (AREA_CODE='" + aCode + "' OR '" + aCode + "'='ALL')" +
                              " AND   (TERRITORY_CODE='" + tCode + "' OR '" + tCode + "'='ALL')" +
                              " AND   (CUSTOMER_CODE='" + cCode + "' OR '" + cCode + "'='ALL')" +
                              " ORDER BY DIVISION_CODE,REGION_CODE, AREA_CODE, TERRITORY_CODE, CUSTOMER_CODE,MARKET_CODE";


                //DataTable dt = _dbHelper.GetDataTable(qry);
                DataTable dt = _dbHelper.GetDataTableWithAuditTrial(qry, "Route Wise IMS (Last Seven Days)");
                int count = 0;
                var item = (from DataRow row in dt.Rows
                            select new RouteWiseImsBEL
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
                                NoOfInv = Convert.ToInt32(row["NO_OF_INV"]),
                                TotalInvAmt = Convert.ToDouble(row["TOTAL_INV_AMT"]),
                                SlabAdjustment = Convert.ToDouble(row["SLAB_ADJUSTMENT"]),
                                NetInvAmount = Convert.ToDouble(row["NET_INV_AMOUNT"]),
                                ReturnValue = Convert.ToDouble(row["RETURN_VALUE"]),
                                ReturnSlabAdjust = Convert.ToDouble(row["RETURN_SLAB_ADJUST"]),
                                NetReturnVal = Convert.ToDouble(row["NET_RETURN_VAL"]),
                                NetIms = Convert.ToDouble(row["NET_IMS"]),
                                NoOfReplaceInv = Convert.ToInt32(row["NO_OF_REPLACE_INV"]),
                                ReplaceInvAmt = Convert.ToDouble(row["REPLACE_INV_AMT"])
                            }).ToList();
                return item;
            }
            catch (Exception e)
            {
                var lineNum = e.StackTrace.Substring(e.StackTrace.LastIndexOf(' '));
                _errorLogger.GetErrorMessage(e.Message, "RouteWiseImsDAL", lineNum);
                ExceptionReturn = e.Message;
                throw;
            }
        }
        public List<RouteWiseImsBEL> GeRouteWiseImsLastThirtydays(string dCode, string rCode, string aCode, string tCode, string cCode)
        {
            try
            {
                string qry = " SELECT DIVISION_CODE, DIVISION_NAME, REGION_CODE, REGION_NAME, AREA_CODE, AREA_NAME, TERRITORY_CODE, TERRITORY_NAME, MARKET_CODE, MARKET_NAME, CUSTOMER_CODE, CUSTOMER_NAME, DB_LOCATION, ROUTE_CODE, ROUTE_NAME, NO_OF_INV, TOTAL_INV_AMT, SLAB_ADJUSTMENT, NET_INV_AMOUNT, RETURN_VALUE, RETURN_SLAB_ADJUST, NET_RETURN_VAL, NET_IMS, NO_OF_REPLACE_INV, REPLACE_INV_AMT " +
                              " FROM MV_ROUTE_WISE_IMS_LAST_30DAYS " +
                              " WHERE (DIVISION_CODE = '" + dCode + "' OR '" + dCode + "' = 'ALL')" +
                              " AND   (REGION_CODE='" + rCode + "' OR '" + rCode + "'='ALL')" +
                              " AND   (AREA_CODE='" + aCode + "' OR '" + aCode + "'='ALL')" +
                              " AND   (TERRITORY_CODE='" + tCode + "' OR '" + tCode + "'='ALL')" +
                              " AND   (CUSTOMER_CODE='" + cCode + "' OR '" + cCode + "'='ALL')" +
                              " ORDER BY DIVISION_CODE,REGION_CODE, AREA_CODE, TERRITORY_CODE, CUSTOMER_CODE,MARKET_CODE";


                //DataTable dt = _dbHelper.GetDataTable(qry);
                DataTable dt = _dbHelper.GetDataTableWithAuditTrial(qry, "Route Wise IMS (Last Thirty Days)");

                int count = 0;
                var item = (from DataRow row in dt.Rows
                            select new RouteWiseImsBEL
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
                                NoOfInv = Convert.ToInt32(row["NO_OF_INV"]),
                                TotalInvAmt = Convert.ToDouble(row["TOTAL_INV_AMT"]),
                                SlabAdjustment = Convert.ToDouble(row["SLAB_ADJUSTMENT"]),
                                NetInvAmount = Convert.ToDouble(row["NET_INV_AMOUNT"]),
                                ReturnValue = Convert.ToDouble(row["RETURN_VALUE"]),
                                ReturnSlabAdjust = Convert.ToDouble(row["RETURN_SLAB_ADJUST"]),
                                NetReturnVal = Convert.ToDouble(row["NET_RETURN_VAL"]),
                                NetIms = Convert.ToDouble(row["NET_IMS"]),
                                NoOfReplaceInv = Convert.ToInt32(row["NO_OF_REPLACE_INV"]),
                                ReplaceInvAmt = Convert.ToDouble(row["REPLACE_INV_AMT"])
                            }).ToList();
                return item;
            }
            catch (Exception e)
            {
                var lineNum = e.StackTrace.Substring(e.StackTrace.LastIndexOf(' '));
                _errorLogger.GetErrorMessage(e.Message, "RouteWiseImsDAL", lineNum);
                ExceptionReturn = e.Message;
                throw;
            }
        }
        public List<RouteWiseImsBEL> GeRouteWiseImsCurrentMonth(string dCode, string rCode, string aCode, string tCode, string cCode)
        {
            try
            {
                string qry = " SELECT DIVISION_CODE, DIVISION_NAME, REGION_CODE, REGION_NAME, AREA_CODE, AREA_NAME, TERRITORY_CODE, TERRITORY_NAME, MARKET_CODE, MARKET_NAME, CUSTOMER_CODE, CUSTOMER_NAME, DB_LOCATION, ROUTE_CODE, ROUTE_NAME, NO_OF_INV, TOTAL_INV_AMT, SLAB_ADJUSTMENT, NET_INV_AMOUNT, RETURN_VALUE, RETURN_SLAB_ADJUST, NET_RETURN_VAL, NET_IMS, NO_OF_REPLACE_INV, REPLACE_INV_AMT " +
                              " FROM MV_ROUTE_WISE_IMS_CMONTH " +
                              " WHERE (DIVISION_CODE = '" + dCode + "' OR '" + dCode + "' = 'ALL')" +
                              " AND   (REGION_CODE='" + rCode + "' OR '" + rCode + "'='ALL')" +
                              " AND   (AREA_CODE='" + aCode + "' OR '" + aCode + "'='ALL')" +
                              " AND   (TERRITORY_CODE='" + tCode + "' OR '" + tCode + "'='ALL')" +
                              " AND   (CUSTOMER_CODE='" + cCode + "' OR '" + cCode + "'='ALL')" +
                              " ORDER BY DIVISION_CODE,REGION_CODE, AREA_CODE, TERRITORY_CODE, CUSTOMER_CODE,MARKET_CODE";


                //DataTable dt = _dbHelper.GetDataTable(qry);
                DataTable dt = _dbHelper.GetDataTableWithAuditTrial(qry, "Route Wise IMS (Current Month)");
                int count = 0;
                var item = (from DataRow row in dt.Rows
                            select new RouteWiseImsBEL
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
                                NoOfInv = Convert.ToInt32(row["NO_OF_INV"]),
                                TotalInvAmt = Convert.ToDouble(row["TOTAL_INV_AMT"]),
                                SlabAdjustment = Convert.ToDouble(row["SLAB_ADJUSTMENT"]),
                                NetInvAmount = Convert.ToDouble(row["NET_INV_AMOUNT"]),
                                ReturnValue = Convert.ToDouble(row["RETURN_VALUE"]),
                                ReturnSlabAdjust = Convert.ToDouble(row["RETURN_SLAB_ADJUST"]),
                                NetReturnVal = Convert.ToDouble(row["NET_RETURN_VAL"]),
                                NetIms = Convert.ToDouble(row["NET_IMS"]),
                                NoOfReplaceInv = Convert.ToInt32(row["NO_OF_REPLACE_INV"]),
                                ReplaceInvAmt = Convert.ToDouble(row["REPLACE_INV_AMT"])
                            }).ToList();
                return item;
            }
            catch (Exception e)
            {
                var lineNum = e.StackTrace.Substring(e.StackTrace.LastIndexOf(' '));
                _errorLogger.GetErrorMessage(e.Message, "RouteWiseImsDAL", lineNum);
                ExceptionReturn = e.Message;
                throw;
            }
        }
        public List<RouteWiseImsBEL> GeRouteWiseImsLastMonth(string dCode, string rCode, string aCode, string tCode, string cCode)
        {
            try
            {
                string qry = " SELECT DIVISION_CODE, DIVISION_NAME, REGION_CODE, REGION_NAME, AREA_CODE, AREA_NAME, TERRITORY_CODE, TERRITORY_NAME, MARKET_CODE, MARKET_NAME, CUSTOMER_CODE, CUSTOMER_NAME, DB_LOCATION, ROUTE_CODE, ROUTE_NAME, NO_OF_INV, TOTAL_INV_AMT, SLAB_ADJUSTMENT, NET_INV_AMOUNT, RETURN_VALUE, RETURN_SLAB_ADJUST, NET_RETURN_VAL, NET_IMS, NO_OF_REPLACE_INV, REPLACE_INV_AMT " +
                              " FROM MV_ROUTE_WISE_IMS_LMONTH " +
                              " WHERE (DIVISION_CODE = '" + dCode + "' OR '" + dCode + "' = 'ALL')" +
                              " AND   (REGION_CODE='" + rCode + "' OR '" + rCode + "'='ALL')" +
                              " AND   (AREA_CODE='" + aCode + "' OR '" + aCode + "'='ALL')" +
                              " AND   (TERRITORY_CODE='" + tCode + "' OR '" + tCode + "'='ALL')" +
                              " AND   (CUSTOMER_CODE='" + cCode + "' OR '" + cCode + "'='ALL')" +
                              " ORDER BY DIVISION_CODE,REGION_CODE, AREA_CODE, TERRITORY_CODE, CUSTOMER_CODE,MARKET_CODE";


                //DataTable dt = _dbHelper.GetDataTable(qry);
                DataTable dt = _dbHelper.GetDataTableWithAuditTrial(qry, "Route Wise IMS (Last Month)");
                int count = 0;
                var item = (from DataRow row in dt.Rows
                            select new RouteWiseImsBEL
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
                                NoOfInv = Convert.ToInt32(row["NO_OF_INV"]),
                                TotalInvAmt = Convert.ToDouble(row["TOTAL_INV_AMT"]),
                                SlabAdjustment = Convert.ToDouble(row["SLAB_ADJUSTMENT"]),
                                NetInvAmount = Convert.ToDouble(row["NET_INV_AMOUNT"]),
                                ReturnValue = Convert.ToDouble(row["RETURN_VALUE"]),
                                ReturnSlabAdjust = Convert.ToDouble(row["RETURN_SLAB_ADJUST"]),
                                NetReturnVal = Convert.ToDouble(row["NET_RETURN_VAL"]),
                                NetIms = Convert.ToDouble(row["NET_IMS"]),
                                NoOfReplaceInv = Convert.ToInt32(row["NO_OF_REPLACE_INV"]),
                                ReplaceInvAmt = Convert.ToDouble(row["REPLACE_INV_AMT"])
                            }).ToList();
                return item;
            }
            catch (Exception e)
            {
                var lineNum = e.StackTrace.Substring(e.StackTrace.LastIndexOf(' '));
                _errorLogger.GetErrorMessage(e.Message, "RouteWiseImsDAL", lineNum);
                ExceptionReturn = e.Message;
                throw;
            }
        }



    }
}