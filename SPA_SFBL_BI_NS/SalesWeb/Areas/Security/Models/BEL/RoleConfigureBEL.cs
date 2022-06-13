using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SalesWeb.Areas.Security.Models.BEL
{
    public class RoleMenuConfigureBEL
    {
        public int Id{ get; set; }
        public int McId{ get; set; }
        public int ParentMenuId { get; set; }
        public string ParentMenuName { get; set; }
        public int? ParentSeq { get; set; }
        public int ChildMenuId { get; set; }
        public string ChildMenuName { get; set; }
        public int ChildSeq { get; set; }
        public int RoleId{ get; set; }
        public string RoleName{ get; set; }
        public string SaveStatus { get; set; }
        public string ViewStatus { get; set; }
        public string DeleteStatus { get; set; }
        public int SlNo { get; set; }
    }

    public class RoleUserConfigureBEL
    {
        public int SlNo { get; set; }
        public int Id { get; set; }
        public int UserId { get; set; }
        public string EmployeeCode { get; set; }
        public string EmployeeName { get; set; }
        public int RoleId { get; set; }
        public string RoleName { get; set; }

    }

}