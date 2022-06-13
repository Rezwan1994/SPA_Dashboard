using System;
using System.Data;
using System.Linq;
using System.Web;
using SalesWeb.Areas.SpaMisTransaction.Models.BEL;
using SalesWeb.Universal.Gateway;
using System.Collections.Generic;
//using Oracle.ManagedDataAccess.Client;
using System.Data.OracleClient;

namespace SalesWeb.Areas.SpaMisTransaction.Models.DAL
{
    public class DistBonusProcessRunDAL : ReturnData
    {
        private readonly DBHelper _dbHelper = new DBHelper();
        private readonly DBHelper2 _dbHelper2 = new DBHelper2();
        private readonly ErrorLogger _errorLogger = new ErrorLogger();

        private static readonly DBConnection DBConnection = new DBConnection();
        public readonly string ConnStringSfblMis = DBConnection.SAConnStrReader("Oracle", "PHARMAERP");
        public readonly string ConnStringSfblTrans = DBConnection.SAConnStrReader("Oracle", "SPASFBL");


        public object GetProcessList()
        {
            try
            {
                string qry = " SELECT PROCESS_SLNO,PROCESS_NO, TO_CHAR(PROCESS_DATE,'DD/MM/RRRR')PROCESS_DATE, TO_CHAR(BONUS_START_DATE,'DD/MM/RRRR')BONUS_START_DATE, TO_CHAR(BONUS_END_DATE,'DD/MM/RRRR')BONUS_END_DATE, APPROVED_STATUS, TO_CHAR(APPROVED_DATE,'DD/MM/RRRR')APPROVED_DATE, PROCESS_RUN_STATUS, TO_CHAR(PROCESS_RUN_DATE,'DD/MM/RRRR')PROCESS_RUN_DATE" +
                             " FROM DIST_BONUS_PROCESS" +
                             " WHERE APPROVED_STATUS='Approved'" +
                             " AND PROCESS_RUN_STATUS='No'"+
                             " ORDER BY PROCESS_SLNO";


                DataTable dt = _dbHelper2.GetDataTable(qry);
                var item = (from DataRow row in dt.Rows
                            select new DistBonusProcessBEL
                            {
                                ProcessSlno = row["PROCESS_SLNO"].ToString(),
                                ProcessNo = row["PROCESS_NO"].ToString(),
                                ProcessDate = row["PROCESS_DATE"].ToString(),
                                BonusStartDate = row["BONUS_START_DATE"].ToString(),
                                BonusEndDate = row["BONUS_END_DATE"].ToString(),
                                ApprovedStatus = row["APPROVED_STATUS"].ToString(),
                                ApprovedDate = row["APPROVED_DATE"].ToString(),
                                ProcessRunStatus = row["PROCESS_RUN_STATUS"].ToString(),
                                ProcessRunDate = row["PROCESS_RUN_DATE"].ToString(),

                            }).ToList();
                return item;

            }
            catch (Exception e)
            {
                var lineNum = e.StackTrace.Substring(e.StackTrace.LastIndexOf(' '));
                _errorLogger.GetErrorMessage(e.Message, "DistBonusAdjClaimDAL", lineNum);
                ExceptionReturn = e.Message;
                return "Not Ok";
            }
        }

        public object SearchData()
        {
            try
            {
                var qry = " select" +
                          " PROCESS_SLNO," +
                          " PROCESS_NO," +
                          " TO_CHAR(PROCESS_DATE,'DD/MM/RRRR')PROCESS_DATE," +
                          " TO_CHAR(BONUS_START_DATE,'DD/MM/RRRR')BONUS_START_DATE," +
                          " TO_CHAR(BONUS_END_DATE,'DD/MM/RRRR')BONUS_END_DATE," +
                          " APPROVED_STATUS," +
                          " TO_CHAR(APPROVED_DATE,'DD/MM/RRRR')APPROVED_DATE," +
                          " PROCESS_RUN_STATUS," +
                          " TO_CHAR(PROCESS_RUN_DATE,'DD/MM/RRRR')PROCESS_RUN_DATE" +
                          " FROM DIST_BONUS_PROCESS" +
                          " WHERE APPROVED_STATUS='Approved'" +
                          " AND PROCESS_RUN_STATUS='Yes'"+
                          " ORDER BY PROCESS_SLNO";

                DataTable dt = _dbHelper2.GetDataTable(qry);
                var item = (from DataRow row in dt.Rows
                            select new DistBonusProcessBEL
                            {
                                ProcessSlno = row["PROCESS_SLNO"].ToString(),
                                ProcessNo = row["PROCESS_NO"].ToString(),
                                ProcessDate = row["PROCESS_DATE"].ToString(),
                                BonusStartDate = row["BONUS_START_DATE"].ToString(),
                                BonusEndDate = row["BONUS_END_DATE"].ToString(),
                                ApprovedStatus = row["APPROVED_STATUS"].ToString(),
                                ApprovedDate = row["APPROVED_DATE"].ToString(),
                                ProcessRunStatus = row["PROCESS_RUN_STATUS"].ToString(),
                                ProcessRunDate = row["PROCESS_RUN_DATE"].ToString()
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
        public bool ProcessRun(string process_slno, string process_run_date)
        {
            bool isTrue = false;

            try
            {
                using (OracleConnection con = new OracleConnection(ConnStringSfblTrans))
                {
                    using (OracleCommand cmd = new OracleCommand())
                    {
                        cmd.Connection = con;
                        con.Open();
                        OracleTransaction trans = con.BeginTransaction();
                        cmd.Transaction = trans;
                        cmd.CommandText = "PRC_DIST_BONUS_DISCOUNT";
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.Add("pProcess_slno", OracleType.VarChar).Value = process_slno;
                        cmd.Parameters.Add("pProcess_run_date", OracleType.VarChar).Value = process_run_date;

                        isTrue = cmd.ExecuteNonQuery() > 0;

                        if (!isTrue)
                        {
                            trans.Rollback();
                        }
                        else
                        {
                            trans.Commit();
                        }

                        return isTrue;
                    }
                }
            }
            catch (Exception e)
            {
                var lineNum = e.StackTrace.Substring(e.StackTrace.LastIndexOf(' '));
                _errorLogger.GetErrorMessage(e.Message, "DistBonusProcessRunDAL", lineNum);
                ExceptionReturn = e.Message;
                return false;
            }
        }





    }
}