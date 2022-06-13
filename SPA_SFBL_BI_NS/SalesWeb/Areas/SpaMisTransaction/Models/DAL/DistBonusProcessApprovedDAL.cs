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
    public class DistBonusProcessApprovedDAL : ReturnData
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
                             " WHERE APPROVED_STATUS='Not Approved'" +
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
                          " TO_CHAR(APPROVED_DATE,'DD/MM/RRRR')APPROVED_DATE" +
                          " FROM DIST_BONUS_PROCESS" +
                          " WHERE APPROVED_STATUS='Approved'" +
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
                                ApprovedDate = row["APPROVED_DATE"].ToString()
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



        public bool UpdateDistBonusProcessApproved(DistBonusProcessBEL master)
        {
            try
            {
                IuMode = "U";
                string qry = " Update DIST_BONUS_PROCESS set APPROVED_DATE=  to_date('" + master.ApprovedDate + "','dd/mm/rrrr') ,APPROVED_STATUS='"  + master.ApprovedStatus + "'"+
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

    }
}