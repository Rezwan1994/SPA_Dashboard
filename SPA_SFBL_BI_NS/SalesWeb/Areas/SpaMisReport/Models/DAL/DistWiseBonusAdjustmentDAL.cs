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
    public class DistWiseBonusAdjustmentDAL : ReturnData
    {
        private readonly DBHelper _dbHelper = new DBHelper();
        private readonly ErrorLogger _errorLogger = new ErrorLogger();
        private static readonly DBConnection DBConnection = new DBConnection();
        public readonly string ConnString = DBConnection.SAConnStrReader("Oracle", "PHARMAERP");

        public List<DistWiseBonusAdjustmentBEL> GetDistWiseBonusAdjustmentCMonth(string dCode, string rCode, string aCode, string tCode, string cCode)

        {
            try
            {
                string qry =  " SELECT " +
                              " DIVISION_CODE," +
                              " DIVISION_NAME," +
                              " REGION_CODE," +
                              " REGION_NAME," +
                              " AREA_CODE," +
                              " AREA_NAME," +
                              " TERRITORY_CODE," +
                              " TERRITORY_NAME," +
                              " CUSTOMER_CODE," +
                              " CUSTOMER_NAME," +
                              " DB_LOCATION," +
                              " PRODUCT_CODE," +
                              " PRODUCT_NAME," +
                              " PACK_SIZE, BONUS_QTY," +
                              " RET_BONUS_QTY," +
                              " NET_BONUS_QTY," +
                              " BONUS_VAL," +
                              " RET_BONUS_VAL," +
                              " NET_BONUS_VAL," +
                              " BONUS_PRICE_DISCOUNT," +
                              " RET_BONUS_PRICE_DISCOUNT," +
                              " NET_BONUS_PRICE_DISCOUNT " +
                              " FROM MV_DIST_BONUS_ADJUST_CMONTH " +
                              " WHERE (DIVISION_CODE = '" + dCode + "' OR '" + dCode + "' = 'ALL')" +
                              " AND   (REGION_CODE='" + rCode + "' OR '" + rCode + "'='ALL')" +
                              " AND   (AREA_CODE='" + aCode + "' OR '" + aCode + "'='ALL')" +
                              " AND   (TERRITORY_CODE='" + tCode + "' OR '" + tCode + "'='ALL')" +
                              " AND   (CUSTOMER_CODE='" + cCode + "' OR '" + cCode + "'='ALL')" +
                              " ORDER BY DIVISION_CODE,REGION_CODE, AREA_CODE, TERRITORY_CODE, CUSTOMER_CODE, PRODUCT_CODE";


                //DataTable dt = _dbHelper.GetDataTable(qry);
                DataTable dt = _dbHelper.GetDataTableWithAuditTrial(qry, "Distributor Bonus Adjustment (Current Month)");
                var item = (from DataRow row in dt.Rows
                            select new DistWiseBonusAdjustmentBEL
                            {
                                DivisionCode = row["DIVISION_CODE"].ToString(),
                                DivisionName = row["DIVISION_NAME"].ToString(),
                                RegionCode = row["REGION_CODE"].ToString(),
                                RegionName = row["REGION_NAME"].ToString(),
                                AreaCode = row["AREA_CODE"].ToString(),
                                AreaName = row["AREA_NAME"].ToString(),
                                TerritoryCode = row["TERRITORY_CODE"].ToString(),
                                TerritoryName = row["TERRITORY_NAME"].ToString(),
                                DBLocation = row["DB_LOCATION"].ToString(),
                                DistributorCode = row["CUSTOMER_CODE"].ToString(),
                                DistributorName = row["CUSTOMER_NAME"].ToString(),
                                ProductCode = row["PRODUCT_CODE"].ToString(),
                                ProductName = row["PRODUCT_NAME"].ToString(),
                                PackSize = row["PACK_SIZE"].ToString(),
                                BonusQty = Convert.ToInt32(row["BONUS_QTY"]),
                                BonusVal = Convert.ToDouble(row["BONUS_VAL"]),
                                RetBnsQty = Convert.ToInt32(row["RET_BONUS_QTY"]),
                                RetBnsVal = Convert.ToDouble(row["RET_BONUS_VAL"]),
                                ActualBnsQty = Convert.ToInt32(row["NET_BONUS_QTY"]),
                                ActualBnsVal = Convert.ToDouble(row["NET_BONUS_VAL"]),
                                BonusPriceDiscount = Convert.ToDouble(row["NET_BONUS_PRICE_DISCOUNT"])

          


    }).ToList();
                return item;
            }
            catch (Exception e)
            {
                var lineNum = e.StackTrace.Substring(e.StackTrace.LastIndexOf(' '));
                _errorLogger.GetErrorMessage(e.Message, "DistWiseBonusAdjustmentDAL", lineNum);
                ExceptionReturn = e.Message;
                throw;
            }
        }
        public List<DistWiseBonusAdjustmentBEL> GetDistWiseBonusAdjustmentLMonth(string dCode, string rCode, string aCode, string tCode, string cCode)

        {
            try
            {
                string qry = " SELECT " +
                              " DIVISION_CODE," +
                              " DIVISION_NAME," +
                              " REGION_CODE," +
                              " REGION_NAME," +
                              " AREA_CODE," +
                              " AREA_NAME," +
                              " TERRITORY_CODE," +
                              " TERRITORY_NAME," +
                              " CUSTOMER_CODE," +
                              " CUSTOMER_NAME," +
                              " DB_LOCATION," +
                              " PRODUCT_CODE," +
                              " PRODUCT_NAME," +
                              " PACK_SIZE, BONUS_QTY," +
                              " RET_BONUS_QTY," +
                              " NET_BONUS_QTY," +
                              " BONUS_VAL," +
                              " RET_BONUS_VAL," +
                              " NET_BONUS_VAL," +
                              " BONUS_PRICE_DISCOUNT," +
                              " RET_BONUS_PRICE_DISCOUNT," +
                              " NET_BONUS_PRICE_DISCOUNT " +
                              " FROM MV_DIST_BONUS_ADJUST_LMONTH " +
                              " WHERE (DIVISION_CODE = '" + dCode + "' OR '" + dCode + "' = 'ALL')" +
                              " AND   (REGION_CODE='" + rCode + "' OR '" + rCode + "'='ALL')" +
                              " AND   (AREA_CODE='" + aCode + "' OR '" + aCode + "'='ALL')" +
                              " AND   (TERRITORY_CODE='" + tCode + "' OR '" + tCode + "'='ALL')" +
                              " AND   (CUSTOMER_CODE='" + cCode + "' OR '" + cCode + "'='ALL')" +
                              " ORDER BY DIVISION_CODE,REGION_CODE, AREA_CODE, TERRITORY_CODE, CUSTOMER_CODE, PRODUCT_CODE";


                //DataTable dt = _dbHelper.GetDataTable(qry);
                DataTable dt = _dbHelper.GetDataTableWithAuditTrial(qry, "Distributor Bonus Adjustment (Last Month)");

                var item = (from DataRow row in dt.Rows
                            select new DistWiseBonusAdjustmentBEL
                            {
                                DivisionCode = row["DIVISION_CODE"].ToString(),
                                DivisionName = row["DIVISION_NAME"].ToString(),
                                RegionCode = row["REGION_CODE"].ToString(),
                                RegionName = row["REGION_NAME"].ToString(),
                                AreaCode = row["AREA_CODE"].ToString(),
                                AreaName = row["AREA_NAME"].ToString(),
                                TerritoryCode = row["TERRITORY_CODE"].ToString(),
                                TerritoryName = row["TERRITORY_NAME"].ToString(),
                                DBLocation = row["DB_LOCATION"].ToString(),
                                DistributorCode = row["CUSTOMER_CODE"].ToString(),
                                DistributorName = row["CUSTOMER_NAME"].ToString(),
                                ProductCode = row["PRODUCT_CODE"].ToString(),
                                ProductName = row["PRODUCT_NAME"].ToString(),
                                PackSize = row["PACK_SIZE"].ToString(),
                                BonusQty = Convert.ToInt32(row["BONUS_QTY"]),
                                BonusVal = Convert.ToDouble(row["BONUS_VAL"]),
                                RetBnsQty = Convert.ToInt32(row["RET_BONUS_QTY"]),
                                RetBnsVal = Convert.ToDouble(row["RET_BONUS_VAL"]),
                                ActualBnsQty = Convert.ToInt32(row["NET_BONUS_QTY"]),
                                ActualBnsVal = Convert.ToDouble(row["NET_BONUS_VAL"]),
                                BonusPriceDiscount = Convert.ToDouble(row["NET_BONUS_PRICE_DISCOUNT"])




                            }).ToList();
                return item;
            }
            catch (Exception e)
            {
                var lineNum = e.StackTrace.Substring(e.StackTrace.LastIndexOf(' '));
                _errorLogger.GetErrorMessage(e.Message, "DistWiseBonusAdjustmentDAL", lineNum);
                ExceptionReturn = e.Message;
                throw;
            }
        }
        public object GetDistWiseBonusAdjustmentDt(string fDate, string tDate, string dCode, string rCode, string aCode, string tCode, string cCode)
        {
            try
            {
                using (OracleConnection objConn = new OracleConnection(ConnString))
                {
                    using (OracleCommand objCmd = new OracleCommand())
                    {
                        objCmd.Connection = objConn;
                        objCmd.CommandText = "FN_DIST_BONUS_ADJ_DATE_RANGE";
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
                        _dbHelper.InsertReportAudit("Distributor Bonus Adjustment (Custom Date)");
                        List<DistWiseBonusAdjustmentBEL> item;
                        item = (from DataRow row in dt.Rows
                                select new DistWiseBonusAdjustmentBEL
                                {

                                    DivisionCode = row["DIVISION_CODE"].ToString(),
                                    DivisionName = row["DIVISION_NAME"].ToString(),
                                    RegionCode = row["REGION_CODE"].ToString(),
                                    RegionName = row["REGION_NAME"].ToString(),
                                    AreaCode = row["AREA_CODE"].ToString(),
                                    AreaName = row["AREA_NAME"].ToString(),
                                    TerritoryCode = row["TERRITORY_CODE"].ToString(),
                                    TerritoryName = row["TERRITORY_NAME"].ToString(),
                                    DBLocation = row["DB_LOCATION"].ToString(),
                                    DistributorCode = row["CUSTOMER_CODE"].ToString(),
                                    DistributorName = row["CUSTOMER_NAME"].ToString(),
                                    ProductCode = row["PRODUCT_CODE"].ToString(),
                                    ProductName = row["PRODUCT_NAME"].ToString(),
                                    PackSize = row["PACK_SIZE"].ToString(),
                                    BonusQty = Convert.ToInt32(row["BONUS_QTY"]),
                                    BonusVal = Convert.ToDouble(row["BONUS_VAL"]),
                                    RetBnsQty = Convert.ToInt32(row["RET_BONUS_QTY"]),
                                    RetBnsVal = Convert.ToDouble(row["RET_BONUS_VAL"]),
                                    ActualBnsQty = Convert.ToInt32(row["NET_BONUS_QTY"]),
                                    ActualBnsVal = Convert.ToDouble(row["NET_BONUS_VAL"]),
                                    BonusPriceDiscount = Convert.ToDouble(row["NET_BONUS_PRICE_DISCOUNT"])

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