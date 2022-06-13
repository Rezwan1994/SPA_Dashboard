using System;
using System.Data;
using System.Linq;
using System.Web;
using SalesWeb.Areas.SpaMisReport.Models.BEL;
using SalesWeb.Areas.Security.Models.BEL;
using SalesWeb.Universal.Gateway;
using System.Collections.Generic;
using System.Data.OracleClient;

namespace SalesWeb.Areas.SpaMisReport.Models.DAL
{
    public class MomProdWiseNtlNuDistDAL : ReturnData
    {
        private readonly DBHelper _dbHelper = new DBHelper();
        private readonly ErrorLogger _errorLogger = new ErrorLogger();
        private static readonly DBConnection DBConnection = new DBConnection();
        public readonly string ConnString = DBConnection.SAConnStrReader("Oracle", "PHARMAERP");

        public object GetBaseProductList()
        {
            try
            {
             string qry = " select 'ALL' name, 'ALL' code, 1 SL from dual " +
                          " UNION " +
                          " select" +
                          " base_product_name name," +
                          " base_product_code code," +
                          " 2 SL"+
                          " from mv_base_product_info" +
                          " where status='A'" +
                          " order by name";

                DataTable dt = _dbHelper.GetDataTable(qry);
                var item = (from DataRow row in dt.Rows
                            select new UserBEL
                            {
                                Name = row["name"].ToString(),
                                Code = row["code"].ToString()

                            }).ToList();
                return item;

            }
            catch (Exception e)
            {
                var lineNum = e.StackTrace.Substring(e.StackTrace.LastIndexOf(' '));
                _errorLogger.GetErrorMessage(e.Message, "UserBaseReportAccessDAL", lineNum);
                ExceptionReturn = e.Message;
                return "Not Ok";
            }
        }


        //public object GetMomProdWiseNtlNuDist(string BaseProductCode)
        //{
        //    try
        //    {

        //        int userId = Convert.ToInt32(HttpContext.Current.Session["USER_ID"]);

        //        using (OracleConnection objConn = new OracleConnection(ConnString))
        //        {
        //            using (OracleCommand objCmd = new OracleCommand())
        //            {
        //                objCmd.Connection = objConn;
        //                objCmd.CommandText = "FN_MOM_NTL_PROD_NUMERIC_DIST";
        //                objCmd.CommandType = CommandType.StoredProcedure;
        //                objCmd.Parameters.Add("pbase_product_code", OracleType.VarChar).Value = BaseProductCode;
        //                objCmd.Parameters.Add("puser_id", OracleType.VarChar).Value = userId;
        //                objCmd.Parameters.Add("return_value", OracleType.Cursor).Direction = ParameterDirection.ReturnValue;
        //                objConn.Open();
        //                objCmd.ExecuteNonQuery();
        //                OracleDataReader rdr = objCmd.ExecuteReader();
        //                DataTable dt = new DataTable();
        //                if (rdr.HasRows)
        //                {
        //                    dt.Load(rdr);
        //                }

        //                List<MomSkuWiseNtlNumericDistributionBEL> item;
        //                item = (from DataRow row in dt.Rows
        //                        select new MomSkuWiseNtlNumericDistributionBEL
        //                        {
        //                            ProductCode = row["BASE_PRODUCT_CODE"].ToString(),
        //                            ProductName = row["BASE_PRODUCT_NAME"].ToString(),
        //                            Jan = row["JAN"].ToString(),
        //                            Feb = row["FEB"].ToString(),
        //                            Mar = row["MAR"].ToString(),
        //                            Apr = row["APR"].ToString(),
        //                            May = row["MAY"].ToString(),
        //                            Jun = row["JUN"].ToString(),
        //                            Jul = row["JUL"].ToString(),
        //                            Aug = row["AUG"].ToString(),
        //                            Sep = row["SEP"].ToString(),
        //                            Oct = row["OCT"].ToString(),
        //                            Nov = row["NOV"].ToString(),
        //                            Dec = row["DEC"].ToString(),
        //                            Total = row["TOTAL"].ToString()
        //                        }).ToList();
        //                return item;

        //            }
        //        }
        //    }
        //    catch (Exception e)
        //    {
        //        Console.WriteLine(e);
        //        return ExceptionReturn = "";
        //    }

        //}



        public List<MomSkuWiseNtlNumericDistributionBEL> GetMomProdWiseNtlNuDist(string BaseProductCode)
        {
            try
            {

                string qry;

                int userId = Convert.ToInt32(HttpContext.Current.Session["USER_ID"]);
                int UserAccessCount = Convert.ToInt32(HttpContext.Current.Session["USER_BASE_REPORT_FILTER"]);

                if (UserAccessCount > 0)
                {

                    qry = " SELECT BASE_PRODUCT_CODE, BASE_PRODUCT_NAME, JAN, FEB, MAR, APR, MAY, JUN, JUL, AUG, SEP, OCT, NOV, DEC, TOTAL" +
                                 " FROM MV_MOM_NTL_PROD_NU_DIST_CY " +
                                 " WHERE (BASE_PRODUCT_CODE='" + BaseProductCode + "' OR '" + BaseProductCode + "'='ALL')" +
                                 //" AND BASE_PRODUCT_CODE IN (SELECT PRODUCT_TYPE_CODE FROM USER_PRODUCT_TYPE WHERE  TYPE_NAME='BASE_PRODUCT' AND USER_ID=" + userId + ")" +
                                 " ORDER BY BASE_PRODUCT_NAME";

                }
                else
                {
                    qry = " SELECT BASE_PRODUCT_CODE, BASE_PRODUCT_NAME, JAN, FEB, MAR, APR, MAY, JUN, JUL, AUG, SEP, OCT, NOV, DEC, TOTAL" +
                                 " FROM MV_MOM_NTL_PROD_NU_DIST_CY " +
                                 " WHERE (BASE_PRODUCT_CODE='" + BaseProductCode + "' OR '" + BaseProductCode + "'='ALL')" +
                                 " ORDER BY BASE_PRODUCT_NAME";

                }



                //DataTable dt = _dbHelper.GetDataTable(qry);
                DataTable dt = _dbHelper.GetDataTableWithAuditTrial(qry, "Product Wise National Numeric Distribution (Current Year - MOM)");
                var item = (from DataRow row in dt.Rows
                            select new MomSkuWiseNtlNumericDistributionBEL
                            {
                                ProductCode = row["BASE_PRODUCT_CODE"].ToString(),
                                ProductName = row["BASE_PRODUCT_NAME"].ToString(),
                                Jan = row["JAN"].ToString(),
                                Feb = row["FEB"].ToString(),
                                Mar = row["MAR"].ToString(),
                                Apr = row["APR"].ToString(),
                                May = row["MAY"].ToString(),
                                Jun = row["JUN"].ToString(),
                                Jul = row["JUL"].ToString(),
                                Aug = row["AUG"].ToString(),
                                Sep = row["SEP"].ToString(),
                                Oct = row["OCT"].ToString(),
                                Nov = row["NOV"].ToString(),
                                Dec = row["DEC"].ToString(),
                                Total = row["TOTAL"].ToString()
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



        //public object GetMomProdWiseNtlNuDistLy(string BaseProductCode)
        //{
        //    try
        //    {

        //        int userId = Convert.ToInt32(HttpContext.Current.Session["USER_ID"]);

        //        using (OracleConnection objConn = new OracleConnection(ConnString))
        //        {
        //            using (OracleCommand objCmd = new OracleCommand())
        //            {
        //                objCmd.Connection = objConn;
        //                objCmd.CommandText = "FN_MOM_NTL_PROD_NU_DIST_LY";
        //                objCmd.CommandType = CommandType.StoredProcedure;
        //                objCmd.Parameters.Add("pbase_product_code", OracleType.VarChar).Value = BaseProductCode;
        //                objCmd.Parameters.Add("puser_id", OracleType.VarChar).Value = userId;
        //                objCmd.Parameters.Add("return_value", OracleType.Cursor).Direction = ParameterDirection.ReturnValue;
        //                objConn.Open();
        //                objCmd.ExecuteNonQuery();
        //                OracleDataReader rdr = objCmd.ExecuteReader();
        //                DataTable dt = new DataTable();
        //                if (rdr.HasRows)
        //                {
        //                    dt.Load(rdr);
        //                }

        //                List<MomSkuWiseNtlNumericDistributionBEL> item;
        //                item = (from DataRow row in dt.Rows
        //                        select new MomSkuWiseNtlNumericDistributionBEL
        //                        {
        //                            ProductCode = row["BASE_PRODUCT_CODE"].ToString(),
        //                            ProductName = row["BASE_PRODUCT_NAME"].ToString(),
        //                            Jan = row["JAN"].ToString(),
        //                            Feb = row["FEB"].ToString(),
        //                            Mar = row["MAR"].ToString(),
        //                            Apr = row["APR"].ToString(),
        //                            May = row["MAY"].ToString(),
        //                            Jun = row["JUN"].ToString(),
        //                            Jul = row["JUL"].ToString(),
        //                            Aug = row["AUG"].ToString(),
        //                            Sep = row["SEP"].ToString(),
        //                            Oct = row["OCT"].ToString(),
        //                            Nov = row["NOV"].ToString(),
        //                            Dec = row["DEC"].ToString(),
        //                            Total = row["TOTAL"].ToString()
        //                        }).ToList();
        //                return item;

        //            }
        //        }
        //    }
        //    catch (Exception e)
        //    {
        //        Console.WriteLine(e);
        //        return ExceptionReturn = "";
        //    }

        //}

        public List<MomSkuWiseNtlNumericDistributionBEL> GetMomProdWiseNtlNuDistLy(string BaseProductCode)
        {
            try
            {

                string qry;

                int userId = Convert.ToInt32(HttpContext.Current.Session["USER_ID"]);
                int UserAccessCount = Convert.ToInt32(HttpContext.Current.Session["USER_BASE_REPORT_FILTER"]);

                if (UserAccessCount > 0)
                {

                    qry = " SELECT BASE_PRODUCT_CODE, BASE_PRODUCT_NAME, JAN, FEB, MAR, APR, MAY, JUN, JUL, AUG, SEP, OCT, NOV, DEC, TOTAL" +
                                     " FROM MV_MOM_NTL_PROD_NU_DIST_LY " +
                                     " WHERE (BASE_PRODUCT_CODE='" + BaseProductCode + "' OR '" + BaseProductCode + "'='ALL')" +
                                     //" AND BASE_PRODUCT_CODE IN (SELECT PRODUCT_TYPE_CODE FROM USER_PRODUCT_TYPE WHERE  TYPE_NAME='BASE_PRODUCT' AND USER_ID=" + userId + ")" +
                                     " ORDER BY BASE_PRODUCT_NAME";

                }
                else
                {
                    qry = " SELECT BASE_PRODUCT_CODE, BASE_PRODUCT_NAME, JAN, FEB, MAR, APR, MAY, JUN, JUL, AUG, SEP, OCT, NOV, DEC, TOTAL" +
                                     " FROM MV_MOM_NTL_PROD_NU_DIST_LY " +
                                     " WHERE (BASE_PRODUCT_CODE='" + BaseProductCode + "' OR '" + BaseProductCode + "'='ALL')" +
                                     " ORDER BY BASE_PRODUCT_NAME";

                }





                //DataTable dt = _dbHelper.GetDataTable(qry);
                DataTable dt = _dbHelper.GetDataTableWithAuditTrial(qry, "Product Wise National Numeric Distribution (Last Year - MOM)");

                var item = (from DataRow row in dt.Rows
                            select new MomSkuWiseNtlNumericDistributionBEL
                            {
                                ProductCode = row["BASE_PRODUCT_CODE"].ToString(),
                                ProductName = row["BASE_PRODUCT_NAME"].ToString(),
                                Jan = row["JAN"].ToString(),
                                Feb = row["FEB"].ToString(),
                                Mar = row["MAR"].ToString(),
                                Apr = row["APR"].ToString(),
                                May = row["MAY"].ToString(),
                                Jun = row["JUN"].ToString(),
                                Jul = row["JUL"].ToString(),
                                Aug = row["AUG"].ToString(),
                                Sep = row["SEP"].ToString(),
                                Oct = row["OCT"].ToString(),
                                Nov = row["NOV"].ToString(),
                                Dec = row["DEC"].ToString(),
                                Total = row["TOTAL"].ToString()
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