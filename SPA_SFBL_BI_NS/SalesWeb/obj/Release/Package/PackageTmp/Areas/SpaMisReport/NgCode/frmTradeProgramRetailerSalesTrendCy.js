app.controller("TradeProgramRetailerSalesTrendCyCtrl", function ($scope, $http, uiGridConstants, uiGridExporterService) {

    $scope.isDisabledFromDate = true;
    $scope.isDisabledToDate = true;
    $scope.EffectType = "";
    var methodName = "";
    $scope.EventPerm(22);

    $scope.GetReportDownLoadStatus = function () {
        $http({
            method: "GET",
            url: MyApp.rootPath + "RetailerIms/GetReportDownLoadStatus",
            datatype: "json",
            params: { url: window.location.pathname }
        }).then(function (response) {
            if (response.data.length > 0) {
                $scope.gridTradeProgramRetailerSalesTrendCy.enableGridMenu = response.data[0].DownLoadStatus;
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
        $http({
            method: "GET",
            url: MyApp.rootPath + "DistSrSales/GetReportTypeList",
            params: { SlNo: "(32,33)" }
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

        if ($scope.frmTradeProgramRetailerSalesTrendCy.RepType.ReportTypeValue == "Today") {
            $scope.isDisabled = true;
            $scope.FromDate = "";
            $scope.ToDate = "";
        }
        else {
            $scope.isDisabled = false;
            $scope.FromDate = "";
            $scope.ToDate = "";
        }
        $scope.GetDivisionList();
    };

    var columnTradeProgramRetailerSalesTrendCy = [
        { name: 'DivisionCode', displayName: "Division Code", width: 150, aggregationType: uiGridConstants.aggregationTypes.count, footerCellFilter: 'number:0' },
        { name: 'DivisionName', displayName: "Division Name", width: 100 },
        { name: 'RegionCode', displayName: "Region Code", width: 100 },
        { name: 'RegionName', displayName: "Region Name", width: 100 },
        { name: 'AreaCode', displayName: "Area Code", width: 100 },
        { name: 'AreaName', displayName: "Area Name", width: 100 },
        { name: 'TerritoryCode', displayName: "Territory Code", width: 100 },
        { name: 'TerritoryName', displayName: "Territory Name", width: 100 },
        { name: 'CustomerCode', displayName: "Distributor Code", width: 100 },
        { name: 'CustomerName', displayName: "Distributor Name", width: 100 },
        { name: 'DbLocation', displayName: "DB Loaction", width: 100 },
        { name: 'MarketCode', displayName: "Market Code", width: 100 },
        { name: 'MarketName', displayName: "Market Name", width: 100 },
        { name: 'RouteCode', displayName: "Route Code", width: 100 },
        { name: 'RouteName', displayName: "Route Name", width: 100 },
        { name: 'RetailerCode', displayName: "Retailer Code", width: 100 },
        { name: 'RetailerName', displayName: "Retailer Name", width: 100 },
        { name: 'TradeProgramNo', displayName: "Trade Program No", width: 100, cellClass: 'grid-align' },
        { name: 'ProgramName', displayName: "Trade Program Name", width: 100 },
        { name: 'EffectType', displayName: "Effect Type", width: 100 },
        { name: 'TradePolicyNo', displayName: "Trade Policy No", width: 100, cellClass: 'grid-align' },
        { name: 'SlabTargetVal', displayName: "Slab Target Val", width: 100, cellClass: 'grid-align', cellFilter: 'number:2' },
        { name: 'SlabUpperAmt', displayName: "Slab Upper Amt", width: 100, cellClass: 'grid-align', cellFilter: 'number:2' },
        { name: 'NoOfInv', displayName: "No Of Invoice", width: 100, cellClass: 'grid-align' },
        { name: 'Gift', displayName: "Gift", width: 100 },
        { name: 'DiscountAmt', displayName: "Discount Amt", width: 100, cellClass: 'grid-align', cellFilter: 'number:2' },
        { name: 'DiscountPercentage', displayName: "Discount Percentage", width: 100, cellClass: 'grid-align', cellFilter: 'number:2' },
        { name: 'EntryDate', displayName: "Entry Date", width: 100 },
        { name: 'ProgramTypeCode', displayName: "Program Type Code", width: 100, visible: false },
        { name: 'ProgramType', displayName: "Program Type", width: 100 },
        { name: 'EffectFromDate', displayName: "Effect From Date", width: 100 },
        { name: 'EffectToDate', displayName: "Effect To Date", width: 100 },

        { name: 'Sales', displayName: "Sales", width: 80, cellClass: 'grid-align', cellFilter: 'number:2', aggregationType: uiGridConstants.aggregationTypes.sum, footerCellFilter: 'number:2' },
        { name: 'Return', displayName: "Return", width: 80, cellClass: 'grid-align', cellFilter: 'number:2', aggregationType: uiGridConstants.aggregationTypes.sum, footerCellFilter: 'number:2' },
        { name: 'Ims', displayName: "IMS", width: 80, cellClass: 'grid-align', cellFilter: 'number:2', aggregationType: uiGridConstants.aggregationTypes.sum, footerCellFilter: 'number:2' },

        { name: 'JanIms', displayName: "Jan IMS", width: 80, cellClass: 'grid-align', cellFilter: 'number:2', aggregationType: uiGridConstants.aggregationTypes.sum, footerCellFilter: 'number:2' },
        { name: 'FebIms', displayName: "Feb IMS", width: 80, cellClass: 'grid-align', cellFilter: 'number:2', aggregationType: uiGridConstants.aggregationTypes.sum, footerCellFilter: 'number:2' },
        { name: 'MarIms', displayName: "Mar IMS", width: 80, cellClass: 'grid-align', cellFilter: 'number:2', aggregationType: uiGridConstants.aggregationTypes.sum, footerCellFilter: 'number:2' },
        { name: 'AprIms', displayName: "Apr IMS", width: 80, cellClass: 'grid-align', cellFilter: 'number:2', aggregationType: uiGridConstants.aggregationTypes.sum, footerCellFilter: 'number:2' },
        { name: 'MayIms', displayName: "May IMS", width: 80, cellClass: 'grid-align', cellFilter: 'number:2', aggregationType: uiGridConstants.aggregationTypes.sum, footerCellFilter: 'number:2' },
        { name: 'JunIms', displayName: "Jun IMS", width: 80, cellClass: 'grid-align', cellFilter: 'number:2', aggregationType: uiGridConstants.aggregationTypes.sum, footerCellFilter: 'number:2' },
        { name: 'JulIms', displayName: "Jul IMS", width: 80, cellClass: 'grid-align', cellFilter: 'number:2', aggregationType: uiGridConstants.aggregationTypes.sum, footerCellFilter: 'number:2' },
        { name: 'AugIms', displayName: "Aug IMS", width: 80, cellClass: 'grid-align', cellFilter: 'number:2', aggregationType: uiGridConstants.aggregationTypes.sum, footerCellFilter: 'number:2' },
        { name: 'SepIms', displayName: "Sep IMS", width: 80, cellClass: 'grid-align', cellFilter: 'number:2', aggregationType: uiGridConstants.aggregationTypes.sum, footerCellFilter: 'number:2' },
        { name: 'OctIms', displayName: "Oct IMS", width: 80, cellClass: 'grid-align', cellFilter: 'number:2', aggregationType: uiGridConstants.aggregationTypes.sum, footerCellFilter: 'number:2' },
        { name: 'NovIms', displayName: "Nov IMS", width: 80, cellClass: 'grid-align', cellFilter: 'number:2', aggregationType: uiGridConstants.aggregationTypes.sum, footerCellFilter: 'number:2' },
        { name: 'DecIms', displayName: "Dec IMS", width: 80, cellClass: 'grid-align', cellFilter: 'number:2', aggregationType: uiGridConstants.aggregationTypes.sum, footerCellFilter: 'number:2' }

    ];

    $scope.gridTradeProgramRetailerSalesTrendCy = {
        showColumnFooter: true,
        enableFiltering: true,
        enableSorting: true,
        columnDefs: columnTradeProgramRetailerSalesTrendCy,
        enableGridMenu: true,
        enableSelectAll: true,
        exporterCsvFilename: 'Trade_Program_Participation_Retailer_Sales_Trend_Current_Year.csv',
        exporterMenuPdf: false,
        exporterCsvLinkElement: angular.element(document.querySelectorAll(".custom-csv-link-location")),
        onRegisterApi: function (gridApi) {
            $scope.gridApi = gridApi;
        }
    };

    $scope.GetTradeProgramRetailerSalesTrendCy = function () {

        if ($scope.frmTradeProgramRetailerSalesTrendCy.RepType.ReportTypeValue == "TradeProgramRetailerSalesTrendCy") {

            methodName = "GetTradeProgramRetailerSalesTrendCy";

        }

        if ($scope.frmTradeProgramRetailerSalesTrendCy.RepType.ReportTypeValue == "TradeProgramRetailerSalesTrendLy") {

            methodName = "GetTradeProgramRetailerSalesTrendLy";

        }

        $http({
            method: "POST",
            url: MyApp.rootPath + "TradeProgramRetailerSalesTrendCy/" + methodName,
            data: {
                dCode: $scope.frmTradeProgramRetailerSalesTrendCy.Division.DivisionCode,
                rCode: $scope.frmTradeProgramRetailerSalesTrendCy.Region.RegionCode,
                aCode: $scope.frmTradeProgramRetailerSalesTrendCy.Area.AreaCode,
                tCode: $scope.frmTradeProgramRetailerSalesTrendCy.Territory.TerritoryCode,
                cCode: $scope.frmTradeProgramRetailerSalesTrendCy.Customer.CustomerCode,
                tradeNo: $scope.frmTradeProgramRetailerSalesTrendCy.TradeProgram.TradeProgramNo,
                eType: $scope.EffectType

            }
        }).then(function (response) {

            if (response.data.length > 0) {
                $scope.gridTradeProgramRetailerSalesTrendCy.data = response.data;
            }
            else {
                toastr.warning("No Data Found!", '');
                $scope.gridTradeProgramRetailerSalesTrendCy.data = [];
            }
        }, function (response) {
            toastr.error("Error!");
        });
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
            params: { dCode: $scope.frmTradeProgramRetailerSalesTrendCy.Division.DivisionCode }
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
            params: { dCode: $scope.frmTradeProgramRetailerSalesTrendCy.Division.DivisionCode, rCode: $scope.frmTradeProgramRetailerSalesTrendCy.Region.RegionCode }
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
            params: { dCode: $scope.frmTradeProgramRetailerSalesTrendCy.Division.DivisionCode, rCode: $scope.frmTradeProgramRetailerSalesTrendCy.Region.RegionCode, aCode: $scope.frmTradeProgramRetailerSalesTrendCy.Area.AreaCode }
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
            params: { dCode: $scope.frmTradeProgramRetailerSalesTrendCy.Division.DivisionCode, rCode: $scope.frmTradeProgramRetailerSalesTrendCy.Region.RegionCode, aCode: $scope.frmTradeProgramRetailerSalesTrendCy.Area.AreaCode, tCode: $scope.frmTradeProgramRetailerSalesTrendCy.Territory.TerritoryCode }
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

    $scope.GetTradeProgramList = function () {
        $http({
            method: "GET",
            url: MyApp.rootPath + "TradeProgramParticipation/GetTradeProgramList",
            params: {
                eType: $scope.EffectType
            }
        }).then(function (response) {
            if (response.data.Status === "" || response.data.Status === null) {
                if (response.data.Data.length > 0) {
                    $scope.TradePrograms = response.data.Data;
                } else {
                    toastr.warning("No Data Found!", { timeOut: 2000 });
                }
            } else {
                toastr.warning(response.data.Status, { timeOut: 2000 });
            }
        }, function (response) {
            if (response.status === 404) {
                toastr.warning("Error Loading Trade Program List!", { timeOut: 2000 });
            }
        });


    }
    $scope.OnEffectTypeClick = function () {

        $scope.GetTradeProgramList();

    }

    $scope.Reset = function () {
        $scope.FromDate = "";
        $scope.ToDate = "";
        $scope.isDisabledFromDate = true;
        $scope.isDisabledToDate = true;
        $scope.EffectType = "";
        $scope.frmTradeProgramRetailerSalesTrendCy.RepType = undefined;
        $scope.frmTradeProgramRetailerSalesTrendCy.Division = undefined;
        $scope.frmTradeProgramRetailerSalesTrendCy.Region = undefined;
        $scope.frmTradeProgramRetailerSalesTrendCy.Area = undefined;
        $scope.frmTradeProgramRetailerSalesTrendCy.Territory = undefined;
        $scope.frmTradeProgramRetailerSalesTrendCy.Customer = undefined;
        $scope.frmTradeProgramRetailerSalesTrendCy.TradeProgram = undefined;
        $scope.Divisions = [];
        $scope.Regions = [];
        $scope.Areas = [];
        $scope.Territories = [];
        $scope.Customers = [];
        $scope.TradePrograms = [];

        $scope.gridTradeProgramRetailerSalesTrendCy.data = [];
    };

});