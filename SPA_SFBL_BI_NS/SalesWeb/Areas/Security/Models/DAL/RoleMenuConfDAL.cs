using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using SalesWeb.Areas.Security.Models.BEL;
using SalesWeb.Universal.Gateway;

namespace SalesWeb.Areas.Security.Models.DAL
{
    // ReSharper disable once InconsistentNaming
    public class RoleMenuConfDAL : ReturnData
    {
        private readonly DBHelper _dbHelper = new DBHelper();
        private readonly ErrorLogger _errorLogger = new ErrorLogger();
        public bool InsertRoleMenuConf(RoleMenuConfigureBEL master)
        {
            try
            {
                MaxID = _dbHelper.GetMaxSl("SC_ROLE_MENU_CONF", "ID");
                IuMode = "I";
               var qry = "Insert into SC_ROLE_MENU_CONF (ID, MC_ID, ROLE_ID, SV, VW, DL) Values('" + MaxID + "', " + master.McId + ", " + master.RoleId + " " +
                      ", '" + master.SaveStatus + "', '" + master.ViewStatus + "', '" + master.DeleteStatus + "')";
                return _dbHelper.CmdExecute(qry, IuMode, "RoleMenuConf", "SC_ROLE_MENU_CONF", MaxID);
            }
            catch (Exception e)
            {
                var lineNum = e.StackTrace.Substring(e.StackTrace.LastIndexOf(' '));
                _errorLogger.GetErrorMessage(e.Message, "RoleMenuConfDAL", lineNum);
                ExceptionReturn = e.Message;
                return false;
            }

        }
        public bool UpdateRoleMenuConf(RoleMenuConfigureBEL master)
        {
            try
            {
                MaxID = master.Id;
                IuMode = "U";
                var qry = "Update SC_ROLE_MENU_CONF set MC_ID='" + master.McId + "', ROLE_ID='" + master.RoleId + "', SV='" + master.SaveStatus + "'," +
                      " VW='" + master.ViewStatus + "', DL='" + master.DeleteStatus + "' Where ID=" + master.Id + "";
                return _dbHelper.CmdExecute(qry, IuMode, "RoleMenuConf", "SC_ROLE_MENU_CONF", MaxID);
            }
            catch (Exception e)
            {
                var lineNum = e.StackTrace.Substring(e.StackTrace.LastIndexOf(' '));
                _errorLogger.GetErrorMessage(e.Message, "RoleMenuConfDAL", lineNum);
                ExceptionReturn = e.Message;
                return false;
            }

        }

        public List<RoleInfo> GetRoleList()
        {
            try
            {
                string qry = "SELECT ROLE_ID,ROLE_NAME FROM SC_ROLE_INFO ";
                //string Qry = "SELECT  DOCTOR_CODE, DOCTOR_NAME, PRACTICING_DAY, PRESCRIPTION_PER_DAY,HONORARIUM_AMOUNT, TERRITORY_CODE_4P, TO_CHAR(SET_DATE,'DD-MM-YYYY') SET_DATE FROM VW_FSM_DOC_HONORARIUM " + queryParam+"";
                DataTable dt = _dbHelper.GetDataTable(qry);
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
                _errorLogger.GetErrorMessage(e.Message, "RoleMenuConfDAL", lineNum);
                ExceptionReturn = e.Message;
                throw;
            }
        }

        public List<RoleMenuConfigureBEL> GetChildMenuList(string roleId)
        {
            try
            {
                //string Qry = "SELECT DISTINCT B.MENU_NAME,B.MENU_ID,A.URL,A.CHILD_SEQ,A.PARENT_ID,A.PARENT_SEQ,(SELECT MENU_NAME FROM SC_MENU_INFO WHERE MENU_ID = A.PARENT_ID ) " +
                //             " PARENT_NAME FROM SC_MENU_CONF A INNER JOIN SC_MENU_INFO B ON A.CHILD_ID = B.MENU_ID INNER JOIN SC_ROLE_MENU_CONF C ON A.ID = C.MC_ID " +
                //             " WHERE ROLE_ID = 1 AND A.URL IS NOT NULL ORDER BY A.PARENT_SEQ,A.PARENT_ID,A.CHILD_SEQ";
                string qry = "SELECT DISTINCT B.MENU_NAME,B.MENU_ID,A.ID,A.URL,A.CHILD_SEQ,A.PARENT_ID,A.PARENT_SEQ,(SELECT MENU_NAME FROM SC_MENU_INFO " +
                             " WHERE MENU_ID = A.PARENT_ID) PARENT_NAME FROM SC_MENU_CONF A INNER JOIN SC_MENU_INFO B ON A.CHILD_ID = B.MENU_ID  WHERE A.ID " +
                             " NOT IN(SELECT MC_ID ID FROM SC_ROLE_MENU_CONF WHERE ROLE_ID = " + Convert.ToInt32(roleId) + ") ORDER BY A.PARENT_SEQ, A.PARENT_ID, A.CHILD_SEQ";
                DataTable dt = _dbHelper.GetDataTable(qry);
                var item = (from DataRow row in dt.Rows
                            select new RoleMenuConfigureBEL
                            {
                                //SL_NO = ++count,
                                McId = Convert.ToInt32(row["ID"]),
                                ChildMenuId = Convert.ToInt32(row["MENU_ID"]),
                                ChildMenuName = row["MENU_NAME"].ToString(),
                                ParentMenuId = Convert.ToInt32(row["PARENT_ID"]),
                                ParentMenuName = row["PARENT_NAME"].ToString(),
                            }).ToList();
                return item;
            }
            catch (Exception e)
            {
                var lineNum = e.StackTrace.Substring(e.StackTrace.LastIndexOf(' '));
                _errorLogger.GetErrorMessage(e.Message, "RoleMenuConfDAL", lineNum);
                ExceptionReturn = e.Message;
                throw;
            }
        }


        public bool IsParentMenuMapped(string roleId, string parentId)
        {
            try
            {
                //string Qry = "SELECT DISTINCT B.MENU_NAME,B.MENU_ID,A.URL,A.CHILD_SEQ,A.PARENT_ID,A.PARENT_SEQ,(SELECT MENU_NAME FROM SC_MENU_INFO WHERE MENU_ID = A.PARENT_ID ) " +
                //             " PARENT_NAME FROM SC_MENU_CONF A INNER JOIN SC_MENU_INFO B ON A.CHILD_ID = B.MENU_ID INNER JOIN SC_ROLE_MENU_CONF C ON A.ID = C.MC_ID " +
                //             " WHERE ROLE_ID = 1 AND A.URL IS NOT NULL ORDER BY A.PARENT_SEQ,A.PARENT_ID,A.CHILD_SEQ";
                //string Qry = "SELECT DISTINCT B.MENU_NAME,B.MENU_ID,A.URL,A.CHILD_SEQ,A.PARENT_ID,A.PARENT_SEQ,(SELECT MENU_NAME FROM SC_MENU_INFO " +
                //             " WHERE MENU_ID = A.PARENT_ID) PARENT_NAME FROM SC_MENU_CONF A INNER JOIN SC_MENU_INFO B ON A.CHILD_ID = B.MENU_ID  WHERE A.ID " +
                //             " NOT IN(SELECT MC_ID ID FROM SC_ROLE_MENU_CONF WHERE ROLE_ID = " + Convert.ToInt32(roleId) + ") ORDER BY A.PARENT_SEQ, A.PARENT_ID, A.CHILD_SEQ";


                string childQry = "select * From SC_MENU_CONF A INNER JOIN SC_ROLE_MENU_CONF B ON A.ID=B.MC_ID" +
                                  " WHERE A.CHILD_ID=" + Convert.ToInt32(parentId) + " AND B.ROLE_ID=" +
                                  Convert.ToInt32(roleId) + "";


                DataTable childDt = _dbHelper.GetDataTable(childQry);
                if (childDt.Rows.Count > 0)
                {
                    return true;
                }
                string parentQry = "select * From SC_MENU_CONF A WHERE A.PARENT_ID=" + Convert.ToInt32(parentId) + " ";
                DataTable parentDt = _dbHelper.GetDataTable(parentQry);
                if (parentDt.Rows.Count > 0)
                {
                    return true;
                }
                return false;
            }
            catch (Exception e)
            {
                var lineNum = e.StackTrace.Substring(e.StackTrace.LastIndexOf(' '));
                _errorLogger.GetErrorMessage(e.Message, "RoleMenuConfDAL", lineNum);
                ExceptionReturn = e.Message;
                throw;
            }
        }

        public List<RoleMenuConfigureBEL> GetRoleMenuConfList(string param)
        {
            try
            {
                var qry = "SELECT DISTINCT A.ID,A.MC_ID,B.CHILD_ID,C.MENU_NAME CHILD_MENU_NAME,B.PARENT_ID,(SELECT MENU_NAME FROM SC_MENU_INFO WHERE " +
                             "MENU_ID = B.PARENT_ID) PARENT_NAME,A.SV, A.VW, A.DL,D.ROLE_ID,D.ROLE_NAME FROM SC_ROLE_MENU_CONF A INNER JOIN SC_MENU_CONF B ON A.MC_ID = B.ID " +
                             " INNER JOIN SC_MENU_INFO C ON B.CHILD_ID = C.MENU_ID INNER JOIN SC_ROLE_INFO D ON A.ROLE_ID = D.ROLE_ID WHERE 1=1 " + param;
                DataTable dt = _dbHelper.GetDataTable(qry);
                int count = 0;
                var item = (from DataRow row in dt.Rows
                            select new RoleMenuConfigureBEL
                            {
                                SlNo = ++count,
                                Id = Convert.ToInt32(row["ID"]),
                                McId = Convert.ToInt32(row["MC_ID"]),
                                ChildMenuId = Convert.ToInt32(row["CHILD_ID"]),
                                ChildMenuName = row["CHILD_MENU_NAME"].ToString(),
                                ParentMenuId = Convert.ToInt32(row["PARENT_ID"]),
                                ParentMenuName = row["PARENT_NAME"].ToString(),
                                SaveStatus = row["SV"].ToString(),
                                ViewStatus = row["VW"].ToString(),
                                DeleteStatus = row["DL"].ToString(),
                                RoleId = Convert.ToInt32(row["PARENT_ID"]),
                                RoleName = row["ROLE_NAME"].ToString()
                            }).ToList();
                return item;

            }
            catch (Exception e)
            {
                var lineNum = e.StackTrace.Substring(e.StackTrace.LastIndexOf(' '));
                _errorLogger.GetErrorMessage(e.Message, "RoleMenuConfDAL", lineNum);
                ExceptionReturn = e.Message;
                throw;
            }
        }

        // ReSharper disable once InconsistentNaming
        public bool DeleteGridRMCRow(string id)
        {
            try
            {
                IuMode = "D";
                string qry = "DELETE FROM SC_ROLE_MENU_CONF where ID='" + Convert.ToInt32(id) + "'";
                return _dbHelper.CmdExecute(qry, IuMode, "RoleMenuConf", "SC_ROLE_MENU_CONF", Convert.ToInt64(id));
            }
            catch (Exception e)
            {
                var lineNum = e.StackTrace.Substring(e.StackTrace.LastIndexOf(' '));
                _errorLogger.GetErrorMessage(e.Message, "RoleMenuConfDAL", lineNum);
                ExceptionReturn = e.Message;
                throw;
            }
        }
    }
}