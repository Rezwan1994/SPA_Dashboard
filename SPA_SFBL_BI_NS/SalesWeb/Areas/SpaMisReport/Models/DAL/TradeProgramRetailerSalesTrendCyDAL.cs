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
    public class TradeProgramRetailerSalesTrendCyDAL : ReturnData
    {
        private readonly DBHelper _dbHelper = new DBHelper();
        private readonly ErrorLogger _errorLogger = new ErrorLogger();
        private static readonly DBConnection DBConnection = new DBConnection();
        public readonly string ConnString = DBConnection.SAConnStrReader("Oracle", "PHARMAERP");


        public object GetTradeProgramRetailerSalesTrendCy(string dCode, string rCode, string aCode, string tCode, string cCode, string tradeNo, string eType)
        {
            try
            {
                using (OracleConnection objConn = new OracleConnection(ConnString))
                {
                    using (OracleCommand objCmd = new OracleCommand())
                    {
                        objCmd.Connection = objConn;
                        objCmd.CommandText = "FN_TRADE_PROG_RET_SALES_TND_CY";
                        objCmd.CommandType = CommandType.StoredProcedure;


                        objCmd.Parameters.Add("p_division_code", OracleType.VarChar).Value = dCode;
                        objCmd.Parameters.Add("p_region_code", OracleType.VarChar).Value = rCode;
                        objCmd.Parameters.Add("p_area_code", OracleType.VarChar).Value = aCode;
                        objCmd.Parameters.Add("p_territory_code", OracleType.VarChar).Value = tCode;
                        objCmd.Parameters.Add("p_customer_code", OracleType.VarChar).Value = cCode;
                        objCmd.Parameters.Add("p_program_no", OracleType.VarChar).Value = tradeNo;
                        objCmd.Parameters.Add("p_effect_type", OracleType.VarChar).Value = eType;


                        objCmd.Parameters.Add("return_value", OracleType.Cursor).Direction = ParameterDirection.ReturnValue;
                        objConn.Open();
                        objCmd.ExecuteNonQuery();
                        OracleDataReader rdr = objCmd.ExecuteReader();
                        DataTable dt = new DataTable();
                        if (rdr.HasRows)
                        {
                            dt.Load(rdr);
                        }
                        _dbHelper.InsertReportAudit("Trade Program Retailer Sales Trend (Current Year)");
                        List<TradeProgramRetailerSalesTrendCyBEL> item;
                        item = (from DataRow row in dt.Rows
                                select new TradeProgramRetailerSalesTrendCyBEL
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


                                    TradeProgramNo = row["TRADE_PROGRAM_NO"].ToString(),
                                    ProgramName = row["PROGRAM_NAME"].ToString(),
                                    EffectType = row["EFFECT_TYPE"].ToString(),
                                    TradePolicyNo = row["TRADE_POLICY_NO"].ToString(),


                                    SlabTargetVal = Convert.ToDouble(row["SLAB_TARGET_VAL"]),
                                    SlabUpperAmt = Convert.ToDouble(row["SLAB_UPPER_AMT"]),

                                    NoOfInv = Convert.ToInt32(row["NO_OF_INV"]),
                                    Gift = row["GIFT"].ToString(),

                                    DiscountAmt = Convert.ToDouble(row["DISCOUNT_AMT"]),
                                    DiscountPercentage = Convert.ToInt32(row["DISCOUNT_PERCENTAGE"]),

                                    EntryDate = row["ENTRY_DATE"].ToString(),
                                    ProgramTypeCode = row["PROGRAM_TYPE_CODE"].ToString(),
                                    ProgramType = row["PROGRAM_TYPE"].ToString(),
                                    EffectFromDate = row["EFFECT_FROM_DATE"].ToString(),
                                    EffectToDate = row["EFFECT_TO_DATE"].ToString(),

                                    Sales = Convert.ToDouble(row["SALES_VALUE"]),
                                    Return = Convert.ToDouble(row["RETURN_VALUE"]),
                                    Ims = Convert.ToDouble(row["IMS_VALUE"]),

                                    JanIms = Convert.ToDouble(row["JAN_IMS_VALUE"]),
                                    FebIms = Convert.ToDouble(row["FEB_IMS_VALUE"]),
                                    MarIms = Convert.ToDouble(row["MAR_IMS_VALUE"]),
                                    AprIms = Convert.ToDouble(row["APR_IMS_VALUE"]),
                                    MayIms = Convert.ToDouble(row["MAY_IMS_VALUE"]),
                                    JunIms = Convert.ToDouble(row["JUN_IMS_VALUE"]),
                                    JulIms = Convert.ToDouble(row["JUL_IMS_VALUE"]),
                                    AugIms = Convert.ToDouble(row["AUG_IMS_VALUE"]),
                                    SepIms = Convert.ToDouble(row["SEP_IMS_VALUE"]),
                                    OctIms = Convert.ToDouble(row["OCT_IMS_VALUE"]),
                                    NovIms = Convert.ToDouble(row["NOV_IMS_VALUE"]),
                                    DecIms = Convert.ToDouble(row["DEC_IMS_VALUE"])

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

        public object GetTradeProgramRetailerSalesTrendLy(string dCode, string rCode, string aCode, string tCode, string cCode, string tradeNo, string eType)
        {
            try
            {
                using (OracleConnection objConn = new OracleConnection(ConnString))
                {
                    using (OracleCommand objCmd = new OracleCommand())
                    {
                        objCmd.Connection = objConn;
                        objCmd.CommandText = "FN_TRADE_PROG_RET_SALES_TND_LY";
                        objCmd.CommandType = CommandType.StoredProcedure;


                        objCmd.Parameters.Add("p_division_code", OracleType.VarChar).Value = dCode;
                        objCmd.Parameters.Add("p_region_code", OracleType.VarChar).Value = rCode;
                        objCmd.Parameters.Add("p_area_code", OracleType.VarChar).Value = aCode;
                        objCmd.Parameters.Add("p_territory_code", OracleType.VarChar).Value = tCode;
                        objCmd.Parameters.Add("p_customer_code", OracleType.VarChar).Value = cCode;
                        objCmd.Parameters.Add("p_program_no", OracleType.VarChar).Value = tradeNo;
                        objCmd.Parameters.Add("p_effect_type", OracleType.VarChar).Value = eType;


                        objCmd.Parameters.Add("return_value", OracleType.Cursor).Direction = ParameterDirection.ReturnValue;
                        objConn.Open();
                        objCmd.ExecuteNonQuery();
                        OracleDataReader rdr = objCmd.ExecuteReader();
                        DataTable dt = new DataTable();
                        if (rdr.HasRows)
                        {
                            dt.Load(rdr);
                        }
                        _dbHelper.InsertReportAudit("Trade Program Retailer Sales Trend (Last Year)");
                        List<TradeProgramRetailerSalesTrendCyBEL> item;
                        item = (from DataRow row in dt.Rows
                                select new TradeProgramRetailerSalesTrendCyBEL
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


                                    TradeProgramNo = row["TRADE_PROGRAM_NO"].ToString(),
                                    ProgramName = row["PROGRAM_NAME"].ToString(),
                                    EffectType = row["EFFECT_TYPE"].ToString(),
                                    TradePolicyNo = row["TRADE_POLICY_NO"].ToString(),


                                    SlabTargetVal = Convert.ToDouble(row["SLAB_TARGET_VAL"]),
                                    SlabUpperAmt = Convert.ToDouble(row["SLAB_UPPER_AMT"]),

                                    NoOfInv = Convert.ToInt32(row["NO_OF_INV"]),
                                    Gift = row["GIFT"].ToString(),

                                    DiscountAmt = Convert.ToDouble(row["DISCOUNT_AMT"]),
                                    DiscountPercentage = Convert.ToInt32(row["DISCOUNT_PERCENTAGE"]),

                                    EntryDate = row["ENTRY_DATE"].ToString(),
                                    ProgramTypeCode = row["PROGRAM_TYPE_CODE"].ToString(),
                                    ProgramType = row["PROGRAM_TYPE"].ToString(),
                                    EffectFromDate = row["EFFECT_FROM_DATE"].ToString(),
                                    EffectToDate = row["EFFECT_TO_DATE"].ToString(),

                                    Sales = Convert.ToDouble(row["SALES_VALUE"]),
                                    Return = Convert.ToDouble(row["RETURN_VALUE"]),
                                    Ims = Convert.ToDouble(row["IMS_VALUE"]),

                                    JanIms = Convert.ToDouble(row["JAN_IMS_VALUE"]),
                                    FebIms = Convert.ToDouble(row["FEB_IMS_VALUE"]),
                                    MarIms = Convert.ToDouble(row["MAR_IMS_VALUE"]),
                                    AprIms = Convert.ToDouble(row["APR_IMS_VALUE"]),
                                    MayIms = Convert.ToDouble(row["MAY_IMS_VALUE"]),
                                    JunIms = Convert.ToDouble(row["JUN_IMS_VALUE"]),
                                    JulIms = Convert.ToDouble(row["JUL_IMS_VALUE"]),
                                    AugIms = Convert.ToDouble(row["AUG_IMS_VALUE"]),
                                    SepIms = Convert.ToDouble(row["SEP_IMS_VALUE"]),
                                    OctIms = Convert.ToDouble(row["OCT_IMS_VALUE"]),
                                    NovIms = Convert.ToDouble(row["NOV_IMS_VALUE"]),
                                    DecIms = Convert.ToDouble(row["DEC_IMS_VALUE"])

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