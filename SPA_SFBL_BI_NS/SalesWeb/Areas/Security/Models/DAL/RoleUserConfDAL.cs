using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using SalesWeb.Areas.Security.Models.BEL;
using SalesWeb.Universal.Gateway;

namespace SalesWeb.Areas.Security.Models.DAL
{
    // ReSharper disable once InconsistentNaming
    public class RoleUserConfDAL : ReturnData
    {
        private readonly DBHelper _dbHelper = new DBHelper();
        private readonly ErrorLogger _errorLogger = new ErrorLogger();
        public bool InsertRoleUserConf(RoleUserConfigureBEL master)
        {
            try
            {
                IuMode = "I";
                MaxID = _dbHelper.GetMaxSl("SC_ROLE_USER_CONF", "ID");
                string qry = "Insert into SC_ROLE_USER_CONF (ID, ROLE_ID, USER_ID) Values('" + MaxID + "', " + master.RoleId + ", " + master.UserId + ")";
                return _dbHelper.CmdExecute(qry, IuMode, "RoleUserConf", "SC_ROLE_USER_CONF", MaxID);
            }
            catch (Exception e)
            {
                var lineNum = e.StackTrace.Substring(e.StackTrace.LastIndexOf(' '));
                _errorLogger.GetErrorMessage(e.Message, "RoleUserConfDAL", lineNum);
                ExceptionReturn = e.Message;
                return false;
            }

        }
        public bool UpdateRoleUserConf(RoleUserConfigureBEL master)
        {
            try
            {
                MaxID = master.Id;
                string qry = "Update SC_ROLE_USER_CONF set ROLE_ID=" + master.RoleId + ", USER_ID=" + master.UserId + "";
                return _dbHelper.CmdExecute(qry, IuMode, "RoleUserConf", "SC_ROLE_USER_CONF", MaxID);
            }
            catch (Exception e)
            {
                var lineNum = e.StackTrace.Substring(e.StackTrace.LastIndexOf(' '));
                _errorLogger.GetErrorMessage(e.Message, "RoleUserConfDAL", lineNum);
                ExceptionReturn = e.Message;
                return false;
            }

        }

        public List<RoleInfo> GetRoleList()
        {
            try
            {
                string Qry = "SELECT ROLE_ID,ROLE_NAME FROM SC_ROLE_INFO ";
                //string Qry = "SELECT  DOCTOR_CODE, DOCTOR_NAME, PRACTICING_DAY, PRESCRIPTION_PER_DAY,HONORARIUM_AMOUNT, TERRITORY_CODE_4P, TO_CHAR(SET_DATE,'DD-MM-YYYY') SET_DATE FROM VW_FSM_DOC_HONORARIUM " + queryParam+"";
                DataTable dt = _dbHelper.GetDataTable(Qry);
                //int count = 0;
                var item = (from DataRow row in dt.Rows
                            select new RoleInfo
                            {
                                //SL_NO = ++count,
                                RoleId = Convert.ToInt32(row["ROLE_ID"]),
                                RoleName = row["ROLE_NAME"].ToString()
                            }).ToList();
                return item;

            }
            catch (Exception e)
            {
                var lineNum = e.StackTrace.Substring(e.StackTrace.LastIndexOf(' '));
                _errorLogger.GetErrorMessage(e.Message, "RoleUserConfDAL", lineNum);
                ExceptionReturn = e.Message;
                throw;
            }
        }

        public List<RoleUserConfigureBEL> GetUserInfoList(string roleId)
        {
            try
            {
                string qry =
                    "SELECT A.EMPLOYEE_CODE,A.EMPLOYEE_NAME,B.USER_ID" +
                    " FROM SC_EMPLOYEE_INFO A" +
                    " INNER JOIN SC_USER_LOGIN B ON A.EMPLOYEE_ID=B.EMPLOYEE_ID" +
                    " WHERE B.USER_ID NOT IN(SELECT USER_ID FROM SC_ROLE_USER_CONF WHERE ROLE_ID =" +
                    Convert.ToInt32(roleId) + " ) ";
                DataTable dt = _dbHelper.GetDataTable(qry);
                var item = (from DataRow row in dt.Rows
                            select new RoleUserConfigureBEL
                            {
                                //SL_NO = ++count,
                                EmployeeCode = row["EMPLOYEE_CODE"].ToString(),
                                EmployeeName = row["EMPLOYEE_NAME"].ToString(),
                                UserId = Convert.ToInt32(row["USER_ID"])
                                
                            }).ToList();
                return item;
            }
            catch (Exception e)
            {
                var lineNum = e.StackTrace.Substring(e.StackTrace.LastIndexOf(' '));
                _errorLogger.GetErrorMessage(e.Message, "RoleUserConfDAL", lineNum);
                ExceptionReturn = e.Message;
                throw;
            }
        }


       

        public List<RoleUserConfigureBEL> GetRoleUserConfList(string param)
        {
            try
            {
                string qry = "SELECT A.ID,A.ROLE_ID,D.ROLE_NAME,A.USER_ID,C.EMPLOYEE_NAME,C.EMPLOYEE_CODE" +
                             " FROM SC_ROLE_USER_CONF A" +
                             " INNER JOIN SC_USER_LOGIN B ON A.USER_ID = B.USER_ID" +
                             " INNER JOIN SC_EMPLOYEE_INFO C ON B.EMPLOYEE_ID = C.EMPLOYEE_ID" +
                             " INNER JOIN SC_ROLE_INFO D ON A.ROLE_ID = D.ROLE_ID " +
                             " WHERE 1=1 " + param;
                DataTable dt = _dbHelper.GetDataTable(qry);
                int count = 0;
                var item = (from DataRow row in dt.Rows
                            select new RoleUserConfigureBEL
                            {
                                SlNo = ++count,
                                Id = Convert.ToInt32(row["ID"]),
                                UserId = Convert.ToInt32(row["USER_ID"]),
                                EmployeeCode = row["EMPLOYEE_CODE"].ToString(),
                                EmployeeName = row["EMPLOYEE_NAME"].ToString(),
                                RoleId = Convert.ToInt32(row["ROLE_ID"]),
                                RoleName = row["ROLE_NAME"].ToString()
                            }).ToList();
                return item;

            }
            catch (Exception e)
            {

                var lineNum = e.StackTrace.Substring(e.StackTrace.LastIndexOf(' '));
                _errorLogger.GetErrorMessage(e.Message, "RoleUserConfDAL", lineNum);
                ExceptionReturn = e.Message;
                throw;
            }
        }

        // ReSharper disable once IdentifierTypo
        // ReSharper disable once InconsistentNaming
        public bool DeletegridRUCRow(string id)
        {
            try
            {
                string qry = "DELETE FROM SC_ROLE_USER_CONF where ID='" + Convert.ToInt32(id) + "'";
                return _dbHelper.CmdExecute(qry, IuMode, "RoleUserConf", "SC_ROLE_USER_CONF", Convert.ToInt64(id));
            }
            catch (Exception e)
            {
                var lineNum = e.StackTrace.Substring(e.StackTrace.LastIndexOf(' '));
                _errorLogger.GetErrorMessage(e.Message, "RoleUserConfDAL", lineNum);
                ExceptionReturn = e.Message;
                throw;
            }
        }
    }
}