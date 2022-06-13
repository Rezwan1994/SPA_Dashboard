app.controller("InvoiceWiseProdSalesCtrl", function ($scope, $http, $interval, uiGridConstants, $filter) {

    var xl_file_name = "";
    var methodName = "";
    var ProductLis="";
    $scope.EventPerm(22);

    $scope.FromDate = "";


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
        if ($scope.frmInvoiceWiseProdSales.RepType.ReportTypeValue == "Today" || $scope.frmInvoiceWiseProdSales.RepType.ReportTypeValue == "Yesterday" || $scope.frmInvoiceWiseProdSales.RepType.ReportTypeValue == "LastSevendays" || $scope.frmInvoiceWiseProdSales.RepType.ReportTypeValue == "LastThirtydays" || $scope.frmInvoiceWiseProdSales.RepType.ReportTypeValue == "CurrentMonth" || $scope.frmInvoiceWiseProdSales.RepType.ReportTypeValue == "LastMonth" || $scope.frmInvoiceWiseProdSales.RepType.ReportTypeValue == "MonthOnMonthCy" || $scope.frmInvoiceWiseProdSales.RepType.ReportTypeValue == "MonthOnMonthLy") {
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

    $scope.GetInvoiceWiseProdSales = function () {

        if ($scope.frmInvoiceWiseProdSales.RepType.ReportTypeValue == "CustomDate") {
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
                    methodName = "GetInvoiceWiseProdSales";

                }
            }
        }

        $http({
            method: "POST",
            url: MyApp.rootPath + "InvoiceWiseProdSales/" + methodName,
            data: {
                fDate: $scope.FromDate,
                tDate: $scope.ToDate,
                pCode: $scope.frmInvoiceWiseProdSales.Product.ProductCode,
                dCode: $scope.frmInvoiceWiseProdSales.Division.DivisionCode,
                rCode: $scope.frmInvoiceWiseProdSales.Region.RegionCode,
                aCode: $scope.frmInvoiceWiseProdSales.Area.AreaCode,
                tCode: $scope.frmInvoiceWiseProdSales.Territory.TerritoryCode,
                cCode: $scope.frmInvoiceWiseProdSales.Customer.CustomerCode
               
            }
        }).then(function (response) {
            if (response.data.length > 0) {
                $scope.gridInvoiceWiseProdSales.data = response.data;
            }
            else {
                toastr.warning("No Data Found!", '');
                $scope.gridInvoiceWiseProdSales.data = [];
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
            params: { dCode: $scope.frmInvoiceWiseProdSales.Division.DivisionCode }
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
            params: { dCode: $scope.frmInvoiceWiseProdSales.Division.DivisionCode, rCode: $scope.frmInvoiceWiseProdSales.Region.RegionCode }
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
            params: { dCode: $scope.frmInvoiceWiseProdSales.Division.DivisionCode, rCode: $scope.frmInvoiceWiseProdSales.Region.RegionCode, aCode: $scope.frmInvoiceWiseProdSales.Area.AreaCode }
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
            params: { dCode: $scope.frmInvoiceWiseProdSales.Division.DivisionCode, rCode: $scope.frmInvoiceWiseProdSales.Region.RegionCode, aCode: $scope.frmInvoiceWiseProdSales.Area.AreaCode, tCode: $scope.frmInvoiceWiseProdSales.Territory.TerritoryCode }
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

        $http({
            method: "GET",
            url: MyApp.rootPath + "ProductBonus/GetProductList"
        }).then(function (response) {
            if (response.data.Data.length > 0) {
                $scope.Products = response.data.Data;
            } else {
                toastr.warning("No Data Found!", { timeOut: 2000 });
            }
        }, function (response) {
            if (response.status === 404) {
                toastr.warning("Error Loading Product List!", { timeOut: 2000 });
            }
        });


    }

    //Grid
    var columnInvoiceWiseProdSales = [
        //{ name: 'SlNo', displayName: "Sl. No", type: 'number', width: 100, cellClass: 'grid-align', footerCellFilter: 'number:2' },
        { name: 'DivisionCode', displayName: "Division Code", width: 150, aggregationType: uiGridConstants.aggregationTypes.count, footerCellFilter: 'number:0'},
        { name: 'DivisionName', displayName: "Division Name", width: 150 },
        { name: 'RegionCode', displayName: "Region Code", width: 150 },
        { name: 'RegionName', displayName: "Region Name", width: 150 },
        { name: 'AreaCode', displayName: "Area Code", width: 150 },
        { name: 'AreaName', displayName: "Area Name", width: 150 },
        { name: 'TerritoryCode', displayName: "Territory Code", width: 150 },
        { name: 'TerritoryName', displayName: "Territory Name", width: 150 },
        { name: 'CustomerCode', displayName: "Customer Code", width: 150 },
        { name: 'CustomerName', displayName: "Customer Name", width: 150 },
        { name: 'MarketCode', displayName: "Market Code", width: 150 },
        { name: 'MarketName', displayName: "Market Name", width: 150 },
        { name: 'RouteCode', displayName: "Route Code", width: 150 },
        { name: 'RouteName', displayName: "Route Name", width: 150 },
        { name: 'RetailerCode', displayName: "Retailer Code", width: 150 },
        { name: 'RetailerName', displayName: "Retailer Name", width: 150 },
        { name: 'InvoiceNo', displayName: "Invoice No", width: 150 },
        { name: 'InvoiceDate', displayName: "Invoice Date", width: 150 },
        { name: 'ProductCode', displayName: "Product Code", width: 150 },
        { name: 'ProductName', displayName: "Product Name", width: 150 },
        { name: 'PackSize', displayName: "PackSize", width: 150 },
        { name: 'SalesQty', displayName: "Sales Qty", width: 150, aggregationType: uiGridConstants.aggregationTypes.sum, cellClass: 'grid-align', footerCellFilter: 'number:0' },
        { name: 'ReturnQty', displayName: "Return Qty", width: 150, aggregationType: uiGridConstants.aggregationTypes.sum, cellClass: 'grid-align', footerCellFilter: 'number:0'},
        { name: 'NetImsQty', displayName: "Net IMS Qty", width: 150 , aggregationType: uiGridConstants.aggregationTypes.sum, cellClass: 'grid-align', footerCellFilter: 'number:0'},
        { name: 'SalesValue', displayName: "Sales Value", width: 150, aggregationType: uiGridConstants.aggregationTypes.sum, cellClass: 'grid-align', footerCellFilter: 'number:2'},
        { name: 'ReturnValue', displayName: "Return Value", width: 150, aggregationType: uiGridConstants.aggregationTypes.sum, cellClass: 'grid-align', footerCellFilter: 'number:2' },
        { name: 'NetImsValue', displayName: "Net IMS Value", width: 150 ,aggregationType: uiGridConstants.aggregationTypes.sum, cellClass: 'grid-align', footerCellFilter: 'number:2' }
    ];
    $scope.gridInvoiceWiseProdSales = {
        showColumnFooter: true,
        //paginationPageSizes: [5, 10, 20],
        //paginationPageSize: 5,
        //enableFullRowSelection: true,
        //multiSelect: false,
        enableFiltering: true,
        enableSorting: true,
        //rowHeight: 25,
        columnDefs: columnInvoiceWiseProdSales,
        enableGridMenu: true,
        enableSelectAll: true,
        exporterCsvFilename: "Invoice_Wise_Product_Sales.csv",
        exporterMenuPdf: false,
        exporterCsvLinkElement: angular.element(document.querySelectorAll(".custom-csv-link-location")),
        onRegisterApi: function (gridApi) {
            $scope.gridApi = gridApi;
        }
    };


    
    //var columnProductGrid = [        
    //    { name: 'ProductCode', displayName: "Product Code"},
    //    { name: 'ProductName', displayName: "Product Name"},
    //    { name: 'PackSize', displayName: "Pack Size"},
    //];
    //$scope.gridProduct = {
    //    enableFiltering: true,
    //    enableSorting: true,
    //    paginationPageSizes: [5, 10, 20],
    //    paginationPageSize: 5,
    //    columnDefs: columnProductGrid,
    //    enableSelectAll: true,
    //};













    $scope.Reset = function () {
        $scope.FromDate = "";
        $scope.ToDate = "";
        $scope.gridInvoiceWiseProdSales.data = [];
        $scope.isDisabled = false;
        $scope.ReportType = "";
        $scope.frmInvoiceWiseProdSales.RepType = undefined;
        $scope.frmInvoiceWiseProdSales.Division = undefined;
        $scope.frmInvoiceWiseProdSales.Region = undefined;
        $scope.frmInvoiceWiseProdSales.Area = undefined;
        $scope.frmInvoiceWiseProdSales.Territory = undefined;
        $scope.frmInvoiceWiseProdSales.Customer = undefined;
        $scope.frmInvoiceWiseProdSales.Product = undefined;
        $scope.Divisions = [];
        $scope.Regions = [];
        $scope.Areas = [];
        $scope.Territories = [];
        $scope.Customers = [];
        $scope.Products = [];
    };

});