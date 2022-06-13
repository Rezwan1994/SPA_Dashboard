using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SalesWeb.Areas.SpaMisReport.Models.BEL
{
    public class SalesRegisterBEL
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
        public string DBLoaction { get; set; }
        public string MarketCode { get; set; }
        public string MarketName { get; set; }
        public string RouteCode { get; set; }
        public string RouteName { get; set; }

        public double OrderValue { get; set; }
        public double InvoiceValue { get; set; }
        public double ReturnValue { get; set; }
        public double NetIms { get; set; }
        public double NoOfOrderingOutlet { get; set; }
        public double NoOfRetailer { get; set; }

        public double NoOfOrderingSku { get; set; }

        public double ProductivityCall { get; set; }

        public double Lpc { get; set; }




    }
}