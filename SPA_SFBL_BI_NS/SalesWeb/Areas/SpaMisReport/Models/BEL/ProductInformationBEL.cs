using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SalesWeb.Areas.SpaMisReport.Models.BEL
{
    public class ProductInformationBEL
    {
        public int SlNo { get; set; }
        public string ProductCode { get; set; }
        public string ProductName { get; set; }
        public string ProductNameBn { get; set; }
        public string PackSize { get; set; }
        public string BaseProductCode { get; set; }
        public string BaseProductName { get; set; }
        public string BrandCode { get; set; }
        public string BrandName { get; set; }
        public string CategoryCode { get; set; }
        public string CategoryName { get; set; }
        public string BonusAllow { get; set; }
        public string DiscountAllow { get; set; }
        public string DiscountType { get; set; }
        public string DiscountVal { get; set; }
        public string ShipperQty { get; set; }
        public string Status { get; set; }
        public string CpFlag { get; set; }
        public string UnitTp { get; set; }
        public string UnitVat { get; set; }
        public string Mrp { get; set; }


        public string FirstInvoiceDate { get; set; }
        public string LastInvoiceDate { get; set; }


    }
}