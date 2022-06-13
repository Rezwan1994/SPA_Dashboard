using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using SalesWeb.Areas.Security.Models.BEL;
using SalesWeb.Universal.Gateway;

namespace SalesWeb.Areas.Security.Models.DAL
{
    // ReSharper disable once InconsistentNaming
    public class RoleReportConfDAL : ReturnData
    {
        private readonly DBHelper _dbHelper = new DBHelper();
        private readonly ErrorLogger _errorLogger = new ErrorLogger();
        public bool InsertRoleReportConf(ReportConfigureBEL master)
        {
            try
            {
                MaxID = _dbHelper.GetMaxSl("SC_ROLE_REPORT_CONF", "ID");
                IuMode = "I";
                var qry = "Insert into SC_ROLE_REPORT_CONF (ID, REPORT_ID, ROLE_ID) Values('" + MaxID + "', " + master.ReportId + ", " + master.RoleId + ")";
                return _dbHelper.CmdExecute(qry, IuMode, "RoleReportConf", "SC_ROLE_REPORT_CONF", MaxID);
            }
            catch (Exception e)
            {
                var lineNum = e.StackTrace.Substring(e.StackTrace.LastIndexOf(' '));
                _errorLogger.GetErrorMessage(e.Message, "RoleReportConfDAL", lineNum);
                ExceptionReturn = e.Message;
                return false;
            }

        }
        public bool UpdateRoleReportConf(ReportConfigureBEL master)
        {
            try
            {
                MaxID = master.Id;
                IuMode = "U";
                var qry = "Update SC_ROLE_REPORT_CONF set REPORT_ID='" + master.ReportId + "', ROLE_ID='" + master.RoleId + "' Where ID=" + master.Id + "";
                return _dbHelper.CmdExecute(qry, IuMode, "RoleReportConf", "SC_ROLE_REPORT_CONF", MaxID);
            }
            catch (Exception e)
            {
                var lineNum = e.StackTrace.Substring(e.StackTrace.LastIndexOf(' '));
                _errorLogger.GetErrorMessage(e.Message, "RoleReportConfDAL", lineNum);
                ExceptionReturn = e.Message;
                return false;
            }

        }
        public List<ReportConfigureBEL> GetRoleReportConfList(string param)
        {
            try
            {
                var qry = "SELECT A.ID,B.REPORT_ID,B.REPORT_NAME,C.ROLE_ID,C.ROLE_NAME FROM SC_ROLE_REPORT_CONF A INNER JOIN SC_REPORT_INFO B ON A.REPORT_ID = B.REPORT_ID " +
                          " INNER JOIN SC_ROLE_INFO C ON A.ROLE_ID = C.ROLE_ID WHERE 1=1 " + param;
                DataTable dt = _dbHelper.GetDataTable(qry);
                var item = (from DataRow row in dt.Rows
                            select new ReportConfigureBEL
                            {

                                Id = Convert.ToInt32(row["ID"]),
                                RoleId = Convert.ToInt32(row["ROLE_ID"]),
                                ReportId = Convert.ToInt32(row["REPORT_ID"]),
                                ReportName = row["REPORT_NAME"].ToString(),
                                RoleName = row["ROLE_NAME"].ToString()

                            }).ToList();
                return item;

            }
            catch (Exception e)
            {
                var lineNum = e.StackTrace.Substring(e.StackTrace.LastIndexOf(' '));
                _errorLogger.GetErrorMessage(e.Message, "RoleReportConfDAL", lineNum);
                ExceptionReturn = e.Message;
                throw;
            }
        }
        public bool DeleteGridRMCRow(string id)
        {
            try
            {
                IuMode = "D";
                string qry = "DELETE FROM SC_ROLE_REPORT_CONF where ID='" + Convert.ToInt32(id) + "'";
                return _dbHelper.CmdExecute(qry, IuMode, "RoleReportConf", "SC_ROLE_REPORT_CONF", Convert.ToInt64(id));
            }
            catch (Exception e)
            {
                var lineNum = e.StackTrace.Substring(e.StackTrace.LastIndexOf(' '));
                _errorLogger.GetErrorMessage(e.Message, "RoleReportConfDAL", lineNum);
                ExceptionReturn = e.Message;
                throw;
            }
        }

        public object GetReportListByRole(string param)
        {

            try
            {
                string qry = "SELECT REPORT_ID,REPORT_NAME FROM SC_REPORT_INFO WHERE REPORT_ID NOT IN (SELECT REPORT_ID FROM SC_ROLE_REPORT_CONF WHERE ROLE_ID=" + param + ") ";
                DataTable dt = _dbHelper.GetDataTable(qry);
                var item = (from DataRow row in dt.Rows
                            select new ReportInfoBEL
                            {
                                //SL_NO = ++count,
                                ReportId = Convert.ToInt32(row["REPORT_ID"]),
                                ReportName = row["REPORT_NAME"].ToString()
                            }).ToList();
                return item;

            }
            catch (Exception e)
            {
                var lineNum = e.StackTrace.Substring(e.StackTrace.LastIndexOf(' '));
                _errorLogger.GetErrorMessage(e.Message, "RoleReportConfDAL", lineNum);
                ExceptionReturn = e.Message;
                throw;
            }
        }
    }
}