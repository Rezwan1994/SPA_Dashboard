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
    public class ComboBonusDecDAL : ReturnData
    {

        private readonly DBHelper _dbHelper = new DBHelper();
        private readonly ErrorLogger _errorLogger = new ErrorLogger();
        private static readonly DBConnection DBConnection = new DBConnection();
        public readonly string ConnString = DBConnection.SAConnStrReader("Oracle", "PHARMAERP");



        public object GetComboBonusDeclaration(string cbNo)
        {
            try
            {
                using (OracleConnection objConn = new OracleConnection(ConnString))
                {
                    using (OracleCommand objCmd = new OracleCommand())
                    {
                        objCmd.Connection = objConn;
                        objCmd.CommandText = "FN_COMBO_BONUS_DEC";
                        objCmd.CommandType = CommandType.StoredProcedure;
                        objCmd.Parameters.Add("pComboBonusNo", OracleType.VarChar).Value = cbNo;
                        objCmd.Parameters.Add("return_value", OracleType.Cursor).Direction = ParameterDirection.ReturnValue;
                        objConn.Open();
                        objCmd.ExecuteNonQuery();
                        OracleDataReader rdr = objCmd.ExecuteReader();
                        DataTable dt = new DataTable();
                        if (rdr.HasRows)
                        {
                            dt.Load(rdr);
                        }
                        _dbHelper.InsertReportAudit("Combo Bonus Declaration");

                        List<ComboBonusDecBEL> item;
                        item = (from DataRow row in dt.Rows
                                select new ComboBonusDecBEL
                                {
                                    ComboBonusNo = row["COMBO_BONUS_NO"].ToString(),
                                    ComboBonusName = row["COMBO_BONUS_NAME"].ToString(),
                                    EffectFromDate = row["EFFECT_FROM_DATE"].ToString(),
                                    EffectToDate = row["EFFECT_TO_DATE"].ToString(),
                                    LocationType = row["LOCATION_TYPE"].ToString(),
                                    LocationCode = row["LOCATION_CODE"].ToString(),
                                    LocationName = row["LOCATION_NAME"].ToString(),
                                    LocationStatus = row["LOCATION_STATUS"].ToString(),
                                    ProductType = row["PRODUCT_TYPE"].ToString(),
                                    ProductTypeCode = row["PRODUCT_TYPE_CODE"].ToString(),
                                    ProductTypeName = row["PRODUCT_TYPE_NAME"].ToString(),
                                    BonusType = row["BONUS_TYPE"].ToString(),
                                    ProductCode = row["PRODUCT_CODE"].ToString(),
                                    ProductName = row["PRODUCT_NAME"].ToString(),
                                    PackSize = row["PACK_SIZE"].ToString(),
                                    BonusProductCode = row["BONUS_PRODUCT_CODE"].ToString(),
                                    BonusProductName = row["BONUS_PRODUCT_NAME"].ToString(),
                                    BonusPackSize = row["BONUS_PRODUCT_PACK_SIZE"].ToString(),
                                    PriorityNo = row["PRIORITY_NO"].ToString(),
                                    SlabQty = row["SLAB_QTY"].ToString(),
                                    BonusQty = row["BONUS_QTY"].ToString(),
                                    BonusDiscount = row["BONUS_DISCOUNT"].ToString(),
                                    BonusStatus = row["BONUS_STATUS"].ToString()
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