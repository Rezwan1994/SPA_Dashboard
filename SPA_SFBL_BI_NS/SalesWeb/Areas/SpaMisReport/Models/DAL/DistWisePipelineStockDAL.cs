
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
    public class DistWisePipelineStockDAL : ReturnData
    {

        private readonly DBHelper _dbHelper = new DBHelper();
        private readonly ErrorLogger _errorLogger = new ErrorLogger();
        private static readonly DBConnection DBConnection = new DBConnection();
        public readonly string ConnString = DBConnection.SAConnStrReader("Oracle", "PHARMAERP");


        public List<DistWisePipelineStockBEL> GetDistWisePipelineStock(string dCode, string rCode, string aCode, string tCode, string cCode)

        {
            try
            {

                string qry;

                int userId = Convert.ToInt32(HttpContext.Current.Session["USER_ID"]);
                int UserAccessCount = Convert.ToInt32(HttpContext.Current.Session["USER_BASE_REPORT_FILTER"]);

                if (UserAccessCount > 0)
                {


                    qry = " SELECT DIVISION_CODE, DIVISION_NAME, REGION_CODE, REGION_NAME, AREA_CODE, AREA_NAME, TERRITORY_CODE, TERRITORY_NAME, DB_LOCATION, CUSTOMER_CODE, CUSTOMER_NAME, INVOICE_NO,TO_CHAR(INVOICE_DATE,'MM/DD/RRRR')INVOICE_DATE, REQUISITION_NO, TO_CHAR(REQUISITION_DATE,'MM/DD/RRRR')REQUISITION_DATE,PRODUCT_CODE, PRODUCT_NAME, PACK_SIZE, TRADE_PRICE, STOCK_QTY, PIPELINE_QTY " +
                          " FROM MV_DIST_WISE_PIPELINE_STK " +
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


                    qry = " SELECT DIVISION_CODE, DIVISION_NAME, REGION_CODE, REGION_NAME, AREA_CODE, AREA_NAME, TERRITORY_CODE, TERRITORY_NAME, DB_LOCATION, CUSTOMER_CODE, CUSTOMER_NAME, INVOICE_NO,TO_CHAR(INVOICE_DATE,'MM/DD/RRRR')INVOICE_DATE, REQUISITION_NO, TO_CHAR(REQUISITION_DATE,'MM/DD/RRRR')REQUISITION_DATE,PRODUCT_CODE, PRODUCT_NAME, PACK_SIZE, TRADE_PRICE, STOCK_QTY, PIPELINE_QTY " +
                          " FROM MV_DIST_WISE_PIPELINE_STK " +
                          " WHERE (DIVISION_CODE = '" + dCode + "' OR '" + dCode + "' = 'ALL')" +
                          " AND   (REGION_CODE='" + rCode + "' OR '" + rCode + "'='ALL')" +
                          " AND   (AREA_CODE='" + aCode + "' OR '" + aCode + "'='ALL')" +
                          " AND   (TERRITORY_CODE='" + tCode + "' OR '" + tCode + "'='ALL')" +
                          " AND   (CUSTOMER_CODE='" + cCode + "' OR '" + cCode + "'='ALL')" +
                          " ORDER BY DIVISION_CODE,REGION_CODE, AREA_CODE, TERRITORY_CODE, CUSTOMER_CODE, PRODUCT_CODE";

                }




                //DataTable dt = _dbHelper.GetDataTable(qry);
                DataTable dt = _dbHelper.GetDataTableWithAuditTrial(qry, "Distributor Wise Pipeline Stock");
                int count = 0;
                var item = (from DataRow row in dt.Rows
                            select new DistWisePipelineStockBEL
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
                                InvoiceNo = row["INVOICE_NO"].ToString(),
                                InvoiceDate = row["INVOICE_DATE"].ToString(),
                                RequisitionNo = row["REQUISITION_NO"].ToString(),
                                RequisitionDate = row["REQUISITION_DATE"].ToString(),
                                ProductCode = row["PRODUCT_CODE"].ToString(),
                                ProductName = row["PRODUCT_NAME"].ToString(),
                                PackSize = row["PACK_SIZE"].ToString(),
                                ProductPrice = Convert.ToDouble(row["TRADE_PRICE"]),
                                StockQty = Convert.ToInt32(row["STOCK_QTY"]),
                                PipelineQty = Convert.ToInt32(row["PIPELINE_QTY"]),



                            }).ToList();
                return item;
            }
            catch (Exception e)
            {
                var lineNum = e.StackTrace.Substring(e.StackTrace.LastIndexOf(' '));
                _errorLogger.GetErrorMessage(e.Message, "DistWisePipelineStockDAL", lineNum);
                ExceptionReturn = e.Message;
                throw;
            }
        }




    }
}

