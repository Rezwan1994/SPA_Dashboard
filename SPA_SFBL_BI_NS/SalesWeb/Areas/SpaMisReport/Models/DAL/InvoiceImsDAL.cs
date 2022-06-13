using System;
using System.Data;
using System.Linq;
using SalesWeb.Areas.SpaMisReport.Models.BEL;
using SalesWeb.Universal.Gateway;
using System.Collections.Generic;
using System.Data.OracleClient;
using System.Web;

namespace SalesWeb.Areas.SpaMisReport.Models.DAL
{
    public class InvoiceImsDAL : ReturnData
    {

        private readonly DBHelper _dbHelper = new DBHelper();
        private readonly ErrorLogger _errorLogger = new ErrorLogger();
        private static readonly DBConnection DBConnection = new DBConnection();
        public readonly string ConnString = DBConnection.SAConnStrReader("Oracle", "PHARMAERP");


        public object GetInvoiceImsDateRange(string fDate, string tDate, string dCode, string rCode, string aCode, string tCode, string cCode)
        {


            try
            {
                using (OracleConnection objConn = new OracleConnection(ConnString))
                {
                    using (OracleCommand objCmd = new OracleCommand())
                    {
                        objCmd.Connection = objConn;
                        objCmd.CommandText = "fn_Invoice_Ims_Date_Range";
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
                        _dbHelper.InsertReportAudit("Invoice Wise IMS(Custom Date)");
                        List<InvoiceImsBEL> item;
                        item = (from DataRow row in dt.Rows
                                select new InvoiceImsBEL
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


                                    FsoCode = row["FSO_CODE"].ToString(),
                                    FsoName = row["FSO_NAME"].ToString(),


                                    MarketCode = row["MARKET_CODE"].ToString(),
                                    MarketName = row["MARKET_NAME"].ToString(),

                                    RouteCode = row["ROUTE_CODE"].ToString(),
                                    RouteName = row["ROUTE_NAME"].ToString(),
                                    RetailerCode = row["RETAILER_CODE"].ToString(),
                                    RetailerName = row["RETAILER_NAME"].ToString(),

                                    InvoiceNo = row["INVOICE_NO"].ToString(),
                                    InvoiceDate = row["INVOICE_DATE"].ToString(),
                                    ReturnDate = row["RETURN_DATE"].ToString(),

                                    InvoiceAmount = Convert.ToDouble(row["INVOICE_AMT"]),
                                    SlabAdjustmentAmt = Convert.ToDouble(row["SLAB_ADJUSTMENT_AMT"]),
                                    NetInvoiceAmt = Convert.ToDouble(row["NET_INVOINCE_AMT"]),
                                    ReturnAmt = Convert.ToDouble(row["RETURN_AMT"]),
                                    ReturnSlabAdjustmentAmt = Convert.ToDouble(row["RETURN_SLAB_ADJUSTMENT_AMT"]),
                                    NetReturnAmt = Convert.ToDouble(row["NET_RETURN_AMT"]),
                                    NetIms = Convert.ToDouble(row["IMS_AMT"])

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