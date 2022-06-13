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
    public class CustomerWiseTradeProgCalDAL : ReturnData
    {
        private readonly DBHelper _dbHelper = new DBHelper();
        private readonly ErrorLogger _errorLogger = new ErrorLogger();
        private static readonly DBConnection DBConnection = new DBConnection();
        public readonly string ConnString = DBConnection.SAConnStrReader("Oracle", "PHARMAERP");


        public object GetCustomerWiseTradeProgramCalculation(string fDate, string tDate, string dCode, string rCode, string aCode, string tCode, string cCode, string tProgramNo)
        {
            try
            {
                using (OracleConnection objConn = new OracleConnection(ConnString))
                {
                    using (OracleCommand objCmd = new OracleCommand())
                    {
                        objCmd.Connection = objConn;
                        objCmd.CommandText = "FN_CUST_WISE_TRADE_PROG_CAL";
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
                        _dbHelper.InsertReportAudit("Customer Wise Trade Program Calculation");
                        List<CustomerWiseTradeProgCalBEL> item;
                        item = (from DataRow row in dt.Rows
                                select new CustomerWiseTradeProgCalBEL
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
                                    ProgramNo = row["PROGRAM_NO"].ToString(),
                                    ProgramName = row["PROGRAM_NAME"].ToString(),
                                    SalesValue = Convert.ToDouble(row["SALES_VALUE"]),
                                    ReturnValue = Convert.ToDouble(row["RETURN_VALUE"]),
                                    NetIms = Convert.ToDouble(row["NET_IMS"]),
                                    ReturnSlabAmount = Convert.ToDouble(row["RETURN_SLAB_AMOUNT"]),
                                    DiscountValue = Convert.ToDouble(row["DISCOUNT_VAL"]),
                                    ActualDiscunt = Convert.ToDouble(row["ACTUAL_DISCOUNT_VAL"])

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