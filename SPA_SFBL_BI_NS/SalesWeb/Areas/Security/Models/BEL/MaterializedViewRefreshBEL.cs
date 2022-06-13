using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SalesWeb.Areas.Security.Models.BEL
{
    public class MaterializedViewRefreshBEL
    {
        public string MaterializedViewName { get; set; }

        public string RunDate { get; set; }

        public string RunDuration { get; set; }
        
        public string RefreshStatus { get; set; }


    }
}