using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;

namespace SalesWeb.Universal.Gateway
{
    public class DBConnection
    {
        string connectionString = "";
        public DBConnection()
        {
            //SAConnStrReader("Dashboard");
        }

        public string SAConnStrReader(string dbType, string companyType)
        {
            connectionString = ConfigurationManager.ConnectionStrings["Conn" + companyType + dbType].ToString();
            return connectionString;
        }


    }
}
