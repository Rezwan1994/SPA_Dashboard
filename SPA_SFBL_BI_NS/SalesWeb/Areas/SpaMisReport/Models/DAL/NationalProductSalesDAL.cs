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
    public class NationalProductSalesDAL : ReturnData
    {
        private readonly DBHelper _dbHelper = new DBHelper();
        private readonly ErrorLogger _errorLogger = new ErrorLogger();
        private static readonly DBConnection DBConnection = new DBConnection();
        public readonly string ConnString = DBConnection.SAConnStrReader("Oracle", "PHARMAERP");


        //Query
        public List<NationalProductSalesBEL> GetNtlProductSalesCurMonth()

        {
            try
            {

                string qry;

                int userId = Convert.ToInt32(HttpContext.Current.Session["USER_ID"]);
                int UserAccessCount = Convert.ToInt32(HttpContext.Current.Session["USER_BASE_REPORT_FILTER"]);

                if (UserAccessCount > 0)
                {

                    qry = " SELECT BASE_PRODUCT_CODE, BASE_PRODUCT_NAME, PRODUCT_CATEGORY_CODE, PRODUCT_CATEGORY_NAME, BRAND_CODE, BRAND_NAME, PRODUCT_CODE, PRODUCT_NAME, PACK_SIZE, CURRENT_MONTH_SALES_VAL," +
                          " CURRENT_MONTH_RETURN_VAL, CURRENT_MONTH_NET_SALES_VAL, CURRENT_MONTH_SALES_QTY," +
                          " CURRENT_MONTH_RETURN_QTY, CURRENT_MONTH_NET_SALES_QTY, LAST_MONTH_SALES_VAL," +
                          " LAST_MONTH_RETURN_VAL, LAST_MONTH_NET_SALES_VAL, LAST_MONTH_SALES_QTY," +
                          " LAST_MONTH_RETURN_QTY, LAST_MONTH_NET_SALES_QTY, CURRENT_MONTH_TARGET_QTY," +
                          " CURRENT_MONTH_TARGET_VAL, CURRENT_MONTH_ACH, CURRENT_MONTH_GROWTH, CURRENT_YEAR_IMS_VAL," +
                          " LAST_YEAR_IMS_VAL " +
                          " FROM MV_NATIONAL_PRODUCT_SALES" +
                          " WHERE PRODUCT_CODE IN(SELECT PRODUCT_CODE FROM USER_PRODUCT_DTL WHERE USER_ID = " + userId + ")" +
                          " ORDER BY PRODUCT_NAME";


                }
                else
                {

                    qry = " SELECT BASE_PRODUCT_CODE, BASE_PRODUCT_NAME, PRODUCT_CATEGORY_CODE, PRODUCT_CATEGORY_NAME, BRAND_CODE, BRAND_NAME,PRODUCT_CODE, PRODUCT_NAME, PACK_SIZE, CURRENT_MONTH_SALES_VAL," +
                          " CURRENT_MONTH_RETURN_VAL, CURRENT_MONTH_NET_SALES_VAL, CURRENT_MONTH_SALES_QTY," +
                          " CURRENT_MONTH_RETURN_QTY, CURRENT_MONTH_NET_SALES_QTY, LAST_MONTH_SALES_VAL," +
                          " LAST_MONTH_RETURN_VAL, LAST_MONTH_NET_SALES_VAL, LAST_MONTH_SALES_QTY," +
                          " LAST_MONTH_RETURN_QTY, LAST_MONTH_NET_SALES_QTY, CURRENT_MONTH_TARGET_QTY," +
                          " CURRENT_MONTH_TARGET_VAL, CURRENT_MONTH_ACH, CURRENT_MONTH_GROWTH, CURRENT_YEAR_IMS_VAL," +
                          " LAST_YEAR_IMS_VAL " +
                          " FROM MV_NATIONAL_PRODUCT_SALES " +
                          " ORDER BY PRODUCT_NAME";

                }

                DataTable dt = _dbHelper.GetDataTable(qry);
                var item = (from DataRow row in dt.Rows
                            select new NationalProductSalesBEL
                            {
                                BaseProductCode = row["BASE_PRODUCT_CODE"].ToString(),
                                BaseProductName = row["BASE_PRODUCT_NAME"].ToString(),

                                CategoryCode = row["PRODUCT_CATEGORY_CODE"].ToString(),
                                CategoryName = row["PRODUCT_CATEGORY_NAME"].ToString(),

                                BrandCode = row["BRAND_CODE"].ToString(),
                                BrandName = row["BRAND_NAME"].ToString(),

                                ProductCode = row["PRODUCT_CODE"].ToString(),
                                ProductName = row["PRODUCT_NAME"].ToString(),
                                PackSize = row["PACK_SIZE"].ToString(),

                                CurrentMonthSalesVal = Convert.ToDouble(row["CURRENT_MONTH_SALES_VAL"]),
                                CurrentMonthReturnVal = Convert.ToDouble(row["CURRENT_MONTH_RETURN_VAL"]),
                                CurrentMonthNetSalesVal = Convert.ToDouble(row["CURRENT_MONTH_NET_SALES_VAL"]),
                                CurrentMonthSalesQty = row["CURRENT_MONTH_SALES_QTY"].ToString(),
                                CurrentMonthReturnQty = row["CURRENT_MONTH_RETURN_QTY"].ToString(),
                                CurrentMonthNetSalesQty = row["CURRENT_MONTH_NET_SALES_QTY"].ToString(),
                                LastMonthSalesVal = Convert.ToDouble(row["LAST_MONTH_SALES_VAL"]),
                                LastMonthReturnVal = Convert.ToDouble(row["LAST_MONTH_RETURN_VAL"]),
                                LastMonthNetSalesVal = Convert.ToDouble(row["LAST_MONTH_NET_SALES_VAL"]),
                                LastMonthSalesQty = row["LAST_MONTH_SALES_QTY"].ToString(),
                                LastMonthReturnQty = row["LAST_MONTH_RETURN_QTY"].ToString(),
                                LastMonthNetSalesQty = row["LAST_MONTH_NET_SALES_QTY"].ToString(),
                                CurrentMonthTargetQty = row["CURRENT_MONTH_TARGET_QTY"].ToString(),
                                CurrentMonthTargetVal = Convert.ToDouble(row["CURRENT_MONTH_TARGET_VAL"]),
                                CurrentMonthAch = row["CURRENT_MONTH_ACH"].ToString(),
                                CurrentMonthGrowth = row["CURRENT_MONTH_GROWTH"].ToString(),
                                CurrentYearImsVal = Convert.ToDouble(row["CURRENT_YEAR_IMS_VAL"]),
                                LastYearImsVal = Convert.ToDouble(row["LAST_YEAR_IMS_VAL"])
                            }).ToList();
                return item;
            }
            catch (Exception e)
            {
                var lineNum = e.StackTrace.Substring(e.StackTrace.LastIndexOf(' '));
                _errorLogger.GetErrorMessage(e.Message, "NationalProductSalesDAL", lineNum);
                ExceptionReturn = e.Message;
                throw;
            }
        }


        //Function
        public object GetNtlProductSalesDateRange(string fDate, string tDate)
        {
            try
            {
                int userId = Convert.ToInt32(HttpContext.Current.Session["USER_ID"]);

                using (OracleConnection objConn = new OracleConnection(ConnString))
                {
                    using (OracleCommand objCmd = new OracleCommand())
                    {
                        objCmd.Connection = objConn;
                        objCmd.CommandText = "FN_NTL_PROD_SAL_DATE_RANGE";
                        objCmd.CommandType = CommandType.StoredProcedure;
                        objCmd.Parameters.Add("pfrom_date", OracleType.VarChar).Value = fDate;
                        objCmd.Parameters.Add("pto_date", OracleType.VarChar).Value = tDate;
                        objCmd.Parameters.Add("puser_id", OracleType.VarChar).Value = userId;
                        objCmd.Parameters.Add("return_value", OracleType.Cursor).Direction = ParameterDirection.ReturnValue;
                        objConn.Open();
                        objCmd.ExecuteNonQuery();
                        OracleDataReader rdr = objCmd.ExecuteReader();
                        DataTable dt = new DataTable();
                        if (rdr.HasRows)
                        {
                            dt.Load(rdr);
                        }
                        _dbHelper.InsertReportAudit("National Product Sales (Custom Date)");
                        var dataList = (from DataRow row in dt.Rows
                                    select new NationalProductSalesBEL
                                    {

                                        BaseProductCode = row["BASE_PRODUCT_CODE"].ToString(),
                                        BaseProductName = row["BASE_PRODUCT_NAME"].ToString(),

                                        CategoryCode = row["PRODUCT_CATEGORY_CODE"].ToString(),
                                        CategoryName = row["PRODUCT_CATEGORY_NAME"].ToString(),

                                        BrandCode = row["BRAND_CODE"].ToString(),
                                        BrandName = row["BRAND_NAME"].ToString(),

                                        ProductCode = row["PRODUCT_CODE"].ToString(),
                                        ProductName = row["PRODUCT_NAME"].ToString(),
                                        PackSize = row["PACK_SIZE"].ToString(),

                                        CurrentMonthSalesVal = Convert.ToDouble(row["CURRENT_MONTH_SALES_VAL"]),
                                        CurrentMonthReturnVal = Convert.ToDouble(row["CURRENT_MONTH_RETURN_VAL"]),
                                        CurrentMonthNetSalesVal = Convert.ToDouble(row["CURRENT_MONTH_NET_SALES_VAL"]),
                                        CurrentMonthSalesQty = row["CURRENT_MONTH_SALES_QTY"].ToString(),
                                        CurrentMonthReturnQty = row["CURRENT_MONTH_RETURN_QTY"].ToString(),
                                        CurrentMonthNetSalesQty = row["CURRENT_MONTH_NET_SALES_QTY"].ToString(),
                                        LastMonthSalesVal = Convert.ToDouble(row["LAST_MONTH_SALES_VAL"]),
                                        LastMonthReturnVal = Convert.ToDouble(row["LAST_MONTH_RETURN_VAL"]),
                                        LastMonthNetSalesVal = Convert.ToDouble(row["LAST_MONTH_NET_SALES_VAL"]),
                                        LastMonthSalesQty = row["LAST_MONTH_SALES_QTY"].ToString(),
                                        LastMonthReturnQty = row["LAST_MONTH_RETURN_QTY"].ToString(),
                                        LastMonthNetSalesQty = row["LAST_MONTH_NET_SALES_QTY"].ToString(),
                                        CurrentMonthTargetQty = row["CURRENT_MONTH_TARGET_QTY"].ToString(),
                                        CurrentMonthTargetVal = Convert.ToDouble(row["CURRENT_MONTH_TARGET_VAL"]),
                                        CurrentMonthAch = row["CURRENT_MONTH_ACH"].ToString(),
                                        CurrentMonthGrowth = row["CURRENT_MONTH_GROWTH"].ToString(),
                                        CurrentYearImsVal = Convert.ToDouble(row["CURRENT_YEAR_IMS_VAL"]),
                                        LastYearImsVal = Convert.ToDouble(row["LAST_YEAR_IMS_VAL"])

                                    }).ToList();
                        return dataList;
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