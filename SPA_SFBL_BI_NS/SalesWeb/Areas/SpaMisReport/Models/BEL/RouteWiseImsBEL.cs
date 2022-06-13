using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SalesWeb.Areas.SpaMisReport.Models.BEL
{
    public class RouteWiseImsBEL
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
        public string MarketCode { get; set; }
        public string MarketName { get; set; }
        public string CustomerCode { get; set; }
        public string CustomerName { get; set; }
        public string DbLocation { get; set; }
        public string RouteCode { get; set; }
        public string RouteName { get; set; }
        public int NoOfInv { get; set; }
        public double TotalInvAmt { get; set; }
        public double SlabAdjustment { get; set; }
        public double NetInvAmount { get; set; }
        public double ReturnValue { get; set; }
        public double ReturnSlabAdjust { get; set; }
        public double NetReturnVal { get; set; }
        public double NetIms { get; set; }
        public int NoOfReplaceInv { get; set; }
        public double ReplaceInvAmt { get; set; }



    }
}