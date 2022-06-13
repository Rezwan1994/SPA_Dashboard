using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SalesWeb.Areas.SpaMisTransaction.Models.BEL
{
    public class DistBonusDiscAdjClaimBEL
    {
        public string ProcessSlno { get; set; }
        public string ProcessNo { get; set; }
        public string ProcessDate { get; set; }
        public string BonusDiscStartDate { get; set; }
        public string BonusDiscEndDate { get; set; }
        public string ApprovedStatus { get; set; }
        public string ApprovedDate { get; set; }
        public string ProcessRunStatus { get; set; }
        public string ProcessRunDate { get; set; }
        public string ClaimNo { get; set; }
        public string ClaimDate { get; set; }
        public string CustomerCode { get; set; }
        public string CustomerName { get; set; }
        public string FactoryCode { get; set; }
        public string FactoryName { get; set; }
        public string BonusDiscAmt { get; set; }
    }
}