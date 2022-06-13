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
    public class ProductInformationDAL : ReturnData
    {

        private readonly DBHelper _dbHelper = new DBHelper();
        private readonly ErrorLogger _errorLogger = new ErrorLogger();
        private static readonly DBConnection DBConnection = new DBConnection();
        public readonly string ConnString = DBConnection.SAConnStrReader("Oracle", "PHARMAERP");


        //Query 
        public List<ProductInformationBEL> GetProductInformationList(string base_product_code, string brand_code, string product_category, string status)
        {
            try
            {
                string qry =  " SELECT PRODUCT_CODE, PRODUCT_NAME, PRODUCT_NAME_BN, PACK_SIZE, BASE_PRODUCT_CODE," +
                              " BASE_PRODUCT_NAME, BRAND_CODE, BRAND_NAME, PRODUCT_CATEGORY_CODE, PRODUCT_CATEGORY_NAME," +
                              " BONUS_ALLOW, DISCOUNT_ALLOW, DISCOUNT_TYPE, DISCOUNT_VAL, SHIPPER_QTY, STATUS, CP_FLAG," +
                              " UNIT_TP, UNIT_VAT, MRP," +
                              " TO_CHAR(FIRST_INVOICE_DATE,'MM/DD/RRRR')FIRST_INVOICE_DATE," +
                              " TO_CHAR(LAST_INVOICE_DATE,'MM/DD/RRRR') LAST_INVOICE_DATE" +
                              " FROM MV_PRODUCT_INFORMATION " +
                              " WHERE (BASE_PRODUCT_CODE = '" + base_product_code + "' OR '" + base_product_code + "' = 'ALL')" +
                              " AND   (BRAND_CODE='" + brand_code + "' OR '" + brand_code + "'='ALL')" +
                              " AND   (PRODUCT_CATEGORY_CODE='" + product_category + "' OR '" + product_category + "'='ALL')" +
                              " AND   (STATUS='" + status + "' OR '" + status + "'='ALL')" +
                              " ORDER BY PRODUCT_NAME";


                //DataTable dt = _dbHelper.GetDataTable(qry);
                DataTable dt = _dbHelper.GetDataTableWithAuditTrial(qry, "Product Information");

                int count = 0;
                var item = (from DataRow row in dt.Rows
                            select new ProductInformationBEL
                            {
                                SlNo = ++count,
                                ProductCode = row["PRODUCT_CODE"].ToString(),
                                ProductName = row["PRODUCT_NAME"].ToString(),
                                ProductNameBn = row["PRODUCT_NAME_BN"].ToString(),
                                PackSize = row["PACK_SIZE"].ToString(),
                                BaseProductCode = row["BASE_PRODUCT_CODE"].ToString(),
                                BaseProductName = row["BASE_PRODUCT_NAME"].ToString(),
                                BrandCode = row["BRAND_CODE"].ToString(),
                                BrandName = row["BRAND_NAME"].ToString(),
                                CategoryCode = row["PRODUCT_CATEGORY_CODE"].ToString(),
                                CategoryName = row["PRODUCT_CATEGORY_NAME"].ToString(),
                                BonusAllow = row["BONUS_ALLOW"].ToString(),
                                DiscountAllow = row["DISCOUNT_ALLOW"].ToString(),
                                DiscountType = row["DISCOUNT_TYPE"].ToString(),
                                DiscountVal = row["DISCOUNT_VAL"].ToString(),
                                ShipperQty = row["SHIPPER_QTY"].ToString(),
                                Status = row["STATUS"].ToString(),
                                CpFlag = row["CP_FLAG"].ToString(),
                                UnitTp = row["UNIT_TP"].ToString(),
                                UnitVat = row["UNIT_VAT"].ToString(),
                                Mrp = row["MRP"].ToString(),
                                FirstInvoiceDate = row["FIRST_INVOICE_DATE"].ToString(),
                                LastInvoiceDate = row["LAST_INVOICE_DATE"].ToString()
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
        public List<ProductInformationBEL> GetDeadProductInformationList(string LastInvoiceDate, string base_product_code, string brand_code, string product_category, string status)
        {
            try
            {
                string qry =  " SELECT PRODUCT_CODE, PRODUCT_NAME, PRODUCT_NAME_BN, PACK_SIZE, BASE_PRODUCT_CODE," +
                              " BASE_PRODUCT_NAME, BRAND_CODE, BRAND_NAME, PRODUCT_CATEGORY_CODE, PRODUCT_CATEGORY_NAME," +
                              " BONUS_ALLOW, DISCOUNT_ALLOW, DISCOUNT_TYPE, DISCOUNT_VAL, SHIPPER_QTY," +
                              " STATUS, CP_FLAG, UNIT_TP, UNIT_VAT, MRP," +
                              " TO_CHAR(FIRST_INVOICE_DATE,'MM/DD/RRRR')FIRST_INVOICE_DATE," +
                              " TO_CHAR(LAST_INVOICE_DATE,'MM/DD/RRRR') LAST_INVOICE_DATE" +
                              " FROM MV_PRODUCT_INFORMATION " +
                              " WHERE (BASE_PRODUCT_CODE = '" + base_product_code + "' OR '" + base_product_code + "' = 'ALL')" +
                              " AND   (BRAND_CODE='" + brand_code + "' OR '" + brand_code + "'='ALL')" +
                              " AND   (PRODUCT_CATEGORY_CODE='" + product_category + "' OR '" + product_category + "'='ALL')" +
                              " AND   (STATUS='" + status + "' OR '" + status + "'='ALL')" +
                              " AND  TO_DATE(LAST_INVOICE_DATE,'DD/MM/RRRR') <= TO_DATE('" + LastInvoiceDate + "','DD/MM/RRRR')" +
                              " ORDER BY PRODUCT_NAME";


                //DataTable dt = _dbHelper.GetDataTable(qry);
                DataTable dt = _dbHelper.GetDataTableWithAuditTrial(qry, "Dead Product Information");
                int count = 0;
                var item = (from DataRow row in dt.Rows
                            select new ProductInformationBEL
                            {
                                SlNo = ++count,
                                ProductCode = row["PRODUCT_CODE"].ToString(),
                                ProductName = row["PRODUCT_NAME"].ToString(),
                                ProductNameBn = row["PRODUCT_NAME_BN"].ToString(),
                                PackSize = row["PACK_SIZE"].ToString(),
                                BaseProductCode = row["BASE_PRODUCT_CODE"].ToString(),
                                BaseProductName = row["BASE_PRODUCT_NAME"].ToString(),
                                BrandCode = row["BRAND_CODE"].ToString(),
                                BrandName = row["BRAND_NAME"].ToString(),
                                CategoryCode = row["PRODUCT_CATEGORY_CODE"].ToString(),
                                CategoryName = row["PRODUCT_CATEGORY_NAME"].ToString(),
                                BonusAllow = row["BONUS_ALLOW"].ToString(),
                                DiscountAllow = row["DISCOUNT_ALLOW"].ToString(),
                                DiscountType = row["DISCOUNT_TYPE"].ToString(),
                                DiscountVal = row["DISCOUNT_VAL"].ToString(),
                                ShipperQty = row["SHIPPER_QTY"].ToString(),
                                Status = row["STATUS"].ToString(),
                                CpFlag = row["CP_FLAG"].ToString(),
                                UnitTp = row["UNIT_TP"].ToString(),
                                UnitVat = row["UNIT_VAT"].ToString(),
                                Mrp = row["MRP"].ToString(),
                                FirstInvoiceDate = row["FIRST_INVOICE_DATE"].ToString(),
                                LastInvoiceDate = row["LAST_INVOICE_DATE"].ToString()
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


        public object GetBaseProductList()
        {
            try
            {
                var qry = " SELECT 'ALL' BASE_PRODUCT_CODE, 'ALL' BASE_PRODUCT_NAME, 1 SL FROM DUAL " +
                          " UNION SELECT BASE_PRODUCT_CODE, BASE_PRODUCT_NAME, 2 SL  FROM MV_BASE_PRODUCT_INFO " +
                          " ORDER BY SL, BASE_PRODUCT_NAME";
                DataTable dt = _dbHelper.GetDataTable(qry);
                var item = (from DataRow row in dt.Rows
                            select new BaseProductBEL
                            {
                                BaseProductCode = row["BASE_PRODUCT_CODE"].ToString(),
                                BaseProductName = row["BASE_PRODUCT_NAME"].ToString()
                            }).ToList();
                return item;

            }
            catch (Exception e)
            {
                var lineNum = e.StackTrace.Substring(e.StackTrace.LastIndexOf(' '));
                _errorLogger.GetErrorMessage(e.Message, "ProductInformationDAL", lineNum);
                ExceptionReturn = e.Message;
                return "Not Ok";
            }
        }

        public object GetBrandList()
        {
            try
            {
                var qry = " SELECT 'ALL' BRAND_CODE, 'ALL' BRAND_NAME, 1 SL FROM DUAL " +
                          " UNION SELECT BRAND_CODE, BRAND_NAME,2 SL  FROM MV_BRAND_INFO " +
                          " ORDER BY SL, BRAND_NAME";
                DataTable dt = _dbHelper.GetDataTable(qry);
                var item = (from DataRow row in dt.Rows
                            select new BrandBEL
                            {
                                BrandCode = row["BRAND_CODE"].ToString(),
                                BrandName = row["BRAND_NAME"].ToString()
                            }).ToList();
                return item;

            }
            catch (Exception e)
            {
                var lineNum = e.StackTrace.Substring(e.StackTrace.LastIndexOf(' '));
                _errorLogger.GetErrorMessage(e.Message, "ProductInformationDAL", lineNum);
                ExceptionReturn = e.Message;
                return "Not Ok";
            }
        }



        public object GetProductCategoryList()
        {
            try
            {
                var qry = " SELECT 'ALL' CATEGORY_CODE, 'ALL' CATEGORY_NAME, 1 SL FROM DUAL " +
                          " UNION SELECT CATEGORY_CODE, CATEGORY_NAME,2 SL  FROM MV_CATEGORY_INFO " +
                          " ORDER BY SL, CATEGORY_NAME";
                DataTable dt = _dbHelper.GetDataTable(qry);
                var item = (from DataRow row in dt.Rows
                            select new ProductCategoryBEL
                            {
                                CategoryCode = row["CATEGORY_CODE"].ToString(),
                                CategoryName = row["CATEGORY_NAME"].ToString()
                            }).ToList();
                return item;

            }
            catch (Exception e)
            {
                var lineNum = e.StackTrace.Substring(e.StackTrace.LastIndexOf(' '));
                _errorLogger.GetErrorMessage(e.Message, "ProductInformationDAL", lineNum);
                ExceptionReturn = e.Message;
                return "Not Ok";
            }
        }










    }
}