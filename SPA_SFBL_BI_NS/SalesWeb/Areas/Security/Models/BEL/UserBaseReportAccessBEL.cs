using SalesWeb.Universal.Common;

namespace SalesWeb.Areas.Security.Models.BEL
{
    public class UserBaseReportAccessBEL
    {



        public class UserMstBEL : EnterUpdate
        {

            public int MstId { get; set; }
            public int UserId { get; set; }
            public string TypeName { get; set; }
            

        }


        public class UserProductTypeBEL : EnterUpdate
        {

            public string ProductTypeId { get; set; }
            public int MstId { get; set; }
            public int UserId { get; set; }
            public string ProductTypeCode { get; set; }
            public string ProductTypeName { get; set; }
            public string TypeName { get; set; }          

        }
        public class UserProductBEL : EnterUpdate
        {
            public int ProductDtlId { get; set; }
            public int ProductTypeId { get; set; }
            public int UserId { get; set; }
            public string ProductCode { get; set; }
            public string ProductName { get; set; }
            public string PackSize { get; set; }
            public string ProductTypeCode { get; set; }
            public string ProductTypeName { get; set; }
            public string TypeName { get; set; }
        }

    }
}