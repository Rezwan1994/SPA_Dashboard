﻿using System;
using System.Data;
using System.Linq;
using System.Web;
using SalesWeb.Areas.SpaMisReport.Models.BEL;
using SalesWeb.Universal.Gateway;
using System.Collections.Generic;
using System.Data.OracleClient;

namespace SalesWeb.Areas.SpaMisReport.Models.DAL
{
    public class DistributorAchievementDAL : ReturnData
    {

        private readonly DBHelper _dbHelper = new DBHelper();
        private readonly ErrorLogger _errorLogger = new ErrorLogger();
        private static readonly DBConnection DBConnection = new DBConnection();
        public readonly string ConnString = DBConnection.SAConnStrReader("Oracle", "PHARMAERP");

        public List<DistributorAchievementBEL> GetDistributorAchievementCurrentMonth(string dCode, string rCode, string aCode, string tCode, string cCode)
        {
            try
            {
                string qry = " SELECT DIVISION_CODE, DIVISION_NAME, REGION_CODE, REGION_NAME, AREA_CODE, AREA_NAME, TERRITORY_CODE, TERRITORY_NAME, CUSTOMER_CODE, CUSTOMER_NAME, DB_LOCATION, PRODUCT_CODE, PRODUCT_NAME, PACK_SIZE, SALES_QTY, SALES_VAL, CUMM_SALES_QTY, CUMM_SALES_VAL, TARGET_QTY, TARGET_VAL, ACHIEVEMENT " +
                              " FROM MV_DISTRIBUTOR_ACH_CMONTH " +
                              " WHERE (DIVISION_CODE = '" + dCode + "' OR '" + dCode + "' = 'ALL')" +
                              " AND   (REGION_CODE='" + rCode + "' OR '" + rCode + "'='ALL')" +
                              " AND   (AREA_CODE='" + aCode + "' OR '" + aCode + "'='ALL')" +
                              " AND   (TERRITORY_CODE='" + tCode + "' OR '" + tCode + "'='ALL')" +
                              " AND   (CUSTOMER_CODE='" + cCode + "' OR '" + cCode + "'='ALL')" +
                              " ORDER BY DIVISION_CODE,REGION_CODE, AREA_CODE, TERRITORY_CODE, CUSTOMER_CODE,PRODUCT_CODE";

                //DataTable dt = _dbHelper.GetDataTable(qry);
                DataTable dt = _dbHelper.GetDataTableWithAuditTrial(qry, "Distributor Achievement(Current Month)");
                int count = 0;
                var item = (from DataRow row in dt.Rows
                            select new DistributorAchievementBEL
                            {
                                SlNo = ++count,
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
                                ProductCode = row["PRODUCT_CODE"].ToString(),
                                ProductName = row["PRODUCT_NAME"].ToString(),
                                PackSize = row["PACK_SIZE"].ToString(),
                                SalesQty = Convert.ToInt32(row["SALES_QTY"]),
                                SalesVal = Convert.ToDouble(row["SALES_VAL"]),
                                CummSalesQty = Convert.ToInt32(row["CUMM_SALES_QTY"]),
                                CummSalesVal = Convert.ToDouble(row["CUMM_SALES_VAL"]),
                                TargetQty = Convert.ToInt32(row["TARGET_QTY"]),
                                TargetVal = Convert.ToDouble(row["TARGET_VAL"]),
                                Achievement = Convert.ToDouble(row["ACHIEVEMENT"])
                            }).ToList();
                return item;
            }
            catch (Exception e)
            {
                var lineNum = e.StackTrace.Substring(e.StackTrace.LastIndexOf(' '));
                _errorLogger.GetErrorMessage(e.Message, "DistributorAchievementDAL", lineNum);
                ExceptionReturn = e.Message;
                throw;
            }
        }



        public List<DistributorAchievementBEL> GetDistributorAchievementLastMonth(string dCode, string rCode, string aCode, string tCode, string cCode)
        {
            try
            {
                string qry = " SELECT DIVISION_CODE, DIVISION_NAME, REGION_CODE, REGION_NAME, AREA_CODE, AREA_NAME, TERRITORY_CODE, TERRITORY_NAME, CUSTOMER_CODE, CUSTOMER_NAME, DB_LOCATION, PRODUCT_CODE, PRODUCT_NAME, PACK_SIZE, SALES_QTY, SALES_VAL, CUMM_SALES_QTY, CUMM_SALES_VAL, TARGET_QTY, TARGET_VAL, ACHIEVEMENT " +
                              " FROM MV_DISTRIBUTOR_ACH_LMONTH " +
                              " WHERE (DIVISION_CODE = '" + dCode + "' OR '" + dCode + "' = 'ALL')" +
                              " AND   (REGION_CODE='" + rCode + "' OR '" + rCode + "'='ALL')" +
                              " AND   (AREA_CODE='" + aCode + "' OR '" + aCode + "'='ALL')" +
                              " AND   (TERRITORY_CODE='" + tCode + "' OR '" + tCode + "'='ALL')" +
                              " AND   (CUSTOMER_CODE='" + cCode + "' OR '" + cCode + "'='ALL')" +
                              " ORDER BY DIVISION_CODE,REGION_CODE, AREA_CODE, TERRITORY_CODE, CUSTOMER_CODE,PRODUCT_CODE";

                //DataTable dt = _dbHelper.GetDataTable(qry);
                DataTable dt = _dbHelper.GetDataTableWithAuditTrial(qry, "Distributor Achievement(Last Month)");
                int count = 0;
                var item = (from DataRow row in dt.Rows
                            select new DistributorAchievementBEL
                            {
                                SlNo = ++count,
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
                                ProductCode = row["PRODUCT_CODE"].ToString(),
                                ProductName = row["PRODUCT_NAME"].ToString(),
                                PackSize = row["PACK_SIZE"].ToString(),
                                SalesQty = Convert.ToInt32(row["SALES_QTY"]),
                                SalesVal = Convert.ToDouble(row["SALES_VAL"]),
                                CummSalesQty = Convert.ToInt32(row["CUMM_SALES_QTY"]),
                                CummSalesVal = Convert.ToDouble(row["CUMM_SALES_VAL"]),
                                TargetQty = Convert.ToInt32(row["TARGET_QTY"]),
                                TargetVal = Convert.ToDouble(row["TARGET_VAL"]),
                                Achievement = Convert.ToDouble(row["ACHIEVEMENT"])
                            }).ToList();
                return item;
            }
            catch (Exception e)
            {
                var lineNum = e.StackTrace.Substring(e.StackTrace.LastIndexOf(' '));
                _errorLogger.GetErrorMessage(e.Message, "DistributorAchievementDAL", lineNum);
                ExceptionReturn = e.Message;
                throw;
            }
        }




    }
}