using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SalesWeb.Areas.SpaMisReport.Models.BEL
{
    public class ComboBonusDecBEL
    {
        public string ComboBonusNo { get; set; }
        public string ComboBonusName { get; set; }
        public string EffectFromDate { get; set; }
        public string EffectToDate { get; set; }
        public string LocationType { get; set; }
        public string LocationCode { get; set; }
        public string LocationName { get; set; }
        public string LocationStatus { get; set; }
        public string ProductType { get; set; }
        public string ProductTypeCode { get; set; }
        public string ProductTypeName { get; set; }
        public string BonusType { get; set; }
        public string ProductCode { get; set; }
        public string ProductName { get; set; }
        public string PackSize { get; set; }
        public string BonusProductCode { get; set; }
        public string BonusProductName { get; set; }
        public string BonusPackSize { get; set; }
        public string PriorityNo { get; set; }
        public string SlabQty { get; set; }
        public string BonusQty { get; set; }
        public string BonusDiscount { get; set; }
        public string BonusStatus { get; set; }        
    }
}