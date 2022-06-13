using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using SalesWeb.Areas.Security.Models.BEL;
using SalesWeb.Universal.Gateway;

namespace SalesWeb.Areas.Security.Models.DAL
{
    public class RoleInfoDAL : ReturnData
    {
        private readonly DBHelper _dbHelper = new DBHelper();
        private readonly IDGenerated _idGenerated = new IDGenerated();
        private readonly ErrorLogger _errorLogger = new ErrorLogger();
        //private readonly AuditTrailDAO _adt = new AuditTrailDAO();
        public bool InsertRoleInfo(RoleInfo master)
        {
            try
            {
                MaxID = _dbHelper.GetMaxSl("SC_ROLE_INFO", "ROLE_ID");
                IuMode = "I";
                string qry = "Insert into SC_ROLE_INFO(ROLE_ID, ROLE_NAME,STATUS) Values('" + MaxID + "', '" + master.RoleName + "', '" + master.Status + "')";
                return _dbHelper.CmdExecute(qry, IuMode, "RoleInfo", "SC_ROLE_INFO", MaxID);
            }
            catch (Exception e)
            {
                var lineNum = e.StackTrace.Substring(e.StackTrace.LastIndexOf(' '));
                _errorLogger.GetErrorMessage(e.Message, "RoleInfoDAL", lineNum);
                ExceptionReturn = e.Message;
                return false;
            }

        }
        public bool UpdateRoleInfo(RoleInfo master)
        {
            try
            {
                MaxID = master.RoleId;
                IuMode = "U";
                string qry = "Update SC_ROLE_INFO set ROLE_NAME='" + master.RoleName + "',STATUS='" + master.Status + "' Where ROLE_ID=" + master.RoleId + "" ;
                return _dbHelper.CmdExecute(qry, IuMode, "RoleInfo", "SC_ROLE_INFO", MaxID);
            }
            catch (Exception e)
            {
               
              var lineNum = e.StackTrace.Substring(e.StackTrace.LastIndexOf(' '));
                _errorLogger.GetErrorMessage(e.Message, "RoleInfoDAL", lineNum);
                ExceptionReturn = e.Message;
                return false;
            }

        }

        public List<RoleInfo> GetRoleList()
        {
            try
            {
                string Qry = "SELECT ROLE_ID,ROLE_NAME,STATUS FROM SC_ROLE_INFO ORDER BY ROLE_ID";
                DataTable dt = _dbHelper.GetDataTable(Qry);
                var item = (from DataRow row in dt.Rows
                    select new RoleInfo
                    {
                        RoleId = Convert.ToInt32(row["ROLE_ID"]),
                        RoleName = row["ROLE_NAME"].ToString(),
                        Status = row["STATUS"].ToString()
                    }).ToList();
                return item;

            }
            catch (Exception e)
            {
                var lineNum = e.StackTrace.Substring(e.StackTrace.LastIndexOf(' '));
                _errorLogger.GetErrorMessage(e.Message, "RoleInfoDAL", lineNum);
                ExceptionReturn = e.Message;
                throw;
            }
        }
    }
}