using System;
using System.Data;
using System.Linq;
using System.Web;
using SalesWeb.Areas.SpaMisReport.Models.BEL;
using SalesWeb.Universal.Gateway;
using System.Collections.Generic;
using System.Data.OracleClient;

namespace SalesWeb.Areas.SpaMisReport.Models.DAL
{
    public class FsoBrandRelationDAL : ReturnData
    {
        private readonly DBHelper _dbHelper = new DBHelper();
        private readonly ErrorLogger _errorLogger = new ErrorLogger();
        private static readonly DBConnection DBConnection = new DBConnection();
        public readonly string ConnString = DBConnection.SAConnStrReader("Oracle", "PHARMAERP");

        public List<FsoBrandRelationBEL> GetFsoBrandRelation(string dCode, string rCode, string aCode, string tCode, string cCode)
        {
            try
            {


                string qry = " SELECT DIVISION_CODE, DIVISION_NAME, REGION_CODE, REGION_NAME, AREA_CODE, AREA_NAME, TERRITORY_CODE, TERRITORY_NAME, MARKET_CODE, MARKET_NAME, CUSTOMER_CODE, CUSTOMER_NAME, DB_LOCATION, FSO_CODE,FSO_NAME,STATUS, BRAND_CODE, BRAND_NAME " +
                             " FROM MV_FSO_BRAND_REALTION " +
                             " WHERE (DIVISION_CODE = '" + dCode + "' OR '" + dCode + "' = 'ALL')" +
                             " AND   (REGION_CODE='" + rCode + "' OR '" + rCode + "'='ALL')" +
                             " AND   (AREA_CODE='" + aCode + "' OR '" + aCode + "'='ALL')" +
                             " AND   (TERRITORY_CODE='" + tCode + "' OR '" + tCode + "'='ALL')" +
                             " AND   (CUSTOMER_CODE='" + cCode + "' OR '" + cCode + "'='ALL')" +
                             " ORDER BY DIVISION_CODE,REGION_CODE, AREA_CODE, TERRITORY_CODE, CUSTOMER_CODE,MARKET_CODE,FSO_CODE";



  
                DataTable dt = _dbHelper.GetDataTableWithAuditTrial(qry, "FSO Brand Relation");

                var item = (from DataRow row in dt.Rows
                            select new FsoBrandRelationBEL
                            {

                                DivisionCode = row["DIVISION_CODE"].ToString(),
                                DivisionName = row["DIVISION_NAME"].ToString(),
                                RegionCode = row["REGION_CODE"].ToString(),
                                RegionName = row["REGION_NAME"].ToString(),
                                AreaCode = row["AREA_CODE"].ToString(),
                                AreaName = row["AREA_NAME"].ToString(),
                                TerritoryCode = row["TERRITORY_CODE"].ToString(),
                                TerritoryName = row["TERRITORY_NAME"].ToString(),
                                CustomerCode = row["CUSTOMER_CODE"].ToString(),
                                CustomerName = row["CUSTOMER_NAME"].ToString(),
                                DbLocation = row["DB_LOCATION"].ToString(),
                                MarketCode = row["MARKET_CODE"].ToString(),
                                MarketName = row["MARKET_NAME"].ToString(),
                                FsoCode = row["FSO_CODE"].ToString(),
                                FsoName = row["FSO_NAME"].ToString(),
                                Status = row["STATUS"].ToString(),
                                BrandCode = row["BRAND_CODE"].ToString(),
                                BrandName = row["BRAND_NAME"].ToString(),

                            }).ToList();
                return item;
            }
            catch (Exception e)
            {
                var lineNum = e.StackTrace.Substring(e.StackTrace.LastIndexOf(' '));
                _errorLogger.GetErrorMessage(e.Message, "FsoBrandRelationDAL", lineNum);
                ExceptionReturn = e.Message;
                throw;
            }
        }




    }
}