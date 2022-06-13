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
    public class InvoiceWiseProdSalesDAL : ReturnData
    {
        private readonly DBHelper _dbHelper = new DBHelper();
        private readonly ErrorLogger _errorLogger = new ErrorLogger();
        private static readonly DBConnection DBConnection = new DBConnection();
        public readonly string ConnString = DBConnection.SAConnStrReader("Oracle", "PHARMAERP");


        //Function
        public object GetInvoiceWiseProdSales(string fDate, string tDate, string pCode, string dCode, string rCode, string aCode, string tCode, string cCode)
        {
            try
            {
                using (OracleConnection objConn = new OracleConnection(ConnString))
                {
                    using (OracleCommand objCmd = new OracleCommand())
                    {
                        objCmd.Connection = objConn;
                        objCmd.CommandText = "FN_INVOICE_WISE_PRODUCT_SALES";
                        objCmd.CommandType = CommandType.StoredProcedure;
                        objCmd.Parameters.Add("p_from_date", OracleType.VarChar).Value = fDate;
                        objCmd.Parameters.Add("p_to_date", OracleType.VarChar).Value = tDate;
                        objCmd.Parameters.Add("p_product_code", OracleType.VarChar).Value = pCode;
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
                        _dbHelper.InsertReportAudit("Invoice Wise Product Sales (Custom Date)");
                        List<InvoiceWiseProdSalesBEL> item;
                        item = (from DataRow row in dt.Rows
                                select new InvoiceWiseProdSalesBEL
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
                                    RetailerCode = row["RETAILER_CODE"].ToString(),
                                    RetailerName = row["RETAILER_NAME"].ToString(),
                                    InvoiceNo = row["INVOICE_NO"].ToString(),
                                    InvoiceDate = row["INVOICE_DATE"].ToString(),
                                    ProductCode = row["PRODUCT_CODE"].ToString(),
                                    ProductName = row["PRODUCT_NAME"].ToString(),
                                    PackSize = row["PACK_SIZE"].ToString(),
                                    SalesQty = row["SALES_QTY"].ToString(),
                                    ReturnQty = row["RETURN_QTY"].ToString(),
                                    NetImsQty = row["NET_IMS_QTY"].ToString(),
                                    SalesValue = Convert.ToDouble(row["SALES_VALUE"]),
                                    ReturnValue = Convert.ToDouble(row["RETURN_VALUE"]),
                                    NetImsValue = Convert.ToDouble(row["NET_IMS_VALUE"])
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