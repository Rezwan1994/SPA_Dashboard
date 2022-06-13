using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SalesWeb.Areas.Security.Models.BEL
{
    public class ReportInfoBEL
    {
        public long ReportId { get; set; }
        public string ReportName { get; set; }
        public string DisplayName { get; set; }
        public int FormId { get; set; }
        public string FormName { get; set; }
        public string FormUrl { get; set; }
    }
    public class ReportConfigureBEL
    {
        public int Id { get; set; }
        public int ReportId { get; set; }
        public int RoleId { get; set; }
        public string RoleName { get; set; }
        public string ReportName { get; set; }
    }
}