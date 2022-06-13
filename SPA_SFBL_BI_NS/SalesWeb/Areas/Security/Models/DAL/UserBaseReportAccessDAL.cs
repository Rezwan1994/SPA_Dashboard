using System;
using System.Data;
using System.Linq;
using SalesWeb.Areas.Security.Models.BEL;
using SalesWeb.Universal.Gateway;
using System.Collections.Generic;
using Oracle.ManagedDataAccess.Client;
using static SalesWeb.Areas.Security.Models.BEL.UserBaseReportAccessBEL;

namespace SalesWeb.Areas.Security.Models.DAL
{
    public class UserBaseReportAccessDAL : ReturnData
    {

        private readonly DBHelper _dbHelper = new DBHelper();
        private readonly ErrorLogger _errorLogger = new ErrorLogger();
        private static readonly DBConnection DBConnection = new DBConnection();
        public readonly string ConnString = DBConnection.SAConnStrReader("Oracle", "PHARMAERP");


        public object GetUserList()
        {
            try
            {
                var qry = " select" +
                          " a.user_id," +
                          " a.user_name," +
                          " a.employee_id," +
                          " b.employee_code," +
                          " b.employee_name" +
                          " from sc_user_login a, sc_employee_info b" +
                          " where a.employee_id=b.employee_id" +
                          //" and a.user_id not in (select nvl(user_id,0) from user_product_mst)" +
                          " order by initcap(a.user_name)";

                DataTable dt = _dbHelper.GetDataTable(qry);
                var item = (from DataRow row in dt.Rows
                            select new UserBEL
                            {
                                UserId = Convert.ToInt32(row["user_id"]),
                                UserName = row["user_name"].ToString(),
                                EmployeeId = Convert.ToInt32(row["employee_id"]),
                                EmployeeCode = row["employee_code"].ToString(),
                                EmployeeName = row["employee_name"].ToString()

                            }).ToList();
                return item;

            }
            catch (Exception e)
            {
                var lineNum = e.StackTrace.Substring(e.StackTrace.LastIndexOf(' '));
                _errorLogger.GetErrorMessage(e.Message, "UserBaseReportAccessDAL", lineNum);
                ExceptionReturn = e.Message;
                return "Not Ok";
            }
        }
        public object GetTypeList( string pType)
        {
            var qry="";
            try
            {  
                if (pType == "BASE_PRODUCT")
                {
                    qry = " select" +
                          " base_product_name name," +
                          " base_product_code code," +
                          " 'BASE_PRODUCT' TypeName" +
                          " from mv_base_product_info" +
                          " where status='A'" +
                          " order by base_product_name";
                }
                else if (pType == "CATEGORY")
                {
                    qry = " select" +
                          " category_name name," +
                          " category_code code," +
                          " 'CATEGORY' TypeName" +
                          " from mv_category_info" +
                          " order by category_name";
                }
                else if (pType == "BRAND")
                {
                    qry = " select" +
                          " brand_name name," +
                          " brand_code code," +
                          " 'BRAND' TypeName" +
                          " from mv_brand_info" +
                          " where brand_status='A'" +
                          " order by brand_name";
                }

                DataTable dt = _dbHelper.GetDataTable(qry);
                var item = (from DataRow row in dt.Rows
                            select new UserBEL
                            {
                                Name = row["name"].ToString(),
                                Code = row["code"].ToString(),
                                TypeName = row["TypeName"].ToString()

                            }).ToList();
                return item;

            }
            catch (Exception e)
            {
                var lineNum = e.StackTrace.Substring(e.StackTrace.LastIndexOf(' '));
                _errorLogger.GetErrorMessage(e.Message, "UserBaseReportAccessDAL", lineNum);
                ExceptionReturn = e.Message;
                return "Not Ok";
            }
        }
        internal bool InsertUserBaseReportAccess(UserMstBEL MstData, List<UserProductTypeBEL> DtlData)
        {
            bool isTrue = false;

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
                        MstID = _dbHelper.GetMaxSl("USER_PRODUCT_MST", "MST_ID");
                        
                        IuMode = "I";

                        string qryMst =
                            "INSERT INTO USER_PRODUCT_MST (" +
                                                           " MST_ID," +
                                                           " USER_ID," +
                                                           " TYPE_NAME" +
                                                           ") " +
                                                   "Values (:MstId," +
                                                           ":UserId," +
                                                           ":TypeName" +
                                                           ")";
                        OracleParameter[] paramMst = new OracleParameter[]
                        {
                            new OracleParameter("MstId", MstID),
                            new OracleParameter("UserId", MstData.UserId),
                            new OracleParameter("TypeName", MstData.TypeName)
                        };
                        cmd.CommandText = qryMst;
                        cmd.Parameters.Clear();
                        cmd.Parameters.AddRange(paramMst);
                        cmd.BindByName = true;
                        int noOfRowsMst = cmd.ExecuteNonQuery();
                        if (noOfRowsMst > 0)
                        {
                            ProductTypeID = _dbHelper.GetMaxSl("USER_PRODUCT_TYPE", "PRODUCT_TYPE_ID");

                            foreach (var data in DtlData)
                            {
                                

                                string qryDtl =
                                    "INSERT INTO USER_PRODUCT_TYPE (" +
                                                                    " PRODUCT_TYPE_ID, " +
                                                                    " MST_ID, " +
                                                                    " USER_ID," +
                                                                    " PRODUCT_TYPE_CODE," +
                                                                    " PRODUCT_TYPE_DESC," +
                                                                    " TYPE_NAME" +
                                                                    ")" +
                                                           " Values (" +
                                                                     ":TypeDtlId," +
                                                                     ":MstId," +
                                                                     ":UserId," +
                                                                     ":ProductTypeCode," +
                                                                     ":ProductTypeName," +
                                                                     ":TypeName" +
                                                                     ")";

                                OracleParameter[] paramDtl = new OracleParameter[]
                                {
                                    new OracleParameter("TypeDtlId", ProductTypeID),
                                    new OracleParameter("MstId", MstID),
                                    new OracleParameter("UserId", MstData.UserId),
                                    new OracleParameter("ProductTypeCode", data.ProductTypeCode),
                                    new OracleParameter("ProductTypeName", data.ProductTypeName),
                                    new OracleParameter("TypeName", MstData.TypeName)
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
                                ProductTypeID = ProductTypeID + 1;
                            }
                            trans.Commit();
                            return isTrue;
                        }
                    }
                    catch (Exception e)
                    {
                        var lineNum = e.StackTrace.Substring(e.StackTrace.LastIndexOf(' '));
                        _errorLogger.GetErrorMessage(e.Message, "GdnConfirmDAL", lineNum);
                        ExceptionReturn = e.Message;
                        trans.Rollback();
                        return false;

                    }
                }
            }
            return false;
        }

        internal bool UpdateUserBaseReportAccess(UserMstBEL MstData, List<UserProductTypeBEL> DtlData)
        {
            bool isTrue = false;

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
                        ProductTypeID = _dbHelper.GetMaxSl("USER_PRODUCT_TYPE", "PRODUCT_TYPE_ID");

                        foreach (var data in DtlData)
                        {
                            if (string.IsNullOrEmpty(data.ProductTypeId))
                            {
                                string qryDtl =
                                    "INSERT INTO USER_PRODUCT_TYPE (" +
                                                                    " PRODUCT_TYPE_ID, " +
                                                                    " MST_ID, " +
                                                                    " USER_ID," +
                                                                    " PRODUCT_TYPE_CODE," +
                                                                    " PRODUCT_TYPE_DESC," +
                                                                    " TYPE_NAME" +
                                                                    ")" +
                                                           " Values (" +
                                                                     ":TypeDtlId," +
                                                                     ":MstId," +
                                                                     ":UserId," +
                                                                     ":ProductTypeCode," +
                                                                     ":ProductTypeName," +
                                                                     ":TypeName" +
                                                                     ")";

                                OracleParameter[] paramDtl = new OracleParameter[]
                                {
                                                new OracleParameter("TypeDtlId", ProductTypeID),
                                                new OracleParameter("MstId", MstData.MstId),
                                                new OracleParameter("UserId", MstData.UserId),
                                                new OracleParameter("ProductTypeCode", data.ProductTypeCode),
                                                new OracleParameter("ProductTypeName", data.ProductTypeName),
                                                new OracleParameter("TypeName", MstData.TypeName)
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
                                ProductTypeID = ProductTypeID + 1;
                            }


                        }
                        trans.Commit();
                        return isTrue;
                    }
                    catch (Exception e)
                    {
                        var lineNum = e.StackTrace.Substring(e.StackTrace.LastIndexOf(' '));
                        _errorLogger.GetErrorMessage(e.Message, "GdnConfirmDAL", lineNum);
                        ExceptionReturn = e.Message;
                        trans.Rollback();
                        return false;

                    }
                }
            }
        }

        public object SearchData()
        {
            try
            {
                var qry = " select" +
                          " a.mst_id," +
                          " a.user_id," +
                          " b.user_name," +
                          " c.employee_code," +
                          " c.employee_name," +
                          " a.type_name" +
                          " from user_product_mst a, sc_user_login b,sc_employee_info c" +
                          " where a.user_id=b.user_id" +
                          " and b.employee_id=c.employee_id" +
                          " order by initcap(b.user_name)";

                DataTable dt = _dbHelper.GetDataTable(qry);
                var item = (from DataRow row in dt.Rows
                            select new UserBEL
                            {
                                MstId = Convert.ToInt32(row["mst_id"]),
                                UserId = Convert.ToInt32(row["user_id"]),
                                UserName = row["user_name"].ToString(),
                                EmployeeCode = row["employee_code"].ToString(),
                                EmployeeName = row["employee_name"].ToString(),
                                TypeName = row["type_name"].ToString()

                            }).ToList();
                return item;

            }
            catch (Exception e)
            {
                var lineNum = e.StackTrace.Substring(e.StackTrace.LastIndexOf(' '));
                _errorLogger.GetErrorMessage(e.Message, "UserBaseReportAccessDAL", lineNum);
                ExceptionReturn = e.Message;
                return "Not Ok";
            }
        }

        public object GetUserProductType(string param)
        {
            try
            {
                var qry = " select" +
                          " product_type_id,mst_id,user_id,product_type_code,product_type_desc,type_name" +
                          " from user_product_type" +
                          " where 1=1 "+ param +
                          " order by product_type_id";

                DataTable dt = _dbHelper.GetDataTable(qry);
                var item = (from DataRow row in dt.Rows
                            select new UserProductTypeBEL
                            {
                                ProductTypeId = row["product_type_id"].ToString(),
                                MstId = Convert.ToInt32(row["mst_id"]),
                                UserId = Convert.ToInt32(row["user_id"]),
                                ProductTypeCode = row["product_type_code"].ToString(),
                                ProductTypeName = row["product_type_desc"].ToString(),
                                TypeName = row["type_name"].ToString()

                            }).ToList();
                return item;

            }
            catch (Exception e)
            {
                var lineNum = e.StackTrace.Substring(e.StackTrace.LastIndexOf(' '));
                _errorLogger.GetErrorMessage(e.Message, "UserBaseReportAccessDAL", lineNum);
                ExceptionReturn = e.Message;
                return "Not Ok";
            }
        }

        public object GetUserProduct(string param)
        {
            try
            {
                var qry = " select" +
                          " product_dtl_id," +
                          " product_type_id," +
                          " user_id," +
                          " product_code," +
                          " product_name," +
                          " pack_size," +
                          " product_type_code," +
                          " product_type_desc," +
                          " type_name" +
                          " from user_product_dtl" +
                          " where 1=1 " + param +
                          " order by product_type_id,product_dtl_id";

                DataTable dt = _dbHelper.GetDataTable(qry);
                var item = (from DataRow row in dt.Rows
                            select new UserProductBEL
                            {
                                ProductDtlId = Convert.ToInt32(row["product_dtl_id"]),
                                ProductTypeId = Convert.ToInt32(row["product_type_id"]),
                                UserId = Convert.ToInt32(row["user_id"]),

                                ProductCode = row["product_code"].ToString(),
                                ProductName = row["product_name"].ToString(),
                                PackSize = row["pack_size"].ToString(),

                                ProductTypeCode = row["product_type_code"].ToString(),
                                ProductTypeName = row["product_type_desc"].ToString(),
                                TypeName = row["type_name"].ToString()

                            }).ToList();
                return item;

            }
            catch (Exception e)
            {
                var lineNum = e.StackTrace.Substring(e.StackTrace.LastIndexOf(' '));
                _errorLogger.GetErrorMessage(e.Message, "UserBaseReportAccessDAL", lineNum);
                ExceptionReturn = e.Message;
                return "Not Ok";
            }
        }




        public bool DeleteProduct(string productDtlId)
        {
            bool isTrue = false;
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
                        IuMode = "D";
                        var qryDtl = "DELETE  FROM USER_PRODUCT_DTL WHERE PRODUCT_DTL_ID=:ProductDtlId";
                        OracleParameter[] paramDtl = new OracleParameter[]
                               {
                                    new OracleParameter("ProductDtlId", productDtlId)
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
                        _errorLogger.GetErrorMessage(e.Message, "UserBaseReportAccessDAL", lineNum);
                        ExceptionReturn = e.Message;
                        return false;
                    }
                }
            }
        }
        public bool DeleteProductType(string typeId)
        {
            bool isTrue = false;
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
                        IuMode = "D";
                        var qryDtl = "DELETE  FROM USER_PRODUCT_TYPE WHERE PRODUCT_TYPE_ID=:ProductTypeId";
                        OracleParameter[] paramDtl = new OracleParameter[]
                               {
                                    new OracleParameter("ProductTypeId", typeId)
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
                        _errorLogger.GetErrorMessage(e.Message, "UserBaseReportAccessDAL", lineNum);
                        ExceptionReturn = e.Message;
                        return false;
                    }
                }
            }
        }
        public bool DeleteUserAccess(string UserID)
        {
            bool isTrue = false;
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
                        IuMode = "D";
                        var qryProdDtl = "DELETE  FROM USER_PRODUCT_DTL WHERE USER_ID=:UserId";
                        OracleParameter[] paramProdDtl = new OracleParameter[]
                               {
                                    new OracleParameter("UserId", UserID)
                               };
                        cmd.CommandText = qryProdDtl;
                        cmd.Parameters.Clear();
                        cmd.Parameters.AddRange(paramProdDtl);
                        cmd.BindByName = true;
                        isTrue = cmd.ExecuteNonQuery() > 0;

                        //if (!isTrue)
                        //{
                        //    trans.Rollback();
                        //}
                        //else
                        //{

                            IuMode = "D";
                            var qryTypeDtl = "DELETE  FROM USER_PRODUCT_TYPE WHERE USER_ID=:UserId";
                            OracleParameter[] paramTypeDtl = new OracleParameter[]
                                   {
                                    new OracleParameter("UserId", UserID)
                                   };
                            cmd.CommandText = qryTypeDtl;
                            cmd.Parameters.Clear();
                            cmd.Parameters.AddRange(paramTypeDtl);
                            cmd.BindByName = true;
                            isTrue = cmd.ExecuteNonQuery() > 0;

                            //if (!isTrue)
                            //{
                            //    trans.Rollback();
                            //}
                            //else
                            //{

                                IuMode = "D";
                                var qryMst = "DELETE  FROM USER_PRODUCT_MST WHERE USER_ID=:UserId";
                                OracleParameter[] paramMst = new OracleParameter[]
                                       {
                                    new OracleParameter("UserId", UserID)
                                       };
                                cmd.CommandText = qryMst;
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