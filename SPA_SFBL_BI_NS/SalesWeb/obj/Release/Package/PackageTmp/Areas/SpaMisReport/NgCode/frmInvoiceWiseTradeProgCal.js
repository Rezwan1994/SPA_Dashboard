app.controller("InvoiceWiseTradeProgCalCtrl", function ($scope, $http, uiGridConstants) {

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
                $scope.gridInvoiceWiseProdSales.enableGridMenu = response.data[0].DownLoadStatus;
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

    $scope.GetInvoiceWiseTradeProgCal = function () {

        if ($scope.frmInvoiceWiseTradeProgCal.RepType.ReportTypeValue == "InvoiceWiseTradeProgramCalculation") {
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
                    methodName = "GetInvoiceWiseTradeProgramCalculation";

                }
            }
        }

        $http({
            method: "POST",
            url: MyApp.rootPath + "InvoiceWiseTradeProgCal/" + methodName,
            data: {
                fDate: $scope.FromDate,
                tDate: $scope.ToDate,
                dCode: $scope.frmInvoiceWiseTradeProgCal.Division.DivisionCode,
                rCode: $scope.frmInvoiceWiseTradeProgCal.Region.RegionCode,
                aCode: $scope.frmInvoiceWiseTradeProgCal.Area.AreaCode,
                tCode: $scope.frmInvoiceWiseTradeProgCal.Territory.TerritoryCode,
                cCode: $scope.frmInvoiceWiseTradeProgCal.Customer.CustomerCode,
                tProgramNo: $scope.frmInvoiceWiseTradeProgCal.TradeProgram.TradeProgramNo
            }
        }).then(function (response) {
            if (response.data.length > 0) {
                $scope.gridInvoiceWiseTradeProgCal.data = response.data;
            }
            else {
                toastr.warning("No Data Found!", '');
                $scope.gridInvoiceWiseTradeProgCal.data = [];
            }
        }, function (response) {
            //alert(response);
            toastr.error("Error!");
        });
    };


    $scope.OnReportTypeChange = function () {
        if ($scope.frmInvoiceWiseTradeProgCal.RepType.ReportTypeValue == "Today" || $scope.frmInvoiceWiseTradeProgCal.RepType.ReportTypeValue == "Yesterday" || $scope.frmInvoiceWiseTradeProgCal.RepType.ReportTypeValue == "LastSevendays" || $scope.frmInvoiceWiseTradeProgCal.RepType.ReportTypeValue == "LastThirtydays" || $scope.frmInvoiceWiseTradeProgCal.RepType.ReportTypeValue == "CurrentMonth" || $scope.frmInvoiceWiseTradeProgCal.RepType.ReportTypeValue == "LastMonth" || $scope.frmInvoiceWiseTradeProgCal.RepType.ReportTypeValue == "MonthOnMonthCy" || $scope.frmInvoiceWiseTradeProgCal.RepType.ReportTypeValue == "MonthOnMonthLy") {
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
        $scope.dCode = $scope.frmInvoiceWiseTradeProgCal.Division.DivisionCode;
        //Region List
        $http({
            method: "GET",
            url: MyApp.rootPath + "DistSrSales/GetRegionList",
            params: { dCode: $scope.frmInvoiceWiseTradeProgCal.Division.DivisionCode }
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
        $scope.rCode = $scope.frmInvoiceWiseTradeProgCal.Region.RegionCode;
        //Area List
        $http({
            method: "GET",
            url: MyApp.rootPath + "DistSrSales/GetAreaList",
            params: { dCode: $scope.frmInvoiceWiseTradeProgCal.Division.DivisionCode, rCode: $scope.frmInvoiceWiseTradeProgCal.Region.RegionCode }
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
        $scope.aCode = $scope.frmInvoiceWiseTradeProgCal.Area.AreaCode;
        //Territory List
        $http({
            method: "GET",
            url: MyApp.rootPath + "DistSrSales/GetTerritoryList",
            params: { dCode: $scope.frmInvoiceWiseTradeProgCal.Division.DivisionCode, rCode: $scope.frmInvoiceWiseTradeProgCal.Region.RegionCode, aCode: $scope.frmInvoiceWiseTradeProgCal.Area.AreaCode }
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
        $scope.tCode = $scope.frmInvoiceWiseTradeProgCal.Territory.TerritoryCode;
        //Customer List
        $http({
            method: "GET",
            url: MyApp.rootPath + "DistSrSales/GetCustomerList",
            params: { dCode: $scope.frmInvoiceWiseTradeProgCal.Division.DivisionCode, rCode: $scope.frmInvoiceWiseTradeProgCal.Region.RegionCode, aCode: $scope.frmInvoiceWiseTradeProgCal.Area.AreaCode, tCode: $scope.frmInvoiceWiseTradeProgCal.Territory.TerritoryCode }
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
            params: { SlNo: "(20)" }
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
    var columnInvoiceWiseTradeProgCal = [        
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
        { name: 'MarketCode', displayName: "Market Code", width: 150 },
        { name: 'MarketName', displayName: "Market Name", width: 150 },
        { name: 'RetailerCode', displayName: "Retailer Code", width: 150 },
        { name: 'RetailerName', displayName: "Retailer Name", width: 150 },
        { name: 'InvoiceNo', displayName: "Invoice No", width: 150 },
        { name: 'InvoiceDate', displayName: "Invoice Date", width: 150 },
        { name: 'ProgramNo', displayName: "Progam No", width: 150 },
        { name: 'ProgramName', displayName: "Progam Name", width: 250 },
        { name: 'SlabNo', displayName: "Slab No", width: 150, cellClass: 'grid-align'},
        { name: 'SlabAmount', displayName: "Slab Amount", width: 120, cellFilter: 'number:2', cellClass: 'grid-align', footerCellFilter: 'number:2', aggregationType: uiGridConstants.aggregationTypes.sum },
        { name: 'DiscPct', displayName: "Discount Pct", width: 120, cellFilter: 'number:2', cellClass: 'grid-align'},
        { name: 'GiftItemQty', displayName: "Gift Item Qty", width: 150, cellClass: 'grid-align' },
        { name: 'SalesValue', displayName: "Sales Value", width: 120, cellFilter: 'number:2', cellClass: 'grid-align', footerCellFilter: 'number:2', aggregationType: uiGridConstants.aggregationTypes.sum },
        { name: 'ReturnValue',      displayName: "Return Value",       width: 120, cellFilter: 'number:2', cellClass: 'grid-align', footerCellFilter: 'number:2', aggregationType: uiGridConstants.aggregationTypes.sum },
        { name: 'NetIms',           displayName: "Net Ims",            width: 120, cellFilter: 'number:2', cellClass: 'grid-align', footerCellFilter: 'number:2', aggregationType: uiGridConstants.aggregationTypes.sum },
        { name: 'ReturnSlabAmount', displayName: "Return Slab Amount", width: 120, cellFilter: 'number:2', cellClass: 'grid-align', footerCellFilter: 'number:2', aggregationType: uiGridConstants.aggregationTypes.sum },
        { name: 'DiscountValue',    displayName: "Discount Value",     width: 120, cellFilter: 'number:2', cellClass: 'grid-align', footerCellFilter: 'number:2', aggregationType: uiGridConstants.aggregationTypes.sum },
        { name: 'ActualDiscunt',    displayName: "Actual Discunt",     width: 120, cellFilter: 'number:2', cellClass: 'grid-align', footerCellFilter: 'number:2', aggregationType: uiGridConstants.aggregationTypes.sum }
        


    ];
    $scope.gridInvoiceWiseTradeProgCal = {
        showColumnFooter: true,
        enableFiltering: true,
        enableSorting: true,
        columnDefs: columnInvoiceWiseTradeProgCal,
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
        $scope.gridInvoiceWiseTradeProgCal.data = [];
        $scope.isDisabled = false;
        $scope.ReportType = "";
        $scope.frmInvoiceWiseTradeProgCal.RepType = undefined;
        $scope.frmInvoiceWiseTradeProgCal.Division = undefined;
        $scope.frmInvoiceWiseTradeProgCal.Region = undefined;
        $scope.frmInvoiceWiseTradeProgCal.Area = undefined;
        $scope.frmInvoiceWiseTradeProgCal.Territory = undefined;
        $scope.frmInvoiceWiseTradeProgCal.Customer = undefined;
        $scope.frmInvoiceWiseTradeProgCal.TradeProgram = undefined;
        $scope.Divisions = [];
        $scope.Regions = [];
        $scope.Areas = [];
        $scope.Territories = [];
        $scope.Customers = [];
        $scope.isDisabled = true;
    };

});