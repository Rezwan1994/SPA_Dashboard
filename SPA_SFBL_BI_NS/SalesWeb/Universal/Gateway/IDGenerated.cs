using System;
using System.Collections.Generic;
using System.Data.OracleClient;
using System.Linq;
using System.Web;

namespace SalesWeb.Universal.Gateway
{
    public class IDGenerated
    {
        private DBConnection _dbConnection = new DBConnection();
        private readonly DBHelper _dbHelper = new DBHelper();
     
        //public string getMAXID(string tableName, string columnName, string fm9, string ConnString)
        //{
        //    string MAXID = "";
        //    string QueryString = "select to_char((select NVL(MAX(" + columnName + "),0)+1 from " + tableName + " ), '" + fm9 + "') id from dual";
        //    using (OracleConnection oracleConnection = new OracleConnection(dbConnection.SAConnStrReader(ConnString)))
        //    {
        //        oracleConnection.Open();
        //        using (OracleCommand oracleCommand = new OracleCommand(QueryString, oracleConnection))
        //        {
        //            using (OracleDataReader rdr = oracleCommand.ExecuteReader())
        //            {
        //                if (rdr.Read())
        //                {
        //                    MAXID = rdr[0].ToString();
        //                }
        //            }
        //        }
        //    }
        //    return MAXID;
        //}
    }
}