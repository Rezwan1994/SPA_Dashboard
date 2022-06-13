using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SalesWeb.Areas.Security.Models.BEL
{
    public class ReportViewTrackingBEL
    {
        public int SlNo { get; set; }
        public string UserId { get; set; }

        public string UserName { get; set; }

        public string EmployeeId { get; set; }

        public string EmployeeName { get; set; }

        public string ReportName { get; set; }

        public string ReportViewDate { get; set; }

        public string ReportViewTerminal { get; set; }

        public string ReportViewIp { get; set; }
    }
}