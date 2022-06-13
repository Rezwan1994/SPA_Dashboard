using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SalesWeb.Areas.SpaMisReport.Models.BEL
{
    public class NationalProductSalesBEL
    {

        public int SlNo { get; set; }


        public string BaseProductCode { get; set; }
        public string BaseProductName { get; set; }
        public string CategoryCode { get; set; }
        public string CategoryName { get; set; }
        public string BrandCode { get; set; }
        public string BrandName { get; set; }


        public string ProductCode { get; set; }
        public string ProductName { get; set; }
        public string PackSize { get; set; }
        public double CurrentMonthSalesVal { get; set; }
        public double CurrentMonthReturnVal { get; set; }
        public double CurrentMonthNetSalesVal { get; set; }
        public string CurrentMonthSalesQty { get; set; }
        public string CurrentMonthReturnQty { get; set; }
        public string CurrentMonthNetSalesQty { get; set; }
        public double LastMonthSalesVal { get; set; }
        public double LastMonthReturnVal { get; set; }
        public double LastMonthNetSalesVal { get; set; }
        public string LastMonthSalesQty { get; set; }
        public string LastMonthReturnQty { get; set; }
        public string LastMonthNetSalesQty { get; set; }
        public string CurrentMonthTargetQty { get; set; }
        public double CurrentMonthTargetVal { get; set; }
        public string CurrentMonthAch { get; set; }
        public string CurrentMonthGrowth { get; set; }
        public double CurrentYearImsVal { get; set; }
        public double LastYearImsVal { get; set; }


    }
}