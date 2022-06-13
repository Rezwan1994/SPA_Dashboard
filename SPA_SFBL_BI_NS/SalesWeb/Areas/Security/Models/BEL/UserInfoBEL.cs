using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SalesWeb.Areas.Security.Models.BEL
{
    public class UserInfoBEL
    {
        public int UserId { get; set; }
        public string UserName { get; set; }

        public string Password { get; set; }

        public int EmployeeId { get; set; }

        public string EmployeeName { get; set; }
        public string EmployeeCode { get; set; }

        public string AccessLocation { get; set; }

        public int LocationId { get; set; }

        public string LocationCode { get; set; }

        public string LocationName { get; set; }

        public string LocationType { get; set; }

        public string Status { get; set; }




    }
}


