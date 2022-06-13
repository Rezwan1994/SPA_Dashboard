using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SalesWeb.Areas.SpaMisReport.Models.BEL
{
    public class TradeProgramParticipationBEL
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
        public string TradeProgramNo { get; set; }
        public string ProgramName { get; set; }
        public string EffectType { get; set; }
        public string TradePolicyNo { get; set; }
        public double SlabTargetVal { get; set; }
        public double SlabUpperAmt { get; set; }
        public int NoOfInv { get; set; }
        public string Gift { get; set; }
        public double DiscountAmt { get; set; }
        public double DiscountPercentage { get; set; }
        public string EntryDate { get; set; }
        public string ProgramTypeCode { get; set; }
        public string ProgramType { get; set; }
        public string EffectFromDate { get; set; }
        public string EffectToDate { get; set; }
        public double SalesValue { get; set; }
        public double ReturnValue { get; set; }
        public double ImsValue { get; set; }


    }
}