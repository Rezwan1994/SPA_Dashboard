using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SalesWeb.Areas.SpaMisReport.Models.BEL
{
    public class RetailerProductIms2BEL
    {
        public string RetailerCode { get; set; }
        public string RetailerName { get; set; }

        public string ProductCode { get; set; }
        public string ProductName { get; set; }
        public string PackSize { get; set; }

        public int ImsSalesQty { get; set; }
        public int ImsBnsQty { get; set; }

        public int NetIms { get; set; }



    }
}