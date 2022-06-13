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
    public class FsoWiseFirstAndLastInvoiceDAL : ReturnData
    {
        private readonly DBHelper _dbHelper = new DBHelper();
        private readonly ErrorLogger _errorLogger = new ErrorLogger();
        private static readonly DBConnection DBConnection = new DBConnection();
        public readonly string ConnString = DBConnection.SAConnStrReader("Oracle", "PHARMAERP");

        public List<FsoWiseFirstAndLastInvoiceBEL> GetFsoWiseFirstAndLastInvoiceRel()

        {
            try
            {
                int userId = Convert.ToInt32(HttpContext.Current.Session["USER_ID"]);
                int UserAccessCount = Convert.ToInt32(HttpContext.Current.Session["USER_BASE_REPORT_FILTER"]);

                string qry = " SELECT CUSTOMER_CODE, CUSTOMER_NAME, DB_LOCATION, MARKET_CODE, MARKET_NAME, EMPLOYEE_CODE, EMPLOYEE_NAME, JOINING_DATE, MOBILE_NO, FIRST_INVOICE_NO, FIRST_INVOICE_DATE, LAST_INVOICE_NO, LAST_INVOICE_DATE, STATUS " +
                             " FROM MV_FSO_WISE_FIRST_LAST_INVOICE ";

                DataTable dt = _dbHelper.GetDataTableWithAuditTrial(qry, "All FSO Wise First & Last Invoice");

                var item = (from DataRow row in dt.Rows
                            select new FsoWiseFirstAndLastInvoiceBEL
                            {
                                CustomerCode = row["CUSTOMER_CODE"].ToString(),
                                CustomerName = row["CUSTOMER_NAME"].ToString(),
                                DbLocation = row["DB_LOCATION"].ToString(),
                                MarketCode = row["MARKET_CODE"].ToString(),
                                MarketName = row["MARKET_NAME"].ToString(),
                                FsoCode = row["EMPLOYEE_CODE"].ToString(),
                                FsoName = row["EMPLOYEE_NAME"].ToString(),
                                JoiningDate = row["JOINING_DATE"].ToString(),
                                MobileNo = row["MOBILE_NO"].ToString(),
                                FirstInvoiceNo = row["FIRST_INVOICE_NO"].ToString(),
                                FirstInvoiceDate = row["FIRST_INVOICE_DATE"].ToString(),
                                LastInvoiceNo = row["LAST_INVOICE_NO"].ToString(),
                                LastInvoiceDate = row["LAST_INVOICE_DATE"].ToString(),
                                Status = row["STATUS"].ToString()
                            }).ToList();
                return item;
            }
            catch (Exception e)
            {
                var lineNum = e.StackTrace.Substring(e.StackTrace.LastIndexOf(' '));
                _errorLogger.GetErrorMessage(e.Message, "FsoWiseFirstAndLastInvoiceDAL", lineNum);
                ExceptionReturn = e.Message;
                throw;
            }
        }
        public List<FsoWiseFirstAndLastInvoiceBEL> GetFsoWiseFirstAndLastInvoiceAll()

        {
            try
            {
                int userId = Convert.ToInt32(HttpContext.Current.Session["USER_ID"]);
                int UserAccessCount = Convert.ToInt32(HttpContext.Current.Session["USER_BASE_REPORT_FILTER"]);

                string qry = " SELECT EMPLOYEE_CODE, EMPLOYEE_NAME, JOINING_DATE, MOBILE_NO, FIRST_INVOICE_NO, FIRST_INVOICE_DATE, LAST_INVOICE_NO, LAST_INVOICE_DATE, STATUS " +
                             " FROM MV_ALL_FSO_FIRST_LAST_INVOICE ";

                DataTable dt = _dbHelper.GetDataTableWithAuditTrial(qry, "All FSO Wise First & Last Invoice");

                var item = (from DataRow row in dt.Rows
                            select new FsoWiseFirstAndLastInvoiceBEL
                            {
                                FsoCode = row["EMPLOYEE_CODE"].ToString(),
                                FsoName = row["EMPLOYEE_NAME"].ToString(),
                                JoiningDate = row["JOINING_DATE"].ToString(),
                                MobileNo = row["MOBILE_NO"].ToString(),
                                FirstInvoiceNo = row["FIRST_INVOICE_NO"].ToString(),
                                FirstInvoiceDate = row["FIRST_INVOICE_DATE"].ToString(),
                                LastInvoiceNo = row["LAST_INVOICE_NO"].ToString(),
                                LastInvoiceDate = row["LAST_INVOICE_DATE"].ToString(),
                                Status = row["STATUS"].ToString()
                            }).ToList();
                return item;
            }
            catch (Exception e)
            {
                var lineNum = e.StackTrace.Substring(e.StackTrace.LastIndexOf(' '));
                _errorLogger.GetErrorMessage(e.Message, "FsoWiseFirstAndLastInvoiceDAL", lineNum);
                ExceptionReturn = e.Message;
                throw;
            }
        }

    }
}