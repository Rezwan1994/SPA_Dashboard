app.controller("TradeProgramParticipationCtrl", function ($scope, $http, uiGridConstants, uiGridExporterService) {

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
                $scope.gridTradeProgramParticipation.enableGridMenu = response.data[0].DownLoadStatus;
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
            params: { SlNo: "(15)" }
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

        if ($scope.frmTradeProgramParticipation.RepType.ReportTypeValue == "Today" || $scope.frmTradeProgramParticipation.RepType.ReportTypeValue == "Yesterday" || $scope.frmTradeProgramParticipation.RepType.ReportTypeValue === "LastSevendays" || $scope.frmTradeProgramParticipation.RepType.ReportTypeValue == "LastThirtydays" || $scope.frmTradeProgramParticipation.RepType.ReportTypeValue == "CurrentMonth" || $scope.frmTradeProgramParticipation.RepType.ReportTypeValue == "LastMonth") {
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

    var columnTradeProgramParticipation = [       
        { name: 'DivisionCode', displayName: "Division Code", width: 150, aggregationType: uiGridConstants.aggregationTypes.count, footerCellFilter: 'number:0' },
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
        { name: 'RetailerCode', displayName: "Retailer Code", width: 150 },
        { name: 'RetailerName', displayName: "Retailer Name", width: 200 },

        { name: 'TradeProgramNo', displayName: "Trade Program No", width: 200, cellClass: 'grid-align'},
        { name: 'ProgramName', displayName: "Trade Program Name", width: 300 },
        { name: 'EffectType', displayName: "Effect Type", width: 200 },
        { name: 'TradePolicyNo', displayName: "Trade Policy No", width: 200, cellClass: 'grid-align'},

        { name: 'SlabTargetVal', displayName: "Slab Target Val", width: 200, cellClass: 'grid-align', cellFilter: 'number:2'},
        { name: 'SlabUpperAmt', displayName: "Slab Upper Amt", width: 200, cellClass: 'grid-align', cellFilter: 'number:2'},

        { name: 'NoOfInv', displayName: "No Of Invoice", width: 200, cellClass: 'grid-align' },
        { name: 'Gift', displayName: "Gift", width: 200 },

        { name: 'DiscountAmt', displayName: "Discount Amt", width: 200, cellClass: 'grid-align', cellFilter: 'number:2' },
        { name: 'DiscountPercentage', displayName: "Discount Percentage", width: 200, cellClass: 'grid-align', cellFilter: 'number:2' },


        { name: 'EntryDate', displayName: "Entry Date", width: 200 },
        { name: 'ProgramTypeCode', displayName: "Program Type Code", width: 150, visible: false },
        { name: 'ProgramType', displayName: "Program Type", width: 200 },
        { name: 'EffectFromDate', displayName: "Effect From Date", width: 200 },
        { name: 'EffectToDate', displayName: "Effect To Date", width: 200 },

        { name: 'SalesValue', displayName: "Sales Value", width: 200, cellClass: 'grid-align', cellFilter: 'number:2', aggregationType: uiGridConstants.aggregationTypes.sum, footerCellFilter: 'number:2'},
        { name: 'ReturnValue', displayName: "Return Value", width: 200, cellClass: 'grid-align', cellFilter: 'number:2', aggregationType: uiGridConstants.aggregationTypes.sum, footerCellFilter: 'number:2'},
        { name: 'ImsValue', displayName: "IMS Value", width: 200, cellClass: 'grid-align', cellFilter: 'number:2', aggregationType: uiGridConstants.aggregationTypes.sum, footerCellFilter: 'number:2'}
    ];


    $scope.gridTradeProgramParticipation = {
        //showGridFooter: true,
        showColumnFooter: true,
        enableFiltering: true,
        enableSorting: true,
        columnDefs: columnTradeProgramParticipation,
        enableGridMenu: true,
        enableSelectAll: true,
        exporterCsvFilename: 'Retailer_Wise_Ims.csv',
        exporterMenuPdf: false,
        exporterCsvLinkElement: angular.element(document.querySelectorAll(".custom-csv-link-location")),

        onRegisterApi: function (gridApi) {
            $scope.gridApi = gridApi;
        }

    };



    $scope.GetTradeProgramParticipation = function () {

        if ($scope.frmTradeProgramParticipation.RepType.ReportTypeValue == "TradeProgramParticipation") {

            methodName = "GetTradeProgramParticipationList";

        }

        $http({
            method: "POST",
            url: MyApp.rootPath + "TradeProgramParticipation/" + methodName,
            data: {
                dCode: $scope.frmTradeProgramParticipation.Division.DivisionCode,
                rCode: $scope.frmTradeProgramParticipation.Region.RegionCode,
                aCode: $scope.frmTradeProgramParticipation.Area.AreaCode,
                tCode: $scope.frmTradeProgramParticipation.Territory.TerritoryCode,
                cCode: $scope.frmTradeProgramParticipation.Customer.CustomerCode,
                tradeNo: $scope.frmTradeProgramParticipation.TradeProgram.TradeProgramNo,
                eType: $scope.EffectType

            }
        }).then(function (response) {

            if (response.data.length > 0) {
                $scope.gridTradeProgramParticipation.data = response.data;
            }
            else {
                toastr.warning("No Data Found!", '');
                $scope.gridTradeProgramParticipation.data = [];
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
            params: { dCode: $scope.frmTradeProgramParticipation.Division.DivisionCode }
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
            params: { dCode: $scope.frmTradeProgramParticipation.Division.DivisionCode, rCode: $scope.frmTradeProgramParticipation.Region.RegionCode }
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
            params: { dCode: $scope.frmTradeProgramParticipation.Division.DivisionCode, rCode: $scope.frmTradeProgramParticipation.Region.RegionCode, aCode: $scope.frmTradeProgramParticipation.Area.AreaCode }
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
            params: { dCode: $scope.frmTradeProgramParticipation.Division.DivisionCode, rCode: $scope.frmTradeProgramParticipation.Region.RegionCode, aCode: $scope.frmTradeProgramParticipation.Area.AreaCode, tCode: $scope.frmTradeProgramParticipation.Territory.TerritoryCode }
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
                toastr.warning("Error Loading SR List!", { timeOut: 2000 });
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

       

        $scope.frmTradeProgramParticipation.RepType = undefined;
        $scope.frmTradeProgramParticipation.Division = undefined;
        $scope.frmTradeProgramParticipation.Region = undefined;
        $scope.frmTradeProgramParticipation.Area = undefined;
        $scope.frmTradeProgramParticipation.Territory = undefined;
        $scope.frmTradeProgramParticipation.Customer = undefined;
        $scope.frmTradeProgramParticipation.TradeProgram = undefined;
        $scope.Divisions = [];
        $scope.Regions = [];
        $scope.Areas = [];
        $scope.Territories = [];
        $scope.Customers = [];
        $scope.TradePrograms = [];

        $scope.gridTradeProgramParticipation.data = [];
    };

});