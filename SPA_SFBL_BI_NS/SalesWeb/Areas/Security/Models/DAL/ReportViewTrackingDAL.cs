using System;
using System.Data;
using System.Linq;
using System.Web;
using SalesWeb.Areas.Security.Models.BEL;
using SalesWeb.Universal.Gateway;
using System.Collections.Generic;
using System.Data.OracleClient;

namespace SalesWeb.Areas.Security.Models.DAL
{
    public class ReportViewTrackingDAL : ReturnData
    {

        private readonly DBHelper _dbHelper = new DBHelper();
        private readonly ErrorLogger _errorLogger = new ErrorLogger();
        private static readonly DBConnection DBConnection = new DBConnection();
        public readonly string ConnString = DBConnection.SAConnStrReader("Oracle", "PHARMAERP");

        public List<ReportViewTrackingBEL> GetUserViewReportList(string userId, string fDate, string tDate)
        {
            try
            {
                string qry = " SELECT " +
                             " SLNO," +
                             " USER_ID," +
                             " USER_NAME," +
                             " EMP_ID," +
                             " EMP_NAME," +
                             " REPORT_NAME," +
                             " TO_CHAR(VIEW_DATE,'MM/DD/RRRR HH:MI:SS AM') VIEW_DATE," +
                             " VIEW_TERMINAL," +
                             " VIEW_IP" +
                             " FROM VW_REPORT_ACCESS " +
                             " WHERE (USER_ID='" + userId + "' OR '" + userId + "'='ALL')" +
                             " AND TO_DATE(VIEW_DATE,'DD/MM/RRRR') BETWEEN TO_DATE('" + fDate+ "','DD/MM/RRRR') AND TO_DATE('"+ tDate+"','DD/MM/RRRR')"+
                             " ORDER BY USER_NAME, SLNO";
                

                DataTable dt = _dbHelper.GetDataTable(qry);
                int count = 0;
                var item = (from DataRow row in dt.Rows
                            select new ReportViewTrackingBEL
                            {
                                SlNo = ++count,
                                UserId = row["USER_ID"].ToString(),
                                UserName = row["USER_NAME"].ToString(),

                                EmployeeId = row["EMP_ID"].ToString(),
                                EmployeeName = row["EMP_NAME"].ToString(),

                                ReportName = row["REPORT_NAME"].ToString(),

                                ReportViewDate = row["VIEW_DATE"].ToString(),
                                ReportViewTerminal = row["VIEW_TERMINAL"].ToString(),
                                ReportViewIp = row["VIEW_IP"].ToString()

                            }).ToList();
                return item;
            }
            catch (Exception e)
            {
                var lineNum = e.StackTrace.Substring(e.StackTrace.LastIndexOf(' '));
                _errorLogger.GetErrorMessage(e.Message, "UserReportDownloadStatusDAL", lineNum);
                ExceptionReturn = e.Message;
                throw;
            }
        }



        public object GetUserList()
        {
            try
            {
                var qry = " select 'ALL' user_id, 'ALL' user_name, 'ALL' employee_id,  'ALL' employee_code, 'ALL' employee_name, 1 SL FROM DUAL UNION " +
                          " select     to_char(a.user_id) user_id,     a.user_name,     to_char(a.employee_id) employee_id,      b.employee_code,     b.employee_name, 2 SL" +
                          " from sc_user_login a, sc_employee_info b" +
                          " where a.employee_id=b.employee_id" +
                          " order by SL";

                DataTable dt = _dbHelper.GetDataTable(qry);
                var item = (from DataRow row in dt.Rows
                            select new UserBEL2
                            {
                                UserId = row["user_id"].ToString(),
                                UserName = row["user_name"].ToString(),
                                EmployeeId = row["employee_id"].ToString(),
                                EmployeeCode = row["employee_code"].ToString(),
                                EmployeeName = row["employee_name"].ToString()

                            }).ToList();
                return item;

            }
            catch (Exception e)
            {
                var lineNum = e.StackTrace.Substring(e.StackTrace.LastIndexOf(' '));
                _errorLogger.GetErrorMessage(e.Message, "UserBaseReportAccessDAL", lineNum);
                ExceptionReturn = e.Message;
                return "Not Ok";
            }
        }

    }
}