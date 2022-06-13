using System;
using System.Data;
using System.Linq;
using System.Web;
using SalesWeb.Areas.Security.Models.BEL;
using SalesWeb.Universal.Gateway;
using System.Collections.Generic;
using System.Data.OracleClient;

namespace SalesWeb.Areas.Security.Models.DAL
{
    public class MaterializedViewRefreshDAL : ReturnData
    {
        private readonly DBHelper _dbHelper = new DBHelper();
        private readonly ErrorLogger _errorLogger = new ErrorLogger();
        private static readonly DBConnection DBConnection = new DBConnection();
        public readonly string ConnString = DBConnection.SAConnStrReader("Oracle", "PHARMAERP");

        //public string OutMsg { get; set; }

        public List<MaterializedViewRefreshBEL> GetMaterializedView()
        {
            try
            {
                string qry = " SELECT JOB_NAME, RUN_DATE, RUN_DURATION, STATUS " +
                              " FROM VW_SCHEDULER_JOB_RUN_DETAILS " +
                              " ORDER BY LOG_ID";


                DataTable dt = _dbHelper.GetDataTable(qry);
                var item = (from DataRow row in dt.Rows
                            select new MaterializedViewRefreshBEL
                            {
                                MaterializedViewName = row["JOB_NAME"].ToString(),
                                RunDate = row["RUN_DATE"].ToString(),
                                RunDuration = row["RUN_DURATION"].ToString(),
                                RefreshStatus = row["STATUS"].ToString()                              
                            }).ToList();
                return item;
            }
            catch (Exception e)
            {
                var lineNum = e.StackTrace.Substring(e.StackTrace.LastIndexOf(' '));
                _errorLogger.GetErrorMessage(e.Message, "MaterializedViewRefreshDAL", lineNum);
                ExceptionReturn = e.Message;
                throw;
            }
        }


        //Procedure
        public bool RefreshMaterializedView(string JobName)
        {
            bool isTrue = false;

            try
            {
                using (OracleConnection con = new OracleConnection(ConnString))
                {
                    using (OracleCommand cmd = new OracleCommand())
                    {
                        cmd.Connection = con;
                        cmd.CommandText = "Prc_RefreshMaterializedView";
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.Add("pJobName", OracleType.VarChar).Value = JobName;
                        //cmd.Parameters.Add("pOutMsg", OracleType.VarChar, 100).Direction = ParameterDirection.Output;
                        con.Open();
                        int cont = cmd.ExecuteNonQuery();

                        if (cont > 0)
                        {


                            //string vOutMsg = cmd.Parameters["pOutMsg"].Value.ToString();

                            //if (vOutMsg != null && vOutMsg != "")
                            //{
                            //    isTrue = true;
                            //    OutMsg = vOutMsg;
                            //}
                            //else
                            //{
                            //    isTrue = false;
                            //}

                            return true;

                        }
                    }

                }

                return isTrue;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }



    }
}