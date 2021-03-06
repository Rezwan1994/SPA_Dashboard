app.controller("MomRetailerImsLyCtrl", function ($scope, $http, uiGridConstants) {

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
                $scope.gridMomRetailerImsLy.enableGridMenu = response.data[0].DownLoadStatus;
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

    $scope.GetMomRetailerImsLy = function () {

        $http({
            method: "POST",
            url: MyApp.rootPath + "MomRetailerImsLy/GetMomRetailerImsLy",
            data: {
                dCode: $scope.frmMomRetailerImsLy.Division.DivisionCode,
                rCode: $scope.frmMomRetailerImsLy.Region.RegionCode,
                aCode: $scope.frmMomRetailerImsLy.Area.AreaCode,
                tCode: $scope.frmMomRetailerImsLy.Territory.TerritoryCode,
                cCode: $scope.frmMomRetailerImsLy.Customer.CustomerCode
            }
        }).then(function (response) {
            if (response.data.length > 0) {
                $scope.gridMomRetailerImsLy.data = response.data;
            }
            else {
                toastr.warning("No Data Found!", '');
                $scope.gridMomRetailerImsLy.data = [];
            }
        }, function (response) {
            toastr.error("Error!");
        });
    };


    $scope.OnReportTypeChange = function () {
        if ($scope.frmMomRetailerImsLy.RepType.ReportTypeValue == "MomRetailerImsLy" || $scope.frmMomRetailerImsLy.RepType.ReportTypeValue == "Yesterday" || $scope.frmMomRetailerImsLy.RepType.ReportTypeValue == "LastSevendays" || $scope.frmMomRetailerImsLy.RepType.ReportTypeValue == "LastThirtydays" || $scope.frmMomRetailerImsLy.RepType.ReportTypeValue == "CurrentMonth" || $scope.frmMomRetailerImsLy.RepType.ReportTypeValue == "LastMonth" || $scope.frmMomRetailerImsLy.RepType.ReportTypeValue == "MomCy" || $scope.frmMomRetailerImsLy.RepType.ReportTypeValue == "MomRetailProductTrend") {
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
        $scope.dCode = $scope.frmMomRetailerImsLy.Division.DivisionCode;
        //Region List
        $http({
            method: "GET",
            url: MyApp.rootPath + "DistSrSales/GetRegionList",
            params: { dCode: $scope.frmMomRetailerImsLy.Division.DivisionCode }
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
        $scope.rCode = $scope.frmMomRetailerImsLy.Region.RegionCode;
        //Area List
        $http({
            method: "GET",
            url: MyApp.rootPath + "DistSrSales/GetAreaList",
            params: { dCode: $scope.frmMomRetailerImsLy.Division.DivisionCode, rCode: $scope.frmMomRetailerImsLy.Region.RegionCode }
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
        $scope.aCode = $scope.frmMomRetailerImsLy.Area.AreaCode;
        //Territory List
        $http({
            method: "GET",
            url: MyApp.rootPath + "DistSrSales/GetTerritoryList",
            params: { dCode: $scope.frmMomRetailerImsLy.Division.DivisionCode, rCode: $scope.frmMomRetailerImsLy.Region.RegionCode, aCode: $scope.frmMomRetailerImsLy.Area.AreaCode }
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
        $scope.tCode = $scope.frmMomRetailerImsLy.Territory.TerritoryCode;
        //Customer List
        $http({
            method: "GET",
            url: MyApp.rootPath + "DistSrSales/GetCustomerList",
            params: { dCode: $scope.frmMomRetailerImsLy.Division.DivisionCode, rCode: $scope.frmMomRetailerImsLy.Region.RegionCode, aCode: $scope.frmMomRetailerImsLy.Area.AreaCode, tCode: $scope.frmMomRetailerImsLy.Territory.TerritoryCode }
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
        $scope.cCode = $scope.frmMomRetailerImsLy.Customer.CustomerCode;
        //SR List
        $http({
            method: "GET",
            url: MyApp.rootPath + "DistSrSales/GetSrList",
            params: { dCode: $scope.frmMomRetailerImsLy.Division.DivisionCode, rCode: $scope.frmMomRetailerImsLy.Region.RegionCode, aCode: $scope.frmMomRetailerImsLy.Area.AreaCode, tCode: $scope.frmMomRetailerImsLy.Territory.TerritoryCode, cCode: $scope.frmMomRetailerImsLy.Customer.CustomerCode }
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



    $scope.GetReportTypeList = function () {
        $http({
            method: "GET",
            url: MyApp.rootPath + "DistSrSales/GetReportTypeList",
            params: { SlNo: "(24)" }
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

    //Grid
    var columnMomRetailerImsLy = [
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
        { name: 'RouteCode', displayName: "Route Code", width: 150 },
        { name: 'RouteName', displayName: "Route Name", width: 150 },
        { name: 'RetailerCode', displayName: "Retailer Code", width: 150 },
        { name: 'RetailerName', displayName: "Retailer Name", width: 150 },

        { name: 'JanNoofInv', displayName: "Jan No of Inv", width: 120, cellFilter: 'number:0', cellClass: 'grid-align', footerCellFilter: 'number:0', aggregationType: uiGridConstants.aggregationTypes.sum },
        { name: 'JanNetIms', displayName: "Jan Net IMS", width: 120, cellFilter: 'number:2', cellClass: 'grid-align', footerCellFilter: 'number:2', aggregationType: uiGridConstants.aggregationTypes.sum },

        { name: 'FebNoofInv', displayName: "Feb No of Inv", width: 120, cellFilter: 'number:0', cellClass: 'grid-align', footerCellFilter: 'number:0', aggregationType: uiGridConstants.aggregationTypes.sum },
        { name: 'FebNetIms', displayName: "Feb Net IMS", width: 120, cellFilter: 'number:2', cellClass: 'grid-align', footerCellFilter: 'number:2', aggregationType: uiGridConstants.aggregationTypes.sum },

        { name: 'MarNoofInv', displayName: "Mar No of Inv", width: 120, cellFilter: 'number:0', cellClass: 'grid-align', footerCellFilter: 'number:0', aggregationType: uiGridConstants.aggregationTypes.sum },
        { name: 'MarNetIms', displayName: "Mar Net IMS", width: 120, cellFilter: 'number:2', cellClass: 'grid-align', footerCellFilter: 'number:2', aggregationType: uiGridConstants.aggregationTypes.sum },

        { name: 'AprNoofInv', displayName: "Apr No of Inv", width: 120, cellFilter: 'number:0', cellClass: 'grid-align', footerCellFilter: 'number:0', aggregationType: uiGridConstants.aggregationTypes.sum },
        { name: 'AprNetIms', displayName: "Apr Net IMS", width: 120, cellFilter: 'number:2', cellClass: 'grid-align', footerCellFilter: 'number:2', aggregationType: uiGridConstants.aggregationTypes.sum },

        { name: 'MayNoofInv', displayName: "May No of Inv", width: 120, cellFilter: 'number:0', cellClass: 'grid-align', footerCellFilter: 'number:0', aggregationType: uiGridConstants.aggregationTypes.sum },
        { name: 'MayNetIms', displayName: "May Net IMS", width: 120, cellFilter: 'number:2', cellClass: 'grid-align', footerCellFilter: 'number:2', aggregationType: uiGridConstants.aggregationTypes.sum },

        { name: 'JunNoofInv', displayName: "Jun No of Inv", width: 120, cellFilter: 'number:0', cellClass: 'grid-align', footerCellFilter: 'number:0', aggregationType: uiGridConstants.aggregationTypes.sum },
        { name: 'JunNetIms', displayName: "Jun Net IMS", width: 120, cellFilter: 'number:2', cellClass: 'grid-align', footerCellFilter: 'number:2', aggregationType: uiGridConstants.aggregationTypes.sum },

        { name: 'JulNoofInv', displayName: "Jul No of Inv", width: 120, cellFilter: 'number:02', cellClass: 'grid-align', footerCellFilter: 'number:0', aggregationType: uiGridConstants.aggregationTypes.sum },
        { name: 'JulNetIms', displayName: "Jul Net IMS", width: 120, cellFilter: 'number:2', cellClass: 'grid-align', footerCellFilter: 'number:2', aggregationType: uiGridConstants.aggregationTypes.sum },

        { name: 'AugNoofInv', displayName: "Aug No of Inv", width: 120, cellFilter: 'number:0', cellClass: 'grid-align', footerCellFilter: 'number:0', aggregationType: uiGridConstants.aggregationTypes.sum },
        { name: 'AugNetIms', displayName: "Aug Net IMS", width: 120, cellFilter: 'number:2', cellClass: 'grid-align', footerCellFilter: 'number:2', aggregationType: uiGridConstants.aggregationTypes.sum },

        { name: 'SepNoofInv', displayName: "Sep No of Inv", width: 120, cellFilter: 'number:0', cellClass: 'grid-align', footerCellFilter: 'number:0', aggregationType: uiGridConstants.aggregationTypes.sum },
        { name: 'SepNetIms', displayName: "Sep Net IMS", width: 120, cellFilter: 'number:2', cellClass: 'grid-align', footerCellFilter: 'number:2', aggregationType: uiGridConstants.aggregationTypes.sum },

        { name: 'OctNoofInv', displayName: "Oct No of Inv", width: 120, cellFilter: 'number:0', cellClass: 'grid-align', footerCellFilter: 'number:0', aggregationType: uiGridConstants.aggregationTypes.sum },
        { name: 'OctNetIms', displayName: "Oct Net IMS", width: 120, cellFilter: 'number:2', cellClass: 'grid-align', footerCellFilter: 'number:2', aggregationType: uiGridConstants.aggregationTypes.sum },

        { name: 'NovNoofInv', displayName: "Nov No of Inv", width: 120, cellFilter: 'number:0', cellClass: 'grid-align', footerCellFilter: 'number:0', aggregationType: uiGridConstants.aggregationTypes.sum },
        { name: 'NovNetIms', displayName: "Nov IMS Val", width: 120, cellFilter: 'number:2', cellClass: 'grid-align', footerCellFilter: 'number:2', aggregationType: uiGridConstants.aggregationTypes.sum },

        { name: 'DecNoofInv', displayName: "Dec No of Inv", width: 120, cellFilter: 'number:0', cellClass: 'grid-align', footerCellFilter: 'number:0', aggregationType: uiGridConstants.aggregationTypes.sum },
        { name: 'DecNetIms', displayName: "Dec Net IMS", width: 120, cellFilter: 'number:2', cellClass: 'grid-align', footerCellFilter: 'number:2', aggregationType: uiGridConstants.aggregationTypes.sum }
    ];

    $scope.gridMomRetailerImsLy = {
        showColumnFooter: true,
        enableFiltering: true,
        enableSorting: true,
        columnDefs: columnMomRetailerImsLy,
        enableGridMenu: true,
        enableSelectAll: true,
        exporterCsvFilename: "Retailer Wise IMS (Last Year).csv",
        exporterMenuPdf: false,
        exporterCsvLinkElement: angular.element(document.querySelectorAll(".custom-csv-link-location")),
        onRegisterApi: function (gridApi) {
            $scope.gridDivisionMarketIms = gridApi;
        }
    };

    $scope.Reset = function () {
        $scope.FromDate = "";
        $scope.ToDate = "";
        $scope.gridMomRetailerImsLy.data = [];
        $scope.isDisabled = false;
        $scope.ReportType = "";
        $scope.frmMomRetailerImsLy.RepType = undefined;
        $scope.frmMomRetailerImsLy.Division = undefined;
        $scope.frmMomRetailerImsLy.Region = undefined;
        $scope.frmMomRetailerImsLy.Area = undefined;
        $scope.frmMomRetailerImsLy.Territory = undefined;
        $scope.frmMomRetailerImsLy.Customer = undefined;
        $scope.Divisions = [];
        $scope.Regions = [];
        $scope.Areas = [];
        $scope.Territories = [];
        $scope.Customers = [];
        $scope.isDisabled = true;
    };

});