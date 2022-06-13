using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SalesWeb.Areas.SpaMisReport.Models.BEL
{
    public class FsoWiseFirstAndLastInvoiceBEL
    {

       
        public string CustomerCode { get; set; }
        public string CustomerName { get; set; }
        public string DbLocation { get; set; }
        public string MarketCode { get; set; }
        public string MarketName { get; set; }
        public string FsoCode { get; set; }
        public string FsoName { get; set; }
        public string JoiningDate { get; set; }
        public string MobileNo { get; set; }
        public string FirstInvoiceNo { get; set; }
        public string FirstInvoiceDate { get; set; }
        public string LastInvoiceNo { get; set; }
        public string LastInvoiceDate { get; set; }
        public string Status { get; set; }

    }
}