using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SalesWeb.Areas.SpaMisReport.Models.BEL
{
    public class DistWiseBonusAdjustmentBEL
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
        public string DBLocation { get; set; }
        public string DistributorCode { get; set; }
        public string DistributorName { get; set; }
        public string ProductCode { get; set; }
        public string ProductName { get; set; }
        public string PackSize { get; set; }
        public int BonusQty { get; set; }
        public double BonusVal { get; set; }
        public int RetBnsQty { get; set; }
        public double RetBnsVal { get; set; }
        public int ActualBnsQty { get; set; }
        public double ActualBnsVal { get; set; }
        public double BonusPriceDiscount { get; set; }


    }
}