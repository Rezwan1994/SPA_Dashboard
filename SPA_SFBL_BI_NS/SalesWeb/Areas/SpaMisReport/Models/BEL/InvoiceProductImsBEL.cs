using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SalesWeb.Areas.SpaMisReport.Models.BEL
{
    public class InvoiceProductImsBEL
    {
        public string DivisionCode { get; set; }
        public string DivisionName { get; set; }
        public string RegionCode { get; set; }
        public string rCode { get; set; }
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
        public string RetailerCode { get; set; }
        public string RetailerName { get; set; }
        public string BaseProductCode { get; set; }
        public string BaseProductName { get; set; }
        public string BrandCode { get; set; }
        public string BrandName { get; set; }
        public string ProductCategoryCode { get; set; }
        public string ProductCategoryName { get; set; }
        public string ProductCode { get; set; }
        public string InvoiceNo { get; set; }
        public string InvoiceDate { get; set; }
        public string ProductName { get; set; }
        public string PackSize { get; set; }
        public int ProductPrice { get; set; }
        public double InvoiceAmt { get; set; }
        public int SalesQty { get; set; }
        public int SalesBonusQty { get; set; }
        public double BonusPriceDiscount { get; set; }
        public int ReplaceQty { get; set; }
        public int ReturnSalesQty { get; set; }
        public int ReturnBnsQty { get; set; }
        public int ImsSalesQty { get; set; }
        public int ImsBnsQty { get; set; }
        public double ReturnValue { get; set; }
        public double BnsDiscRet { get; set; }
        public double DiscountVal { get; set; }
        public double NetIms { get; set; }
        public int TargetQty { get; set; }
    }
}