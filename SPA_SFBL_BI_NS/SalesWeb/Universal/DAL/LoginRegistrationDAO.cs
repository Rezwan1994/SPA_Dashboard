using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using SalesWeb.Universal.BEL;
using SalesWeb.Universal.Gateway;

namespace SalesWeb.Universal.DAL
{
    public class LoginRegistrationDAO
    {
        readonly DBHelper _dbHelper = new DBHelper();
        private readonly ErrorLogger _errorLogger = new ErrorLogger();
        public List<LoginRegistrationBEL> CheckUserCredential()
        {
            try
            {

                string uQry = "SELECT USER_ID, USER_NAME, PASSWORD,EMPLOYEE_ID FROM SC_USER_LOGIN";
                DataTable dt = _dbHelper.GetDataTable(uQry);
                var item = (from DataRow row in dt.Rows
                    select new LoginRegistrationBEL
                    {
                        USER_ID = row["USER_ID"].ToString(),
                        USER_NAME = row["USER_NAME"].ToString(),
                        PASSWORD = row["PASSWORD"].ToString(),
                        EMPLOYEE_ID = Convert.ToInt32(row["EMPLOYEE_ID"])
                        //SupervisorID = row["SupervisorID"].ToString(),
                        //SupervisorName = row["SupervisorName"].ToString(),
                        //Designation = row["Designation"].ToString(),
                        //EmploymentDate = row["EmploymentDate"].ToString(),
                        //IsActive = Convert.ToBoolean(row["IsActive"].ToString())

                    }).ToList();
                return item;
            }
            catch (Exception e)
            {
                var lineNum = e.StackTrace.Substring(e.StackTrace.LastIndexOf(' '));
                _errorLogger.GetErrorMessage(e.Message, "Home", lineNum);
                throw;
            }

        }
    }
}