app.controller("CatNumericSalesAnalysisCtrl", function ($scope, $http, uiGridConstants) {
    $scope.isDisabled = true;
    $scope.EventPerm(22);

    $scope.GetReportDownLoadStatus = function () {
        $http({
            method: "GET",
            url: MyApp.rootPath + "RetailerIms/GetReportDownLoadStatus",
            datatype: "json",
            params: { url: window.location.pathname }
        }).then(function (response) {
            if (response.data.length > 0) {
                $scope.gridCatNumericSalesAnalysis.enableGridMenu = response.data[0].DownLoadStatus;
            }
            //else {
            //    toastr.warning("No Data Found!", { timeOut: 2000 });
            //}
        }, function (response) {
            if (response.status === 404) {
                toastr.warning("Error Loading Report Download Status!", { timeOut: 2000 });
            }
        });
    };
    $scope.GetReportDownLoadStatus();

    $scope.GetReportTypeList = function () {
        //Report Type List
        $http({
            method: "GET",
            url: MyApp.rootPath + "DistSrSales/GetReportTypeList",
            params: { SlNo: "(4,5)" }
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
        if ($scope.frmCatNumericSalesAnalysis.RepType.ReportTypeValue == "Yesterday" || $scope.frmCatNumericSalesAnalysis.RepType.ReportTypeValue == "LastSevendays" || $scope.frmCatNumericSalesAnalysis.RepType.ReportTypeValue == "LastThirtydays" || $scope.frmCatNumericSalesAnalysis.RepType.ReportTypeValue == "CurrentMonth" || $scope.frmCatNumericSalesAnalysis.RepType.ReportTypeValue == "LastMonth" || $scope.frmCatNumericSalesAnalysis.RepType.ReportTypeValue == "MonthOnMonthCy" || $scope.frmCatNumericSalesAnalysis.RepType.ReportTypeValue == "MonthOnMonthLy") {
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
        $http({
            method: "GET",
            url: MyApp.rootPath + "DistSrSales/GetDivisionList",
        }).then(function (response) {
            if (response.data.Data.length > 0) {
                $scope.Divisions = response.data.Data;
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
        $http({
            method: "GET",
            url: MyApp.rootPath + "DistSrSales/GetRegionList",
            params: { dCode: $scope.frmCatNumericSalesAnalysis.Division.DivisionCode }
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
        $http({
            method: "GET",
            url: MyApp.rootPath + "DistSrSales/GetAreaList",
            params: { dCode: $scope.frmCatNumericSalesAnalysis.Division.DivisionCode, rCode: $scope.frmCatNumericSalesAnalysis.Region.RegionCode }
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
            params: { dCode: $scope.frmCatNumericSalesAnalysis.Division.DivisionCode, rCode: $scope.frmCatNumericSalesAnalysis.Region.RegionCode, aCode: $scope.frmCatNumericSalesAnalysis.Area.AreaCode }
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
        $http({
            method: "GET",
            url: MyApp.rootPath + "DistSrSales/GetCustomerList",
            params: { dCode: $scope.frmCatNumericSalesAnalysis.Division.DivisionCode, rCode: $scope.frmCatNumericSalesAnalysis.Region.RegionCode, aCode: $scope.frmCatNumericSalesAnalysis.Area.AreaCode, tCode: $scope.frmCatNumericSalesAnalysis.Territory.TerritoryCode }
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
            url: MyApp.rootPath + "CatNumericSalesAnalysis/GetProductCategoryList",
        }).then(function (response) {
            if (response.data.Data.length > 0) {
                $scope.Categoris = response.data.Data;
            } else {
                toastr.warning("No Data Found!", { timeOut: 2000 });
            }

        }, function (response) {
            if (response.status === 404) {
                toastr.warning("Error Loading Category List!", { timeOut: 2000 });
            }
        });
    }
    $scope.GetProductCategoryList();

    var columnCatNumericSalesAnalysis = [
        //{ name: 'SlNo', displayName: "Sl. No", type: 'number', width: 100, cellClass: 'grid-align', footerCellFilter: 'number:2' },
        { name: 'DivisionCode', displayName: "Division Code", width: 150, aggregationType: uiGridConstants.aggregationTypes.count, footerCellFilter: 'number:0'},
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
        { name: 'TotalRetailer', displayName: "Total Retailer", width: 120, cellClass: 'grid-align', footerCellFilter: 'number:2' },
        { name: 'NoOfRetailer', displayName: "No Of Retailer", width: 120, cellClass: 'grid-align', footerCellFilter: 'number:2' },
        { name: 'AvgOrderQtyPerRet', displayName: "Average Order Qty Per Retailer", width: 220, cellClass: 'grid-align', footerCellFilter: 'number:2' },
        { name: 'AvgSalesQtyPerInvoice', displayName: "Average Sales Qty Per Invoice", width: 220, cellClass: 'grid-align', footerCellFilter: 'number:2' },
        { name: 'AvgSalesQtyPerMonthRet', displayName: "Retailer Average Sales Qty Per Month", width: 250, cellClass: 'grid-align', footerCellFilter: 'number:2' }

    ];
    $scope.gridCatNumericSalesAnalysis = {
        showColumnFooter: true,
        enableFiltering: true,
        enableSorting: true,
        columnDefs: columnCatNumericSalesAnalysis,
        enableGridMenu: true,
        enableSelectAll: true,
        exporterCsvFilename: "Cat_Wise_Numeric_Sales_Analysis.csv",
        exporterMenuPdf: false,
        exporterCsvLinkElement: angular.element(document.querySelectorAll(".custom-csv-link-location")),
        onRegisterApi: function (gridApi) {
            $scope.gridApi = gridApi;
        }
    };

    $scope.GetCatNumericSalesAnalysis = function () {


        if ($scope.frmCatNumericSalesAnalysis.RepType.ReportTypeValue == "CurrentMonth") {
            methodName = "GetCatNumericSalesAnalysisCMonth";
        }
        if ($scope.frmCatNumericSalesAnalysis.RepType.ReportTypeValue == "LastMonth") {
            methodName = "GetCatNumericSalesAnalysisLMonth";
        }

        $http({
            method: "POST",
            url: MyApp.rootPath + "CatNumericSalesAnalysis/" + methodName,
            data: {
                fromDate: $scope.FromDate,
                toDate: $scope.ToDate,
                dCode: $scope.frmCatNumericSalesAnalysis.Division.DivisionCode,
                rCode: $scope.frmCatNumericSalesAnalysis.Region.RegionCode,
                aCode: $scope.frmCatNumericSalesAnalysis.Area.AreaCode,
                tCode: $scope.frmCatNumericSalesAnalysis.Territory.TerritoryCode,
                cCode: $scope.frmCatNumericSalesAnalysis.Customer.CustomerCode,
                pcCode: $scope.frmCatNumericSalesAnalysis.Category.CategoryCode,

            }
        }).then(function (response) {
            if (response.data.length > 0) {
                $scope.gridCatNumericSalesAnalysis.data = response.data;
            }
            else {
                toastr.warning("No Data Found!", '');
                $scope.gridCatNumericSalesAnalysis.data = [];
            }
        }, function (response) {
            toastr.error("Error!");
        });
    };


    $scope.Reset = function () {
        $scope.FromDate = "";
        $scope.ToDate = "";
        $scope.gridCatNumericSalesAnalysis.data = [];
        $scope.isDisabled = true;
        $scope.frmCatNumericSalesAnalysis.RepType = undefined;
        $scope.frmCatNumericSalesAnalysis.Division = undefined;
        $scope.frmCatNumericSalesAnalysis.Region = undefined;
        $scope.frmCatNumericSalesAnalysis.Area = undefined;
        $scope.frmCatNumericSalesAnalysis.Territory = undefined;
        $scope.frmCatNumericSalesAnalysis.Customer = undefined;
        $scope.frmCatNumericSalesAnalysis.Category = undefined;
        $scope.Divisions = [];
        $scope.Regions = [];
        $scope.Areas = [];
        $scope.Territories = [];
        $scope.Customers = [];
    };

});