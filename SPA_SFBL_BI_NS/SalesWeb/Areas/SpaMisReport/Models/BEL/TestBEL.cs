using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SalesWeb.Areas.SpaMisReport.Models.BEL
{
    public class TestBEL
    {

        public string ProductCode { get; set; }
        public string ProductName { get; set; }
        public string PackSize { get; set; }

        public class OrderMstBEL
        {
            public string MstId { get; set; }
            public string CustomerCode { get; set; }
            public string CustomerName { get; set; }
            public string OrderStatus { get; set; }
        }
        public class OrderDtlBEL
        {
            public string DtlId { get; set; }
            public string MstId { get; set; }
            public string ProductCode { get; set; }
            public string ProductName { get; set; }
            public string PackSize { get; set; }
            public string OrderQty { get; set; }
        }




    }



}