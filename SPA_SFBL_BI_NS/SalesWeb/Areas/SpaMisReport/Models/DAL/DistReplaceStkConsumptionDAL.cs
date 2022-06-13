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
    public class DistReplaceStkConsumptionDAL :ReturnData
    {
        private readonly DBHelper _dbHelper = new DBHelper();
        private readonly ErrorLogger _errorLogger = new ErrorLogger();
        private static readonly DBConnection DBConnection = new DBConnection();
        public readonly string ConnString = DBConnection.SAConnStrReader("Oracle", "PHARMAERP");


        public List<DistReplaceStkConsumptionBEL> GetDistReplaceStkConsumption(string dCode, string rCode, string aCode, string tCode, string cCode)


        {
            try
            {
                string qry = "SELECT DIVISION_CODE, DIVISION_NAME, REGION_CODE, REGION_NAME, AREA_CODE, AREA_NAME, TERRITORY_CODE, TERRITORY_NAME, DB_LOCATION, CUSTOMER_CODE, CUSTOMER_NAME, ADDRESS, PRODUCT_CODE, PRODUCT_NAME, PACK_SIZE, UNIT_TP, OPENING_REPLACE_QTY, REPLACE_RECV_RET_QTY, TOTAL_QTY, REPLACE_RETURN_QTY, REPLACE_FACTORY_QTY, TOTAL_DEDUCT_QTY, CLOSING_REPLACE_QTY " +
                              " FROM MV_DIST_REPLACE_STK_TRANS " +
                              " WHERE (DIVISION_CODE = '" + dCode + "' OR '" + dCode + "' = 'ALL')" +
                              " AND   (REGION_CODE='" + rCode + "' OR '" + rCode + "'='ALL')" +
                              " AND   (AREA_CODE='" + aCode + "' OR '" + aCode + "'='ALL')" +
                              " AND   (TERRITORY_CODE='" + tCode + "' OR '" + tCode + "'='ALL')" +
                              " AND   (CUSTOMER_CODE='" + cCode + "' OR '" + cCode + "'='ALL')" +
                              " ORDER BY DIVISION_CODE,REGION_CODE, AREA_CODE, TERRITORY_CODE, CUSTOMER_CODE, PRODUCT_CODE";


                //DataTable dt = _dbHelper.GetDataTable(qry);
                DataTable dt = _dbHelper.GetDataTableWithAuditTrial(qry, "Distributor Replace Stock Consumption");
                int count = 0;
                var item = (from DataRow row in dt.Rows
                            select new DistReplaceStkConsumptionBEL
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
                                DBLocation = row["DB_LOCATION"].ToString(),
                                CustomerCode = row["CUSTOMER_CODE"].ToString(),
                                CustomerName = row["CUSTOMER_NAME"].ToString(),
                                Address = row["ADDRESS"].ToString(),
                                ProductCode = row["PRODUCT_CODE"].ToString(),
                                ProductName = row["PRODUCT_NAME"].ToString(),
                                PackSize = row["PACK_SIZE"].ToString(),
                                ProductPrice = row["UNIT_TP"].ToString(),
                                OpeningReplaceQty = Convert.ToInt32(row["OPENING_REPLACE_QTY"]),
                                ReplaceRecvRetQty = Convert.ToInt32(row["REPLACE_RECV_RET_QTY"]),
                                TotalQty = Convert.ToInt32(row["TOTAL_QTY"]),
                                ReplaceReturnQty = Convert.ToInt32(row["REPLACE_RETURN_QTY"]),
                                ReplaceFactoryQty = Convert.ToInt32(row["REPLACE_FACTORY_QTY"]),
                                TotalDeductQty = Convert.ToInt32(row["TOTAL_DEDUCT_QTY"]),
                                ClosingReplaceQty = Convert.ToInt32(row["CLOSING_REPLACE_QTY"])
                             

                            }).ToList();
                return item;
            }
            catch (Exception e)
            {
                var lineNum = e.StackTrace.Substring(e.StackTrace.LastIndexOf(' '));
                _errorLogger.GetErrorMessage(e.Message, "DistReplaceStkConsumptionDAL", lineNum);
                ExceptionReturn = e.Message;
                throw;
            }
        }





    }
}