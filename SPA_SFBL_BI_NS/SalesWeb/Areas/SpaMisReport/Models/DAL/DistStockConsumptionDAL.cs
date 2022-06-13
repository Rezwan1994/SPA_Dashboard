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
    public class DistStockConsumptionDAL : ReturnData
    {

        private readonly DBHelper _dbHelper = new DBHelper();
        private readonly ErrorLogger _errorLogger = new ErrorLogger();
        private static readonly DBConnection DBConnection = new DBConnection();
        public readonly string ConnString = DBConnection.SAConnStrReader("Oracle", "PHARMAERP");


        public List<DistStockConsumptionBEL> GetDistStockConsumption(string dCode, string rCode, string aCode, string tCode, string cCode)

        {
            try
            {

                string qry;

                int userId = Convert.ToInt32(HttpContext.Current.Session["USER_ID"]);
                int UserAccessCount = Convert.ToInt32(HttpContext.Current.Session["USER_BASE_REPORT_FILTER"]);

                if (UserAccessCount > 0)
                {


                    qry = " SELECT DIVISION_CODE, DIVISION_NAME, REGION_CODE, REGION_NAME, AREA_CODE, AREA_NAME, TERRITORY_CODE, TERRITORY_NAME, DB_LOCATION, CUSTOMER_CODE, CUSTOMER_NAME, ADDRESS, PRODUCT_CODE, PRODUCT_NAME, PACK_SIZE, UNIT_TP, OPENING_STOCK_QTY, RECEIVE_QTY, REPLACE_RECEIVE_QTY, PREVIOUS_RETURN_RECEIVE_QTY, RETURN_RECEIVE_QTY, PREV_REPLACE_RET_RECEIVE_QTY, REPLACE_RET_RECEIVE_QTY, GAIN_QTY, TOTAL_IN_QTY, ISSUED_QTY, REPLACE_ISSUED_QTY, DISPATCH_QTY, BONUS_QTY, TRADE_BONUS_QTY, COMBO_BONUS_QTY, LOSS_QTY, REQUI_RETURN_QTY, DAMAGE_STOCK_TRANSFER_QTY, TOTAL_OUT_QTY, CLOSING_QTY, CLOSING_VALUE, CUST_PROD_WISE_TARGET_QTY, CUST_PROD_WISE_TARGET_VALUE " +
                          " FROM MV_DIST_STOCK_CONSUMPTION " +
                          " WHERE (DIVISION_CODE = '" + dCode + "' OR '" + dCode + "' = 'ALL')" +
                          " AND   (REGION_CODE='" + rCode + "' OR '" + rCode + "'='ALL')" +
                          " AND   (AREA_CODE='" + aCode + "' OR '" + aCode + "'='ALL')" +
                          " AND   (TERRITORY_CODE='" + tCode + "' OR '" + tCode + "'='ALL')" +
                          " AND   (CUSTOMER_CODE='" + cCode + "' OR '" + cCode + "'='ALL')" +
                          " AND   PRODUCT_CODE IN (SELECT PRODUCT_CODE FROM USER_PRODUCT_DTL WHERE USER_ID=" + userId + ")" +
                          " ORDER BY DIVISION_CODE,REGION_CODE, AREA_CODE, TERRITORY_CODE, CUSTOMER_CODE, PRODUCT_CODE";


                }
                else
                {


                    qry = " SELECT DIVISION_CODE, DIVISION_NAME, REGION_CODE, REGION_NAME, AREA_CODE, AREA_NAME, TERRITORY_CODE, TERRITORY_NAME, DB_LOCATION, CUSTOMER_CODE, CUSTOMER_NAME, ADDRESS, PRODUCT_CODE, PRODUCT_NAME, PACK_SIZE, UNIT_TP, OPENING_STOCK_QTY, RECEIVE_QTY, REPLACE_RECEIVE_QTY, PREVIOUS_RETURN_RECEIVE_QTY, RETURN_RECEIVE_QTY, PREV_REPLACE_RET_RECEIVE_QTY, REPLACE_RET_RECEIVE_QTY, GAIN_QTY, TOTAL_IN_QTY, ISSUED_QTY, REPLACE_ISSUED_QTY, DISPATCH_QTY, BONUS_QTY, TRADE_BONUS_QTY, COMBO_BONUS_QTY, LOSS_QTY, REQUI_RETURN_QTY, DAMAGE_STOCK_TRANSFER_QTY, TOTAL_OUT_QTY, CLOSING_QTY, CLOSING_VALUE, CUST_PROD_WISE_TARGET_QTY, CUST_PROD_WISE_TARGET_VALUE " +
                          " FROM MV_DIST_STOCK_CONSUMPTION " +
                          " WHERE (DIVISION_CODE = '" + dCode + "' OR '" + dCode + "' = 'ALL')" +
                          " AND   (REGION_CODE='" + rCode + "' OR '" + rCode + "'='ALL')" +
                          " AND   (AREA_CODE='" + aCode + "' OR '" + aCode + "'='ALL')" +
                          " AND   (TERRITORY_CODE='" + tCode + "' OR '" + tCode + "'='ALL')" +
                          " AND   (CUSTOMER_CODE='" + cCode + "' OR '" + cCode + "'='ALL')" +
                          " ORDER BY DIVISION_CODE,REGION_CODE, AREA_CODE, TERRITORY_CODE, CUSTOMER_CODE, PRODUCT_CODE";

                }




                //DataTable dt = _dbHelper.GetDataTable(qry);
                DataTable dt = _dbHelper.GetDataTableWithAuditTrial(qry, "Distributor Stock Consumption");
                int count = 0;
                var item = (from DataRow row in dt.Rows
                            select new DistStockConsumptionBEL
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
                                DistributorCode = row["CUSTOMER_CODE"].ToString(),
                                DistributorName = row["CUSTOMER_NAME"].ToString(),
                                DistributorAdd = row["ADDRESS"].ToString(),
                                ProductCode = row["PRODUCT_CODE"].ToString(),
                                ProductName = row["PRODUCT_NAME"].ToString(),
                                PackSize = row["PACK_SIZE"].ToString(),
                                ProductPrice = Convert.ToDouble(row["UNIT_TP"]),
                                OpeningQty = Convert.ToInt32(row["OPENING_STOCK_QTY"]),
                                ReceiveQty = Convert.ToInt32(row["RECEIVE_QTY"]),
                                ReplaceRcvQty = Convert.ToInt32(row["REPLACE_RECEIVE_QTY"]),
                                PreviousReturnReceiveQty = Convert.ToInt32(row["PREVIOUS_RETURN_RECEIVE_QTY"]),
                                ReturnReceiveQty = Convert.ToInt32(row["RETURN_RECEIVE_QTY"]),
                                PrevReplaceRetReceiveQty = Convert.ToInt32(row["PREV_REPLACE_RET_RECEIVE_QTY"]),
                                ReplaceRetReceiveQty = Convert.ToInt32(row["REPLACE_RET_RECEIVE_QTY"]),
                                GainQty = Convert.ToInt32(row["GAIN_QTY"]),
                                TotalInQty = Convert.ToInt32(row["TOTAL_IN_QTY"]),
                                IssuedQty = Convert.ToInt32(row["ISSUED_QTY"]),
                                ReplaceIssueQty = Convert.ToInt32(row["REPLACE_ISSUED_QTY"]),   
                                DispatchQty = Convert.ToInt32(row["DISPATCH_QTY"]),
                                BonusQty = Convert.ToInt32(row["BONUS_QTY"]),
                                TradeBonusQty = Convert.ToInt32(row["TRADE_BONUS_QTY"]),
                                ComboBonusQty = Convert.ToInt32(row["COMBO_BONUS_QTY"]),
                                LossQty = Convert.ToInt32(row["LOSS_QTY"]),
                                RequiReturnQty = Convert.ToInt32(row["REQUI_RETURN_QTY"]),
                                DamageStockTransferQty = Convert.ToInt32(row["DAMAGE_STOCK_TRANSFER_QTY"]),
                                TotalOutQty = Convert.ToInt32(row["TOTAL_OUT_QTY"]),
                                ClosingQty = Convert.ToInt32(row["CLOSING_QTY"]),
                                ClosingValue = Convert.ToDouble(row["CLOSING_VALUE"]),
                                TargetQty = Convert.ToInt32(row["CUST_PROD_WISE_TARGET_QTY"]),
                                TargetVal = Convert.ToDouble(row["CUST_PROD_WISE_TARGET_VALUE"])
                            }).ToList();
                return item;
            }
            catch (Exception e)
            {
                var lineNum = e.StackTrace.Substring(e.StackTrace.LastIndexOf(' '));
                _errorLogger.GetErrorMessage(e.Message, "DistStockConsumptionDAL", lineNum);
                ExceptionReturn = e.Message;
                throw;
            }
        }




    }
}