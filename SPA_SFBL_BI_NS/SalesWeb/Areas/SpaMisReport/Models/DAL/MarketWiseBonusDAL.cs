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
    public class MarketWiseBonusDAL : ReturnData
    {

        private readonly DBHelper _dbHelper = new DBHelper();
        private readonly ErrorLogger _errorLogger = new ErrorLogger();
        private static readonly DBConnection DBConnection = new DBConnection();
        public readonly string ConnString = DBConnection.SAConnStrReader("Oracle", "PHARMAERP");


        public object GetMarketWiseBonusCustomDate(string fDate, string tDate, string dCode, string rCode, string aCode, string tCode, string cCode)
        {
            try
            {
                using (OracleConnection objConn = new OracleConnection(ConnString))
                {
                    using (OracleCommand objCmd = new OracleCommand())
                    {
                        objCmd.Connection = objConn;
                        objCmd.CommandText = "FN_MARKET_WISE_BONUS";
                        objCmd.CommandType = CommandType.StoredProcedure;
                        objCmd.Parameters.Add("p_from_date", OracleType.VarChar).Value = fDate;
                        objCmd.Parameters.Add("p_to_date", OracleType.VarChar).Value = tDate;
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
                        _dbHelper.InsertReportAudit("Market Wise Product IMS(Custom Date)");

                        List<BonusReportBEL> item;
                        item = (from DataRow row in dt.Rows
                                select new BonusReportBEL
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
                                    //RouteCode = row["ROUTE_CODE"].ToString(),
                                    //RouteName = row["ROUTE_NAME"].ToString(),
                                    //RetailerCode = row["RETAILER_CODE"].ToString(),
                                    //RetailerName = row["RETAILER_NAME"].ToString(),
                                    //InvoiceNo = row["INVOICE_NO"].ToString(),
                                    //InvoiceDate = row["INVOICE_DATE"].ToString(),
                                    //OrderNo = row["ORDER_NO"].ToString(),
                                    //OrderDate = row["ORDER_DATE"].ToString(),
                                    ProductCode = row["PRODUCT_CODE"].ToString(),
                                    ProductName = row["PRODUCT_NAME"].ToString(),
                                    BrandCode = row["BRAND_CODE"].ToString(),
                                    BrandName = row["BRAND_NAME"].ToString(),
                                    CategoryCode = row["CATEGORY_CODE"].ToString(),
                                    CategoryName = row["CATEGORY_NAME"].ToString(),
                                    SkuCode = row["SKU_CODE"].ToString(),
                                    SkuName = row["SKU_NAME"].ToString(),
                                    PackSize = row["PACK_SIZE"].ToString(),

                                    ProductPrice = row["PRODUCT_PRICE"].ToString(),
                                    BonusPriceDiscount = row["BONUS_PRICE_DISCOUNT"].ToString(),
                                    RetBonusPriceDiscount = row["RET_BONUS_PRICE_DISCOUNT"].ToString(),
                                    NetBonusPriceDiscount = row["NET_BONUS_PRICE_DISCOUNT"].ToString(),

                                    BonusQty = row["BONUS_QTY"].ToString(),
                                    ReturnBonusQty = row["RET_BONUS_QTY"].ToString(),
                                    ImsBonusQty = row["IMS_BONUS_QTY"].ToString(),
                                    ImsBonusValue = row["IMS_BONUS_VAL"].ToString(),

                                    TradeBobusQty = row["TRADE_BONUS_QTY"].ToString(),
                                    RetrunTradeBobusQty = row["RET_TRADE_BONUS_QTY"].ToString(),
                                    ImsTradeBobusQty = row["IMS_TRADE_BONUS_QTY"].ToString(),
                                    ImsTradeBobusValue = row["IMS_TRADE_BONUS_VAL"].ToString(),

                                    ComboBobusQty = row["COMBO_BONUS_QTY"].ToString(),
                                    RetrunComboBobusQty = row["RET_COMBO_BONUS_QTY"].ToString(),
                                    ImsComboBobusQty = row["IMS_COMBO_BONUS_QTY"].ToString(),
                                    ImsComboBobusValue = row["IMS_COMBO_BONUS_VAL"].ToString(),

                                    DisplayBobusQty = row["DISPLAY_BONUS_QTY"].ToString(),
                                    RetrunDisplayBobusQty = row["RET_DISPLAY_BONUS_QTY"].ToString(),
                                    ImsDisplayBobusQty = row["IMS_DISPLAY_BONUS_QTY"].ToString(),
                                    ImsDisplayBobusValue = row["IMS_DISPLAY_BONUS_VAL"].ToString(),

                                    TotalBobusQty = row["TOTAL_BONUS_QTY"].ToString(),
                                    RetrunTotalBobusQty = row["TOTAL_RET_BONUS_QTY"].ToString(),
                                    ImsTotalBobusQty = row["TOTAL_IMS_BONUS_QTY"].ToString(),
                                    ImsTotalBobusValue = row["TOTAL_IMS_BONUS_VAL"].ToString()


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