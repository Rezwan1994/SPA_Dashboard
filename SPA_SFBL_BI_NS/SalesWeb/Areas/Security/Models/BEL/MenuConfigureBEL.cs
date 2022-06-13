using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SalesWeb.Areas.Security.Models.BEL
{
    public class MenuConfigureBEL
    {
        public int Id{ get; set; }
        public int ParentMenuId{ get; set; }
        public int ParentSeq{ get; set; }
        public int ChildMenuId{ get; set; }
        public int ChildSeq{ get; set; }
        public string Url{ get; set; }
    }

    public class MenuConfigureMapBEL
    {
        public int Id { get; set; }
        public int ParentMenuId { get; set; }
        public string ParentMenuName { get; set; }
        public int ParentSeq { get; set; }
        public int ChildMenuId { get; set; }
        public string ChildMenuName { get; set; }
        public int ChildSeq { get; set; }
        public string Url { get; set; }
        public string DisplayName { get; set; }
    }

}