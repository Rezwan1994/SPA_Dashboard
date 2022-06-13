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
    public class DisplayProgramParticipationDAL : ReturnData
    {

        private readonly DBHelper _dbHelper = new DBHelper();
        private readonly ErrorLogger _errorLogger = new ErrorLogger();
        private static readonly DBConnection DBConnection = new DBConnection();
        public readonly string ConnString = DBConnection.SAConnStrReader("Oracle", "PHARMAERP");




        public List<DisplayProgramParticipationBEL> GetDisplayProgramParticipation(string dCode, string rCode, string aCode, string tCode, string cCode, string dProgramNo)
        {
            try
            {
                string qry =  " SELECT" +
                              " DIVISION_CODE," +
                              " DIVISION_NAME," +
                              " REGION_CODE," +
                              " REGION_NAME," +
                              " AREA_CODE," +
                              " AREA_NAME," +
                              " TERRITORY_CODE," +
                              " TERRITORY_NAME," +
                              " CUSTOMER_CODE," +
                              " CUSTOMER_NAME," +
                              " DB_LOCATION," +
                              " MARKET_CODE," +
                              " MARKET_NAME," +
                              " ROUTE_CODE," +
                              " ROUTE_NAME," +
                              " RETAILER_CODE," +
                              " RETAILER_NAME," +
                              " RETAILER_ADDRESS," +
                              " RETAILER_CONTACT_NO," +
                              " DISPLAY_PROGRAM_NO," +
                              " DISPLAY_PROGRAM_NAME," +
                              " PARTICIPATION_MONTH_CODE," +
                              " PARTICIPATION_MONTH," +
                              " DECODE(PARTICIPATION_STATUS,'A','Active','I','Inactive') PARTICIPATION_STATUS," +
                              " TO_CHAR(DISCONTINUE_DATE,'MM/DD/RRRR')DISCONTINUE_DATE," +
                              " DISCONTINUE_REASON," +
                              " TO_CHAR(PARTICIPATE_DATE,'MM/DD/RRRR')PARTICIPATE_DATE," +
                              " INVOICE_NO," +
                              " TO_CHAR(INVOICE_DATE,'MM/DD/RRRR') INVOICE_DATE," +
                              " PRODUCT_CODE," +
                              " PRODUCT_NAME," +
                              " PACK_SIZE," +
                              " TRADE_PRICE," +
                              " ISSUED_QTY," +
                              " RETURN_QTY," +
                              " IMS_QTY " +
                              " FROM MV_DISPLAY_PROG_PARTICIPATION " +
                              " WHERE (DIVISION_CODE = '" + dCode + "' OR '" + dCode + "' = 'ALL')" +
                              " AND   (REGION_CODE='" + rCode + "' OR '" + rCode + "'='ALL')" +
                              " AND   (AREA_CODE='" + aCode + "' OR '" + aCode + "'='ALL')" +
                              " AND   (TERRITORY_CODE='" + tCode + "' OR '" + tCode + "'='ALL')" +
                              " AND   (CUSTOMER_CODE='" + cCode + "' OR '" + cCode + "'='ALL')" +
                              " AND   (DISPLAY_PROGRAM_NO='" + dProgramNo + "' OR '" + dProgramNo + "'='ALL')" +
                              " ORDER BY DIVISION_CODE,REGION_CODE, AREA_CODE, TERRITORY_CODE, CUSTOMER_CODE,MARKET_CODE,ROUTE_CODE,RETAILER_CODE,PRODUCT_CODE";


                //DataTable dt = _dbHelper.GetDataTable(qry);
                DataTable dt = _dbHelper.GetDataTableWithAuditTrial(qry, "Display Program Participation");
                int count = 0;
                var item = (from DataRow row in dt.Rows
                            select new DisplayProgramParticipationBEL
                            {
                                SlNo = ++count,
                                DivisionCode = row["DIVISION_CODE"].ToString(),
                                DivisionName = row["DIVISION_NAME"].ToString(),
                                RegionCode = row["REGION_CODE"].ToString(),
                                RegionName = row["REGION_NAME"].ToString(),
                                AreaCode = row["AREA_CODE"].ToString(),
                                AreaName = row["AREA_NAME"].ToString(),
                                TerritoryCode = row["TERRITORY_CODE"].ToString(),
                                TerritoryName = row["TERRITORY_NAME"].ToString(),
                                MarketCode = row["MARKET_CODE"].ToString(),
                                MarketName = row["MARKET_NAME"].ToString(),
                                CustomerCode = row["CUSTOMER_CODE"].ToString(),
                                CustomerName = row["CUSTOMER_NAME"].ToString(),
                                DbLocation = row["DB_LOCATION"].ToString(),
                                RouteCode = row["ROUTE_CODE"].ToString(),
                                RouteName = row["ROUTE_NAME"].ToString(),
                                RetailerCode = row["RETAILER_CODE"].ToString(),
                                RetailerName = row["RETAILER_NAME"].ToString(),
                                RetailerAddress = row["RETAILER_ADDRESS"].ToString(),
                                RetailerContactNo = row["RETAILER_CONTACT_NO"].ToString(),
                                DisplayProgramNo = row["DISPLAY_PROGRAM_NO"].ToString(),
                                DisplayProgramName = row["DISPLAY_PROGRAM_NAME"].ToString(),
                                ParticipationMonthCode = row["PARTICIPATION_MONTH_CODE"].ToString(),
                                ParticipationMonth = row["PARTICIPATION_MONTH"].ToString(),
                                ParticipationStatus = row["PARTICIPATION_STATUS"].ToString(),
                                DiscontinueDate = row["DISCONTINUE_DATE"].ToString(),
                                DiscontinueReason = row["DISCONTINUE_REASON"].ToString(),
                                ParticipateDate = row["PARTICIPATE_DATE"].ToString(),
                                InvoiceNo = row["INVOICE_NO"].ToString(),
                                InvoiceDate = row["INVOICE_DATE"].ToString(),
                                ProductCode = row["PRODUCT_CODE"].ToString(),
                                ProductName = row["PRODUCT_NAME"].ToString(),
                                PackSize = row["PACK_SIZE"].ToString(),
                                ProductPrice = Convert.ToInt32(row["TRADE_PRICE"]),
                                IssuedQty = Convert.ToInt32(row["ISSUED_QTY"]),
                                ReturnQty = Convert.ToInt32(row["RETURN_QTY"]),
                                ImsQty = Convert.ToInt32(row["IMS_QTY"]),
                            }).ToList();
                return item;
            }
            catch (Exception e)
            {
                var lineNum = e.StackTrace.Substring(e.StackTrace.LastIndexOf(' '));
                _errorLogger.GetErrorMessage(e.Message, "RouteWiseImsDAL", lineNum);
                ExceptionReturn = e.Message;
                throw;
            }
        }




    }


}
