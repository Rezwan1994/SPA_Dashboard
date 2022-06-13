using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SalesWeb.Areas.Security.Models.BEL
{
    public class ChangPassBEL
    {
        public int  UserId { get; set; }
        public string Username{ get; set; }
        public string Password { get; set; }

    }
}