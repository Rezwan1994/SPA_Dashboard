using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SalesWeb.Areas.SpaMisReport.Models.BEL
{
    public class FsoTimeKeepingBEL
    {
        public string DivisionCode { get; set; }
        public string DivisionName { get; set; }
        public string RegionCode { get; set; }
        public string RegionName { get; set; }
        public string AreaCode { get; set; }
        public string AreaName { get; set; }
        public string TerritoryCode { get; set; }
        public string TerritoryName { get; set; }
        public string CustomerCode { get; set; }
        public string CustomerName { get; set; }
        public string DbLocation { get; set; }
        public string MarketCode { get; set; }
        public string MarketName { get; set; }
        public string EmployeeCode { get; set; }
        public string EmployeeName { get; set; }
        public string RouteCode { get; set; }
        public string RouteName { get; set; }
        public string RetailerCode { get; set; }
        public string RetailerName { get; set; }

        public string OrderDate { get; set; }
        public string DeliveryDate { get; set; }
        public string OrderTime { get; set; }
        public string OrderNo { get; set; }
        public string OrderType { get; set; }
        public string InvoiceStatus { get; set; }
        public string NumberOfProduct { get; set; }
        public string OrderValue { get; set; }
        public string TotalRouteRetailer { get; set; }
        public string FirstOrder { get; set; }
        public string FirstOrderTime { get; set; }
        public string LastOrder { get; set; }
        public string LastOrderTime { get; set; }



    }
}