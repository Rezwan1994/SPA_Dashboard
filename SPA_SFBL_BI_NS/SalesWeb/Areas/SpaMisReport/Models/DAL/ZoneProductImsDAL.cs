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
    public class ZoneProductImsDAL : ReturnData
    {
        private readonly DBHelper _dbHelper = new DBHelper();
        private readonly ErrorLogger _errorLogger = new ErrorLogger();
        private static readonly DBConnection DBConnection = new DBConnection();
        public readonly string ConnString = DBConnection.SAConnStrReader("Oracle", "PHARMAERP");

        //Function
        public object GetZoneProductImsDateRange(string fDate, string tDate, string dCode)
        {
            try
            {

                //int userId = Convert.ToInt32(HttpContext.Current.Session["USER_ID"]);

                using (OracleConnection objConn = new OracleConnection(ConnString))
                {
                    using (OracleCommand objCmd = new OracleCommand())
                    {
                        objCmd.Connection = objConn;
                        objCmd.CommandText = "FN_DIVISION_PROD_IMS";
                        objCmd.CommandType = CommandType.StoredProcedure;
                        objCmd.Parameters.Add("pfrom_date", OracleType.VarChar).Value = fDate;
                        objCmd.Parameters.Add("pto_date", OracleType.VarChar).Value = tDate;
                        objCmd.Parameters.Add("pdivision_code", OracleType.VarChar).Value = dCode;
                        //objCmd.Parameters.Add("puser_id", OracleType.VarChar).Value = userId;
                        objCmd.Parameters.Add("return_value", OracleType.Cursor).Direction = ParameterDirection.ReturnValue;
                        objConn.Open();
                        objCmd.ExecuteNonQuery();
                        OracleDataReader rdr = objCmd.ExecuteReader();
                        DataTable dt = new DataTable();
                        if (rdr.HasRows)
                        {
                            dt.Load(rdr);
                        }
                        _dbHelper.InsertReportAudit("Zone Wise Product IMS (Custom Date)");
                        List<ZoneProductImsBEL> item;
                        item = (from DataRow row in dt.Rows
                                select new ZoneProductImsBEL
                                {
                                    DivisionCode = row["DIVISION_CODE"].ToString(),
                                    DivisionName = row["DIVISION_NAME"].ToString(),                                    
                                    ProductCode = row["PRODUCT_CODE"].ToString(),
                                    ProductName = row["PRODUCT_NAME"].ToString(),
                                    PackSize = row["PACK_SIZE"].ToString(),
                                    ImsSalesQty = Convert.ToInt32(row["IMS_SALES_QTY"]),
                                    TargetQty = Convert.ToInt32(row["TARGET_QTY"]),
                                    LastYearAsOnDateSalesQty = Convert.ToInt32(row["LAST_YEAR_AS_ON_DATE_IMS_QTY"]),
                                    Achievement = Convert.ToDouble(row["ACHIEVEMENT"]),
                                    Growth = Convert.ToDouble(row["GROWTH"]),


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