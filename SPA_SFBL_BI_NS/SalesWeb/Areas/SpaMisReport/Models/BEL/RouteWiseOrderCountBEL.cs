using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SalesWeb.Areas.SpaMisReport.Models.BEL
{
    public class RouteWiseOrderCountBEL
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
        public string DbLocation { get; set; }
        public string MarketCode { get; set; }
        public string MarketName { get; set; }
        public string RouteCode { get; set; }
        public string RouteName { get; set; }

        public string TotalRouteRetailer { get; set; }

        public string NoOfRouteVisit { get; set; }

        public string TotalVisitRetailer { get; set; }

        public string NoOfNormalOrder { get; set; }

        public string NoOfReplaceOrder { get; set; }

        public string NoOfOrderingRetailer { get; set; }

        public string NoOfOrderingSku { get; set; }

        public string OrderValue { get; set; }

        public string ProductivityCall { get; set; }

        public string Lpc { get; set; }


    }
}