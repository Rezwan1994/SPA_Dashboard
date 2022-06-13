using System;
using System.Data;
using System.Linq;
using System.Web;
using SalesWeb.Areas.SpaMisReport.Models.BEL;
using SalesWeb.Universal.Gateway;
using System.Collections.Generic;
using static SalesWeb.Areas.SpaMisReport.Models.BEL.TestBEL;
//using Oracle.ManagedDataAccess.Client;
using System.Data.OracleClient;

namespace SalesWeb.Areas.SpaMisReport.Models.DAL
{
    public class CallTestProcedureDAL : ReturnData
    {

        private readonly DBHelper _dbHelper = new DBHelper();
        private readonly ErrorLogger _errorLogger = new ErrorLogger();
        private static readonly DBConnection DBConnection = new DBConnection();
        public readonly string ConnString = DBConnection.SAConnStrReader("Oracle", "PHARMAERP");

        public string OutMsg { get; set; }


        //Procedure
        public bool ExcuteTestProcedure()
        {
            bool isTrue = false;

            try
            {
                using (OracleConnection con = new OracleConnection(ConnString))
                {
                    using (OracleCommand cmd = new OracleCommand())
                    {
                        cmd.Connection = con;
                        cmd.CommandText = "Prc_Test";
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.Add("pOutMsg", OracleType.VarChar, 100).Direction = ParameterDirection.Output;
                        con.Open();
                        int cont = cmd.ExecuteNonQuery();

                        if (cont > 0)
                        {

                            string vOutMsg = cmd.Parameters["pOutMsg"].Value.ToString();

                            if (vOutMsg != null && vOutMsg != "")
                            {
                                isTrue = true;
                                OutMsg = vOutMsg;
                            }
                            else
                            {
                                isTrue = false;
                            }

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