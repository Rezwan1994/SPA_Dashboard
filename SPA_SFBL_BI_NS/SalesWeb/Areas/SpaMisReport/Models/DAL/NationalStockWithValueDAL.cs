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
    public class NationalStockWithValueDAL :ReturnData
    {
        private readonly DBHelper _dbHelper = new DBHelper();
        private readonly ErrorLogger _errorLogger = new ErrorLogger();
        private static readonly DBConnection DBConnection = new DBConnection();
        public readonly string ConnString = DBConnection.SAConnStrReader("Oracle", "PHARMAERP");

        public List<NationalStockWithValueBEL> GetNationalStockWithValue()

        {
            try
            {

                string qry;

                int userId = Convert.ToInt32(HttpContext.Current.Session["USER_ID"]);
                int UserAccessCount = Convert.ToInt32(HttpContext.Current.Session["USER_BASE_REPORT_FILTER"]);

                if (UserAccessCount > 0)
                {

                    qry = " SELECT PRODUCT_CODE, PRODUCT_NAME, PACK_SIZE, UNIT_TP, STOCK_QTY, STOCK_VALUE " +
                          " FROM VW_NATIONAL_STOCK " +
                          " WHERE PRODUCT_CODE IN (SELECT PRODUCT_CODE FROM USER_PRODUCT_DTL WHERE USER_ID=" + userId + ")" +
                          " ORDER BY PRODUCT_CODE, PRODUCT_NAME, PACK_SIZE, UNIT_TP, STOCK_QTY";

                }
                else
                {

                    qry = " SELECT PRODUCT_CODE, PRODUCT_NAME, PACK_SIZE, UNIT_TP, STOCK_QTY, STOCK_VALUE " +
                          " FROM VW_NATIONAL_STOCK " +
                          " ORDER BY PRODUCT_CODE, PRODUCT_NAME, PACK_SIZE, UNIT_TP, STOCK_QTY";

                }






               //DataTable dt = _dbHelper.GetDataTable(qry);
               DataTable dt = _dbHelper.GetDataTableWithAuditTrial(qry, "National Stock Report");

                int count = 0;
        var item = (from DataRow row in dt.Rows
                    select new NationalStockWithValueBEL
                    {
                        SlNo = ++count,
                        ProductCode = row["PRODUCT_CODE"].ToString(),
                        ProductName = row["PRODUCT_NAME"].ToString(),
                        PackSize = row["PACK_SIZE"].ToString(),
                        UnitTp = row["UNIT_TP"].ToString(),
                        StockQty = Convert.ToInt32(row["STOCK_QTY"]),
                        StockValue = Convert.ToInt32(row["STOCK_VALUE"])



                    }).ToList();
                return item;
            }
            catch (Exception e)
            {
                var lineNum = e.StackTrace.Substring(e.StackTrace.LastIndexOf(' '));
                _errorLogger.GetErrorMessage(e.Message, "NationalStockWithValueDAL", lineNum);
                ExceptionReturn = e.Message;
                throw;
            }
        }



    }
}