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
    public class MarketProductImsDAL :ReturnData
    {



        private readonly DBHelper _dbHelper = new DBHelper();
        private readonly ErrorLogger _errorLogger = new ErrorLogger();
        private static readonly DBConnection DBConnection = new DBConnection();
        public readonly string ConnString = DBConnection.SAConnStrReader("Oracle", "PHARMAERP");

        //Function
        public object GetMarketProductImsToday(string dCode, string rCode, string aCode, string tCode, string cCode)
        {
            try
            {

                int userId = Convert.ToInt32(HttpContext.Current.Session["USER_ID"]);

                using (OracleConnection objConn = new OracleConnection(ConnString))
                {
                    using (OracleCommand objCmd = new OracleCommand())
                    {
                        objCmd.Connection = objConn;
                        objCmd.CommandText = "FN_MARKET_PROD_IMS_TODAY";
                        objCmd.CommandType = CommandType.StoredProcedure;


                        objCmd.Parameters.Add("pdivision_code", OracleType.VarChar).Value = dCode;
                        objCmd.Parameters.Add("pregion_code", OracleType.VarChar).Value = rCode;
                        objCmd.Parameters.Add("parea_code", OracleType.VarChar).Value = aCode;
                        objCmd.Parameters.Add("pterritory_code", OracleType.VarChar).Value = tCode;
                        objCmd.Parameters.Add("pcustomer_code", OracleType.VarChar).Value = cCode;
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
                        _dbHelper.InsertReportAudit("Market Wise Product IMS (Today)");
                        List<MarketProductImsBEL> item;
                        item = (from DataRow row in dt.Rows
                                select new MarketProductImsBEL
                                {
                                    DivisionCode = row["DIVISION_CODE"].ToString(),
                                    DivisionName = row["DIVISION_NAME"].ToString(),
                                    RegionCode = row["REGION_CODE"].ToString(),
                                    RegionName = row["REGION_NAME"].ToString(),
                                    AreaCode = row["AREA_CODE"].ToString(),
                                    AreaName = row["AREA_NAME"].ToString(),
                                    TerritoryCode = row["TERRITORY_CODE"].ToString(),
                                    TerritoryName = row["TERRITORY_NAME"].ToString(),
                                    DBLoaction = row["DB_LOCATION"].ToString(),
                                    CustomerCode = row["CUSTOMER_CODE"].ToString(),
                                    CustomerName = row["CUSTOMER_NAME"].ToString(),
                                    MarketCode = row["MARKET_CODE"].ToString(),
                                    MarketName = row["MARKET_NAME"].ToString(),
                                    ProductCode = row["PRODUCT_CODE"].ToString(),
                                    ProductName = row["PRODUCT_NAME"].ToString(),
                                    PackSize = row["PACK_SIZE"].ToString(),
                                    ProductPrice = Convert.ToDouble(row["PRODUCT_PRICE"]),

                                    InvoiceQty = Convert.ToInt32(row["SALES_QTY"]),
                                    ReturnSalesQty = Convert.ToInt32(row["RETURN_SALES_QTY"]),
                                    IMSSalesQty = Convert.ToInt32(row["IMS_SALES_QTY"]),

                                    InvBonusQty = Convert.ToInt32(row["BONUS_QTY"]),
                                    ReturnBnsQty = Convert.ToInt32(row["RETURN_BONUS_QTY"]),
                                    IMSBnsQty = Convert.ToInt32(row["IMS_BONUS_QTY"]),

                                    InvoiceAmt = Convert.ToDouble(row["INVOICE_VALUE"]),
                                    ReturnValue = Convert.ToDouble(row["RETURN_VALUE"]),
                                    IMSSalesVal = Convert.ToDouble(row["IMS_SALES_VAL"]),
                                    IMSBnsVal = Convert.ToDouble(row["IMS_BONUS_VAL"]),

                                    BonusPriceDiscount = Convert.ToDouble(row["BONUS_PRICE_DISCOUNT"]),
                                    BnsDiscRet = Convert.ToDouble(row["RET_BONUS_PRICE_DISCOUNT"]),

                                    NetIMS = Convert.ToDouble(row["NET_IMS"]),
                                    BonusPer = Convert.ToInt32(row["BONUS_PER"]),
                                    TargetQty = Convert.ToInt32(row["TARGET_QTY"]),
                                    TargetVal = Convert.ToDouble(row["TARGET_VAL"])

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
        public object GetMarketProductImsDateRange(string fDate, string tDate, string dCode, string rCode, string aCode, string tCode, string cCode)
        {
            try
            {

                int userId = Convert.ToInt32(HttpContext.Current.Session["USER_ID"]);

                using (OracleConnection objConn = new OracleConnection(ConnString))
                {
                    using (OracleCommand objCmd = new OracleCommand())
                    {
                        objCmd.Connection = objConn;
                        objCmd.CommandText = "FN_MARKET_PROD_IMS_DATE_RANGE";
                        objCmd.CommandType = CommandType.StoredProcedure;
                        objCmd.Parameters.Add("pfrom_date", OracleType.VarChar).Value = fDate;
                        objCmd.Parameters.Add("pto_date", OracleType.VarChar).Value = tDate;
                        objCmd.Parameters.Add("pdivision_code", OracleType.VarChar).Value = dCode;
                        objCmd.Parameters.Add("pregion_code", OracleType.VarChar).Value = rCode;
                        objCmd.Parameters.Add("parea_code", OracleType.VarChar).Value = aCode;
                        objCmd.Parameters.Add("pterritory_code", OracleType.VarChar).Value = tCode;
                        objCmd.Parameters.Add("pcustomer_code", OracleType.VarChar).Value = cCode;
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
                        _dbHelper.InsertReportAudit("Market Wise Product IMS (Custom Date)");
                        List<MarketProductImsBEL> item;
                        item = (from DataRow row in dt.Rows
                                select new MarketProductImsBEL
                                {
                                    DivisionCode = row["DIVISION_CODE"].ToString(),
                                    DivisionName = row["DIVISION_NAME"].ToString(),
                                    RegionCode = row["REGION_CODE"].ToString(),
                                    RegionName = row["REGION_NAME"].ToString(),
                                    AreaCode = row["AREA_CODE"].ToString(),
                                    AreaName = row["AREA_NAME"].ToString(),
                                    TerritoryCode = row["TERRITORY_CODE"].ToString(),
                                    TerritoryName = row["TERRITORY_NAME"].ToString(),
                                    DBLoaction = row["DB_LOCATION"].ToString(),
                                    CustomerCode = row["CUSTOMER_CODE"].ToString(),
                                    CustomerName = row["CUSTOMER_NAME"].ToString(),
                                    MarketCode = row["MARKET_CODE"].ToString(),
                                    MarketName = row["MARKET_NAME"].ToString(),
                                    ProductCode = row["PRODUCT_CODE"].ToString(),
                                    ProductName = row["PRODUCT_NAME"].ToString(),
                                    PackSize = row["PACK_SIZE"].ToString(),
                                    ProductPrice = Convert.ToDouble(row["PRODUCT_PRICE"]),

                                    InvoiceQty = Convert.ToInt32(row["SALES_QTY"]),
                                    ReturnSalesQty = Convert.ToInt32(row["RETURN_SALES_QTY"]),
                                    IMSSalesQty = Convert.ToInt32(row["IMS_SALES_QTY"]),

                                    InvBonusQty = Convert.ToInt32(row["BONUS_QTY"]),
                                    ReturnBnsQty = Convert.ToInt32(row["RETURN_BONUS_QTY"]),
                                    IMSBnsQty = Convert.ToInt32(row["IMS_BONUS_QTY"]),

                                    InvoiceAmt = Convert.ToDouble(row["INVOICE_VALUE"]),
                                    ReturnValue = Convert.ToDouble(row["RETURN_VALUE"]),
                                    IMSSalesVal = Convert.ToDouble(row["IMS_SALES_VAL"]),
                                    IMSBnsVal = Convert.ToDouble(row["IMS_BONUS_VAL"]),

                                    BonusPriceDiscount = Convert.ToDouble(row["BONUS_PRICE_DISCOUNT"]),
                                    BnsDiscRet = Convert.ToDouble(row["RET_BONUS_PRICE_DISCOUNT"]),

                                    NetIMS = Convert.ToDouble(row["NET_IMS"]),
                                    BonusPer = Convert.ToInt32(row["BONUS_PER"]),
                                    TargetQty = Convert.ToInt32(row["TARGET_QTY"]),
                                    TargetVal = Convert.ToDouble(row["TARGET_VAL"])

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





        //Query
        public List<MarketProductImsBEL> GetMarketProductImsYesterday(string dCode, string rCode, string aCode, string tCode, string cCode)
        {
            try
            {
                string qry;

                int userId = Convert.ToInt32(HttpContext.Current.Session["USER_ID"]);
                int UserAccessCount = Convert.ToInt32(HttpContext.Current.Session["USER_BASE_REPORT_FILTER"]);

                if (UserAccessCount > 0)
                {

                    qry = " SELECT DIVISION_CODE, DIVISION_NAME, REGION_CODE, REGION_NAME, AREA_CODE, AREA_NAME, TERRITORY_CODE, TERRITORY_NAME, DB_LOCATION, CUSTOMER_CODE, CUSTOMER_NAME, MARKET_CODE, MARKET_NAME, PRODUCT_CODE, PRODUCT_NAME, PACK_SIZE, PRODUCT_PRICE, SALES_QTY, RETURN_SALES_QTY, IMS_SALES_QTY, BONUS_QTY, RETURN_BONUS_QTY, IMS_BONUS_QTY, INVOICE_VALUE, RETURN_VALUE, IMS_SALES_VAL, IMS_BONUS_VAL, BONUS_PRICE_DISCOUNT, RET_BONUS_PRICE_DISCOUNT, NET_IMS, BONUS_PER, TARGET_QTY, TARGET_VAL" +
                          " FROM MV_MARKET_PROD_IMS_YESTERDAY " +
                          " WHERE (DIVISION_CODE = '" + dCode + "' OR '" + dCode + "' = 'ALL')" +
                          " AND   (REGION_CODE='" + rCode + "' OR '" + rCode + "'='ALL')" +
                          " AND   (AREA_CODE='" + aCode + "' OR '" + aCode + "'='ALL')" +
                          " AND   (TERRITORY_CODE='" + tCode + "' OR '" + tCode + "'='ALL')" +
                          " AND   (CUSTOMER_CODE='" + cCode + "' OR '" + cCode + "'='ALL')" +
                          " AND   PRODUCT_CODE IN (SELECT PRODUCT_CODE FROM USER_PRODUCT_DTL WHERE USER_ID=" + userId + ")" +
                          " ORDER BY DIVISION_CODE,REGION_CODE, AREA_CODE, TERRITORY_CODE, CUSTOMER_CODE,MARKET_CODE, PRODUCT_CODE";

                }
                else
                {

                    qry = " SELECT DIVISION_CODE, DIVISION_NAME, REGION_CODE, REGION_NAME, AREA_CODE, AREA_NAME, TERRITORY_CODE, TERRITORY_NAME, DB_LOCATION, CUSTOMER_CODE, CUSTOMER_NAME, MARKET_CODE, MARKET_NAME, PRODUCT_CODE, PRODUCT_NAME, PACK_SIZE, PRODUCT_PRICE, SALES_QTY, RETURN_SALES_QTY, IMS_SALES_QTY, BONUS_QTY, RETURN_BONUS_QTY, IMS_BONUS_QTY, INVOICE_VALUE, RETURN_VALUE, IMS_SALES_VAL, IMS_BONUS_VAL, BONUS_PRICE_DISCOUNT, RET_BONUS_PRICE_DISCOUNT, NET_IMS, BONUS_PER, TARGET_QTY, TARGET_VAL" +
                          " FROM MV_MARKET_PROD_IMS_YESTERDAY " +
                          " WHERE (DIVISION_CODE = '" + dCode + "' OR '" + dCode + "' = 'ALL')" +
                          " AND   (REGION_CODE='" + rCode + "' OR '" + rCode + "'='ALL')" +
                          " AND   (AREA_CODE='" + aCode + "' OR '" + aCode + "'='ALL')" +
                          " AND   (TERRITORY_CODE='" + tCode + "' OR '" + tCode + "'='ALL')" +
                          " AND   (CUSTOMER_CODE='" + cCode + "' OR '" + cCode + "'='ALL')" +
                          " ORDER BY DIVISION_CODE,REGION_CODE, AREA_CODE, TERRITORY_CODE, CUSTOMER_CODE,MARKET_CODE, PRODUCT_CODE";


                }




                //DataTable dt = _dbHelper.GetDataTable(qry);
                DataTable dt = _dbHelper.GetDataTableWithAuditTrial(qry, "Market Wise Product IMS (Yester)");
                var item = (from DataRow row in dt.Rows
                            select new MarketProductImsBEL
                            {
                                DivisionCode = row["DIVISION_CODE"].ToString(),
                                DivisionName = row["DIVISION_NAME"].ToString(),
                                RegionCode = row["REGION_CODE"].ToString(),
                                RegionName = row["REGION_NAME"].ToString(),
                                AreaCode = row["AREA_CODE"].ToString(),
                                AreaName = row["AREA_NAME"].ToString(),
                                TerritoryCode = row["TERRITORY_CODE"].ToString(),
                                TerritoryName = row["TERRITORY_NAME"].ToString(),
                                DBLoaction = row["DB_LOCATION"].ToString(),
                                CustomerCode = row["CUSTOMER_CODE"].ToString(),
                                CustomerName = row["CUSTOMER_NAME"].ToString(),
                                MarketCode = row["MARKET_CODE"].ToString(),
                                MarketName = row["MARKET_NAME"].ToString(),
                                ProductCode = row["PRODUCT_CODE"].ToString(),
                                ProductName = row["PRODUCT_NAME"].ToString(),
                                PackSize = row["PACK_SIZE"].ToString(),
                                ProductPrice = Convert.ToDouble(row["PRODUCT_PRICE"]),

                                InvoiceQty = Convert.ToInt32(row["SALES_QTY"]),
                                ReturnSalesQty = Convert.ToInt32(row["RETURN_SALES_QTY"]),
                                IMSSalesQty = Convert.ToInt32(row["IMS_SALES_QTY"]),

                                InvBonusQty = Convert.ToInt32(row["BONUS_QTY"]),
                                ReturnBnsQty = Convert.ToInt32(row["RETURN_BONUS_QTY"]),
                                IMSBnsQty = Convert.ToInt32(row["IMS_BONUS_QTY"]),

                                InvoiceAmt = Convert.ToDouble(row["INVOICE_VALUE"]),
                                ReturnValue = Convert.ToDouble(row["RETURN_VALUE"]),
                                IMSSalesVal = Convert.ToDouble(row["IMS_SALES_VAL"]),
                                IMSBnsVal = Convert.ToDouble(row["IMS_BONUS_VAL"]),

                                BonusPriceDiscount = Convert.ToDouble(row["BONUS_PRICE_DISCOUNT"]),
                                BnsDiscRet = Convert.ToDouble(row["RET_BONUS_PRICE_DISCOUNT"]),

                                NetIMS = Convert.ToDouble(row["NET_IMS"]),
                                BonusPer = Convert.ToInt32(row["BONUS_PER"]),
                                TargetQty = Convert.ToInt32(row["TARGET_QTY"]),
                                TargetVal = Convert.ToDouble(row["TARGET_VAL"])
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

        //Query
        public List<MarketProductImsBEL> GetMarketProductImsLastSevendays(string dCode, string rCode, string aCode, string tCode, string cCode)
        {
            try
            {
                string qry;

                int userId = Convert.ToInt32(HttpContext.Current.Session["USER_ID"]);
                int UserAccessCount = Convert.ToInt32(HttpContext.Current.Session["USER_BASE_REPORT_FILTER"]);

                if (UserAccessCount > 0)
                {

                    qry = " SELECT DIVISION_CODE, DIVISION_NAME, REGION_CODE, REGION_NAME, AREA_CODE, AREA_NAME, TERRITORY_CODE, TERRITORY_NAME, DB_LOCATION, CUSTOMER_CODE, CUSTOMER_NAME, MARKET_CODE, MARKET_NAME, PRODUCT_CODE, PRODUCT_NAME, PACK_SIZE, PRODUCT_PRICE, SALES_QTY, RETURN_SALES_QTY, IMS_SALES_QTY, BONUS_QTY, RETURN_BONUS_QTY, IMS_BONUS_QTY, INVOICE_VALUE, RETURN_VALUE, IMS_SALES_VAL, IMS_BONUS_VAL, BONUS_PRICE_DISCOUNT, RET_BONUS_PRICE_DISCOUNT, NET_IMS, BONUS_PER, TARGET_QTY, TARGET_VAL " +
                          " FROM MV_MARKET_PROD_IMS_LAST_7DAY " +
                          " WHERE (DIVISION_CODE = '" + dCode + "' OR '" + dCode + "' = 'ALL')" +
                          " AND   (REGION_CODE='" + rCode + "' OR '" + rCode + "'='ALL')" +
                          " AND   (AREA_CODE='" + aCode + "' OR '" + aCode + "'='ALL')" +
                          " AND   (TERRITORY_CODE='" + tCode + "' OR '" + tCode + "'='ALL')" +
                          " AND   (CUSTOMER_CODE='" + cCode + "' OR '" + cCode + "'='ALL')" +
                          " AND   PRODUCT_CODE IN (SELECT PRODUCT_CODE FROM USER_PRODUCT_DTL WHERE USER_ID=" + userId + ")" +
                          " ORDER BY DIVISION_CODE,REGION_CODE, AREA_CODE, TERRITORY_CODE, CUSTOMER_CODE, MARKET_CODE, PRODUCT_CODE";

                }
                else
                {

                    qry = " SELECT DIVISION_CODE, DIVISION_NAME, REGION_CODE, REGION_NAME, AREA_CODE, AREA_NAME, TERRITORY_CODE, TERRITORY_NAME, DB_LOCATION, CUSTOMER_CODE, CUSTOMER_NAME, MARKET_CODE, MARKET_NAME, PRODUCT_CODE, PRODUCT_NAME, PACK_SIZE, PRODUCT_PRICE, SALES_QTY, RETURN_SALES_QTY, IMS_SALES_QTY, BONUS_QTY, RETURN_BONUS_QTY, IMS_BONUS_QTY, INVOICE_VALUE, RETURN_VALUE, IMS_SALES_VAL, IMS_BONUS_VAL, BONUS_PRICE_DISCOUNT, RET_BONUS_PRICE_DISCOUNT, NET_IMS, BONUS_PER, TARGET_QTY, TARGET_VAL " +
                          " FROM MV_MARKET_PROD_IMS_LAST_7DAY " +
                          " WHERE (DIVISION_CODE = '" + dCode + "' OR '" + dCode + "' = 'ALL')" +
                          " AND   (REGION_CODE='" + rCode + "' OR '" + rCode + "'='ALL')" +
                          " AND   (AREA_CODE='" + aCode + "' OR '" + aCode + "'='ALL')" +
                          " AND   (TERRITORY_CODE='" + tCode + "' OR '" + tCode + "'='ALL')" +
                          " AND   (CUSTOMER_CODE='" + cCode + "' OR '" + cCode + "'='ALL')" +
                          " ORDER BY DIVISION_CODE,REGION_CODE, AREA_CODE, TERRITORY_CODE, CUSTOMER_CODE, MARKET_CODE, PRODUCT_CODE";

                }




                //DataTable dt = _dbHelper.GetDataTable(qry);
                DataTable dt = _dbHelper.GetDataTableWithAuditTrial(qry, "Market Wise Product IMS (Last Seven Days)");

                var item = (from DataRow row in dt.Rows
                            select new MarketProductImsBEL
                            {
                                DivisionCode = row["DIVISION_CODE"].ToString(),
                                DivisionName = row["DIVISION_NAME"].ToString(),
                                RegionCode = row["REGION_CODE"].ToString(),
                                RegionName = row["REGION_NAME"].ToString(),
                                AreaCode = row["AREA_CODE"].ToString(),
                                AreaName = row["AREA_NAME"].ToString(),
                                TerritoryCode = row["TERRITORY_CODE"].ToString(),
                                TerritoryName = row["TERRITORY_NAME"].ToString(),
                                DBLoaction = row["DB_LOCATION"].ToString(),
                                CustomerCode = row["CUSTOMER_CODE"].ToString(),
                                CustomerName = row["CUSTOMER_NAME"].ToString(),
                                MarketCode = row["MARKET_CODE"].ToString(),
                                MarketName = row["MARKET_NAME"].ToString(),
                                ProductCode = row["PRODUCT_CODE"].ToString(),
                                ProductName = row["PRODUCT_NAME"].ToString(),
                                PackSize = row["PACK_SIZE"].ToString(),
                                ProductPrice = Convert.ToDouble(row["PRODUCT_PRICE"]),

                                InvoiceQty = Convert.ToInt32(row["SALES_QTY"]),
                                ReturnSalesQty = Convert.ToInt32(row["RETURN_SALES_QTY"]),
                                IMSSalesQty = Convert.ToInt32(row["IMS_SALES_QTY"]),

                                InvBonusQty = Convert.ToInt32(row["BONUS_QTY"]),
                                ReturnBnsQty = Convert.ToInt32(row["RETURN_BONUS_QTY"]),
                                IMSBnsQty = Convert.ToInt32(row["IMS_BONUS_QTY"]),

                                InvoiceAmt = Convert.ToDouble(row["INVOICE_VALUE"]),
                                ReturnValue = Convert.ToDouble(row["RETURN_VALUE"]),
                                IMSSalesVal = Convert.ToDouble(row["IMS_SALES_VAL"]),
                                IMSBnsVal = Convert.ToDouble(row["IMS_BONUS_VAL"]),

                                BonusPriceDiscount = Convert.ToDouble(row["BONUS_PRICE_DISCOUNT"]),
                                BnsDiscRet = Convert.ToDouble(row["RET_BONUS_PRICE_DISCOUNT"]),

                                NetIMS = Convert.ToDouble(row["NET_IMS"]),
                                BonusPer = Convert.ToInt32(row["BONUS_PER"]),
                                TargetQty = Convert.ToInt32(row["TARGET_QTY"]),
                                TargetVal = Convert.ToDouble(row["TARGET_VAL"])
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


        //Query
        public List<MarketProductImsBEL> GetMarketProductImsLastThirtydays(string dCode, string rCode, string aCode, string tCode, string cCode)
        {
            try
            {

                string qry;

                int userId = Convert.ToInt32(HttpContext.Current.Session["USER_ID"]);
                int UserAccessCount = Convert.ToInt32(HttpContext.Current.Session["USER_BASE_REPORT_FILTER"]);

                if (UserAccessCount > 0)
                {

                    qry = " SELECT DIVISION_CODE, DIVISION_NAME, REGION_CODE, REGION_NAME, AREA_CODE, AREA_NAME, TERRITORY_CODE, TERRITORY_NAME, DB_LOCATION, CUSTOMER_CODE, CUSTOMER_NAME, MARKET_CODE, MARKET_NAME, PRODUCT_CODE, PRODUCT_NAME, PACK_SIZE, PRODUCT_PRICE, SALES_QTY, RETURN_SALES_QTY, IMS_SALES_QTY, BONUS_QTY, RETURN_BONUS_QTY, IMS_BONUS_QTY, INVOICE_VALUE, RETURN_VALUE, IMS_SALES_VAL, IMS_BONUS_VAL, BONUS_PRICE_DISCOUNT, RET_BONUS_PRICE_DISCOUNT, NET_IMS, BONUS_PER, TARGET_QTY, TARGET_VAL " +
                          " FROM MV_MARKET_PROD_IMS_LAST_30DAY " +
                          " WHERE (DIVISION_CODE = '" + dCode + "' OR '" + dCode + "' = 'ALL')" +
                          " AND   (REGION_CODE='" + rCode + "' OR '" + rCode + "'='ALL')" +
                          " AND   (AREA_CODE='" + aCode + "' OR '" + aCode + "'='ALL')" +
                          " AND   (TERRITORY_CODE='" + tCode + "' OR '" + tCode + "'='ALL')" +
                          " AND   (CUSTOMER_CODE='" + cCode + "' OR '" + cCode + "'='ALL')" +
                          " AND   PRODUCT_CODE IN (SELECT PRODUCT_CODE FROM USER_PRODUCT_DTL WHERE USER_ID=" + userId + ")" +
                          " ORDER BY DIVISION_CODE,REGION_CODE, AREA_CODE, TERRITORY_CODE, CUSTOMER_CODE, MARKET_CODE, PRODUCT_CODE";

                }
                else
                {

                    qry = " SELECT DIVISION_CODE, DIVISION_NAME, REGION_CODE, REGION_NAME, AREA_CODE, AREA_NAME, TERRITORY_CODE, TERRITORY_NAME, DB_LOCATION, CUSTOMER_CODE, CUSTOMER_NAME, MARKET_CODE, MARKET_NAME, PRODUCT_CODE, PRODUCT_NAME, PACK_SIZE, PRODUCT_PRICE, SALES_QTY, RETURN_SALES_QTY, IMS_SALES_QTY, BONUS_QTY, RETURN_BONUS_QTY, IMS_BONUS_QTY, INVOICE_VALUE, RETURN_VALUE, IMS_SALES_VAL, IMS_BONUS_VAL, BONUS_PRICE_DISCOUNT, RET_BONUS_PRICE_DISCOUNT, NET_IMS, BONUS_PER, TARGET_QTY, TARGET_VAL " +
                          " FROM MV_MARKET_PROD_IMS_LAST_30DAY " +
                          " WHERE (DIVISION_CODE = '" + dCode + "' OR '" + dCode + "' = 'ALL')" +
                          " AND   (REGION_CODE='" + rCode + "' OR '" + rCode + "'='ALL')" +
                          " AND   (AREA_CODE='" + aCode + "' OR '" + aCode + "'='ALL')" +
                          " AND   (TERRITORY_CODE='" + tCode + "' OR '" + tCode + "'='ALL')" +
                          " AND   (CUSTOMER_CODE='" + cCode + "' OR '" + cCode + "'='ALL')" +
                          " ORDER BY DIVISION_CODE,REGION_CODE, AREA_CODE, TERRITORY_CODE, CUSTOMER_CODE, MARKET_CODE, PRODUCT_CODE";

                }




                //DataTable dt = _dbHelper.GetDataTable(qry);
                DataTable dt = _dbHelper.GetDataTableWithAuditTrial(qry, "Market Wise Product IMS (Last Thirty Days)");
                var item = (from DataRow row in dt.Rows
                            select new MarketProductImsBEL
                            {
                                DivisionCode = row["DIVISION_CODE"].ToString(),
                                DivisionName = row["DIVISION_NAME"].ToString(),
                                RegionCode = row["REGION_CODE"].ToString(),
                                RegionName = row["REGION_NAME"].ToString(),
                                AreaCode = row["AREA_CODE"].ToString(),
                                AreaName = row["AREA_NAME"].ToString(),
                                TerritoryCode = row["TERRITORY_CODE"].ToString(),
                                TerritoryName = row["TERRITORY_NAME"].ToString(),
                                DBLoaction = row["DB_LOCATION"].ToString(),
                                CustomerCode = row["CUSTOMER_CODE"].ToString(),
                                CustomerName = row["CUSTOMER_NAME"].ToString(),
                                MarketCode = row["MARKET_CODE"].ToString(),
                                MarketName = row["MARKET_NAME"].ToString(),
                                ProductCode = row["PRODUCT_CODE"].ToString(),
                                ProductName = row["PRODUCT_NAME"].ToString(),
                                PackSize = row["PACK_SIZE"].ToString(),
                                ProductPrice = Convert.ToDouble(row["PRODUCT_PRICE"]),

                                InvoiceQty = Convert.ToInt32(row["SALES_QTY"]),
                                ReturnSalesQty = Convert.ToInt32(row["RETURN_SALES_QTY"]),
                                IMSSalesQty = Convert.ToInt32(row["IMS_SALES_QTY"]),

                                InvBonusQty = Convert.ToInt32(row["BONUS_QTY"]),
                                ReturnBnsQty = Convert.ToInt32(row["RETURN_BONUS_QTY"]),
                                IMSBnsQty = Convert.ToInt32(row["IMS_BONUS_QTY"]),

                                InvoiceAmt = Convert.ToDouble(row["INVOICE_VALUE"]),
                                ReturnValue = Convert.ToDouble(row["RETURN_VALUE"]),
                                IMSSalesVal = Convert.ToDouble(row["IMS_SALES_VAL"]),
                                IMSBnsVal = Convert.ToDouble(row["IMS_BONUS_VAL"]),

                                BonusPriceDiscount = Convert.ToDouble(row["BONUS_PRICE_DISCOUNT"]),
                                BnsDiscRet = Convert.ToDouble(row["RET_BONUS_PRICE_DISCOUNT"]),

                                NetIMS = Convert.ToDouble(row["NET_IMS"]),
                                BonusPer = Convert.ToInt32(row["BONUS_PER"]),
                                TargetQty = Convert.ToInt32(row["TARGET_QTY"]),
                                TargetVal = Convert.ToDouble(row["TARGET_VAL"])
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

        //Query
        public List<MarketProductImsBEL> GetMarketProductImsCurrentMonth(string dCode, string rCode, string aCode, string tCode, string cCode)
        {
            try
            {

                string qry;

                int userId = Convert.ToInt32(HttpContext.Current.Session["USER_ID"]);
                int UserAccessCount = Convert.ToInt32(HttpContext.Current.Session["USER_BASE_REPORT_FILTER"]);

                if (UserAccessCount > 0)
                {

                    qry = " SELECT DIVISION_CODE, DIVISION_NAME, REGION_CODE, REGION_NAME, AREA_CODE, AREA_NAME, TERRITORY_CODE, TERRITORY_NAME, DB_LOCATION, CUSTOMER_CODE, CUSTOMER_NAME, MARKET_CODE, MARKET_NAME, PRODUCT_CODE, PRODUCT_NAME, PACK_SIZE, PRODUCT_PRICE, SALES_QTY, RETURN_SALES_QTY, IMS_SALES_QTY, BONUS_QTY, RETURN_BONUS_QTY, IMS_BONUS_QTY, INVOICE_VALUE, RETURN_VALUE, IMS_SALES_VAL, IMS_BONUS_VAL, BONUS_PRICE_DISCOUNT, RET_BONUS_PRICE_DISCOUNT, NET_IMS, BONUS_PER, TARGET_QTY, TARGET_VAL," +
                          " LAST_YEAR_AS_ON_DATE_IMS_QTY," +
                          " LAST_YEAR_AS_ON_DATE_IMS_VAL " +
                          " FROM MV_MARKET_PROD_IMS_CMONTH " +
                          " WHERE (DIVISION_CODE = '" + dCode + "' OR '" + dCode + "' = 'ALL')" +
                          " AND   (REGION_CODE='" + rCode + "' OR '" + rCode + "'='ALL')" +
                          " AND   (AREA_CODE='" + aCode + "' OR '" + aCode + "'='ALL')" +
                          " AND   (TERRITORY_CODE='" + tCode + "' OR '" + tCode + "'='ALL')" +
                          " AND   (CUSTOMER_CODE='" + cCode + "' OR '" + cCode + "'='ALL')" +
                          " AND   PRODUCT_CODE IN (SELECT PRODUCT_CODE FROM USER_PRODUCT_DTL WHERE USER_ID=" + userId + ")" +
                          " ORDER BY DIVISION_CODE,REGION_CODE, AREA_CODE, TERRITORY_CODE, CUSTOMER_CODE, MARKET_CODE, PRODUCT_CODE";

                }
                else
                {

                    qry = " SELECT DIVISION_CODE, DIVISION_NAME, REGION_CODE, REGION_NAME, AREA_CODE, AREA_NAME, TERRITORY_CODE, TERRITORY_NAME, DB_LOCATION, CUSTOMER_CODE, CUSTOMER_NAME, MARKET_CODE, MARKET_NAME, PRODUCT_CODE, PRODUCT_NAME, PACK_SIZE, PRODUCT_PRICE, SALES_QTY, RETURN_SALES_QTY, IMS_SALES_QTY, BONUS_QTY, RETURN_BONUS_QTY, IMS_BONUS_QTY, INVOICE_VALUE, RETURN_VALUE, IMS_SALES_VAL, IMS_BONUS_VAL, BONUS_PRICE_DISCOUNT, RET_BONUS_PRICE_DISCOUNT, NET_IMS, BONUS_PER, TARGET_QTY, TARGET_VAL," +
                          " LAST_YEAR_AS_ON_DATE_IMS_QTY," +
                          " LAST_YEAR_AS_ON_DATE_IMS_VAL " +
                          " FROM MV_MARKET_PROD_IMS_CMONTH " +
                          " WHERE (DIVISION_CODE = '" + dCode + "' OR '" + dCode + "' = 'ALL')" +
                          " AND   (REGION_CODE='" + rCode + "' OR '" + rCode + "'='ALL')" +
                          " AND   (AREA_CODE='" + aCode + "' OR '" + aCode + "'='ALL')" +
                          " AND   (TERRITORY_CODE='" + tCode + "' OR '" + tCode + "'='ALL')" +
                          " AND   (CUSTOMER_CODE='" + cCode + "' OR '" + cCode + "'='ALL')" +
                          " ORDER BY DIVISION_CODE,REGION_CODE, AREA_CODE, TERRITORY_CODE, CUSTOMER_CODE, MARKET_CODE, PRODUCT_CODE";

                }




                //DataTable dt = _dbHelper.GetDataTable(qry);
                DataTable dt = _dbHelper.GetDataTableWithAuditTrial(qry, "Market Wise Product IMS (Current Month)");
                var item = (from DataRow row in dt.Rows
                            select new MarketProductImsBEL
                            {
                                DivisionCode = row["DIVISION_CODE"].ToString(),
                                DivisionName = row["DIVISION_NAME"].ToString(),
                                RegionCode = row["REGION_CODE"].ToString(),
                                RegionName = row["REGION_NAME"].ToString(),
                                AreaCode = row["AREA_CODE"].ToString(),
                                AreaName = row["AREA_NAME"].ToString(),
                                TerritoryCode = row["TERRITORY_CODE"].ToString(),
                                TerritoryName = row["TERRITORY_NAME"].ToString(),
                                DBLoaction = row["DB_LOCATION"].ToString(),
                                CustomerCode = row["CUSTOMER_CODE"].ToString(),
                                CustomerName = row["CUSTOMER_NAME"].ToString(),
                                MarketCode = row["MARKET_CODE"].ToString(),
                                MarketName = row["MARKET_NAME"].ToString(),
                                ProductCode = row["PRODUCT_CODE"].ToString(),
                                ProductName = row["PRODUCT_NAME"].ToString(),
                                PackSize = row["PACK_SIZE"].ToString(),
                                ProductPrice = Convert.ToDouble(row["PRODUCT_PRICE"]),

                                InvoiceQty = Convert.ToInt32(row["SALES_QTY"]),
                                ReturnSalesQty = Convert.ToInt32(row["RETURN_SALES_QTY"]),
                                IMSSalesQty = Convert.ToInt32(row["IMS_SALES_QTY"]),

                                InvBonusQty = Convert.ToInt32(row["BONUS_QTY"]),
                                ReturnBnsQty = Convert.ToInt32(row["RETURN_BONUS_QTY"]),
                                IMSBnsQty = Convert.ToInt32(row["IMS_BONUS_QTY"]),

                                InvoiceAmt = Convert.ToDouble(row["INVOICE_VALUE"]),
                                ReturnValue = Convert.ToDouble(row["RETURN_VALUE"]),
                                IMSSalesVal = Convert.ToDouble(row["IMS_SALES_VAL"]),
                                IMSBnsVal = Convert.ToDouble(row["IMS_BONUS_VAL"]),

                                BonusPriceDiscount = Convert.ToDouble(row["BONUS_PRICE_DISCOUNT"]),
                                BnsDiscRet = Convert.ToDouble(row["RET_BONUS_PRICE_DISCOUNT"]),

                                NetIMS = Convert.ToDouble(row["NET_IMS"]),
                                BonusPer = Convert.ToInt32(row["BONUS_PER"]),
                                TargetQty = Convert.ToInt32(row["TARGET_QTY"]),
                                TargetVal = Convert.ToDouble(row["TARGET_VAL"]),

                                LastYearAsOnDateImsQty = Convert.ToInt32(row["LAST_YEAR_AS_ON_DATE_IMS_QTY"]),
                                LastYearAsOnDateImsVal = Convert.ToDouble(row["LAST_YEAR_AS_ON_DATE_IMS_VAL"])
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

        //Query 
        public List<MarketProductImsBEL> GetMarketProductImsLastMonth(string dCode, string rCode, string aCode, string tCode, string cCode)
        {
            try
            {

                string qry;

                int userId = Convert.ToInt32(HttpContext.Current.Session["USER_ID"]);
                int UserAccessCount = Convert.ToInt32(HttpContext.Current.Session["USER_BASE_REPORT_FILTER"]);

                if (UserAccessCount > 0)
                {

                    qry = " SELECT DIVISION_CODE, DIVISION_NAME, REGION_CODE, REGION_NAME, AREA_CODE, AREA_NAME, TERRITORY_CODE, TERRITORY_NAME, DB_LOCATION, CUSTOMER_CODE, CUSTOMER_NAME, MARKET_CODE, MARKET_NAME, PRODUCT_CODE, PRODUCT_NAME, PACK_SIZE, PRODUCT_PRICE, SALES_QTY, RETURN_SALES_QTY, IMS_SALES_QTY, BONUS_QTY, RETURN_BONUS_QTY, IMS_BONUS_QTY, INVOICE_VALUE, RETURN_VALUE, IMS_SALES_VAL, IMS_BONUS_VAL, BONUS_PRICE_DISCOUNT, RET_BONUS_PRICE_DISCOUNT, NET_IMS, BONUS_PER, TARGET_QTY, TARGET_VAL," +
                          " LAST_YEAR_AS_ON_DATE_IMS_QTY," +
                          " LAST_YEAR_AS_ON_DATE_IMS_VAL " +
                          " FROM MV_MARKET_PROD_IMS_LMONTH " +
                          " WHERE (DIVISION_CODE = '" + dCode + "' OR '" + dCode + "' = 'ALL')" +
                          " AND   (REGION_CODE='" + rCode + "' OR '" + rCode + "'='ALL')" +
                          " AND   (AREA_CODE='" + aCode + "' OR '" + aCode + "'='ALL')" +
                          " AND   (TERRITORY_CODE='" + tCode + "' OR '" + tCode + "'='ALL')" +
                          " AND   (CUSTOMER_CODE='" + cCode + "' OR '" + cCode + "'='ALL')" +
                          " AND   PRODUCT_CODE IN (SELECT PRODUCT_CODE FROM USER_PRODUCT_DTL WHERE USER_ID=" + userId + ")" +
                          " ORDER BY DIVISION_CODE,REGION_CODE, AREA_CODE, TERRITORY_CODE, CUSTOMER_CODE, MARKET_CODE, PRODUCT_CODE";

                }
                else
                {

                    qry = " SELECT DIVISION_CODE, DIVISION_NAME, REGION_CODE, REGION_NAME, AREA_CODE, AREA_NAME, TERRITORY_CODE, TERRITORY_NAME, DB_LOCATION, CUSTOMER_CODE, CUSTOMER_NAME, MARKET_CODE, MARKET_NAME, PRODUCT_CODE, PRODUCT_NAME, PACK_SIZE, PRODUCT_PRICE, SALES_QTY, RETURN_SALES_QTY, IMS_SALES_QTY, BONUS_QTY, RETURN_BONUS_QTY, IMS_BONUS_QTY, INVOICE_VALUE, RETURN_VALUE, IMS_SALES_VAL, IMS_BONUS_VAL, BONUS_PRICE_DISCOUNT, RET_BONUS_PRICE_DISCOUNT, NET_IMS, BONUS_PER, TARGET_QTY, TARGET_VAL," +
                          " LAST_YEAR_AS_ON_DATE_IMS_QTY," +
                          " LAST_YEAR_AS_ON_DATE_IMS_VAL " +
                          " FROM MV_MARKET_PROD_IMS_LMONTH " +
                          " WHERE (DIVISION_CODE = '" + dCode + "' OR '" + dCode + "' = 'ALL')" +
                          " AND   (REGION_CODE='" + rCode + "' OR '" + rCode + "'='ALL')" +
                          " AND   (AREA_CODE='" + aCode + "' OR '" + aCode + "'='ALL')" +
                          " AND   (TERRITORY_CODE='" + tCode + "' OR '" + tCode + "'='ALL')" +
                          " AND   (CUSTOMER_CODE='" + cCode + "' OR '" + cCode + "'='ALL')" +
                          " ORDER BY DIVISION_CODE,REGION_CODE, AREA_CODE, TERRITORY_CODE, CUSTOMER_CODE, MARKET_CODE, PRODUCT_CODE";

                }




                //DataTable dt = _dbHelper.GetDataTable(qry);
                DataTable dt = _dbHelper.GetDataTableWithAuditTrial(qry, "Market Wise Product IMS (Last Month)");
                var item = (from DataRow row in dt.Rows
                            select new MarketProductImsBEL
                            {
                                DivisionCode = row["DIVISION_CODE"].ToString(),
                                DivisionName = row["DIVISION_NAME"].ToString(),
                                RegionCode = row["REGION_CODE"].ToString(),
                                RegionName = row["REGION_NAME"].ToString(),
                                AreaCode = row["AREA_CODE"].ToString(),
                                AreaName = row["AREA_NAME"].ToString(),
                                TerritoryCode = row["TERRITORY_CODE"].ToString(),
                                TerritoryName = row["TERRITORY_NAME"].ToString(),
                                DBLoaction = row["DB_LOCATION"].ToString(),
                                CustomerCode = row["CUSTOMER_CODE"].ToString(),
                                CustomerName = row["CUSTOMER_NAME"].ToString(),
                                MarketCode = row["MARKET_CODE"].ToString(),
                                MarketName = row["MARKET_NAME"].ToString(),
                                ProductCode = row["PRODUCT_CODE"].ToString(),
                                ProductName = row["PRODUCT_NAME"].ToString(),
                                PackSize = row["PACK_SIZE"].ToString(),
                                ProductPrice = Convert.ToDouble(row["PRODUCT_PRICE"]),

                                InvoiceQty = Convert.ToInt32(row["SALES_QTY"]),
                                ReturnSalesQty = Convert.ToInt32(row["RETURN_SALES_QTY"]),
                                IMSSalesQty = Convert.ToInt32(row["IMS_SALES_QTY"]),

                                InvBonusQty = Convert.ToInt32(row["BONUS_QTY"]),
                                ReturnBnsQty = Convert.ToInt32(row["RETURN_BONUS_QTY"]),
                                IMSBnsQty = Convert.ToInt32(row["IMS_BONUS_QTY"]),
                                
                                InvoiceAmt = Convert.ToDouble(row["INVOICE_VALUE"]),
                                ReturnValue = Convert.ToDouble(row["RETURN_VALUE"]),
                                IMSSalesVal = Convert.ToDouble(row["IMS_SALES_VAL"]),
                                IMSBnsVal = Convert.ToDouble(row["IMS_BONUS_VAL"]),

                                BonusPriceDiscount = Convert.ToDouble(row["BONUS_PRICE_DISCOUNT"]),
                                BnsDiscRet = Convert.ToDouble(row["RET_BONUS_PRICE_DISCOUNT"]),

                                NetIMS = Convert.ToDouble(row["NET_IMS"]),
                                BonusPer = Convert.ToInt32(row["BONUS_PER"]),
                                TargetQty = Convert.ToInt32(row["TARGET_QTY"]),
                                TargetVal = Convert.ToDouble(row["TARGET_VAL"]),
                                LastYearAsOnDateImsQty = Convert.ToInt32(row["LAST_YEAR_AS_ON_DATE_IMS_QTY"]),
                                LastYearAsOnDateImsVal = Convert.ToDouble(row["LAST_YEAR_AS_ON_DATE_IMS_VAL"])
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