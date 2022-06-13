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
    public class MomCatWiseNumericDistributionDAL : ReturnData
    {
        private readonly DBHelper _dbHelper = new DBHelper();
        private readonly ErrorLogger _errorLogger = new ErrorLogger();
        private static readonly DBConnection DBConnection = new DBConnection();
        public readonly string ConnString = DBConnection.SAConnStrReader("Oracle", "PHARMAERP");



        //public object GetMomCatWiseNumericDistribution(string dCode, string rCode, string aCode, string tCode, string cCode, string pcCode)
        //{
        //    try
        //    {

        //        int userId = Convert.ToInt32(HttpContext.Current.Session["USER_ID"]);

        //        using (OracleConnection objConn = new OracleConnection(ConnString))
        //        {
        //            using (OracleCommand objCmd = new OracleCommand())
        //            {
        //                objCmd.Connection = objConn;
        //                objCmd.CommandText = "FN_MOM_LOC_CAT_NUMERIC_DIST";
        //                objCmd.CommandType = CommandType.StoredProcedure;
        //                objCmd.Parameters.Add("pdivision_code", OracleType.VarChar).Value = dCode;
        //                objCmd.Parameters.Add("pregion_code", OracleType.VarChar).Value = rCode;
        //                objCmd.Parameters.Add("parea_code", OracleType.VarChar).Value = aCode;
        //                objCmd.Parameters.Add("pterritory_code", OracleType.VarChar).Value = tCode;
        //                objCmd.Parameters.Add("pcustomer_code", OracleType.VarChar).Value = cCode;
        //                objCmd.Parameters.Add("pcategory_code", OracleType.VarChar).Value = pcCode;
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

        //                List<MomCatWiseNumericDistributionBEL> item;
        //                item = (from DataRow row in dt.Rows
        //                        select new MomCatWiseNumericDistributionBEL
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
        //                            ProductCategoryCode = row["PRODUCT_CATEGORY_CODE"].ToString(),
        //                            ProductCategoryName = row["PRODUCT_CATEGORY_NAME"].ToString(),

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

        public List<MomCatWiseNumericDistributionBEL> GetMomCatWiseNumericDistribution(string dCode, string rCode, string aCode, string tCode, string cCode, string pcCode)
        {
            try
            {
                string qry = " SELECT DIVISION_CODE, DIVISION_NAME, REGION_CODE, REGION_NAME, AREA_CODE, AREA_NAME, TERRITORY_CODE, TERRITORY_NAME,  CUSTOMER_CODE, CUSTOMER_NAME,DB_LOCATION,MARKET_CODE, MARKET_NAME, PRODUCT_CATEGORY_CODE, PRODUCT_CATEGORY_NAME, JAN, FEB, MAR, APR, MAY, JUN, JUL, AUG, SEP, OCT, NOV, DEC, TOTAL " +
                             " FROM MV_MOM_LOC_CAT_NUMERIC_DIST_CY " +
                             " WHERE (DIVISION_CODE = '" + dCode + "' OR '" + dCode + "' = 'ALL')" +
                             " AND   (REGION_CODE='" + rCode + "' OR '" + rCode + "'='ALL')" +
                             " AND   (AREA_CODE='" + aCode + "' OR '" + aCode + "'='ALL')" +
                             " AND   (TERRITORY_CODE='" + tCode + "' OR '" + tCode + "'='ALL')" +
                             " AND   (CUSTOMER_CODE='" + cCode + "' OR '" + cCode + "'='ALL')" +
                             " AND   (PRODUCT_CATEGORY_CODE='" + pcCode + "' OR '" + pcCode + "'='ALL')" +
                             " ORDER BY DIVISION_CODE,REGION_CODE, AREA_CODE, TERRITORY_CODE, CUSTOMER_CODE,MARKET_CODE,PRODUCT_CATEGORY_CODE";

                //DataTable dt = _dbHelper.GetDataTable(qry);
                DataTable dt = _dbHelper.GetDataTableWithAuditTrial(qry, "Category Wise Numeric Distribution(Current Year-MOM)");
                var item = (from DataRow row in dt.Rows
                            select new MomCatWiseNumericDistributionBEL
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
                                ProductCategoryCode = row["PRODUCT_CATEGORY_CODE"].ToString(),
                                ProductCategoryName = row["PRODUCT_CATEGORY_NAME"].ToString(),

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
                _errorLogger.GetErrorMessage(e.Message, "CatNumericSalesAnalysisDAL", lineNum);
                ExceptionReturn = e.Message;
                throw;
            }
        }



        //public object GetMomCatWiseNumericDistributionLy(string dCode, string rCode, string aCode, string tCode, string cCode, string pcCode)
        //{
        //    try
        //    {

        //        int userId = Convert.ToInt32(HttpContext.Current.Session["USER_ID"]);

        //        using (OracleConnection objConn = new OracleConnection(ConnString))
        //        {
        //            using (OracleCommand objCmd = new OracleCommand())
        //            {
        //                objCmd.Connection = objConn;
        //                objCmd.CommandText = "FN_MOM_LOC_CAT_NU_DIST_LY";
        //                objCmd.CommandType = CommandType.StoredProcedure;
        //                objCmd.Parameters.Add("pdivision_code", OracleType.VarChar).Value = dCode;
        //                objCmd.Parameters.Add("pregion_code", OracleType.VarChar).Value = rCode;
        //                objCmd.Parameters.Add("parea_code", OracleType.VarChar).Value = aCode;
        //                objCmd.Parameters.Add("pterritory_code", OracleType.VarChar).Value = tCode;
        //                objCmd.Parameters.Add("pcustomer_code", OracleType.VarChar).Value = cCode;
        //                objCmd.Parameters.Add("pcategory_code", OracleType.VarChar).Value = pcCode;
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

        //                List<MomCatWiseNumericDistributionBEL> item;
        //                item = (from DataRow row in dt.Rows
        //                        select new MomCatWiseNumericDistributionBEL
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
        //                            ProductCategoryCode = row["PRODUCT_CATEGORY_CODE"].ToString(),
        //                            ProductCategoryName = row["PRODUCT_CATEGORY_NAME"].ToString(),

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

        public List<MomCatWiseNumericDistributionBEL> GetMomCatWiseNumericDistributionLy(string dCode, string rCode, string aCode, string tCode, string cCode, string pcCode)
        {
            try
            {
                string qry = " SELECT DIVISION_CODE, DIVISION_NAME, REGION_CODE, REGION_NAME, AREA_CODE, AREA_NAME, TERRITORY_CODE, TERRITORY_NAME,  CUSTOMER_CODE, CUSTOMER_NAME,DB_LOCATION,MARKET_CODE, MARKET_NAME, PRODUCT_CATEGORY_CODE, PRODUCT_CATEGORY_NAME, JAN, FEB, MAR, APR, MAY, JUN, JUL, AUG, SEP, OCT, NOV, DEC, TOTAL " +
                             " FROM MV_MOM_LOC_CAT_NUMERIC_DIST_LY " +
                             " WHERE (DIVISION_CODE = '" + dCode + "' OR '" + dCode + "' = 'ALL')" +
                             " AND   (REGION_CODE='" + rCode + "' OR '" + rCode + "'='ALL')" +
                             " AND   (AREA_CODE='" + aCode + "' OR '" + aCode + "'='ALL')" +
                             " AND   (TERRITORY_CODE='" + tCode + "' OR '" + tCode + "'='ALL')" +
                             " AND   (CUSTOMER_CODE='" + cCode + "' OR '" + cCode + "'='ALL')" +
                             " AND   (PRODUCT_CATEGORY_CODE='" + pcCode + "' OR '" + pcCode + "'='ALL')" +
                             " ORDER BY DIVISION_CODE,REGION_CODE, AREA_CODE, TERRITORY_CODE, CUSTOMER_CODE,MARKET_CODE,PRODUCT_CATEGORY_CODE";

                //DataTable dt = _dbHelper.GetDataTable(qry);
                DataTable dt = _dbHelper.GetDataTableWithAuditTrial(qry, "Category Wise Numeric Distribution(Last Year-MOM)");
                var item = (from DataRow row in dt.Rows
                            select new MomCatWiseNumericDistributionBEL
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
                                ProductCategoryCode = row["PRODUCT_CATEGORY_CODE"].ToString(),
                                ProductCategoryName = row["PRODUCT_CATEGORY_NAME"].ToString(),

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
                _errorLogger.GetErrorMessage(e.Message, "CatNumericSalesAnalysisDAL", lineNum);
                ExceptionReturn = e.Message;
                throw;
            }
        }

    }
}