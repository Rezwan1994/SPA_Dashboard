using System;
using System.Data;
using System.Linq;
using System.Web;
using SalesWeb.Areas.SpaMisTransaction.Models.BEL;
using SalesWeb.Areas.SpaMisReport.Models.BEL;
using SalesWeb.Universal.Gateway;
using System.Collections.Generic;
//using Oracle.ManagedDataAccess.Client;
using System.Data.OracleClient;

namespace SalesWeb.Areas.SpaMisTransaction.Models.DAL
{
    public class DistBonusDiscAdjClaimDAL : ReturnData
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
                             " AND PROCESS_RUN_STATUS='Yes'" +
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


        public object GetCustomerList(string param)
        {
            try
            {
                string qry = " SELECT DISTINCT CUSTOMER_CODE, FN_CUSTOMER_NAME(CUSTOMER_CODE)CUSTOMER_NAME, 2 SLNO FROM DIST_BONUS_DISC_CLAIM" +
                             " WHERE 1=1 " + param +
                             " UNION SELECT 'ALL' CUSTOMER_CODE, 'ALL'CUSTOMER_NAME , 1 SLNO FROM DUAL" +
                             " ORDER BY SLNO ,CUSTOMER_NAME";


                DataTable dt = _dbHelper2.GetDataTable(qry);
                var item = (from DataRow row in dt.Rows
                            select new CustomerInfoBEL
                            {
                                CustomerCode = row["CUSTOMER_CODE"].ToString(),
                                CustomerName = row["CUSTOMER_NAME"].ToString(),

                            }).ToList();
                return item;

            }
            catch (Exception e)
            {
                var lineNum = e.StackTrace.Substring(e.StackTrace.LastIndexOf(' '));
                _errorLogger.GetErrorMessage(e.Message, "DistProductFactoryRelationDAL", lineNum);
                ExceptionReturn = e.Message;
                return "Not Ok";
            }
        }

        public List<DistBonusDiscAdjClaimBEL> GetData(string ProcessNo, string CustomerCode)
        {
            try
            {
                var qry = " SELECT" +
                          " A.PROCESS_NO," +
                          " TO_CHAR(E.PROCESS_DATE,'MM/DD/RRRR') PROCESS_DATE," +
                          " TO_CHAR(A.BONUS_DISC_START_DATE,'MM/DD/RRRR')BONUS_DISC_START_DATE," +
                          " TO_CHAR(A.BONUS_DISC_END_DATE,'MM/DD/RRRR')BONUS_DISC_END_DATE," +
                          " A.CLAIM_NO," +
                          " TO_CHAR(A.CLAIM_DATE,'MM/DD/RRRR') CLAIM_DATE," +
                          " A.CUSTOMER_CODE," +
                          " C.CUSTOMER_NAME," +
                          " A.FACTORY_CODE," +
                          " D.FACTORY_NAME," +
                          " TO_CHAR(A.APPROVED_DATE,'MM/DD/RRRR')APPROVED_DATE," +
                          " TO_CHAR(E.PROCESS_RUN_DATE,'MM/DD/RRRR')PROCESS_RUN_DATE," +
                          " A.BONUS_DISC_AMT" +
                          " FROM  DIST_BONUS_DISC_CLAIM A,CUSTOMER_INFO C,FACTORY_INFO D,DIST_BONUS_PROCESS E" +
                          " WHERE A.CUSTOMER_CODE=C.CUSTOMER_CODE" +
                          " AND   A.FACTORY_CODE=D.FACTORY_CODE " +
                          " AND   A.PROCESS_NO=E.PROCESS_NO" +
                          " AND   A.PROCESS_NO='" + ProcessNo + "'" +
                          " AND   (A.CUSTOMER_CODE ='" + CustomerCode + "' OR '" + CustomerCode + "'='ALL')" +
                          " ORDER BY A.PROCESS_NO,CLAIM_NO";

                DataTable dt = _dbHelper2.GetDataTable(qry);
                var item = (from DataRow row in dt.Rows
                            select new DistBonusDiscAdjClaimBEL
                            {
                                ProcessNo = row["PROCESS_NO"].ToString(),
                                ProcessDate = row["PROCESS_DATE"].ToString(),
                                BonusDiscStartDate = row["BONUS_DISC_START_DATE"].ToString(),
                                BonusDiscEndDate = row["BONUS_DISC_END_DATE"].ToString(),
                                ApprovedDate = row["APPROVED_DATE"].ToString(),
                                ProcessRunDate = row["PROCESS_RUN_DATE"].ToString(),
                                FactoryCode = row["FACTORY_CODE"].ToString(),
                                FactoryName = row["FACTORY_NAME"].ToString(),
                                ClaimNo = row["CLAIM_NO"].ToString(),
                                ClaimDate = row["CLAIM_DATE"].ToString(),
                                CustomerCode = row["CUSTOMER_CODE"].ToString(),
                                CustomerName = row["CUSTOMER_NAME"].ToString(),
                                BonusDiscAmt = row["BONUS_DISC_AMT"].ToString()
                            }).ToList();
                return item;

            }
            catch (Exception e)
            {
                var lineNum = e.StackTrace.Substring(e.StackTrace.LastIndexOf(' '));
                _errorLogger.GetErrorMessage(e.Message, "DistWiseSrSalesDAL", lineNum);
                ExceptionReturn = e.Message;
                throw;
            }
        }



    }
}