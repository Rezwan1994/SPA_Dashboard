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
    public class DistBonusAdjUnitSelectionDAL : ReturnData
    {
        private readonly DBHelper _dbHelper = new DBHelper();
        private readonly DBHelper2 _dbHelper2 = new DBHelper2();
        private readonly ErrorLogger _errorLogger = new ErrorLogger();

        private static readonly DBConnection DBConnection = new DBConnection();
        public readonly string ConnStringSfblMis = DBConnection.SAConnStrReader("Oracle", "PHARMAERP");
        public readonly string ConnStringSfblTrans = DBConnection.SAConnStrReader("Oracle", "SPASFBL");

    }
}