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
    public class DistSrSalesDAL : ReturnData
    {


        private readonly DBHelper _dbHelper = new DBHelper();
        private readonly ErrorLogger _errorLogger = new ErrorLogger();
        private static readonly DBConnection DBConnection = new DBConnection();
        public readonly string ConnString = DBConnection.SAConnStrReader("Oracle", "PHARMAERP");



        public object GetSrProductImsToday(string dCode, string rCode, string aCode, string tCode, string cCode, string sCode)
        {
            try
            {
                using (OracleConnection objConn = new OracleConnection(ConnString))
                {
                    using (OracleCommand objCmd = new OracleCommand())
                    {
                        objCmd.Connection = objConn;
                        objCmd.CommandText = "FN_SR_PROD_IMS_TODAY";
                        objCmd.CommandType = CommandType.StoredProcedure;


                        objCmd.Parameters.Add("pdivision_code", OracleType.VarChar).Value = dCode;
                        objCmd.Parameters.Add("pregion_code", OracleType.VarChar).Value = rCode;
                        objCmd.Parameters.Add("parea_code", OracleType.VarChar).Value = aCode;
                        objCmd.Parameters.Add("pterritory_code", OracleType.VarChar).Value = tCode;
                        objCmd.Parameters.Add("pcustomer_code", OracleType.VarChar).Value = cCode;
                        objCmd.Parameters.Add("psr_code", OracleType.VarChar).Value = sCode;


                        objCmd.Parameters.Add("return_value", OracleType.Cursor).Direction = ParameterDirection.ReturnValue;
                        objConn.Open();
                        objCmd.ExecuteNonQuery();
                        OracleDataReader rdr = objCmd.ExecuteReader();
                        DataTable dt = new DataTable();
                        if (rdr.HasRows)
                        {
                            dt.Load(rdr);
                        }
                        _dbHelper.InsertReportAudit("SR Wise Product IMS(Tpday)");
                        //int count = 0;


                        List<DistSrSalesBEL> item;
                        item = (from DataRow row in dt.Rows
                                select new DistSrSalesBEL
                                {
                                    //SlNo = ++count,
                                    DivisionCode = row["DIVISION_CODE"].ToString(),
                                    DivisionName = row["DIVISION_NAME"].ToString(),
                                    RegionCode = row["REGION_CODE"].ToString(),
                                    RegionName = row["REGION_NAME"].ToString(),
                                    AreaCode = row["AREA_CODE"].ToString(),
                                    AreaName = row["AREA_NAME"].ToString(),
                                    TerritoryCode = row["TERRITORY_CODE"].ToString(),
                                    TerritoryName = row["TERRITORY_NAME"].ToString(),
                                    DBLoaction = row["DB_LOCATION"].ToString(),
                                    CustomerCode = row["CUSTOMER_CODE"].ToString(),
                                    CustomerName = row["CUSTOMER_NAME"].ToString(),
                                    EmployeeCode = row["EMPLOYEE_CODE"].ToString(),
                                    EmployeeName = row["EMPLOYEE_NAME"].ToString(),
                                    MarketCode = row["MARKET_CODE"].ToString(),
                                    MarketName = row["MARKET_NAME"].ToString(),
                                    ProductCode = row["PRODUCT_CODE"].ToString(),
                                    ProductName = row["PRODUCT_NAME"].ToString(),
                                    PackSize = row["PACK_SIZE"].ToString(),
                                    ProductPrice = Convert.ToDouble(row["PRODUCT_PRICE"]),
                                    InvoiceQty = Convert.ToInt32(row["INVOICE_QTY"]),
                                    InvBonusQty = Convert.ToInt32(row["INV_BONUS_QTY"]),
                                    BonusPriceDiscount = Convert.ToDouble(row["BONUS_PRICE_DISCOUNT"]),
                                    IMSSalesQty = Convert.ToInt32(row["IMS_SALES_QTY"]),
                                    IMSBnsQty = Convert.ToInt32(row["IMS_BNS_QTY"]),
                                    BnsDiscRet = Convert.ToDouble(row["BNS_DISC_RET"]),
                                    InvoiceAmt = Convert.ToDouble(row["INVOICE_AMT"]),
                                    ReturnSalesQty = Convert.ToInt32(row["RETURN_SALES_QTY"]),
                                    ReturnBnsQty = Convert.ToInt32(row["RETURN_BNS_QTY"]),
                                    ReturnValue = Convert.ToDouble(row["RETURN_VALUE"]),
                                    IMSSalesVal = Convert.ToDouble(row["IMS_SALES_VAL"]),
                                    IMSBnsVal = Convert.ToDouble(row["IMS_BNS_VAL"]),
                                    NetIMS = Convert.ToDouble(row["NET_IMS"]),
                                    BonusPer = Convert.ToDouble(row["BONUS_PER"])

                                }).ToList();
                        return item;
                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return ExceptionReturn = "";
            }

        }
        public object GetSrProductImsDateRange(string fDate, string tDate, string dCode, string rCode, string aCode, string tCode, string cCode, string sCode)
        {
            try
            {
                using (OracleConnection objConn = new OracleConnection(ConnString))
                {
                    using (OracleCommand objCmd = new OracleCommand())
                    {
                        objCmd.Connection = objConn;
                        objCmd.CommandText = "FN_SR_PROD_IMS_DATE_RANGE";
                        objCmd.CommandType = CommandType.StoredProcedure;
                        objCmd.Parameters.Add("pfrom_date", OracleType.VarChar).Value = fDate;
                        objCmd.Parameters.Add("pto_date", OracleType.VarChar).Value = tDate;
                        objCmd.Parameters.Add("pdivision_code", OracleType.VarChar).Value = dCode;
                        objCmd.Parameters.Add("pregion_code", OracleType.VarChar).Value = rCode;
                        objCmd.Parameters.Add("parea_code", OracleType.VarChar).Value = aCode;
                        objCmd.Parameters.Add("pterritory_code", OracleType.VarChar).Value = tCode;
                        objCmd.Parameters.Add("pcustomer_code", OracleType.VarChar).Value = cCode;
                        objCmd.Parameters.Add("psr_code", OracleType.VarChar).Value = sCode;


                        objCmd.Parameters.Add("return_value", OracleType.Cursor).Direction = ParameterDirection.ReturnValue;
                        objConn.Open();
                        objCmd.ExecuteNonQuery();
                        OracleDataReader rdr = objCmd.ExecuteReader();
                        DataTable dt = new DataTable();
                        if (rdr.HasRows)
                        {
                            dt.Load(rdr);
                        }
                        _dbHelper.InsertReportAudit("SR Wise Product IMS(Custom Date)");

                        List<DistSrSalesBEL> item;
                        item = (from DataRow row in dt.Rows
                                select new DistSrSalesBEL
                                {
                                    //SlNo = ++count,
                                    DivisionCode = row["DIVISION_CODE"].ToString(),
                                    DivisionName = row["DIVISION_NAME"].ToString(),
                                    RegionCode = row["REGION_CODE"].ToString(),
                                    RegionName = row["REGION_NAME"].ToString(),
                                    AreaCode = row["AREA_CODE"].ToString(),
                                    AreaName = row["AREA_NAME"].ToString(),
                                    TerritoryCode = row["TERRITORY_CODE"].ToString(),
                                    TerritoryName = row["TERRITORY_NAME"].ToString(),
                                    DBLoaction = row["DB_LOCATION"].ToString(),
                                    CustomerCode = row["CUSTOMER_CODE"].ToString(),
                                    CustomerName = row["CUSTOMER_NAME"].ToString(),
                                    EmployeeCode = row["EMPLOYEE_CODE"].ToString(),
                                    EmployeeName = row["EMPLOYEE_NAME"].ToString(),
                                    MarketCode = row["MARKET_CODE"].ToString(),
                                    MarketName = row["MARKET_NAME"].ToString(),
                                    ProductCode = row["PRODUCT_CODE"].ToString(),
                                    ProductName = row["PRODUCT_NAME"].ToString(),
                                    PackSize = row["PACK_SIZE"].ToString(),
                                    ProductPrice = Convert.ToDouble(row["PRODUCT_PRICE"]),
                                    InvoiceQty = Convert.ToInt32(row["INVOICE_QTY"]),
                                    InvBonusQty = Convert.ToInt32(row["INV_BONUS_QTY"]),
                                    BonusPriceDiscount = Convert.ToDouble(row["BONUS_PRICE_DISCOUNT"]),
                                    IMSSalesQty = Convert.ToInt32(row["IMS_SALES_QTY"]),
                                    IMSBnsQty = Convert.ToInt32(row["IMS_BNS_QTY"]),
                                    BnsDiscRet = Convert.ToDouble(row["BNS_DISC_RET"]),
                                    InvoiceAmt = Convert.ToDouble(row["INVOICE_AMT"]),
                                    ReturnSalesQty = Convert.ToInt32(row["RETURN_SALES_QTY"]),
                                    ReturnBnsQty = Convert.ToInt32(row["RETURN_BNS_QTY"]),
                                    ReturnValue = Convert.ToDouble(row["RETURN_VALUE"]),
                                    IMSSalesVal = Convert.ToDouble(row["IMS_SALES_VAL"]),
                                    IMSBnsVal = Convert.ToDouble(row["IMS_BNS_VAL"]),
                                    NetIMS = Convert.ToDouble(row["NET_IMS"]),
                                    BonusPer = Convert.ToDouble(row["BONUS_PER"])

                                }).ToList();
                        return item;
                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return ExceptionReturn = "";
            }

        }
        public List<DistSrSalesBEL> GetSrProductImsYesterday(string dCode, string rCode, string aCode, string tCode, string cCode, string sCode)
        {
            try
            {
                string qry = " SELECT DIVISION_CODE, DIVISION_NAME, REGION_CODE, REGION_NAME, AREA_CODE, AREA_NAME, TERRITORY_CODE, TERRITORY_NAME, DB_LOCATION, CUSTOMER_CODE, CUSTOMER_NAME, EMPLOYEE_CODE, EMPLOYEE_NAME, MARKET_CODE, MARKET_NAME, PRODUCT_CODE, PRODUCT_NAME, PACK_SIZE, PRODUCT_PRICE, INVOICE_QTY, INV_BONUS_QTY, BONUS_PRICE_DISCOUNT, IMS_SALES_QTY, IMS_BNS_QTY, BNS_DISC_RET, INVOICE_AMT, RETURN_SALES_QTY, RETURN_BNS_QTY, RETURN_VALUE, IMS_SALES_VAL, IMS_BNS_VAL, NET_IMS, BONUS_PER " +
                              " FROM MV_SR_PRODUCT_IMS_YESTERDAY " +
                              " WHERE (DIVISION_CODE = '" + dCode + "' OR '" + dCode + "' = 'ALL')" +
                              " AND   (REGION_CODE='" + rCode + "' OR '" + rCode + "'='ALL')" +
                              " AND   (AREA_CODE='" + aCode + "' OR '" + aCode + "'='ALL')" +
                              " AND   (TERRITORY_CODE='" + tCode + "' OR '" + tCode + "'='ALL')" +
                              " AND   (CUSTOMER_CODE='" + cCode + "' OR '" + cCode + "'='ALL')" +
                              " AND   (EMPLOYEE_CODE='" + sCode + "' OR '" + sCode + "'='ALL')" +
                              " ORDER BY DIVISION_CODE,REGION_CODE, AREA_CODE, TERRITORY_CODE, CUSTOMER_CODE, EMPLOYEE_CODE, MARKET_CODE, PRODUCT_CODE";


                //DataTable dt = _dbHelper.GetDataTable(qry);
                DataTable dt = _dbHelper.GetDataTableWithAuditTrial(qry, "SR Wise Product IMS(Yesterday)");
                var item = (from DataRow row in dt.Rows
                            select new DistSrSalesBEL
                            {
                                //SlNo = ++count,
                                DivisionCode = row["DIVISION_CODE"].ToString(),
                                DivisionName = row["DIVISION_NAME"].ToString(),
                                RegionCode = row["REGION_CODE"].ToString(),
                                RegionName = row["REGION_NAME"].ToString(),
                                AreaCode = row["AREA_CODE"].ToString(),
                                AreaName = row["AREA_NAME"].ToString(),
                                TerritoryCode = row["TERRITORY_CODE"].ToString(),
                                TerritoryName = row["TERRITORY_NAME"].ToString(),
                                DBLoaction = row["DB_LOCATION"].ToString(),
                                CustomerCode = row["CUSTOMER_CODE"].ToString(),
                                CustomerName = row["CUSTOMER_NAME"].ToString(),
                                EmployeeCode = row["EMPLOYEE_CODE"].ToString(),
                                EmployeeName = row["EMPLOYEE_NAME"].ToString(),
                                MarketCode = row["MARKET_CODE"].ToString(),
                                MarketName = row["MARKET_NAME"].ToString(),
                                ProductCode = row["PRODUCT_CODE"].ToString(),
                                ProductName = row["PRODUCT_NAME"].ToString(),
                                PackSize = row["PACK_SIZE"].ToString(),
                                ProductPrice = Convert.ToDouble(row["PRODUCT_PRICE"]),
                                InvoiceQty = Convert.ToInt32(row["INVOICE_QTY"]),
                                InvBonusQty = Convert.ToInt32(row["INV_BONUS_QTY"]),
                                BonusPriceDiscount = Convert.ToDouble(row["BONUS_PRICE_DISCOUNT"]),
                                IMSSalesQty = Convert.ToInt32(row["IMS_SALES_QTY"]),
                                IMSBnsQty = Convert.ToInt32(row["IMS_BNS_QTY"]),
                                BnsDiscRet = Convert.ToDouble(row["BNS_DISC_RET"]),
                                InvoiceAmt = Convert.ToDouble(row["INVOICE_AMT"]),
                                ReturnSalesQty = Convert.ToInt32(row["RETURN_SALES_QTY"]),
                                ReturnBnsQty = Convert.ToInt32(row["RETURN_BNS_QTY"]),
                                ReturnValue = Convert.ToDouble(row["RETURN_VALUE"]),
                                IMSSalesVal = Convert.ToDouble(row["IMS_SALES_VAL"]),
                                IMSBnsVal = Convert.ToDouble(row["IMS_BNS_VAL"]),
                                NetIMS = Convert.ToDouble(row["NET_IMS"]),
                                BonusPer = Convert.ToDouble(row["BONUS_PER"])
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
        public List<DistSrSalesBEL> GetSrProductImsLastSevendays(string dCode, string rCode, string aCode, string tCode, string cCode, string sCode)
        {
            try
            {
                string qry = " SELECT DIVISION_CODE, DIVISION_NAME, REGION_CODE, REGION_NAME, AREA_CODE, AREA_NAME, TERRITORY_CODE, TERRITORY_NAME, DB_LOCATION, CUSTOMER_CODE, CUSTOMER_NAME, EMPLOYEE_CODE, EMPLOYEE_NAME, MARKET_CODE, MARKET_NAME, PRODUCT_CODE, PRODUCT_NAME, PACK_SIZE, PRODUCT_PRICE, INVOICE_QTY, INV_BONUS_QTY, BONUS_PRICE_DISCOUNT, IMS_SALES_QTY, IMS_BNS_QTY, BNS_DISC_RET, INVOICE_AMT, RETURN_SALES_QTY, RETURN_BNS_QTY, RETURN_VALUE, IMS_SALES_VAL, IMS_BNS_VAL, NET_IMS, BONUS_PER " +
                              " FROM MV_SR_PRODUCT_IMS_LAST_7DAYS " +
                              " WHERE (DIVISION_CODE = '" + dCode + "' OR '" + dCode + "' = 'ALL')" +
                              " AND   (REGION_CODE='" + rCode + "' OR '" + rCode + "'='ALL')" +
                              " AND   (AREA_CODE='" + aCode + "' OR '" + aCode + "'='ALL')" +
                              " AND   (TERRITORY_CODE='" + tCode + "' OR '" + tCode + "'='ALL')" +
                              " AND   (CUSTOMER_CODE='" + cCode + "' OR '" + cCode + "'='ALL')" +
                              " AND   (EMPLOYEE_CODE='" + sCode + "' OR '" + sCode + "'='ALL')" +
                              " ORDER BY DIVISION_CODE,REGION_CODE, AREA_CODE, TERRITORY_CODE, CUSTOMER_CODE, EMPLOYEE_CODE, MARKET_CODE, PRODUCT_CODE";


                //DataTable dt = _dbHelper.GetDataTable(qry);
                DataTable dt = _dbHelper.GetDataTableWithAuditTrial(qry, "SR Wise Product IMS(Last Seven Days)");
                //int count = 0;
                var item = (from DataRow row in dt.Rows
                            select new DistSrSalesBEL
                            {
                                //SlNo = ++count,
                                DivisionCode = row["DIVISION_CODE"].ToString(),
                                DivisionName = row["DIVISION_NAME"].ToString(),
                                RegionCode = row["REGION_CODE"].ToString(),
                                RegionName = row["REGION_NAME"].ToString(),
                                AreaCode = row["AREA_CODE"].ToString(),
                                AreaName = row["AREA_NAME"].ToString(),
                                TerritoryCode = row["TERRITORY_CODE"].ToString(),
                                TerritoryName = row["TERRITORY_NAME"].ToString(),
                                DBLoaction = row["DB_LOCATION"].ToString(),
                                CustomerCode = row["CUSTOMER_CODE"].ToString(),
                                CustomerName = row["CUSTOMER_NAME"].ToString(),
                                EmployeeCode = row["EMPLOYEE_CODE"].ToString(),
                                EmployeeName = row["EMPLOYEE_NAME"].ToString(),
                                MarketCode = row["MARKET_CODE"].ToString(),
                                MarketName = row["MARKET_NAME"].ToString(),
                                ProductCode = row["PRODUCT_CODE"].ToString(),
                                ProductName = row["PRODUCT_NAME"].ToString(),
                                PackSize = row["PACK_SIZE"].ToString(),
                                ProductPrice = Convert.ToDouble(row["PRODUCT_PRICE"]),
                                InvoiceQty = Convert.ToInt32(row["INVOICE_QTY"]),
                                InvBonusQty = Convert.ToInt32(row["INV_BONUS_QTY"]),
                                BonusPriceDiscount = Convert.ToDouble(row["BONUS_PRICE_DISCOUNT"]),
                                IMSSalesQty = Convert.ToInt32(row["IMS_SALES_QTY"]),
                                IMSBnsQty = Convert.ToInt32(row["IMS_BNS_QTY"]),
                                BnsDiscRet = Convert.ToDouble(row["BNS_DISC_RET"]),
                                InvoiceAmt = Convert.ToDouble(row["INVOICE_AMT"]),
                                ReturnSalesQty = Convert.ToInt32(row["RETURN_SALES_QTY"]),
                                ReturnBnsQty = Convert.ToInt32(row["RETURN_BNS_QTY"]),
                                ReturnValue = Convert.ToDouble(row["RETURN_VALUE"]),
                                IMSSalesVal = Convert.ToDouble(row["IMS_SALES_VAL"]),
                                IMSBnsVal = Convert.ToDouble(row["IMS_BNS_VAL"]),
                                NetIMS = Convert.ToDouble(row["NET_IMS"]),
                                BonusPer = Convert.ToDouble(row["BONUS_PER"])
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
        public List<DistSrSalesBEL> GetSrProductImsLastThirtydays(string dCode, string rCode, string aCode, string tCode, string cCode, string sCode)
        {
            try
            {
                string qry = " SELECT DIVISION_CODE, DIVISION_NAME, REGION_CODE, REGION_NAME, AREA_CODE, AREA_NAME, TERRITORY_CODE, TERRITORY_NAME, DB_LOCATION, CUSTOMER_CODE, CUSTOMER_NAME, EMPLOYEE_CODE, EMPLOYEE_NAME, MARKET_CODE, MARKET_NAME, PRODUCT_CODE, PRODUCT_NAME, PACK_SIZE, PRODUCT_PRICE, INVOICE_QTY, INV_BONUS_QTY, BONUS_PRICE_DISCOUNT, IMS_SALES_QTY, IMS_BNS_QTY, BNS_DISC_RET, INVOICE_AMT, RETURN_SALES_QTY, RETURN_BNS_QTY, RETURN_VALUE, IMS_SALES_VAL, IMS_BNS_VAL, NET_IMS, BONUS_PER " +
                              " FROM MV_SR_PRODUCT_IMS_LAST_30DAYS " +
                              " WHERE (DIVISION_CODE = '" + dCode + "' OR '" + dCode + "' = 'ALL')" +
                              " AND   (REGION_CODE='" + rCode + "' OR '" + rCode + "'='ALL')" +
                              " AND   (AREA_CODE='" + aCode + "' OR '" + aCode + "'='ALL')" +
                              " AND   (TERRITORY_CODE='" + tCode + "' OR '" + tCode + "'='ALL')" +
                              " AND   (CUSTOMER_CODE='" + cCode + "' OR '" + cCode + "'='ALL')" +
                              " AND   (EMPLOYEE_CODE='" + sCode + "' OR '" + sCode + "'='ALL')" +
                              " ORDER BY DIVISION_CODE,REGION_CODE, AREA_CODE, TERRITORY_CODE, CUSTOMER_CODE, EMPLOYEE_CODE, MARKET_CODE, PRODUCT_CODE";


                //DataTable dt = _dbHelper.GetDataTable(qry);
                DataTable dt = _dbHelper.GetDataTableWithAuditTrial(qry, "SR Wise Product IMS(Last Thirty Days)");
                //int count = 0;
                var item = (from DataRow row in dt.Rows
                            select new DistSrSalesBEL
                            {
                                //SlNo = ++count,
                                DivisionCode = row["DIVISION_CODE"].ToString(),
                                DivisionName = row["DIVISION_NAME"].ToString(),
                                RegionCode = row["REGION_CODE"].ToString(),
                                RegionName = row["REGION_NAME"].ToString(),
                                AreaCode = row["AREA_CODE"].ToString(),
                                AreaName = row["AREA_NAME"].ToString(),
                                TerritoryCode = row["TERRITORY_CODE"].ToString(),
                                TerritoryName = row["TERRITORY_NAME"].ToString(),
                                DBLoaction = row["DB_LOCATION"].ToString(),
                                CustomerCode = row["CUSTOMER_CODE"].ToString(),
                                CustomerName = row["CUSTOMER_NAME"].ToString(),
                                EmployeeCode = row["EMPLOYEE_CODE"].ToString(),
                                EmployeeName = row["EMPLOYEE_NAME"].ToString(),
                                MarketCode = row["MARKET_CODE"].ToString(),
                                MarketName = row["MARKET_NAME"].ToString(),
                                ProductCode = row["PRODUCT_CODE"].ToString(),
                                ProductName = row["PRODUCT_NAME"].ToString(),
                                PackSize = row["PACK_SIZE"].ToString(),
                                ProductPrice = Convert.ToDouble(row["PRODUCT_PRICE"]),
                                InvoiceQty = Convert.ToInt32(row["INVOICE_QTY"]),
                                InvBonusQty = Convert.ToInt32(row["INV_BONUS_QTY"]),
                                BonusPriceDiscount = Convert.ToDouble(row["BONUS_PRICE_DISCOUNT"]),
                                IMSSalesQty = Convert.ToInt32(row["IMS_SALES_QTY"]),
                                IMSBnsQty = Convert.ToInt32(row["IMS_BNS_QTY"]),
                                BnsDiscRet = Convert.ToDouble(row["BNS_DISC_RET"]),
                                InvoiceAmt = Convert.ToDouble(row["INVOICE_AMT"]),
                                ReturnSalesQty = Convert.ToInt32(row["RETURN_SALES_QTY"]),
                                ReturnBnsQty = Convert.ToInt32(row["RETURN_BNS_QTY"]),
                                ReturnValue = Convert.ToDouble(row["RETURN_VALUE"]),
                                IMSSalesVal = Convert.ToDouble(row["IMS_SALES_VAL"]),
                                IMSBnsVal = Convert.ToDouble(row["IMS_BNS_VAL"]),
                                NetIMS = Convert.ToDouble(row["NET_IMS"]),
                                BonusPer = Convert.ToDouble(row["BONUS_PER"])
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
        public List<DistSrSalesBEL> GetSrProductImsCurrentMonth(string dCode, string rCode, string aCode, string tCode, string cCode, string sCode)
        {
            try
            {
                string qry = " SELECT DIVISION_CODE, DIVISION_NAME, REGION_CODE, REGION_NAME, AREA_CODE, AREA_NAME, TERRITORY_CODE, TERRITORY_NAME, DB_LOCATION, CUSTOMER_CODE, CUSTOMER_NAME, EMPLOYEE_CODE, EMPLOYEE_NAME, MARKET_CODE, MARKET_NAME, PRODUCT_CODE, PRODUCT_NAME, PACK_SIZE, PRODUCT_PRICE, INVOICE_QTY, INV_BONUS_QTY, BONUS_PRICE_DISCOUNT, IMS_SALES_QTY, IMS_BNS_QTY, BNS_DISC_RET, INVOICE_AMT, RETURN_SALES_QTY, RETURN_BNS_QTY, RETURN_VALUE, IMS_SALES_VAL, IMS_BNS_VAL, NET_IMS, BONUS_PER " +
                              " FROM MV_SR_PRODUCT_IMS_CMONTH " +
                              " WHERE (DIVISION_CODE = '" + dCode + "' OR '" + dCode + "' = 'ALL')" +
                              " AND   (REGION_CODE='" + rCode + "' OR '" + rCode + "'='ALL')" +
                              " AND   (AREA_CODE='" + aCode + "' OR '" + aCode + "'='ALL')" +
                              " AND   (TERRITORY_CODE='" + tCode + "' OR '" + tCode + "'='ALL')" +
                              " AND   (CUSTOMER_CODE='" + cCode + "' OR '" + cCode + "'='ALL')" +
                              " AND   (EMPLOYEE_CODE='" + sCode + "' OR '" + sCode + "'='ALL')" +
                              " ORDER BY DIVISION_CODE,REGION_CODE, AREA_CODE, TERRITORY_CODE, CUSTOMER_CODE, EMPLOYEE_CODE, MARKET_CODE, PRODUCT_CODE";


                //DataTable dt = _dbHelper.GetDataTable(qry);
                DataTable dt = _dbHelper.GetDataTableWithAuditTrial(qry, "SR Wise Product IMS(Current Month)");
                //int count = 0;
                var item = (from DataRow row in dt.Rows
                            select new DistSrSalesBEL
                            {
                                //SlNo = ++count,
                                DivisionCode = row["DIVISION_CODE"].ToString(),
                                DivisionName = row["DIVISION_NAME"].ToString(),
                                RegionCode = row["REGION_CODE"].ToString(),
                                RegionName = row["REGION_NAME"].ToString(),
                                AreaCode = row["AREA_CODE"].ToString(),
                                AreaName = row["AREA_NAME"].ToString(),
                                TerritoryCode = row["TERRITORY_CODE"].ToString(),
                                TerritoryName = row["TERRITORY_NAME"].ToString(),
                                DBLoaction = row["DB_LOCATION"].ToString(),
                                CustomerCode = row["CUSTOMER_CODE"].ToString(),
                                CustomerName = row["CUSTOMER_NAME"].ToString(),
                                EmployeeCode = row["EMPLOYEE_CODE"].ToString(),
                                EmployeeName = row["EMPLOYEE_NAME"].ToString(),
                                MarketCode = row["MARKET_CODE"].ToString(),
                                MarketName = row["MARKET_NAME"].ToString(),
                                ProductCode = row["PRODUCT_CODE"].ToString(),
                                ProductName = row["PRODUCT_NAME"].ToString(),
                                PackSize = row["PACK_SIZE"].ToString(),
                                ProductPrice = Convert.ToDouble(row["PRODUCT_PRICE"]),
                                InvoiceQty = Convert.ToInt32(row["INVOICE_QTY"]),
                                InvBonusQty = Convert.ToInt32(row["INV_BONUS_QTY"]),
                                BonusPriceDiscount = Convert.ToDouble(row["BONUS_PRICE_DISCOUNT"]),
                                IMSSalesQty = Convert.ToInt32(row["IMS_SALES_QTY"]),
                                IMSBnsQty = Convert.ToInt32(row["IMS_BNS_QTY"]),
                                BnsDiscRet = Convert.ToDouble(row["BNS_DISC_RET"]),
                                InvoiceAmt = Convert.ToDouble(row["INVOICE_AMT"]),
                                ReturnSalesQty = Convert.ToInt32(row["RETURN_SALES_QTY"]),
                                ReturnBnsQty = Convert.ToInt32(row["RETURN_BNS_QTY"]),
                                ReturnValue = Convert.ToDouble(row["RETURN_VALUE"]),
                                IMSSalesVal = Convert.ToDouble(row["IMS_SALES_VAL"]),
                                IMSBnsVal = Convert.ToDouble(row["IMS_BNS_VAL"]),
                                NetIMS = Convert.ToDouble(row["NET_IMS"]),
                                BonusPer = Convert.ToDouble(row["BONUS_PER"])
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
        public List<DistSrSalesBEL> GetSrProductImsLastMonth(string dCode, string rCode, string aCode, string tCode, string cCode, string sCode)
        {
            try
            {
                string qry = " SELECT DIVISION_CODE, DIVISION_NAME, REGION_CODE, REGION_NAME, AREA_CODE, AREA_NAME, TERRITORY_CODE, TERRITORY_NAME, DB_LOCATION, CUSTOMER_CODE, CUSTOMER_NAME, EMPLOYEE_CODE, EMPLOYEE_NAME, MARKET_CODE, MARKET_NAME, PRODUCT_CODE, PRODUCT_NAME, PACK_SIZE, PRODUCT_PRICE, INVOICE_QTY, INV_BONUS_QTY, BONUS_PRICE_DISCOUNT, IMS_SALES_QTY, IMS_BNS_QTY, BNS_DISC_RET, INVOICE_AMT, RETURN_SALES_QTY, RETURN_BNS_QTY, RETURN_VALUE, IMS_SALES_VAL, IMS_BNS_VAL, NET_IMS, BONUS_PER " +
                              " FROM MV_SR_PRODUCT_IMS_LMONTH " +
                              " WHERE (DIVISION_CODE = '" + dCode + "' OR '" + dCode + "' = 'ALL')" +
                              " AND   (REGION_CODE='" + rCode + "' OR '" + rCode + "'='ALL')" +
                              " AND   (AREA_CODE='" + aCode + "' OR '" + aCode + "'='ALL')" +
                              " AND   (TERRITORY_CODE='" + tCode + "' OR '" + tCode + "'='ALL')" +
                              " AND   (CUSTOMER_CODE='" + cCode + "' OR '" + cCode + "'='ALL')" +
                              " AND   (EMPLOYEE_CODE='" + sCode + "' OR '" + sCode + "'='ALL')" +
                              " ORDER BY DIVISION_CODE,REGION_CODE, AREA_CODE, TERRITORY_CODE, CUSTOMER_CODE, EMPLOYEE_CODE, MARKET_CODE, PRODUCT_CODE";


                //DataTable dt = _dbHelper.GetDataTable(qry);
                DataTable dt = _dbHelper.GetDataTableWithAuditTrial(qry, "SR Wise Product IMS(Last Month)");
                //int count = 0;
                var item = (from DataRow row in dt.Rows
                            select new DistSrSalesBEL
                            {
                                //SlNo = ++count,
                                DivisionCode = row["DIVISION_CODE"].ToString(),
                                DivisionName = row["DIVISION_NAME"].ToString(),
                                RegionCode = row["REGION_CODE"].ToString(),
                                RegionName = row["REGION_NAME"].ToString(),
                                AreaCode = row["AREA_CODE"].ToString(),
                                AreaName = row["AREA_NAME"].ToString(),
                                TerritoryCode = row["TERRITORY_CODE"].ToString(),
                                TerritoryName = row["TERRITORY_NAME"].ToString(),
                                DBLoaction = row["DB_LOCATION"].ToString(),
                                CustomerCode = row["CUSTOMER_CODE"].ToString(),
                                CustomerName = row["CUSTOMER_NAME"].ToString(),
                                EmployeeCode = row["EMPLOYEE_CODE"].ToString(),
                                EmployeeName = row["EMPLOYEE_NAME"].ToString(),
                                MarketCode = row["MARKET_CODE"].ToString(),
                                MarketName = row["MARKET_NAME"].ToString(),
                                ProductCode = row["PRODUCT_CODE"].ToString(),
                                ProductName = row["PRODUCT_NAME"].ToString(),
                                PackSize = row["PACK_SIZE"].ToString(),
                                ProductPrice = Convert.ToDouble(row["PRODUCT_PRICE"]),
                                InvoiceQty = Convert.ToInt32(row["INVOICE_QTY"]),
                                InvBonusQty = Convert.ToInt32(row["INV_BONUS_QTY"]),
                                BonusPriceDiscount = Convert.ToDouble(row["BONUS_PRICE_DISCOUNT"]),
                                IMSSalesQty = Convert.ToInt32(row["IMS_SALES_QTY"]),
                                IMSBnsQty = Convert.ToInt32(row["IMS_BNS_QTY"]),
                                BnsDiscRet = Convert.ToDouble(row["BNS_DISC_RET"]),
                                InvoiceAmt = Convert.ToDouble(row["INVOICE_AMT"]),
                                ReturnSalesQty = Convert.ToInt32(row["RETURN_SALES_QTY"]),
                                ReturnBnsQty = Convert.ToInt32(row["RETURN_BNS_QTY"]),
                                ReturnValue = Convert.ToDouble(row["RETURN_VALUE"]),
                                IMSSalesVal = Convert.ToDouble(row["IMS_SALES_VAL"]),
                                IMSBnsVal = Convert.ToDouble(row["IMS_BNS_VAL"]),
                                NetIMS = Convert.ToDouble(row["NET_IMS"]),
                                BonusPer = Convert.ToDouble(row["BONUS_PER"])
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



        public object GetReportTypeList1()
        {
            try
            {
                var qry = " SELECT 'ALL' DIVISION_CODE, 'ALL' DIVISION_NAME, 1 SL FROM DUAL " +
                          " UNION SELECT DISTINCT DIVISION_CODE, DIVISION_NAME,2 SL  FROM MV_UPTO_CUR_MONTH_DIV_RET_REL " +
                          " ORDER BY SL, DIVISION_NAME";
                DataTable dt = _dbHelper.GetDataTable(qry);
                var item = (from DataRow row in dt.Rows
                            select new DivisionInfoBEL
                            {
                                DivisionCode = row["DIVISION_CODE"].ToString(),
                                DivisionName = row["DIVISION_NAME"].ToString()
                            }).ToList();
                return item;

            }
            catch (Exception e)
            {
                var lineNum = e.StackTrace.Substring(e.StackTrace.LastIndexOf(' '));
                _errorLogger.GetErrorMessage(e.Message, "DistWiseSrSalesDAL", lineNum);
                ExceptionReturn = e.Message;
                return "Not Ok";
            }
        }
        public object GetReportTypeList(string SlNo)
        {
            try
            {

                var qry = " SELECT REPORT_TYPE_NAME,REPORT_TYPE_VALUE,REPORT_NAME" +
                           " FROM VW_REPORT_TYPE "+
                           " WHERE SLNO IN " + SlNo +
                           " ORDER BY SLNO ";


                DataTable dt = _dbHelper.GetDataTable(qry);
                var item = (from DataRow row in dt.Rows
                            select new ReportTypeBEL
                            {
                                ReportTypeName = row["REPORT_TYPE_NAME"].ToString(),
                                ReportTypeValue = row["REPORT_TYPE_VALUE"].ToString(),
                                ReportName = row["REPORT_NAME"].ToString()
                            }).ToList();
                return item;

            }
            catch (Exception e)
            {
                var lineNum = e.StackTrace.Substring(e.StackTrace.LastIndexOf(' '));
                _errorLogger.GetErrorMessage(e.Message, "RouteWiseImsDAL", lineNum);
                ExceptionReturn = e.Message;
                return "Not Ok";
            }
        }  
        public object GetDivisionList()
        {
            try
            {
                var qry = " SELECT 'ALL' DIVISION_CODE, 'ALL' DIVISION_NAME, 1 SL FROM DUAL " +
                          " UNION SELECT DISTINCT DIVISION_CODE, DIVISION_NAME,2 SL  FROM MV_UPTO_CUR_MONTH_DIV_RET_REL " +
                          " ORDER BY SL, DIVISION_NAME";
                DataTable dt = _dbHelper.GetDataTable(qry);
                var item = (from DataRow row in dt.Rows
                            select new DivisionInfoBEL
                            {
                                DivisionCode = row["DIVISION_CODE"].ToString(),
                                DivisionName = row["DIVISION_NAME"].ToString()
                            }).ToList();
                return item;

            }
            catch (Exception e)
            {
                var lineNum = e.StackTrace.Substring(e.StackTrace.LastIndexOf(' '));
                _errorLogger.GetErrorMessage(e.Message, "DistWiseSrSalesDAL", lineNum);
                ExceptionReturn = e.Message;
                return "Not Ok";
            }
        }
        public object GetRegionList(string dCode)
        {
            try
            {
                string qry = " SELECT 'ALL' REGION_CODE, 'ALL' REGION_NAME, 1 SL FROM DUAL " +
                             " UNION " +
                             " SELECT DISTINCT REGION_CODE, REGION_NAME, 2 SL FROM MV_UPTO_CUR_MONTH_DIV_RET_REL" +
                             " WHERE (DIVISION_CODE='" + dCode + "' OR '" + dCode + "' ='ALL')" +
                             " ORDER BY SL,REGION_NAME";
                DataTable dt = _dbHelper.GetDataTable(qry);
                var item = (from DataRow row in dt.Rows
                            select new RegionInfoBEL
                            {

                                RegionCode = row["REGION_CODE"].ToString(),
                                RegionName = row["REGION_NAME"].ToString()

                            }).ToList();
                return item;

            }
            catch (Exception e)
            {
                var lineNum = e.StackTrace.Substring(e.StackTrace.LastIndexOf(' '));
                _errorLogger.GetErrorMessage(e.Message, "DistWiseSrSalesDAL", lineNum);
                ExceptionReturn = e.Message;
                return "Not Ok";
            }
        }
        public object GetAreaList(string dCode,string rCode)
        {
            try
            {
                string qry = " SELECT 'ALL' AREA_CODE, 'ALL' AREA_NAME, 1 SL FROM DUAL " +
                             " UNION " +
                             " SELECT DISTINCT AREA_CODE, AREA_NAME, 2 SL FROM MV_UPTO_CUR_MONTH_DIV_RET_REL" +
                             " WHERE (DIVISION_CODE='" + dCode + "' OR '" + dCode + "' ='ALL')" + 
                             " AND   (REGION_CODE='" + rCode + "' OR '" + rCode + "'='ALL')" +
                             " ORDER BY SL, AREA_NAME"; ;
                DataTable dt = _dbHelper.GetDataTable(qry);
                var item = (from DataRow row in dt.Rows
                            select new AreaInfoBEL
                            {
                                AreaCode = row["AREA_CODE"].ToString(),
                                AreaName = row["AREA_NAME"].ToString(),

                            }).ToList();
                return item;

            }
            catch (Exception e)
            {
                var lineNum = e.StackTrace.Substring(e.StackTrace.LastIndexOf(' '));
                _errorLogger.GetErrorMessage(e.Message, "DistWiseSrSalesDAL", lineNum);
                ExceptionReturn = e.Message;
                return "Not Ok";
            }
        }
        public object GetTerritoryList(string dCode, string rCode,string aCode)
        {
            try
            {
                string qry = " SELECT 'ALL' TERRITORY_CODE, 'ALL' TERRITORY_NAME, 1 SL FROM DUAL " +
                             " UNION " +
                             " SELECT DISTINCT TERRITORY_CODE, TERRITORY_NAME, 2 SL FROM MV_UPTO_CUR_MONTH_DIV_RET_REL" +
                             " WHERE  (DIVISION_CODE = '" + dCode + "' OR '" + dCode + "' = 'ALL')" +
                             " AND   (REGION_CODE='" + rCode + "' OR '" + rCode + "'='ALL')" +
                             " AND   (AREA_CODE='" + aCode + "' OR '" + aCode + "'='ALL')" +
                             " ORDER BY SL, TERRITORY_NAME"; 



                DataTable dt = _dbHelper.GetDataTable(qry);
                var item = (from DataRow row in dt.Rows
                            select new TerritoryInfoBEL
                            {
                                TerritoryCode = row["TERRITORY_CODE"].ToString(),
                                TerritoryName = row["TERRITORY_NAME"].ToString(),

                            }).ToList();
                return item;

            }
            catch (Exception e)
            {
                var lineNum = e.StackTrace.Substring(e.StackTrace.LastIndexOf(' '));
                _errorLogger.GetErrorMessage(e.Message, "DistWiseSrSalesDAL", lineNum);
                ExceptionReturn = e.Message;
                return "Not Ok";
            }
        }
        public object GetCustomerList(string dCode, string rCode, string aCode,string tCode)
        {
            try
            {
                string qry = " SELECT 'ALL' CUSTOMER_CODE, 'ALL' CUSTOMER_NAME, 1 SL FROM DUAL " +
                             " UNION " +
                             " SELECT DISTINCT CUSTOMER_CODE, CUSTOMER_NAME, 2 SL FROM MV_UPTO_CUR_MONTH_DIV_RET_REL" +
                             " WHERE  (DIVISION_CODE = '" + dCode + "' OR '" + dCode + "' = 'ALL')" +
                             " AND   (REGION_CODE='" + rCode + "' OR '" + rCode + "'='ALL')" +
                             " AND   (AREA_CODE='" + aCode + "' OR '" + aCode + "'='ALL')" +
                             " AND   (TERRITORY_CODE='" + tCode + "' OR '" + tCode + "'='ALL')" +
                             " ORDER BY SL, CUSTOMER_NAME";


                DataTable dt = _dbHelper.GetDataTable(qry);
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
                _errorLogger.GetErrorMessage(e.Message, "DistWiseSrSalesDAL", lineNum);
                ExceptionReturn = e.Message;
                return "Not Ok";
            }
        }
        public object GetSrList(string dCode, string rCode, string aCode, string tCode,string cCode)
        {
            try
            {
                string qry = " SELECT 'ALL' SR_CODE, 'ALL' SR_NAME, 1 SL FROM DUAL " +
                             " UNION " +
                             " SELECT DISTINCT SR_CODE, SR_NAME, 2 SL FROM MV_UPTO_CUR_MONTH_DIV_RET_REL" +
                             " WHERE   (DIVISION_CODE = '" + dCode + "' OR '" + dCode + "' = 'ALL')" +
                             " AND   (REGION_CODE='" + rCode + "' OR '" + rCode + "'='ALL')" +
                             " AND   (AREA_CODE='" + aCode + "' OR '" + aCode + "'='ALL')" +
                             " AND   (TERRITORY_CODE='" + tCode + "' OR '" + tCode + "'='ALL')" +
                             " AND   (CUSTOMER_CODE='" + cCode + "' OR '" + cCode + "'='ALL')" +
                             " ORDER BY SL, SR_NAME";
                DataTable dt = _dbHelper.GetDataTable(qry);
                var item = (from DataRow row in dt.Rows
                            select new SrInfoBEL
                            {
                                SrCode = row["SR_CODE"].ToString(),
                                SrName = row["SR_NAME"].ToString(),

                            }).ToList();
                return item;

            }
            catch (Exception e)
            {
                var lineNum = e.StackTrace.Substring(e.StackTrace.LastIndexOf(' '));
                _errorLogger.GetErrorMessage(e.Message, "DistWiseSrSalesDAL", lineNum);
                ExceptionReturn = e.Message;
                return "Not Ok";
            }
        }
        public object GetDisplayProgramList(string dCode, string rCode, string aCode, string tCode, string cCode)
        {
            try
            {
                string qry = " SELECT 'ALL' DISPLAY_PROGRAM_NO, 'ALL' DISPLAY_PROGRAM_NAME, 1 SL FROM DUAL " +
                             " UNION " +
                             " SELECT DISTINCT TO_CHAR(DISPLAY_PROGRAM_NO) DISPLAY_PROGRAM_NO, DISPLAY_PROGRAM_NAME, 2 SL FROM MV_DISPLAY_PROG_PARTICIPATION" +
                             " WHERE   (DIVISION_CODE = '" + dCode + "' OR '" + dCode + "' = 'ALL')" +
                             " AND   (REGION_CODE='" + rCode + "' OR '" + rCode + "'='ALL')" +
                             " AND   (AREA_CODE='" + aCode + "' OR '" + aCode + "'='ALL')" +
                             " AND   (TERRITORY_CODE='" + tCode + "' OR '" + tCode + "'='ALL')" +
                             " AND   (CUSTOMER_CODE='" + cCode + "' OR '" + cCode + "'='ALL')" +
                             " ORDER BY SL, DISPLAY_PROGRAM_NAME";
                DataTable dt = _dbHelper.GetDataTable(qry);
                var item = (from DataRow row in dt.Rows
                            select new DisplayProgramBEL
                            {
                                DisplayProgramNo = row["DISPLAY_PROGRAM_NO"].ToString(),
                                DisplayProgramNane = row["DISPLAY_PROGRAM_NAME"].ToString()

                            }).ToList();
                return item;

            }
            catch (Exception e)
            {
                var lineNum = e.StackTrace.Substring(e.StackTrace.LastIndexOf(' '));
                _errorLogger.GetErrorMessage(e.Message, "DistWiseSrSalesDAL", lineNum);
                ExceptionReturn = e.Message;
                return "Not Ok";
            }
        }




    }
}