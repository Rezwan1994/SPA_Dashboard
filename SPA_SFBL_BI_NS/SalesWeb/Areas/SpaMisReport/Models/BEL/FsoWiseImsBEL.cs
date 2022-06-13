using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SalesWeb.Areas.SpaMisReport.Models.BEL
{
    public class FsoWiseImsBEL
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
        public string FsoCode { get; set; }
        public string FsoName { get; set; }
        public string MarketCode { get; set; }
        public string MarketName { get; set; }
        public double NoOfInvoice { get; set; }
        public double InvoiceAmount { get; set; }
        public double SlabAdjustmentAmt { get; set; }
        public double NetInvoiceAmt { get; set; }
        public double ReturnAmt { get; set; }
        public double ReturnSlabAdjustmentAmt { get; set; }
        public double NetReturnAmt { get; set; }
        public double NetIms { get; set; }
    }
}