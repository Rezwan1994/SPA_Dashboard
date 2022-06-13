app.controller("DivisionMarketImsCtrl", function ($scope, $http, $interval, uiGridConstants, $filter) {

    var xl_file_name = "";
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
                $scope.gridDivisionMarketIms.enableGridMenu = response.data[0].DownLoadStatus;
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


    //Grid
    var columnDivisionMarketIms = [
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
        { name: 'DbLoaction', displayName: "DB Loaction", width: 150 },
        { name: 'NoOfInvoice', displayName: "No Of Invoice", width: 120, aggregationType: uiGridConstants.aggregationTypes.sum, cellClass: 'grid-align', footerCellFilter: 'number:0' },
        { name: 'TotalInvoiceAmount', displayName: "Total Invoice Amount", width: 120, cellFilter: 'number:2', aggregationType: uiGridConstants.aggregationTypes.sum, cellClass: 'grid-align', footerCellFilter: 'number:2' },
        { name: 'SlabAdjustment', displayName: "Slab Adjustment", width: 120, cellFilter: 'number:2', aggregationType: uiGridConstants.aggregationTypes.sum, cellClass: 'grid-align', footerCellFilter: 'number:2' },
        { name: 'NetInvoiceAmount', displayName: "Net Invoice Amount", width: 120, cellFilter: 'number:2', aggregationType: uiGridConstants.aggregationTypes.sum, cellClass: 'grid-align', footerCellFilter: 'number:2' },
        { name: 'ReturnValue', displayName: "Return Value", width: 120, cellFilter: 'number:2', aggregationType: uiGridConstants.aggregationTypes.sum, cellClass: 'grid-align', footerCellFilter: 'number:2' },
        { name: 'ReturnSlabAdjustment', displayName: "Return Slab Adjustment", width: 120, cellFilter: 'number:2', aggregationType: uiGridConstants.aggregationTypes.sum, cellClass: 'grid-align', footerCellFilter: 'number:2' },
        { name: 'NetReturnValue', displayName: "Net Return Value", width: 150, cellFilter: 'number:2', aggregationType: uiGridConstants.aggregationTypes.sum, cellClass: 'grid-align', footerCellFilter: 'number:2' },
        { name: 'NetIms', displayName: "Net Ims", width: 120, cellFilter: 'number:2', aggregationType: uiGridConstants.aggregationTypes.sum, cellClass: 'grid-align', footerCellFilter: 'number:02' },
        { name: 'NoOfReplaceInvoice', displayName: "No Of Replace Invoice", width: 100, aggregationType: uiGridConstants.aggregationTypes.sum, cellClass: 'grid-align', footerCellFilter: 'number:0' },
        { name: 'ReplaceInvoiceAmount', displayName: "Replace Invoice Amount", width: 120, cellFilter: 'number:2', aggregationType: uiGridConstants.aggregationTypes.sum, cellClass: 'grid-align', footerCellFilter: 'number:2' },
        { name: 'TargetValue', displayName: "Target Value", width: 120, cellFilter: 'number:2', aggregationType: uiGridConstants.aggregationTypes.sum, cellClass: 'grid-align', footerCellFilter: 'number:2' },
        { name: 'NoOfRetailer', displayName: "No Of Retailer", width: 120, aggregationType: uiGridConstants.aggregationTypes.sum, cellClass: 'grid-align', footerCellFilter: 'number:0' },
        { name: 'NoOfOrderRetailer', displayName: "No Of Order Retailer", width: 120, aggregationType: uiGridConstants.aggregationTypes.sum, cellClass: 'grid-align', footerCellFilter: 'number:0' }



    ];
    $scope.gridDivisionMarketIms = {
        showColumnFooter: true,
        enableFiltering: true,
        enableSorting: true,
        columnDefs: columnDivisionMarketIms,
        enableGridMenu: true,
        enableSelectAll: true,
        exporterCsvFilename: "Division_Market_Wise_IMS.csv",
        exporterMenuPdf: false,
        exporterCsvLinkElement: angular.element(document.querySelectorAll(".custom-csv-link-location")),
        onRegisterApi: function (gridApi) {
            $scope.gridApi = gridApi;
        }
    };

    $scope.GetReportTypeList = function () {
        $http({
            method: "GET",
            url: MyApp.rootPath + "DistSrSales/GetReportTypeList",
            params: { SlNo: "(0,1,2,3,4,5,9)" }
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
        if ($scope.frmDivisionMarketIms.RepType.ReportTypeValue == "Today" ||$scope.frmDivisionMarketIms.RepType.ReportTypeValue == "Yesterday" || $scope.frmDivisionMarketIms.RepType.ReportTypeValue == "LastSevendays" || $scope.frmDivisionMarketIms.RepType.ReportTypeValue == "LastThirtydays" || $scope.frmDivisionMarketIms.RepType.ReportTypeValue == "CurrentMonth" || $scope.frmDivisionMarketIms.RepType.ReportTypeValue == "LastMonth" || $scope.frmDivisionMarketIms.RepType.ReportTypeValue == "MonthOnMonthCy" || $scope.frmDivisionMarketIms.RepType.ReportTypeValue == "MonthOnMonthLy") {
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

    $scope.GetDivisionMarketWiseIms = function () {
        if ($scope.frmDivisionMarketIms.RepType.ReportTypeValue == "Today") {
            methodName = "GetDivMktImsToday";
        }
        if ($scope.frmDivisionMarketIms.RepType.ReportTypeValue == "Yesterday") {
            methodName = "GetDivMktImsYesterday";
        }
        if ($scope.frmDivisionMarketIms.RepType.ReportTypeValue == "LastSevendays") {
            methodName = "GetDivMktImsLastSevendays";
        }
        if ($scope.frmDivisionMarketIms.RepType.ReportTypeValue == "LastThirtydays") {
            methodName = "GetDivMktImsLastThirtydays";
        }
        if ($scope.frmDivisionMarketIms.RepType.ReportTypeValue == "CurrentMonth") {
            methodName = "GetDivMktImsCurrentMonth";
        }
        if ($scope.frmDivisionMarketIms.RepType.ReportTypeValue == "LastMonth") {
            methodName = "GetDivMktImsLastMonth";
        }
        if ($scope.frmDivisionMarketIms.RepType.ReportTypeValue == "MonthOnMonthCy") {
            methodName = "GetRouteBrandImsMonthOnMonthCy";
        }
        if ($scope.frmDivisionMarketIms.RepType.ReportTypeValue == "MonthOnMonthLy") {
            methodName = "GetDivMktImsMonthOnMonthLy";
        }
        if ($scope.frmDivisionMarketIms.RepType.ReportTypeValue == "MonthOnMonthLpy") {
            methodName = "GetDivMktImsMonthOnMonthLpy";
        }
        if ($scope.frmDivisionMarketIms.RepType.ReportTypeValue == "CustomDate") {
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
                    methodName = "GetDivMktImsDateRange";

                }
            }
        }

        $http({
            method: "POST",
            url: MyApp.rootPath + "DivisionMarketIms/" + methodName,
            data: {
                fDate: $scope.FromDate,
                tDate: $scope.ToDate,
                dCode: $scope.frmDivisionMarketIms.Division.DivisionCode,
                rCode: $scope.frmDivisionMarketIms.Region.RegionCode,
                aCode: $scope.frmDivisionMarketIms.Area.AreaCode,
                tCode: $scope.frmDivisionMarketIms.Territory.TerritoryCode,
                cCode: $scope.frmDivisionMarketIms.Customer.CustomerCode
            }
        }).then(function (response) {
            if (response.data.length > 0) {
                $scope.gridDivisionMarketIms.data = response.data;
            }
            else {
                toastr.warning("No Data Found!", '');
                $scope.gridDivisionMarketIms.data = [];
            }
        }, function (response) {
            toastr.error("Error!");
        });
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
            params: { dCode: $scope.frmDivisionMarketIms.Division.DivisionCode }
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
            params: { dCode: $scope.frmDivisionMarketIms.Division.DivisionCode, rCode: $scope.frmDivisionMarketIms.Region.RegionCode }
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
            params: { dCode: $scope.frmDivisionMarketIms.Division.DivisionCode, rCode: $scope.frmDivisionMarketIms.Region.RegionCode, aCode: $scope.frmDivisionMarketIms.Area.AreaCode }
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
            params: { dCode: $scope.frmDivisionMarketIms.Division.DivisionCode, rCode: $scope.frmDivisionMarketIms.Region.RegionCode, aCode: $scope.frmDivisionMarketIms.Area.AreaCode, tCode: $scope.frmDivisionMarketIms.Territory.TerritoryCode }
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
    $scope.OnCustomerClick = function () {

        //SR List
        $http({
            method: "GET",
            url: MyApp.rootPath + "DistSrSales/GetSrList",
            params: { dCode: $scope.frmDivisionMarketIms.Division.DivisionCode, rCode: $scope.frmDivisionMarketIms.Region.RegionCode, aCode: $scope.frmDivisionMarketIms.Area.AreaCode, tCode: $scope.frmDivisionMarketIms.Territory.TerritoryCode, cCode: $scope.frmDivisionMarketIms.Customer.CustomerCode }
        }).then(function (response) {
            if (response.data.Status === "" || response.data.Status === null) {
                if (response.data.Data.length > 0) {
                    $scope.Srs = response.data.Data;
                } else {
                    toastr.warning("No Data Found!", { timeOut: 2000 });
                }
            } else {
                toastr.warning(response.data.Status, { timeOut: 2000 });
            }
        }, function (response) {
            if (response.status === 404) {
                toastr.warning("Error Loading SR List!", { timeOut: 2000 });
            }
        });


    }






    $scope.Reset = function () {
        $scope.FromDate = "";
        $scope.ToDate = "";
        $scope.gridDivisionMarketIms.data = [];
        $scope.isDisabled = true;
        $scope.ReportType = "";
        $scope.frmDivisionMarketIms.RepType = undefined;
        $scope.frmDivisionMarketIms.Division = undefined;
        $scope.frmDivisionMarketIms.Region = undefined;
        $scope.frmDivisionMarketIms.Area = undefined;
        $scope.frmDivisionMarketIms.Territory = undefined;
        $scope.frmDivisionMarketIms.Customer = undefined;
        $scope.Divisions = [];
        $scope.Regions = [];
        $scope.Areas = [];
        $scope.Territories = [];
        $scope.Customers = [];
    };

});