using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SalesWeb.Areas.Security.Models.BEL
{
    public class RoleInfo
    {
        public int RoleId { get; set; }
        public string RoleName { get; set; }

        public string Status { get; set; }
    }
}