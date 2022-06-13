using SalesWeb.Areas.Security.Models.BEL;
using SalesWeb.Universal.Gateway;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace SalesWeb.Areas.Security.Models.DAL
{
    public class ChangPassDAL : ReturnData
    {
        private readonly DBHelper _dbHelper = new DBHelper();
        private readonly ErrorLogger _errorLogger = new ErrorLogger();

        public bool UpdatePassword(ChangPassBEL changPass)
        {
           // int userId = Convert.ToInt32(HttpContext.Current.Session["USER_ID"]);
            try
            {
                MaxID = changPass.UserId;
                IuMode = "U";
                string qry = "Update SC_USER_LOGIN set PASSWORD='" + changPass.Password + "' Where USER_ID=" + changPass.UserId + "";
                return _dbHelper.CmdExecute(qry, IuMode, "ChangPass", "SC_USER_LOGIN", MaxID);
            }
            catch (Exception e)
            {

                var lineNum = e.StackTrace.Substring(e.StackTrace.LastIndexOf(' '));
                _errorLogger.GetErrorMessage(e.Message, "ChangPassDAL", lineNum);
                ExceptionReturn = e.Message;
                return false;
            }

        }

        public object GetCurrentPassword()
        {
            int userId = Convert.ToInt32(HttpContext.Current.Session["USER_ID"]);
            try
            {
                string qry = "SELECT PASSWORD, USER_NAME,USER_ID FROM SC_USER_LOGIN WHERE USER_ID= " + userId +"";
                 DataTable dt = _dbHelper.GetDataTable(qry);
                /*
                 string currentPassword;
                 string currentPassword = _dbHelper.GetValue(qry);
                 return currentPassword;
                 */
                ChangPassBEL item = new ChangPassBEL();
                if (dt.Rows.Count > 0)
                {
                    item.Password = dt.Rows[0]["PASSWORD"].ToString();
                    item.Username = dt.Rows[0]["USER_NAME"].ToString();
                    item.UserId = Convert.ToInt32(dt.Rows[0]["USER_ID"]);
                }
                return item;
                
            }
            catch (Exception e)
            {
                var lineNum = e.StackTrace.Substring(e.StackTrace.LastIndexOf(' '));
                _errorLogger.GetErrorMessage(e.Message, "ChangPassDAL", lineNum);
                ExceptionReturn = e.Message;
                return "Not Ok";
            }
        }
    }
}