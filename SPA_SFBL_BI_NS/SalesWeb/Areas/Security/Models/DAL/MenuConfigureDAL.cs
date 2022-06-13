using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using SalesWeb.Areas.Security.Models.BEL;
using SalesWeb.Universal.Gateway;

namespace SalesWeb.Areas.Security.Models.DAL
{

    public class MenuConfigureDAL : ReturnData
    {
        //private DBConnection _dbConn = new DBConnection();
        private readonly DBHelper _dbHelper = new DBHelper();
        private readonly ErrorLogger _errorLogger = new ErrorLogger();
        public List<MenuConfigureMapBEL> GetParentMenuList()
        {
            //string qry = "SELECT DISTINCT MI.MENU_ID, MI.MENU_NAME, MI.MENU_DISPLAY_NAME, (SELECT DISTINCT NVL(PARENT_SEQ,0) FROM SC_MENU_CONF WHERE PARENT_ID=MI.MENU_ID) PARENT_SEQ FROM SC_MENU_INFO MI LEFT JOIN SC_MENU_CONF MC ON MI.MENU_ID = MC.CHILD_ID WHERE MC.URL IS NULL ORDER BY MI.MENU_NAME";
            string qry = " SELECT DISTINCT " +
                         " MI.MENU_ID," +
                         " MI.MENU_NAME," +
                         " MI.MENU_DISPLAY_NAME," +
                         " (" +
                         " SELECT DISTINCT NVL(PARENT_SEQ, 0) " +
                         " FROM SC_MENU_CONF " +
                         " WHERE PARENT_ID = MI.MENU_ID " +
                         " UNION " +
                         " SELECT DISTINCT NVL(CHILD_SEQ, 0) " +
                         " FROM SC_MENU_CONF " +
                         " WHERE CHILD_ID = MI.MENU_ID" +
                         " ) PARENT_SEQ " +
                         " FROM SC_MENU_INFO MI " +
                         " LEFT JOIN SC_MENU_CONF MC ON MI.MENU_ID = MC.CHILD_ID " +
                         " WHERE MC.URL IS NULL " +
                         " AND MI.MENU_STATUS='Active'" +
                         " ORDER BY MI.MENU_NAME";
            DataTable dt = _dbHelper.GetDataTable(qry);

            var item = (from DataRow row in dt.Rows
                        select new MenuConfigureMapBEL
                        {
                            ParentMenuId = Convert.ToInt32(row["MENU_ID"]),
                            ParentMenuName = row["MENU_NAME"].ToString(),
                            DisplayName = row["MENU_DISPLAY_NAME"].ToString(),
                            ParentSeq = row["PARENT_SEQ"].ToString() == "" ? 0 : Convert.ToInt32(row["PARENT_SEQ"])
                        }).ToList();
            return item;
        }
        public List<MenuConfigureMapBEL> GetChildMenuList(int parentMenuId)
        {
            string qry = " select " +
                         " MI.MENU_ID," +
                         " MI.MENU_NAME," +
                         " MI.MENU_DISPLAY_NAME," +
                         " MC.CHILD_ID," +
                         " NVL(MC.CHILD_SEQ,0) CHILD_SEQ " +
                         " from SC_MENU_INFO MI " +
                         " LEFT JOIN SC_MENU_CONF MC ON MI.MENU_ID = MC.CHILD_ID " +
                         " WHERE MI.MENU_ID NOT IN (SELECT CHILD_ID  MENU_ID FROM SC_MENU_CONF) " +
                         " AND   MI.MENU_ID NOT IN (SELECT PARENT_ID MENU_ID FROM SC_MENU_CONF) " +
                         " AND   MI.MENU_ID != " + parentMenuId + 
                         " AND   MI.MENU_STATUS='Active'"+
                         " ORDER BY MI.MENU_NAME";
            DataTable dt = _dbHelper.GetDataTable(qry);
            var item = (from DataRow row in dt.Rows
                        select new MenuConfigureMapBEL
                        {
                            ChildMenuId = Convert.ToInt32(row["MENU_ID"]),
                            ChildMenuName = row["MENU_NAME"].ToString(),
                            DisplayName = row["MENU_DISPLAY_NAME"].ToString(),
                            ChildSeq = Convert.ToInt32(row["CHILD_SEQ"])
                        }).ToList();
            return item;
        }
        public bool InsertMenuInfo(MenuConfigureBEL menuConfigureBel)
        {
            try
            {
                MaxID = _dbHelper.GetMaxSl("SC_MENU_CONF", "ID");
                IuMode = "I";
                var qry = "Insert into SC_MENU_CONF (ID, PARENT_ID,  PARENT_SEQ, CHILD_ID, CHILD_SEQ, URL) Values('" + MaxID + "', '" + menuConfigureBel.ParentMenuId + "','" + menuConfigureBel.ParentSeq + "','" + menuConfigureBel.ChildMenuId + "','" + menuConfigureBel.ChildSeq + "', '" + menuConfigureBel.Url + "')";
                return _dbHelper.CmdExecute(qry, IuMode, "MenuConfigure", "SC_MENU_CONF", MaxID);
            }
            catch (Exception e)
            {
                var lineNum = e.StackTrace.Substring(e.StackTrace.LastIndexOf(' '));
                _errorLogger.GetErrorMessage(e.Message, "MenuConfigureDAL", lineNum);
                ExceptionReturn = e.Message;
                return false;
            }
        }
        public bool UpdateMenuInfo(MenuConfigureBEL menuConfigureBel)
        {
            try
            {
                MaxID = menuConfigureBel.Id;
                IuMode = "U";
                var qry = "Update SC_MENU_CONF set PARENT_ID='" + menuConfigureBel.ParentMenuId + "',PARENT_SEQ= '" + menuConfigureBel.ParentSeq + "', CHILD_ID ='" + menuConfigureBel.ChildMenuId + "',CHILD_SEQ='" + menuConfigureBel.ChildSeq + "',URL='" + menuConfigureBel.Url + "' Where ID='" + menuConfigureBel.Id + "'";
                return _dbHelper.CmdExecute(qry, IuMode, "MenuConfigure", "SC_MENU_CONF", MaxID);
            }
            catch (Exception e)
            {
                var lineNum = e.StackTrace.Substring(e.StackTrace.LastIndexOf(' '));
                _errorLogger.GetErrorMessage(e.Message, "MenuConfigureDAL", lineNum);
                ExceptionReturn = e.Message;
                return false;
            }
        }
        public bool DeleteMenuConfigure(int id)
        {
            try
            {
                string qry = "delete from SC_MENU_CONF where ID='" + id + "'";
                return _dbHelper.CmdExecute(qry, IuMode, "MenuConfigure", "SC_MENU_CONF", id);
            }
            catch (Exception e)
            {
                var lineNum = e.StackTrace.Substring(e.StackTrace.LastIndexOf(' '));
                _errorLogger.GetErrorMessage(e.Message, "MenuConfigureDAL", lineNum);
                ExceptionReturn = e.Message;
                return false;
            }
        }
        public List<MenuConfigureMapBEL> GetMenuConfigureList(int parentId)
        {
            string qry = "Select MC.ID, MC.PARENT_ID,(SELECT MENU_NAME FROM SC_MENU_INFO WHERE MENU_ID=MC.PARENT_ID) PARENT_NAME, MC.PARENT_SEQ,MC.CHILD_ID,(SELECT MENU_NAME FROM SC_MENU_INFO WHERE MENU_ID=MC.CHILD_ID) CHILD_NAME, MC.CHILD_SEQ,MC.URL FROM SC_MENU_CONF MC WHERE MC.PARENT_ID=" + parentId + " ORDER BY MC.CHILD_SEQ";
            DataTable dt = _dbHelper.GetDataTable(qry);
            var item = (from DataRow row in dt.Rows
                        select new MenuConfigureMapBEL
                        {
                            Id = Convert.ToInt32(row["ID"]),
                            ParentMenuId = Convert.ToInt32(row["PARENT_ID"]),
                            ParentMenuName = row["PARENT_NAME"].ToString(),
                            ParentSeq = Convert.ToInt32(row["PARENT_SEQ"]),
                            ChildMenuId = Convert.ToInt32(row["CHILD_ID"]),
                            ChildMenuName = row["CHILD_NAME"].ToString(),
                            ChildSeq = Convert.ToInt32(row["CHILD_SEQ"]),
                            Url = row["URL"].ToString(),
                        }).ToList();
            return item;
        }


        public object IsChildSeqExist(int parentId, string parentSeq)
        {
            try
            {
                string qry = "SELECT DISTINCT CHILD_SEQ FROM SC_MENU_CONF WHERE CHILD_ID=" + parentId;
                string existParentSeq = _dbHelper.GetValue(qry);
                if (string.IsNullOrEmpty(existParentSeq) || existParentSeq == parentSeq) return parentSeq;
                parentSeq = existParentSeq;
                ExceptionReturn = "Data Exist";
                return parentSeq;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }

        public object IsParentSeqExist(int childId, string childSeq)
        {
            try
            {
                string qry = "SELECT DISTINCT PARENT_SEQ FROM SC_MENU_CONF WHERE PARENT_ID=" + childId;
                string existChildSeq = _dbHelper.GetValue(qry);
                if (string.IsNullOrEmpty(existChildSeq) || existChildSeq == childSeq) return childSeq;
                childSeq = existChildSeq;
                ExceptionReturn = "Data Exist";
                return childSeq;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }

        public bool UpdateParent(MenuConfigureBEL menuConfigureBel)
        {
            try
            {
                MaxID = menuConfigureBel.Id;
                IuMode = "U";
                var qry = "Update SC_MENU_CONF set PARENT_SEQ= '" + menuConfigureBel.ParentSeq + "' Where PARENT_ID='" + menuConfigureBel.ParentMenuId + "'";
                return _dbHelper.CmdExecute(qry, IuMode, "MenuConfigure", "SC_MENU_CONF", MaxID);
            }
            catch (Exception e)
            {
                var lineNum = e.StackTrace.Substring(e.StackTrace.LastIndexOf(' '));
                _errorLogger.GetErrorMessage(e.Message, "MenuConfigureDAL", lineNum);
                ExceptionReturn = e.Message;
                return false;
            }
        }
    }
}