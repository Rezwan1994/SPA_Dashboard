using SalesWeb.Universal.Common;

namespace SalesWeb.Areas.SpaMisTransaction.Models.BEL
{
    public class DistProductFactoryRelationBEL
    {

        public class DistProductFactoryRelationMstBEL : EnterUpdate
        {

            public string MstId { get; set; }
            public string CustomerCode { get; set; }
            public string CustomerName { get; set; }


        }


        public class DistProductFactoryRelationDtlBEL : EnterUpdate
        {
            public string DtlId { get; set; }
            public string MstId { get; set; }
            public string CustomerCode { get; set; }
            public string CustomerName { get; set; }
            public string ProductCode { get; set; }
            public string ProductName { get; set; }
            public string PackSize { get; set; }
            public string FactoryCode { get; set; }
            public string FactoryName { get; set; }

        }

    }
}