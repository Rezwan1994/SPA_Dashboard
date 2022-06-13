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
    public class InvoiceProductImsDAL : ReturnData
    {
        private readonly DBHelper _dbHelper = new DBHelper();
        private readonly ErrorLogger _errorLogger = new ErrorLogger();
        private static readonly DBConnection DBConnection = new DBConnection();
        public readonly string ConnString = DBConnection.SAConnStrReader("Oracle", "PHARMAERP");

        public object GetProductList(string baseProductCode, string brandCode, string categoryCode)
        {
            try
            {
                var qry = " SELECT 'ALL' PRODUCT_CODE, 'ALL' PRODUCT_NAME, 'ALL' PACK_SIZE,  1 SL FROM DUAL " +
                          " UNION SELECT PRODUCT_CODE,PRODUCT_NAME,PACK_SIZE ,2 SL  FROM MV_PRODUCT_INFO" +
                          " WHERE STATUS='A'" +
                          " AND   (BASE_PRODUCT_CODE='" + baseProductCode + "' OR '" + baseProductCode + "'='ALL')" +
                          " AND   (BRAND_CODE='" + brandCode + "' OR '" + brandCode + "'='ALL')" +
                          " AND   (PRODUCT_CATEGORY='" + categoryCode + "' OR '" + categoryCode + "'='ALL')" +
                          " ORDER BY SL, PRODUCT_NAME";
                DataTable dt = _dbHelper.GetDataTable(qry);
                var item = (from DataRow row in dt.Rows
                            select new ProductInfoBEL
                            {
                                ProductCode = row["PRODUCT_CODE"].ToString(),
                                ProductName = row["PRODUCT_NAME"].ToString(),
                                PackSize = row["PACK_SIZE"].ToString()
                            }).ToList();
                return item;

            }
            catch (Exception e)
            {
                var lineNum = e.StackTrace.Substring(e.StackTrace.LastIndexOf(' '));
                _errorLogger.GetErrorMessage(e.Message, "ProductBonusDAL", lineNum);
                ExceptionReturn = e.Message;
                return "Not Ok";
            }
        }

        public List<InvoiceProductImsBEL> GetInvoiceProductImsCurrentMonth(string dCode, string rCode, string aCode, string tCode, string cCode, string pBaseProductCode, string pBrandCode, string pCategoryCode, string pCode)
        {
            try
            {

                string qry;

                int userId = Convert.ToInt32(HttpContext.Current.Session["USER_ID"]);
                int UserAccessCount = Convert.ToInt32(HttpContext.Current.Session["USER_BASE_REPORT_FILTER"]);

                if (UserAccessCount > 0)
                {

                    qry = " SELECT DIVISION_CODE, DIVISION_NAME, REGION_CODE, REGION_NAME, AREA_CODE, AREA_NAME, TERRITORY_CODE, TERRITORY_NAME, MARKET_CODE, MARKET_NAME, CUSTOMER_CODE, CUSTOMER_NAME, DB_LOCATION, ROUTE_CODE, ROUTE_NAME, RETAILER_CODE, RETAILER_NAME, BASE_PRODUCT_CODE, BASE_PRODUCT_NAME, BRAND_CODE, BRAND_NAME, PRODUCT_CATEGORY_CODE, PRODUCT_CATEGORY_NAME, INVOICE_NO, TO_CHAR(INVOICE_DATE,'MM/DD/RRRR HH:MI:SS AM')INVOICE_DATE, PRODUCT_CODE, PRODUCT_NAME, PACK_SIZE, PRODUCT_PRICE, INVOICE_AMT, SALES_QTY, SALES_BONUS_QTY, BONUS_PRICE_DISCOUNT, REPLACE_QTY, RETURN_SALES_QTY, RETURN_BNS_QTY, IMS_SALES_QTY, IMS_BNS_QTY, RETURN_VALUE, BNS_DISC_RET, DISCOUNT_VAL, NET_IMS,TARGET_QTY " +
                          " FROM MV_INVOICE_PROD_IMS_CMONTH " +
                          " WHERE (DIVISION_CODE = '" + dCode + "' OR '" + dCode + "' = 'ALL')" +
                          " AND   (REGION_CODE='" + rCode + "' OR '" + rCode + "'='ALL')" +
                          " AND   (AREA_CODE='" + aCode + "' OR '" + aCode + "'='ALL')" +
                          " AND   (TERRITORY_CODE='" + tCode + "' OR '" + tCode + "'='ALL')" +
                          " AND   (CUSTOMER_CODE='" + cCode + "' OR '" + cCode + "'='ALL')" +
                          " AND   (BASE_PRODUCT_CODE='" + pBaseProductCode + "' OR '" + pBaseProductCode + "'='ALL')" +
                          " AND   (BRAND_CODE='" + pBrandCode + "' OR '" + pBrandCode + "'='ALL')" +
                          " AND   (PRODUCT_CATEGORY_CODE='" + pCategoryCode + "' OR '" + pCategoryCode + "'='ALL')" +
                          " AND   (PRODUCT_CODE='" + pCode + "' OR '" + pCode + "'='ALL')" +
                          " AND   PRODUCT_CODE IN (SELECT PRODUCT_CODE FROM USER_PRODUCT_DTL WHERE USER_ID=" + userId + ")" +
                          " ORDER BY DIVISION_CODE,REGION_CODE, AREA_CODE, TERRITORY_CODE, CUSTOMER_CODE,MARKET_CODE,ROUTE_CODE,RETAILER_CODE,PRODUCT_CODE";
                }
                else
                {

                    qry = " SELECT DIVISION_CODE, DIVISION_NAME, REGION_CODE, REGION_NAME, AREA_CODE, AREA_NAME, TERRITORY_CODE, TERRITORY_NAME, MARKET_CODE, MARKET_NAME, CUSTOMER_CODE, CUSTOMER_NAME, DB_LOCATION, ROUTE_CODE, ROUTE_NAME, RETAILER_CODE, RETAILER_NAME, BASE_PRODUCT_CODE, BASE_PRODUCT_NAME, BRAND_CODE, BRAND_NAME, PRODUCT_CATEGORY_CODE, PRODUCT_CATEGORY_NAME, INVOICE_NO, TO_CHAR(INVOICE_DATE,'MM/DD/RRRR HH:MI:SS AM')INVOICE_DATE, PRODUCT_CODE, PRODUCT_NAME, PACK_SIZE, PRODUCT_PRICE, INVOICE_AMT, SALES_QTY, SALES_BONUS_QTY, BONUS_PRICE_DISCOUNT, REPLACE_QTY, RETURN_SALES_QTY, RETURN_BNS_QTY, IMS_SALES_QTY, IMS_BNS_QTY, RETURN_VALUE, BNS_DISC_RET, DISCOUNT_VAL, NET_IMS,TARGET_QTY" +
                          " FROM MV_INVOICE_PROD_IMS_CMONTH " +
                          " WHERE (DIVISION_CODE = '" + dCode + "' OR '" + dCode + "' = 'ALL')" +
                          " AND   (REGION_CODE='" + rCode + "' OR '" + rCode + "'='ALL')" +
                          " AND   (AREA_CODE='" + aCode + "' OR '" + aCode + "'='ALL')" +
                          " AND   (TERRITORY_CODE='" + tCode + "' OR '" + tCode + "'='ALL')" +
                          " AND   (CUSTOMER_CODE='" + cCode + "' OR '" + cCode + "'='ALL')" +
                          " AND   (BASE_PRODUCT_CODE='" + pBaseProductCode + "' OR '" + pBaseProductCode + "'='ALL')" +
                          " AND   (BRAND_CODE='" + pBrandCode + "' OR '" + pBrandCode + "'='ALL')" +
                          " AND   (PRODUCT_CATEGORY_CODE='" + pCategoryCode + "' OR '" + pCategoryCode + "'='ALL')" +
                          " AND   (PRODUCT_CODE='" + pCode + "' OR '" + pCode + "'='ALL')" +
                          " ORDER BY DIVISION_CODE,REGION_CODE, AREA_CODE, TERRITORY_CODE, CUSTOMER_CODE,MARKET_CODE,ROUTE_CODE,RETAILER_CODE,PRODUCT_CODE";

                }

                //DataTable dt = _dbHelper.GetDataTable(qry);
                DataTable dt = _dbHelper.GetDataTableWithAuditTrial(qry, "Invoice Wise Product IMS (Current Month)");
                var item = (from DataRow row in dt.Rows
                            select new InvoiceProductImsBEL
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

                                BaseProductCode = row["BASE_PRODUCT_CODE"].ToString(),
                                BaseProductName = row["BASE_PRODUCT_NAME"].ToString(),
                                BrandCode = row["BRAND_CODE"].ToString(),
                                BrandName = row["BRAND_NAME"].ToString(),
                                ProductCategoryCode = row["PRODUCT_CATEGORY_CODE"].ToString(),
                                ProductCategoryName = row["PRODUCT_CATEGORY_NAME"].ToString(),

                                InvoiceNo = row["INVOICE_NO"].ToString(),
                                InvoiceDate = row["INVOICE_DATE"].ToString(),

                                ProductCode = row["PRODUCT_CODE"].ToString(),
                                ProductName = row["PRODUCT_NAME"].ToString(),
                                PackSize = row["PACK_SIZE"].ToString(),
                                ProductPrice = Convert.ToInt32(row["PRODUCT_PRICE"]),

                                InvoiceAmt = Convert.ToDouble(row["INVOICE_AMT"]),
                                SalesQty = Convert.ToInt32(row["SALES_QTY"]),
                                SalesBonusQty = Convert.ToInt32(row["SALES_BONUS_QTY"]),
                                BonusPriceDiscount = Convert.ToDouble(row["BONUS_PRICE_DISCOUNT"]),
                                ReplaceQty = Convert.ToInt32(row["REPLACE_QTY"]),
                                ReturnSalesQty = Convert.ToInt32(row["RETURN_SALES_QTY"]),
                                ReturnBnsQty = Convert.ToInt32(row["RETURN_BNS_QTY"]),
                                ImsSalesQty = Convert.ToInt32(row["IMS_SALES_QTY"]),
                                ImsBnsQty = Convert.ToInt32(row["IMS_BNS_QTY"]),
                                ReturnValue = Convert.ToDouble(row["RETURN_VALUE"]),
                                BnsDiscRet = Convert.ToDouble(row["BNS_DISC_RET"]),
                                DiscountVal = Convert.ToDouble(row["DISCOUNT_VAL"]),
                                NetIms = Convert.ToDouble(row["NET_IMS"]),
                                TargetQty = Convert.ToInt32(row["TARGET_QTY"])

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
        public List<InvoiceProductImsBEL> GetInvoiceProductImsLastMonth(string dCode, string rCode, string aCode, string tCode, string cCode, string pBaseProductCode, string pBrandCode, string pCategoryCode, string pCode)
        {
            try
            {

                string qry;

                int userId = Convert.ToInt32(HttpContext.Current.Session["USER_ID"]);
                int UserAccessCount = Convert.ToInt32(HttpContext.Current.Session["USER_BASE_REPORT_FILTER"]);

                if (UserAccessCount > 0)
                {

                    qry = " SELECT DIVISION_CODE, DIVISION_NAME, REGION_CODE, REGION_NAME, AREA_CODE, AREA_NAME, TERRITORY_CODE, TERRITORY_NAME, MARKET_CODE, MARKET_NAME, CUSTOMER_CODE, CUSTOMER_NAME, DB_LOCATION, ROUTE_CODE, ROUTE_NAME, RETAILER_CODE, RETAILER_NAME, BASE_PRODUCT_CODE, BASE_PRODUCT_NAME, BRAND_CODE, BRAND_NAME, PRODUCT_CATEGORY_CODE, PRODUCT_CATEGORY_NAME, INVOICE_NO, TO_CHAR(INVOICE_DATE,'MM/DD/RRRR HH:MI:SS AM')INVOICE_DATE, PRODUCT_CODE, PRODUCT_NAME, PACK_SIZE, PRODUCT_PRICE, INVOICE_AMT, SALES_QTY, SALES_BONUS_QTY, BONUS_PRICE_DISCOUNT, REPLACE_QTY, RETURN_SALES_QTY, RETURN_BNS_QTY, IMS_SALES_QTY, IMS_BNS_QTY, RETURN_VALUE, BNS_DISC_RET, DISCOUNT_VAL, NET_IMS,TARGET_QTY " +
                          " FROM MV_INVOICE_PROD_IMS_LMONTH " +
                          " WHERE (DIVISION_CODE = '" + dCode + "' OR '" + dCode + "' = 'ALL')" +
                          " AND   (REGION_CODE='" + rCode + "' OR '" + rCode + "'='ALL')" +
                          " AND   (AREA_CODE='" + aCode + "' OR '" + aCode + "'='ALL')" +
                          " AND   (TERRITORY_CODE='" + tCode + "' OR '" + tCode + "'='ALL')" +
                          " AND   (CUSTOMER_CODE='" + cCode + "' OR '" + cCode + "'='ALL')" +
                          " AND   (BASE_PRODUCT_CODE='" + pBaseProductCode + "' OR '" + pBaseProductCode + "'='ALL')" +
                          " AND   (BRAND_CODE='" + pBrandCode + "' OR '" + pBrandCode + "'='ALL')" +
                          " AND   (PRODUCT_CATEGORY_CODE='" + pCategoryCode + "' OR '" + pCategoryCode + "'='ALL')" +
                          " AND   (PRODUCT_CODE='" + pCode + "' OR '" + pCode + "'='ALL')" +
                          " AND   PRODUCT_CODE IN (SELECT PRODUCT_CODE FROM USER_PRODUCT_DTL WHERE USER_ID=" + userId + ")" +
                          " ORDER BY DIVISION_CODE,REGION_CODE, AREA_CODE, TERRITORY_CODE, CUSTOMER_CODE,MARKET_CODE,ROUTE_CODE,RETAILER_CODE,PRODUCT_CODE";
                }
                else
                {

                    qry = " SELECT DIVISION_CODE, DIVISION_NAME, REGION_CODE, REGION_NAME, AREA_CODE, AREA_NAME, TERRITORY_CODE, TERRITORY_NAME, MARKET_CODE, MARKET_NAME, CUSTOMER_CODE, CUSTOMER_NAME, DB_LOCATION, ROUTE_CODE, ROUTE_NAME, RETAILER_CODE, RETAILER_NAME, BASE_PRODUCT_CODE, BASE_PRODUCT_NAME, BRAND_CODE, BRAND_NAME, PRODUCT_CATEGORY_CODE, PRODUCT_CATEGORY_NAME, INVOICE_NO, TO_CHAR(INVOICE_DATE,'MM/DD/RRRR HH:MI:SS AM')INVOICE_DATE, PRODUCT_CODE, PRODUCT_NAME, PACK_SIZE, PRODUCT_PRICE, INVOICE_AMT, SALES_QTY, SALES_BONUS_QTY, BONUS_PRICE_DISCOUNT, REPLACE_QTY, RETURN_SALES_QTY, RETURN_BNS_QTY, IMS_SALES_QTY, IMS_BNS_QTY, RETURN_VALUE, BNS_DISC_RET, DISCOUNT_VAL, NET_IMS,TARGET_QTY" +
                          " FROM MV_INVOICE_PROD_IMS_LMONTH " +
                          " WHERE (DIVISION_CODE = '" + dCode + "' OR '" + dCode + "' = 'ALL')" +
                          " AND   (REGION_CODE='" + rCode + "' OR '" + rCode + "'='ALL')" +
                          " AND   (AREA_CODE='" + aCode + "' OR '" + aCode + "'='ALL')" +
                          " AND   (TERRITORY_CODE='" + tCode + "' OR '" + tCode + "'='ALL')" +
                          " AND   (CUSTOMER_CODE='" + cCode + "' OR '" + cCode + "'='ALL')" +
                          " AND   (BASE_PRODUCT_CODE='" + pBaseProductCode + "' OR '" + pBaseProductCode + "'='ALL')" +
                          " AND   (BRAND_CODE='" + pBrandCode + "' OR '" + pBrandCode + "'='ALL')" +
                          " AND   (PRODUCT_CATEGORY_CODE='" + pCategoryCode + "' OR '" + pCategoryCode + "'='ALL')" +
                          " AND   (PRODUCT_CODE='" + pCode + "' OR '" + pCode + "'='ALL')" +
                          " ORDER BY DIVISION_CODE,REGION_CODE, AREA_CODE, TERRITORY_CODE, CUSTOMER_CODE,MARKET_CODE,ROUTE_CODE,RETAILER_CODE,PRODUCT_CODE";

                }

                //DataTable dt = _dbHelper.GetDataTable(qry);
                DataTable dt = _dbHelper.GetDataTableWithAuditTrial(qry, "Invoice Wise Product IMS (Last Month)");
                Int64 cont = dt.Rows.Count;
                var item = (from DataRow row in dt.Rows
                            select new InvoiceProductImsBEL
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

                                BaseProductCode = row["BASE_PRODUCT_CODE"].ToString(),
                                BaseProductName = row["BASE_PRODUCT_NAME"].ToString(),
                                BrandCode = row["BRAND_CODE"].ToString(),
                                BrandName = row["BRAND_NAME"].ToString(),
                                ProductCategoryCode = row["PRODUCT_CATEGORY_CODE"].ToString(),
                                ProductCategoryName = row["PRODUCT_CATEGORY_NAME"].ToString(),
                                InvoiceNo = row["INVOICE_NO"].ToString(),
                                InvoiceDate = row["INVOICE_DATE"].ToString(),
                                ProductCode = row["PRODUCT_CODE"].ToString(),
                                ProductName = row["PRODUCT_NAME"].ToString(),
                                PackSize = row["PACK_SIZE"].ToString(),
                                ProductPrice = Convert.ToInt32(row["PRODUCT_PRICE"]),

                                InvoiceAmt = Convert.ToDouble(row["INVOICE_AMT"]),
                                SalesQty = Convert.ToInt32(row["SALES_QTY"]),
                                SalesBonusQty = Convert.ToInt32(row["SALES_BONUS_QTY"]),
                                BonusPriceDiscount = Convert.ToDouble(row["BONUS_PRICE_DISCOUNT"]),
                                ReplaceQty = Convert.ToInt32(row["REPLACE_QTY"]),
                                ReturnSalesQty = Convert.ToInt32(row["RETURN_SALES_QTY"]),
                                ReturnBnsQty = Convert.ToInt32(row["RETURN_BNS_QTY"]),
                                ImsSalesQty = Convert.ToInt32(row["IMS_SALES_QTY"]),
                                ImsBnsQty = Convert.ToInt32(row["IMS_BNS_QTY"]),
                                ReturnValue = Convert.ToDouble(row["RETURN_VALUE"]),
                                BnsDiscRet = Convert.ToDouble(row["BNS_DISC_RET"]),
                                DiscountVal = Convert.ToDouble(row["DISCOUNT_VAL"]),
                                NetIms = Convert.ToDouble(row["NET_IMS"]),
                                TargetQty = Convert.ToInt32(row["TARGET_QTY"])

                            }).ToList();
                return item;
            }
            catch (Exception e)
            {
                var lineNum = e.StackTrace.Substring(e.StackTrace.LastIndexOf(' '));
                _errorLogger.GetErrorMessage(e.Message, "RetailerProductImsDAL", lineNum);
                ExceptionReturn = e.Message;
                throw;
            }
        }
        public object GetInvoiceProductImsDateRange(string fDate, string tDate, string dCode, string rCode, string aCode, string tCode, string cCode, string pBaseProductCode, string pBrandCode, string pCategoryCode, string pCode)
        {
            try
            {
                int userId = Convert.ToInt32(HttpContext.Current.Session["USER_ID"]);

                using (OracleConnection objConn = new OracleConnection(ConnString))
                {
                    using (OracleCommand objCmd = new OracleCommand())
                    {
                        objCmd.Connection = objConn;
                        objCmd.CommandText = "FN_INVOICE_PROD_IMS_DATE_RANGE";
                        objCmd.CommandType = CommandType.StoredProcedure;
                        objCmd.Parameters.Add("pfrom_date", OracleType.VarChar).Value = fDate;
                        objCmd.Parameters.Add("pto_date", OracleType.VarChar).Value = tDate;
                        objCmd.Parameters.Add("pdivision_code", OracleType.VarChar).Value = dCode;
                        objCmd.Parameters.Add("pregion_code", OracleType.VarChar).Value = rCode;
                        objCmd.Parameters.Add("parea_code", OracleType.VarChar).Value = aCode;
                        objCmd.Parameters.Add("pterritory_code", OracleType.VarChar).Value = tCode;
                        objCmd.Parameters.Add("pcustomer_code", OracleType.VarChar).Value = cCode;

                        objCmd.Parameters.Add("pbase_product_code", OracleType.VarChar).Value = pBaseProductCode;
                        objCmd.Parameters.Add("pbrand_code", OracleType.VarChar).Value = pBrandCode;
                        objCmd.Parameters.Add("pcategory_code", OracleType.VarChar).Value = pCategoryCode;

                        objCmd.Parameters.Add("pproduct_code", OracleType.VarChar).Value = pCode;
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
                        _dbHelper.InsertReportAudit("Invoice Wise Product IMS (Custom Date)");
                        List<InvoiceProductImsBEL> item;
                        item = (from DataRow row in dt.Rows
                                select new InvoiceProductImsBEL
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

                                    BaseProductCode = row["BASE_PRODUCT_CODE"].ToString(),
                                    BaseProductName = row["BASE_PRODUCT_NAME"].ToString(),
                                    BrandCode = row["BRAND_CODE"].ToString(),
                                    BrandName = row["BRAND_NAME"].ToString(),
                                    ProductCategoryCode = row["PRODUCT_CATEGORY_CODE"].ToString(),
                                    ProductCategoryName = row["PRODUCT_CATEGORY_NAME"].ToString(),
                                    InvoiceNo = row["INVOICE_NO"].ToString(),
                                    InvoiceDate = row["INVOICE_DATE"].ToString(),
                                    ProductCode = row["PRODUCT_CODE"].ToString(),
                                    ProductName = row["PRODUCT_NAME"].ToString(),
                                    PackSize = row["PACK_SIZE"].ToString(),
                                    ProductPrice = Convert.ToInt32(row["PRODUCT_PRICE"]),

                                    InvoiceAmt = Convert.ToDouble(row["INVOICE_AMT"]),
                                    SalesQty = Convert.ToInt32(row["SALES_QTY"]),
                                    SalesBonusQty = Convert.ToInt32(row["SALES_BONUS_QTY"]),
                                    BonusPriceDiscount = Convert.ToDouble(row["BONUS_PRICE_DISCOUNT"]),
                                    ReplaceQty = Convert.ToInt32(row["REPLACE_QTY"]),
                                    ReturnSalesQty = Convert.ToInt32(row["RETURN_SALES_QTY"]),
                                    ReturnBnsQty = Convert.ToInt32(row["RETURN_BNS_QTY"]),
                                    ImsSalesQty = Convert.ToInt32(row["IMS_SALES_QTY"]),
                                    ImsBnsQty = Convert.ToInt32(row["IMS_BNS_QTY"]),
                                    ReturnValue = Convert.ToDouble(row["RETURN_VALUE"]),
                                    BnsDiscRet = Convert.ToDouble(row["BNS_DISC_RET"]),
                                    DiscountVal = Convert.ToDouble(row["DISCOUNT_VAL"]),
                                    NetIms = Convert.ToDouble(row["NET_IMS"]),
                                    TargetQty = Convert.ToInt32(row["TARGET_QTY"])

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
        public object GetInvoiceProductImsToday(string dCode, string rCode, string aCode, string tCode, string cCode, string pBaseProductCode, string pBrandCode, string pCategoryCode, string pCode)
        {
            try
            {
                int userId = Convert.ToInt32(HttpContext.Current.Session["USER_ID"]);

                using (OracleConnection objConn = new OracleConnection(ConnString))
                {
                    using (OracleCommand objCmd = new OracleCommand())
                    {
                        objCmd.Connection = objConn;
                        objCmd.CommandText = "FN_INVOICE_PROD_IMS_TODAY";
                        objCmd.CommandType = CommandType.StoredProcedure;
                        objCmd.Parameters.Add("pdivision_code", OracleType.VarChar).Value = dCode;
                        objCmd.Parameters.Add("pregion_code", OracleType.VarChar).Value = rCode;
                        objCmd.Parameters.Add("parea_code", OracleType.VarChar).Value = aCode;
                        objCmd.Parameters.Add("pterritory_code", OracleType.VarChar).Value = tCode;
                        objCmd.Parameters.Add("pcustomer_code", OracleType.VarChar).Value = cCode;

                        objCmd.Parameters.Add("pbase_product_code", OracleType.VarChar).Value = pBaseProductCode;
                        objCmd.Parameters.Add("pbrand_code", OracleType.VarChar).Value = pBrandCode;
                        objCmd.Parameters.Add("pcategory_code", OracleType.VarChar).Value = pCategoryCode;

                        objCmd.Parameters.Add("pproduct_code", OracleType.VarChar).Value = pCode;
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
                        _dbHelper.InsertReportAudit("Invoice Wise Product IMS (Today)");
                        List<InvoiceProductImsBEL> item;
                        item = (from DataRow row in dt.Rows
                                select new InvoiceProductImsBEL
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

                                    BaseProductCode = row["BASE_PRODUCT_CODE"].ToString(),
                                    BaseProductName = row["BASE_PRODUCT_NAME"].ToString(),
                                    BrandCode = row["BRAND_CODE"].ToString(),
                                    BrandName = row["BRAND_NAME"].ToString(),
                                    ProductCategoryCode = row["PRODUCT_CATEGORY_CODE"].ToString(),
                                    ProductCategoryName = row["PRODUCT_CATEGORY_NAME"].ToString(),
                                    InvoiceNo = row["INVOICE_NO"].ToString(),
                                    InvoiceDate = row["INVOICE_DATE"].ToString(),
                                    ProductCode = row["PRODUCT_CODE"].ToString(),
                                    ProductName = row["PRODUCT_NAME"].ToString(),
                                    PackSize = row["PACK_SIZE"].ToString(),
                                    ProductPrice = Convert.ToInt32(row["PRODUCT_PRICE"]),

                                    InvoiceAmt = Convert.ToDouble(row["INVOICE_AMT"]),
                                    SalesQty = Convert.ToInt32(row["SALES_QTY"]),
                                    SalesBonusQty = Convert.ToInt32(row["SALES_BONUS_QTY"]),
                                    BonusPriceDiscount = Convert.ToDouble(row["BONUS_PRICE_DISCOUNT"]),
                                    ReplaceQty = Convert.ToInt32(row["REPLACE_QTY"]),
                                    ReturnSalesQty = Convert.ToInt32(row["RETURN_SALES_QTY"]),
                                    ReturnBnsQty = Convert.ToInt32(row["RETURN_BNS_QTY"]),
                                    ImsSalesQty = Convert.ToInt32(row["IMS_SALES_QTY"]),
                                    ImsBnsQty = Convert.ToInt32(row["IMS_BNS_QTY"]),
                                    ReturnValue = Convert.ToDouble(row["RETURN_VALUE"]),
                                    BnsDiscRet = Convert.ToDouble(row["BNS_DISC_RET"]),
                                    DiscountVal = Convert.ToDouble(row["DISCOUNT_VAL"]),
                                    NetIms = Convert.ToDouble(row["NET_IMS"]),
                                    TargetQty = Convert.ToInt32(row["TARGET_QTY"])

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