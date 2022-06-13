app.controller("CustomerWiseTradeProgCalCtrl", function ($scope, $http, uiGridConstants) {

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
                $scope.gridCustomerWiseTradeProgCal.enableGridMenu = response.data[0].DownLoadStatus;
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


    $scope.GetCustomerWiseTradeProgCal = function () {

        if ($scope.frmCustomerWiseTradeProgCal.RepType.ReportTypeValue == "CustomerWiseTradeProgramCalculation") {
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
                    methodName = "GetCustomerWiseTradeProgramCalculation";

                }
            }
        }

        $http({
            method: "POST",
            url: MyApp.rootPath + "CustomerWiseTradeProgCal/" + methodName,
            data: {
                fDate: $scope.FromDate,
                tDate: $scope.ToDate,
                dCode: $scope.frmCustomerWiseTradeProgCal.Division.DivisionCode,
                rCode: $scope.frmCustomerWiseTradeProgCal.Region.RegionCode,
                aCode: $scope.frmCustomerWiseTradeProgCal.Area.AreaCode,
                tCode: $scope.frmCustomerWiseTradeProgCal.Territory.TerritoryCode,
                cCode: $scope.frmCustomerWiseTradeProgCal.Customer.CustomerCode,
                tProgramNo: $scope.frmCustomerWiseTradeProgCal.TradeProgram.TradeProgramNo
            }
        }).then(function (response) {
            if (response.data.length > 0) {
                $scope.gridCustomerWiseTradeProgCal.data = response.data;
            }
            else {
                toastr.warning("No Data Found!", '');
                $scope.gridCustomerWiseTradeProgCal.data = [];
            }
        }, function (response) {
            //alert(response);
            toastr.error("Error!");
        });
    };


    $scope.OnReportTypeChange = function () {
        if ($scope.frmCustomerWiseTradeProgCal.RepType.ReportTypeValue == "Today" || $scope.frmCustomerWiseTradeProgCal.RepType.ReportTypeValue == "Yesterday" || $scope.frmCustomerWiseTradeProgCal.RepType.ReportTypeValue == "LastSevendays" || $scope.frmCustomerWiseTradeProgCal.RepType.ReportTypeValue == "LastThirtydays" || $scope.frmCustomerWiseTradeProgCal.RepType.ReportTypeValue == "CurrentMonth" || $scope.frmCustomerWiseTradeProgCal.RepType.ReportTypeValue == "LastMonth" || $scope.frmCustomerWiseTradeProgCal.RepType.ReportTypeValue == "MonthOnMonthCy" || $scope.frmCustomerWiseTradeProgCal.RepType.ReportTypeValue == "MonthOnMonthLy") {
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
        $scope.dCode = $scope.frmCustomerWiseTradeProgCal.Division.DivisionCode;
        //Region List
        $http({
            method: "GET",
            url: MyApp.rootPath + "DistSrSales/GetRegionList",
            params: { dCode: $scope.frmCustomerWiseTradeProgCal.Division.DivisionCode }
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
        $scope.rCode = $scope.frmCustomerWiseTradeProgCal.Region.RegionCode;
        //Area List
        $http({
            method: "GET",
            url: MyApp.rootPath + "DistSrSales/GetAreaList",
            params: { dCode: $scope.frmCustomerWiseTradeProgCal.Division.DivisionCode, rCode: $scope.frmCustomerWiseTradeProgCal.Region.RegionCode }
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
        $scope.aCode = $scope.frmCustomerWiseTradeProgCal.Area.AreaCode;
        //Territory List
        $http({
            method: "GET",
            url: MyApp.rootPath + "DistSrSales/GetTerritoryList",
            params: { dCode: $scope.frmCustomerWiseTradeProgCal.Division.DivisionCode, rCode: $scope.frmCustomerWiseTradeProgCal.Region.RegionCode, aCode: $scope.frmCustomerWiseTradeProgCal.Area.AreaCode }
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
        $scope.tCode = $scope.frmCustomerWiseTradeProgCal.Territory.TerritoryCode;
        //Customer List
        $http({
            method: "GET",
            url: MyApp.rootPath + "DistSrSales/GetCustomerList",
            params: { dCode: $scope.frmCustomerWiseTradeProgCal.Division.DivisionCode, rCode: $scope.frmCustomerWiseTradeProgCal.Region.RegionCode, aCode: $scope.frmCustomerWiseTradeProgCal.Area.AreaCode, tCode: $scope.frmCustomerWiseTradeProgCal.Territory.TerritoryCode }
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
            url: MyApp.rootPath + "InvoiceWiseTradeProgCal/GetTradeProgramList",
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
    $scope.GetTradeProgramList();


    $scope.GetReportTypeList = function () {
        $http({
            method: "GET",
            url: MyApp.rootPath + "DistSrSales/GetReportTypeList",
            params: { SlNo: "(30)" }
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
    var columnCustomerWiseTradeProgCal = [
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
        { name: 'DbLocation', displayName: "DB Location", width: 150 },
        //{ name: 'MarketCode', displayName: "Market Code", width: 150 },
        //{ name: 'MarketName', displayName: "Market Name", width: 150 },
        //{ name: 'RetailerCode', displayName: "Retailer Code", width: 150 },
        //{ name: 'RetailerName', displayName: "Retailer Name", width: 150 },
        //{ name: 'InvoiceNo', displayName: "Invoice No", width: 150 },
        //{ name: 'InvoiceDate', displayName: "Invoice Date", width: 150 },
        { name: 'ProgramNo', displayName: "Progam No", width: 150 },
        { name: 'ProgramName', displayName: "Progam Name", width: 250 },
        //{ name: 'SlabNo', displayName: "Slab No", width: 150, cellClass: 'grid-align' },
        //{ name: 'SlabAmount', displayName: "Slab Amount", width: 120, cellFilter: 'number:2', cellClass: 'grid-align', footerCellFilter: 'number:2', aggregationType: uiGridConstants.aggregationTypes.sum },
        //{ name: 'DiscPct', displayName: "Discount Pct", width: 120, cellFilter: 'number:2', cellClass: 'grid-align' },
        { name: 'SalesValue', displayName: "Sales Value", width: 120, cellFilter: 'number:2', cellClass: 'grid-align', footerCellFilter: 'number:2', aggregationType: uiGridConstants.aggregationTypes.sum },
        { name: 'ReturnValue', displayName: "Return Value", width: 120, cellFilter: 'number:2', cellClass: 'grid-align', footerCellFilter: 'number:2', aggregationType: uiGridConstants.aggregationTypes.sum },
        { name: 'NetIms', displayName: "Net Ims", width: 120, cellFilter: 'number:2', cellClass: 'grid-align', footerCellFilter: 'number:2', aggregationType: uiGridConstants.aggregationTypes.sum },
        { name: 'ReturnSlabAmount', displayName: "Return Slab Amount", width: 120, cellFilter: 'number:2', cellClass: 'grid-align', footerCellFilter: 'number:2', aggregationType: uiGridConstants.aggregationTypes.sum },
        { name: 'DiscountValue', displayName: "Discount Value", width: 120, cellFilter: 'number:2', cellClass: 'grid-align', footerCellFilter: 'number:2', aggregationType: uiGridConstants.aggregationTypes.sum },
        { name: 'ActualDiscunt', displayName: "Actual Discunt", width: 120, cellFilter: 'number:2', cellClass: 'grid-align', footerCellFilter: 'number:2', aggregationType: uiGridConstants.aggregationTypes.sum }



    ];
    $scope.gridCustomerWiseTradeProgCal = {
        showColumnFooter: true,
        enableFiltering: true,
        enableSorting: true,
        columnDefs: columnCustomerWiseTradeProgCal,
        enableGridMenu: true,
        enableSelectAll: true,
        exporterCsvFilename: "Invoice_Wise_Trade_Program_Calculation.csv",
        exporterMenuPdf: false,
        exporterCsvLinkElement: angular.element(document.querySelectorAll(".custom-csv-link-location")),
        onRegisterApi: function (gridApi) {
            $scope.gridApi = gridApi;
        }
    };

    $scope.Reset = function () {
        $scope.FromDate = "";
        $scope.ToDate = "";
        $scope.gridCustomerWiseTradeProgCal.data = [];
        $scope.isDisabled = false;
        $scope.ReportType = "";
        $scope.frmCustomerWiseTradeProgCal.RepType = undefined;
        $scope.frmCustomerWiseTradeProgCal.Division = undefined;
        $scope.frmCustomerWiseTradeProgCal.Region = undefined;
        $scope.frmCustomerWiseTradeProgCal.Area = undefined;
        $scope.frmCustomerWiseTradeProgCal.Territory = undefined;
        $scope.frmCustomerWiseTradeProgCal.Customer = undefined;
        $scope.frmCustomerWiseTradeProgCal.TradeProgram = undefined;
        $scope.Divisions = [];
        $scope.Regions = [];
        $scope.Areas = [];
        $scope.Territories = [];
        $scope.Customers = [];
        $scope.isDisabled = true;
    };

});