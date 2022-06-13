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
    public class DistSalesSummaryDAL:ReturnData
    {



        private readonly DBHelper _dbHelper = new DBHelper();
        private readonly ErrorLogger _errorLogger = new ErrorLogger();
        private static readonly DBConnection DBConnection = new DBConnection();
        public readonly string ConnString = DBConnection.SAConnStrReader("Oracle", "PHARMAERP");


        //Function
        public object GetDistProductImsToday(string dCode, string rCode, string aCode, string tCode, string cCode)
        {
            try
            {
                int userId = Convert.ToInt32(HttpContext.Current.Session["USER_ID"]);

                using (OracleConnection objConn = new OracleConnection(ConnString))
                {
                    using (OracleCommand objCmd = new OracleCommand())
                    {
                        objCmd.Connection = objConn;
                        objCmd.CommandText = "FN_DIST_PROD_IMS_TODAY";
                        objCmd.CommandType = CommandType.StoredProcedure;
                        objCmd.Parameters.Add("pdivision_code", OracleType.VarChar).Value = dCode;
                        objCmd.Parameters.Add("pregion_code", OracleType.VarChar).Value = rCode;
                        objCmd.Parameters.Add("parea_code", OracleType.VarChar).Value = aCode;
                        objCmd.Parameters.Add("pterritory_code", OracleType.VarChar).Value = tCode;
                        objCmd.Parameters.Add("pcustomer_code", OracleType.VarChar).Value = cCode;
                        objCmd.Parameters.Add("puser_id", OracleType.VarChar).Value = userId;

                        objCmd.Parameters.Add("return_value", OracleType.Cursor).Direction = ParameterDirection.ReturnValue;
                        objConn.Open();
                        objCmd.ExecuteNonQuery();
                        OracleDataReader rdr = objCmd.ExecuteReader();
                        DataTable dt = new DataTable();
                        if (rdr.HasRows)
                        {
                            dt.Load(rdr);
                        }
                        _dbHelper.InsertReportAudit("Distributor Wise Product IMS(Today)");
                        int count = 0;
                        List<DistSalesSummaryBEL> item;
                        item = (from DataRow row in dt.Rows
                                select new DistSalesSummaryBEL
                                {
                                    SlNo = ++count,
                                    DivisionCode = row["division_code"].ToString(),
                                    DivisionName = row["division_name"].ToString(),
                                    RegionCode = row["region_code"].ToString(),
                                    RegionName = row["region_name"].ToString(),
                                    AreaCode = row["area_code"].ToString(),
                                    AreaName = row["area_name"].ToString(),
                                    TerritoryCode = row["territory_code"].ToString(),
                                    TerritoryName = row["territory_name"].ToString(),
                                    CustomerCode = row["customer_code"].ToString(),
                                    CustomerName = row["customer_name"].ToString(),
                                    DbLocation = row["db_location"].ToString(),
                                    ProductCode = row["product_code"].ToString(),
                                    ProductName = row["product_name"].ToString(),
                                    PackSize = row["pack_size"].ToString(),
                                    ProductPrice = Convert.ToDouble(row["product_price"]),
                                    InvoiceQty = Convert.ToInt32(row["invoice_qty"]),
                                    InvBonusQty = Convert.ToInt32(row["inv_bonus_qty"]),
                                    BonusPriceDiscount = Convert.ToDouble(row["bonus_price_discount"]),
                                    ImsSalesQty = Convert.ToInt32(row["ims_sales_qty"]),
                                    ImsBnsQty = Convert.ToInt32(row["ims_bns_qty"]),
                                    InvoiceAmt = Convert.ToDouble(row["invoice_amt"]),
                                    ReturnSalesQty = Convert.ToInt32(row["return_sales_qty"]),
                                    ReturnBnsQty = Convert.ToInt32(row["return_bns_qty"]),
                                    BnsDiscRet = Convert.ToDouble(row["bns_disc_ret"]),
                                    ReturnValue = Convert.ToDouble(row["return_value"]),
                                    ImsSalesVal = Convert.ToDouble(row["ims_sales_val"]),
                                    ImsBnsVal = Convert.ToDouble(row["ims_bns_val"]),
                                    NetIms = Convert.ToDouble(row["net_ims"])

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

        //Function
        public object GetDistProductImsDateRange(string fDate, string tDate, string dCode, string rCode, string aCode, string tCode, string cCode)
        {
            try
            {
                int userId = Convert.ToInt32(HttpContext.Current.Session["USER_ID"]);

                using (OracleConnection objConn = new OracleConnection(ConnString))
                {
                    using (OracleCommand objCmd = new OracleCommand())
                    {
                        objCmd.Connection = objConn;
                        objCmd.CommandText = "FN_DIST_PROD_IMS_DATE_RANGE";
                        objCmd.CommandType = CommandType.StoredProcedure;
                        objCmd.Parameters.Add("pfrom_date", OracleType.VarChar).Value = fDate;
                        objCmd.Parameters.Add("pto_date", OracleType.VarChar).Value = tDate;
                        objCmd.Parameters.Add("pdivision_code", OracleType.VarChar).Value = dCode;
                        objCmd.Parameters.Add("pregion_code", OracleType.VarChar).Value = rCode;
                        objCmd.Parameters.Add("parea_code", OracleType.VarChar).Value = aCode;
                        objCmd.Parameters.Add("pterritory_code", OracleType.VarChar).Value = tCode;
                        objCmd.Parameters.Add("pcustomer_code", OracleType.VarChar).Value = cCode;
                        objCmd.Parameters.Add("puser_id", OracleType.VarChar).Value = userId;

                        objCmd.Parameters.Add("return_value", OracleType.Cursor).Direction = ParameterDirection.ReturnValue;
                        objConn.Open();
                        objCmd.ExecuteNonQuery();
                        OracleDataReader rdr = objCmd.ExecuteReader();
                        DataTable dt = new DataTable();
                        if (rdr.HasRows)
                        {
                            dt.Load(rdr);
                        }
                        _dbHelper.InsertReportAudit("Distributor Wise Product IMS(Custom Date)");
                        int count = 0;
                        List<DistSalesSummaryBEL> item;
                        item = (from DataRow row in dt.Rows
                                select new DistSalesSummaryBEL
                                {
                                    SlNo = ++count,
                                    DivisionCode = row["division_code"].ToString(),
                                    DivisionName = row["division_name"].ToString(),
                                    RegionCode = row["region_code"].ToString(),
                                    RegionName = row["region_name"].ToString(),
                                    AreaCode = row["area_code"].ToString(),
                                    AreaName = row["area_name"].ToString(),
                                    TerritoryCode = row["territory_code"].ToString(),
                                    TerritoryName = row["territory_name"].ToString(),
                                    CustomerCode = row["customer_code"].ToString(),
                                    CustomerName = row["customer_name"].ToString(),
                                    DbLocation = row["db_location"].ToString(),
                                    ProductCode = row["product_code"].ToString(),
                                    ProductName = row["product_name"].ToString(),
                                    PackSize = row["pack_size"].ToString(),
                                    ProductPrice = Convert.ToDouble(row["product_price"]),
                                    InvoiceQty = Convert.ToInt32(row["invoice_qty"]),
                                    InvBonusQty = Convert.ToInt32(row["inv_bonus_qty"]),
                                    BonusPriceDiscount = Convert.ToDouble(row["bonus_price_discount"]),
                                    ImsSalesQty = Convert.ToInt32(row["ims_sales_qty"]),
                                    ImsBnsQty = Convert.ToInt32(row["ims_bns_qty"]),
                                    InvoiceAmt = Convert.ToDouble(row["invoice_amt"]),
                                    ReturnSalesQty = Convert.ToInt32(row["return_sales_qty"]),
                                    ReturnBnsQty = Convert.ToInt32(row["return_bns_qty"]),
                                    BnsDiscRet = Convert.ToDouble(row["bns_disc_ret"]),
                                    ReturnValue = Convert.ToDouble(row["return_value"]),
                                    ImsSalesVal = Convert.ToDouble(row["ims_sales_val"]),
                                    ImsBnsVal = Convert.ToDouble(row["ims_bns_val"]),
                                    NetIms = Convert.ToDouble(row["net_ims"])

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

        //Query
        public List<DistSalesSummaryBEL> GetDistProductImsYesterday(string dCode, string rCode, string aCode, string tCode, string cCode)
        {
            try
            {
                string qry;

                int userId = Convert.ToInt32(HttpContext.Current.Session["USER_ID"]);
                int UserAccessCount = Convert.ToInt32(HttpContext.Current.Session["USER_BASE_REPORT_FILTER"]);

                if (UserAccessCount > 0)
                {
                    qry = " SELECT DIVISION_CODE, DIVISION_NAME, REGION_CODE, REGION_NAME, AREA_CODE, AREA_NAME, TERRITORY_CODE, TERRITORY_NAME,CUSTOMER_CODE, CUSTOMER_NAME, DB_LOCATION, PRODUCT_CODE, PRODUCT_NAME, PACK_SIZE, PRODUCT_PRICE, INVOICE_QTY, INV_BONUS_QTY, BONUS_PRICE_DISCOUNT, IMS_SALES_QTY, IMS_BNS_QTY, INVOICE_AMT, RETURN_SALES_QTY, RETURN_BNS_QTY, BNS_DISC_RET, RETURN_VALUE, IMS_SALES_VAL, IMS_BNS_VAL, NET_IMS" +
                          " FROM MV_DIST_PRODUCT_IMS_YESTERDAY " +
                          " WHERE (DIVISION_CODE = '" + dCode + "' OR '" + dCode + "' = 'ALL')" +
                          " AND   (REGION_CODE='" + rCode + "' OR '" + rCode + "'='ALL')" +
                          " AND   (AREA_CODE='" + aCode + "' OR '" + aCode + "'='ALL')" +
                          " AND   (TERRITORY_CODE='" + tCode + "' OR '" + tCode + "'='ALL')" +
                          " AND   (CUSTOMER_CODE='" + cCode + "' OR '" + cCode + "'='ALL')" +
                          " AND   PRODUCT_CODE IN (SELECT PRODUCT_CODE FROM USER_PRODUCT_DTL WHERE USER_ID=" + userId + ")" +
                          " ORDER BY DIVISION_CODE,REGION_CODE, AREA_CODE, TERRITORY_CODE, CUSTOMER_CODE,PRODUCT_CODE";
                }
                else
                {
                    qry = " SELECT DIVISION_CODE, DIVISION_NAME, REGION_CODE, REGION_NAME, AREA_CODE, AREA_NAME, TERRITORY_CODE, TERRITORY_NAME,CUSTOMER_CODE, CUSTOMER_NAME, DB_LOCATION, PRODUCT_CODE, PRODUCT_NAME, PACK_SIZE, PRODUCT_PRICE, INVOICE_QTY, INV_BONUS_QTY, BONUS_PRICE_DISCOUNT, IMS_SALES_QTY, IMS_BNS_QTY, INVOICE_AMT, RETURN_SALES_QTY, RETURN_BNS_QTY, BNS_DISC_RET, RETURN_VALUE, IMS_SALES_VAL, IMS_BNS_VAL, NET_IMS" +
                          " FROM MV_DIST_PRODUCT_IMS_YESTERDAY " +
                          " WHERE (DIVISION_CODE = '" + dCode + "' OR '" + dCode + "' = 'ALL')" +
                          " AND   (REGION_CODE='" + rCode + "' OR '" + rCode + "'='ALL')" +
                          " AND   (AREA_CODE='" + aCode + "' OR '" + aCode + "'='ALL')" +
                          " AND   (TERRITORY_CODE='" + tCode + "' OR '" + tCode + "'='ALL')" +
                          " AND   (CUSTOMER_CODE='" + cCode + "' OR '" + cCode + "'='ALL')" +
                          " ORDER BY DIVISION_CODE,REGION_CODE, AREA_CODE, TERRITORY_CODE, CUSTOMER_CODE,PRODUCT_CODE";
                }





                //DataTable dt = _dbHelper.GetDataTable(qry);
                DataTable dt = _dbHelper.GetDataTableWithAuditTrial(qry, "Distributor Wise Product IMS(Yesterday)");
                int count = 0;
                var item = (from DataRow row in dt.Rows
                            select new DistSalesSummaryBEL
                            {
                                SlNo = ++count,
                                DivisionCode = row["division_code"].ToString(),
                                DivisionName = row["division_name"].ToString(),
                                RegionCode = row["region_code"].ToString(),
                                RegionName = row["region_name"].ToString(),
                                AreaCode = row["area_code"].ToString(),
                                AreaName = row["area_name"].ToString(),
                                TerritoryCode = row["territory_code"].ToString(),
                                TerritoryName = row["territory_name"].ToString(),
                                CustomerCode = row["customer_code"].ToString(),
                                CustomerName = row["customer_name"].ToString(),
                                DbLocation = row["db_location"].ToString(),
                                ProductCode = row["product_code"].ToString(),
                                ProductName = row["product_name"].ToString(),
                                PackSize = row["pack_size"].ToString(),
                                ProductPrice = Convert.ToDouble(row["product_price"]),
                                InvoiceQty = Convert.ToInt32(row["invoice_qty"]),
                                InvBonusQty = Convert.ToInt32(row["inv_bonus_qty"]),
                                BonusPriceDiscount = Convert.ToDouble(row["bonus_price_discount"]),
                                ImsSalesQty = Convert.ToInt32(row["ims_sales_qty"]),
                                ImsBnsQty = Convert.ToInt32(row["ims_bns_qty"]),
                                InvoiceAmt = Convert.ToDouble(row["invoice_amt"]),
                                ReturnSalesQty = Convert.ToInt32(row["return_sales_qty"]),
                                ReturnBnsQty = Convert.ToInt32(row["return_bns_qty"]),
                                BnsDiscRet = Convert.ToDouble(row["bns_disc_ret"]),
                                ReturnValue = Convert.ToDouble(row["return_value"]),
                                ImsSalesVal = Convert.ToDouble(row["ims_sales_val"]),
                                ImsBnsVal = Convert.ToDouble(row["ims_bns_val"]),
                                NetIms = Convert.ToDouble(row["net_ims"])
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

        //Query
        public List<DistSalesSummaryBEL> GetDistProductImsLastSevendays(string dCode, string rCode, string aCode, string tCode, string cCode)
        {
            try
            {

                string qry;

                int userId = Convert.ToInt32(HttpContext.Current.Session["USER_ID"]);
                int UserAccessCount = Convert.ToInt32(HttpContext.Current.Session["USER_BASE_REPORT_FILTER"]);

                if (UserAccessCount > 0)
                {
                    qry = " SELECT DIVISION_CODE, DIVISION_NAME, REGION_CODE, REGION_NAME, AREA_CODE, AREA_NAME, TERRITORY_CODE, TERRITORY_NAME,  CUSTOMER_CODE, CUSTOMER_NAME, DB_LOCATION, PRODUCT_CODE, PRODUCT_NAME, PACK_SIZE, PRODUCT_PRICE, INVOICE_QTY, INV_BONUS_QTY, BONUS_PRICE_DISCOUNT, IMS_SALES_QTY, IMS_BNS_QTY, INVOICE_AMT, RETURN_SALES_QTY, RETURN_BNS_QTY, BNS_DISC_RET, RETURN_VALUE, IMS_SALES_VAL, IMS_BNS_VAL, NET_IMS" +
                           " FROM MV_DIST_PRODUCT_IMS_LAST_7DAY " +
                           " WHERE (DIVISION_CODE = '" + dCode + "' OR '" + dCode + "' = 'ALL')" +
                           " AND   (REGION_CODE='" + rCode + "' OR '" + rCode + "'='ALL')" +
                           " AND   (AREA_CODE='" + aCode + "' OR '" + aCode + "'='ALL')" +
                           " AND   (TERRITORY_CODE='" + tCode + "' OR '" + tCode + "'='ALL')" +
                           " AND   (CUSTOMER_CODE='" + cCode + "' OR '" + cCode + "'='ALL')" +
                           " AND   PRODUCT_CODE IN (SELECT PRODUCT_CODE FROM USER_PRODUCT_DTL WHERE USER_ID=" + userId + ")" +
                           " ORDER BY DIVISION_CODE,REGION_CODE, AREA_CODE, TERRITORY_CODE, CUSTOMER_CODE,PRODUCT_CODE";
                }
                else
                {
                    qry = " SELECT DIVISION_CODE, DIVISION_NAME, REGION_CODE, REGION_NAME, AREA_CODE, AREA_NAME, TERRITORY_CODE, TERRITORY_NAME,  CUSTOMER_CODE, CUSTOMER_NAME, DB_LOCATION, PRODUCT_CODE, PRODUCT_NAME, PACK_SIZE, PRODUCT_PRICE, INVOICE_QTY, INV_BONUS_QTY, BONUS_PRICE_DISCOUNT, IMS_SALES_QTY, IMS_BNS_QTY, INVOICE_AMT, RETURN_SALES_QTY, RETURN_BNS_QTY, BNS_DISC_RET, RETURN_VALUE, IMS_SALES_VAL, IMS_BNS_VAL, NET_IMS" +
                           " FROM MV_DIST_PRODUCT_IMS_LAST_7DAY " +
                           " WHERE (DIVISION_CODE = '" + dCode + "' OR '" + dCode + "' = 'ALL')" +
                           " AND   (REGION_CODE='" + rCode + "' OR '" + rCode + "'='ALL')" +
                           " AND   (AREA_CODE='" + aCode + "' OR '" + aCode + "'='ALL')" +
                           " AND   (TERRITORY_CODE='" + tCode + "' OR '" + tCode + "'='ALL')" +
                           " AND   (CUSTOMER_CODE='" + cCode + "' OR '" + cCode + "'='ALL')" +
                           " ORDER BY DIVISION_CODE,REGION_CODE, AREA_CODE, TERRITORY_CODE, CUSTOMER_CODE,PRODUCT_CODE";
                }



                //DataTable dt = _dbHelper.GetDataTable(qry);
                DataTable dt = _dbHelper.GetDataTableWithAuditTrial(qry, "Distributor Wise Product IMS(Last Seven Days)");
                int count = 0;
                var item = (from DataRow row in dt.Rows
                            select new DistSalesSummaryBEL
                            {
                                SlNo = ++count,
                                DivisionCode = row["division_code"].ToString(),
                                DivisionName = row["division_name"].ToString(),
                                RegionCode = row["region_code"].ToString(),
                                RegionName = row["region_name"].ToString(),
                                AreaCode = row["area_code"].ToString(),
                                AreaName = row["area_name"].ToString(),
                                TerritoryCode = row["territory_code"].ToString(),
                                TerritoryName = row["territory_name"].ToString(),
                                CustomerCode = row["customer_code"].ToString(),
                                CustomerName = row["customer_name"].ToString(),
                                DbLocation = row["db_location"].ToString(),
                                ProductCode = row["product_code"].ToString(),
                                ProductName = row["product_name"].ToString(),
                                PackSize = row["pack_size"].ToString(),
                                ProductPrice = Convert.ToDouble(row["product_price"]),
                                InvoiceQty = Convert.ToInt32(row["invoice_qty"]),
                                InvBonusQty = Convert.ToInt32(row["inv_bonus_qty"]),
                                BonusPriceDiscount = Convert.ToDouble(row["bonus_price_discount"]),
                                ImsSalesQty = Convert.ToInt32(row["ims_sales_qty"]),
                                ImsBnsQty = Convert.ToInt32(row["ims_bns_qty"]),
                                InvoiceAmt = Convert.ToDouble(row["invoice_amt"]),
                                ReturnSalesQty = Convert.ToInt32(row["return_sales_qty"]),
                                ReturnBnsQty = Convert.ToInt32(row["return_bns_qty"]),
                                BnsDiscRet = Convert.ToDouble(row["bns_disc_ret"]),
                                ReturnValue = Convert.ToDouble(row["return_value"]),
                                ImsSalesVal = Convert.ToDouble(row["ims_sales_val"]),
                                ImsBnsVal = Convert.ToDouble(row["ims_bns_val"]),
                                NetIms = Convert.ToDouble(row["net_ims"])
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

        //Query
        public List<DistSalesSummaryBEL> GetDistProductImsLastThirtydays(string dCode, string rCode, string aCode, string tCode, string cCode)
        {
            try
            {

                string qry;

                int userId = Convert.ToInt32(HttpContext.Current.Session["USER_ID"]);
                int UserAccessCount = Convert.ToInt32(HttpContext.Current.Session["USER_BASE_REPORT_FILTER"]);

                if (UserAccessCount > 0)
                {
                    qry = " SELECT DIVISION_CODE, DIVISION_NAME, REGION_CODE, REGION_NAME, AREA_CODE, AREA_NAME, TERRITORY_CODE, TERRITORY_NAME,CUSTOMER_CODE, CUSTOMER_NAME, DB_LOCATION, PRODUCT_CODE, PRODUCT_NAME, PACK_SIZE, PRODUCT_PRICE, INVOICE_QTY, INV_BONUS_QTY, BONUS_PRICE_DISCOUNT, IMS_SALES_QTY, IMS_BNS_QTY, INVOICE_AMT, RETURN_SALES_QTY, RETURN_BNS_QTY, BNS_DISC_RET, RETURN_VALUE, IMS_SALES_VAL, IMS_BNS_VAL, NET_IMS" +
                          " FROM MV_DIST_PRODUCT_IMS_LAST_30DAY " +
                          " WHERE (DIVISION_CODE = '" + dCode + "' OR '" + dCode + "' = 'ALL')" +
                          " AND   (REGION_CODE='" + rCode + "' OR '" + rCode + "'='ALL')" +
                          " AND   (AREA_CODE='" + aCode + "' OR '" + aCode + "'='ALL')" +
                          " AND   (TERRITORY_CODE='" + tCode + "' OR '" + tCode + "'='ALL')" +
                          " AND   (CUSTOMER_CODE='" + cCode + "' OR '" + cCode + "'='ALL')" +
                          " AND   PRODUCT_CODE IN (SELECT PRODUCT_CODE FROM USER_PRODUCT_DTL WHERE USER_ID=" + userId + ")" +
                          " ORDER BY DIVISION_CODE,REGION_CODE, AREA_CODE, TERRITORY_CODE, CUSTOMER_CODE,PRODUCT_CODE";
                }
                else
                {
                    qry = " SELECT DIVISION_CODE, DIVISION_NAME, REGION_CODE, REGION_NAME, AREA_CODE, AREA_NAME, TERRITORY_CODE, TERRITORY_NAME,CUSTOMER_CODE, CUSTOMER_NAME, DB_LOCATION, PRODUCT_CODE, PRODUCT_NAME, PACK_SIZE, PRODUCT_PRICE, INVOICE_QTY, INV_BONUS_QTY, BONUS_PRICE_DISCOUNT, IMS_SALES_QTY, IMS_BNS_QTY, INVOICE_AMT, RETURN_SALES_QTY, RETURN_BNS_QTY, BNS_DISC_RET, RETURN_VALUE, IMS_SALES_VAL, IMS_BNS_VAL, NET_IMS" +
                          " FROM MV_DIST_PRODUCT_IMS_LAST_30DAY " +
                          " WHERE (DIVISION_CODE = '" + dCode + "' OR '" + dCode + "' = 'ALL')" +
                          " AND   (REGION_CODE='" + rCode + "' OR '" + rCode + "'='ALL')" +
                          " AND   (AREA_CODE='" + aCode + "' OR '" + aCode + "'='ALL')" +
                          " AND   (TERRITORY_CODE='" + tCode + "' OR '" + tCode + "'='ALL')" +
                          " AND   (CUSTOMER_CODE='" + cCode + "' OR '" + cCode + "'='ALL')" +
                          " ORDER BY DIVISION_CODE,REGION_CODE, AREA_CODE, TERRITORY_CODE, CUSTOMER_CODE,PRODUCT_CODE";
                }



                //DataTable dt = _dbHelper.GetDataTable(qry);
                DataTable dt = _dbHelper.GetDataTableWithAuditTrial(qry, "Distributor Wise Product IMS(Last Thirty Days)");
                int count = 0;
                var item = (from DataRow row in dt.Rows
                            select new DistSalesSummaryBEL
                            {
                                SlNo = ++count,
                                DivisionCode = row["division_code"].ToString(),
                                DivisionName = row["division_name"].ToString(),
                                RegionCode = row["region_code"].ToString(),
                                RegionName = row["region_name"].ToString(),
                                AreaCode = row["area_code"].ToString(),
                                AreaName = row["area_name"].ToString(),
                                TerritoryCode = row["territory_code"].ToString(),
                                TerritoryName = row["territory_name"].ToString(),
                                CustomerCode = row["customer_code"].ToString(),
                                CustomerName = row["customer_name"].ToString(),
                                DbLocation = row["db_location"].ToString(),
                                ProductCode = row["product_code"].ToString(),
                                ProductName = row["product_name"].ToString(),
                                PackSize = row["pack_size"].ToString(),
                                ProductPrice = Convert.ToDouble(row["product_price"]),
                                InvoiceQty = Convert.ToInt32(row["invoice_qty"]),
                                InvBonusQty = Convert.ToInt32(row["inv_bonus_qty"]),
                                BonusPriceDiscount = Convert.ToDouble(row["bonus_price_discount"]),
                                ImsSalesQty = Convert.ToInt32(row["ims_sales_qty"]),
                                ImsBnsQty = Convert.ToInt32(row["ims_bns_qty"]),
                                InvoiceAmt = Convert.ToDouble(row["invoice_amt"]),
                                ReturnSalesQty = Convert.ToInt32(row["return_sales_qty"]),
                                ReturnBnsQty = Convert.ToInt32(row["return_bns_qty"]),
                                BnsDiscRet = Convert.ToDouble(row["bns_disc_ret"]),
                                ReturnValue = Convert.ToDouble(row["return_value"]),
                                ImsSalesVal = Convert.ToDouble(row["ims_sales_val"]),
                                ImsBnsVal = Convert.ToDouble(row["ims_bns_val"]),
                                NetIms = Convert.ToDouble(row["net_ims"])
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

        //Query
        public List<DistSalesSummaryBEL> GetDistProductImsCurrentMonth(string dCode, string rCode, string aCode, string tCode, string cCode)
        {
            try
            {

                string qry;

                int userId = Convert.ToInt32(HttpContext.Current.Session["USER_ID"]);
                int UserAccessCount = Convert.ToInt32(HttpContext.Current.Session["USER_BASE_REPORT_FILTER"]);

                if (UserAccessCount > 0)
                {
                    qry = " SELECT DIVISION_CODE, DIVISION_NAME, REGION_CODE, REGION_NAME, AREA_CODE, AREA_NAME, TERRITORY_CODE, TERRITORY_NAME,  CUSTOMER_CODE, CUSTOMER_NAME, DB_LOCATION, PRODUCT_CODE, PRODUCT_NAME, PACK_SIZE, PRODUCT_PRICE, INVOICE_QTY, INV_BONUS_QTY, BONUS_PRICE_DISCOUNT, IMS_SALES_QTY, IMS_BNS_QTY, INVOICE_AMT, RETURN_SALES_QTY, RETURN_BNS_QTY, BNS_DISC_RET, RETURN_VALUE, IMS_SALES_VAL, IMS_BNS_VAL, NET_IMS," +
                          " LAST_YEAR_AS_ON_DATE_IMS_QTY," +
                          " LAST_YEAR_AS_ON_DATE_IMS_VAL " +
                          " FROM MV_DIST_PRODUCT_IMS_CMONTH " +
                          " WHERE (DIVISION_CODE = '" + dCode + "' OR '" + dCode + "' = 'ALL')" +
                          " AND   (REGION_CODE='" + rCode + "' OR '" + rCode + "'='ALL')" +
                          " AND   (AREA_CODE='" + aCode + "' OR '" + aCode + "'='ALL')" +
                          " AND   (TERRITORY_CODE='" + tCode + "' OR '" + tCode + "'='ALL')" +
                          " AND   (CUSTOMER_CODE='" + cCode + "' OR '" + cCode + "'='ALL')" +
                          " AND   PRODUCT_CODE IN (SELECT PRODUCT_CODE FROM USER_PRODUCT_DTL WHERE USER_ID=" + userId + ")" +
                          " ORDER BY DIVISION_CODE,REGION_CODE, AREA_CODE, TERRITORY_CODE, CUSTOMER_CODE,PRODUCT_CODE";
                }
                else
                {
                    qry = " SELECT DIVISION_CODE, DIVISION_NAME, REGION_CODE, REGION_NAME, AREA_CODE, AREA_NAME, TERRITORY_CODE, TERRITORY_NAME,  CUSTOMER_CODE, CUSTOMER_NAME, DB_LOCATION, PRODUCT_CODE, PRODUCT_NAME, PACK_SIZE, PRODUCT_PRICE, INVOICE_QTY, INV_BONUS_QTY, BONUS_PRICE_DISCOUNT, IMS_SALES_QTY, IMS_BNS_QTY, INVOICE_AMT, RETURN_SALES_QTY, RETURN_BNS_QTY, BNS_DISC_RET, RETURN_VALUE, IMS_SALES_VAL, IMS_BNS_VAL, NET_IMS," +
                          " LAST_YEAR_AS_ON_DATE_IMS_QTY," +
                          " LAST_YEAR_AS_ON_DATE_IMS_VAL " +
                          " FROM MV_DIST_PRODUCT_IMS_CMONTH " +
                          " WHERE (DIVISION_CODE = '" + dCode + "' OR '" + dCode + "' = 'ALL')" +
                          " AND   (REGION_CODE='" + rCode + "' OR '" + rCode + "'='ALL')" +
                          " AND   (AREA_CODE='" + aCode + "' OR '" + aCode + "'='ALL')" +
                          " AND   (TERRITORY_CODE='" + tCode + "' OR '" + tCode + "'='ALL')" +
                          " AND   (CUSTOMER_CODE='" + cCode + "' OR '" + cCode + "'='ALL')" +
                          " ORDER BY DIVISION_CODE,REGION_CODE, AREA_CODE, TERRITORY_CODE, CUSTOMER_CODE,PRODUCT_CODE";
                }



                //DataTable dt = _dbHelper.GetDataTable(qry);
                DataTable dt = _dbHelper.GetDataTableWithAuditTrial(qry, "Distributor Wise Product IMS(Current Month)");
                int count = 0;
                var item = (from DataRow row in dt.Rows
                            select new DistSalesSummaryBEL
                            {
                                SlNo = ++count,
                                DivisionCode = row["division_code"].ToString(),
                                DivisionName = row["division_name"].ToString(),
                                RegionCode = row["region_code"].ToString(),
                                RegionName = row["region_name"].ToString(),
                                AreaCode = row["area_code"].ToString(),
                                AreaName = row["area_name"].ToString(),
                                TerritoryCode = row["territory_code"].ToString(),
                                TerritoryName = row["territory_name"].ToString(),
                                CustomerCode = row["customer_code"].ToString(),
                                CustomerName = row["customer_name"].ToString(),
                                DbLocation = row["db_location"].ToString(),
                                ProductCode = row["product_code"].ToString(),
                                ProductName = row["product_name"].ToString(),
                                PackSize = row["pack_size"].ToString(),
                                ProductPrice = Convert.ToDouble(row["product_price"]),
                                InvoiceQty = Convert.ToInt32(row["invoice_qty"]),
                                InvBonusQty = Convert.ToInt32(row["inv_bonus_qty"]),
                                BonusPriceDiscount = Convert.ToDouble(row["bonus_price_discount"]),
                                ImsSalesQty = Convert.ToInt32(row["ims_sales_qty"]),
                                ImsBnsQty = Convert.ToInt32(row["ims_bns_qty"]),
                                InvoiceAmt = Convert.ToDouble(row["invoice_amt"]),
                                ReturnSalesQty = Convert.ToInt32(row["return_sales_qty"]),
                                ReturnBnsQty = Convert.ToInt32(row["return_bns_qty"]),
                                BnsDiscRet = Convert.ToDouble(row["bns_disc_ret"]),
                                ReturnValue = Convert.ToDouble(row["return_value"]),
                                ImsSalesVal = Convert.ToDouble(row["ims_sales_val"]),
                                ImsBnsVal = Convert.ToDouble(row["ims_bns_val"]),
                                NetIms = Convert.ToDouble(row["net_ims"]),
                                LastYearAsOnDateImsQty = Convert.ToInt32(row["LAST_YEAR_AS_ON_DATE_IMS_QTY"]),
                                LastYearAsOnDateImsVal = Convert.ToDouble(row["LAST_YEAR_AS_ON_DATE_IMS_VAL"])
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

        //Query 
        public List<DistSalesSummaryBEL> GetDistProductImsLastMonth(string dCode, string rCode, string aCode, string tCode, string cCode)
        {
            try
            {

                string qry;

                int userId = Convert.ToInt32(HttpContext.Current.Session["USER_ID"]);
                int UserAccessCount = Convert.ToInt32(HttpContext.Current.Session["USER_BASE_REPORT_FILTER"]);

                if (UserAccessCount > 0)
                {
                    qry = " SELECT  DIVISION_CODE, DIVISION_NAME, REGION_CODE, REGION_NAME, AREA_CODE, AREA_NAME, TERRITORY_CODE, TERRITORY_NAME, CUSTOMER_CODE, CUSTOMER_NAME, DB_LOCATION, PRODUCT_CODE, PRODUCT_NAME, PACK_SIZE, PRODUCT_PRICE, INVOICE_QTY, INV_BONUS_QTY, BONUS_PRICE_DISCOUNT, IMS_SALES_QTY, IMS_BNS_QTY, INVOICE_AMT, RETURN_SALES_QTY, RETURN_BNS_QTY, BNS_DISC_RET, RETURN_VALUE, IMS_SALES_VAL, IMS_BNS_VAL, NET_IMS," +
                          " LAST_YEAR_AS_ON_DATE_IMS_QTY," +
                          " LAST_YEAR_AS_ON_DATE_IMS_VAL " +
                          " FROM MV_DIST_PRODUCT_IMS_LMONTH " +
                          " WHERE (DIVISION_CODE = '" + dCode + "' OR '" + dCode + "' = 'ALL')" +
                          " AND   (REGION_CODE='" + rCode + "' OR '" + rCode + "'='ALL')" +
                          " AND   (AREA_CODE='" + aCode + "' OR '" + aCode + "'='ALL')" +
                          " AND   (TERRITORY_CODE='" + tCode + "' OR '" + tCode + "'='ALL')" +
                          " AND   (CUSTOMER_CODE='" + cCode + "' OR '" + cCode + "'='ALL')" +
                          " AND   PRODUCT_CODE IN (SELECT PRODUCT_CODE FROM USER_PRODUCT_DTL WHERE USER_ID=" + userId + ")" +
                          " ORDER BY DIVISION_CODE,REGION_CODE, AREA_CODE, TERRITORY_CODE, CUSTOMER_CODE,PRODUCT_CODE";
                }
                else
                {
                    qry = " SELECT  DIVISION_CODE, DIVISION_NAME, REGION_CODE, REGION_NAME, AREA_CODE, AREA_NAME, TERRITORY_CODE, TERRITORY_NAME, CUSTOMER_CODE, CUSTOMER_NAME, DB_LOCATION, PRODUCT_CODE, PRODUCT_NAME, PACK_SIZE, PRODUCT_PRICE, INVOICE_QTY, INV_BONUS_QTY, BONUS_PRICE_DISCOUNT, IMS_SALES_QTY, IMS_BNS_QTY, INVOICE_AMT, RETURN_SALES_QTY, RETURN_BNS_QTY, BNS_DISC_RET, RETURN_VALUE, IMS_SALES_VAL, IMS_BNS_VAL, NET_IMS," +
                          " LAST_YEAR_AS_ON_DATE_IMS_QTY," +
                          " LAST_YEAR_AS_ON_DATE_IMS_VAL " +
                          " FROM MV_DIST_PRODUCT_IMS_LMONTH " +
                          " WHERE (DIVISION_CODE = '" + dCode + "' OR '" + dCode + "' = 'ALL')" +
                          " AND   (REGION_CODE='" + rCode + "' OR '" + rCode + "'='ALL')" +
                          " AND   (AREA_CODE='" + aCode + "' OR '" + aCode + "'='ALL')" +
                          " AND   (TERRITORY_CODE='" + tCode + "' OR '" + tCode + "'='ALL')" +
                          " AND   (CUSTOMER_CODE='" + cCode + "' OR '" + cCode + "'='ALL')" +
                          " ORDER BY DIVISION_CODE,REGION_CODE, AREA_CODE, TERRITORY_CODE, CUSTOMER_CODE,PRODUCT_CODE";
                }



                //DataTable dt = _dbHelper.GetDataTable(qry);
                DataTable dt = _dbHelper.GetDataTableWithAuditTrial(qry, "Distributor Wise Product IMS(Last Month)");
                int count = 0;
                var item = (from DataRow row in dt.Rows
                            select new DistSalesSummaryBEL
                            {
                                SlNo = ++count,
                                DivisionCode = row["division_code"].ToString(),
                                DivisionName = row["division_name"].ToString(),
                                RegionCode = row["region_code"].ToString(),
                                RegionName = row["region_name"].ToString(),
                                AreaCode = row["area_code"].ToString(),
                                AreaName = row["area_name"].ToString(),
                                TerritoryCode = row["territory_code"].ToString(),
                                TerritoryName = row["territory_name"].ToString(),
                                CustomerCode = row["customer_code"].ToString(),
                                CustomerName = row["customer_name"].ToString(),
                                DbLocation = row["db_location"].ToString(),
                                ProductCode = row["product_code"].ToString(),
                                ProductName = row["product_name"].ToString(),
                                PackSize = row["pack_size"].ToString(),
                                ProductPrice = Convert.ToDouble(row["product_price"]),
                                InvoiceQty = Convert.ToInt32(row["invoice_qty"]),
                                InvBonusQty = Convert.ToInt32(row["inv_bonus_qty"]),
                                BonusPriceDiscount = Convert.ToDouble(row["bonus_price_discount"]),
                                ImsSalesQty = Convert.ToInt32(row["ims_sales_qty"]),
                                ImsBnsQty = Convert.ToInt32(row["ims_bns_qty"]),
                                InvoiceAmt = Convert.ToDouble(row["invoice_amt"]),
                                ReturnSalesQty = Convert.ToInt32(row["return_sales_qty"]),
                                ReturnBnsQty = Convert.ToInt32(row["return_bns_qty"]),
                                BnsDiscRet = Convert.ToDouble(row["bns_disc_ret"]),
                                ReturnValue = Convert.ToDouble(row["return_value"]),
                                ImsSalesVal = Convert.ToDouble(row["ims_sales_val"]),
                                ImsBnsVal = Convert.ToDouble(row["ims_bns_val"]),
                                NetIms = Convert.ToDouble(row["net_ims"]),

                                LastYearAsOnDateImsQty = Convert.ToInt32(row["LAST_YEAR_AS_ON_DATE_IMS_QTY"]),
                                LastYearAsOnDateImsVal = Convert.ToDouble(row["LAST_YEAR_AS_ON_DATE_IMS_VAL"])
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