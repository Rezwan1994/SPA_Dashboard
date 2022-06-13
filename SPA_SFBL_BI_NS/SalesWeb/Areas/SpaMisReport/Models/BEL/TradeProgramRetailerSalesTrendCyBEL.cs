using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SalesWeb.Areas.SpaMisReport.Models.BEL
{
    public class TradeProgramRetailerSalesTrendCyBEL
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

        public double Sales { get; set; }
        public double Return { get; set; }
        public double Ims { get; set; }

        public double JanIms { get; set; }
        public double FebIms { get; set; }
        public double MarIms { get; set; }
        public double AprIms { get; set; }
        public double MayIms { get; set; }
        public double JunIms { get; set; }
        public double JulIms { get; set; }
        public double AugIms { get; set; }
        public double SepIms { get; set; }
        public double OctIms { get; set; }
        public double NovIms { get; set; }
        public double DecIms { get; set; }

    }
}