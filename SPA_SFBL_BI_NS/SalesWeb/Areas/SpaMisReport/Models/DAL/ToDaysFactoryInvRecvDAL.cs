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
    public class ToDaysFactoryInvRecvDAL : ReturnData
    {

        private readonly DBHelper _dbHelper = new DBHelper();
        private readonly ErrorLogger _errorLogger = new ErrorLogger();
        private static readonly DBConnection DBConnection = new DBConnection();
        public readonly string ConnString = DBConnection.SAConnStrReader("Oracle", "PHARMAERP");

        public List<ToDaysFactoryInvRecvBEL> GetToDaysFactoryInvRecvCustomDate(string fDate, string tDate, string dCode, string rCode, string aCode, string tCode, string cCode)

        {
            try
            {
                string qry = " SELECT DIVISION_CODE, DIVISION_NAME, REGION_CODE, REGION_NAME, AREA_CODE, AREA_NAME, TERRITORY_CODE, TERRITORY_NAME, CUSTOMER_CODE, CUSTOMER_NAME, DB_LOCATION, REQUISITION_NO, RECEIVE_DATE, INVOICE_NO, INVOICE_DATE, INV_TYPE_CODE, INV_TYPE_NAME, PRODUCT_CODE, PRODUCT_NAME, PACK_SIZE, PRODUCT_PRICE, PRODUCT_BATCH_NO, LOT_NO, RECEIVE_QTY " +
                              " FROM MV_TO_DAYS_FACTORY_INV_RECV  " +
                              " WHERE TO_DATE(RECEIVE_DATE,'DD/MM/RRRR') BETWEEN TO_DATE('" + fDate+ "','DD/MM/RRRR') AND TO_DATE('" +tDate+ "','DD/MM/RRRR')"+                                    
                              " AND (DIVISION_CODE = '" + dCode + "' OR '" + dCode + "' = 'ALL')" +
                              " AND   (REGION_CODE='" + rCode + "' OR '" + rCode + "'='ALL')" +
                              " AND   (AREA_CODE='" + aCode + "' OR '" + aCode + "'='ALL')" +
                              " AND   (TERRITORY_CODE='" + tCode + "' OR '" + tCode + "'='ALL')" +
                              " AND   (CUSTOMER_CODE='" + cCode + "' OR '" + cCode + "'='ALL')" +
                              " ORDER BY DIVISION_CODE,REGION_CODE, AREA_CODE, TERRITORY_CODE, CUSTOMER_CODE, PRODUCT_CODE";


                //DataTable dt = _dbHelper.GetDataTable(qry);
                DataTable dt = _dbHelper.GetDataTableWithAuditTrial(qry, "Today Factory Invoice Receive");
                int count = 0;
                var item = (from DataRow row in dt.Rows
                            select new ToDaysFactoryInvRecvBEL
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
                                RequisitionNo = row["REQUISITION_NO"].ToString(),
                                ReceiveDate = row["RECEIVE_DATE"].ToString(),
                                InvoiceNo = row["INVOICE_NO"].ToString(),
                                InvoiceDate = row["INVOICE_DATE"].ToString(),
                                InvTypeCode = row["INV_TYPE_CODE"].ToString(),
                                InvTypeName = row["INV_TYPE_NAME"].ToString(),
                                ProductCode = row["PRODUCT_CODE"].ToString(),
                                ProductName = row["PRODUCT_NAME"].ToString(),
                                PackSize = row["PACK_SIZE"].ToString(),
                                ProductPrice = row["PRODUCT_PRICE"].ToString(),
                                ProductBatchNo = row["PRODUCT_BATCH_NO"].ToString(),
                                LotNo = Convert.ToInt32(row["LOT_NO"]),
                                ReceiveQty = Convert.ToInt32(row["RECEIVE_QTY"])



                            }).ToList();
                return item;
            }
            catch (Exception e)
            {
                var lineNum = e.StackTrace.Substring(e.StackTrace.LastIndexOf(' '));
                _errorLogger.GetErrorMessage(e.Message, "ToDaysFactoryInvRecvDAL", lineNum);
                ExceptionReturn = e.Message;
                throw;
            }
        }




    }
}