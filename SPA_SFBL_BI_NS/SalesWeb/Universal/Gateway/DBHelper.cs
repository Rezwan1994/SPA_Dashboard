using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Web;
using Oracle.ManagedDataAccess.Client;

using System.Net;
using System.Net.Sockets;

namespace SalesWeb.Universal.Gateway
{
    public class DBHelper : ReturnData
    {
        private static readonly DBConnection DBConnection = new DBConnection();
        private readonly ErrorLogger _errorLogger = new ErrorLogger();

        private const string DBType = "Oracle";
        private const string CompanyType = "PHARMAERP";
        public readonly string ConnString = DBConnection.SAConnStrReader(DBType, CompanyType);

        public bool CmdExecute(string qry, string activityType, string actionForm, string actionTable, long transactionId)
        {
            try
            {
                var isTrue = false;
                using (OracleConnection con = new OracleConnection(ConnString))
                {
                    using (OracleCommand cmd = new OracleCommand(qry, con))
                    {
                        con.Open();
                        int noOfRows = cmd.ExecuteNonQuery();
                        if (noOfRows > 0)
                        {
                            InsertAudit(activityType, actionForm, actionTable, transactionId);
                            isTrue = true;
                        }
                    }
                }
                return isTrue;
            }
            catch (Exception e)
            {
 
                ExceptionReturn = e.Message;
                throw;
            }
        }
        public bool ParameterizedCmdExecute(string qry, string activityType, string actionForm, string actionTable, long transactionId, OracleParameter[] parameters)
        {
            try
            {
                var isTrue = false;
                using (OracleConnection con = new OracleConnection(ConnString))
                {
                    using (OracleCommand cmd = new OracleCommand(qry, con))
                    {
                        con.Open();
                        cmd.Parameters.Clear();
                        cmd.Parameters.AddRange(parameters);
                        cmd.BindByName = true;
                        int noOfRows = cmd.ExecuteNonQuery();
                        if (noOfRows > 0)
                        {
                            InsertAudit(activityType, actionForm, actionTable, transactionId);
                            isTrue = true;
                        }
                    }
                }
                return isTrue;
            }
            catch (Exception e)
            {

                ExceptionReturn = e.Message;
                throw;
            }
        }
        public bool CmdTransExecute(string qry)
        {
            try
            {
                bool isTrue = false;
                using (OracleConnection con = new OracleConnection(ConnString))
                {
                    OracleCommand cmd = new OracleCommand(qry, con);
                    con.Open();
                    int noOfRows = cmd.ExecuteNonQuery();
                    if (noOfRows > 0)
                    {
                        isTrue = true;
                    }
                }
                return isTrue;
            }
            catch (Exception e)
            {
                var lineNum = e.StackTrace.Substring(e.StackTrace.LastIndexOf(' '));
                _errorLogger.GetErrorMessage(e.Message, "DBHelper", lineNum);
                throw;
            }
        }
        public bool InsertAudit(string activityType, string actionForm, string actionTable, long transactionId)
        {
            try
            {
                MaxID = GetMaxSl("SC_AUDIT_TRAIL", "AUDIT_ID");
                var enteredTerminal = GetIpAddress();
                var enteredBy = HttpContext.Current.Session["EMPLOYEE_ID"].ToString();
                var qry = "Insert into SC_AUDIT_TRAIL (AUDIT_ID, ACTION_BY, TERMINAL,ACTION_DATE, ACTIVITY_TYPE, ACTION_FORM,ACTION_TABLE, TRANSACTION_ID) " +
                          " Values ('" + MaxID + "', '" + enteredBy + "', '" + enteredTerminal + "', TO_DATE('" + DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss") + "','DD/MM/YYYY HH24:MI:SS'), " + "'" + activityType + "','" + actionForm + "','" + actionTable + "'," + transactionId + ")";
                bool isTrue = false;
                using (OracleConnection con = new OracleConnection(ConnString))
                {
                    OracleCommand cmd = new OracleCommand(qry, con);
                    con.Open();
                    int noOfRows = cmd.ExecuteNonQuery();
                    if (noOfRows > 0)
                    {
                        isTrue = true;
                    }
                }
                return isTrue;
            }
            catch (Exception e)
            {
                var lineNum = e.StackTrace.Substring(e.StackTrace.LastIndexOf(' '));
                _errorLogger.GetErrorMessage(e.Message, "DBHelper", lineNum);
                throw;
            }
        }
        public Int64 GetMaxSl(string tableName, string columnName)
        {
            Int64 maxId = 0;
            string queryString = "select NVL(MAX(" + columnName + "),0)+1 id from " + tableName + "";
            using (OracleConnection oracleConnection = new OracleConnection(ConnString))
            {
                oracleConnection.Open();
                using (OracleCommand oracleCommand = new OracleCommand(queryString, oracleConnection))
                {
                    using (OracleDataReader rdr = oracleCommand.ExecuteReader())
                    {
                        if (rdr.Read())
                        {
                            maxId = Convert.ToInt64(rdr["id"].ToString());
                        }
                    }
                }
            }
            return maxId;
        }
        public Int64 GetMaxSl(string tableName, string columnName, string param)
        {
            Int64 maxId = 0;
            string queryString = "select NVL(MAX(" + columnName + "),0)+1 id from " + tableName + " WHERE 1=1 " + param + " ";
            using (OracleConnection oracleConnection = new OracleConnection(ConnString))
            {
                oracleConnection.Open();
                using (OracleCommand oracleCommand = new OracleCommand(queryString, oracleConnection))
                {
                    using (OracleDataReader rdr = oracleCommand.ExecuteReader())
                    {
                        if (rdr.Read())
                        {
                            maxId = Convert.ToInt64(rdr["id"].ToString());
                        }
                    }
                }
            }
            return maxId;
        }
        public Int64 GetMaxSlSbStr(string tableName, string columnName, int length, string param)
        {
            Int64 maxId = 0;
            string queryString = "select NVL(MAX(SUBSTR(" + columnName + "," + length + ")),0)+1 id from " + tableName + " WHERE 1=1 " + param + " ";
            using (OracleConnection oracleConnection = new OracleConnection(ConnString))
            {
                oracleConnection.Open();
                using (OracleCommand oracleCommand = new OracleCommand(queryString, oracleConnection))
                {
                    using (OracleDataReader rdr = oracleCommand.ExecuteReader())
                    {
                        if (rdr.Read())
                        {
                            maxId = Convert.ToInt64(rdr["id"].ToString());
                        }
                    }
                }
            }
            return maxId;
        }
        public DataTable GetDataTable(string qry)
        {
            try
            {
                DataTable dt = new DataTable();
                using (OracleConnection objConn = new OracleConnection(ConnString))
                {
                    using (OracleCommand objCmd = new OracleCommand())
                    {
                        objCmd.CommandText = qry;
                        objCmd.Connection = objConn;
                        objConn.Open();
                        objCmd.ExecuteNonQuery();
                        using (OracleDataReader rdr = objCmd.ExecuteReader())
                        {
                            if (rdr.HasRows)
                            {
                                dt.Load(rdr);
                            }
                        }
                    }
                }
                return dt;
            }
            catch (OracleException)
            {

                throw;
            }

        }
        public DataTable GetDataTableWithAuditTrial(string qry,string reportName)
        {
            try
            {
                DataTable dt = new DataTable();
                using (OracleConnection objConn = new OracleConnection(ConnString))
                {
                    using (OracleCommand objCmd = new OracleCommand())
                    {
                        objCmd.CommandText = qry;
                        objCmd.Connection = objConn;
                        objConn.Open();
                        objCmd.ExecuteNonQuery();
                        using (OracleDataReader rdr = objCmd.ExecuteReader())
                        {
                            if (rdr.HasRows)
                            {
                                dt.Load(rdr);
                            }
                            InsertReportAudit(reportName);
                        }
                    }
                }
                return dt;
            }
            catch (OracleException)
            {

                throw;
            }

        }
        public bool InsertReportAudit(string reportName)
        {
            try
            {
                //var enteredTerminal = GetIpAddress();
                var enteredIp = GetIP;
                var enteredTerminal = GetHostName;
                var userId = HttpContext.Current.Session["USER_ID"].ToString();
                var qry = "Insert into SC_REPORT_ACCESS_INFO (USER_ID, REPORT_NAME, REPORT_ACCESS_DATE, REPORT_ACCESS_TERMINAL,REPORT_ACCESS_IP) " +
                          " Values ('" + userId + "', '" + reportName + "', TO_DATE('" + DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss") + "','DD/MM/YYYY HH24:MI:SS'),'" + enteredTerminal+"','" + enteredIp + "')";
                bool isTrue = false;
                using (OracleConnection con = new OracleConnection(ConnString))
                {
                    OracleCommand cmd = new OracleCommand(qry, con);
                    con.Open();
                    int noOfRows = cmd.ExecuteNonQuery();
                    if (noOfRows > 0)
                    {
                        isTrue = true;
                    }
                }
                return isTrue;
            }
            catch (Exception e)
            {
                var lineNum = e.StackTrace.Substring(e.StackTrace.LastIndexOf(' '));
                _errorLogger.GetErrorMessage(e.Message, "DBHelper", lineNum);
                throw;
            }
        }
        public string GetValue(string qry)
        {
            string value = "";
            using (OracleConnection odbcConnection = new OracleConnection(ConnString))
            {
                odbcConnection.Open();
                using (OracleCommand odbcCommand = new OracleCommand(qry, odbcConnection))
                {
                    OracleDataReader rdr = odbcCommand.ExecuteReader();
                    if (rdr.Read())
                    {
                        value = rdr[0].ToString();
                    }
                    rdr.Close();
                    odbcConnection.Close();
                    return value;
                }
            }
        }
        public DataRow GetDataRow(string qry)
        {
            DataRow row = null;
            using (OracleConnection odbcConnection = new OracleConnection(ConnString))
            {
                odbcConnection.Open();
                using (OracleCommand odbcCommand = new OracleCommand(qry, odbcConnection))
                {
                    OracleDataReader rdr = odbcCommand.ExecuteReader();
                    if (rdr.Read())
                    {
                        row[0] = rdr[0];
                    }
                    rdr.Close();
                    odbcConnection.Close();
                    return row;
                }
            }
        }
        public string GetEmpName(int empID)
        {

            string empName = "";
            string queryString = "select EMPLOYEE_NAME from EMPLOYEE_INFO where employee_id = " + empID + " ";
            using (OracleConnection oracleConnection = new OracleConnection(ConnString))
            {
                oracleConnection.Open();
                using (OracleCommand oracleCommand = new OracleCommand(queryString, oracleConnection))
                {
                    using (OracleDataReader rdr = oracleCommand.ExecuteReader())
                    {
                        if (rdr.Read())
                        {
                            empName = rdr["EMPLOYEE_NAME"].ToString();
                        }
                    }
                }
            }

            return empName.ToString();
        }
        public string GetNo(string tableName, string columnName)
        {
            string noId = "";
            string queryString = "SELECT TO_CHAR(SYSDATE,'YY')||TO_CHAR(SYSDATE,'MM')||LPAD(NVL(MAX(NVL(TO_NUMBER(" + columnName + "),0)),0)+1,6,0) gNo FROM " + tableName + "";
            using (OracleConnection oracleConnection = new OracleConnection(ConnString))
            {
                oracleConnection.Open();
                using (OracleCommand oracleCommand = new OracleCommand(queryString, oracleConnection))
                {
                    using (OracleDataReader rdr = oracleCommand.ExecuteReader())
                    {
                        if (rdr.Read())
                        {
                            noId = rdr["gNo"].ToString();
                        }
                    }
                }
            }
            return noId;
        }

    }

    public class DBHelper2 : ReturnData
    {
        private static readonly DBConnection DBConnection = new DBConnection();
        private readonly ErrorLogger _errorLogger = new ErrorLogger();

        private const string DBType = "Oracle";
        private const string CompanyType = "SPASFBL";
        public readonly string ConnString = DBConnection.SAConnStrReader(DBType, CompanyType);

        public bool CmdExecute(string qry, string activityType, string actionForm, string actionTable, long transactionId)
        {
            try
            {
                var isTrue = false;
                using (OracleConnection con = new OracleConnection(ConnString))
                {
                    using (OracleCommand cmd = new OracleCommand(qry, con))
                    {
                        con.Open();
                        int noOfRows = cmd.ExecuteNonQuery();
                        if (noOfRows > 0)
                        {
                            //InsertAudit(activityType, actionForm, actionTable, transactionId);
                            isTrue = true;
                        }
                    }
                }
                return isTrue;
            }
            catch (Exception e)
            {

                ExceptionReturn = e.Message;
                throw;
            }
        }
        public bool ParameterizedCmdExecute(string qry, string activityType, string actionForm, string actionTable, long transactionId, OracleParameter[] parameters)
        {
            try
            {
                var isTrue = false;
                using (OracleConnection con = new OracleConnection(ConnString))
                {
                    using (OracleCommand cmd = new OracleCommand(qry, con))
                    {
                        con.Open();
                        cmd.Parameters.Clear();
                        cmd.Parameters.AddRange(parameters);
                        cmd.BindByName = true;
                        int noOfRows = cmd.ExecuteNonQuery();
                        if (noOfRows > 0)
                        {
                            InsertAudit(activityType, actionForm, actionTable, transactionId);
                            isTrue = true;
                        }
                    }
                }
                return isTrue;
            }
            catch (Exception e)
            {

                ExceptionReturn = e.Message;
                throw;
            }
        }
        public bool CmdTransExecute(string qry)
        {
            try
            {
                bool isTrue = false;
                using (OracleConnection con = new OracleConnection(ConnString))
                {
                    OracleCommand cmd = new OracleCommand(qry, con);
                    con.Open();
                    int noOfRows = cmd.ExecuteNonQuery();
                    if (noOfRows > 0)
                    {
                        isTrue = true;
                    }
                }
                return isTrue;
            }
            catch (Exception e)
            {
                var lineNum = e.StackTrace.Substring(e.StackTrace.LastIndexOf(' '));
                _errorLogger.GetErrorMessage(e.Message, "DBHelper", lineNum);
                throw;
            }
        }
        public bool InsertAudit(string activityType, string actionForm, string actionTable, long transactionId)
        {
            try
            {
                MaxID = GetMaxSl("SC_AUDIT_TRAIL", "AUDIT_ID");
                var enteredTerminal = GetIpAddress();
                var enteredBy = HttpContext.Current.Session["EMPLOYEE_ID"].ToString();
                var qry = "Insert into SC_AUDIT_TRAIL (AUDIT_ID, ACTION_BY, TERMINAL,ACTION_DATE, ACTIVITY_TYPE, ACTION_FORM,ACTION_TABLE, TRANSACTION_ID) " +
                          " Values ('" + MaxID + "', '" + enteredBy + "', '" + enteredTerminal + "', TO_DATE('" + DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss") + "','DD/MM/YYYY HH24:MI:SS'), " + "'" + activityType + "','" + actionForm + "','" + actionTable + "'," + transactionId + ")";
                bool isTrue = false;
                using (OracleConnection con = new OracleConnection(ConnString))
                {
                    OracleCommand cmd = new OracleCommand(qry, con);
                    con.Open();
                    int noOfRows = cmd.ExecuteNonQuery();
                    if (noOfRows > 0)
                    {
                        isTrue = true;
                    }
                }
                return isTrue;
            }
            catch (Exception e)
            {
                var lineNum = e.StackTrace.Substring(e.StackTrace.LastIndexOf(' '));
                _errorLogger.GetErrorMessage(e.Message, "DBHelper", lineNum);
                throw;
            }
        }
        public Int64 GetMaxSl(string tableName, string columnName)
        {
            Int64 maxId = 0;
            string queryString = "select NVL(MAX(" + columnName + "),0)+1 id from " + tableName + "";
            using (OracleConnection oracleConnection = new OracleConnection(ConnString))
            {
                oracleConnection.Open();
                using (OracleCommand oracleCommand = new OracleCommand(queryString, oracleConnection))
                {
                    using (OracleDataReader rdr = oracleCommand.ExecuteReader())
                    {
                        if (rdr.Read())
                        {
                            maxId = Convert.ToInt64(rdr["id"].ToString());
                        }
                    }
                }
            }
            return maxId;
        }
        public Int64 GetMaxSl(string tableName, string columnName, string param)
        {
            Int64 maxId = 0;
            string queryString = "select NVL(MAX(" + columnName + "),0)+1 id from " + tableName + " WHERE 1=1 " + param + " ";
            using (OracleConnection oracleConnection = new OracleConnection(ConnString))
            {
                oracleConnection.Open();
                using (OracleCommand oracleCommand = new OracleCommand(queryString, oracleConnection))
                {
                    using (OracleDataReader rdr = oracleCommand.ExecuteReader())
                    {
                        if (rdr.Read())
                        {
                            maxId = Convert.ToInt64(rdr["id"].ToString());
                        }
                    }
                }
            }
            return maxId;
        }
        public Int64 GetMaxSlSbStr(string tableName, string columnName, int length, string param)
        {
            Int64 maxId = 0;
            string queryString = "select NVL(MAX(SUBSTR(" + columnName + "," + length + ")),0)+1 id from " + tableName + " WHERE 1=1 " + param + " ";
            using (OracleConnection oracleConnection = new OracleConnection(ConnString))
            {
                oracleConnection.Open();
                using (OracleCommand oracleCommand = new OracleCommand(queryString, oracleConnection))
                {
                    using (OracleDataReader rdr = oracleCommand.ExecuteReader())
                    {
                        if (rdr.Read())
                        {
                            maxId = Convert.ToInt64(rdr["id"].ToString());
                        }
                    }
                }
            }
            return maxId;
        }
        public DataTable GetDataTable(string qry)
        {
            try
            {
                DataTable dt = new DataTable();
                using (OracleConnection objConn = new OracleConnection(ConnString))
                {
                    using (OracleCommand objCmd = new OracleCommand())
                    {
                        objCmd.CommandText = qry;
                        objCmd.Connection = objConn;
                        objConn.Open();
                        objCmd.ExecuteNonQuery();
                        using (OracleDataReader rdr = objCmd.ExecuteReader())
                        {
                            if (rdr.HasRows)
                            {
                                dt.Load(rdr);
                            }
                        }
                    }
                }
                return dt;
            }
            catch (OracleException)
            {

                throw;
            }

        }
        public DataTable GetDataTableWithAuditTrial(string qry, string reportName)
        {
            try
            {
                DataTable dt = new DataTable();
                using (OracleConnection objConn = new OracleConnection(ConnString))
                {
                    using (OracleCommand objCmd = new OracleCommand())
                    {
                        objCmd.CommandText = qry;
                        objCmd.Connection = objConn;
                        objConn.Open();
                        objCmd.ExecuteNonQuery();
                        using (OracleDataReader rdr = objCmd.ExecuteReader())
                        {
                            if (rdr.HasRows)
                            {
                                dt.Load(rdr);
                            }
                            InsertReportAudit(reportName);
                        }
                    }
                }
                return dt;
            }
            catch (OracleException)
            {

                throw;
            }

        }
        public bool InsertReportAudit(string reportName)
        {
            try
            {
                //var enteredTerminal = GetIpAddress();
                var enteredIp = GetIP;
                var enteredTerminal = GetHostName;
                var userId = HttpContext.Current.Session["USER_ID"].ToString();
                var qry = "Insert into SC_REPORT_ACCESS_INFO (USER_ID, REPORT_NAME, REPORT_ACCESS_DATE, REPORT_ACCESS_TERMINAL,REPORT_ACCESS_IP) " +
                          " Values ('" + userId + "', '" + reportName + "', TO_DATE('" + DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss") + "','DD/MM/YYYY HH24:MI:SS'),'" + enteredTerminal + "','" + enteredIp + "')";
                bool isTrue = false;
                using (OracleConnection con = new OracleConnection(ConnString))
                {
                    OracleCommand cmd = new OracleCommand(qry, con);
                    con.Open();
                    int noOfRows = cmd.ExecuteNonQuery();
                    if (noOfRows > 0)
                    {
                        isTrue = true;
                    }
                }
                return isTrue;
            }
            catch (Exception e)
            {
                var lineNum = e.StackTrace.Substring(e.StackTrace.LastIndexOf(' '));
                _errorLogger.GetErrorMessage(e.Message, "DBHelper", lineNum);
                throw;
            }
        }
        public string GetValue(string qry)
        {
            string value = "";
            using (OracleConnection odbcConnection = new OracleConnection(ConnString))
            {
                odbcConnection.Open();
                using (OracleCommand odbcCommand = new OracleCommand(qry, odbcConnection))
                {
                    OracleDataReader rdr = odbcCommand.ExecuteReader();
                    if (rdr.Read())
                    {
                        value = rdr[0].ToString();
                    }
                    rdr.Close();
                    odbcConnection.Close();
                    return value;
                }
            }
        }
        public DataRow GetDataRow(string qry)
        {
            DataRow row = null;
            using (OracleConnection odbcConnection = new OracleConnection(ConnString))
            {
                odbcConnection.Open();
                using (OracleCommand odbcCommand = new OracleCommand(qry, odbcConnection))
                {
                    OracleDataReader rdr = odbcCommand.ExecuteReader();
                    if (rdr.Read())
                    {
                        row[0] = rdr[0];
                    }
                    rdr.Close();
                    odbcConnection.Close();
                    return row;
                }
            }
        }
        public string GetEmpName(int empID)
        {

            string empName = "";
            string queryString = "select EMPLOYEE_NAME from EMPLOYEE_INFO where employee_id = " + empID + " ";
            using (OracleConnection oracleConnection = new OracleConnection(ConnString))
            {
                oracleConnection.Open();
                using (OracleCommand oracleCommand = new OracleCommand(queryString, oracleConnection))
                {
                    using (OracleDataReader rdr = oracleCommand.ExecuteReader())
                    {
                        if (rdr.Read())
                        {
                            empName = rdr["EMPLOYEE_NAME"].ToString();
                        }
                    }
                }
            }

            return empName.ToString();
        }
        public string GetNo(string tableName, string columnName)
        {
            string noId = "";
            string queryString = "SELECT TO_CHAR(SYSDATE,'YY')||TO_CHAR(SYSDATE,'MM')||LPAD(NVL(MAX(NVL(TO_NUMBER(" + columnName + "),0)),0)+1,6,0) gNo FROM " + tableName + "";
            using (OracleConnection oracleConnection = new OracleConnection(ConnString))
            {
                oracleConnection.Open();
                using (OracleCommand oracleCommand = new OracleCommand(queryString, oracleConnection))
                {
                    using (OracleDataReader rdr = oracleCommand.ExecuteReader())
                    {
                        if (rdr.Read())
                        {
                            noId = rdr["gNo"].ToString();
                        }
                    }
                }
            }
            return noId;
        }



        public string GetProcessNo(string BonusEndDate)
        {
            string ProcessNo = "";
            string queryString = "SELECT to_char(to_date('"+BonusEndDate+"','dd/mm/rrrr'),'YYYYMM')||count(*)+1 pNo FROM   DIST_BONUS_PROCESS WHERE  TO_CHAR(BONUS_START_DATE,'YYYYMM')=to_char(to_date('"+BonusEndDate+"','dd/mm/rrrr'),'YYYYMM')";
            using (OracleConnection oracleConnection = new OracleConnection(ConnString))
            {
                oracleConnection.Open();
                using (OracleCommand oracleCommand = new OracleCommand(queryString, oracleConnection))
                {
                    using (OracleDataReader rdr = oracleCommand.ExecuteReader())
                    {
                        if (rdr.Read())
                        {
                            ProcessNo = rdr["pNo"].ToString();
                        }
                    }
                }
            }
            return ProcessNo;
        }









    }

}
