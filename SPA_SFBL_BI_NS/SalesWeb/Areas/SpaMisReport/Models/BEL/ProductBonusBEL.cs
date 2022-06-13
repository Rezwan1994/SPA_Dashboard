using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SalesWeb.Areas.SpaMisReport.Models.BEL
{
	public class ProductBonusBEL
	{

        public int SlNo { get; set; }
        public string SalesProductCode { get; set; }
        public string SalesProductName { get; set; }
        public string SalesProductPackSize { get; set; }
        public string BonusProductCode { get; set; }
        public string BonusProductName { get; set; }
        public string BonusProductPackSize { get; set; }
        public string BonusSlabQty { get; set; }
        public string BonusPrdQty { get; set; }
        public string BonusPriceDisc { get; set; }
        public string PrdLocationType { get; set; }
        public string PrdLocationTypeName { get; set; }
        public string BonusLocationCode { get; set; }
        public string BonusLocationName { get; set; }
        public string SalesQty { get; set; }
        public string PriceDiscount { get; set; }
        public string PriceLocationType { get; set; }
        public string PriceLocationTypeName { get; set; }
        public string PriceLocationCode { get; set; }
        public string PriceLocationName { get; set; }




        public string EffectFormDate { get; set; }
        public string EffectToDate { get; set; }
        public string BonusStatus { get; set; }

        public string PriceStatus { get; set; }
    }
}