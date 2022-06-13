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
    public class MomRetailerImsLyDAL : ReturnData
    {
        private readonly DBHelper _dbHelper = new DBHelper();
        private readonly ErrorLogger _errorLogger = new ErrorLogger();
        private static readonly DBConnection DBConnection = new DBConnection();
        public readonly string ConnString = DBConnection.SAConnStrReader("Oracle", "PHARMAERP");

        public List<MomRetailerImsLyBEL> GetMomRetailerImsLy(string dCode, string rCode, string aCode, string tCode, string cCode)
        {
            try
            {


                string qry = " SELECT DIVISION_CODE, DIVISION_NAME, REGION_CODE, REGION_NAME, AREA_CODE, AREA_NAME, TERRITORY_CODE, TERRITORY_NAME, CUSTOMER_CODE, CUSTOMER_NAME, DB_LOCATION, MARKET_CODE, MARKET_NAME, ROUTE_CODE, ROUTE_NAME, RETAILER_CODE, RETAILER_NAME," +
                              " JAN_NO_OF_INV, JAN_NET_IMS," +
                              " FEB_NO_OF_INV, FEB_NET_IMS," +
                              " MAR_NO_OF_INV, MAR_NET_IMS," +
                              " APR_NO_OF_INV, APR_NET_IMS," +
                              " MAY_NO_OF_INV, MAY_NET_IMS," +
                              " JUN_NO_OF_INV, JUN_NET_IMS," +
                              " JUL_NO_OF_INV, JUL_NET_IMS," +
                              " AUG_NO_OF_INV, AUG_NET_IMS," +
                              " SEP_NO_OF_INV, SEP_NET_IMS," +
                              " OCT_NO_OF_INV, OCT_NET_IMS," +
                              " NOV_NO_OF_INV, NOV_NET_IMS," +
                              " DEC_NO_OF_INV, DEC_NET_IMS" +
                              " FROM MV_RETAILER_IMS_LY " +
                              " WHERE (DIVISION_CODE = '" + dCode + "' OR '" + dCode + "' = 'ALL')" +
                              " AND   (REGION_CODE='" + rCode + "' OR '" + rCode + "'='ALL')" +
                              " AND   (AREA_CODE='" + aCode + "' OR '" + aCode + "'='ALL')" +
                              " AND   (TERRITORY_CODE='" + tCode + "' OR '" + tCode + "'='ALL')" +
                              " AND   (CUSTOMER_CODE='" + cCode + "' OR '" + cCode + "'='ALL')" +
                              " ORDER BY DIVISION_CODE,REGION_CODE, AREA_CODE, TERRITORY_CODE, CUSTOMER_CODE,MARKET_CODE,ROUTE_CODE,RETAILER_CODE";



                //DataTable dt = _dbHelper.GetDataTable(qry);
                DataTable dt = _dbHelper.GetDataTableWithAuditTrial(qry, "Retailer Wise IMS (Last Year - MOM)");
                Int64 cont = dt.Rows.Count;

                var item = (from DataRow row in dt.Rows
                            select new MomRetailerImsLyBEL
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
                                DbLocation = row["DB_LOCATION"].ToString(),
                                MarketCode = row["MARKET_CODE"].ToString(),
                                MarketName = row["MARKET_NAME"].ToString(),


                                RouteCode = row["ROUTE_CODE"].ToString(),
                                RouteName = row["ROUTE_NAME"].ToString(),
                                RetailerCode = row["RETAILER_CODE"].ToString(),
                                RetailerName = row["RETAILER_NAME"].ToString(),

                                JanNoofInv = Convert.ToInt32(row["JAN_NO_OF_INV"]),
                                JanNetIms = Convert.ToDouble(row["JAN_NET_IMS"]),


                                FebNoofInv = Convert.ToInt32(row["FEB_NO_OF_INV"]),
                                FebNetIms = Convert.ToDouble(row["FEB_NET_IMS"]),

                                MarNoofInv = Convert.ToInt32(row["MAR_NO_OF_INV"]),
                                MarNetIms = Convert.ToDouble(row["MAR_NET_IMS"]),

                                AprNoofInv = Convert.ToInt32(row["APR_NO_OF_INV"]),
                                AprNetIms = Convert.ToDouble(row["APR_NET_IMS"]),

                                MayNoofInv = Convert.ToInt32(row["MAY_NO_OF_INV"]),
                                MayNetIms = Convert.ToDouble(row["MAY_NET_IMS"]),

                                JunNoofInv = Convert.ToInt32(row["JUN_NO_OF_INV"]),
                                JunNetIms = Convert.ToDouble(row["JUN_NET_IMS"]),

                                JulNoofInv = Convert.ToInt32(row["JUL_NO_OF_INV"]),
                                JulNetIms = Convert.ToDouble(row["JUL_NET_IMS"]),

                                AugNoofInv = Convert.ToInt32(row["AUG_NO_OF_INV"]),
                                AugNetIms = Convert.ToDouble(row["AUG_NET_IMS"]),

                                SepNoofInv = Convert.ToInt32(row["SEP_NO_OF_INV"]),
                                SepNetIms = Convert.ToDouble(row["SEP_NET_IMS"]),

                                OctNoofInv = Convert.ToInt32(row["OCT_NO_OF_INV"]),
                                OctNetIms = Convert.ToDouble(row["OCT_NET_IMS"]),

                                NovNoofInv = Convert.ToInt32(row["NOV_NO_OF_INV"]),
                                NovNetIms = Convert.ToDouble(row["NOV_NET_IMS"]),

                                DecNoofInv = Convert.ToInt32(row["DEC_NO_OF_INV"]),
                                DecNetIms = Convert.ToDouble(row["DEC_NET_IMS"])


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