using System;
using System.Data;
using System.Linq;
using System.Web;
using SalesWeb.Areas.SpaMisTransaction.Models.BEL;
using SalesWeb.Areas.SpaMisReport.Models.BEL;
using SalesWeb.Universal.Gateway;
using System.Collections.Generic;
using Oracle.ManagedDataAccess.Client;
//using System.Data.OracleClient;
using static SalesWeb.Areas.SpaMisTransaction.Models.BEL.DistProductFactoryRelationBEL;

namespace SalesWeb.Areas.SpaMisTransaction.Models.DAL
{
    public class DistProductFactoryRelationDAL : ReturnData
    {

        private readonly DBHelper _dbHelper = new DBHelper();
        private readonly DBHelper2 _dbHelper2 = new DBHelper2();
        private readonly ErrorLogger _errorLogger = new ErrorLogger();

        private static readonly DBConnection DBConnection = new DBConnection();
        public readonly string ConnStringSfblMis = DBConnection.SAConnStrReader("Oracle", "PHARMAERP");
        public readonly string ConnStringSfblTrans = DBConnection.SAConnStrReader("Oracle", "SPASFBL");


        public object GetCustomerList()
        {
            try
            {
                string qry = " SELECT DISTINCT CUSTOMER_CODE, CUSTOMER_NAME FROM VW_LOCATION_RELATION" +
                             " WHERE CUSTOMER_CODE NOT IN (SELECT CUSTOMER_CODE FROM DIST_PRODUCT_FACTORY_REL_MST)" +
                             " ORDER BY CUSTOMER_NAME";


                DataTable dt = _dbHelper2.GetDataTable(qry);
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
                _errorLogger.GetErrorMessage(e.Message, "DistProductFactoryRelationDAL", lineNum);
                ExceptionReturn = e.Message;
                return "Not Ok";
            }
        }
        public object GetProductList()
        {
            try
            {
                var qry = " SELECT PRODUCT_CODE,PRODUCT_NAME,PACK_SIZE  FROM MV_PRODUCT_INFO WHERE STATUS='A'" +
                          " ORDER by PRODUCT_NAME";
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
                _errorLogger.GetErrorMessage(e.Message, "DistProductFactoryRelationDAL", lineNum);
                ExceptionReturn = e.Message;
                return "Not Ok";
            }
        }
        public object GetFactoryList()
        {
            try
            {
                string qry = " SELECT FACTORY_CODE, FACTORY_NAME FROM VW_FACTORY_LIST" +
                             " ORDER BY FACTORY_NAME";


                DataTable dt = _dbHelper2.GetDataTable(qry);
                var item = (from DataRow row in dt.Rows
                            select new FactoryInfoBEL
                            {
                                FactoryCode = row["FACTORY_CODE"].ToString(),
                                FactoryName = row["FACTORY_NAME"].ToString(),

                            }).ToList();
                return item;

            }
            catch (Exception e)
            {
                var lineNum = e.StackTrace.Substring(e.StackTrace.LastIndexOf(' '));
                _errorLogger.GetErrorMessage(e.Message, "DistProductFactoryRelationDAL", lineNum);
                ExceptionReturn = e.Message;
                return "Not Ok";
            }
        }

        internal bool InsertData(DistProductFactoryRelationMstBEL mstData, List<DistProductFactoryRelationDtlBEL> dtlData)
        {
            bool isTrue = false;

            using (OracleConnection con = new OracleConnection(ConnStringSfblTrans))
            {
                using (OracleCommand cmd = new OracleCommand())
                {
                    cmd.Connection = con;
                    con.Open();
                    OracleTransaction trans = con.BeginTransaction();
                    cmd.Transaction = trans;
                    try
                    {
                        MstID = _dbHelper2.GetMaxSl("DIST_PRODUCT_FACTORY_REL_MST", "MST_ID");

                        IuMode = "I";

                        string qryMst =
                            "INSERT INTO DIST_PRODUCT_FACTORY_REL_MST (" +
                                                                       " MST_ID," +
                                                                       " CUSTOMER_CODE"+
                                                                       ") " +
                                                               "Values (:MstId," +
                                                                       ":CustomerCode" +
                                                                       ")";
                        OracleParameter[] paramMst = new OracleParameter[]
                        {
                            new OracleParameter("MstId", MstID),
                            new OracleParameter("CustomerCode", mstData.CustomerCode)
                        };
                        cmd.CommandText = qryMst;
                        cmd.Parameters.Clear();
                        cmd.Parameters.AddRange(paramMst);
                        cmd.BindByName = true;
                        int noOfRowsMst = cmd.ExecuteNonQuery();
                        if (noOfRowsMst > 0)
                        {
                            DtlID = _dbHelper2.GetMaxSl("DIST_PRODUCT_FACTORY_REL_DTL", "DTL_ID");

                            foreach (var data in dtlData)
                            {

                                string qryDtl =
                                    "INSERT INTO DIST_PRODUCT_FACTORY_REL_DTL  (" +
                                                                                " DTL_ID, " +
                                                                                " MST_ID, " +
                                                                                " PRODUCT_CODE," +
                                                                                " FACTORY_CODE," +
                                                                                " CUSTOMER_CODE" +
                                                                                ")" +
                                                                       " Values (" +
                                                                                 ":DtlId," +
                                                                                 ":MstId," +
                                                                                 ":ProductCode," +
                                                                                 ":FactoryCode," +
                                                                                 ":CustomerCode" +
                                                                                 ")";

                                OracleParameter[] paramDtl = new OracleParameter[]
                                {
                                    new OracleParameter("DtlId", DtlID),
                                    new OracleParameter("MstId", MstID),
                                    new OracleParameter("ProductCode", data.ProductCode),
                                    new OracleParameter("FactoryCode", data.FactoryCode),
                                    new OracleParameter("CustomerCode", data.CustomerCode)
                                };
                                cmd.CommandText = qryDtl;
                                cmd.Parameters.Clear();
                                cmd.Parameters.AddRange(paramDtl);
                                isTrue = cmd.ExecuteNonQuery() > 0;
                                if (!isTrue)
                                {
                                    trans.Rollback();
                                    break;
                                }
                                DtlID = DtlID + 1;
                            }
                            trans.Commit();
                            return isTrue;
                        }
                    }
                    catch (Exception e)
                    {
                        var lineNum = e.StackTrace.Substring(e.StackTrace.LastIndexOf(' '));
                        _errorLogger.GetErrorMessage(e.Message, "DistProductFactoryRelationDAL", lineNum);
                        ExceptionReturn = e.Message;
                        trans.Rollback();
                        return false;

                    }
                }
            }
            return false;
        }

        internal bool UpdateData(DistProductFactoryRelationMstBEL mstData, List<DistProductFactoryRelationDtlBEL> dtlData)
        {
            bool isTrue = false;

            using (OracleConnection con = new OracleConnection(ConnStringSfblTrans))
            {
                using (OracleCommand cmd = new OracleCommand())
                {
                    cmd.Connection = con;
                    con.Open();
                    OracleTransaction trans = con.BeginTransaction();
                    cmd.Transaction = trans;
                    try
                    {
                        IuMode = "U";
                        DtlID = _dbHelper2.GetMaxSl("DIST_PRODUCT_FACTORY_REL_DTL", "DTL_ID");

                        foreach (var data in dtlData)
                        {
                            if (string.IsNullOrEmpty(data.DtlId))
                            {
                                string qryDtl =
                                    "INSERT INTO DIST_PRODUCT_FACTORY_REL_DTL  (" +
                                                                                " DTL_ID, " +
                                                                                " MST_ID, " +
                                                                                " PRODUCT_CODE," +
                                                                                " FACTORY_CODE," +
                                                                                " CUSTOMER_CODE" +
                                                                                ")" +
                                                                       " Values (" +
                                                                                 ":DtlId," +
                                                                                 ":MstId," +
                                                                                 ":ProductCode," +
                                                                                 ":FactoryCode," +
                                                                                 ":CustomerCode" +
                                                                                 ")";

                                OracleParameter[] paramDtl = new OracleParameter[]
                                {
                                    new OracleParameter("DtlId", DtlID),
                                    new OracleParameter("MstId", mstData.MstId),
                                    new OracleParameter("ProductCode", data.ProductCode),
                                    new OracleParameter("FactoryCode", data.FactoryCode),
                                    new OracleParameter("CustomerCode", data.CustomerCode)
                                };

                                cmd.CommandText = qryDtl;
                                cmd.Parameters.Clear();
                                cmd.Parameters.AddRange(paramDtl);
                                isTrue = cmd.ExecuteNonQuery() > 0;
                                if (!isTrue)
                                {
                                    trans.Rollback();
                                    break;
                                }
                                DtlID = DtlID + 1;
                            }


                        }
                        trans.Commit();
                        return isTrue;
                    }
                    catch (Exception e)
                    {
                        var lineNum = e.StackTrace.Substring(e.StackTrace.LastIndexOf(' '));
                        _errorLogger.GetErrorMessage(e.Message, "DistProductFactoryRelationDAL", lineNum);
                        ExceptionReturn = e.Message;
                        trans.Rollback();
                        return false;

                    }
                }
            }
        }


        public object GetSearchProduct(string param)
        {
            try
            {
                var qry = " select" +
                          " a.dtl_id," +
                          " a.mst_id," +
                          " a.product_code," +
                          " b.product_name," +
                          " b.pack_size," +
                          " a.factory_code," +
                          " c.factory_name," +
                          " a.customer_code," +
                          " d.customer_name" +
                          " from dist_product_factory_rel_dtl a, product_info b,vw_factory_list c,customer_info d" +
                          " where a.product_code=b.product_code" +
                          " and   a.factory_code=c.factory_code " +
                          " and   a.customer_code=d.customer_code " + param +
                          " order by dtl_id";

                DataTable dt = _dbHelper2.GetDataTable(qry);
                var item = (from DataRow row in dt.Rows
                            select new DistProductFactoryRelationDtlBEL
                            {
                                DtlId = row["dtl_id"].ToString(),
                                MstId = row["mst_id"].ToString(),
                                ProductCode = row["product_code"].ToString(),
                                ProductName = row["product_name"].ToString(),
                                PackSize = row["pack_size"].ToString(),
                                FactoryCode = row["factory_code"].ToString(),
                                FactoryName = row["factory_name"].ToString(),
                                CustomerCode = row["customer_code"].ToString(),
                                CustomerName = row["customer_name"].ToString()
                            }).ToList();
                return item;

            }
            catch (Exception e)
            {
                var lineNum = e.StackTrace.Substring(e.StackTrace.LastIndexOf(' '));
                _errorLogger.GetErrorMessage(e.Message, "DistProductFactoryRelationDAL", lineNum);
                ExceptionReturn = e.Message;
                return "Not Ok";
            }
        }


        public object SearchMstData()
        {
            try
            {
                var qry = " select" +
                          " a.mst_id," +                          
                          " a.customer_code," +
                          " b.customer_name" +
                          " from dist_product_factory_rel_mst a,customer_info b" +
                          " where a.customer_code=b.customer_code(+) "+
                          " order by a.mst_id desc";

                DataTable dt = _dbHelper2.GetDataTable(qry);
                var item = (from DataRow row in dt.Rows
                            select new DistProductFactoryRelationMstBEL
                            {
                                MstId = row["mst_id"].ToString(),
                                CustomerCode = row["customer_code"].ToString(),
                                CustomerName = row["customer_name"].ToString()
                            }).ToList();
                return item;

            }
            catch (Exception e)
            {
                var lineNum = e.StackTrace.Substring(e.StackTrace.LastIndexOf(' '));
                _errorLogger.GetErrorMessage(e.Message, "DistProductFactoryRelationDAL", lineNum);
                ExceptionReturn = e.Message;
                return "Not Ok";
            }
        }


        public bool DeleteProduct(string DtlId)
        {
            bool isTrue = false;
            using (OracleConnection con = new OracleConnection(ConnStringSfblTrans))
            {
                using (OracleCommand cmd = new OracleCommand())
                {
                    cmd.Connection = con;
                    con.Open();
                    OracleTransaction trans = con.BeginTransaction();
                    cmd.Transaction = trans;
                    try
                    {
                        IuMode = "D";
                        var qryDtl = "DELETE  FROM DIST_PRODUCT_FACTORY_REL_DTL WHERE DTL_ID=:DtlId";
                        OracleParameter[] paramDtl = new OracleParameter[]
                               {
                                    new OracleParameter("DtlId", DtlId)
                               };
                        cmd.CommandText = qryDtl;
                        cmd.Parameters.Clear();
                        cmd.Parameters.AddRange(paramDtl);
                        cmd.BindByName = true;
                        isTrue = cmd.ExecuteNonQuery() > 0;

                        if (!isTrue)
                        {
                            trans.Rollback();
                        }
                        else
                        {
                            trans.Commit();
                        }

                        return isTrue;


                    }
                    catch (Exception e)
                    {
                        var lineNum = e.StackTrace.Substring(e.StackTrace.LastIndexOf(' '));
                        _errorLogger.GetErrorMessage(e.Message, "DistProductFactoryRelationDAL", lineNum);
                        ExceptionReturn = e.Message;
                        return false;
                    }
                }
            }
        }




        public bool DeleteMstDtl(string MstId)
        {
            bool isTrue = false;
            using (OracleConnection con = new OracleConnection(ConnStringSfblTrans))
            {
                using (OracleCommand cmd = new OracleCommand())
                {
                    cmd.Connection = con;
                    con.Open();
                    OracleTransaction trans = con.BeginTransaction();
                    cmd.Transaction = trans;
                    try
                    {
                        IuMode = "D";
                        var qryProdDtl = "DELETE  FROM DIST_PRODUCT_FACTORY_REL_DTL WHERE MST_ID=:MstId";
                        OracleParameter[] paramDtl = new OracleParameter[]
                               {
                                    new OracleParameter("MstId", MstId)
                               };
                        cmd.CommandText = qryProdDtl;
                        cmd.Parameters.Clear();
                        cmd.Parameters.AddRange(paramDtl);
                        cmd.BindByName = true;
                        isTrue = cmd.ExecuteNonQuery() > 0;

                        //if (!isTrue)
                        //{
                            //trans.Rollback();
                        //}
                        //else
                        //{

                            IuMode = "D";
                            var qryTypeDtl = "DELETE FROM DIST_PRODUCT_FACTORY_REL_MST WHERE MST_ID=:MstId";
                            OracleParameter[] paramMst = new OracleParameter[]
                                   {
                                    new OracleParameter("MstId", MstId)
                                   };
                            cmd.CommandText = qryTypeDtl;
                            cmd.Parameters.Clear();
                            cmd.Parameters.AddRange(paramMst);
                            cmd.BindByName = true;
                            isTrue = cmd.ExecuteNonQuery() > 0;

                                if (!isTrue)
                                {
                                    trans.Rollback();
                                }
                                else
                                {
                                trans.Commit();
                                }
                        //}
                    

                        return isTrue;


                    }
                    catch (Exception e)
                    {
                        var lineNum = e.StackTrace.Substring(e.StackTrace.LastIndexOf(' '));
                        _errorLogger.GetErrorMessage(e.Message, "UserBaseReportAccessDAL", lineNum);
                        ExceptionReturn = e.Message;
                        return false;
                    }
                }
            }
        }





    }
}