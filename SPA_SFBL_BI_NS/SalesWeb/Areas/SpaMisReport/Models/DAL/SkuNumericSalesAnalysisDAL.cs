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
    public class SkuNumericSalesAnalysisDAL : ReturnData
    {
        private readonly DBHelper _dbHelper = new DBHelper();
        private readonly ErrorLogger _errorLogger = new ErrorLogger();
        private static readonly DBConnection DBConnection = new DBConnection();
        public readonly string ConnString = DBConnection.SAConnStrReader("Oracle", "PHARMAERP");

        public object GetProductCategoryList()
        {
            try
            {
                var qry = " SELECT 'ALL' CATEGORY_CODE, 'ALL' CATEGORY_NAME, 1 SL FROM DUAL " +
                          " UNION SELECT CATEGORY_CODE, CATEGORY_NAME,2 SL  FROM MV_CATEGORY_INFO " +
                          " ORDER BY SL, CATEGORY_NAME";
                DataTable dt = _dbHelper.GetDataTable(qry);
                var item = (from DataRow row in dt.Rows
                            select new ProductCategoryBEL
                            {
                                CategoryCode = row["CATEGORY_CODE"].ToString(),
                                CategoryName = row["CATEGORY_NAME"].ToString()
                            }).ToList();
                return item;

            }
            catch (Exception e)
            {
                var lineNum = e.StackTrace.Substring(e.StackTrace.LastIndexOf(' '));
                _errorLogger.GetErrorMessage(e.Message, "SkuNumericSalesAnalysisDAL", lineNum);
                ExceptionReturn = e.Message;
                return "Not Ok";
            }
        }
        public List<SkuNumericSalesAnalysisBEL> GetSkuNumericSalesAnalysisCMonth(string dCode, string rCode, string aCode, string tCode, string cCode, string pcCode)
        {
            try
            {
                string qry;

                int userId = Convert.ToInt32(HttpContext.Current.Session["USER_ID"]);
                int UserAccessCount = Convert.ToInt32(HttpContext.Current.Session["USER_BASE_REPORT_FILTER"]);

                if (UserAccessCount > 0)
                {

                    qry =  " SELECT DIVISION_CODE, DIVISION_NAME, REGION_CODE, REGION_NAME, AREA_CODE, AREA_NAME, TERRITORY_CODE, TERRITORY_NAME, MARKET_CODE, MARKET_NAME, CUSTOMER_CODE, CUSTOMER_NAME, DB_LOCATION, TOTAL_RETAILER, PRODUCT_CATEGORY_CODE, PRODUCT_CATEGORY_NAME, PRODUCT_CODE, PRODUCT_NAME, PACK_SIZE, NO_OF_RETAILER, AVG_ORDER_QTY_PER_RET, AVG_SALES_QTY_PER_INVOICE, AVG_SALES_QTY_PER_MONTH_RET " +
                           " FROM MV_SKU_NUMERIC_SALES_CMONTH " +
                           " WHERE (DIVISION_CODE = '" + dCode + "' OR '" + dCode + "' = 'ALL')" +
                           " AND   (REGION_CODE='" + rCode + "' OR '" + rCode + "'='ALL')" +
                           " AND   (AREA_CODE='" + aCode + "' OR '" + aCode + "'='ALL')" +
                           " AND   (TERRITORY_CODE='" + tCode + "' OR '" + tCode + "'='ALL')" +
                           " AND   (CUSTOMER_CODE='" + cCode + "' OR '" + cCode + "'='ALL')" +
                           " AND   (PRODUCT_CATEGORY_CODE='" + pcCode + "' OR '" + pcCode + "'='ALL')" +
                           " AND   PRODUCT_CODE IN (SELECT PRODUCT_CODE FROM USER_PRODUCT_DTL WHERE USER_ID=" + userId + ")" +
                           " ORDER BY DIVISION_CODE,REGION_CODE, AREA_CODE, TERRITORY_CODE, CUSTOMER_CODE,MARKET_CODE,PRODUCT_CODE";

                }
                else
                {

                    qry = " SELECT DIVISION_CODE, DIVISION_NAME, REGION_CODE, REGION_NAME, AREA_CODE, AREA_NAME, TERRITORY_CODE, TERRITORY_NAME, MARKET_CODE, MARKET_NAME, CUSTOMER_CODE, CUSTOMER_NAME, DB_LOCATION, TOTAL_RETAILER, PRODUCT_CATEGORY_CODE, PRODUCT_CATEGORY_NAME, PRODUCT_CODE, PRODUCT_NAME, PACK_SIZE, NO_OF_RETAILER, AVG_ORDER_QTY_PER_RET, AVG_SALES_QTY_PER_INVOICE, AVG_SALES_QTY_PER_MONTH_RET " +
                           " FROM MV_SKU_NUMERIC_SALES_CMONTH " +
                           " WHERE (DIVISION_CODE = '" + dCode + "' OR '" + dCode + "' = 'ALL')" +
                           " AND   (REGION_CODE='" + rCode + "' OR '" + rCode + "'='ALL')" +
                           " AND   (AREA_CODE='" + aCode + "' OR '" + aCode + "'='ALL')" +
                           " AND   (TERRITORY_CODE='" + tCode + "' OR '" + tCode + "'='ALL')" +
                           " AND   (CUSTOMER_CODE='" + cCode + "' OR '" + cCode + "'='ALL')" +
                           " AND   (PRODUCT_CATEGORY_CODE='" + pcCode + "' OR '" + pcCode + "'='ALL')" +
                           " ORDER BY DIVISION_CODE,REGION_CODE, AREA_CODE, TERRITORY_CODE, CUSTOMER_CODE,MARKET_CODE,PRODUCT_CODE";


                }



                //DataTable dt = _dbHelper.GetDataTable(qry);
                DataTable dt = _dbHelper.GetDataTableWithAuditTrial(qry, "SKU Wise Numeric Distribution (Last Month)");

                int count = 0;
                var item = (from DataRow row in dt.Rows
                            select new SkuNumericSalesAnalysisBEL
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
                                TotalRetailer = row["TOTAL_RETAILER"].ToString(),
                                ProductCategoryCode = row["PRODUCT_CATEGORY_CODE"].ToString(),
                                ProductCategoryName = row["PRODUCT_CATEGORY_NAME"].ToString(),
                                ProductCode = row["PRODUCT_CODE"].ToString(),
                                ProductName = row["PRODUCT_NAME"].ToString(),
                                PackSize = row["PACK_SIZE"].ToString(),
                                NoOfRetailer = row["NO_OF_RETAILER"].ToString(),
                                AvgOrderQtyPerRet = row["AVG_ORDER_QTY_PER_RET"].ToString(),
                                AvgSalesQtyPerInvoice = row["AVG_SALES_QTY_PER_INVOICE"].ToString(),
                                AvgSalesQtyPerMonthRet = row["AVG_SALES_QTY_PER_MONTH_RET"].ToString()
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


        public List<SkuNumericSalesAnalysisBEL> GetSkuNumericSalesAnalysisLMonth(string dCode, string rCode, string aCode, string tCode, string cCode, string pcCode)
        {
            try
            {

                string qry;

                int userId = Convert.ToInt32(HttpContext.Current.Session["USER_ID"]);
                int UserAccessCount = Convert.ToInt32(HttpContext.Current.Session["USER_BASE_REPORT_FILTER"]);

                if (UserAccessCount > 0)
                {

                    qry = " SELECT DIVISION_CODE, DIVISION_NAME, REGION_CODE, REGION_NAME, AREA_CODE, AREA_NAME, TERRITORY_CODE, TERRITORY_NAME, MARKET_CODE, MARKET_NAME, CUSTOMER_CODE, CUSTOMER_NAME, DB_LOCATION, TOTAL_RETAILER, PRODUCT_CATEGORY_CODE, PRODUCT_CATEGORY_NAME, PRODUCT_CODE, PRODUCT_NAME, PACK_SIZE, NO_OF_RETAILER, AVG_ORDER_QTY_PER_RET, AVG_SALES_QTY_PER_INVOICE, AVG_SALES_QTY_PER_MONTH_RET " +
                          " FROM MV_SKU_NUMERIC_SALES_LMONTH " +
                          " WHERE (DIVISION_CODE = '" + dCode + "' OR '" + dCode + "' = 'ALL')" +
                          " AND   (REGION_CODE='" + rCode + "' OR '" + rCode + "'='ALL')" +
                          " AND   (AREA_CODE='" + aCode + "' OR '" + aCode + "'='ALL')" +
                          " AND   (TERRITORY_CODE='" + tCode + "' OR '" + tCode + "'='ALL')" +
                          " AND   (CUSTOMER_CODE='" + cCode + "' OR '" + cCode + "'='ALL')" +
                          " AND   (PRODUCT_CATEGORY_CODE='" + pcCode + "' OR '" + pcCode + "'='ALL')" +
                          " AND   PRODUCT_CODE IN (SELECT PRODUCT_CODE FROM USER_PRODUCT_DTL WHERE USER_ID=" + userId + ")" +
                          " ORDER BY DIVISION_CODE,REGION_CODE, AREA_CODE, TERRITORY_CODE, CUSTOMER_CODE,MARKET_CODE,PRODUCT_CODE";


                }
                else
                {

                    qry = " SELECT DIVISION_CODE, DIVISION_NAME, REGION_CODE, REGION_NAME, AREA_CODE, AREA_NAME, TERRITORY_CODE, TERRITORY_NAME, MARKET_CODE, MARKET_NAME, CUSTOMER_CODE, CUSTOMER_NAME, DB_LOCATION, TOTAL_RETAILER, PRODUCT_CATEGORY_CODE, PRODUCT_CATEGORY_NAME, PRODUCT_CODE, PRODUCT_NAME, PACK_SIZE, NO_OF_RETAILER, AVG_ORDER_QTY_PER_RET, AVG_SALES_QTY_PER_INVOICE, AVG_SALES_QTY_PER_MONTH_RET " +
                            " FROM MV_SKU_NUMERIC_SALES_LMONTH " +
                            " WHERE (DIVISION_CODE = '" + dCode + "' OR '" + dCode + "' = 'ALL')" +
                            " AND   (REGION_CODE='" + rCode + "' OR '" + rCode + "'='ALL')" +
                            " AND   (AREA_CODE='" + aCode + "' OR '" + aCode + "'='ALL')" +
                            " AND   (TERRITORY_CODE='" + tCode + "' OR '" + tCode + "'='ALL')" +
                            " AND   (CUSTOMER_CODE='" + cCode + "' OR '" + cCode + "'='ALL')" +
                            " AND   (PRODUCT_CATEGORY_CODE='" + pcCode + "' OR '" + pcCode + "'='ALL')" +
                            " ORDER BY DIVISION_CODE,REGION_CODE, AREA_CODE, TERRITORY_CODE, CUSTOMER_CODE,MARKET_CODE,PRODUCT_CODE";


                }




                //DataTable dt = _dbHelper.GetDataTable(qry);
                DataTable dt = _dbHelper.GetDataTableWithAuditTrial(qry, "SKU Wise Numeric Distribution (Last Month)");

                int count = 0;
                var item = (from DataRow row in dt.Rows
                            select new SkuNumericSalesAnalysisBEL
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
                                TotalRetailer = row["TOTAL_RETAILER"].ToString(),
                                ProductCategoryCode = row["PRODUCT_CATEGORY_CODE"].ToString(),
                                ProductCategoryName = row["PRODUCT_CATEGORY_NAME"].ToString(),
                                ProductCode = row["PRODUCT_CODE"].ToString(),
                                ProductName = row["PRODUCT_NAME"].ToString(),
                                PackSize = row["PACK_SIZE"].ToString(),
                                NoOfRetailer = row["NO_OF_RETAILER"].ToString(),
                                AvgOrderQtyPerRet = row["AVG_ORDER_QTY_PER_RET"].ToString(),
                                AvgSalesQtyPerInvoice = row["AVG_SALES_QTY_PER_INVOICE"].ToString(),
                                AvgSalesQtyPerMonthRet = row["AVG_SALES_QTY_PER_MONTH_RET"].ToString()
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

        public object GetSkuNumericSalesAnalysisDateRange(string fromDate, string toDate, string dCode, string rCode, string aCode, string tCode, string cCode, string pcCode)
        {
            try
            {
                int userId = Convert.ToInt32(HttpContext.Current.Session["USER_ID"]);

                using (OracleConnection objConn = new OracleConnection(ConnString))
                {
                    using (OracleCommand objCmd = new OracleCommand())
                    {
                        objCmd.Connection = objConn;
                        objCmd.CommandText = "FN_SKU_NUMERIC_SAL_ANALYSIS";
                        objCmd.CommandType = CommandType.StoredProcedure;

                        objCmd.Parameters.Add("pfrom_date", OracleType.VarChar).Value = fromDate;
                        objCmd.Parameters.Add("pto_date", OracleType.VarChar).Value = toDate;
                        objCmd.Parameters.Add("pdivision_code", OracleType.VarChar).Value = dCode;
                        objCmd.Parameters.Add("pregion_code", OracleType.VarChar).Value = rCode;
                        objCmd.Parameters.Add("parea_code", OracleType.VarChar).Value = aCode;
                        objCmd.Parameters.Add("pterritory_code", OracleType.VarChar).Value = tCode;
                        objCmd.Parameters.Add("pcustomer_code", OracleType.VarChar).Value = cCode;
                        objCmd.Parameters.Add("pproduct_category", OracleType.VarChar).Value = pcCode;
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
                        _dbHelper.InsertReportAudit("SKU Wise Numeric Distribution (Custom Date)");

                        int count = 0;


                        List<SkuNumericSalesAnalysisBEL> item;
                        item = (from DataRow row in dt.Rows
                                select new SkuNumericSalesAnalysisBEL
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
                                    TotalRetailer = row["TOTAL_RETAILER"].ToString(),
                                    ProductCategoryCode = row["PRODUCT_CATEGORY_CODE"].ToString(),
                                    ProductCategoryName = row["PRODUCT_CATEGORY_NAME"].ToString(),
                                    ProductCode = row["PRODUCT_CODE"].ToString(),
                                    ProductName = row["PRODUCT_NAME"].ToString(),
                                    PackSize = row["PACK_SIZE"].ToString(),
                                    NoOfRetailer = row["NO_OF_RETAILER"].ToString(),
                                    AvgOrderQtyPerRet = row["AVG_ORDER_QTY_PER_RET"].ToString(),
                                    AvgSalesQtyPerInvoice = row["AVG_SALES_QTY_PER_INVOICE"].ToString(),
                                    AvgSalesQtyPerMonthRet = row["AVG_SALES_QTY_PER_MONTH_RET"].ToString()

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