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
    public class RetBnsSlabAdjDAL : ReturnData
    {

        private readonly DBHelper _dbHelper = new DBHelper();
        private readonly ErrorLogger _errorLogger = new ErrorLogger();
        private static readonly DBConnection DBConnection = new DBConnection();
        public readonly string ConnString = DBConnection.SAConnStrReader("Oracle", "PHARMAERP");


      
        public object GetRetBnsSlabAdjCustomDate(string fromDate, string toDate, string dCode, string rCode, string aCode, string tCode, string cCode, string bType)
        {
            try
            {
                using (OracleConnection objConn = new OracleConnection(ConnString))
                {
                    using (OracleCommand objCmd = new OracleCommand())
                    {
                        objCmd.Connection = objConn;
                        objCmd.CommandText = "FN_RET_BONUS_SLAB_ADJ";
                        objCmd.CommandType = CommandType.StoredProcedure;
                        objCmd.Parameters.Add("pfrom_date", OracleType.VarChar).Value = fromDate;
                        objCmd.Parameters.Add("pto_date", OracleType.VarChar).Value = toDate;
                        objCmd.Parameters.Add("pdivision_code", OracleType.VarChar).Value = dCode;
                        objCmd.Parameters.Add("pregion_code", OracleType.VarChar).Value = rCode;
                        objCmd.Parameters.Add("parea_code", OracleType.VarChar).Value = aCode;
                        objCmd.Parameters.Add("pterritory_code", OracleType.VarChar).Value = tCode;
                        objCmd.Parameters.Add("pcustomer_code", OracleType.VarChar).Value = cCode;
                        objCmd.Parameters.Add("pbonus_type", OracleType.VarChar).Value = bType;
                        objCmd.Parameters.Add("return_value", OracleType.Cursor).Direction = ParameterDirection.ReturnValue;
                        objConn.Open();
                        objCmd.ExecuteNonQuery();
                        OracleDataReader rdr = objCmd.ExecuteReader();
                        DataTable dt = new DataTable();
                        if (rdr.HasRows)
                        {
                            dt.Load(rdr);
                        }
                        _dbHelper.InsertReportAudit("Retailer Bonus Slab Adjustment (Custom Date)");
                        var dataList = (from DataRow row in dt.Rows
                                        select new RetBnsSlabAdjBEL
                                        {
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
                                            InvoiceNo = row["INVOICE_NO"].ToString(),
                                            InvoiceDate = row["INVOICE_DATE"].ToString(),
                                            ProductCode = row["PRODUCT_CODE"].ToString(),
                                            ProductName = row["PRODUCT_NAME"].ToString(),
                                            PackSize = row["PACK_SIZE"].ToString(),
                                            BonusSlabType = row["BONUS_SLAB_TYPE"].ToString(),
                                            BonusSlabQty = Convert.ToInt32(row["SLAB_QTY"]),
                                            NormalDecSlab = Convert.ToInt32(row["NORMAL_DEC_SLAB"]),
                                            IssuedQty = Convert.ToInt32(row["ISSUED_QTY"]),
                                            ReturnQty = Convert.ToInt32(row["RETURN_QTY"]),
                                            TotalImsQty = Convert.ToInt32(row["TOTAL_IMS_QTY"]),
                                            NormalImsQty = Convert.ToInt32(row["NORMAL_IMS_QTY"]),
                                            ImsNormalBnsQty = Convert.ToInt32(row["IMS_NORMAL_BNS_QTY"]),
                                            SlabImsQty = Convert.ToInt32(row["SLAB_IMS_QTY"]),
                                            ImsSlabBnsQty = Convert.ToInt32(row["IMS_SLAB_BNS_QTY"]),
                                            BonusRatio = Convert.ToInt32(row["BONUS_RATIO"]),
                                            AdjustableSlab = Convert.ToInt32(row["ADJUSTABLE_SLAB"])

                                        }).ToList();
                        return dataList;
                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return ExceptionReturn = "";
            }

        }


        public object GetRetBnsSlabAdjToday(string dCode, string rCode, string aCode, string tCode, string cCode, string bType)
        {
            try
            {
                using (OracleConnection objConn = new OracleConnection(ConnString))
                {
                    using (OracleCommand objCmd = new OracleCommand())
                    {
                        objCmd.Connection = objConn;
                        objCmd.CommandText = "FN_RET_BONUS_SLAB_ADJ_TODAY";
                        objCmd.CommandType = CommandType.StoredProcedure;
                        objCmd.Parameters.Add("pdivision_code", OracleType.VarChar).Value = dCode;
                        objCmd.Parameters.Add("pregion_code", OracleType.VarChar).Value = rCode;
                        objCmd.Parameters.Add("parea_code", OracleType.VarChar).Value = aCode;
                        objCmd.Parameters.Add("pterritory_code", OracleType.VarChar).Value = tCode;
                        objCmd.Parameters.Add("pcustomer_code", OracleType.VarChar).Value = cCode;
                        objCmd.Parameters.Add("pbonus_type", OracleType.VarChar).Value = bType;
                        objCmd.Parameters.Add("return_value", OracleType.Cursor).Direction = ParameterDirection.ReturnValue;
                        objConn.Open();
                        objCmd.ExecuteNonQuery();
                        OracleDataReader rdr = objCmd.ExecuteReader();
                        DataTable dt = new DataTable();
                        if (rdr.HasRows)
                        {
                            dt.Load(rdr);
                        }
                        _dbHelper.InsertReportAudit("Retailer Bonus Slab Adjustment (Today)");
                        var dataList = (from DataRow row in dt.Rows
                                        select new RetBnsSlabAdjBEL
                                        {
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
                                            InvoiceNo = row["INVOICE_NO"].ToString(),
                                            InvoiceDate = row["INVOICE_DATE"].ToString(),
                                            ProductCode = row["PRODUCT_CODE"].ToString(),
                                            ProductName = row["PRODUCT_NAME"].ToString(),
                                            PackSize = row["PACK_SIZE"].ToString(),
                                            BonusSlabType = row["BONUS_SLAB_TYPE"].ToString(),
                                            BonusSlabQty = Convert.ToInt32(row["SLAB_QTY"]),
                                            NormalDecSlab = Convert.ToInt32(row["NORMAL_DEC_SLAB"]),
                                            IssuedQty = Convert.ToInt32(row["ISSUED_QTY"]),
                                            ReturnQty = Convert.ToInt32(row["RETURN_QTY"]),
                                            TotalImsQty = Convert.ToInt32(row["TOTAL_IMS_QTY"]),
                                            NormalImsQty = Convert.ToInt32(row["NORMAL_IMS_QTY"]),
                                            ImsNormalBnsQty = Convert.ToInt32(row["IMS_NORMAL_BNS_QTY"]),
                                            SlabImsQty = Convert.ToInt32(row["SLAB_IMS_QTY"]),
                                            ImsSlabBnsQty = Convert.ToInt32(row["IMS_SLAB_BNS_QTY"]),
                                            BonusRatio = Convert.ToInt32(row["BONUS_RATIO"]),
                                            AdjustableSlab = Convert.ToInt32(row["ADJUSTABLE_SLAB"])

                                        }).ToList();
                        return dataList;
                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return ExceptionReturn = "";
            }

        }



        //public List<RetBnsSlabAdjBEL> GetRetBnsSlabAdjCustomDate(string fromDate, string toDate, string dCode, string rCode, string aCode, string tCode, string cCode, string bType)
        //{
        //    try
        //    {
        //        string qry = " SELECT " +
        //                     " DIVISION_CODE," +
        //                     " DIVISION_NAME," +
        //                     " REGION_CODE," +
        //                     " REGION_NAME," +
        //                     " AREA_CODE," +
        //                     " AREA_NAME," +
        //                     " TERRITORY_CODE," +
        //                     " TERRITORY_NAME," +
        //                     " MARKET_CODE," +
        //                     " MARKET_NAME," +
        //                     " CUSTOMER_CODE," +
        //                     " CUSTOMER_NAME," +
        //                     " DB_LOCATION," +
        //                     " ROUTE_CODE," +
        //                     " ROUTE_NAME," +
        //                     " RETAILER_CODE," +
        //                     " RETAILER_NAME," +
        //                     " INVOICE_NO," +
        //                     " TO_CHAR(INVOICE_DATE,'DD/MM/RRRR') INVOICE_DATE," +
        //                     " PRODUCT_CODE," +
        //                     " PRODUCT_NAME," +
        //                     " PACK_SIZE," +
        //                     " BONUS_SLAB_TYPE," +
        //                     " NORMAL_DEC_SLAB," +
        //                     " ISSUED_QTY," +
        //                     " RETURN_QTY," +
        //                     " TOTAL_IMS_QTY," +
        //                     " NORMAL_IMS_QTY," +
        //                     " IMS_NORMAL_BNS_QTY," +
        //                     " SLAB_IMS_QTY," +
        //                     " IMS_SLAB_BNS_QTY," +
        //                     " BONUS_RATIO, " +
        //                     " ADJUSTABLE_SLAB " +
        //                     " FROM MV_RET_BONUS_SLAB_ADJ_L3MONTH " +
        //                     " WHERE (DIVISION_CODE = '" + dCode + "' OR '" + dCode + "' = 'ALL')" +
        //                     " AND   (REGION_CODE='" + rCode + "' OR '" + rCode + "'='ALL')" +
        //                     " AND   (AREA_CODE='" + aCode + "' OR '" + aCode + "'='ALL')" +
        //                     " AND   (TERRITORY_CODE='" + tCode + "' OR '" + tCode + "'='ALL')" +
        //                     " AND   (CUSTOMER_CODE='" + cCode + "' OR '" + cCode + "'='ALL')" +
        //                     " AND   (BONUS_SLAB_TYPE='" + bType + "' OR '" + bType + "'='ALL')" +
        //                     " AND   TO_DATE(INVOICE_DATE,'DD/MM/RRRR') BETWEEN TO_DATE('" + fromDate + "','DD/MM/RRRR') AND TO_DATE('" + toDate + "','DD/MM/RRRR')" +
        //                     " ORDER BY DIVISION_CODE,REGION_CODE, AREA_CODE, TERRITORY_CODE, CUSTOMER_CODE,MARKET_CODE,ROUTE_CODE,RETAILER_CODE,INVOICE_NO,PRODUCT_CODE";


        //        DataTable dt = _dbHelper.GetDataTable(qry);
        //        int count = 0;
        //        var item = (from DataRow row in dt.Rows
        //                    select new RetBnsSlabAdjBEL
        //                    {
        //                        SlNo = ++count,
        //                        DivisionCode = row["DIVISION_CODE"].ToString(),
        //                        DivisionName = row["DIVISION_NAME"].ToString(),
        //                        RegionCode = row["REGION_CODE"].ToString(),
        //                        RegionName = row["REGION_NAME"].ToString(),
        //                        AreaCode = row["AREA_CODE"].ToString(),
        //                        AreaName = row["AREA_NAME"].ToString(),
        //                        TerritoryCode = row["TERRITORY_CODE"].ToString(),
        //                        TerritoryName = row["TERRITORY_NAME"].ToString(),
        //                        MarketCode = row["MARKET_CODE"].ToString(),
        //                        MarketName = row["MARKET_NAME"].ToString(),
        //                        CustomerCode = row["CUSTOMER_CODE"].ToString(),
        //                        CustomerName = row["CUSTOMER_NAME"].ToString(),
        //                        DbLocation = row["DB_LOCATION"].ToString(),
        //                        RouteCode = row["ROUTE_CODE"].ToString(),
        //                        RouteName = row["ROUTE_NAME"].ToString(),
        //                        RetailerCode = row["RETAILER_CODE"].ToString(),
        //                        RetailerName = row["RETAILER_NAME"].ToString(),
        //                        InvoiceNo = row["INVOICE_NO"].ToString(),
        //                        InvoiceDate = row["INVOICE_DATE"].ToString(),
        //                        ProductCode = row["PRODUCT_CODE"].ToString(),
        //                        ProductName = row["PRODUCT_NAME"].ToString(),
        //                        PackSize = row["PACK_SIZE"].ToString(),
        //                        BonusSlabType = row["BONUS_SLAB_TYPE"].ToString(),
        //                        NormalDecSlab = Convert.ToInt32(row["NORMAL_DEC_SLAB"]),
        //                        IssuedQty = Convert.ToInt32(row["ISSUED_QTY"]),
        //                        ReturnQty = Convert.ToInt32(row["RETURN_QTY"]),
        //                        TotalImsQty = Convert.ToInt32(row["TOTAL_IMS_QTY"]),
        //                        NormalImsQty = Convert.ToInt32(row["NORMAL_IMS_QTY"]),
        //                        ImsNormalBnsQty = Convert.ToInt32(row["IMS_NORMAL_BNS_QTY"]),
        //                        SlabImsQty = Convert.ToInt32(row["SLAB_IMS_QTY"]),
        //                        ImsSlabBnsQty = Convert.ToInt32(row["IMS_SLAB_BNS_QTY"]),
        //                        BonusRatio = Convert.ToInt32(row["BONUS_RATIO"]),
        //                        AdjustableSlab = Convert.ToInt32(row["ADJUSTABLE_SLAB"])


        //                    }).ToList();
        //        return item;
        //    }
        //    catch (Exception e)
        //    {
        //        var lineNum = e.StackTrace.Substring(e.StackTrace.LastIndexOf(' '));
        //        _errorLogger.GetErrorMessage(e.Message, "RetBnsSlabAdjDAL", lineNum);
        //        ExceptionReturn = e.Message;
        //        throw;
        //    }
        //}


        //Function

    }
}