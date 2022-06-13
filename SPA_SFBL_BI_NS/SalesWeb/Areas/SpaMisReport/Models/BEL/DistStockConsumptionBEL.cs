using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SalesWeb.Areas.SpaMisReport.Models.BEL
{
    public class DistStockConsumptionBEL
    {
        public int SlNo { get; set; }
        public string DivisionCode { get; set; }
        public string DivisionName { get; set; }
        public string RegionCode { get; set; }
        public string RegionName { get; set; }
        public string AreaCode { get; set; }
        public string AreaName { get; set; }
        public string TerritoryCode { get; set; }
        public string TerritoryName { get; set; }
        public string DBLocation { get; set; }
        public string DistributorCode { get; set; }
        public string DistributorName { get; set; }
        public string DistributorAdd { get; set; }
        public string ProductCode { get; set; }
        public string ProductName { get; set; }
        public string PackSize { get; set; }
        public double ProductPrice { get; set; }
        public int OpeningQty { get; set; }
        public int ReceiveQty { get; set; }
        public int ReplaceRcvQty { get; set; }
        public int PreviousReturnReceiveQty { get; set; }
        public int ReturnReceiveQty { get; set; }
        public int PrevReplaceRetReceiveQty { get; set; }
        public int ReplaceRetReceiveQty { get; set; }
        public int GainQty { get; set; }
        public int TotalInQty { get; set; }
        public int IssuedQty { get; set; }
        public int ReplaceIssueQty { get; set; }
        public int DispatchQty { get; set; }
        public int BonusQty { get; set; }
        public int TradeBonusQty { get; set; }
        public int ComboBonusQty { get; set; }
        public int LossQty { get; set; }
        public int RequiReturnQty { get; set; }
        public int DamageStockTransferQty { get; set; }
        public int TotalOutQty { get; set; }
        public int ClosingQty { get; set; }
        public double ClosingValue { get; set; }
        public int TargetQty { get; set; }
        public double  TargetVal { get; set; }
    }
}