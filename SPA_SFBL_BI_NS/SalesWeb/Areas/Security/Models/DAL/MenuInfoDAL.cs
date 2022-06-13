using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using SalesWeb.Areas.Security.Models.BEL;
using SalesWeb.Universal.Gateway;

namespace SalesWeb.Areas.Security.Models.DAL
{
    //
    public class MenuInfoDAL : ReturnData
    {
        private readonly DBHelper _dbHelper = new DBHelper();
        private readonly ErrorLogger _errorLogger = new ErrorLogger();
        public List<MenuInfoBEL> GetMenuList()
        {
            try
            {
                const string qry = "SELECT MENU_ID, MENU_NAME,MENU_DISPLAY_NAME,MENU_TYPE,MENU_STATUS FROM SC_MENU_INFO Order by MENU_NAME";
                DataTable dt = _dbHelper.GetDataTable(qry);
                var item = (from DataRow row in dt.Rows
                            select new MenuInfoBEL
                            {
                                Id = Convert.ToInt64(row["MENU_ID"]),
                                Name = row["MENU_NAME"].ToString(),
                                DisplayName = row["MENU_DISPLAY_NAME"].ToString(),
                                MenuType = row["MENU_TYPE"].ToString(),
                                Status = row["MENU_STATUS"].ToString()
                            }).ToList();
                return item;
            }
            catch (Exception e)
            {
                var lineNum = e.StackTrace.Substring(e.StackTrace.LastIndexOf(' '));
                _errorLogger.GetErrorMessage(e.Message, "MenuInfoDAL", lineNum);
                ExceptionReturn = e.Message;
                throw;
            }
        }

        public bool InsertMenuInfo(MenuInfoBEL master)
        {
            try
            {
                MaxID = _dbHelper.GetMaxSl("SC_MENU_INFO", "MENU_ID");
                IuMode = "I";
                string qry = "Insert into SC_MENU_INFO(MENU_ID,MENU_NAME,MENU_DISPLAY_NAME,MENU_TYPE,MENU_STATUS) Values ('" + MaxID + "', '" + master.Name + "', '" + master.DisplayName + "', '" + master.MenuType + "', '" + master.Status + "')";
                return _dbHelper.CmdExecute(qry, IuMode, "MenuInfo", "SC_MENU_INFO", MaxID);
            }
            catch (Exception e)
            {
                var lineNum = e.StackTrace.Substring(e.StackTrace.LastIndexOf(' '));
                _errorLogger.GetErrorMessage(e.Message, "MenuInfoDAL", lineNum);
                ExceptionReturn = e.Message;
                return false;
            }
        }
        public bool UpdateMenuInfo(MenuInfoBEL master)
        {
            try
            {
                MaxID = master.Id;
                IuMode = "U";
                string qry = "Update SC_MENU_INFO set MENU_NAME='" + master.Name + "',MENU_DISPLAY_NAME='" + master.DisplayName +  "',MENU_STATUS='" + master.Status + "' Where MENU_ID='" + master.Id + "'";
                return _dbHelper.CmdExecute(qry, IuMode, "MenuInfo", "SC_MENU_INFO", MaxID);
            }
            catch (Exception e)
            {
                var lineNum = e.StackTrace.Substring(e.StackTrace.LastIndexOf(' '));
                _errorLogger.GetErrorMessage(e.Message, "MenuInfoDAL", lineNum);
                ExceptionReturn = e.Message;
                return false;
            }
        }
    }
}