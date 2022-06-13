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
    public class InvoiceWiseTradeProgCalDAL : ReturnData
    {
        private readonly DBHelper _dbHelper = new DBHelper();
        private readonly ErrorLogger _errorLogger = new ErrorLogger();
        private static readonly DBConnection DBConnection = new DBConnection();
        public readonly string ConnString = DBConnection.SAConnStrReader("Oracle", "PHARMAERP");

        public object GetTradeProgramList()
        {
            try
            {
                var qry = " SELECT TRADE_NAME TRADE_PROGRAM_NAME," +
                          " TRADE_PROG_PROD_MST_SLNO TRADE_PROGRAM_NO," +
                          " DECODE(TRADE_PROGRAM_STATUS,'A','Active','I','Inactive') STATUS," +
                          " TO_CHAR(TRADE_EFFECT_FROM_DATE,'DD/Mon/RR') ||' - '|| TO_CHAR(TRADE_EFFECT_TO_DATE,'DD/Mon/RR') DURATION," +
                          " EFFECT_TYPE" +
                          " FROM MV_TRADE_PROG_PROD_MST " +
                          " ORDER BY TRADE_PROG_PROD_MST_SLNO DESC";
                DataTable dt = _dbHelper.GetDataTable(qry);
                var item = (from DataRow row in dt.Rows
                            select new TradeProgramBEL
                            {
                                TradeProgramNo = row["TRADE_PROGRAM_NO"].ToString(),
                                TradeProgramName = row["TRADE_PROGRAM_NAME"].ToString(),
                                TradeProgramStatus = row["STATUS"].ToString(),
                                ProgramDuration = row["DURATION"].ToString(),
                                EffectType = row["EFFECT_TYPE"].ToString()
                            }).ToList();
                return item;

            }
            catch (Exception e)
            {
                var lineNum = e.StackTrace.Substring(e.StackTrace.LastIndexOf(' '));
                _errorLogger.GetErrorMessage(e.Message, "DistWiseSrSalesDAL", lineNum);
                ExceptionReturn = e.Message;
                return "Not Ok";
            }
        }


        public object GetInvoiceWiseTradeProgramCalculation(string fDate, string tDate, string dCode, string rCode, string aCode, string tCode, string cCode, string tProgramNo)
        {
            try
            {
                using (OracleConnection objConn = new OracleConnection(ConnString))
                {
                    using (OracleCommand objCmd = new OracleCommand())
                    {
                        objCmd.Connection = objConn;
                        objCmd.CommandText = "FN_INVOICE_WISE_TRADE_PROG_CAL";
                        objCmd.CommandType = CommandType.StoredProcedure;
                        objCmd.Parameters.Add("pfrom_date", OracleType.VarChar).Value = fDate;
                        objCmd.Parameters.Add("pto_date", OracleType.VarChar).Value = tDate;
                        objCmd.Parameters.Add("pdivision_code", OracleType.VarChar).Value = dCode;
                        objCmd.Parameters.Add("pregion_code", OracleType.VarChar).Value = rCode;
                        objCmd.Parameters.Add("parea_code", OracleType.VarChar).Value = aCode;
                        objCmd.Parameters.Add("pterritory_code", OracleType.VarChar).Value = tCode;
                        objCmd.Parameters.Add("pcustomer_code", OracleType.VarChar).Value = cCode;
                        objCmd.Parameters.Add("ppolicy_no", OracleType.VarChar).Value = tProgramNo;

                        objCmd.Parameters.Add("return_value", OracleType.Cursor).Direction = ParameterDirection.ReturnValue;

                        objConn.Open();
                        objCmd.ExecuteNonQuery();
                        OracleDataReader rdr = objCmd.ExecuteReader();
                        DataTable dt = new DataTable();
                        if (rdr.HasRows)
                        {
                            dt.Load(rdr);
                        }
                        _dbHelper.InsertReportAudit("Invoice Wise Trade Calculation");
                        List<InvoiceWiseTradeProgCalBEL> item;
                        item = (from DataRow row in dt.Rows
                                select new InvoiceWiseTradeProgCalBEL
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
                                    RetailerCode = row["RETAILER_CODE"].ToString(),
                                    RetailerName = row["RETAILER_NAME"].ToString(),
                                    InvoiceNo = row["INVOICE_NO"].ToString(),
                                    InvoiceDate = row["INVOICE_DATE"].ToString(),
                                    ProgramNo = row["PROGRAM_NO"].ToString(),
                                    ProgramName = row["PROGRAM_NAME"].ToString(),
                                    SlabNo = row["SLAB_NO"].ToString(),
                                    SlabAmount = Convert.ToDouble(row["SLAB_AMOUNT"]),
                                    DiscPct = row["DISC_PCT"].ToString(),
                                    GiftItemQty= Convert.ToDouble(row["ITEM_QTY"]),
                                    SalesValue = Convert.ToDouble(row["SALE_VALUE"]),
                                    ReturnValue = Convert.ToDouble(row["RETURN_VALUE"]),
                                    NetIms = Convert.ToDouble(row["NET_IMS"]),
                                    ReturnSlabAmount = Convert.ToDouble(row["RETURN_SLAB_AMOUNT"]),
                                    DiscountValue = Convert.ToDouble(row["DISCOUNT_VAL"]),
                                    ActualDiscunt = Convert.ToDouble(row["ACTUAL_DISCOUNT"])

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