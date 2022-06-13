using System;
using System.Data;
using System.Linq;
using System.Web;
using SalesWeb.Areas.SpaMisTransaction.Models.BEL;
using SalesWeb.Universal.Gateway;
using System.Collections.Generic;
using Oracle.ManagedDataAccess.Client;

namespace SalesWeb.Areas.SpaMisTransaction.Models.DAL
{
    public class DistBonusProcessDAL : ReturnData
    {
        private readonly DBHelper _dbHelper = new DBHelper();
        private readonly DBHelper2 _dbHelper2 = new DBHelper2();        
        private readonly ErrorLogger _errorLogger = new ErrorLogger();

        private static readonly DBConnection DBConnection = new DBConnection();
        public readonly string ConnStringSfblMis = DBConnection.SAConnStrReader("Oracle", "PHARMAERP");
        public readonly string ConnStringSfblTrans = DBConnection.SAConnStrReader("Oracle", "SPASFBL");

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

        public bool InsertDistBonusProcess(DistBonusProcessBEL master)
        {
            try
            {
                MaxCode = _dbHelper2.GetProcessNo(master.BonusEndDate);
                MaxID = _dbHelper2.GetMaxSl("DIST_BONUS_PROCESS", "PROCESS_SLNO");
                IuMode = "I";

                string qry = "Insert into DIST_BONUS_PROCESS(PROCESS_SLNO,PROCESS_NO,PROCESS_DATE,BONUS_START_DATE,BONUS_END_DATE) " +
                                                   " Values ('" + MaxID +"','" + MaxCode + "',to_date('" + master.ProcessDate + "','dd/mm/rrrr'),to_date('" + master.BonusStartDate + "','dd/mm/rrrr'),to_date('" + master.BonusEndDate + "','dd/mm/rrrr'))";
                
                return _dbHelper2.CmdExecute(qry, IuMode, "DistributorBonusProcess", "DIST_BONUS_PROCESS", MaxID);
            }
            catch (Exception e)
            {
                var lineNum = e.StackTrace.Substring(e.StackTrace.LastIndexOf(' '));
                _errorLogger.GetErrorMessage(e.Message, "DistBonusProcessDAL", lineNum);
                ExceptionReturn = e.Message;
                return false;
            }
        }




        public bool UpdateDistBonusProcess(DistBonusProcessBEL master)
        {
            try
            {
                IuMode = "U";
                string qry = " Update DIST_BONUS_PROCESS set PROCESS_DATE=  to_date('" + master.ProcessDate + "','dd/mm/rrrr') ,BONUS_START_DATE= to_date('" + master.BonusStartDate + "', 'dd/mm/rrrr'),BONUS_END_DATE= to_date('" + master.BonusEndDate + "', 'dd/mm/rrrr') " +
                             " Where PROCESS_SLNO='" + master.ProcessSlno + "'";
                return _dbHelper2.CmdExecute(qry, IuMode, "DistributorBonusProcess", "DIST_BONUS_PROCESS", MaxID);
            }
            catch (Exception e)
            {
                var lineNum = e.StackTrace.Substring(e.StackTrace.LastIndexOf(' '));
                _errorLogger.GetErrorMessage(e.Message, "DistBonusProcessDAL", lineNum);
                ExceptionReturn = e.Message;
                return false;
            }
        }



        public bool DeleteProcess(string processSlno)
        {
            bool isTrue = false;
            using (OracleConnection con = new OracleConnection(ConnStringSfblTrans))
            {
                using (OracleCommand cmd = new OracleCommand())
                {
                    cmd.Connection = con;
                    con.Open();
                    OracleTransaction trans = con.BeginTransaction();
                    cmd.Transaction = trans;
                    try
                    {
                        IuMode = "D";
                        var qryProdDtl = " DELETE  FROM DIST_BONUS_PROCESS" +
                                         " WHERE APPROVED_STATUS='Not Approved' And PROCESS_SLNO=:ProcessSlno";
                        OracleParameter[] paramProdDtl = new OracleParameter[]
                               {
                                    new OracleParameter("ProcessSlno", processSlno)
                               };
                        cmd.CommandText = qryProdDtl;
                        cmd.Parameters.Clear();
                        cmd.Parameters.AddRange(paramProdDtl);
                        cmd.BindByName = true;
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
                    catch (Exception e)
                    {
                        var lineNum = e.StackTrace.Substring(e.StackTrace.LastIndexOf(' '));
                        _errorLogger.GetErrorMessage(e.Message, "UserBaseReportAccessDAL", lineNum);
                        ExceptionReturn = e.Message;
                        return false;
                    }
                }
            }
        }


        public object GetLastBonusProcessData()
        {
            try
            {
                string qry = " SELECT" +
                             " TO_CHAR (TO_DATE(BONUS_END_DATE),'DD/MM/RRRR') LAST_BONUS_END_DATE," +
                             " TO_CHAR (TO_DATE(BONUS_END_DATE)+1,'DD/MM/RRRR') BONUS_START_DATE," +
                             " DECODE(TO_CHAR (TO_DATE (BONUS_END_DATE) + 1, 'DD'),'01',TO_CHAR (TO_DATE (BONUS_END_DATE) + 15, 'DD/MM/RRRR'),TO_CHAR (LAST_DAY (TO_DATE (BONUS_END_DATE) + 1), 'DD/MM/RRRR')) BONUS_END_DATE" +
                             " FROM DIST_BONUS_PROCESS" +
                             " WHERE PROCESS_SLNO=(SELECT MAX(PROCESS_SLNO) FROM DIST_BONUS_PROCESS)";


                DataTable dt = _dbHelper2.GetDataTable(qry);
                var item = (from DataRow row in dt.Rows
                            select new DistBonusProcess2BEL
                            {
                                LastBonusEndDate = row["LAST_BONUS_END_DATE"].ToString(),
                                BonusStartDate = row["BONUS_START_DATE"].ToString(),
                                BonusEndDate = row["BONUS_END_DATE"].ToString(),
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

    }
}