using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SalesWeb.Universal.Common
{
    public class CommonBEL
    {
    }
    public class EnterUpdate
    {
        public int EnteredBy { get; set; }
        public string EnteredDate { get; set; }
        public string EnteredTerminal { get; set; }
        public int UpdatedBy { get; set; }
        public string UpdatedDate { get; set; }
        public string UpdatedTerminal { get; set; }
    }

}