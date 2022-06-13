using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SalesWeb.Areas.SpaMisTransaction.Models.BEL
{
    public class DistBonusProcessBEL
    {
        public string ProcessSlno { get; set; }
        public string ProcessNo { get; set; }
        public string ProcessDate { get; set; }
        public string BonusStartDate { get; set; }
        public string BonusEndDate { get; set; }        
        public string ApprovedStatus { get; set; }
        public string ApprovedDate { get; set; }
        public string ProcessRunStatus { get; set; }
        public string ProcessRunDate { get; set; }

        public string ProcessFinalizeStatus { get; set; }
        public string ProcessFinalizeDate { get; set; }

    }
}