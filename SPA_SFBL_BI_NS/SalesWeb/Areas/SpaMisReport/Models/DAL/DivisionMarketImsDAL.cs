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

    public class DivisionMarketImsDAL : ReturnData
    {



        private readonly DBHelper _dbHelper = new DBHelper();
        private readonly ErrorLogger _errorLogger = new ErrorLogger();
        private static readonly DBConnection DBConnection = new DBConnection();
        public readonly string ConnString = DBConnection.SAConnStrReader("Oracle", "PHARMAERP");



        public object GetDivMktImsToday(string dCode, string rCode, string aCode, string tCode, string cCode)
        {
            try
            {
                using (OracleConnection objConn = new OracleConnection(ConnString))
                {
                    using (OracleCommand objCmd = new OracleCommand())
                    {
                        objCmd.Connection = objConn;
                        objCmd.CommandText = "FN_MARKET_IMS_TODAY";
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
                        _dbHelper.InsertReportAudit("Market Wise IMS (Today)");
                        List<DivisionMarketImsBEL> item;
                        item = (from DataRow row in dt.Rows
                                select new DivisionMarketImsBEL
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
                                MarketCode = row["MARKET_CODE"].ToString(),
                                MarketName = row["MARKET_NAME"].ToString(),
                                CustomerCode = row["CUSTOMER_CODE"].ToString(),
                                CustomerName = row["CUSTOMER_NAME"].ToString(),
                                DbLoaction = row["DB_LOCATION"].ToString(),

                                NoOfInvoice = Convert.ToInt32(row["NO_OF_INV"]),
                                TotalInvoiceAmount = Convert.ToInt32(row["TOTAL_INV_AMT"]),
                                SlabAdjustment = Convert.ToInt32(row["SLAB_ADJUSTMENT"]),
                                NetInvoiceAmount = Convert.ToInt32(row["NET_INV_AMOUNT"]),
                                ReturnValue = Convert.ToInt32(row["RETURN_VALUE"]),
                                ReturnSlabAdjustment = Convert.ToInt32(row["RETURN_SLAB_ADJUST"]),
                                NetReturnValue = Convert.ToInt32(row["NET_RETURN_VAL"]),
                                NetIms = Convert.ToInt32(row["NET_IMS"]),
                                NoOfReplaceInvoice = Convert.ToInt32(row["NO_OF_REPLACE_INV"]),
                                ReplaceInvoiceAmount = Convert.ToInt32(row["REPLACE_INV_AMT"]),

                                TargetValue = Convert.ToInt32(row["TARGET_VAL"]),
                                NoOfRetailer = Convert.ToInt32(row["NO_OF_RETAILER"]),
                                NoOfOrderRetailer = Convert.ToInt32(row["NO_OF_ORD_RETAILER"])

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
        public object GetDivMktImsDateRange(string fDate, string tDate, string dCode, string rCode, string aCode, string tCode, string cCode)
        {
            try
            {
                using (OracleConnection objConn = new OracleConnection(ConnString))
                {
                    using (OracleCommand objCmd = new OracleCommand())
                    {
                        objCmd.Connection = objConn;
                        objCmd.CommandText = "FN_MARKET_IMS_DATE_RANGE";
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
                        _dbHelper.InsertReportAudit("Market Wise IMS (Custom Date)");
                        List<DivisionMarketImsBEL> item;
                        item = (from DataRow row in dt.Rows
                                select new DivisionMarketImsBEL
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
                                    MarketCode = row["MARKET_CODE"].ToString(),
                                    MarketName = row["MARKET_NAME"].ToString(),
                                    CustomerCode = row["CUSTOMER_CODE"].ToString(),
                                    CustomerName = row["CUSTOMER_NAME"].ToString(),
                                    DbLoaction = row["DB_LOCATION"].ToString(),

                                    NoOfInvoice = Convert.ToInt32(row["NO_OF_INV"]),
                                    TotalInvoiceAmount = Convert.ToDouble(row["TOTAL_INV_AMT"]),
                                    SlabAdjustment = Convert.ToDouble(row["SLAB_ADJUSTMENT"]),
                                    NetInvoiceAmount = Convert.ToDouble(row["NET_INV_AMOUNT"]),
                                    ReturnValue = Convert.ToDouble(row["RETURN_VALUE"]),
                                    ReturnSlabAdjustment = Convert.ToDouble(row["RETURN_SLAB_ADJUST"]),
                                    NetReturnValue = Convert.ToDouble(row["NET_RETURN_VAL"]),
                                    NetIms = Convert.ToDouble(row["NET_IMS"]),
                                    NoOfReplaceInvoice = Convert.ToInt32(row["NO_OF_REPLACE_INV"]),
                                    ReplaceInvoiceAmount = Convert.ToDouble(row["REPLACE_INV_AMT"]),
                                    TargetValue = Convert.ToDouble(row["TARGET_VAL"]),
                                    NoOfRetailer = Convert.ToInt32(row["NO_OF_RETAILER"]),
                                    NoOfOrderRetailer = Convert.ToInt32(row["NO_OF_ORD_RETAILER"])

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
        public List<DivisionMarketImsBEL> GetDivMktImsYesterday(string dCode, string rCode, string aCode, string tCode, string cCode)
        {
            try
            {
                string qry = " SELECT DIVISION_CODE, DIVISION_NAME, REGION_CODE, REGION_NAME, AREA_CODE, AREA_NAME, TERRITORY_CODE, TERRITORY_NAME, MARKET_CODE, MARKET_NAME, CUSTOMER_CODE, CUSTOMER_NAME, DB_LOCATION, NO_OF_INV, TOTAL_INV_AMT, SLAB_ADJUSTMENT, NET_INV_AMOUNT, RETURN_VALUE, RETURN_SLAB_ADJUST, NET_RETURN_VAL, NET_IMS, NO_OF_REPLACE_INV, REPLACE_INV_AMT, TARGET_VAL, NO_OF_RETAILER, NO_OF_ORD_RETAILER " +
                              " FROM MV_DIVISION_MKT_IMS_YESTERDAY " +
                              " WHERE (DIVISION_CODE = '" + dCode + "' OR '" + dCode + "' = 'ALL')" +
                              " AND   (REGION_CODE='" + rCode + "' OR '" + rCode + "'='ALL')" +
                              " AND   (AREA_CODE='" + aCode + "' OR '" + aCode + "'='ALL')" +
                              " AND   (TERRITORY_CODE='" + tCode + "' OR '" + tCode + "'='ALL')" +
                              " AND   (CUSTOMER_CODE='" + cCode + "' OR '" + cCode + "'='ALL')" +
                              " ORDER BY DIVISION_CODE,REGION_CODE, AREA_CODE, TERRITORY_CODE, CUSTOMER_CODE,MARKET_CODE";


                //DataTable dt = _dbHelper.GetDataTable(qry);
                DataTable dt = _dbHelper.GetDataTableWithAuditTrial(qry, "Market Wise IMS (Yesterday)");
                var item = (from DataRow row in dt.Rows
                            select new DivisionMarketImsBEL
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
                                MarketCode = row["MARKET_CODE"].ToString(),
                                MarketName = row["MARKET_NAME"].ToString(),
                                CustomerCode = row["CUSTOMER_CODE"].ToString(),
                                CustomerName = row["CUSTOMER_NAME"].ToString(),
                                DbLoaction = row["DB_LOCATION"].ToString(),

                                NoOfInvoice = Convert.ToInt32(row["NO_OF_INV"]),
                                TotalInvoiceAmount = Convert.ToDouble(row["TOTAL_INV_AMT"]),
                                SlabAdjustment = Convert.ToDouble(row["SLAB_ADJUSTMENT"]),
                                NetInvoiceAmount = Convert.ToDouble(row["NET_INV_AMOUNT"]),
                                ReturnValue = Convert.ToDouble(row["RETURN_VALUE"]),
                                ReturnSlabAdjustment = Convert.ToDouble(row["RETURN_SLAB_ADJUST"]),
                                NetReturnValue = Convert.ToDouble(row["NET_RETURN_VAL"]),
                                NetIms = Convert.ToDouble(row["NET_IMS"]),
                                NoOfReplaceInvoice = Convert.ToInt32(row["NO_OF_REPLACE_INV"]),
                                ReplaceInvoiceAmount = Convert.ToDouble(row["REPLACE_INV_AMT"]),
                                TargetValue = Convert.ToDouble(row["TARGET_VAL"]),
                                NoOfRetailer = Convert.ToInt32(row["NO_OF_RETAILER"]),
                                NoOfOrderRetailer = Convert.ToInt32(row["NO_OF_ORD_RETAILER"])
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
        public List<DivisionMarketImsBEL> GetDivMktImsLastSevendays(string dCode, string rCode, string aCode, string tCode, string cCode)
        {
            try
            {
                string qry = " SELECT DIVISION_CODE, DIVISION_NAME, REGION_CODE, REGION_NAME, AREA_CODE, AREA_NAME, TERRITORY_CODE, TERRITORY_NAME, MARKET_CODE, MARKET_NAME, CUSTOMER_CODE, CUSTOMER_NAME, DB_LOCATION, NO_OF_INV, TOTAL_INV_AMT, SLAB_ADJUSTMENT, NET_INV_AMOUNT, RETURN_VALUE, RETURN_SLAB_ADJUST, NET_RETURN_VAL, NET_IMS, NO_OF_REPLACE_INV, REPLACE_INV_AMT, TARGET_VAL, NO_OF_RETAILER, NO_OF_ORD_RETAILER " +
                              " FROM MV_DIVISION_MKT_IMS_LAST_7DAYS " +
                              " WHERE (DIVISION_CODE = '" + dCode + "' OR '" + dCode + "' = 'ALL')" +
                              " AND   (REGION_CODE='" + rCode + "' OR '" + rCode + "'='ALL')" +
                              " AND   (AREA_CODE='" + aCode + "' OR '" + aCode + "'='ALL')" +
                              " AND   (TERRITORY_CODE='" + tCode + "' OR '" + tCode + "'='ALL')" +
                              " AND   (CUSTOMER_CODE='" + cCode + "' OR '" + cCode + "'='ALL')" +
                              " ORDER BY DIVISION_CODE,REGION_CODE, AREA_CODE, TERRITORY_CODE, CUSTOMER_CODE,MARKET_CODE";


                //DataTable dt = _dbHelper.GetDataTable(qry);
                DataTable dt = _dbHelper.GetDataTableWithAuditTrial(qry, "Market Wise IMS (Last Seven Days)");
                var item = (from DataRow row in dt.Rows
                            select new DivisionMarketImsBEL
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
                                MarketCode = row["MARKET_CODE"].ToString(),
                                MarketName = row["MARKET_NAME"].ToString(),
                                CustomerCode = row["CUSTOMER_CODE"].ToString(),
                                CustomerName = row["CUSTOMER_NAME"].ToString(),
                                DbLoaction = row["DB_LOCATION"].ToString(),

                                NoOfInvoice = Convert.ToInt32(row["NO_OF_INV"]),
                                TotalInvoiceAmount = Convert.ToDouble(row["TOTAL_INV_AMT"]),
                                SlabAdjustment = Convert.ToDouble(row["SLAB_ADJUSTMENT"]),
                                NetInvoiceAmount = Convert.ToDouble(row["NET_INV_AMOUNT"]),
                                ReturnValue = Convert.ToDouble(row["RETURN_VALUE"]),
                                ReturnSlabAdjustment = Convert.ToDouble(row["RETURN_SLAB_ADJUST"]),
                                NetReturnValue = Convert.ToDouble(row["NET_RETURN_VAL"]),
                                NetIms = Convert.ToDouble(row["NET_IMS"]),
                                NoOfReplaceInvoice = Convert.ToInt32(row["NO_OF_REPLACE_INV"]),
                                ReplaceInvoiceAmount = Convert.ToDouble(row["REPLACE_INV_AMT"]),
                                TargetValue = Convert.ToDouble(row["TARGET_VAL"]),
                                NoOfRetailer = Convert.ToInt32(row["NO_OF_RETAILER"]),
                                NoOfOrderRetailer = Convert.ToInt32(row["NO_OF_ORD_RETAILER"])
                            }).ToList();
                return item;
            }
            catch (Exception e)
            {
                var lineNum = e.StackTrace.Substring(e.StackTrace.LastIndexOf(' '));
                _errorLogger.GetErrorMessage(e.Message, "DivisionMarketImsDAL", lineNum);
                ExceptionReturn = e.Message;
                throw;
            }
        }
        public List<DivisionMarketImsBEL> GetDivMktImsLastThirtydays(string dCode, string rCode, string aCode, string tCode, string cCode)
        {
            try
            {
                string qry = " SELECT DIVISION_CODE, DIVISION_NAME, REGION_CODE, REGION_NAME, AREA_CODE, AREA_NAME, TERRITORY_CODE, TERRITORY_NAME, MARKET_CODE, MARKET_NAME, CUSTOMER_CODE, CUSTOMER_NAME, DB_LOCATION, NO_OF_INV, TOTAL_INV_AMT, SLAB_ADJUSTMENT, NET_INV_AMOUNT, RETURN_VALUE, RETURN_SLAB_ADJUST, NET_RETURN_VAL, NET_IMS, NO_OF_REPLACE_INV, REPLACE_INV_AMT, TARGET_VAL, NO_OF_RETAILER, NO_OF_ORD_RETAILER " +
                              " FROM MV_DIVISION_MKT_IMS_LAST_30DAY " +
                              " WHERE (DIVISION_CODE = '" + dCode + "' OR '" + dCode + "' = 'ALL')" +
                              " AND   (REGION_CODE='" + rCode + "' OR '" + rCode + "'='ALL')" +
                              " AND   (AREA_CODE='" + aCode + "' OR '" + aCode + "'='ALL')" +
                              " AND   (TERRITORY_CODE='" + tCode + "' OR '" + tCode + "'='ALL')" +
                              " AND   (CUSTOMER_CODE='" + cCode + "' OR '" + cCode + "'='ALL')" +
                              " ORDER BY DIVISION_CODE,REGION_CODE, AREA_CODE, TERRITORY_CODE, CUSTOMER_CODE,MARKET_CODE";


                //DataTable dt = _dbHelper.GetDataTable(qry);
                DataTable dt = _dbHelper.GetDataTableWithAuditTrial(qry, "Market Wise IMS (Last Thirty Days)");
                var item = (from DataRow row in dt.Rows
                            select new DivisionMarketImsBEL
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
                                MarketCode = row["MARKET_CODE"].ToString(),
                                MarketName = row["MARKET_NAME"].ToString(),
                                CustomerCode = row["CUSTOMER_CODE"].ToString(),
                                CustomerName = row["CUSTOMER_NAME"].ToString(),
                                DbLoaction = row["DB_LOCATION"].ToString(),

                                NoOfInvoice = Convert.ToInt32(row["NO_OF_INV"]),
                                TotalInvoiceAmount = Convert.ToDouble(row["TOTAL_INV_AMT"]),
                                SlabAdjustment = Convert.ToDouble(row["SLAB_ADJUSTMENT"]),
                                NetInvoiceAmount = Convert.ToDouble(row["NET_INV_AMOUNT"]),
                                ReturnValue = Convert.ToDouble(row["RETURN_VALUE"]),
                                ReturnSlabAdjustment = Convert.ToDouble(row["RETURN_SLAB_ADJUST"]),
                                NetReturnValue = Convert.ToDouble(row["NET_RETURN_VAL"]),
                                NetIms = Convert.ToDouble(row["NET_IMS"]),
                                NoOfReplaceInvoice = Convert.ToInt32(row["NO_OF_REPLACE_INV"]),
                                ReplaceInvoiceAmount = Convert.ToDouble(row["REPLACE_INV_AMT"]),
                                TargetValue = Convert.ToDouble(row["TARGET_VAL"]),
                                NoOfRetailer = Convert.ToInt32(row["NO_OF_RETAILER"]),
                                NoOfOrderRetailer = Convert.ToInt32(row["NO_OF_ORD_RETAILER"])
                            }).ToList();
                return item;
            }
            catch (Exception e)
            {
                var lineNum = e.StackTrace.Substring(e.StackTrace.LastIndexOf(' '));
                _errorLogger.GetErrorMessage(e.Message, "DivisionMarketImsDAL", lineNum);
                ExceptionReturn = e.Message;
                throw;
            }
        }
        public List<DivisionMarketImsBEL> GetDivMktImsCurrentMonth(string dCode, string rCode, string aCode, string tCode, string cCode)
        {
            try
            {
                string qry = " SELECT DIVISION_CODE, DIVISION_NAME, REGION_CODE, REGION_NAME, AREA_CODE, AREA_NAME, TERRITORY_CODE, TERRITORY_NAME, MARKET_CODE, MARKET_NAME, CUSTOMER_CODE, CUSTOMER_NAME, DB_LOCATION, NO_OF_INV, TOTAL_INV_AMT, SLAB_ADJUSTMENT, NET_INV_AMOUNT, RETURN_VALUE, RETURN_SLAB_ADJUST, NET_RETURN_VAL, NET_IMS, NO_OF_REPLACE_INV, REPLACE_INV_AMT, TARGET_VAL, NO_OF_RETAILER, NO_OF_ORD_RETAILER " +
                              " FROM MV_DIVISION_MARKET_IMS_CMONTH " +
                              " WHERE (DIVISION_CODE = '" + dCode + "' OR '" + dCode + "' = 'ALL')" +
                              " AND   (REGION_CODE='" + rCode + "' OR '" + rCode + "'='ALL')" +
                              " AND   (AREA_CODE='" + aCode + "' OR '" + aCode + "'='ALL')" +
                              " AND   (TERRITORY_CODE='" + tCode + "' OR '" + tCode + "'='ALL')" +
                              " AND   (CUSTOMER_CODE='" + cCode + "' OR '" + cCode + "'='ALL')" +
                              " ORDER BY DIVISION_CODE,REGION_CODE, AREA_CODE, TERRITORY_CODE, CUSTOMER_CODE,MARKET_CODE";


                //DataTable dt = _dbHelper.GetDataTable(qry);
                DataTable dt = _dbHelper.GetDataTableWithAuditTrial(qry, "Market Wise IMS (Current Month)");
                var item = (from DataRow row in dt.Rows
                            select new DivisionMarketImsBEL
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
                                MarketCode = row["MARKET_CODE"].ToString(),
                                MarketName = row["MARKET_NAME"].ToString(),
                                CustomerCode = row["CUSTOMER_CODE"].ToString(),
                                CustomerName = row["CUSTOMER_NAME"].ToString(),
                                DbLoaction = row["DB_LOCATION"].ToString(),

                                NoOfInvoice = Convert.ToInt32(row["NO_OF_INV"]),
                                TotalInvoiceAmount = Convert.ToDouble(row["TOTAL_INV_AMT"]),
                                SlabAdjustment = Convert.ToDouble(row["SLAB_ADJUSTMENT"]),
                                NetInvoiceAmount = Convert.ToDouble(row["NET_INV_AMOUNT"]),
                                ReturnValue = Convert.ToDouble(row["RETURN_VALUE"]),
                                ReturnSlabAdjustment = Convert.ToDouble(row["RETURN_SLAB_ADJUST"]),
                                NetReturnValue = Convert.ToDouble(row["NET_RETURN_VAL"]),
                                NetIms = Convert.ToDouble(row["NET_IMS"]),
                                NoOfReplaceInvoice = Convert.ToInt32(row["NO_OF_REPLACE_INV"]),
                                ReplaceInvoiceAmount = Convert.ToDouble(row["REPLACE_INV_AMT"]),
                                TargetValue = Convert.ToDouble(row["TARGET_VAL"]),
                                NoOfRetailer = Convert.ToInt32(row["NO_OF_RETAILER"]),
                                NoOfOrderRetailer = Convert.ToInt32(row["NO_OF_ORD_RETAILER"])
                            }).ToList();
                return item;
            }
            catch (Exception e)
            {
                var lineNum = e.StackTrace.Substring(e.StackTrace.LastIndexOf(' '));
                _errorLogger.GetErrorMessage(e.Message, "DivisionMarketImsDAL", lineNum);
                ExceptionReturn = e.Message;
                throw;
            }
        }
        public List<DivisionMarketImsBEL> GetDivMktImsLastMonth(string dCode, string rCode, string aCode, string tCode, string cCode)
        {
            try
            {
                string qry = " SELECT DIVISION_CODE, DIVISION_NAME, REGION_CODE, REGION_NAME, AREA_CODE, AREA_NAME, TERRITORY_CODE, TERRITORY_NAME, MARKET_CODE, MARKET_NAME, CUSTOMER_CODE, CUSTOMER_NAME, DB_LOCATION, NO_OF_INV, TOTAL_INV_AMT, SLAB_ADJUSTMENT, NET_INV_AMOUNT, RETURN_VALUE, RETURN_SLAB_ADJUST, NET_RETURN_VAL, NET_IMS, NO_OF_REPLACE_INV, REPLACE_INV_AMT, TARGET_VAL, NO_OF_RETAILER, NO_OF_ORD_RETAILER " +
                              " FROM MV_DIVISION_MARKET_IMS_LMONTH " +
                              " WHERE (DIVISION_CODE = '" + dCode + "' OR '" + dCode + "' = 'ALL')" +
                              " AND   (REGION_CODE='" + rCode + "' OR '" + rCode + "'='ALL')" +
                              " AND   (AREA_CODE='" + aCode + "' OR '" + aCode + "'='ALL')" +
                              " AND   (TERRITORY_CODE='" + tCode + "' OR '" + tCode + "'='ALL')" +
                              " AND   (CUSTOMER_CODE='" + cCode + "' OR '" + cCode + "'='ALL')" +
                              " ORDER BY DIVISION_CODE,REGION_CODE, AREA_CODE, TERRITORY_CODE, CUSTOMER_CODE,MARKET_CODE";


                //DataTable dt = _dbHelper.GetDataTable(qry);
                DataTable dt = _dbHelper.GetDataTableWithAuditTrial(qry, "Market Wise IMS (Last Month)");
                var item = (from DataRow row in dt.Rows
                            select new DivisionMarketImsBEL
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
                                MarketCode = row["MARKET_CODE"].ToString(),
                                MarketName = row["MARKET_NAME"].ToString(),
                                CustomerCode = row["CUSTOMER_CODE"].ToString(),
                                CustomerName = row["CUSTOMER_NAME"].ToString(),
                                DbLoaction = row["DB_LOCATION"].ToString(),

                                NoOfInvoice = Convert.ToInt32(row["NO_OF_INV"]),
                                TotalInvoiceAmount = Convert.ToDouble(row["TOTAL_INV_AMT"]),
                                SlabAdjustment = Convert.ToDouble(row["SLAB_ADJUSTMENT"]),
                                NetInvoiceAmount = Convert.ToDouble(row["NET_INV_AMOUNT"]),
                                ReturnValue = Convert.ToDouble(row["RETURN_VALUE"]),
                                ReturnSlabAdjustment = Convert.ToDouble(row["RETURN_SLAB_ADJUST"]),
                                NetReturnValue = Convert.ToDouble(row["NET_RETURN_VAL"]),
                                NetIms = Convert.ToDouble(row["NET_IMS"]),
                                NoOfReplaceInvoice = Convert.ToInt32(row["NO_OF_REPLACE_INV"]),
                                ReplaceInvoiceAmount = Convert.ToDouble(row["REPLACE_INV_AMT"]),
                                TargetValue = Convert.ToDouble(row["TARGET_VAL"]),
                                NoOfRetailer = Convert.ToInt32(row["NO_OF_RETAILER"]),
                                NoOfOrderRetailer = Convert.ToInt32(row["NO_OF_ORD_RETAILER"])
                            }).ToList();
                return item;
            }
            catch (Exception e)
            {
                var lineNum = e.StackTrace.Substring(e.StackTrace.LastIndexOf(' '));
                _errorLogger.GetErrorMessage(e.Message, "DivisionMarketImsDAL", lineNum);
                ExceptionReturn = e.Message;
                throw;
            }
        }



    }
}