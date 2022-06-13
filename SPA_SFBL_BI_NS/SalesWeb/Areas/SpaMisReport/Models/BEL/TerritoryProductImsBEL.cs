using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SalesWeb.Areas.SpaMisReport.Models.BEL
{
    public class TerritoryProductImsBEL
    {
        public string DivisionCode { get; set; }
        public string DivisionName { get; set; }

        public string AreaCode { get; set; }
        public string AreaName { get; set; }

        public string TerritoryCode { get; set; }
        public string TerritoryName { get; set; }

        public string ProductCode { get; set; }
        public string ProductName { get; set; }
        public string PackSize { get; set; }

        public int ImsSalesQty { get; set; }

        public int TargetQty { get; set; }

        public int LastYearAsOnDateSalesQty { get; set; }

        public double Achievement { get; set; }

        public double Growth { get; set; }
    }
}