using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SalesWeb.Areas.SpaMisReport.Models.BEL
{
    public class DistReplaceStkConsumptionBEL
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
        public string CustomerCode { get; set; }
        public string CustomerName { get; set; }
        public string Address { get; set; }
        public string ProductCode { get; set; }
        public string ProductName { get; set; }
        public string PackSize { get; set; }
        public string ProductPrice { get; set; }
        public int OpeningReplaceQty { get; set; }
        public int ReplaceRecvRetQty { get; set; }
        public int TotalQty { get; set; }
        public int ReplaceReturnQty { get; set; }
        public int ReplaceFactoryQty { get; set; }
        public int TotalDeductQty { get; set; }
        public int ClosingReplaceQty { get; set; }

   

    }
}