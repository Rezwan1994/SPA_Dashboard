using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SalesWeb.Areas.Security.Models.BEL
{
    public class UserBEL
    {
        public int MstId { get; set; }
        public int UserId { get; set; }
        public string  UserName { get; set; }
        public int EmployeeId { get; set; }
        public string EmployeeCode { get; set; }
        public string EmployeeName { get; set; }


        public string Code { get; set; }
        public string Name { get; set; }

        public int TypeId { get; set; }
        public string TypeName { get; set; }

    }

}