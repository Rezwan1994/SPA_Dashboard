using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SalesWeb.Areas.Security.Models.BEL
{
    public class MenuInfoBEL
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public string DisplayName { get; set; }

        public string MenuType { get; set; }

        public string Status { get; set; }
    }
}