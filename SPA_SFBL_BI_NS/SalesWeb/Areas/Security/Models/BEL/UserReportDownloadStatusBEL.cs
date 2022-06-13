using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SalesWeb.Areas.Security.Models.BEL
{
    public class UserReportDownloadStatusBEL
    {
        public string UserId { get; set; }
        public string MenuId { get; set; }
        public string ReportName { get; set; }

        public string ReportDisplayName { get; set; }
        public string DownloadStatus { get; set; }
    }
}