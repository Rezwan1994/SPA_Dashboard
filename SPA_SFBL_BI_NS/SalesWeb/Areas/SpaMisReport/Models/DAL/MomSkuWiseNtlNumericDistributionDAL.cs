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
    public class MomSkuWiseNtlNumericDistributionDAL : ReturnData
    {
        private readonly DBHelper _dbHelper = new DBHelper();
        private readonly ErrorLogger _errorLogger = new ErrorLogger();
        private static readonly DBConnection DBConnection = new DBConnection();
        public readonly string ConnString = DBConnection.SAConnStrReader("Oracle", "PHARMAERP");


        public List<MomSkuWiseNtlNumericDistributionBEL> GetMomSkuWiseNtlNumericDistribution(string pCode)
        {
            try
            {

                string qry;

                int userId = Convert.ToInt32(HttpContext.Current.Session["USER_ID"]);
                int UserAccessCount = Convert.ToInt32(HttpContext.Current.Session["USER_BASE_REPORT_FILTER"]);

                if (UserAccessCount > 0)
                {

                    qry = " SELECT PRODUCT_CODE, PRODUCT_NAME, PACK_SIZE, JAN, FEB, MAR, APR, MAY, JUN, JUL, AUG, SEP, OCT, NOV, DEC, TOTAL" +
                          " FROM MV_MOM_NTL_SKU_NUMERIC_DIST_CY " +
                          " WHERE (PRODUCT_CODE='" + pCode + "' OR '" + pCode + "'='ALL')" +
                          " AND   PRODUCT_CODE IN (SELECT PRODUCT_CODE FROM USER_PRODUCT_DTL WHERE USER_ID=" + userId + ")" +
                          " ORDER BY PRODUCT_CODE";

                }
                else
                {
                    qry = " SELECT PRODUCT_CODE, PRODUCT_NAME, PACK_SIZE, JAN, FEB, MAR, APR, MAY, JUN, JUL, AUG, SEP, OCT, NOV, DEC, TOTAL" +
                          " FROM MV_MOM_NTL_SKU_NUMERIC_DIST_CY " +
                          " WHERE (PRODUCT_CODE='" + pCode + "' OR '" + pCode + "'='ALL')" +                          
                          " ORDER BY PRODUCT_CODE";

                }

                //DataTable dt = _dbHelper.GetDataTable(qry);
                DataTable dt = _dbHelper.GetDataTableWithAuditTrial(qry, "SKU Wise National Numeric Distribution (Current Year - MOM)");

                var item = (from DataRow row in dt.Rows
                            select new MomSkuWiseNtlNumericDistributionBEL
                            {
                                ProductCode = row["PRODUCT_CODE"].ToString(),
                                ProductName = row["PRODUCT_NAME"].ToString(),
                                PackSize = row["PACK_SIZE"].ToString(),

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




        public List<MomSkuWiseNtlNumericDistributionBEL> GetMomSkuWiseNtlNumericDistributionLy(string pCode)
        {
            try
            {

                string qry;

                int userId = Convert.ToInt32(HttpContext.Current.Session["USER_ID"]);
                int UserAccessCount = Convert.ToInt32(HttpContext.Current.Session["USER_BASE_REPORT_FILTER"]);

                if (UserAccessCount > 0)
                {

                    qry = " SELECT PRODUCT_CODE, PRODUCT_NAME, PACK_SIZE, JAN, FEB, MAR, APR, MAY, JUN, JUL, AUG, SEP, OCT, NOV, DEC, TOTAL" +
                                 " FROM MV_MOM_NTL_SKU_NUMERIC_DIST_LY " +
                                 " WHERE (PRODUCT_CODE='" + pCode + "' OR '" + pCode + "'='ALL')" +
                                 " AND   PRODUCT_CODE IN (SELECT PRODUCT_CODE FROM USER_PRODUCT_DTL WHERE USER_ID=" + userId + ")" +
                                 " ORDER BY PRODUCT_CODE";

                }
                else
                {
                    qry = " SELECT PRODUCT_CODE, PRODUCT_NAME, PACK_SIZE, JAN, FEB, MAR, APR, MAY, JUN, JUL, AUG, SEP, OCT, NOV, DEC, TOTAL" +
                                 " FROM MV_MOM_NTL_SKU_NUMERIC_DIST_LY " +
                                 " WHERE (PRODUCT_CODE='" + pCode + "' OR '" + pCode + "'='ALL')" +
                                 " ORDER BY PRODUCT_CODE";

                }




                //DataTable dt = _dbHelper.GetDataTable(qry);
                DataTable dt = _dbHelper.GetDataTableWithAuditTrial(qry, "SKU Wise National Numeric Distribution (Last Year - MOM)");


                var item = (from DataRow row in dt.Rows
                            select new MomSkuWiseNtlNumericDistributionBEL
                            {
                                ProductCode = row["PRODUCT_CODE"].ToString(),
                                ProductName = row["PRODUCT_NAME"].ToString(),
                                PackSize = row["PACK_SIZE"].ToString(),

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