using SalesWeb.Areas.Dashboard.Models.BEL;
using SalesWeb.Areas.Dashboard.Models.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SalesWeb.Areas.Dashboard.Controllers
{
    public class MasterDashboardController : Controller
    {
        // GET: Dashboard/MasterDashboard
        public ActionResult Index()
        {
            masterDashboardDal _masterDashboardDAL = new masterDashboardDal();
            int user = Convert.ToInt32(this.Session["USER_ID"]);
            DashboardModel model = new DashboardModel();
            string Year = DateTime.Now.Year.ToString() + DateTime.Now.ToString("MM");
            model.OrderModel = _masterDashboardDAL.GetOrderCount(user);
            if (model.OrderModel == null)
                model.OrderModel = new OrderModel();
            model.SalesModel = _masterDashboardDAL.GetSalesCount(user);
            model.MonthlySalesModel = _masterDashboardDAL.GetMonthlySalesCount(user);
            if (model.MonthlySalesModel == null)
                model.MonthlySalesModel = new MonthlySalesModel();
            model.YearlySalesModel = _masterDashboardDAL.GetYearlySalesCount(user);
            if (model.YearlySalesModel == null)
                model.YearlySalesModel = new YearlySalesModel();
            model.MonthlyTargetModel = _masterDashboardDAL.GetMonthlyTargetCount(user);
            if (model.MonthlyTargetModel == null)
                model.MonthlyTargetModel = new MonthlyTargetModel();
            model.YearlyTargetModel = _masterDashboardDAL.GetYearlyTargetCount(user);
            if (model.YearlyTargetModel == null)
                model.YearlyTargetModel = new YearlyTargetModel();
            if (model.SalesModel == null)
                model.SalesModel = new SalesModel();
            List<string> xAxisCategoryList = new List<string>();
            int month = Convert.ToInt32(DateTime.Now.ToString("MM"));
            string monthname = DateTime.Now.ToString("MMM");
            int year = Convert.ToInt32(DateTime.Now.ToString("yyyy"));
            ViewBag.Year = year;

            int num = DateTime.DaysInMonth(year, month);
            for (int index = 1; index <= num; ++index)
            {
                string str2 = index.ToString() + " " + monthname;
                xAxisCategoryList.Add(str2);
            }
            ViewBag.xAxisCategoryList = xAxisCategoryList;

            #region IMS
            try
            {
                List<DayWiseModel> imsValue = _masterDashboardDAL.GetIMSValue(user);
                List<double> doubleList1 = new List<double>();
                List<double> doubleList2 = new List<double>();
                if (imsValue != null)
                {
                    if (imsValue.Count > 0)
                    {
                        doubleList1.Add(imsValue[0].DAY_01);
                        doubleList1.Add(imsValue[0].DAY_02);
                        doubleList1.Add(imsValue[0].DAY_03);
                        doubleList1.Add(imsValue[0].DAY_04);
                        doubleList1.Add(imsValue[0].DAY_05);
                        doubleList1.Add(imsValue[0].DAY_06);
                        doubleList1.Add(imsValue[0].DAY_07);
                        doubleList1.Add(imsValue[0].DAY_08);
                        doubleList1.Add(imsValue[0].DAY_09);
                        doubleList1.Add(imsValue[0].DAY_10);
                        doubleList1.Add(imsValue[0].DAY_11);
                        doubleList1.Add(imsValue[0].DAY_12);
                        doubleList1.Add(imsValue[0].DAY_13);
                        doubleList1.Add(imsValue[0].DAY_14);
                        doubleList1.Add(imsValue[0].DAY_15);
                        doubleList1.Add(imsValue[0].DAY_16);
                        doubleList1.Add(imsValue[0].DAY_17);
                        doubleList1.Add(imsValue[0].DAY_18);
                        doubleList1.Add(imsValue[0].DAY_19);
                        doubleList1.Add(imsValue[0].DAY_20);
                        doubleList1.Add(imsValue[0].DAY_21);
                        doubleList1.Add(imsValue[0].DAY_22);
                        doubleList1.Add(imsValue[0].DAY_23);
                        doubleList1.Add(imsValue[0].DAY_24);
                        doubleList1.Add(imsValue[0].DAY_25);
                        doubleList1.Add(imsValue[0].DAY_26);
                        doubleList1.Add(imsValue[0].DAY_27);
                        doubleList1.Add(imsValue[0].DAY_28);
                        doubleList1.Add(imsValue[0].DAY_29);
                        doubleList1.Add(imsValue[0].DAY_30);
                        doubleList1.Add(imsValue[0].DAY_31);
                    }
                    if (imsValue.Count > 1)
                    {
                        doubleList2.Add(imsValue[1].DAY_01);
                        doubleList2.Add(imsValue[1].DAY_02);
                        doubleList2.Add(imsValue[1].DAY_03);
                        doubleList2.Add(imsValue[1].DAY_04);
                        doubleList2.Add(imsValue[1].DAY_05);
                        doubleList2.Add(imsValue[1].DAY_06);
                        doubleList2.Add(imsValue[1].DAY_07);
                        doubleList2.Add(imsValue[1].DAY_08);
                        doubleList2.Add(imsValue[1].DAY_09);
                        doubleList2.Add(imsValue[1].DAY_10);
                        doubleList2.Add(imsValue[1].DAY_11);
                        doubleList2.Add(imsValue[1].DAY_12);
                        doubleList2.Add(imsValue[1].DAY_13);
                        doubleList2.Add(imsValue[1].DAY_14);
                        doubleList2.Add(imsValue[1].DAY_15);
                        doubleList2.Add(imsValue[1].DAY_16);
                        doubleList2.Add(imsValue[1].DAY_17);
                        doubleList2.Add(imsValue[1].DAY_18);
                        doubleList2.Add(imsValue[1].DAY_19);
                        doubleList2.Add(imsValue[1].DAY_20);
                        doubleList2.Add(imsValue[1].DAY_21);
                        doubleList2.Add(imsValue[1].DAY_22);
                        doubleList2.Add(imsValue[1].DAY_23);
                        doubleList2.Add(imsValue[1].DAY_24);
                        doubleList2.Add(imsValue[1].DAY_25);
                        doubleList2.Add(imsValue[1].DAY_26);
                        doubleList2.Add(imsValue[1].DAY_27);
                        doubleList2.Add(imsValue[1].DAY_28);
                        doubleList2.Add(imsValue[1].DAY_29);
                        doubleList2.Add(imsValue[1].DAY_30);
                        doubleList2.Add(imsValue[1].DAY_31);
                    }
                }

                ViewBag.ImsArray1 = doubleList1;
                ViewBag.ImsArray2 = doubleList2;
            }
            catch(Exception ex)
            {
                ViewBag.Error = ex.Message;
            }
           

            #endregion
            #region Returnd IMS
            List<double> returnList = new List<double>();
            DayWiseModel imsReturnPct = _masterDashboardDAL.GetIMSReturnPct(user);
            if (imsReturnPct != null)
            {
                returnList.Add(imsReturnPct.DAY_01);
                returnList.Add(imsReturnPct.DAY_02);
                returnList.Add(imsReturnPct.DAY_03);
                returnList.Add(imsReturnPct.DAY_04);
                returnList.Add(imsReturnPct.DAY_05);
                returnList.Add(imsReturnPct.DAY_06);
                returnList.Add(imsReturnPct.DAY_07);
                returnList.Add(imsReturnPct.DAY_08);
                returnList.Add(imsReturnPct.DAY_09);
                returnList.Add(imsReturnPct.DAY_10);
                returnList.Add(imsReturnPct.DAY_11);
                returnList.Add(imsReturnPct.DAY_12);
                returnList.Add(imsReturnPct.DAY_13);
                returnList.Add(imsReturnPct.DAY_14);
                returnList.Add(imsReturnPct.DAY_15);
                returnList.Add(imsReturnPct.DAY_16);
                returnList.Add(imsReturnPct.DAY_17);
                returnList.Add(imsReturnPct.DAY_18);
                returnList.Add(imsReturnPct.DAY_19);
                returnList.Add(imsReturnPct.DAY_20);
                returnList.Add(imsReturnPct.DAY_21);
                returnList.Add(imsReturnPct.DAY_22);
                returnList.Add(imsReturnPct.DAY_23);
                returnList.Add(imsReturnPct.DAY_24);
                returnList.Add(imsReturnPct.DAY_25);
                returnList.Add(imsReturnPct.DAY_26);
                returnList.Add(imsReturnPct.DAY_27);
                returnList.Add(imsReturnPct.DAY_28);
                returnList.Add(imsReturnPct.DAY_29);
                returnList.Add(imsReturnPct.DAY_30);
                returnList.Add(imsReturnPct.DAY_31);
            }

            ViewBag.ReturnArray = returnList;


            #endregion
            #region PC Trend
            List<double> pcTrendList = new List<double>();
            DayWiseModel pcTrend = _masterDashboardDAL.GetPCTrend(user, Year);
            if (pcTrend != null)
            {
                pcTrendList.Add(pcTrend.DAY_01);
                pcTrendList.Add(pcTrend.DAY_02);
                pcTrendList.Add(pcTrend.DAY_03);
                pcTrendList.Add(pcTrend.DAY_04);
                pcTrendList.Add(pcTrend.DAY_05);
                pcTrendList.Add(pcTrend.DAY_06);
                pcTrendList.Add(pcTrend.DAY_07);
                pcTrendList.Add(pcTrend.DAY_08);
                pcTrendList.Add(pcTrend.DAY_09);
                pcTrendList.Add(pcTrend.DAY_10);
                pcTrendList.Add(pcTrend.DAY_11);
                pcTrendList.Add(pcTrend.DAY_12);
                pcTrendList.Add(pcTrend.DAY_13);
                pcTrendList.Add(pcTrend.DAY_14);
                pcTrendList.Add(pcTrend.DAY_15);
                pcTrendList.Add(pcTrend.DAY_16);
                pcTrendList.Add(pcTrend.DAY_17);
                pcTrendList.Add(pcTrend.DAY_18);
                pcTrendList.Add(pcTrend.DAY_19);
                pcTrendList.Add(pcTrend.DAY_20);
                pcTrendList.Add(pcTrend.DAY_21);
                pcTrendList.Add(pcTrend.DAY_22);
                pcTrendList.Add(pcTrend.DAY_23);
                pcTrendList.Add(pcTrend.DAY_24);
                pcTrendList.Add(pcTrend.DAY_25);
                pcTrendList.Add(pcTrend.DAY_26);
                pcTrendList.Add(pcTrend.DAY_27);
                pcTrendList.Add(pcTrend.DAY_28);
                pcTrendList.Add(pcTrend.DAY_29);
                pcTrendList.Add(pcTrend.DAY_30);
                pcTrendList.Add(pcTrend.DAY_31);
            }
            ViewBag.PcTrendArray = pcTrendList;
            #endregion

            #region Product Wise Sales
            List<SalesWiseModel> productWiseSales = _masterDashboardDAL.GetProductWiseSales();
            List<List<object>> product = new List<List<object>>();
            if (productWiseSales != null && productWiseSales.Count > 0)
            {
                foreach (SalesWiseModel salesWiseModel in productWiseSales)
                    product.Add(new List<object>((IEnumerable<object>)new object[2]
                    {
            (object) salesWiseModel.PRODUCT_NAME,
            (object) salesWiseModel.PCT_OF_TOTAL_IMS_VALUE
                    }));
            }
            ViewBag.productSalesResults = product;
            #endregion

            #region Brand Wise Sales
            List<SalesWiseModel> brandWiseSales = _masterDashboardDAL.GetBrandWiseSales();
            List<List<object>> brand = new List<List<object>>();
            if (brandWiseSales != null && brandWiseSales.Count > 0)
            {
                foreach (SalesWiseModel salesWiseModel in brandWiseSales)
                    brand.Add(new List<object>((IEnumerable<object>)new object[2]
                    {
            (object) salesWiseModel.BRAND_NAME,
            (object) salesWiseModel.IMS_VALUE_CORE
                    }));
            }
            ViewBag.brandSalesResults = brand;
            #endregion

            #region Category Wise Sales
            List<SalesWiseModel> categoryWiseSales = _masterDashboardDAL.GetCategoryWiseSales();
            List<List<object>> category = new List<List<object>>();
            if (categoryWiseSales != null && categoryWiseSales.Count > 0)
            {
                foreach (SalesWiseModel salesWiseModel in categoryWiseSales)
                    category.Add(new List<object>((IEnumerable<object>)new object[2]
                    {
            (object) salesWiseModel.CATEGORY_NAME,
            (object) salesWiseModel.PCT_OF_TOTAL_IMS_VALUE
                    }));
            }
            ViewBag.categorySalesResults = category;
            #endregion

            #region StockList
            List<double> transitStock = new List<double>();
            List<double> physicalStock = new List<double>();
            List<DayWiseModel> transitStockList = _masterDashboardDAL.GetPhysicalAndTransitStockList(user, Year);
            if (transitStockList != null)
            {
                if (transitStockList.Count > 0)
                {
                    transitStock.Add(transitStockList[0].DAY_01);
                    transitStock.Add(transitStockList[0].DAY_02);
                    transitStock.Add(transitStockList[0].DAY_03);
                    transitStock.Add(transitStockList[0].DAY_04);
                    transitStock.Add(transitStockList[0].DAY_05);
                    transitStock.Add(transitStockList[0].DAY_06);
                    transitStock.Add(transitStockList[0].DAY_07);
                    transitStock.Add(transitStockList[0].DAY_08);
                    transitStock.Add(transitStockList[0].DAY_09);
                    transitStock.Add(transitStockList[0].DAY_10);
                    transitStock.Add(transitStockList[0].DAY_11);
                    transitStock.Add(transitStockList[0].DAY_12);
                    transitStock.Add(transitStockList[0].DAY_13);
                    transitStock.Add(transitStockList[0].DAY_14);
                    transitStock.Add(transitStockList[0].DAY_15);
                    transitStock.Add(transitStockList[0].DAY_16);
                    transitStock.Add(transitStockList[0].DAY_17);
                    transitStock.Add(transitStockList[0].DAY_18);
                    transitStock.Add(transitStockList[0].DAY_19);
                    transitStock.Add(transitStockList[0].DAY_20);
                    transitStock.Add(transitStockList[0].DAY_21);
                    transitStock.Add(transitStockList[0].DAY_22);
                    transitStock.Add(transitStockList[0].DAY_23);
                    transitStock.Add(transitStockList[0].DAY_24);
                    transitStock.Add(transitStockList[0].DAY_25);
                    transitStock.Add(transitStockList[0].DAY_26);
                    transitStock.Add(transitStockList[0].DAY_27);
                    transitStock.Add(transitStockList[0].DAY_28);
                    transitStock.Add(transitStockList[0].DAY_29);
                    transitStock.Add(transitStockList[0].DAY_30);
                    transitStock.Add(transitStockList[0].DAY_31);
                }
                if (transitStockList.Count > 1)
                {
                    physicalStock.Add(transitStockList[1].DAY_01);
                    physicalStock.Add(transitStockList[1].DAY_02);
                    physicalStock.Add(transitStockList[1].DAY_03);
                    physicalStock.Add(transitStockList[1].DAY_04);
                    physicalStock.Add(transitStockList[1].DAY_05);
                    physicalStock.Add(transitStockList[1].DAY_06);
                    physicalStock.Add(transitStockList[1].DAY_07);
                    physicalStock.Add(transitStockList[1].DAY_08);
                    physicalStock.Add(transitStockList[1].DAY_09);
                    physicalStock.Add(transitStockList[1].DAY_10);
                    physicalStock.Add(transitStockList[1].DAY_11);
                    physicalStock.Add(transitStockList[1].DAY_12);
                    physicalStock.Add(transitStockList[1].DAY_13);
                    physicalStock.Add(transitStockList[1].DAY_14);
                    physicalStock.Add(transitStockList[1].DAY_15);
                    physicalStock.Add(transitStockList[1].DAY_16);
                    physicalStock.Add(transitStockList[1].DAY_17);
                    physicalStock.Add(transitStockList[1].DAY_18);
                    physicalStock.Add(transitStockList[1].DAY_19);
                    physicalStock.Add(transitStockList[1].DAY_20);
                    physicalStock.Add(transitStockList[1].DAY_21);
                    physicalStock.Add(transitStockList[1].DAY_22);
                    physicalStock.Add(transitStockList[1].DAY_23);
                    physicalStock.Add(transitStockList[1].DAY_24);
                    physicalStock.Add(transitStockList[1].DAY_25);
                    physicalStock.Add(transitStockList[1].DAY_26);
                    physicalStock.Add(transitStockList[1].DAY_27);
                    physicalStock.Add(transitStockList[1].DAY_28);
                    physicalStock.Add(transitStockList[1].DAY_29);
                    physicalStock.Add(transitStockList[1].DAY_30);
                    physicalStock.Add(transitStockList[1].DAY_31);
                }
            }
            ViewBag.physicalStrockArray = physicalStock;
            ViewBag.pipeLineStrockArray = transitStock;
            #endregion

            #region Ims Volume
            List<double> currentYearVolume = new List<double>();
            List<double> lastYearVolume = new List<double>();
            List<DayWiseModel> imsVolumeList = _masterDashboardDAL.GetIMSVolumeList(user);
            if (imsVolumeList != null)
            {
                if (imsVolumeList.Count > 0)
                {
                    currentYearVolume.Add(imsVolumeList[0].DAY_01);
                    currentYearVolume.Add(imsVolumeList[0].DAY_02);
                    currentYearVolume.Add(imsVolumeList[0].DAY_03);
                    currentYearVolume.Add(imsVolumeList[0].DAY_04);
                    currentYearVolume.Add(imsVolumeList[0].DAY_05);
                    currentYearVolume.Add(imsVolumeList[0].DAY_06);
                    currentYearVolume.Add(imsVolumeList[0].DAY_07);
                    currentYearVolume.Add(imsVolumeList[0].DAY_08);
                    currentYearVolume.Add(imsVolumeList[0].DAY_09);
                    currentYearVolume.Add(imsVolumeList[0].DAY_10);
                    currentYearVolume.Add(imsVolumeList[0].DAY_11);
                    currentYearVolume.Add(imsVolumeList[0].DAY_12);
                    currentYearVolume.Add(imsVolumeList[0].DAY_13);
                    currentYearVolume.Add(imsVolumeList[0].DAY_14);
                    currentYearVolume.Add(imsVolumeList[0].DAY_15);
                    currentYearVolume.Add(imsVolumeList[0].DAY_16);
                    currentYearVolume.Add(imsVolumeList[0].DAY_17);
                    currentYearVolume.Add(imsVolumeList[0].DAY_18);
                    currentYearVolume.Add(imsVolumeList[0].DAY_19);
                    currentYearVolume.Add(imsVolumeList[0].DAY_20);
                    currentYearVolume.Add(imsVolumeList[0].DAY_21);
                    currentYearVolume.Add(imsVolumeList[0].DAY_22);
                    currentYearVolume.Add(imsVolumeList[0].DAY_23);
                    currentYearVolume.Add(imsVolumeList[0].DAY_24);
                    currentYearVolume.Add(imsVolumeList[0].DAY_25);
                    currentYearVolume.Add(imsVolumeList[0].DAY_26);
                    currentYearVolume.Add(imsVolumeList[0].DAY_27);
                    currentYearVolume.Add(imsVolumeList[0].DAY_28);
                    currentYearVolume.Add(imsVolumeList[0].DAY_29);
                    currentYearVolume.Add(imsVolumeList[0].DAY_30);
                    currentYearVolume.Add(imsVolumeList[0].DAY_31);
                }
                if (imsVolumeList.Count > 1)
                {
                    lastYearVolume.Add(imsVolumeList[1].DAY_01);
                    lastYearVolume.Add(imsVolumeList[1].DAY_02);
                    lastYearVolume.Add(imsVolumeList[1].DAY_03);
                    lastYearVolume.Add(imsVolumeList[1].DAY_04);
                    lastYearVolume.Add(imsVolumeList[1].DAY_05);
                    lastYearVolume.Add(imsVolumeList[1].DAY_06);
                    lastYearVolume.Add(imsVolumeList[1].DAY_07);
                    lastYearVolume.Add(imsVolumeList[1].DAY_08);
                    lastYearVolume.Add(imsVolumeList[1].DAY_09);
                    lastYearVolume.Add(imsVolumeList[1].DAY_10);
                    lastYearVolume.Add(imsVolumeList[1].DAY_11);
                    lastYearVolume.Add(imsVolumeList[1].DAY_12);
                    lastYearVolume.Add(imsVolumeList[1].DAY_13);
                    lastYearVolume.Add(imsVolumeList[1].DAY_14);
                    lastYearVolume.Add(imsVolumeList[1].DAY_15);
                    lastYearVolume.Add(imsVolumeList[1].DAY_16);
                    lastYearVolume.Add(imsVolumeList[1].DAY_17);
                    lastYearVolume.Add(imsVolumeList[1].DAY_18);
                    lastYearVolume.Add(imsVolumeList[1].DAY_19);
                    lastYearVolume.Add(imsVolumeList[1].DAY_20);
                    lastYearVolume.Add(imsVolumeList[1].DAY_21);
                    lastYearVolume.Add(imsVolumeList[1].DAY_22);
                    lastYearVolume.Add(imsVolumeList[1].DAY_23);
                    lastYearVolume.Add(imsVolumeList[1].DAY_24);
                    lastYearVolume.Add(imsVolumeList[1].DAY_25);
                    lastYearVolume.Add(imsVolumeList[1].DAY_26);
                    lastYearVolume.Add(imsVolumeList[1].DAY_27);
                    lastYearVolume.Add(imsVolumeList[1].DAY_28);
                    lastYearVolume.Add(imsVolumeList[1].DAY_29);
                    lastYearVolume.Add(imsVolumeList[1].DAY_30);
                    lastYearVolume.Add(imsVolumeList[1].DAY_31);
                }
            }

            ViewBag.curentVolumeArray = currentYearVolume;
            ViewBag.lastVolumeArray = lastYearVolume;
            #endregion

            #region Curent Sales,Target,ACH
            List<double> currentSalses = new List<double>();
            List<double> currentTarget = new List<double>();
            List<double> currentAch = new List<double>();
            TargetSalesModel targetSalesList = _masterDashboardDAL.GetTargetSalesList(user, DateTime.Now.Year.ToString());
            if (pcTrend != null)
            {
                currentSalses.Add(targetSalesList.JAN_IMS_VAL);
                currentSalses.Add(targetSalesList.FEB_IMS_VAL);
                currentSalses.Add(targetSalesList.MAR_IMS_VAL);
                currentSalses.Add(targetSalesList.APR_IMS_VAL);
                currentSalses.Add(targetSalesList.MAY_IMS_VAL);
                currentSalses.Add(targetSalesList.JUN_IMS_VAL);
                currentSalses.Add(targetSalesList.JUL_IMS_VAL);
                currentSalses.Add(targetSalesList.AUG_IMS_VAL);
                currentSalses.Add(targetSalesList.SEP_IMS_VAL);
                currentSalses.Add(targetSalesList.OCT_IMS_VAL);
                currentSalses.Add(targetSalesList.NOV_IMS_VAL);
                currentSalses.Add(targetSalesList.DEC_IMS_VAL);
                currentTarget.Add(targetSalesList.JAN_TARGET_VAL);
                currentTarget.Add(targetSalesList.FEB_TARGET_VAL);
                currentTarget.Add(targetSalesList.MAR_TARGET_VAL);
                currentTarget.Add(targetSalesList.APR_TARGET_VAL);
                currentTarget.Add(targetSalesList.MAY_TARGET_VAL);
                currentTarget.Add(targetSalesList.JUN_TARGET_VAL);
                currentTarget.Add(targetSalesList.JUL_TARGET_VAL);
                currentTarget.Add(targetSalesList.AUG_TARGET_VAL);
                currentTarget.Add(targetSalesList.SEP_TARGET_VAL);
                currentTarget.Add(targetSalesList.OCT_TARGET_VAL);
                currentTarget.Add(targetSalesList.NOV_TARGET_VAL);
                currentTarget.Add(targetSalesList.DEC_TARGET_VAL);
                currentAch.Add(targetSalesList.JAN_ACH);
                currentAch.Add(targetSalesList.FEB_ACH);
                currentAch.Add(targetSalesList.MAR_ACH);
                currentAch.Add(targetSalesList.APR_ACH);
                currentAch.Add(targetSalesList.MAY_ACH);
                currentAch.Add(targetSalesList.JUN_ACH);
                currentAch.Add(targetSalesList.JUL_ACH);
                currentAch.Add(targetSalesList.AUG_ACH);
                currentAch.Add(targetSalesList.SEP_ACH);
                currentAch.Add(targetSalesList.OCT_ACH);
                currentAch.Add(targetSalesList.NOV_ACH);
                currentAch.Add(targetSalesList.DEC_ACH);
            }

            ViewBag.curentSalesList = currentSalses;
            ViewBag.curentTargetList = currentTarget;
            ViewBag.curentACHList = currentAch;
            #endregion

            #region Last Year Sales,Target,ACH
            List<double> lastSales = new List<double>();
            List<double> lastTarget = new List<double>();
            List<double> lastACH = new List<double>();
            TargetSalesModel salesListLastYear = _masterDashboardDAL.GetTargetSalesListLastYear(user, (DateTime.Now.Year - 1).ToString());
            if (pcTrend != null)
            {
                lastSales.Add(salesListLastYear.JAN_IMS_VAL);
                lastSales.Add(salesListLastYear.FEB_IMS_VAL);
                lastSales.Add(salesListLastYear.MAR_IMS_VAL);
                lastSales.Add(salesListLastYear.APR_IMS_VAL);
                lastSales.Add(salesListLastYear.MAY_IMS_VAL);
                lastSales.Add(salesListLastYear.JUN_IMS_VAL);
                lastSales.Add(salesListLastYear.JUL_IMS_VAL);
                lastSales.Add(salesListLastYear.AUG_IMS_VAL);
                lastSales.Add(salesListLastYear.SEP_IMS_VAL);
                lastSales.Add(salesListLastYear.OCT_IMS_VAL);
                lastSales.Add(salesListLastYear.NOV_IMS_VAL);
                lastSales.Add(salesListLastYear.DEC_IMS_VAL);
                lastTarget.Add(salesListLastYear.JAN_TARGET_VAL);
                lastTarget.Add(salesListLastYear.FEB_TARGET_VAL);
                lastTarget.Add(salesListLastYear.MAR_TARGET_VAL);
                lastTarget.Add(salesListLastYear.APR_TARGET_VAL);
                lastTarget.Add(salesListLastYear.MAY_TARGET_VAL);
                lastTarget.Add(salesListLastYear.JUN_TARGET_VAL);
                lastTarget.Add(salesListLastYear.JUL_TARGET_VAL);
                lastTarget.Add(salesListLastYear.AUG_TARGET_VAL);
                lastTarget.Add(salesListLastYear.SEP_TARGET_VAL);
                lastTarget.Add(salesListLastYear.OCT_TARGET_VAL);
                lastTarget.Add(salesListLastYear.NOV_TARGET_VAL);
                lastTarget.Add(salesListLastYear.DEC_TARGET_VAL);
                lastACH.Add(salesListLastYear.JAN_ACH);
                lastACH.Add(salesListLastYear.FEB_ACH);
                lastACH.Add(salesListLastYear.MAR_ACH);
                lastACH.Add(salesListLastYear.APR_ACH);
                lastACH.Add(salesListLastYear.MAY_ACH);
                lastACH.Add(salesListLastYear.JUN_ACH);
                lastACH.Add(salesListLastYear.JUL_ACH);
                lastACH.Add(salesListLastYear.SEP_ACH);
                lastACH.Add(salesListLastYear.OCT_ACH);
                lastACH.Add(salesListLastYear.NOV_ACH);
                lastACH.Add(salesListLastYear.DEC_ACH);
            }

            ViewBag.lastSalesList = lastSales;
            ViewBag.lastTargetList = lastTarget;
            ViewBag.lastACHList = lastACH;
            #endregion

            #region Last Five Years Sales Target
            List<double>  lastFiveTarget = new List<double>();
            List<double> lastFiveSales = new List<double>();
            List<string> lastFiveYear = new List<string>();
            List<LastFiveYearSalesModel> fiveYearSalesModelList = _masterDashboardDAL.GetfiveYearsSaleTrend();

            if (fiveYearSalesModelList != null && fiveYearSalesModelList.Count > 0)
            {
                int index = 0;
                foreach (LastFiveYearSalesModel fiveYearSalesModel in fiveYearSalesModelList)
                {
                    lastFiveYear.Add(fiveYearSalesModelList[index].Year);
                    lastFiveSales.Add(fiveYearSalesModelList[index].IMS_VAL);
                    lastFiveTarget.Add(fiveYearSalesModelList[index].TARGET_VAL);
                    ++index;
                }
            }
            ViewBag.fiveYearsTargetList = lastFiveTarget;
            ViewBag.fiveYearsSaleList = lastFiveSales;
            ViewBag.yearList = lastFiveYear;
            #endregion
            return View(model);
        }

        public ActionResult TodaySalesPartial()
        {
            masterDashboardDal _masterDashboardDAL = new masterDashboardDal();
            int user = Convert.ToInt32(this.Session["USER_ID"]);
            DashboardModel model = new DashboardModel();
            string str = DateTime.Now.Year.ToString() + DateTime.Now.ToString("MM");
            model.OrderModel = _masterDashboardDAL.GetOrderCount(user);
            if (model.OrderModel == null)
                model.OrderModel = new OrderModel();
            model.SalesModel = _masterDashboardDAL.GetSalesCount(user);
            if (model.SalesModel == null)
                model.SalesModel = new SalesModel();
            return (ActionResult)this.View((object)model);
        }
    }
}