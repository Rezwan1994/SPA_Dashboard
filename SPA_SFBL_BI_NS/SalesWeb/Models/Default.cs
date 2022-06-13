using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SalesWeb.Areas.Security.Models.BEL
{

    public class MenuInfo
    {
        public int MId { get; set; }
        public string MName { get; set; }
        public string Url { get; set; }
        public string BgColor { get; set; }
    }

    public class MenuConf
    {
        public int ParentId { get; set; }
        public string ParentSeq { get; set; }
        public int ChildId { get; set; }
        public int RoleId { get; set; }
        public string ChildSeq { get; set; }
        public string Url { get; set; }

    }

    public class EventPermission
    {
        public string Sv { get; set; }
        public string Vw { get; set; }
        public string Dl { get; set; }
        public string DisplayName { get; set; }
        public string MenuName { get; set; }
    }
    public class VarFieldName
    {
        public string DepotVar { get; set; }
        public string RegionVar { get; set; }
        public string ZoneVar { get; set; }
        public string AreaVar { get; set; }
        public string TerritoryVar { get; set; }
        public string MarketVar { get; set; }
    }
    public class VarQuarterInfo
    {
        public string QuarterValue { get; set; }
        public string QuarterName { get; set; }
        public string QuarterFullName { get; set; }
       
    }
    public class VarDesigName
    {
        public string DepotDesigVar { get; set; }
        public string RegionDesigVar { get; set; }
        public string ZoneDesigVar { get; set; }
        public string AreaDesigVar { get; set; }
        public string TerritoryDesigVar { get; set; }
        public string MarketDesigVar { get; set; }
    }

    public class UserLogin
    {
        public int UserId { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public string LogId { get; set; }
        public string Status { get; set; }
        public string AccessLevel { get; set; }
        public string EmployeeCode { get; set; }
        public string EmployeeName { get; set; }
        public string GroupCode { get; set; }
        public string Code { get; set; }
        public int RoleId { get; set; }
        public string CompanyName { get; set; }
        public string CompanyAddress { get; set; }
        public int EmployeeID { get; internal set; }
        public string CompanyLogoUrl { get; internal set; }

        public int UserBaseReportFilter { get; set; }

        public string ReportDownLoadStatus { get; set; }

    }
}
