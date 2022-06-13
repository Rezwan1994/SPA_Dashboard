using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using SalesWeb.Areas.Security.Models.BEL;
using SalesWeb.Universal.Gateway;

namespace SalesWeb.Areas.Security.Models.DAL
{
    public class UserInfoDAL : ReturnData
    {

        private readonly DBHelper _dbHelper = new DBHelper();
        private readonly IDGenerated _idGenerated = new IDGenerated();
        private readonly ErrorLogger _errorLogger = new ErrorLogger();



        public bool InsertUserInfo(UserInfoBEL master)
        {
            try
            {
                MaxID = _dbHelper.GetMaxSl("SC_USER_LOGIN", "USER_ID");
                IuMode = "I";
                string qry = "Insert into SC_USER_LOGIN(USER_ID, USER_NAME, PASSWORD, STATUS, ACCESS_LOCATION, EMPLOYEE_ID,LOCATION_ID) Values('" + MaxID + "', '" + master.UserName + "', '" + master.Password + "', '" + master.Status + "', '" + master.AccessLocation + "', '" + master.EmployeeId + "', '" + master.LocationId + "')";
                return _dbHelper.CmdExecute(qry, IuMode, "UserInfo", "SC_USER_LOGIN", MaxID);
            }
            catch (Exception e)
            {
                var lineNum = e.StackTrace.Substring(e.StackTrace.LastIndexOf(' '));
                _errorLogger.GetErrorMessage(e.Message, "UserInfoDAL", lineNum);
                ExceptionReturn = e.Message;
                return false;
            }

        }


        public bool UpdateUserInfo(UserInfoBEL master)
        {
            try
            {
                MaxID = master.UserId;
                IuMode = "U";
                string qry = "Update SC_USER_LOGIN set USER_NAME='" + master.UserName + "',PASSWORD='" + master.Password + "',ACCESS_LOCATION='" + master.AccessLocation + "',EMPLOYEE_ID='" + master.EmployeeId + "',STATUS='" + master.Status + "',LOCATION_ID='" + master.LocationId + "' Where USER_ID='" + master.UserId + "'";
                return _dbHelper.CmdExecute(qry, IuMode, "UserInfo", "SC_USER_LOGIN", MaxID);
            }
            catch (Exception e)
            {

                var lineNum = e.StackTrace.Substring(e.StackTrace.LastIndexOf(' '));
                _errorLogger.GetErrorMessage(e.Message, "UserInfoDAL", lineNum);
                ExceptionReturn = e.Message;
                return false;
            }

        }

        public object GetUserList()
        {
            string qry = " SELECT" +
                          " USER_ID," +
                          " USER_NAME," +
                          " PASSWORD," +
                          " ACCESS_LOCATION," +
                          " LOCATION_ID," +
                          " FN_LOCATION_NAME(LOCATION_ID, ACCESS_LOCATION) LOCATION_NAME," +
                          " U.EMPLOYEE_ID, " +
                          " EMPLOYEE_CODE," +
                          " EMPLOYEE_NAME," +
                          " STATUS" +
                          " FROM SC_USER_LOGIN U " +
                          " INNER JOIN SC_EMPLOYEE_INFO E ON U.EMPLOYEE_ID = E.EMPLOYEE_ID";

            DataTable dt = _dbHelper.GetDataTable(qry);
            var item = (from DataRow row in dt.Rows
                        select new UserInfoBEL
                        {
                            UserId = Convert.ToInt32(row["USER_ID"]),
                            UserName = row["USER_NAME"].ToString(),
                            Password = row["PASSWORD"].ToString(),
                            AccessLocation = row["ACCESS_LOCATION"].ToString(),
                            EmployeeId = Convert.ToInt32(row["EMPLOYEE_ID"]),
                            EmployeeCode = row["EMPLOYEE_CODE"].ToString(),
                            EmployeeName = row["EMPLOYEE_NAME"].ToString(),
                            LocationId = Convert.ToInt32(row["LOCATION_ID"]),
                            LocationName = row["LOCATION_NAME"].ToString(),
                            Status = row["STATUS"].ToString(),

                        }).ToList();
            return item;
        }


        public List<UserInfoBEL> GetDepotList()
        {
            try
            {

                string qry = " SELECT DEPOT_ID LOCATION_ID,DEPOT_CODE LOCATION_CODE, DEPOT_NAME LOCATION_NAME,'Depot'LOCATION_TYPE" +
                             " FROM DEPOT_INFO" +
                             " ORDER BY DEPOT_ID";

                DataTable dt = _dbHelper.GetDataTable(qry);
                var item = (from DataRow row in dt.Rows
                            select new UserInfoBEL
                            {
                                LocationId = Convert.ToInt32(row["LOCATION_ID"]),
                                LocationName = row["LOCATION_NAME"].ToString(),
                                LocationCode = row["LOCATION_CODE"].ToString(),
                                LocationType = row["LOCATION_TYPE"].ToString()
                            }).ToList();
                return item;



            }
            catch (Exception e)
            {
                var lineNum = e.StackTrace.Substring(e.StackTrace.LastIndexOf(' '));
                _errorLogger.GetErrorMessage(e.Message, "UserInfoDAL", lineNum);
                ExceptionReturn = e.Message;
                throw;
            }
        }


        public object GetEmployeeList(string param)
        {
            try
            {
                var qry = "SELECT EMPLOYEE_ID, EMPLOYEE_CODE, EMPLOYEE_NAME FROM SC_EMPLOYEE_INFO WHERE 1=1  " + param + "" +
                                "  ORDER BY EMPLOYEE_NAME";
                DataTable dt = _dbHelper.GetDataTable(qry);

                var item = (from DataRow row in dt.Rows
                            select new EmpBEL
                            {
                                EmployeeId = Convert.ToInt32(row["EMPLOYEE_ID"]),
                                EmployeeCode = row["EMPLOYEE_CODE"].ToString(),
                                EmployeeName = row["EMPLOYEE_NAME"].ToString()
                            }).ToList();
                return item;

            }
            catch (Exception e)
            {
                var lineNum = e.StackTrace.Substring(e.StackTrace.LastIndexOf(' '));
                _errorLogger.GetErrorMessage(e.Message, "EmployeeInfoDAl", lineNum);
                ExceptionReturn = e.Message;
                throw;
            }
        }




        //public object GetZoneList()
        //{
        //    try
        //    {
        //        var qry = " SELECT ZONE_ID LOCATION_ID,ZONE_CODE LOCATION_CODE, ZONE_NAME LOCATION_NAME, 'Zone' LOCATION_TYPE" +
        //                  " FROM ZONE_INFO" +
        //                  " WHERE ZONE_STATUS='Active'" +
        //                  " AND ZONE_ID NOT IN (SELECT NVL(LOCATION_ID,0) FROM SC_USER_LOGIN WHERE ACCESS_LOCATION='Zone' AND STATUS='Active')" +
        //                  " ORDER BY ZONE_NAME";
        //        DataTable dt = _dbHelper.GetDataTable(qry);
        //        var item = (from DataRow row in dt.Rows
        //                    select new UserInfoBEL
        //                    {
        //                        LocationId = Convert.ToInt32(row["LOCATION_ID"]),
        //                        LocationCode = row["LOCATION_CODE"].ToString(),
        //                        LocationName = row["LOCATION_NAME"].ToString(),
        //                        LocationType = row["LOCATION_TYPE"].ToString()
        //                    }).ToList();
        //        return item;

        //    }
        //    catch (Exception e)
        //    {
        //        var lineNum = e.StackTrace.Substring(e.StackTrace.LastIndexOf(' '));
        //        _errorLogger.GetErrorMessage(e.Message, "UserInfoDAL", lineNum);
        //        ExceptionReturn = e.Message;
        //        return "Not Ok";
        //    }
        //}
        //public object GetRegionList()
        //{
        //    try
        //    {
        //        var qry = " SELECT REGION_ID LOCATION_ID,REGION_CODE LOCATION_CODE, REGION_NAME LOCATION_NAME, 'Region' LOCATION_TYPE" +
        //                  " FROM REGION_INFO" +
        //                  " WHERE REGION_STATUS='Active'" +
        //                  " AND REGION_ID NOT IN (SELECT NVL(LOCATION_ID,0) FROM SC_USER_LOGIN WHERE ACCESS_LOCATION='Region' AND STATUS='Active')" +
        //                  " ORDER BY REGION_NAME";
        //        DataTable dt = _dbHelper.GetDataTable(qry);
        //        var item = (from DataRow row in dt.Rows
        //                    select new UserInfoBEL
        //                    {
        //                        LocationId = Convert.ToInt32(row["LOCATION_ID"]),
        //                        LocationCode = row["LOCATION_CODE"].ToString(),
        //                        LocationName = row["LOCATION_NAME"].ToString(),
        //                        LocationType = row["LOCATION_TYPE"].ToString()
        //                    }).ToList();
        //        return item;

        //    }
        //    catch (Exception e)
        //    {
        //        var lineNum = e.StackTrace.Substring(e.StackTrace.LastIndexOf(' '));
        //        _errorLogger.GetErrorMessage(e.Message, "UserInfoDAL", lineNum);
        //        ExceptionReturn = e.Message;
        //        return "Not Ok";
        //    }
        //}
        //public object GetAreaList()
        //{
        //    try
        //    {
        //        var qry = " SELECT AREA_ID LOCATION_ID,AREA_CODE LOCATION_CODE, AREA_NAME LOCATION_NAME, 'Area' LOCATION_TYPE" +
        //                  " FROM AREA_INFO" +
        //                  " WHERE AREA_STATUS='Active'" +
        //                  " AND AREA_ID NOT IN (SELECT NVL(LOCATION_ID,0) FROM SC_USER_LOGIN WHERE ACCESS_LOCATION='Area' AND STATUS='Active')" +
        //                  " ORDER BY AREA_NAME";
        //        DataTable dt = _dbHelper.GetDataTable(qry);
        //        var item = (from DataRow row in dt.Rows
        //                    select new UserInfoBEL
        //                    {
        //                        LocationId = Convert.ToInt32(row["LOCATION_ID"]),
        //                        LocationCode = row["LOCATION_CODE"].ToString(),
        //                        LocationName = row["LOCATION_NAME"].ToString(),
        //                        LocationType = row["LOCATION_TYPE"].ToString()
        //                    }).ToList();
        //        return item;

        //    }
        //    catch (Exception e)
        //    {
        //        var lineNum = e.StackTrace.Substring(e.StackTrace.LastIndexOf(' '));
        //        _errorLogger.GetErrorMessage(e.Message, "UserInfoDAL", lineNum);
        //        ExceptionReturn = e.Message;
        //        return "Not Ok";
        //    }
        //}
        //public object GetTerritoryList()
        //{
        //    try
        //    {
        //        var qry = " SELECT TERRITORY_ID LOCATION_ID,TERRITORY_CODE LOCATION_CODE, TERRITORY_NAME LOCATION_NAME, 'Territory' LOCATION_TYPE" +
        //                  " FROM TERRITORY_INFO" +
        //                  " WHERE TERRITORY_STATUS='Active'" +
        //                  " AND TERRITORY_ID NOT IN (SELECT NVL(LOCATION_ID,0) FROM SC_USER_LOGIN WHERE ACCESS_LOCATION='Territory' AND STATUS='Active')" +
        //                  " ORDER BY TERRITORY_NAME";
        //        DataTable dt = _dbHelper.GetDataTable(qry);
        //        var item = (from DataRow row in dt.Rows
        //                    select new UserInfoBEL
        //                    {
        //                        LocationId = Convert.ToInt32(row["LOCATION_ID"]),
        //                        LocationCode = row["LOCATION_CODE"].ToString(),
        //                        LocationName = row["LOCATION_NAME"].ToString(),
        //                        LocationType = row["LOCATION_TYPE"].ToString()
        //                    }).ToList();
        //        return item;

        //    }
        //    catch (Exception e)
        //    {
        //        var lineNum = e.StackTrace.Substring(e.StackTrace.LastIndexOf(' '));
        //        _errorLogger.GetErrorMessage(e.Message, "UserInfoDAL", lineNum);
        //        ExceptionReturn = e.Message;
        //        return "Not Ok";
        //    }
        //}

    }
}