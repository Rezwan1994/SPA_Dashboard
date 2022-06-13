﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SalesWeb.Areas.SpaMisReport.Models.BEL
{
    public class ComboBonusBEL
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
        public string RouteCode { get; set; }
        public string RouteName { get; set; }
        public string RetailerCode { get; set; }
        public string RetailerName { get; set; }

        public string InvoiceDate { get; set; }
        public string InvoiceNo { get; set; }

        public string ComboBonusNo { get; set; }
        public string ComboBonusName { get; set; }

        public string ProductCode { get; set; }
        public string ProductName { get; set; }
        public string PackSize { get; set; }


        public string SalesQty { get; set; }
        public string ReturnSalesQty { get; set; }
        public string NetImsQty { get; set; }

        public string SalesValue { get; set; }
        public string ReturnSalesValue { get; set; }
        public string NetImsValue { get; set; }

        public string ComboBonusQty { get; set; }
        public string ReturnComboBonusQty { get; set; }

        public string NetComboBonusQty { get; set; }

    }
}