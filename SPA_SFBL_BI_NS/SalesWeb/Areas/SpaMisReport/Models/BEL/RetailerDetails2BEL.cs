using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SalesWeb.Areas.SpaMisReport.Models.BEL
{
    public class RetailerDetails2BEL
    {
        //public int SlNo { get; set; }
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
        public string RouteCode { get; set; }
        public string RouteName { get; set; }
        public string MarketRouteRelStatus { get; set; }
        public string RetailerCode { get; set; }
        public string RetailerName { get; set; }
        public string RetailerNameBn { get; set; }
        public string Address { get; set; }
        public string RetailerAddressBn { get; set; }
        public string RouteRetailerRelStatus { get; set; }
        public string RetailerStatus { get; set; }
        public string ContactPerson { get; set; }
        public string ContactNo { get; set; }
        public string RetailerType { get; set; }
        //public string RetailerCategoryCode { get; set; }
        public string RetailerCategoryDesc { get; set; }
        public string LocationType { get; set; }
        public string RetailerEntryDate { get; set; }
        //public string RecommendStatus { get; set; }
        //public string RecommendStatusDesc { get; set; }
        //public string RecommendBy { get; set; }
        //public string RecommendDate { get; set; }
        //public string ApprovedStatus { get; set; }
        public string ApprovedStatusDesc { get; set; }
        //public string ApprovedBy { get; set; }
        public string ApprovedDate { get; set; }
        public string MonthleAvgSales { get; set; }
        public string FirstInvoiceDate { get; set; }
        public string LastInvoiceDate { get; set; }
        //public string LastInvoiceDay { get; set; }
    }
}