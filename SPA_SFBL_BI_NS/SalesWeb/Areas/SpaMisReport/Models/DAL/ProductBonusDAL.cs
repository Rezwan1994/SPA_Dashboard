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
	public class ProductBonusDAL : ReturnData
    {
        private readonly DBHelper _dbHelper = new DBHelper();
        private readonly ErrorLogger _errorLogger = new ErrorLogger();
        private static readonly DBConnection DBConnection = new DBConnection();
        public readonly string ConnString = DBConnection.SAConnStrReader("Oracle", "PHARMAERP");

        public object GetProductList()
        {
            try
            {
                var qry = " SELECT 'ALL' PRODUCT_CODE, 'ALL' PRODUCT_NAME, 'ALL' PACK_SIZE,  1 SL FROM DUAL " +
                          " UNION SELECT PRODUCT_CODE,PRODUCT_NAME,PACK_SIZE ,2 SL  FROM MV_PRODUCT_INFO WHERE STATUS='A'" +
                          " ORDER BY SL, PRODUCT_NAME";
                DataTable dt = _dbHelper.GetDataTable(qry);
                var item = (from DataRow row in dt.Rows
                            select new ProductInfoBEL
                            {
                                ProductCode = row["PRODUCT_CODE"].ToString(),
                                ProductName = row["PRODUCT_NAME"].ToString(),
                                PackSize = row["PACK_SIZE"].ToString()
                            }).ToList();
                return item;

            }
            catch (Exception e)
            {
                var lineNum = e.StackTrace.Substring(e.StackTrace.LastIndexOf(' '));
                _errorLogger.GetErrorMessage(e.Message, "ProductBonusDAL", lineNum);
                ExceptionReturn = e.Message;
                return "Not Ok";
            }
        }


        //Function 
        public object GetProductBonus(string FromDate, string ToDate, string ProductCode)
        {
            try
            {
                using (OracleConnection objConn = new OracleConnection(ConnString))
                {
                    using (OracleCommand objCmd = new OracleCommand())
                    {
                        objCmd.Connection = objConn;
                        objCmd.CommandText = "FN_PRODUCT_BONUS";
                        objCmd.CommandType = CommandType.StoredProcedure;
                        objCmd.Parameters.Add("p_from_date", OracleType.VarChar).Value = FromDate;
                        objCmd.Parameters.Add("p_to_date", OracleType.VarChar).Value = ToDate;
                        objCmd.Parameters.Add("p_product_code", OracleType.VarChar).Value = ProductCode;
                        objCmd.Parameters.Add("return_value", OracleType.Cursor).Direction = ParameterDirection.ReturnValue;
                        objConn.Open();
                        objCmd.ExecuteNonQuery();
                        OracleDataReader rdr = objCmd.ExecuteReader();
                        DataTable dt = new DataTable();
                        if (rdr.HasRows)
                        {
                            dt.Load(rdr);
                        }
                        _dbHelper.InsertReportAudit("Product Bonus Information");
                        int count = 0;
                        List<ProductBonusBEL> item;
                        item = (from DataRow row in dt.Rows
                                select new ProductBonusBEL
                                {
                                    SlNo = ++count,
                                    SalesProductCode = row["SALES_PRODUCT_CODE"].ToString(),
                                    SalesProductName = row["SALES_PRODUCT_NAME"].ToString(),
                                    SalesProductPackSize = row["SALES_PRODUCT_PACK_SIZE"].ToString(),
                                    BonusProductCode = row["BONUS_PRODUCT_CODE"].ToString(),
                                    BonusProductName = row["BONUS_PRODUCT_NAME"].ToString(),
                                    BonusProductPackSize = row["BONUS_PRODUCT_PACK_SIZE"].ToString(),
                                    BonusSlabQty = row["BONUS_SLAB_QTY"].ToString(),
                                    BonusPrdQty = row["BONUS_PRD_QTY"].ToString(),
                                    BonusPriceDisc = row["BONUS_PRICE_DISC"].ToString(),
                                    PrdLocationType = row["PRD_LOCATION_TYPE"].ToString(),
                                    PrdLocationTypeName = row["PRD_LOCATION_TYPE_NAME"].ToString(),
                                    BonusLocationCode = row["BONUS_LOCATION_CODE"].ToString(),
                                    BonusLocationName = row["BONUS_LOCATION_NAME"].ToString(),
                                    SalesQty = row["SALES_QTY"].ToString(),
                                    PriceDiscount = row["PRICE_DISCOUNT"].ToString(),
                                    PriceLocationType = row["PRICE_LOCATION_TYPE"].ToString(),
                                    PriceLocationTypeName = row["PRICE_LOCATION_TYPE_NAME"].ToString(),
                                    PriceLocationCode = row["PRICE_LOCATION_CODE"].ToString(),
                                    PriceLocationName = row["PRICE_LOCATION_NAME"].ToString(),
                                    EffectFormDate = row["EFFECT_FROM_DATE"].ToString(),
                                    EffectToDate = row["EFFECT_TO_DATE"].ToString(),
                                    BonusStatus = row["BONUS_STATUS"].ToString(),
                                    PriceStatus = row["PRICE_STATUS"].ToString()
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

    }
}