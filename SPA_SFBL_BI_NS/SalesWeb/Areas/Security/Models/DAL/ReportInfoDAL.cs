using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using SalesWeb.Areas.Security.Models.BEL;
using SalesWeb.Universal.Gateway;

namespace SalesWeb.Areas.Security.Models.DAL
{
    //
    public class ReportInfoDAL : ReturnData
    {
        private readonly DBHelper _dbHelper = new DBHelper();
        private readonly ErrorLogger _errorLogger = new ErrorLogger();
        

        public bool InsertReportInfo(ReportInfoBEL master)
        {
            try
            {
                MaxID = _dbHelper.GetMaxSl("SC_REPORT_INFO", "REPORT_ID");
                IuMode = "I";
                string qry = "Insert into SC_REPORT_INFO(REPORT_ID,REPORT_NAME,DISPLAY_NAME,FORM_NAME,FORM_URL) Values ('" + MaxID + "', " +
                             " '" + master.ReportName + "', '" + master.DisplayName + "', '" + master.FormName + "', '" + master.FormUrl + "')";
                return _dbHelper.CmdExecute(qry, IuMode, "ReportInfo", "SC_REPORT_INFO", MaxID);
            }
            catch (Exception e)
            {
                var lineNum = e.StackTrace.Substring(e.StackTrace.LastIndexOf(' '));
                _errorLogger.GetErrorMessage(e.Message, "ReportInfoDAL", lineNum);
                ExceptionReturn = e.Message;
                return false;
            }
        }
        public bool UpdateReportInfo(ReportInfoBEL master)
        {
            try
            {
                MaxID = master.ReportId;
                IuMode = "U";
                string qry = "Update SC_REPORT_INFO set REPORT_NAME='" + master.ReportName + "'" +
                             " ,DISPLAY_NAME='" + master.DisplayName + "',FORM_NAME='" + master.FormName + "',FORM_URL='" + master.FormUrl + "' Where REPORT_ID='" + master.ReportId + "'";
                return _dbHelper.CmdExecute(qry, IuMode, "ReportInfo", "SC_REPORT_INFO", MaxID);
            }
            catch (Exception e)
            {
                var lineNum = e.StackTrace.Substring(e.StackTrace.LastIndexOf(' '));
                _errorLogger.GetErrorMessage(e.Message, "ReportInfoDAL", lineNum);
                ExceptionReturn = e.Message;
                return false;
            }

        }
        public List<ReportInfoBEL> GetReportList()
        {
            try
            {
                const string qry = "SELECT REPORT_ID, REPORT_NAME,DISPLAY_NAME,FORM_NAME,FORM_URL FROM SC_REPORT_INFO Order by REPORT_NAME";
                DataTable dt = _dbHelper.GetDataTable(qry);
                var item = (from DataRow row in dt.Rows
                    select new ReportInfoBEL
                    {
                        ReportId = Convert.ToInt64(row["REPORT_ID"]),
                        ReportName = row["REPORT_NAME"].ToString(),
                        DisplayName = row["DISPLAY_NAME"].ToString(),
                        FormName = row["FORM_NAME"].ToString(),
                        FormUrl = row["FORM_URL"].ToString()
                    }).ToList();
                return item;
            }
            catch (Exception e)
            {
                var lineNum = e.StackTrace.Substring(e.StackTrace.LastIndexOf(' '));
                _errorLogger.GetErrorMessage(e.Message, "ReportInfoDAL", lineNum);
                ExceptionReturn = e.Message;
                throw;
            }
        }
        public object GetFormNameList()
        {
            string qry = "SELECT B.MENU_ID,B.MENU_NAME,B.MENU_DISPLAY_NAME,A.URL FROM SC_MENU_CONF A INNER JOIN SC_MENU_INFO B ON A.CHILD_ID = B.MENU_ID " +
                         " WHERE A.URL IS NOT NULL ORDER BY B.MENU_NAME";
            DataTable dt = _dbHelper.GetDataTable(qry);
            var item = (from DataRow row in dt.Rows
                select new ReportInfoBEL
                {

                    FormName = row["MENU_NAME"].ToString(),
                    FormUrl = row["URL"].ToString()
                }).ToList();
            return item;

        }
    }
}