app.controller("InvoiceImsCtrl", function ($scope, $http, uiGridConstants, uiGridExporterService) {
    $scope.isDisabled = true;
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
                $scope.gridInvoiceIms.enableGridMenu = response.data[0].DownLoadStatus;
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

    var columnInvoiceIms = [
        { name: 'DivisionCode', displayName: "Division Code", width: 100, aggregationType: uiGridConstants.aggregationTypes.count, footerCellFilter: 'number:0' },
        { name: 'DivisionName', displayName: "Division Name", width: 150 },
        { name: 'RegionCode', displayName: "Region Code", width: 100 },
        { name: 'RegionName', displayName: "Region Name", width: 150 },
        { name: 'AreaCode', displayName: "Area Code", width: 100 },
        { name: 'AreaName', displayName: "Area Name", width: 150 },
        { name: 'TerritoryCode', displayName: "Territory Code", width: 100 },
        { name: 'TerritoryName', displayName: "Territory Name", width: 150 },
        { name: 'CustomerCode', displayName: "Distributor Code", width: 100 },
        { name: 'CustomerName', displayName: "Distributor Name", width: 150 },
        { name: 'DbLocation', displayName: "DB Location", width: 150 },

        { name: 'FsoCode', displayName: "FSO Code", width: 100 },
        { name: 'FsoName', displayName: "FSO Name", width: 150 },

        { name: 'MarketCode', displayName: "Market Code", width: 100 },
        { name: 'MarketName', displayName: "Market Name", width: 150 },
        { name: 'RouteCode', displayName: "Route Code", width: 100 },
        { name: 'RouteName', displayName: "Route Name", width: 150 },
        { name: 'RetailerCode', displayName: "Retailer Code", width: 100 },
        { name: 'RetailerName', displayName: "Retailer Name", width: 150 },

        { name: 'InvoiceNo', displayName: "Invoice No", width: 100 },
        { name: 'InvoiceDate', displayName: "Invoice Date", width: 150 },
        { name: 'ReturnDate', displayName: "Return Date", width: 100 },
        
        { name: 'InvoiceAmount', displayName: "Invoice Amt", width: 120, cellClass: 'grid-align', footerCellFilter: 'number:2', aggregationType: uiGridConstants.aggregationTypes.sum },
        { name: 'SlabAdjustmentAmt', displayName: "Slab Adjustment Amt", width: 120, cellClass: 'grid-align', footerCellFilter: 'number:2', aggregationType: uiGridConstants.aggregationTypes.sum },
        { name: 'NetInvoiceAmt', displayName: "Net Invoice Amt", width: 120, cellFilter: 'number:2', cellClass: 'grid-align', footerCellFilter: 'number:2', aggregationType: uiGridConstants.aggregationTypes.sum },
        { name: 'ReturnAmt', displayName: "Return Amt", width: 120, cellClass: 'grid-align', footerCellFilter: 'number:2', aggregationType: uiGridConstants.aggregationTypes.sum },
        { name: 'ReturnSlabAdjustmentAmt', displayName: "Return Slab Adjustment Amt", width: 120, cellClass: 'grid-align', footerCellFilter: 'number:2', aggregationType: uiGridConstants.aggregationTypes.sum },
        { name: 'NetReturnAmt', displayName: "Net Return Amt", width: 150, cellFilter: 'number:2', cellClass: 'grid-align', footerCellFilter: 'number:2', aggregationType: uiGridConstants.aggregationTypes.sum },
        { name: 'NetIms', displayName: "Net Ims", width: 120, cellFilter: 'number:2', cellClass: 'grid-align', footerCellFilter: 'number:2', aggregationType: uiGridConstants.aggregationTypes.sum }
    ];
    $scope.gridInvoiceIms = {
        showColumnFooter: true,
        enableFiltering: true,
        enableSorting: true,
        columnDefs: columnInvoiceIms,
        enableGridMenu: true,
        enableSelectAll: true,
        exporterMenuPdf: false,
        exporterCsvLinkElement: angular.element(document.querySelectorAll(".custom-csv-link-location")),
        exporterCsvFilename: 'Invoice_Wise_Ims.csv',
        onRegisterApi: function (gridApi) {
            $scope.gridApi = gridApi;
        }
    };

    $scope.GetReportTypeList = function () {
        $http({
            method: "GET",
            url: MyApp.rootPath + "DistSrSales/GetReportTypeList",
            params: { SlNo: "(9)" }
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


        if ($scope.frmInvoiceIms.RepType.ReportTypeValue == "Today" || $scope.frmInvoiceIms.RepType.ReportTypeValue == "Yesterday" || $scope.frmInvoiceIms.RepType.ReportTypeValue == "LastSevendays" || $scope.frmInvoiceIms.RepType.ReportTypeValue == "LastThirtydays" || $scope.frmInvoiceIms.RepType.ReportTypeValue == "CurrentMonth" || $scope.frmInvoiceIms.RepType.ReportTypeValue == "LastMonth") {
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

    $scope.GetInvoiceIms = function () {

          if ($scope.frmInvoiceIms.RepType.ReportTypeValue == "CustomDate") {
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
                    methodName = "GetInvoiceImsDateRange";

                }
            }
        }

        $http({
            method: "POST",
            url: MyApp.rootPath + "InvoiceIms/" + methodName,
            data: {
                fDate: $scope.FromDate,
                tDate: $scope.ToDate,
                dCode: $scope.frmInvoiceIms.Division.DivisionCode,
                rCode: $scope.frmInvoiceIms.Region.RegionCode,
                aCode: $scope.frmInvoiceIms.Area.AreaCode,
                tCode: $scope.frmInvoiceIms.Territory.TerritoryCode,
                cCode: $scope.frmInvoiceIms.Customer.CustomerCode

            }
        }).then(function (response) {
            if (response.data.length > 0) {
                $scope.gridInvoiceIms.data = response.data;
            }
            else {
                toastr.warning("No Data Found!", '');
                $scope.gridInvoiceIms.data = [];
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
            params: { dCode: $scope.frmInvoiceIms.Division.DivisionCode }
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
            params: { dCode: $scope.frmInvoiceIms.Division.DivisionCode, rCode: $scope.frmInvoiceIms.Region.RegionCode }
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
            params: { dCode: $scope.frmInvoiceIms.Division.DivisionCode, rCode: $scope.frmInvoiceIms.Region.RegionCode, aCode: $scope.frmInvoiceIms.Area.AreaCode }
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
            params: { dCode: $scope.frmInvoiceIms.Division.DivisionCode, rCode: $scope.frmInvoiceIms.Region.RegionCode, aCode: $scope.frmInvoiceIms.Area.AreaCode, tCode: $scope.frmInvoiceIms.Territory.TerritoryCode }
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
            params: { dCode: $scope.frmInvoiceIms.Division.DivisionCode, rCode: $scope.frmInvoiceIms.Region.RegionCode, aCode: $scope.frmInvoiceIms.Area.AreaCode, tCode: $scope.frmInvoiceIms.Territory.TerritoryCode, cCode: $scope.frmInvoiceIms.Customer.CustomerCode }
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
        $scope.gridInvoiceIms.data = [];
        $scope.isDisabled = false;
        $scope.ReportType = "";
        $scope.frmInvoiceIms.Division = undefined;
        $scope.frmInvoiceIms.Region = undefined;
        $scope.frmInvoiceIms.Area = undefined;
        $scope.frmInvoiceIms.Territory = undefined;
        $scope.frmInvoiceIms.Customer = undefined;

        $scope.frmInvoiceIms.RepType = undefined;


        $scope.Divisions = [];
        $scope.Regions = [];
        $scope.Areas = [];
        $scope.Territories = [];
        $scope.Customers = [];


    };

});