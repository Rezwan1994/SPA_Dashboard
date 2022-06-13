app.controller("DistWisePipelineStockCtrl", function ($scope, $http, uiGridConstants) {

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
                $scope.gridDistWisePipelineStock.enableGridMenu = response.data[0].DownLoadStatus;
            } else {
                toastr.warning("No Data Found!", { timeOut: 2000 });
            }
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
            params: { SlNo: "(4)" }
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
        if ($scope.frmDistWisePipelineStock.RepType.ReportTypeValue == "CurrentMonth") {
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

    var columnDistWisePipelineStock = [
        //{ name: 'SlNo', displayName: "Sl. No", type: 'number', width: 100, cellClass: 'grid-align', footerCellFilter: 'number:2' },
        { name: 'DivisionCode', displayName: "Division Code", width: 150, aggregationType: uiGridConstants.aggregationTypes.count, footerCellFilter: 'number:0' },
        { name: 'DivisionName', displayName: "Division Name", width: 150 },
        { name: 'RegionCode', displayName: "Region Code", width: 150 },
        { name: 'RegionName', displayName: "Region Name", width: 150 },
        { name: 'AreaCode', displayName: "Area Code", width: 150 },
        { name: 'AreaName', displayName: "Area Name", width: 150 },
        { name: 'TerritoryCode', displayName: "Territory Code", width: 150 },
        { name: 'TerritoryName', displayName: "Territory Name", width: 150 },

        { name: 'InvoiceNo', displayName: "Invoice No", width: 150 },
        { name: 'InvoiceDate', displayName: "Invoice Date", width: 150 },

        { name: 'RequisitionNo', displayName: "Requisition No", width: 150 },
        { name: 'RequisitionDate', displayName: "Requisition Date", width: 150 },

        { name: 'DBLocation', displayName: "DB Loaction", width: 150 },
        { name: 'DistributorCode', displayName: "Distributor Code", width: 150 },
        { name: 'DistributorName', displayName: "Distributor Name", width: 150 },
        
        { name: 'ProductCode', displayName: "Product Code", width: 150 },
        { name: 'ProductName', displayName: "Product Name", width: 200 },
        { name: 'PackSize', displayName: "Pack Size", width: 150 },
        { name: 'ProductPrice', displayName: "Product Price", width: 120, cellFilter: 'number:2', cellClass: 'grid-align', footerCellFilter: 'number:2' },

        { name: 'StockQty', displayName: "Stock Qty", width: 120, cellClass: 'grid-align', footerCellFilter: 'number:2' },
        { name: 'PipelineQty', displayName: "'Pipeline Qty'", width: 120, cellClass: 'grid-align', footerCellFilter: 'number:2' }

        
    ];
    $scope.gridDistWisePipelineStock = {
        showColumnFooter: true,
        enableFiltering: true,
        enableSorting: true,
        columnDefs: columnDistWisePipelineStock,
        //rowTemplate: rowTemplate(),
        enableGridMenu: true,
        enableSelectAll: true,
        exporterCsvFilename: 'Distributor_Wise_Pipeline_Stock.csv',
        exporterMenuPdf: false,
        exporterCsvLinkElement: angular.element(document.querySelectorAll(".custom-csv-link-location")),
        onRegisterApi: function (gridApi) {
            $scope.gridApi = gridApi;
        }
    };










    $scope.GetDistWisePipelineStock = function () {

        methodName = "";

        if ($scope.frmDistWisePipelineStock.RepType.ReportTypeValue == "CurrentMonth") {

            methodName = "GetDistWisePipelineStock";

        }

        $http({
            method: "POST",
            url: MyApp.rootPath + "DistWisePipelineStock/" + methodName,
            data: {

                dCode: $scope.frmDistWisePipelineStock.Division.DivisionCode,
                rCode: $scope.frmDistWisePipelineStock.Region.RegionCode,
                aCode: $scope.frmDistWisePipelineStock.Area.AreaCode,
                tCode: $scope.frmDistWisePipelineStock.Territory.TerritoryCode,
                cCode: $scope.frmDistWisePipelineStock.Customer.CustomerCode

            }
        }).then(function (response) {
            if (response.data.length > 0) {
                $scope.gridDistWisePipelineStock.data = response.data;

            }
            else {
                toastr.warning("No Data Found!", '');
                $scope.gridDistWisePipelineStock.data = [];
            }
        }, function () {
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
            params: { dCode: $scope.frmDistWisePipelineStock.Division.DivisionCode }
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
            params: { dCode: $scope.frmDistWisePipelineStock.Division.DivisionCode, rCode: $scope.frmDistWisePipelineStock.Region.RegionCode }
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
            params: { dCode: $scope.frmDistWisePipelineStock.Division.DivisionCode, rCode: $scope.frmDistWisePipelineStock.Region.RegionCode, aCode: $scope.frmDistWisePipelineStock.Area.AreaCode }
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
            params: { dCode: $scope.frmDistWisePipelineStock.Division.DivisionCode, rCode: $scope.frmDistWisePipelineStock.Region.RegionCode, aCode: $scope.frmDistWisePipelineStock.Area.AreaCode, tCode: $scope.frmDistWisePipelineStock.Territory.TerritoryCode }
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
    $scope.Reset = function () {

        $scope.FromDate = "";
        $scope.ToDate = "";
        $scope.gridDistWisePipelineStock.data = [];
        $scope.isDisabled = false;

        $scope.frmDistWisePipelineStock.RepType = "";
        $scope.frmDistWisePipelineStock.Division = undefined;
        $scope.frmDistWisePipelineStock.Region = undefined;
        $scope.frmDistWisePipelineStock.Area = undefined;
        $scope.frmDistWisePipelineStock.Territory = undefined;
        $scope.frmDistWisePipelineStock.Customer = undefined;


        $scope.Divisions = [];
        $scope.Regions = [];
        $scope.Areas = [];
        $scope.Territories = [];
        $scope.Customers = [];
        $scope.Srs = [];

    };

});