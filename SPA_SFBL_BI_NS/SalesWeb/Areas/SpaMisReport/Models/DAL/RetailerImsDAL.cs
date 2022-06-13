using System;
using System.Data;
using System.Linq;
using SalesWeb.Areas.SpaMisReport.Models.BEL;
using SalesWeb.Universal.Gateway;
using System.Collections.Generic;
using System.Data.OracleClient;
using System.Web;


namespace SalesWeb.Areas.SpaMisReport.Models.DAL
{
    public class RetailerImsDAL : ReturnData
    {


        private readonly DBHelper _dbHelper = new DBHelper();
        private readonly ErrorLogger _errorLogger = new ErrorLogger();
        private static readonly DBConnection DBConnection = new DBConnection();
        public readonly string ConnString = DBConnection.SAConnStrReader("Oracle", "PHARMAERP");

        public object GetReportDownLoadStatus(string url)
        {
            try
            {
                int userId = Convert.ToInt32(HttpContext.Current.Session["USER_ID"]);

                string Qry = " SELECT DECODE(LTRIM(RTRIM(DOWNLOAD_STATUS)),'No','false','Yes','true') DOWNLOAD_STATUS " +
                             " FROM SC_USER_REPORT_DOWNLOAD_YN A," +
                             " (" +
                             " SELECT MENU_NAME FROM SC_MENU_CONF A, SC_MENU_INFO B WHERE A.CHILD_ID=B.MENU_ID AND   A.URL = '" + url + "'" +
                             " )B" +
                             " WHERE A.REPORT_NAME=B.MENU_NAME" +
                             " AND USER_ID=" + userId;

                var dt = _dbHelper.GetDataTable(Qry);
                var item = (from DataRow row in dt.Rows
                                     select new ReportDownLoadStatusBEL
                                     {
                                         DownLoadStatus = row["DOWNLOAD_STATUS"].ToString()
                                     }).ToList();
                return item;
            }
            catch (Exception e)
            {
                var lineNum = e.StackTrace.Substring(e.StackTrace.LastIndexOf(' '));
                _errorLogger.GetErrorMessage(e.Message, "Home", lineNum);
                throw;
            }
        }

        //Function 
        public object GetRetailerImsUptoCurMonth(string dCode, string rCode, string aCode, string tCode, string cCode)
        {


            try
            {
                using (OracleConnection objConn = new OracleConnection(ConnString))
                {
                    using (OracleCommand objCmd = new OracleCommand())
                    {
                        objCmd.Connection = objConn;
                        objCmd.CommandText = "fn_Retailer_Ims";
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
                        _dbHelper.InsertReportAudit("Retailer Wise IMS(Current Month)");
                        List<RetailerImsBEL> item;
                        item = (from DataRow row in dt.Rows
                                select new RetailerImsBEL
                                {
                                    //SlNo = ++count,
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
                                    DBLoaction = row["DB_LOCATION"].ToString(),
                                    MarketCode = row["MARKET_CODE"].ToString(),
                                    MarketName = row["MARKET_NAME"].ToString(),


                                    RouteCode = row["ROUTE_CODE"].ToString(),
                                    RouteName = row["ROUTE_NAME"].ToString(),
                                    RetailerCode = row["RETAILER_CODE"].ToString(),
                                    RetailerName = row["RETAILER_NAME"].ToString(),


                                    NoOfInvoice = Convert.ToInt32(row["NO_OF_INV"]),
                                    TotalInvoiceAmount = Convert.ToDouble(row["TOTAL_INV_AMT"]),
                                    SlabAdjustment = Convert.ToDouble(row["SLAB_ADJUSTMENT"]),
                                    NetInvoiceAmount = Convert.ToDouble(row["NET_INV_AMOUNT"]),
                                    ReturnValue = Convert.ToDouble(row["RETURN_VALUE"]),
                                    ReturnSlabAdjustment = Convert.ToDouble(row["RETURN_SLAB_ADJUST"]),
                                    NetReturnValue = Convert.ToDouble(row["NET_RETURN_VAL"]),
                                    NetIms = Convert.ToDouble(row["NET_IMS"]),
                                    NoOfReplaceInvoice = Convert.ToInt32(row["NO_OF_REPLACE_INV"]),
                                    ReplaceInvoiceAmount = Convert.ToDouble(row["REPLACE_INV_AMT"])

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
        public object GetRetailerImsAnyDate(string fDate, string tDate, string dCode, string rCode, string aCode, string tCode, string cCode)
        {


            try
            {
                using (OracleConnection objConn = new OracleConnection(ConnString))
                {
                    using (OracleCommand objCmd = new OracleCommand())
                    {
                        objCmd.Connection = objConn;
                        objCmd.CommandText = "FN_RETAILER_IMS_ANY_DATE";
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
                        _dbHelper.InsertReportAudit("Retailer Wise IMS(Custom Date)");
                        List<RetailerImsBEL> item;
                        item = (from DataRow row in dt.Rows
                                select new RetailerImsBEL
                                {
                                    //SlNo = ++count,
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
                                    DBLoaction = row["DB_LOCATION"].ToString(),
                                    MarketCode = row["MARKET_CODE"].ToString(),
                                    MarketName = row["MARKET_NAME"].ToString(),


                                    RouteCode = row["ROUTE_CODE"].ToString(),
                                    RouteName = row["ROUTE_NAME"].ToString(),
                                    RetailerCode = row["RETAILER_CODE"].ToString(),
                                    RetailerName = row["RETAILER_NAME"].ToString(),


                                    NoOfInvoice = Convert.ToInt32(row["NO_OF_INV"]),
                                    TotalInvoiceAmount = Convert.ToDouble(row["TOTAL_INV_AMT"]),
                                    SlabAdjustment = Convert.ToDouble(row["SLAB_ADJUSTMENT"]),
                                    NetInvoiceAmount = Convert.ToDouble(row["NET_INV_AMOUNT"]),
                                    ReturnValue = Convert.ToDouble(row["RETURN_VALUE"]),
                                    ReturnSlabAdjustment = Convert.ToDouble(row["RETURN_SLAB_ADJUST"]),
                                    NetReturnValue = Convert.ToDouble(row["NET_RETURN_VAL"]),
                                    NetIms = Convert.ToDouble(row["NET_IMS"]),
                                    NoOfReplaceInvoice = Convert.ToInt32(row["NO_OF_REPLACE_INV"]),
                                    ReplaceInvoiceAmount = Convert.ToDouble(row["REPLACE_INV_AMT"])

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
        public object GetRetailerImsToDay(string dCode, string rCode, string aCode, string tCode, string cCode)
        {


            try
            {
                using (OracleConnection objConn = new OracleConnection(ConnString))
                {
                    using (OracleCommand objCmd = new OracleCommand())
                    {
                        objCmd.Connection = objConn;
                        objCmd.CommandText = "FN_RETAILER_IMS_TO_DAY";
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
                        _dbHelper.InsertReportAudit("Retailer Wise IMS(Today)");
                        List<RetailerImsBEL> item;
                        item = (from DataRow row in dt.Rows
                                select new RetailerImsBEL
                                {
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
                                    DBLoaction = row["DB_LOCATION"].ToString(),
                                    MarketCode = row["MARKET_CODE"].ToString(),
                                    MarketName = row["MARKET_NAME"].ToString(),


                                    RouteCode = row["ROUTE_CODE"].ToString(),
                                    RouteName = row["ROUTE_NAME"].ToString(),
                                    RetailerCode = row["RETAILER_CODE"].ToString(),
                                    RetailerName = row["RETAILER_NAME"].ToString(),


                                    NoOfInvoice = Convert.ToInt32(row["NO_OF_INV"]),
                                    TotalInvoiceAmount = Convert.ToDouble(row["TOTAL_INV_AMT"]),
                                    SlabAdjustment = Convert.ToDouble(row["SLAB_ADJUSTMENT"]),
                                    NetInvoiceAmount = Convert.ToDouble(row["NET_INV_AMOUNT"]),
                                    ReturnValue = Convert.ToDouble(row["RETURN_VALUE"]),
                                    ReturnSlabAdjustment = Convert.ToDouble(row["RETURN_SLAB_ADJUST"]),
                                    NetReturnValue = Convert.ToDouble(row["NET_RETURN_VAL"]),
                                    NetIms = Convert.ToDouble(row["NET_IMS"]),
                                    NoOfReplaceInvoice = Convert.ToInt32(row["NO_OF_REPLACE_INV"]),
                                    ReplaceInvoiceAmount = Convert.ToDouble(row["REPLACE_INV_AMT"])

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
        public List<RetailerImsBEL> GetRetailerImsUptoPrevMonth(string dCode, string rCode, string aCode, string tCode, string cCode)
        {
            try
            {
                string qry = " SELECT DIVISION_CODE, DIVISION_NAME, REGION_CODE, REGION_NAME, AREA_CODE, AREA_NAME, TERRITORY_CODE, TERRITORY_NAME, CUSTOMER_CODE, CUSTOMER_NAME, DB_LOCATION, MARKET_CODE, MARKET_NAME, ROUTE_CODE, ROUTE_NAME, RETAILER_CODE, RETAILER_NAME, NO_OF_INV, TOTAL_INV_AMT, SLAB_ADJUSTMENT, NET_INV_AMOUNT, RETURN_VALUE, RETURN_SLAB_ADJUST, NET_RETURN_VAL, NET_IMS, NO_OF_REPLACE_INV, REPLACE_INV_AMT " +
                              " FROM MV_RETAILER_IMS_PMONTH " +
                              " WHERE (DIVISION_CODE = '" + dCode + "' OR '" + dCode + "' = 'ALL')" +
                              " AND   (REGION_CODE='" + rCode + "' OR '" + rCode + "'='ALL')" +
                              " AND   (AREA_CODE='" + aCode + "' OR '" + aCode + "'='ALL')" +
                              " AND   (TERRITORY_CODE='" + tCode + "' OR '" + tCode + "'='ALL')" +
                              " AND   (CUSTOMER_CODE='" + cCode + "' OR '" + cCode + "'='ALL')" +
                              " ORDER BY DIVISION_CODE,REGION_CODE, AREA_CODE, TERRITORY_CODE, CUSTOMER_CODE, MARKET_CODE, ROUTE_CODE, RETAILER_CODE";


                //DataTable dt = _dbHelper.GetDataTable(qry);
                DataTable dt = _dbHelper.GetDataTableWithAuditTrial(qry, "Retailer Wise IMS(Last Month)");

                
                //int count = 0;
                var item = (from DataRow row in dt.Rows
                            select new RetailerImsBEL
                            {
                                //SlNo = ++count,
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
                                DBLoaction = row["DB_LOCATION"].ToString(),
                                MarketCode = row["MARKET_CODE"].ToString(),
                                MarketName = row["MARKET_NAME"].ToString(),

                                RouteCode = row["ROUTE_CODE"].ToString(),
                                RouteName = row["ROUTE_NAME"].ToString(),
                                RetailerCode = row["RETAILER_CODE"].ToString(),
                                RetailerName = row["RETAILER_NAME"].ToString(),

                                NoOfInvoice = Convert.ToInt32(row["NO_OF_INV"]),
                                TotalInvoiceAmount = Convert.ToDouble(row["TOTAL_INV_AMT"]),
                                SlabAdjustment = Convert.ToDouble(row["SLAB_ADJUSTMENT"]),
                                NetInvoiceAmount = Convert.ToDouble(row["NET_INV_AMOUNT"]),
                                ReturnValue = Convert.ToDouble(row["RETURN_VALUE"]),
                                ReturnSlabAdjustment = Convert.ToDouble(row["RETURN_SLAB_ADJUST"]),
                                NetReturnValue = Convert.ToDouble(row["NET_RETURN_VAL"]),
                                NetIms = Convert.ToDouble(row["NET_IMS"]),
                                NoOfReplaceInvoice = Convert.ToInt32(row["NO_OF_REPLACE_INV"]),
                                ReplaceInvoiceAmount = Convert.ToDouble(row["REPLACE_INV_AMT"])

                            }).ToList();
                return item;
            }
            catch (Exception e)
            {
                var lineNum = e.StackTrace.Substring(e.StackTrace.LastIndexOf(' '));
                _errorLogger.GetErrorMessage(e.Message, "DistWiseSrSalesDAL", lineNum);
                ExceptionReturn = e.Message;
                throw;
            }
        }
        public List<RetailerImsBEL> GeRetailerWiseImsYesterday(string dCode, string rCode, string aCode, string tCode, string cCode)
        {
            try
            {
                string qry = " SELECT DIVISION_CODE, DIVISION_NAME, REGION_CODE, REGION_NAME, AREA_CODE, AREA_NAME, TERRITORY_CODE, TERRITORY_NAME, CUSTOMER_CODE, CUSTOMER_NAME, DB_LOCATION, MARKET_CODE, MARKET_NAME, ROUTE_CODE, ROUTE_NAME, RETAILER_CODE, RETAILER_NAME, NO_OF_INV, TOTAL_INV_AMT, SLAB_ADJUSTMENT, NET_INV_AMOUNT, RETURN_VALUE, RETURN_SLAB_ADJUST, NET_RETURN_VAL, NET_IMS, NO_OF_REPLACE_INV, REPLACE_INV_AMT " +
                              " FROM MV_RETAILER_IMS_YESTERDAY " +
                              " WHERE (DIVISION_CODE = '" + dCode + "' OR '" + dCode + "' = 'ALL')" +
                              " AND   (REGION_CODE='" + rCode + "' OR '" + rCode + "'='ALL')" +
                              " AND   (AREA_CODE='" + aCode + "' OR '" + aCode + "'='ALL')" +
                              " AND   (TERRITORY_CODE='" + tCode + "' OR '" + tCode + "'='ALL')" +
                              " AND   (CUSTOMER_CODE='" + cCode + "' OR '" + cCode + "'='ALL')" +
                              " ORDER BY DIVISION_CODE,REGION_CODE, AREA_CODE, TERRITORY_CODE, CUSTOMER_CODE, MARKET_CODE, ROUTE_CODE, RETAILER_CODE";


                //DataTable dt = _dbHelper.GetDataTable(qry);
                DataTable dt = _dbHelper.GetDataTableWithAuditTrial(qry, "Retailer Wise IMS(Yesterday)");
                //int count = 0;
                var item = (from DataRow row in dt.Rows
                            select new RetailerImsBEL
                            {
                                //SlNo = ++count,
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
                                DBLoaction = row["DB_LOCATION"].ToString(),
                                MarketCode = row["MARKET_CODE"].ToString(),
                                MarketName = row["MARKET_NAME"].ToString(),

                                RouteCode = row["ROUTE_CODE"].ToString(),
                                RouteName = row["ROUTE_NAME"].ToString(),
                                RetailerCode = row["RETAILER_CODE"].ToString(),
                                RetailerName = row["RETAILER_NAME"].ToString(),

                                NoOfInvoice = Convert.ToInt32(row["NO_OF_INV"]),
                                TotalInvoiceAmount = Convert.ToDouble(row["TOTAL_INV_AMT"]),
                                SlabAdjustment = Convert.ToDouble(row["SLAB_ADJUSTMENT"]),
                                NetInvoiceAmount = Convert.ToDouble(row["NET_INV_AMOUNT"]),
                                ReturnValue = Convert.ToDouble(row["RETURN_VALUE"]),
                                ReturnSlabAdjustment = Convert.ToDouble(row["RETURN_SLAB_ADJUST"]),
                                NetReturnValue = Convert.ToDouble(row["NET_RETURN_VAL"]),
                                NetIms = Convert.ToDouble(row["NET_IMS"]),
                                NoOfReplaceInvoice = Convert.ToInt32(row["NO_OF_REPLACE_INV"]),
                                ReplaceInvoiceAmount = Convert.ToDouble(row["REPLACE_INV_AMT"])

                            }).ToList();
                return item;
            }
            catch (Exception e)
            {
                var lineNum = e.StackTrace.Substring(e.StackTrace.LastIndexOf(' '));
                _errorLogger.GetErrorMessage(e.Message, "DistWiseSrSalesDAL", lineNum);
                ExceptionReturn = e.Message;
                throw;
            }
        }
        public List<RetailerImsBEL> GeRetailerWiseImsLastSevendays(string dCode, string rCode, string aCode, string tCode, string cCode)
        {
            try
            {
                string qry = " SELECT DIVISION_CODE, DIVISION_NAME, REGION_CODE, REGION_NAME, AREA_CODE, AREA_NAME, TERRITORY_CODE, TERRITORY_NAME, CUSTOMER_CODE, CUSTOMER_NAME, DB_LOCATION, MARKET_CODE, MARKET_NAME, ROUTE_CODE, ROUTE_NAME, RETAILER_CODE, RETAILER_NAME, NO_OF_INV, TOTAL_INV_AMT, SLAB_ADJUSTMENT, NET_INV_AMOUNT, RETURN_VALUE, RETURN_SLAB_ADJUST, NET_RETURN_VAL, NET_IMS, NO_OF_REPLACE_INV, REPLACE_INV_AMT " +
                              " FROM MV_RETAILER_IMS_LAST_7DAY " +
                              " WHERE (DIVISION_CODE = '" + dCode + "' OR '" + dCode + "' = 'ALL')" +
                              " AND   (REGION_CODE='" + rCode + "' OR '" + rCode + "'='ALL')" +
                              " AND   (AREA_CODE='" + aCode + "' OR '" + aCode + "'='ALL')" +
                              " AND   (TERRITORY_CODE='" + tCode + "' OR '" + tCode + "'='ALL')" +
                              " AND   (CUSTOMER_CODE='" + cCode + "' OR '" + cCode + "'='ALL')" +
                              " ORDER BY DIVISION_CODE,REGION_CODE, AREA_CODE, TERRITORY_CODE, CUSTOMER_CODE, MARKET_CODE, ROUTE_CODE, RETAILER_CODE";


                //DataTable dt = _dbHelper.GetDataTable(qry);
                DataTable dt = _dbHelper.GetDataTableWithAuditTrial(qry, "Retailer Wise IMS(Last seven days)");
                //int count = 0;
                var item = (from DataRow row in dt.Rows
                            select new RetailerImsBEL
                            {
                                //SlNo = ++count,
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
                                DBLoaction = row["DB_LOCATION"].ToString(),
                                MarketCode = row["MARKET_CODE"].ToString(),
                                MarketName = row["MARKET_NAME"].ToString(),

                                RouteCode = row["ROUTE_CODE"].ToString(),
                                RouteName = row["ROUTE_NAME"].ToString(),
                                RetailerCode = row["RETAILER_CODE"].ToString(),
                                RetailerName = row["RETAILER_NAME"].ToString(),

                                NoOfInvoice = Convert.ToInt32(row["NO_OF_INV"]),
                                TotalInvoiceAmount = Convert.ToDouble(row["TOTAL_INV_AMT"]),
                                SlabAdjustment = Convert.ToDouble(row["SLAB_ADJUSTMENT"]),
                                NetInvoiceAmount = Convert.ToDouble(row["NET_INV_AMOUNT"]),
                                ReturnValue = Convert.ToDouble(row["RETURN_VALUE"]),
                                ReturnSlabAdjustment = Convert.ToDouble(row["RETURN_SLAB_ADJUST"]),
                                NetReturnValue = Convert.ToDouble(row["NET_RETURN_VAL"]),
                                NetIms = Convert.ToDouble(row["NET_IMS"]),
                                NoOfReplaceInvoice = Convert.ToInt32(row["NO_OF_REPLACE_INV"]),
                                ReplaceInvoiceAmount = Convert.ToDouble(row["REPLACE_INV_AMT"])

                            }).ToList();
                return item;
            }
            catch (Exception e)
            {
                var lineNum = e.StackTrace.Substring(e.StackTrace.LastIndexOf(' '));
                _errorLogger.GetErrorMessage(e.Message, "DistWiseSrSalesDAL", lineNum);
                ExceptionReturn = e.Message;
                throw;
            }
        }
        public List<RetailerImsBEL> GeRetailerWiseImsLastThirtydays(string dCode, string rCode, string aCode, string tCode, string cCode)
        {
            try
            {
                string qry = " SELECT DIVISION_CODE, DIVISION_NAME, REGION_CODE, REGION_NAME, AREA_CODE, AREA_NAME, TERRITORY_CODE, TERRITORY_NAME, CUSTOMER_CODE, CUSTOMER_NAME, DB_LOCATION, MARKET_CODE, MARKET_NAME, ROUTE_CODE, ROUTE_NAME, RETAILER_CODE, RETAILER_NAME, NO_OF_INV, TOTAL_INV_AMT, SLAB_ADJUSTMENT, NET_INV_AMOUNT, RETURN_VALUE, RETURN_SLAB_ADJUST, NET_RETURN_VAL, NET_IMS, NO_OF_REPLACE_INV, REPLACE_INV_AMT " +
                              " FROM MV_RETAILER_IMS_LAST_30DAY " +
                              " WHERE (DIVISION_CODE = '" + dCode + "' OR '" + dCode + "' = 'ALL')" +
                              " AND   (REGION_CODE='" + rCode + "' OR '" + rCode + "'='ALL')" +
                              " AND   (AREA_CODE='" + aCode + "' OR '" + aCode + "'='ALL')" +
                              " AND   (TERRITORY_CODE='" + tCode + "' OR '" + tCode + "'='ALL')" +
                              " AND   (CUSTOMER_CODE='" + cCode + "' OR '" + cCode + "'='ALL')" +
                              " ORDER BY DIVISION_CODE,REGION_CODE, AREA_CODE, TERRITORY_CODE, CUSTOMER_CODE, MARKET_CODE, ROUTE_CODE, RETAILER_CODE";


                //DataTable dt = _dbHelper.GetDataTable(qry);
                DataTable dt = _dbHelper.GetDataTableWithAuditTrial(qry, "Retailer Wise IMS(Last thirty days)");
                //int count = 0;
                var item = (from DataRow row in dt.Rows
                            select new RetailerImsBEL
                            {
                                //SlNo = ++count,
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
                                DBLoaction = row["DB_LOCATION"].ToString(),
                                MarketCode = row["MARKET_CODE"].ToString(),
                                MarketName = row["MARKET_NAME"].ToString(),

                                RouteCode = row["ROUTE_CODE"].ToString(),
                                RouteName = row["ROUTE_NAME"].ToString(),
                                RetailerCode = row["RETAILER_CODE"].ToString(),
                                RetailerName = row["RETAILER_NAME"].ToString(),

                                NoOfInvoice = Convert.ToInt32(row["NO_OF_INV"]),
                                TotalInvoiceAmount = Convert.ToDouble(row["TOTAL_INV_AMT"]),
                                SlabAdjustment = Convert.ToDouble(row["SLAB_ADJUSTMENT"]),
                                NetInvoiceAmount = Convert.ToDouble(row["NET_INV_AMOUNT"]),
                                ReturnValue = Convert.ToDouble(row["RETURN_VALUE"]),
                                ReturnSlabAdjustment = Convert.ToDouble(row["RETURN_SLAB_ADJUST"]),
                                NetReturnValue = Convert.ToDouble(row["NET_RETURN_VAL"]),
                                NetIms = Convert.ToDouble(row["NET_IMS"]),
                                NoOfReplaceInvoice = Convert.ToInt32(row["NO_OF_REPLACE_INV"]),
                                ReplaceInvoiceAmount = Convert.ToDouble(row["REPLACE_INV_AMT"])

                            }).ToList();
                return item;
            }
            catch (Exception e)
            {
                var lineNum = e.StackTrace.Substring(e.StackTrace.LastIndexOf(' '));
                _errorLogger.GetErrorMessage(e.Message, "DistWiseSrSalesDAL", lineNum);
                ExceptionReturn = e.Message;
                throw;
            }
        }

    }
}