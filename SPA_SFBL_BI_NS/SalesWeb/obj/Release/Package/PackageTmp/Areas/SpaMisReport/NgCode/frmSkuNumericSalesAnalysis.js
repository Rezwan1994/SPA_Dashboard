app.controller("SkuNumericSalesAnalysisCtrl", function ($scope, $http, uiGridConstants) {

    $scope.EventPerm(22);
    $scope.isDisabled = true;

    $scope.GetReportDownLoadStatus = function () {
        $http({
            method: "GET",
            url: MyApp.rootPath + "RetailerIms/GetReportDownLoadStatus",
            datatype: "json",
            params: { url: window.location.pathname }
        }).then(function (response) {
            if (response.data.length > 0) {
                $scope.gridSkuNumericSalesAnalysis.enableGridMenu = response.data[0].DownLoadStatus;
            } //else {
            //toastr.warning("No Data Found!", { timeOut: 2000 });
            //}
        }, function (response) {
            if (response.status === 404) {
                toastr.warning("Error Loading Report Download Status!", { timeOut: 2000 });
            }
        });
    };
    $scope.GetReportDownLoadStatus();


    var columnSkuNumericSalesAnalysis = [
        //{ name: 'SlNo', displayName: "Sl. No", type: 'number', width: 100, cellClass: 'grid-align', footerCellFilter: 'number:2' },
        { name: 'DivisionCode', displayName: "Division Code", width: 150, aggregationType: uiGridConstants.aggregationTypes.count, footerCellFilter: 'number:0' },
        { name: 'DivisionName', displayName: "Division Name", width: 150 },
        { name: 'RegionCode', displayName: "Region Code", width: 150 },
        { name: 'RegionName', displayName: "Region Name", width: 150 },
        { name: 'AreaCode', displayName: "Area Code", width: 150 },
        { name: 'AreaName', displayName: "Area Name", width: 150 },
        { name: 'TerritoryCode', displayName: "Territory Code", width: 150 },
        { name: 'TerritoryName', displayName: "Territory Name", width: 150 },
        { name: 'MarketCode', displayName: "Market Code", width: 150 },
        { name: 'MarketName', displayName: "Market Name", width: 150 },
        { name: 'CustomerCode', displayName: "Distributor Code", width: 150 },
        { name: 'CustomerName', displayName: "Distributor Name", width: 150 },
        { name: 'DbLocation', displayName: "DB Location", width: 150 },
        { name: 'ProductCategoryCode', displayName: "Category Code", width: 150 },
        { name: 'ProductCategoryName', displayName: "Category Name", width: 150 },
        { name: 'ProductCode', displayName: "Product Code", width: 150 },
        { name: 'ProductName', displayName: "Product Name", width: 150 },
        { name: 'PackSize', displayName: "Pack Size", width: 150 },
        { name: 'TotalRetailer', displayName: "Total Retailer", width: 120, cellClass: 'grid-align', footerCellFilter: 'number:2' },
        { name: 'NoOfRetailer', displayName: "No Of Retailer", width: 120, cellClass: 'grid-align', footerCellFilter: 'number:2' },
        { name: 'AvgOrderQtyPerRet', displayName: "Average Order Qty Per Retailer", width: 220, cellClass: 'grid-align', footerCellFilter: 'number:2' },
        { name: 'AvgSalesQtyPerInvoice', displayName: "Average Sales Qty Per Invoice", width: 220, cellClass: 'grid-align', footerCellFilter: 'number:2' },
        { name: 'AvgSalesQtyPerMonthRet', displayName: "Retailer Average Sales Qty Per Month", width: 250, cellClass: 'grid-align', footerCellFilter: 'number:2' }
    ];
    $scope.gridSkuNumericSalesAnalysis = {
        showColumnFooter: true,
        enableFiltering: true,
        enableSorting: true,
        columnDefs: columnSkuNumericSalesAnalysis,
        enableGridMenu: true,
        enableSelectAll: true,
        exporterCsvFilename: "SKU_Wise_Numeric_Sales_Analysis.csv",
        exporterMenuPdf: false,
        exporterCsvLinkElement: angular.element(document.querySelectorAll(".custom-csv-link-location")),
        onRegisterApi: function (gridApi) {
            $scope.gridApi = gridApi;
        }
    };


    $scope.GetReportTypeList = function () {
        //Report Type List
        $http({
            method: "GET",
            url: MyApp.rootPath + "DistSrSales/GetReportTypeList",
            params: { SlNo: "(4,5,9)" }
        }).then(function (response) {
            if (response.data.Data.length > 0) {
                $scope.RepTypes = response.data.Data;
            } else {
                toastr.warning("No Data Found!", { timeOut: 2000 });
            }

        }, function (response) {
            if (response.status === 404) {
                toastr.warning("Error Loading Division List!", { timeOut: 2000 });
            }
        });

    }
    $scope.GetReportTypeList();
    $scope.OnReportTypeChange = function () {
        if ($scope.frmSkuNumericSalesAnalysis.RepType.ReportTypeValue == "Yesterday" || $scope.frmSkuNumericSalesAnalysis.RepType.ReportTypeValue == "LastSevendays" || $scope.frmSkuNumericSalesAnalysis.RepType.ReportTypeValue == "LastThirtydays" || $scope.frmSkuNumericSalesAnalysis.RepType.ReportTypeValue == "CurrentMonth" || $scope.frmSkuNumericSalesAnalysis.RepType.ReportTypeValue == "LastMonth" || $scope.frmSkuNumericSalesAnalysis.RepType.ReportTypeValue == "MonthOnMonthCy" || $scope.frmSkuNumericSalesAnalysis.RepType.ReportTypeValue == "MonthOnMonthLy") {
            $scope.isDisabled = true;
            $scope.FromDate = "";
            $scope.ToDate = "";
        }
        else {
            $scope.FromDate = "";
            $scope.ToDate = "";
            $scope.isDisabled = false;
        }
        $scope.GetDivisionList();
    };
    $scope.GetDivisionList = function () {

        //Division List
        $http({
            method: "GET",
            url: MyApp.rootPath + "DistSrSales/GetDivisionList",
        }).then(function (response) {
            if (response.data.Data.length > 0) {
                $scope.Divisions = response.data.Data;
                //$scope.ReportTypes = response.data.Data;
            } else {
                toastr.warning("No Data Found!", { timeOut: 2000 });
            }

        }, function (response) {
            if (response.status === 404) {
                toastr.warning("Error Loading Division List!", { timeOut: 2000 });
            }
        });

    }
    $scope.OnDivisionClick = function () {
        //Region List
        $http({
            method: "GET",
            url: MyApp.rootPath + "DistSrSales/GetRegionList",
            params: { dCode: $scope.frmSkuNumericSalesAnalysis.Division.DivisionCode }
        }).then(function (response) {
            if (response.data.Data.length > 0) {
                $scope.Regions = response.data.Data;
            } else {
                toastr.warning("No Data Found!", { timeOut: 2000 });
            }
        }, function (response) {
            if (response.status === 404) {
                toastr.warning("Error Loading Region List!", { timeOut: 2000 });
            }
        });
    }
    $scope.OnRegionClick = function () {
        //Area List
        $http({
            method: "GET",
            url: MyApp.rootPath + "DistSrSales/GetAreaList",
            params: { dCode: $scope.frmSkuNumericSalesAnalysis.Division.DivisionCode, rCode: $scope.frmSkuNumericSalesAnalysis.Region.RegionCode }
        }).then(function (response) {
            if (response.data.Status === "" || response.data.Status === null) {
                if (response.data.Data.length > 0) {
                    $scope.Areas = response.data.Data;
                } else {
                    toastr.warning("No Data Found!", { timeOut: 2000 });
                }
            } else {
                toastr.warning(response.data.Status, { timeOut: 2000 });
            }
        }, function (response) {
            if (response.status === 404) {
                toastr.warning("Error Loading Area List!", { timeOut: 2000 });
            }
        });
    }
    $scope.OnAreaClick = function () {
        //Territory List
        $http({
            method: "GET",
            url: MyApp.rootPath + "DistSrSales/GetTerritoryList",
            params: { dCode: $scope.frmSkuNumericSalesAnalysis.Division.DivisionCode, rCode: $scope.frmSkuNumericSalesAnalysis.Region.RegionCode, aCode: $scope.frmSkuNumericSalesAnalysis.Area.AreaCode }
        }).then(function (response) {
            if (response.data.Status === "" || response.data.Status === null) {
                if (response.data.Data.length > 0) {
                    $scope.Territories = response.data.Data;
                } else {
                    toastr.warning("No Data Found!", { timeOut: 2000 });
                }
            } else {
                toastr.warning(response.data.Status, { timeOut: 2000 });
            }
        }, function (response) {
            if (response.status === 404) {
                toastr.warning("Error Loading Territory List!", { timeOut: 2000 });
            }
        });
    }
    $scope.OnTerritoryClick = function () {
        //Customer List
        $http({
            method: "GET",
            url: MyApp.rootPath + "DistSrSales/GetCustomerList",
            params: { dCode: $scope.frmSkuNumericSalesAnalysis.Division.DivisionCode, rCode: $scope.frmSkuNumericSalesAnalysis.Region.RegionCode, aCode: $scope.frmSkuNumericSalesAnalysis.Area.AreaCode, tCode: $scope.frmSkuNumericSalesAnalysis.Territory.TerritoryCode }
        }).then(function (response) {
            if (response.data.Status === "" || response.data.Status === null) {
                if (response.data.Data.length > 0) {
                    $scope.Customers = response.data.Data;
                } else {
                    toastr.warning("No Data Found!", { timeOut: 2000 });
                }
            } else {
                toastr.warning(response.data.Status, { timeOut: 2000 });
            }
        }, function (response) {
            if (response.status === 404) {
                toastr.warning("Error Loading Customer List!", { timeOut: 2000 });
            }
        });

    }
    $scope.GetProductCategoryList = function () {

        $http({
            method: "GET",
            url: MyApp.rootPath + "SkuNumericSalesAnalysis/GetProductCategoryList",
        }).then(function (response) {
            if (response.data.Data.length > 0) {
                $scope.Categoris = response.data.Data;
            } else {
                toastr.warning("No Data Found!", { timeOut: 2000 });
            }

        }, function (response) {
            if (response.status === 404) {
                toastr.warning("Error Loading Division List!", { timeOut: 2000 });
            }
        });
    }
    $scope.GetProductCategoryList();




    $scope.GetSkuNumericSalesAnalysis = function () {


        if ($scope.frmSkuNumericSalesAnalysis.RepType.ReportTypeValue == "CurrentMonth") {
            methodName = "GetSkuNumericSalesAnalysisCMonth";
        }
        if ($scope.frmSkuNumericSalesAnalysis.RepType.ReportTypeValue == "LastMonth") {
            methodName = "GetSkuNumericSalesAnalysisLMonth";
        }

        if ($scope.frmSkuNumericSalesAnalysis.RepType.ReportTypeValue == "CustomDate") {
            if ($scope.ToDate == "" || $scope.ToDate == undefined || $scope.ToDate == null) {
                toastr.warning("To Date  Cannot be empty !");
                return false;
            } else {
                var todayDate = new Date(new Date().getFullYear(), new Date().getMonth(), new Date().getDate());
                var endDate = $scope.ToDate.split("/");
                var convertedEndDate = new Date(+endDate[2], endDate[1] - 1, +endDate[0]);
                var eDate = new Date(convertedEndDate.getFullYear(), convertedEndDate.getMonth(), convertedEndDate.getDate());
                if (eDate >= todayDate) {
                    toastr.warning("To Date  Less Than Current Date !");
                    return false;
                } else {
                    methodName = "GetSkuNumericSalesAnalysisDateRange";

                }
            }
        }

        $http({
            method: "POST",
            url: MyApp.rootPath + "SkuNumericSalesAnalysis/" + methodName,
            data: {
                fromDate: $scope.FromDate,
                toDate: $scope.ToDate,
                dCode: $scope.frmSkuNumericSalesAnalysis.Division.DivisionCode,
                rCode: $scope.frmSkuNumericSalesAnalysis.Region.RegionCode,
                aCode: $scope.frmSkuNumericSalesAnalysis.Area.AreaCode,
                tCode: $scope.frmSkuNumericSalesAnalysis.Territory.TerritoryCode,
                cCode: $scope.frmSkuNumericSalesAnalysis.Customer.CustomerCode,
                pcCode: $scope.frmSkuNumericSalesAnalysis.Category.CategoryCode,
            }
        }).then(function (response) {
            if (response.data.length > 0) {
                $scope.gridSkuNumericSalesAnalysis.data = response.data;
            }
            else {
                toastr.warning("No Data Found!", '');
                $scope.gridSkuNumericSalesAnalysis.data = [];
            }
        }, function (response) {
            toastr.error("Error!");
        });
    };

    $scope.Reset = function () {
        $scope.BonusType = "";
        $scope.FromDate = "";
        $scope.ToDate = "";
        $scope.gridSkuNumericSalesAnalysis.data = [];
        $scope.isDisabled = true;
        $scope.frmSkuNumericSalesAnalysis.RepType = undefined;
        $scope.frmSkuNumericSalesAnalysis.Division = undefined;
        $scope.frmSkuNumericSalesAnalysis.Region = undefined;
        $scope.frmSkuNumericSalesAnalysis.Area = undefined;
        $scope.frmSkuNumericSalesAnalysis.Territory = undefined;
        $scope.frmSkuNumericSalesAnalysis.Customer = undefined;
        $scope.frmSkuNumericSalesAnalysis.Category = undefined;
        $scope.Divisions = [];
        $scope.Regions = [];
        $scope.Areas = [];
        $scope.Territories = [];
        $scope.Customers = [];
    };

});