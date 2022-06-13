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
    public class RetailerDetailsDAL : ReturnData
    {


        private readonly DBHelper _dbHelper = new DBHelper();
        private readonly ErrorLogger _errorLogger = new ErrorLogger();
        private static readonly DBConnection DBConnection = new DBConnection();
        public readonly string ConnString = DBConnection.SAConnStrReader("Oracle", "PHARMAERP");

        public object GetMarketList(string dCode, string rCode, string aCode, string tCode, string cCode)
        {
            try
            {
                string qry = " SELECT 'ALL' MARKET_CODE, 'ALL' MARKET_NAME, 1 SL FROM DUAL " +
                             " UNION " +
                             " SELECT DISTINCT MARKET_CODE, MARKET_NAME, 2 SL FROM MV_DIVISION_CUSTOMER_RELATION" +
                             " WHERE (ZONE_CODE = '" + dCode + "' OR '" + dCode + "' = 'ALL')" +
                             " AND   (AREA_CODE='" + rCode + "' OR '" + rCode + "'='ALL')" +
                             " AND   (BELT_CODE='" + aCode + "' OR '" + aCode + "'='ALL')" +
                             " AND   (TERRITORY_CODE='" + tCode + "' OR '" + tCode + "'='ALL')" +
                             " AND   (CUSTOMER_CODE='" + cCode + "' OR '" + cCode + "'='ALL')" +
                             " ORDER BY SL, MARKET_NAME";
                DataTable dt = _dbHelper.GetDataTable(qry);
                var item = (from DataRow row in dt.Rows
                            select new MarketBEL
                            {
                                MarketCode = row["MARKET_CODE"].ToString(),
                                MarketName = row["MARKET_NAME"].ToString(),

                            }).ToList();
                return item;

            }
            catch (Exception e)
            {
                var lineNum = e.StackTrace.Substring(e.StackTrace.LastIndexOf(' '));
                _errorLogger.GetErrorMessage(e.Message, "RetailerDetailsDAL", lineNum);
                ExceptionReturn = e.Message;
                return "Not Ok";
            }
        }

        public object GetRouteList(string dCode, string rCode, string aCode, string tCode, string cCode,String mCode)
        {
            try
            {
                string qry = " SELECT 'ALL' ROUTE_CODE, 'ALL' ROUTE_NAME, 1 SL FROM DUAL " +
                             " UNION " +
                             " SELECT DISTINCT ROUTE_CODE, ROUTE_NAME, 2 SL FROM VW_DIVSION_RETAILER_RELATION" +
                             " WHERE (ZONE_CODE = '" + dCode + "' OR '" + dCode + "' = 'ALL')" +
                             " AND   (AREA_CODE='" + rCode + "' OR '" + rCode + "'='ALL')" +
                             " AND   (BELT_CODE='" + aCode + "' OR '" + aCode + "'='ALL')" +
                             " AND   (TERRITORY_CODE='" + tCode + "' OR '" + tCode + "'='ALL')" +
                             " AND   (CUSTOMER_CODE='" + cCode + "' OR '" + cCode + "'='ALL')" +
                             " AND   (MARKET_CODE='" + mCode + "' OR '" + mCode + "'='ALL')" +
                             " ORDER BY SL, ROUTE_NAME";
                DataTable dt = _dbHelper.GetDataTable(qry);
                var item = (from DataRow row in dt.Rows
                            select new RouteBEL
                            {
                                RouteCode = row["ROUTE_CODE"].ToString(),
                                RouteName = row["ROUTE_NAME"].ToString(),

                            }).ToList();
                return item;

            }
            catch (Exception e)
            {
                var lineNum = e.StackTrace.Substring(e.StackTrace.LastIndexOf(' '));
                _errorLogger.GetErrorMessage(e.Message, "RetailerDetailsDAL", lineNum);
                ExceptionReturn = e.Message;
                return "Not Ok";
            }
        }

        public object GetRetailerCategoryList()
        {
            try
            {
                string qry = " SELECT 'ALL' RETILER_CATEGORY_CODE, 'ALL' RETAILER_CATEGORY_NAME, 1 SL FROM DUAL " +
                             " UNION " +
                             " SELECT RETAILER_CATEGORY_CODE,RETAILER_CATEGORY_DETAILS RETAILER_CATEGORY_NAME, 2 SL " +
                             " FROM SPA_SFBL.RETAILER_CATEGORY_INFO@DL_SPASFBL.SQUAREGROUP.COM " +
                             " ORDER BY SL, RETAILER_CATEGORY_NAME";
                DataTable dt = _dbHelper.GetDataTable(qry);
                var item = (from DataRow row in dt.Rows
                            select new RetailerCategoryBEL
                            {
                                RetailerCategoryCode = row["RETILER_CATEGORY_CODE"].ToString(),
                                RetailerCategoryName = row["RETAILER_CATEGORY_NAME"].ToString(),

                            }).ToList();
                return item;

            }
            catch (Exception e)
            {
                var lineNum = e.StackTrace.Substring(e.StackTrace.LastIndexOf(' '));
                _errorLogger.GetErrorMessage(e.Message, "RetailerDetailsDAL", lineNum);
                ExceptionReturn = e.Message;
                return "Not Ok";
            }
        }
        

        //Query 
        public List<RetailerDetails2BEL> GetRetailerDetails(string dCode, string rCode, string aCode, string tCode, string cCode, string mCode, string rCatCode, string rType, string rlocType, string status)
        {
            try
            {
                string qry =  " SELECT" +
                              " DIVISION_CODE, DIVISION_NAME, REGION_CODE, REGION_NAME, AREA_CODE, AREA_NAME, TERRITORY_CODE, TERRITORY_NAME," +
                              " CUSTOMER_CODE, CUSTOMER_NAME, DB_LOCATION," +
                              " MARKET_CODE, MARKET_NAME," +
                              " ROUTE_CODE, ROUTE_NAME,MARKET_ROUTE_REL_STATUS," +
                              " RETAILER_CODE, RETAILER_NAME, ROUTE_RETAILER_REL_STATUS,RETAILER_STATUS," +
                              " RETAILER_NAME_BN," +
                              " ADDRESS," +
                              " RETAILER_ADDRESS_BN," +
                              " CONTACT_PERSON, CONTACT_NO, RETAILER_TYPE," +
                              //" RETAILER_CATEGORY_CODE," +
                              " RETAILER_CATEGORY_DESC," +
                              " LOCATION_TYPE," +
                              " TO_CHAR(RETAILER_ENTRY_DATE,'MM/DD/RRRR') RETAILER_ENTRY_DATE," +
                              //" RECOMMEND_STATUS, RECOMMEND_STATUS_DESC, RECOMMEND_BY," +
                              //" RECOMMEND_DATE, APPROVED_STATUS," +
                              " APPROVED_STATUS_DESC," +
                              //" APPROVED_BY,"+ 
                              " TO_CHAR(APPROVED_DATE,'MM/DD/RRRR') APPROVED_DATE," +
                              " MONTHLE_AVG_SALES," +
                              " TO_CHAR(FIRST_INVOICE_DATE,'MM/DD/RRRR')FIRST_INVOICE_DATE," +
                              " TO_CHAR(LAST_INVOICE_DATE,'MM/DD/RRRR')LAST_INVOICE_DATE" +
                              //" LAST_INVOICE_DAY " +
                              " FROM MV_RETAILER_DETAILS_INFO " +
                              " WHERE (DIVISION_CODE = '" + dCode + "' OR '" + dCode + "' = 'ALL')" +
                              " AND   (REGION_CODE='" + rCode + "' OR '" + rCode + "'='ALL')" +
                              " AND   (AREA_CODE='" + aCode + "' OR '" + aCode + "'='ALL')" +
                              " AND   (TERRITORY_CODE='" + tCode + "' OR '" + tCode + "'='ALL')" +
                              " AND   (CUSTOMER_CODE='" + cCode + "' OR '" + cCode + "'='ALL')" +
                              " AND   (MARKET_CODE='" + mCode + "' OR '" + mCode + "'='ALL')" +
                              " AND   (RETAILER_CATEGORY_CODE='" + rCatCode + "' OR '" + rCatCode + "'='ALL')" +
                              " AND   (RETAILER_TYPE='" + rType + "' OR '" + rType + "'='ALL')" +
                              " AND   (LOCATION_TYPE='" + rlocType + "' OR '" + rlocType + "'='ALL')" +
                              " AND   (RETAILER_STATUS='" + status + "' OR '" + status + "'='ALL')" +
                              " ORDER BY DIVISION_CODE,REGION_CODE, AREA_CODE, TERRITORY_CODE, CUSTOMER_CODE, MARKET_CODE, ROUTE_CODE,RETAILER_CODE";


                //DataTable dt = _dbHelper.GetDataTable(qry);
                DataTable dt = _dbHelper.GetDataTableWithAuditTrial(qry, "Retailer Information");

                Int64 cont = dt.Rows.Count;
                var item = (from DataRow row in dt.Rows
                            select new RetailerDetails2BEL
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
                                DbLocation = row["DB_LOCATION"].ToString(),
                                MarketCode = row["MARKET_CODE"].ToString(),
                                MarketName = row["MARKET_NAME"].ToString(),
                                RouteCode = row["ROUTE_CODE"].ToString(),
                                RouteName = row["ROUTE_NAME"].ToString(),
                                MarketRouteRelStatus = row["MARKET_ROUTE_REL_STATUS"].ToString(),
                                RetailerCode = row["RETAILER_CODE"].ToString(),
                                RetailerName = row["RETAILER_NAME"].ToString(),
                                RetailerNameBn = row["RETAILER_NAME_BN"].ToString(),
                                Address = row["ADDRESS"].ToString(),
                                RetailerAddressBn = row["RETAILER_ADDRESS_BN"].ToString(),
                                RouteRetailerRelStatus = row["ROUTE_RETAILER_REL_STATUS"].ToString(),
                                RetailerStatus = row["RETAILER_STATUS"].ToString(),
                                ContactPerson = row["CONTACT_PERSON"].ToString(),
                                ContactNo = row["CONTACT_NO"].ToString(),
                                RetailerType = row["RETAILER_TYPE"].ToString(),
                                //RetailerCategoryCode = row["RETAILER_CATEGORY_CODE"].ToString(),
                                RetailerCategoryDesc = row["RETAILER_CATEGORY_DESC"].ToString(),
                                LocationType = row["LOCATION_TYPE"].ToString(),
                                RetailerEntryDate = row["RETAILER_ENTRY_DATE"].ToString(),
                                //RecommendStatus = row["RECOMMEND_STATUS"].ToString(),
                                //RecommendStatusDesc = row["RECOMMEND_STATUS_DESC"].ToString(),
                                //RecommendBy = row["RECOMMEND_BY"].ToString(),
                                //RecommendDate = row["RECOMMEND_DATE"].ToString(),
                                //ApprovedStatus = row["APPROVED_STATUS"].ToString(),
                                ApprovedStatusDesc = row["APPROVED_STATUS_DESC"].ToString(),
                                //ApprovedBy = row["APPROVED_BY"].ToString(),
                                ApprovedDate = row["APPROVED_DATE"].ToString(),
                                MonthleAvgSales = row["MONTHLE_AVG_SALES"].ToString(),
                                FirstInvoiceDate = row["FIRST_INVOICE_DATE"].ToString(),
                                LastInvoiceDate = row["LAST_INVOICE_DATE"].ToString()
                                //LastInvoiceDay = row["LAST_INVOICE_DAY"].ToString(),
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
        public List<RetailerDetails2BEL> GetDeadRetailer(string fDate, string dCode, string rCode, string aCode, string tCode, string cCode, string mCode, string rCatCode, string rType, string rlocType, string status)
        {
            try
            {
                string qry =  " SELECT DIVISION_CODE, DIVISION_NAME, REGION_CODE, REGION_NAME, AREA_CODE, AREA_NAME," +
                              " TERRITORY_CODE, TERRITORY_NAME, CUSTOMER_CODE, CUSTOMER_NAME, DB_LOCATION, MARKET_CODE," +
                              " MARKET_NAME, ROUTE_CODE, ROUTE_NAME,MARKET_ROUTE_REL_STATUS," +
                              " RETAILER_CODE, RETAILER_NAME, ROUTE_RETAILER_REL_STATUS,RETAILER_STATUS, RETAILER_NAME_BN," +
                              " ADDRESS, RETAILER_ADDRESS_BN," +
                              " CONTACT_PERSON, CONTACT_NO," +
                              " RETAILER_TYPE," +
                              //" RETAILER_CATEGORY_CODE, " +
                              " RETAILER_CATEGORY_DESC, LOCATION_TYPE," +
                              " TO_CHAR(RETAILER_ENTRY_DATE,'MM/DD/RRRR') RETAILER_ENTRY_DATE," +
                              //" RECOMMEND_STATUS," +
                              //" RECOMMEND_STATUS_DESC," +
                              //" RECOMMEND_BY," +
                              //" RECOMMEND_DATE, " +
                              //" APPROVED_STATUS," +
                              //" APPROVED_STATUS_DESC," +
                              //" APPROVED_BY, " +
                              " TO_CHAR(APPROVED_DATE,'MM/DD/RRRR') APPROVED_DATE, " +
                              " MONTHLE_AVG_SALES," +
                              " TO_CHAR(FIRST_INVOICE_DATE,'MM/DD/RRRR')FIRST_INVOICE_DATE," +
                              " TO_CHAR(LAST_INVOICE_DATE,'MM/DD/RRRR')LAST_INVOICE_DATE" +
                              //" LAST_INVOICE_DAY " +
                              " FROM MV_RETAILER_DETAILS_INFO " +
                              " WHERE (DIVISION_CODE = '" + dCode + "' OR '" + dCode + "' = 'ALL')" +
                              " AND   (REGION_CODE='" + rCode + "' OR '" + rCode + "'='ALL')" +
                              " AND   (AREA_CODE='" + aCode + "' OR '" + aCode + "'='ALL')" +
                              " AND   (TERRITORY_CODE='" + tCode + "' OR '" + tCode + "'='ALL')" +
                              " AND   (CUSTOMER_CODE='" + cCode + "' OR '" + cCode + "'='ALL')" +
                              " AND   (MARKET_CODE='" + mCode + "' OR '" + mCode + "'='ALL')" +
                              " AND   (RETAILER_CATEGORY_CODE='" + rCatCode + "' OR '" + rCatCode + "'='ALL')" +
                              " AND   (RETAILER_TYPE='" + rType + "' OR '" + rType + "'='ALL')" +
                              " AND   (LOCATION_TYPE='" + rlocType + "' OR '" + rlocType + "'='ALL')" +
                              " AND   (RETAILER_STATUS='" + status + "' OR '" + status + "'='ALL')" +
                              //" AND   TO_DATE(LAST_INVOICE_DATE,'DD/MM/RRRR') <= TO_DATE('"+ fDate + "','DD/MM/RRRR')" +

                              " AND   APPROVED_DATE IS NOT NULL " +
                              " AND FIRST_INVOICE_DATE IS NULL" +
                              " AND LAST_INVOICE_DATE IS NULL" +

                              " ORDER BY DIVISION_CODE,REGION_CODE, AREA_CODE, TERRITORY_CODE, CUSTOMER_CODE, MARKET_CODE, ROUTE_CODE,RETAILER_CODE";


                //DataTable dt = _dbHelper.GetDataTable(qry);
                DataTable dt = _dbHelper.GetDataTableWithAuditTrial(qry, "Dead Retailer Information");

                var item = (from DataRow row in dt.Rows
                            select new RetailerDetails2BEL
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
                                DbLocation = row["DB_LOCATION"].ToString(),
                                MarketCode = row["MARKET_CODE"].ToString(),
                                MarketName = row["MARKET_NAME"].ToString(),
                                RouteCode = row["ROUTE_CODE"].ToString(),
                                RouteName = row["ROUTE_NAME"].ToString(),
                                MarketRouteRelStatus = row["MARKET_ROUTE_REL_STATUS"].ToString(),
                                RetailerCode = row["RETAILER_CODE"].ToString(),
                                RetailerName = row["RETAILER_NAME"].ToString(),
                                RetailerNameBn = row["RETAILER_NAME_BN"].ToString(),
                                Address = row["ADDRESS"].ToString(),
                                RetailerAddressBn = row["RETAILER_ADDRESS_BN"].ToString(),
                                RouteRetailerRelStatus = row["ROUTE_RETAILER_REL_STATUS"].ToString(),
                                RetailerStatus = row["RETAILER_STATUS"].ToString(),
                                ContactPerson = row["CONTACT_PERSON"].ToString(),
                                ContactNo = row["CONTACT_NO"].ToString(),
                                RetailerType = row["RETAILER_TYPE"].ToString(),
                                //RetailerCategoryCode = row["RETAILER_CATEGORY_CODE"].ToString(),
                                RetailerCategoryDesc = row["RETAILER_CATEGORY_DESC"].ToString(),
                                LocationType = row["LOCATION_TYPE"].ToString(),
                                RetailerEntryDate = row["RETAILER_ENTRY_DATE"].ToString(),
                                //RecommendStatus = row["RECOMMEND_STATUS"].ToString(),
                                //RecommendStatusDesc = row["RECOMMEND_STATUS_DESC"].ToString(),
                                //RecommendBy = row["RECOMMEND_BY"].ToString(),
                                //RecommendDate = row["RECOMMEND_DATE"].ToString(),
                                //ApprovedStatus = row["APPROVED_STATUS"].ToString(),
                                //ApprovedStatusDesc = row["APPROVED_STATUS_DESC"].ToString(),
                                //ApprovedBy = row["APPROVED_BY"].ToString(),
                                ApprovedDate = row["APPROVED_DATE"].ToString(),
                                MonthleAvgSales = row["MONTHLE_AVG_SALES"].ToString(),
                                FirstInvoiceDate = row["FIRST_INVOICE_DATE"].ToString(),
                                LastInvoiceDate = row["LAST_INVOICE_DATE"].ToString()
                                //LastInvoiceDay = row["LAST_INVOICE_DAY"].ToString(),
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