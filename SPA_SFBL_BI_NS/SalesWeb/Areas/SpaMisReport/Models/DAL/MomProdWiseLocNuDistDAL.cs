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
    public class MomProdWiseLocNuDistDAL : ReturnData
    {
        private readonly DBHelper _dbHelper = new DBHelper();
        private readonly ErrorLogger _errorLogger = new ErrorLogger();
        private static readonly DBConnection DBConnection = new DBConnection();
        public readonly string ConnString = DBConnection.SAConnStrReader("Oracle", "PHARMAERP");


        //public object GetMomProdWiseLocNuDist(string dCode, string rCode, string aCode, string tCode, string cCode, string pCode)
        //{
        //    try
        //    {

        //        int userId = Convert.ToInt32(HttpContext.Current.Session["USER_ID"]);

        //        using (OracleConnection objConn = new OracleConnection(ConnString))
        //        {
        //            using (OracleCommand objCmd = new OracleCommand())
        //            {
        //                objCmd.Connection = objConn;
        //                objCmd.CommandText = "FN_MOM_LOC_PROD_NUMERIC_DIST";
        //                objCmd.CommandType = CommandType.StoredProcedure;
        //                objCmd.Parameters.Add("pdivision_code", OracleType.VarChar).Value = dCode;
        //                objCmd.Parameters.Add("pregion_code", OracleType.VarChar).Value = rCode;
        //                objCmd.Parameters.Add("parea_code", OracleType.VarChar).Value = aCode;
        //                objCmd.Parameters.Add("pterritory_code", OracleType.VarChar).Value = tCode;
        //                objCmd.Parameters.Add("pcustomer_code", OracleType.VarChar).Value = cCode;
        //                objCmd.Parameters.Add("pbase_product_code", OracleType.VarChar).Value = pCode;
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

        //                List<MomSkuWiseNumericDistributionBEL> item;
        //                item = (from DataRow row in dt.Rows
        //                        select new MomSkuWiseNumericDistributionBEL
        //                        {
        //                            DivisionCode = row["DIVISION_CODE"].ToString(),
        //                            DivisionName = row["DIVISION_NAME"].ToString(),
        //                            RegionCode = row["REGION_CODE"].ToString(),
        //                            RegionName = row["REGION_NAME"].ToString(),
        //                            AreaCode = row["AREA_CODE"].ToString(),
        //                            AreaName = row["AREA_NAME"].ToString(),
        //                            TerritoryCode = row["TERRITORY_CODE"].ToString(),
        //                            TerritoryName = row["TERRITORY_NAME"].ToString(),

        //                            CustomerCode = row["CUSTOMER_CODE"].ToString(),
        //                            CustomerName = row["CUSTOMER_NAME"].ToString(),
        //                            DbLocation = row["DB_LOCATION"].ToString(),

        //                            MarketCode = row["MARKET_CODE"].ToString(),
        //                            MarketName = row["MARKET_NAME"].ToString(),

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



        public List<MomSkuWiseNumericDistributionBEL> GetMomProdWiseLocNuDist(string dCode, string rCode, string aCode, string tCode, string cCode, string pCode)
        {
            try
            {

                string qry;

                int userId = Convert.ToInt32(HttpContext.Current.Session["USER_ID"]);
                int UserAccessCount = Convert.ToInt32(HttpContext.Current.Session["USER_BASE_REPORT_FILTER"]);

                if (UserAccessCount > 0)
                {

                    qry = " SELECT DIVISION_CODE, DIVISION_NAME, REGION_CODE, REGION_NAME, AREA_CODE, AREA_NAME, TERRITORY_CODE, TERRITORY_NAME, DB_LOCATION, CUSTOMER_CODE, CUSTOMER_NAME, MARKET_CODE, MARKET_NAME, BASE_PRODUCT_CODE, BASE_PRODUCT_NAME, JAN, FEB, MAR, APR, MAY, JUN, JUL, AUG, SEP, OCT, NOV, DEC, TOTAL " +
                          " FROM MV_MOM_LOC_PROD_NM_DIST_CY " +
                          " WHERE (DIVISION_CODE = '" + dCode + "' OR '" + dCode + "' = 'ALL')" +
                          " AND   (REGION_CODE='" + rCode + "' OR '" + rCode + "'='ALL')" +
                          " AND   (AREA_CODE='" + aCode + "' OR '" + aCode + "'='ALL')" +
                          " AND   (TERRITORY_CODE='" + tCode + "' OR '" + tCode + "'='ALL')" +
                          " AND   (CUSTOMER_CODE='" + cCode + "' OR '" + cCode + "'='ALL')" +
                          " AND   (BASE_PRODUCT_CODE='" + pCode + "' OR '" + pCode + "'='ALL')" +
                          //" AND   BASE_PRODUCT_CODE IN (SELECT PRODUCT_TYPE_CODE FROM USER_PRODUCT_TYPE WHERE TYPE_NAME='BASE_PRODUCT' AND USER_ID=" + userId + ")" +
                          " ORDER BY DIVISION_NAME,REGION_NAME, AREA_NAME, TERRITORY_NAME, CUSTOMER_NAME, MARKET_NAME, BASE_PRODUCT_NAME";

                }
                else
                {
                    qry = " SELECT DIVISION_CODE, DIVISION_NAME, REGION_CODE, REGION_NAME, AREA_CODE, AREA_NAME, TERRITORY_CODE, TERRITORY_NAME, DB_LOCATION, CUSTOMER_CODE, CUSTOMER_NAME, MARKET_CODE, MARKET_NAME, BASE_PRODUCT_CODE, BASE_PRODUCT_NAME, JAN, FEB, MAR, APR, MAY, JUN, JUL, AUG, SEP, OCT, NOV, DEC, TOTAL " +
                          " FROM MV_MOM_LOC_PROD_NM_DIST_CY " +
                          " WHERE (DIVISION_CODE = '" + dCode + "' OR '" + dCode + "' = 'ALL')" +
                          " AND   (REGION_CODE='" + rCode + "' OR '" + rCode + "'='ALL')" +
                          " AND   (AREA_CODE='" + aCode + "' OR '" + aCode + "'='ALL')" +
                          " AND   (TERRITORY_CODE='" + tCode + "' OR '" + tCode + "'='ALL')" +
                          " AND   (CUSTOMER_CODE='" + cCode + "' OR '" + cCode + "'='ALL')" +
                          " AND   (BASE_PRODUCT_CODE='" + pCode + "' OR '" + pCode + "'='ALL')" +
                          " ORDER BY DIVISION_NAME,REGION_NAME, AREA_NAME, TERRITORY_NAME, CUSTOMER_NAME, MARKET_NAME, BASE_PRODUCT_NAME";

                }

                //DataTable dt = _dbHelper.GetDataTable(qry);
                DataTable dt = _dbHelper.GetDataTableWithAuditTrial(qry, "Product Wise Numeric Distribution (Current Year - MOM)");
                var item = (from DataRow row in dt.Rows
                            select new MomSkuWiseNumericDistributionBEL
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



        //public object GetMomProdWiseLocNuDistLy(string dCode, string rCode, string aCode, string tCode, string cCode, string pCode)
        //{
        //    try
        //    {

        //        int userId = Convert.ToInt32(HttpContext.Current.Session["USER_ID"]);

        //        using (OracleConnection objConn = new OracleConnection(ConnString))
        //        {
        //            using (OracleCommand objCmd = new OracleCommand())
        //            {
        //                objCmd.Connection = objConn;
        //                objCmd.CommandText = "FN_MOM_LOC_PROD_NU_DIST_LY";
        //                objCmd.CommandType = CommandType.StoredProcedure;
        //                objCmd.Parameters.Add("pdivision_code", OracleType.VarChar).Value = dCode;
        //                objCmd.Parameters.Add("pregion_code", OracleType.VarChar).Value = rCode;
        //                objCmd.Parameters.Add("parea_code", OracleType.VarChar).Value = aCode;
        //                objCmd.Parameters.Add("pterritory_code", OracleType.VarChar).Value = tCode;
        //                objCmd.Parameters.Add("pcustomer_code", OracleType.VarChar).Value = cCode;
        //                objCmd.Parameters.Add("pbase_product_code", OracleType.VarChar).Value = pCode;
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

        //                List<MomSkuWiseNumericDistributionBEL> item;
        //                item = (from DataRow row in dt.Rows
        //                        select new MomSkuWiseNumericDistributionBEL
        //                        {
        //                            DivisionCode = row["DIVISION_CODE"].ToString(),
        //                            DivisionName = row["DIVISION_NAME"].ToString(),
        //                            RegionCode = row["REGION_CODE"].ToString(),
        //                            RegionName = row["REGION_NAME"].ToString(),
        //                            AreaCode = row["AREA_CODE"].ToString(),
        //                            AreaName = row["AREA_NAME"].ToString(),
        //                            TerritoryCode = row["TERRITORY_CODE"].ToString(),
        //                            TerritoryName = row["TERRITORY_NAME"].ToString(),

        //                            CustomerCode = row["CUSTOMER_CODE"].ToString(),
        //                            CustomerName = row["CUSTOMER_NAME"].ToString(),
        //                            DbLocation = row["DB_LOCATION"].ToString(),

        //                            MarketCode = row["MARKET_CODE"].ToString(),
        //                            MarketName = row["MARKET_NAME"].ToString(),

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



        public List<MomSkuWiseNumericDistributionBEL> GetMomProdWiseLocNuDistLy(string dCode, string rCode, string aCode, string tCode, string cCode, string pCode)
        {
            try
            {
                string qry;

                int userId = Convert.ToInt32(HttpContext.Current.Session["USER_ID"]);
                int UserAccessCount = Convert.ToInt32(HttpContext.Current.Session["USER_BASE_REPORT_FILTER"]);

                if (UserAccessCount > 0)
                {

                    qry = " SELECT DIVISION_CODE, DIVISION_NAME, REGION_CODE, REGION_NAME, AREA_CODE, AREA_NAME, TERRITORY_CODE, TERRITORY_NAME, DB_LOCATION, CUSTOMER_CODE, CUSTOMER_NAME, MARKET_CODE, MARKET_NAME, BASE_PRODUCT_CODE, BASE_PRODUCT_NAME, JAN, FEB, MAR, APR, MAY, JUN, JUL, AUG, SEP, OCT, NOV, DEC, TOTAL " +
                          " FROM MV_MOM_LOC_PROD_NM_DIST_LY " +
                          " WHERE (DIVISION_CODE = '" + dCode + "' OR '" + dCode + "' = 'ALL')" +
                          " AND   (REGION_CODE='" + rCode + "' OR '" + rCode + "'='ALL')" +
                          " AND   (AREA_CODE='" + aCode + "' OR '" + aCode + "'='ALL')" +
                          " AND   (TERRITORY_CODE='" + tCode + "' OR '" + tCode + "'='ALL')" +
                          " AND   (CUSTOMER_CODE='" + cCode + "' OR '" + cCode + "'='ALL')" +
                          " AND   (BASE_PRODUCT_CODE='" + pCode + "' OR '" + pCode + "'='ALL')" +
                          //" AND BASE_PRODUCT_CODE IN (SELECT PRODUCT_TYPE_CODE FROM USER_PRODUCT_TYPE WHERE  TYPE_NAME='BASE_PRODUCT' AND USER_ID=" + userId + ")" +
                          " ORDER BY DIVISION_NAME,REGION_NAME, AREA_NAME, TERRITORY_NAME, CUSTOMER_NAME, MARKET_NAME, BASE_PRODUCT_NAME";

                }
                else
                {
                    qry = " SELECT DIVISION_CODE, DIVISION_NAME, REGION_CODE, REGION_NAME, AREA_CODE, AREA_NAME, TERRITORY_CODE, TERRITORY_NAME, DB_LOCATION, CUSTOMER_CODE, CUSTOMER_NAME, MARKET_CODE, MARKET_NAME, BASE_PRODUCT_CODE, BASE_PRODUCT_NAME, JAN, FEB, MAR, APR, MAY, JUN, JUL, AUG, SEP, OCT, NOV, DEC, TOTAL " +
                          " FROM MV_MOM_LOC_PROD_NM_DIST_LY " +
                          " WHERE (DIVISION_CODE = '" + dCode + "' OR '" + dCode + "' = 'ALL')" +
                          " AND   (REGION_CODE='" + rCode + "' OR '" + rCode + "'='ALL')" +
                          " AND   (AREA_CODE='" + aCode + "' OR '" + aCode + "'='ALL')" +
                          " AND   (TERRITORY_CODE='" + tCode + "' OR '" + tCode + "'='ALL')" +
                          " AND   (CUSTOMER_CODE='" + cCode + "' OR '" + cCode + "'='ALL')" +
                          " AND   (BASE_PRODUCT_CODE='" + pCode + "' OR '" + pCode + "'='ALL')" +
                          " ORDER BY DIVISION_NAME,REGION_NAME, AREA_NAME, TERRITORY_NAME, CUSTOMER_NAME, MARKET_NAME, BASE_PRODUCT_NAME";

                }

                //DataTable dt = _dbHelper.GetDataTable(qry);
                DataTable dt = _dbHelper.GetDataTableWithAuditTrial(qry, "Product Wise Numeric Distribution (Last Year - MOM)");
                var item = (from DataRow row in dt.Rows
                            select new MomSkuWiseNumericDistributionBEL
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