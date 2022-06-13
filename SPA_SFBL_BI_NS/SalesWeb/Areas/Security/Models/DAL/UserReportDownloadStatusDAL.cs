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
    public class UserReportDownloadStatusDAL : ReturnData
    {

        private readonly DBHelper _dbHelper = new DBHelper();
        private readonly ErrorLogger _errorLogger = new ErrorLogger();
        private static readonly DBConnection DBConnection = new DBConnection();
        public readonly string ConnString = DBConnection.SAConnStrReader("Oracle", "PHARMAERP");

        public List<UserReportDownloadStatusBEL> GetUserReportList(string userId )
        {
            try
            {
                string qry = " SELECT USER_ID, MENU_ID, MENU_NAME, MENU_DISPLAY_NAME, LTRIM(RTRIM(DOWNLOAD_STATUS)) DOWNLOAD_STATUS" +
                              " FROM VW_USER_ROLE_MENU_DTL " +
                              " WHERE USER_ID="+ userId+
                              " ORDER BY MENU_NAME";


                DataTable dt = _dbHelper.GetDataTable(qry);
                var item = (from DataRow row in dt.Rows
                            select new UserReportDownloadStatusBEL
                            {
                                UserId = row["USER_ID"].ToString(),
                                MenuId = row["MENU_ID"].ToString(),
                                ReportName = row["MENU_NAME"].ToString(),
                                ReportDisplayName = row["MENU_DISPLAY_NAME"].ToString(),
                                DownloadStatus = row["DOWNLOAD_STATUS"].ToString()
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



        public bool InsertUpdateReportDownloadStatus(string pUserId, string pMenuId, string pReportName, string pDownloadStatus)
        {
            bool isTrue = false;

            try
            {
                using (OracleConnection con = new OracleConnection(ConnString))
                {
                    using (OracleCommand cmd = new OracleCommand())
                    {
                        cmd.Connection = con;
                        cmd.CommandText = "Prc_ReportDownloadStatus";
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.Add("pUserId", OracleType.VarChar).Value = pUserId;
                        cmd.Parameters.Add("pReportName", OracleType.VarChar).Value = pReportName;
                        cmd.Parameters.Add("pDownloadStatus", OracleType.VarChar).Value = pDownloadStatus;
                        cmd.Parameters.Add("pMenuId", OracleType.VarChar).Value = pMenuId;
                        con.Open();
                        int cont = cmd.ExecuteNonQuery();

                        if (cont > 0)
                        {
                            return true;

                        }
                    }

                }

                return isTrue;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }




    }
}