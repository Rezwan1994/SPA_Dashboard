using System;
using System.Data;
using System.Linq;
using System.Web;
using SalesWeb.Areas.SpaMisReport.Models.BEL;
using SalesWeb.Universal.Gateway;
using System.Collections.Generic;
using static SalesWeb.Areas.SpaMisReport.Models.BEL.TestBEL;
using Oracle.ManagedDataAccess.Client;
//using System.Data.OracleClient;

namespace SalesWeb.Areas.SpaMisReport.Models.DAL
{
    public class TestDAL : ReturnData
    {
        private readonly DBHelper _dbHelper = new DBHelper();
        private readonly ErrorLogger _errorLogger = new ErrorLogger();
        private static readonly DBConnection DBConnection = new DBConnection();


        public readonly string ConnString = DBConnection.SAConnStrReader("Oracle", "PHARMAERP");

        public string OutMsg {get;set;}















        public List<TestBEL> GetProductList()
        {
            try
            {
                var qry = " select" +
                          " product_code," +
                          " product_name," +
                          " pack_size" +
                          " from product_info";
                          
                DataTable dt = _dbHelper.GetDataTable(qry);

                var item = (from DataRow row in dt.Rows
                            select new TestBEL
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
                _errorLogger.GetErrorMessage(e.Message, "TestDAL", lineNum);
                ExceptionReturn = e.Message;
                throw;
            }
        }


        internal bool InsertOrderEntry(OrderMstBEL OrderMst, List<OrderDtlBEL> OrderDtl)
        {
            bool isTrue = false;
            int empId = Convert.ToInt32(HttpContext.Current.Session["EMPLOYEE_ID"]);

            using (OracleConnection con = new OracleConnection(ConnString))
            {
                using (OracleCommand cmd = new OracleCommand())
                {
                    cmd.Connection = con;
                    con.Open();
                    OracleTransaction trans = con.BeginTransaction();
                    cmd.Transaction = trans;
                    try
                    {

                        MaxID = _dbHelper.GetMaxSl("TEST_ORDER_MST", "MST_ID");
                        IuMode = "I";

                        string qryMst = "INSERT INTO TEST_ORDER_MST ( MST_ID,  " +
                                                               " CUSTOMER_CODE," +
                                                               " ORDER_STATUS" +
                                                               ")" +
                                                       "Values (:MstId," +
                                                               ":CustomerCode," +
                                                               ":OrderStatus" +
                                                               ")";

                        OracleParameter[] paramMst = new OracleParameter[]
                        {
                            new OracleParameter("MstId", MaxID),
                            new OracleParameter("CustomerCode", OrderMst.CustomerCode),
                            new OracleParameter("OrderStatus", OrderMst.OrderStatus),
                        };
                        cmd.CommandText = qryMst;
                        cmd.Parameters.Clear();
                        cmd.Parameters.AddRange(paramMst);
                        cmd.BindByName = true;
                        int noOfRowsMst = cmd.ExecuteNonQuery();
                        if (noOfRowsMst > 0)
                        {

                            var MaxDtlID = _dbHelper.GetMaxSl("TEST_ORDER_DTL", "DTL_ID");

                            foreach (var data in OrderDtl)
                            {
                                string qryDtl = "INSERT INTO TEST_ORDER_DTL     ( DTL_ID," +
                                                                                " MST_ID," +
                                                                                " PRODUCT_CODE," +
                                                                                " ORDER_QTY" +
                                                                                ")" +
                                                                       " Values (:DtlId," +
                                                                               " :MstId," +
                                                                               " :ProductCode," +
                                                                               " :OrderQty" +
                                                                               ")";
                                OracleParameter[] paramDtl = new OracleParameter[]
                                {
                                    new OracleParameter("DtlId", MaxDtlID),
                                    new OracleParameter("MstId", MaxID),
                                    new OracleParameter("ProductCode", data.ProductCode),
                                    new OracleParameter("OrderQty", data.OrderQty)
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
                                MaxDtlID += 1;
                            }
                            trans.Commit();
                            return isTrue;
                        }
                    }
                    catch (Exception e)
                    {
                        var lineNum = e.StackTrace.Substring(e.StackTrace.LastIndexOf(' '));
                        _errorLogger.GetErrorMessage(e.Message, "TestDAL", lineNum);
                        ExceptionReturn = e.Message;
                        trans.Rollback();
                        return false;

                    }
                }
            }
            return false;
        }


        internal bool UpdateOrderEntry(OrderMstBEL OrderMst, List<OrderDtlBEL> OrderDtl)
        {
            bool isTrue = false;
            int empId = Convert.ToInt32(HttpContext.Current.Session["EMPLOYEE_ID"]);
            using (OracleConnection con = new OracleConnection(ConnString))
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

                        string qryMst =
                            " UPDATE  TEST_ORDER_MST" +
                            " SET" +
                            " ORDER_STATUS=:OrderStatus" +
                            " WHERE MST_ID=:MstId";

                        OracleParameter[] paramMst = new OracleParameter[]
                        {
                            new OracleParameter("MstId", OrderMst.MstId),
                            new OracleParameter("OrderStatus", OrderMst.OrderStatus)
                        };
                        cmd.CommandText = qryMst;
                        cmd.Parameters.Clear();
                        cmd.Parameters.AddRange(paramMst);
                        cmd.BindByName = true;
                        int noOfRowsMst = cmd.ExecuteNonQuery();
                        if (noOfRowsMst > 0)
                        {


                            var MaxDtlID = _dbHelper.GetMaxSl("TEST_ORDER_DTL", "DTL_ID");

                            foreach (var data in OrderDtl)
                            {
                                if (!string.IsNullOrEmpty(data.DtlId))
                                {

                                    string qryDtl =
                                        " UPDATE  TEST_ORDER_DTL" +
                                        " SET  " +
                                        " ORDER_QTY=:OrderQty" +
                                        " WHERE MST_ID=:MstId" +
                                        " AND DTL_ID=:DtlId" +
                                        " AND PRODUCT_CODE=:ProductCode";

                                    OracleParameter[] paramDtl = new OracleParameter[]
                                    {
                                    new OracleParameter("MstId", OrderMst.MstId),
                                    new OracleParameter("DtlId", data.DtlId),
                                    new OracleParameter("ProductCode", data.ProductCode),
                                    new OracleParameter("OrderQty", data.OrderQty)
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


                                }
                                else
                                {

                                    string qryDtl = "INSERT INTO TEST_ORDER_DTL     ( DTL_ID," +
                                                                                " MST_ID," +
                                                                                " PRODUCT_CODE," +
                                                                                " ORDER_QTY" +
                                                                                ")" +
                                                                       " Values (:DtlId," +
                                                                               " :MstId," +
                                                                               " :ProductCode," +
                                                                               " :OrderQty" +
                                                                               ")";
                                    OracleParameter[] paramDtl = new OracleParameter[]
                                    {
                                    new OracleParameter("DtlId", MaxDtlID),
                                    new OracleParameter("MstId", OrderMst.MstId),
                                    new OracleParameter("ProductCode", data.ProductCode),
                                    new OracleParameter("OrderQty", data.OrderQty)
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
                                    MaxDtlID += 1;

                                };   

                            }

                            trans.Commit();
                            return isTrue;
                        }
                    }
                    catch (Exception e)
                    {
                        var lineNum = e.StackTrace.Substring(e.StackTrace.LastIndexOf(' '));
                        _errorLogger.GetErrorMessage(e.Message, "TestDAL", lineNum);
                        ExceptionReturn = e.Message;
                        trans.Rollback();
                        return false;

                    }
                }
            }
            return false;
        }


        public List<OrderMstBEL> GetOrderMst(string mstId)
        {
            try
            {
                var qry = " select" +
                          "  a.mst_id, a.customer_code,b.customer_name, a.order_status " +
                          " from test_order_mst a, customer_info b" +
                          " where a.customer_code=b.customer_code " +
                          " and a.mst_id=" + 1 +" "+
                          " order by  a.mst_id";

                DataTable dt = _dbHelper.GetDataTable(qry);

                var item = (from DataRow row in dt.Rows
                            select new OrderMstBEL
                            {
                                MstId = row["MST_ID"].ToString(),
                                CustomerCode = row["CUSTOMER_CODE"].ToString(),
                                CustomerName = row["CUSTOMER_NAME"].ToString(),
                                OrderStatus = row["ORDER_STATUS"].ToString()
                            }).ToList();
                return item;

            }
            catch (Exception e)
            {
                var lineNum = e.StackTrace.Substring(e.StackTrace.LastIndexOf(' '));
                _errorLogger.GetErrorMessage(e.Message, "TestDAL", lineNum);
                ExceptionReturn = e.Message;
                throw;
            }
        }




        public List<OrderDtlBEL> GetOrderDtl(string mstId)
        {
            try
            {
                var qry = " select" +
                          "  b.dtl_id, b.mst_id, b.product_code,c.product_name,c.pack_size, b.order_qty" +
                          " from test_order_mst a, test_order_dtl b,product_info c "+
                          " where a.mst_id=b.mst_id "+
                          " and b.product_code=c.product_code "+
                          " and a.mst_id="+ mstId +
                          " order by  b.dtl_id";

                DataTable dt = _dbHelper.GetDataTable(qry);

                var item = (from DataRow row in dt.Rows
                            select new OrderDtlBEL
                            {
                                MstId = row["MST_ID"].ToString(),
                                DtlId = row["DTL_ID"].ToString(),
                                ProductCode = row["PRODUCT_CODE"].ToString(),
                                ProductName = row["PRODUCT_NAME"].ToString(),
                                PackSize = row["PACK_SIZE"].ToString(),
                                OrderQty = row["ORDER_QTY"].ToString()

                            }).ToList();
                return item;

            }
            catch (Exception e)
            {
                var lineNum = e.StackTrace.Substring(e.StackTrace.LastIndexOf(' '));
                _errorLogger.GetErrorMessage(e.Message, "TestDAL", lineNum);
                ExceptionReturn = e.Message;
                throw;
            }
        }





    }
}