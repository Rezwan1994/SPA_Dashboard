using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SalesWeb.Areas.SpaMisReport.Models.BEL
{
    public class DistSalesSummaryBEL
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

        public string DbLocation { get; set; }
        public string CustomerName { get; set; }
        public string ProductCode { get; set; }
        public string ProductName { get; set; }
        public string PackSize { get; set; }
        public double ProductPrice { get; set; }
        public int InvoiceQty { get; set; }
        public int InvBonusQty { get; set; }
        public double BonusPriceDiscount { get; set; }
        public int ImsSalesQty { get; set; }
        public int ImsBnsQty { get; set; }
        public double InvoiceAmt { get; set; }
        public int ReturnSalesQty { get; set; }
        public int ReturnBnsQty { get; set; }
        public double BnsDiscRet { get; set; }
        public double ReturnValue { get; set; }
        public double ImsSalesVal { get; set; }
        public double ImsBnsVal { get; set; }
        public double NetIms { get; set; }

        public int LastYearAsOnDateImsQty { get; set; }
        public double LastYearAsOnDateImsVal { get; set; }
    }
}