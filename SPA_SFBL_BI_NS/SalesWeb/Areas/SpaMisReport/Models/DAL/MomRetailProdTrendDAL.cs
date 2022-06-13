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
    public class MomRetailProdTrendDAL : ReturnData
    {

        private readonly DBHelper _dbHelper = new DBHelper();
        private readonly ErrorLogger _errorLogger = new ErrorLogger();
        private static readonly DBConnection DBConnection = new DBConnection();
        public readonly string ConnString = DBConnection.SAConnStrReader("Oracle", "PHARMAERP");


        public object GetProductList(string bCode, string bpCode, string pcCode)
        {
            try
            {
                var qry = " SELECT 'ALL' PRODUCT_CODE, 'ALL' PRODUCT_NAME, 'ALL' PACK_SIZE,  1 SL FROM DUAL " +
                          " UNION SELECT PRODUCT_CODE,PRODUCT_NAME,PACK_SIZE ,2 SL  FROM MV_PRODUCT_INFO" +
                          " WHERE STATUS='A'" +
                          " AND (BRAND_CODE = '" + bCode + "' OR '" + bCode + "' = 'ALL')" +
                          " AND (BASE_PRODUCT_CODE = '" + bpCode + "' OR '" + bpCode + "' = 'ALL')" +
                          " AND (PRODUCT_CATEGORY = '" + pcCode + "' OR '" + pcCode + "' = 'ALL')" +
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


        public List<MomRetailProdTrendBEL> GetMomRetailProdTrend(string dCode, string rCode, string aCode, string tCode, string cCode, string bCode, string bpCode, string pcCode, string pCode)
        {
            try
            {

                string qry;

                int userId = Convert.ToInt32(HttpContext.Current.Session["USER_ID"]);
                int UserAccessCount = Convert.ToInt32(HttpContext.Current.Session["USER_BASE_REPORT_FILTER"]);

                if (UserAccessCount > 0)
                {


                    qry = " SELECT DIVISION_CODE, DIVISION_NAME, REGION_CODE, REGION_NAME, AREA_CODE, AREA_NAME, TERRITORY_CODE, TERRITORY_NAME, CUSTOMER_CODE, CUSTOMER_NAME, DB_LOCATION, MARKET_CODE, MARKET_NAME, ROUTE_CODE, ROUTE_NAME, RETAILER_CODE, RETAILER_NAME, PRODUCT_CODE, PRODUCT_NAME, PACK_SIZE," +
                          " JAN_SALES_QTY," +
                          " JAN_SALES_VAL," +
                          " FEB_SALES_QTY," +
                          " FEB_SALES_VAL," +
                          " MAR_SALES_QTY," +
                          " MAR_SALES_VAL," +
                          " APR_SALES_QTY," +
                          " APR_SALES_VAL," +
                          " MAY_SALES_QTY," +
                          " MAY_SALES_VAL," +
                          " JUN_SALES_QTY," +
                          " JUN_SALES_VAL," +
                          " JUL_SALES_QTY," +
                          " JUL_SALES_VAL," +
                          " AUG_SALES_QTY," +
                          " AUG_SALES_VAL," +
                          " SEP_SALES_QTY," +
                          " SEP_SALES_VAL," +
                          " OCT_SALES_QTY," +
                          " OCT_SALES_VAL," +
                          " NOV_SALES_QTY," +
                          " NOV_SALES_VAL," +
                          " DEC_SALES_QTY," +
                          " DEC_SALES_VAL," +
                          " TOTAL_SALES_QTY," +
                          " TOTAL_SALES_VAL " +
                          " FROM MV_MOM_RETAIL_PRODUCT_TREND " +
                         " WHERE (DIVISION_CODE = '" + dCode + "' OR '" + dCode + "' = 'ALL')" +
                         " AND   (REGION_CODE='" + rCode + "' OR '" + rCode + "'='ALL')" +
                         " AND   (AREA_CODE='" + aCode + "' OR '" + aCode + "'='ALL')" +
                         " AND   (TERRITORY_CODE='" + tCode + "' OR '" + tCode + "'='ALL')" +
                         " AND   (CUSTOMER_CODE='" + cCode + "' OR '" + cCode + "'='ALL')" +

                         " AND   (BRAND_CODE='" + bCode + "' OR '" + bCode + "'='ALL')" +
                         " AND   (BASE_PRODUCT_CODE='" + bpCode + "' OR '" + bpCode + "'='ALL')" +
                         " AND   (PRODUCT_CATEGORY='" + pcCode + "' OR '" + pcCode + "'='ALL')" +

                         " AND   (PRODUCT_CODE='" + pCode + "' OR '" + pCode + "'='ALL')" +
                         
                         " AND   PRODUCT_CODE IN (SELECT PRODUCT_CODE FROM USER_PRODUCT_DTL WHERE USER_ID=" + userId + ")" +
                         " ORDER BY DIVISION_CODE,REGION_CODE, AREA_CODE, TERRITORY_CODE, CUSTOMER_CODE,MARKET_CODE,ROUTE_CODE,RETAILER_CODE,PRODUCT_CODE";
                }
                else
                {

                    qry = " SELECT DIVISION_CODE, DIVISION_NAME, REGION_CODE, REGION_NAME, AREA_CODE, AREA_NAME, TERRITORY_CODE, TERRITORY_NAME, CUSTOMER_CODE, CUSTOMER_NAME, DB_LOCATION, MARKET_CODE, MARKET_NAME, ROUTE_CODE, ROUTE_NAME, RETAILER_CODE, RETAILER_NAME, PRODUCT_CODE, PRODUCT_NAME, PACK_SIZE," +
                          " JAN_SALES_QTY," +
                          " JAN_SALES_VAL," +
                          " FEB_SALES_QTY," +
                          " FEB_SALES_VAL," +
                          " MAR_SALES_QTY," +
                          " MAR_SALES_VAL," +
                          " APR_SALES_QTY," +
                          " APR_SALES_VAL," +
                          " MAY_SALES_QTY," +
                          " MAY_SALES_VAL," +
                          " JUN_SALES_QTY," +
                          " JUN_SALES_VAL," +
                          " JUL_SALES_QTY," +
                          " JUL_SALES_VAL," +
                          " AUG_SALES_QTY," +
                          " AUG_SALES_VAL," +
                          " SEP_SALES_QTY," +
                          " SEP_SALES_VAL," +
                          " OCT_SALES_QTY," +
                          " OCT_SALES_VAL," +
                          " NOV_SALES_QTY," +
                          " NOV_SALES_VAL," +
                          " DEC_SALES_QTY," +
                          " DEC_SALES_VAL," +
                          " TOTAL_SALES_QTY," +
                          " TOTAL_SALES_VAL " +
                          " FROM MV_MOM_RETAIL_PRODUCT_TREND_CY " +
                          " WHERE (DIVISION_CODE = '" + dCode + "' OR '" + dCode + "' = 'ALL')" +
                          " AND   (REGION_CODE='" + rCode + "' OR '" + rCode + "'='ALL')" +
                          " AND   (AREA_CODE='" + aCode + "' OR '" + aCode + "'='ALL')" +
                          " AND   (TERRITORY_CODE='" + tCode + "' OR '" + tCode + "'='ALL')" +
                          " AND   (CUSTOMER_CODE='" + cCode + "' OR '" + cCode + "'='ALL')" +

                         " AND   (BRAND_CODE='" + bCode + "' OR '" + bCode + "'='ALL')" +
                         " AND   (BASE_PRODUCT_CODE='" + bpCode + "' OR '" + bpCode + "'='ALL')" +
                         " AND   (PRODUCT_CATEGORY='" + pcCode + "' OR '" + pcCode + "'='ALL')" +

                          " AND   (PRODUCT_CODE='" + pCode + "' OR '" + pCode + "'='ALL')" +
                          " ORDER BY DIVISION_CODE,REGION_CODE, AREA_CODE, TERRITORY_CODE, CUSTOMER_CODE,MARKET_CODE,ROUTE_CODE,RETAILER_CODE,PRODUCT_CODE";

                }


                //DataTable dt = _dbHelper.GetDataTable(qry);
                DataTable dt = _dbHelper.GetDataTableWithAuditTrial(qry, "Retailer Product Trend (Current Year)");
                Int64 cont = dt.Rows.Count;

                var item = (from DataRow row in dt.Rows
                            select new MomRetailProdTrendBEL
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

                                ProductCode = row["PRODUCT_CODE"].ToString(),
                                ProductName = row["PRODUCT_NAME"].ToString(),
                                PackSize = row["PACK_SIZE"].ToString(),

                                JanSalesQty = Convert.ToInt32(row["JAN_SALES_QTY"]),
                                JanSalesVal = Convert.ToDouble(row["JAN_SALES_VAL"]),


                                FebSalesQty = Convert.ToInt32(row["FEB_SALES_QTY"]),
                                FebSalesVal = Convert.ToDouble(row["FEB_SALES_VAL"]),

                                MarSalesQty = Convert.ToInt32(row["MAR_SALES_QTY"]),
                                MarSalesVal = Convert.ToDouble(row["MAR_SALES_VAL"]),

                                AprSalesQty = Convert.ToInt32(row["APR_SALES_QTY"]),
                                AprSalesVal = Convert.ToDouble(row["APR_SALES_VAL"]),

                                MaySalesQty = Convert.ToInt32(row["MAY_SALES_QTY"]),
                                MaySalesVal = Convert.ToDouble(row["MAY_SALES_VAL"]),

                                JunSalesQty = Convert.ToInt32(row["JUN_SALES_QTY"]),
                                JunSalesVal = Convert.ToDouble(row["JUN_SALES_VAL"]),

                                JulSalesQty = Convert.ToInt32(row["JUL_SALES_QTY"]),
                                JulSalesVal = Convert.ToDouble(row["JUL_SALES_VAL"]),

                                AugSalesQty = Convert.ToInt32(row["AUG_SALES_QTY"]),
                                AugSalesVal = Convert.ToDouble(row["AUG_SALES_VAL"]),

                                SepSalesQty = Convert.ToInt32(row["SEP_SALES_QTY"]),
                                SepSalesVal = Convert.ToDouble(row["SEP_SALES_VAL"]),

                                OctSalesQty = Convert.ToInt32(row["OCT_SALES_QTY"]),
                                OctSalesVal = Convert.ToDouble(row["OCT_SALES_VAL"]),

                                NovSalesQty = Convert.ToInt32(row["NOV_SALES_QTY"]),
                                NovSalesVal = Convert.ToDouble(row["NOV_SALES_VAL"]),

                                DecSalesQty = Convert.ToInt32(row["DEC_SALES_QTY"]),
                                DecSalesVal = Convert.ToDouble(row["DEC_SALES_VAL"]),

                                TotalSalesQty = Convert.ToInt32(row["TOTAL_SALES_QTY"]),
                                TotalSalesVal = Convert.ToDouble(row["TOTAL_SALES_VAL"])


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
        public List<MomRetailProdTrendBEL> GetMomRetailProdTrendLy(string dCode, string rCode, string aCode, string tCode, string cCode, string bCode, string bpCode, string pcCode, string pCode)
        {
            try
            {

                string qry;

                int userId = Convert.ToInt32(HttpContext.Current.Session["USER_ID"]);
                int UserAccessCount = Convert.ToInt32(HttpContext.Current.Session["USER_BASE_REPORT_FILTER"]);

                if (UserAccessCount > 0)
                {


                    qry = " SELECT DIVISION_CODE, DIVISION_NAME, REGION_CODE, REGION_NAME, AREA_CODE, AREA_NAME, TERRITORY_CODE, TERRITORY_NAME, CUSTOMER_CODE, CUSTOMER_NAME, DB_LOCATION, MARKET_CODE, MARKET_NAME, ROUTE_CODE, ROUTE_NAME, RETAILER_CODE, RETAILER_NAME, PRODUCT_CODE, PRODUCT_NAME, PACK_SIZE," +
                          " JAN_SALES_QTY," +
                          " JAN_SALES_VAL," +
                          " FEB_SALES_QTY," +
                          " FEB_SALES_VAL," +
                          " MAR_SALES_QTY," +
                          " MAR_SALES_VAL," +
                          " APR_SALES_QTY," +
                          " APR_SALES_VAL," +
                          " MAY_SALES_QTY," +
                          " MAY_SALES_VAL," +
                          " JUN_SALES_QTY," +
                          " JUN_SALES_VAL," +
                          " JUL_SALES_QTY," +
                          " JUL_SALES_VAL," +
                          " AUG_SALES_QTY," +
                          " AUG_SALES_VAL," +
                          " SEP_SALES_QTY," +
                          " SEP_SALES_VAL," +
                          " OCT_SALES_QTY," +
                          " OCT_SALES_VAL," +
                          " NOV_SALES_QTY," +
                          " NOV_SALES_VAL," +
                          " DEC_SALES_QTY," +
                          " DEC_SALES_VAL," +
                          " TOTAL_SALES_QTY," +
                          " TOTAL_SALES_VAL " +
                          " FROM MV_MOM_RETAIL_PRODUCT_TREND_LY " +
                         " WHERE (DIVISION_CODE = '" + dCode + "' OR '" + dCode + "' = 'ALL')" +
                         " AND   (REGION_CODE='" + rCode + "' OR '" + rCode + "'='ALL')" +
                         " AND   (AREA_CODE='" + aCode + "' OR '" + aCode + "'='ALL')" +
                         " AND   (TERRITORY_CODE='" + tCode + "' OR '" + tCode + "'='ALL')" +
                         " AND   (CUSTOMER_CODE='" + cCode + "' OR '" + cCode + "'='ALL')" +

                         " AND   (BRAND_CODE='" + bCode + "' OR '" + bCode + "'='ALL')" +
                         " AND   (BASE_PRODUCT_CODE='" + bpCode + "' OR '" + bpCode + "'='ALL')" +
                         " AND   (PRODUCT_CATEGORY='" + pcCode + "' OR '" + pcCode + "'='ALL')" +

                         " AND   (PRODUCT_CODE='" + pCode + "' OR '" + pCode + "'='ALL')" +

                         " AND   PRODUCT_CODE IN (SELECT PRODUCT_CODE FROM USER_PRODUCT_DTL WHERE USER_ID=" + userId + ")" +
                         " ORDER BY DIVISION_CODE,REGION_CODE, AREA_CODE, TERRITORY_CODE, CUSTOMER_CODE,MARKET_CODE,ROUTE_CODE,RETAILER_CODE,PRODUCT_CODE";
                }
                else
                {

                    qry = " SELECT DIVISION_CODE, DIVISION_NAME, REGION_CODE, REGION_NAME, AREA_CODE, AREA_NAME, TERRITORY_CODE, TERRITORY_NAME, CUSTOMER_CODE, CUSTOMER_NAME, DB_LOCATION, MARKET_CODE, MARKET_NAME, ROUTE_CODE, ROUTE_NAME, RETAILER_CODE, RETAILER_NAME, PRODUCT_CODE, PRODUCT_NAME, PACK_SIZE," +
                          " JAN_SALES_QTY," +
                          " JAN_SALES_VAL," +
                          " FEB_SALES_QTY," +
                          " FEB_SALES_VAL," +
                          " MAR_SALES_QTY," +
                          " MAR_SALES_VAL," +
                          " APR_SALES_QTY," +
                          " APR_SALES_VAL," +
                          " MAY_SALES_QTY," +
                          " MAY_SALES_VAL," +
                          " JUN_SALES_QTY," +
                          " JUN_SALES_VAL," +
                          " JUL_SALES_QTY," +
                          " JUL_SALES_VAL," +
                          " AUG_SALES_QTY," +
                          " AUG_SALES_VAL," +
                          " SEP_SALES_QTY," +
                          " SEP_SALES_VAL," +
                          " OCT_SALES_QTY," +
                          " OCT_SALES_VAL," +
                          " NOV_SALES_QTY," +
                          " NOV_SALES_VAL," +
                          " DEC_SALES_QTY," +
                          " DEC_SALES_VAL," +
                          " TOTAL_SALES_QTY," +
                          " TOTAL_SALES_VAL " +
                          " FROM MV_MOM_RETAIL_PRODUCT_TREND_LY " +
                          " WHERE (DIVISION_CODE = '" + dCode + "' OR '" + dCode + "' = 'ALL')" +
                          " AND   (REGION_CODE='" + rCode + "' OR '" + rCode + "'='ALL')" +
                          " AND   (AREA_CODE='" + aCode + "' OR '" + aCode + "'='ALL')" +
                          " AND   (TERRITORY_CODE='" + tCode + "' OR '" + tCode + "'='ALL')" +
                          " AND   (CUSTOMER_CODE='" + cCode + "' OR '" + cCode + "'='ALL')" +

                         " AND   (BRAND_CODE='" + bCode + "' OR '" + bCode + "'='ALL')" +
                         " AND   (BASE_PRODUCT_CODE='" + bpCode + "' OR '" + bpCode + "'='ALL')" +
                         " AND   (PRODUCT_CATEGORY='" + pcCode + "' OR '" + pcCode + "'='ALL')" +

                          " AND   (PRODUCT_CODE='" + pCode + "' OR '" + pCode + "'='ALL')" +
                          " ORDER BY DIVISION_CODE,REGION_CODE, AREA_CODE, TERRITORY_CODE, CUSTOMER_CODE,MARKET_CODE,ROUTE_CODE,RETAILER_CODE,PRODUCT_CODE";

                }


                //DataTable dt = _dbHelper.GetDataTable(qry);
                DataTable dt = _dbHelper.GetDataTableWithAuditTrial(qry, "Retailer Product Trend (Last Year)");
                Int64 cont = dt.Rows.Count;

                var item = (from DataRow row in dt.Rows
                            select new MomRetailProdTrendBEL
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

                                ProductCode = row["PRODUCT_CODE"].ToString(),
                                ProductName = row["PRODUCT_NAME"].ToString(),
                                PackSize = row["PACK_SIZE"].ToString(),

                                JanSalesQty = Convert.ToInt32(row["JAN_SALES_QTY"]),
                                JanSalesVal = Convert.ToDouble(row["JAN_SALES_VAL"]),


                                FebSalesQty = Convert.ToInt32(row["FEB_SALES_QTY"]),
                                FebSalesVal = Convert.ToDouble(row["FEB_SALES_VAL"]),

                                MarSalesQty = Convert.ToInt32(row["MAR_SALES_QTY"]),
                                MarSalesVal = Convert.ToDouble(row["MAR_SALES_VAL"]),

                                AprSalesQty = Convert.ToInt32(row["APR_SALES_QTY"]),
                                AprSalesVal = Convert.ToDouble(row["APR_SALES_VAL"]),

                                MaySalesQty = Convert.ToInt32(row["MAY_SALES_QTY"]),
                                MaySalesVal = Convert.ToDouble(row["MAY_SALES_VAL"]),

                                JunSalesQty = Convert.ToInt32(row["JUN_SALES_QTY"]),
                                JunSalesVal = Convert.ToDouble(row["JUN_SALES_VAL"]),

                                JulSalesQty = Convert.ToInt32(row["JUL_SALES_QTY"]),
                                JulSalesVal = Convert.ToDouble(row["JUL_SALES_VAL"]),

                                AugSalesQty = Convert.ToInt32(row["AUG_SALES_QTY"]),
                                AugSalesVal = Convert.ToDouble(row["AUG_SALES_VAL"]),

                                SepSalesQty = Convert.ToInt32(row["SEP_SALES_QTY"]),
                                SepSalesVal = Convert.ToDouble(row["SEP_SALES_VAL"]),

                                OctSalesQty = Convert.ToInt32(row["OCT_SALES_QTY"]),
                                OctSalesVal = Convert.ToDouble(row["OCT_SALES_VAL"]),

                                NovSalesQty = Convert.ToInt32(row["NOV_SALES_QTY"]),
                                NovSalesVal = Convert.ToDouble(row["NOV_SALES_VAL"]),

                                DecSalesQty = Convert.ToInt32(row["DEC_SALES_QTY"]),
                                DecSalesVal = Convert.ToDouble(row["DEC_SALES_VAL"]),

                                TotalSalesQty = Convert.ToInt32(row["TOTAL_SALES_QTY"]),
                                TotalSalesVal = Convert.ToDouble(row["TOTAL_SALES_VAL"])


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