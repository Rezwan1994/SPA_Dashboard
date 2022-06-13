using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SalesWeb.Areas.SpaMisReport.Models.BEL
{
    public class RetailerImsBEL
    {
        public int SlNo { get; set; }
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
        public string DBLoaction { get; set; }
        public string MarketCode { get; set; }
        public string MarketName { get; set; }
        public string RouteCode { get; set; }
        public string RouteName { get; set; }
        public string RetailerCode { get; set; }
        public string RetailerName { get; set; }



        public int NoOfInvoice { get; set; }
        public double TotalInvoiceAmount { get; set; }
        public double SlabAdjustment { get; set; }
        public double NetInvoiceAmount { get; set; }
        public double ReturnValue { get; set; }
        public double ReturnSlabAdjustment { get; set; }
        public double NetReturnValue { get; set; }
        public double NetIms { get; set; }
        public int NoOfReplaceInvoice { get; set; }
        public double ReplaceInvoiceAmount { get; set; }
      
    }
}