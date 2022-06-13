app.controller("RouteWiseOrderCountCtrl", function ($scope, $http, uiGridConstants) {

    var methodName = "";
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
                $scope.gridRouteWiseOrderCount.enableGridMenu = response.data[0].DownLoadStatus;
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

    $scope.GetReportTypeList = function () {
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
                toastr.warning("Error Loading Report Type List!", { timeOut: 2000 });
            }
        });
    }
    $scope.GetReportTypeList();
    $scope.OnReportTypeChange = function () {
        if ($scope.frmRouteWiseOrderCount.RepType.ReportTypeValue == "CurrentMonth" || $scope.frmRouteWiseOrderCount.RepType.ReportTypeValue == "LastMonth") {
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
            params: { dCode: $scope.frmRouteWiseOrderCount.Division.DivisionCode }
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
            params: { dCode: $scope.frmRouteWiseOrderCount.Division.DivisionCode, rCode: $scope.frmRouteWiseOrderCount.Region.RegionCode }
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
        $http({
            method: "GET",
            url: MyApp.rootPath + "DistSrSales/GetTerritoryList",
            params: { dCode: $scope.frmRouteWiseOrderCount.Division.DivisionCode, rCode: $scope.frmRouteWiseOrderCount.Region.RegionCode, aCode: $scope.frmRouteWiseOrderCount.Area.AreaCode }
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
            params: { dCode: $scope.frmRouteWiseOrderCount.Division.DivisionCode, rCode: $scope.frmRouteWiseOrderCount.Region.RegionCode, aCode: $scope.frmRouteWiseOrderCount.Area.AreaCode, tCode: $scope.frmRouteWiseOrderCount.Territory.TerritoryCode }
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

    var columnRouteWiseOrderCount = [
        //{ name: 'SlNo', displayName: "Sl. No", type: 'number', width: 100, cellClass: 'grid-align', footerCellFilter: 'number:2' },
        { name: 'DivisionCode', displayName: "Division Code", width: 150, aggregationType: uiGridConstants.aggregationTypes.count, footerCellFilter: 'number:0'},
        { name: 'DivisionName', displayName: "Division Name", width: 150 },
        { name: 'RegionCode', displayName: "Region Code", width: 150 },
        { name: 'RegionName', displayName: "Region Name", width: 150 },
        { name: 'AreaCode', displayName: "Area Code", width: 150 },
        { name: 'AreaName', displayName: "Area Name", width: 150 },
        { name: 'TerritoryCode', displayName: "Territory Code", width: 150 },
        { name: 'TerritoryName', displayName: "Territory Name", width: 150 },
        { name: 'CustomerCode', displayName: "Distributor Code", width: 150 },
        { name: 'CustomerName', displayName: "Distributor Name", width: 150 },
        { name: 'DbLocation', displayName: "DB Loaction", width: 150 },
        { name: 'MarketCode', displayName: "Market Code", width: 150 },
        { name: 'MarketName', displayName: "Market Name", width: 150 },
        { name: 'RouteCode', displayName: "Route Code", width: 150 },
        { name: 'RouteName', displayName: "Route Name", width: 200 },
        { name: 'TotalRouteRetailer', displayName: "Total Route Retailer", width: 120, cellClass: 'grid-align', aggregationType: uiGridConstants.aggregationTypes.sum, footerCellFilter: 'number:2'},
        { name: 'NoOfRouteVisit', displayName: "No Of Route Visit", width: 120, cellClass: 'grid-align', aggregationType: uiGridConstants.aggregationTypes.sum, footerCellFilter: 'number:2'},
        { name: 'TotalVisitRetailer', displayName: "Total Visit Retailer", width: 120, cellClass: 'grid-align', aggregationType: uiGridConstants.aggregationTypes.sum, footerCellFilter: 'number:2' },
        { name: 'NoOfNormalOrder', displayName: "No Of Normal Order", width: 120, cellClass: 'grid-align', aggregationType: uiGridConstants.aggregationTypes.sum, footerCellFilter: 'number:2'},
        { name: 'NoOfReplaceOrder', displayName: "No Of Replace Order", width: 120, cellClass: 'grid-align', aggregationType: uiGridConstants.aggregationTypes.sum, footerCellFilter: 'number:2'},
        { name: 'NoOfOrderingRetailer', displayName: "No Of Ordering Retailer", width: 120, cellClass: 'grid-align', aggregationType: uiGridConstants.aggregationTypes.sum, footerCellFilter: 'number:2'},
        { name: 'NoOfOrderingSku', displayName: "No Of Ordering Sku", width: 150, cellClass: 'grid-align', aggregationType: uiGridConstants.aggregationTypes.sum, footerCellFilter: 'number:2' },
        { name: 'OrderValue', displayName: "Order Value", width: 120, cellFilter: 'number:2', cellClass: 'grid-align', footerCellFilter: 'number:2', aggregationType: uiGridConstants.aggregationTypes.sum },
        { name: 'ProductivityCall', displayName: "Productivity Call", width: 120, cellClass: 'grid-align'},
        { name: 'Lpc', displayName: "Lpc", width: 120, cellClass: 'grid-align'}
    ];
    $scope.gridRouteWiseOrderCount = {
        showColumnFooter: true,
        enableFiltering: true,
        enableSorting: true,
        columnDefs: columnRouteWiseOrderCount,
        enableGridMenu: true,
        enableSelectAll: true,
        exporterCsvFilename: 'Route_Wise_Order_Count.csv',
        exporterMenuPdf: false,
        exporterCsvLinkElement: angular.element(document.querySelectorAll(".custom-csv-link-location")),
        onRegisterApi: function (gridApi) {
            $scope.gridApi = gridApi;
        }
    };

    $scope.GetRouteWiseOrderCount = function () {
        if ($scope.frmRouteWiseOrderCount.RepType.ReportTypeValue == "CurrentMonth") {
            methodName = "GetRouteWiseOrderCountCurrentMonth";
        }
        if ($scope.frmRouteWiseOrderCount.RepType.ReportTypeValue == "LastMonth") {
            methodName = "GetRouteWiseOrderCountLastMonth";
        }
        if ($scope.frmRouteWiseOrderCount.RepType.ReportTypeValue == "CustomDate") {
            methodName = "GetRouteWiseOrderCountCustomDate";
        }
        $http({
            method: "POST",
            url: MyApp.rootPath + "RouteWiseOrderCount/" + methodName,
            data: {
                fromDate: $scope.FromDate,
                toDate: $scope.ToDate,
                dCode: $scope.frmRouteWiseOrderCount.Division.DivisionCode,
                rCode: $scope.frmRouteWiseOrderCount.Region.RegionCode,
                aCode: $scope.frmRouteWiseOrderCount.Area.AreaCode,
                tCode: $scope.frmRouteWiseOrderCount.Territory.TerritoryCode,
                cCode: $scope.frmRouteWiseOrderCount.Customer.CustomerCode
            }
        }).then(function (response) {
            if (response.data.length > 0) {
                $scope.gridRouteWiseOrderCount.data = response.data;
            }
            else {
                toastr.warning("No Data Found!", '');
                $scope.gridRouteWiseOrderCount.data = [];
            }
         }, function (response) {
            toastr.error("Error!");
        });
    };

    $scope.Reset = function () {
        $scope.FromDate = "";
        $scope.ToDate = "";
        $scope.gridRouteWiseOrderCount.data = [];
        $scope.isDisabled = false;
        $scope.frmRouteWiseOrderCount.RepType = undefined;
        $scope.frmRouteWiseOrderCount.Division = undefined;
        $scope.frmRouteWiseOrderCount.Region = undefined;
        $scope.frmRouteWiseOrderCount.Area = undefined;
        $scope.frmRouteWiseOrderCount.Territory = undefined;
        $scope.frmRouteWiseOrderCount.Customer = undefined;
        $scope.Divisions = [];
        $scope.Regions = [];
        $scope.Areas = [];
        $scope.Territories = [];
        $scope.Customers = [];
    };

});