app.controller("BonusReportCtrl", function ($scope, $http, uiGridConstants, uiGridExporterService) {
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
                $scope.gridDistSrSales.enableGridMenu = response.data[0].DownLoadStatus;
            }
        }, function (response) {
            if (response.status === 404) {
                toastr.warning("Error Loading Report Download Status!", { timeOut: 2000 });
            }
        });
    };
    $scope.GetReportDownLoadStatus();

    var columnBonusReport = [
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

        { name: 'InvoiceNo', displayName: "Invoice No", width: 200 },
        { name: 'InvoiceDate', displayName: "Invoice Date", width: 200 },
        { name: 'OrderNo', displayName: "Order No", width: 200 },
        { name: 'OrderDate', displayName: "Order Date", width: 200 },
        { name: 'ProductCode', displayName: "Product Code", width: 150 },
        { name: 'ProductName', displayName: "Product Name", width: 150 },
        { name: 'BrandCode', displayName: "Brand Code", width: 150 },
        { name: 'BrandName', displayName: "Brand Name", width: 150 },
        { name: 'CategoryCode', displayName: "Category Code", width: 150 },
        { name: 'CategoryName', displayName: "Category Name", width: 150 },
        { name: 'SkuCode', displayName: "Sku Code", width: 150 },
        { name: 'SkuName', displayName: "Sku Name", width: 150 },
        { name: 'PackSize', displayName: "Pack Size", width: 150 },
        { name: 'ProductPrice', displayName: "Product Price", width: 120, cellClass: 'grid-align', cellFilter: 'number:2' },

        { name: 'BonusPriceDiscount', displayName: "Bonus Price Discount", width: 120, cellClass: 'grid-align', cellFilter: 'number:2', aggregationType: uiGridConstants.aggregationTypes.sum, footerCellFilter: 'number:2'},
        { name: 'RetBonusPriceDiscount', displayName: "Ret Bonus Price Discount", width: 120, cellClass: 'grid-align', cellFilter: 'number:2', aggregationType: uiGridConstants.aggregationTypes.sum, footerCellFilter: 'number:2'},
        { name: 'NetBonusPriceDiscount', displayName: "Net Bonus Price Discount", width: 120, cellClass: 'grid-align', cellFilter: 'number:2', aggregationType: uiGridConstants.aggregationTypes.sum, footerCellFilter: 'number:2'},

        { name: 'BonusQty', displayName: "Bonus Qty", width: 120, cellClass: 'grid-align', cellFilter: 'number:2' },
        { name: 'ReturnBonusQty', displayName: "Return Bonus Qty", width: 120, cellClass: 'grid-align', cellFilter: 'number:2' },
        { name: 'ImsBonusQty', displayName: "IMS Bonus Qty", width: 120, cellClass: 'grid-align', cellFilter: 'number:2' },
        { name: 'ImsBonusValue', displayName: "IMS Bonus Value", width: 120, cellClass: 'grid-align', cellFilter: 'number:2' },

        { name: 'TradeBobusQty', displayName: "Trade Bobus Qty", width: 120, cellClass: 'grid-align', cellFilter: 'number:2' },
        { name: 'RetrunTradeBobusQty', displayName: "Return Trade Bobus Qty", width: 120, cellClass: 'grid-align', cellFilter: 'number:2' },
        { name: 'ImsTradeBobusQty', displayName: "IMS Trade Bobus Qty", width: 120, cellClass: 'grid-align', cellFilter: 'number:2' },
        { name: 'ImsTradeBobusValue', displayName: "IMS Trade Bobus Value", width: 120, cellClass: 'grid-align', cellFilter: 'number:2' },

        { name: 'ComboBobusQty', displayName: "Combo Bobus Qty", width: 120, cellClass: 'grid-align', cellFilter: 'number:2' },
        { name: 'RetrunComboBobusQty', displayName: "Return Combo Bobus Qty", width: 120, cellClass: 'grid-align', cellFilter: 'number:2' },
        { name: 'ImsComboBobusQty', displayName: "IMS Combo Bobus Qty", width: 120, cellClass: 'grid-align', cellFilter: 'number:2' },
        { name: 'ImsComboBobusValue', displayName: "IMS Combo Bobus Value", width: 120, cellClass: 'grid-align', cellFilter: 'number:2' },

        { name: 'DisplayBobusQty', displayName: "Display Bobus Qty", width: 120, cellClass: 'grid-align', cellFilter: 'number:2' },
        { name: 'RetrunDisplayBobusQty', displayName: "Return Display Bobus Qty", width: 120, cellClass: 'grid-align', cellFilter: 'number:2' },
        { name: 'ImsDisplayBobusQty', displayName: "IMS Display Bobus Qty", width: 120, cellClass: 'grid-align', cellFilter: 'number:2' },
        { name: 'ImsDisplayBobusValue', displayName: "IMS Display Bobus Value", width: 120, cellClass: 'grid-align', cellFilter: 'number:2' },

        { name: 'TotalBobusQty', displayName: "Total Bobus Qty", width: 120, cellClass: 'grid-align', cellFilter: 'number:2' },
        { name: 'RetrunTotalBobusQty', displayName: "Return Total Bobus Qty", width: 120, cellClass: 'grid-align', cellFilter: 'number:2' },
        { name: 'ImsTotalBobusQty', displayName: "IMS Total Bobus Qty", width: 120, cellClass: 'grid-align', cellFilter: 'number:2' },
        { name: 'ImsTotalBobusValue', displayName: "IMS Total Bobus Value", width: 120, cellClass: 'grid-align', cellFilter: 'number:2', aggregationType: uiGridConstants.aggregationTypes.sum, footerCellFilter: 'number:2' },
    ];
    $scope.gridBonusReport = {
        showColumnFooter: true,
        enableFiltering: true,
        enableSorting: true,
        columnDefs: columnBonusReport,
        enableGridMenu: true,
        enableSelectAll: true,
        exporterMenuPdf: false,
        exporterCsvLinkElement: angular.element(document.querySelectorAll(".custom-csv-link-location")),
        exporterCsvFilename: 'Bonus_Report.csv',
        onRegisterApi: function (gridApi) {
            $scope.gridApi = gridApi;
        }
    };

    $scope.GetReportTypeList = function () {
        $http({
            method: "GET",
            url: MyApp.rootPath + "DistSrSales/GetReportTypeList",
            params: { SlNo: "(37)" }
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


        if ($scope.frmBonusReport.RepType.ReportTypeValue == "Today" || $scope.frmBonusReport.RepType.ReportTypeValue == "Yesterday" || $scope.frmBonusReport.RepType.ReportTypeValue == "LastSevendays" || $scope.frmBonusReport.RepType.ReportTypeValue == "LastThirtydays" || $scope.frmBonusReport.RepType.ReportTypeValue == "CurrentMonth" || $scope.frmBonusReport.RepType.ReportTypeValue == "LastMonth") {
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

    $scope.GetBonusReport = function () {

          if ($scope.frmBonusReport.RepType.ReportTypeValue == "BonusReport") {
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
                    methodName = "GetBonusReportCustomDate";

                }
            }
        }

        $http({
            method: "POST",
            url: MyApp.rootPath + "BonusReport/" + methodName,
            data: {
                fDate: $scope.FromDate,
                tDate: $scope.ToDate,
                dCode: $scope.frmBonusReport.Division.DivisionCode,
                rCode: $scope.frmBonusReport.Region.RegionCode,
                aCode: $scope.frmBonusReport.Area.AreaCode,
                tCode: $scope.frmBonusReport.Territory.TerritoryCode,
                cCode: $scope.frmBonusReport.Customer.CustomerCode

            }
        }).then(function (response) {
            if (response.data.length > 0) {
                $scope.gridBonusReport.data = response.data;

            }
            else {
                toastr.warning("No Data Found!", '');
                $scope.gridBonusReport.data = [];
            }
        }, function (response) {
            toastr.error("Bonus Report Error!");
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
            params: { dCode: $scope.frmBonusReport.Division.DivisionCode }
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
            params: { dCode: $scope.frmBonusReport.Division.DivisionCode, rCode: $scope.frmBonusReport.Region.RegionCode }
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
            params: { dCode: $scope.frmBonusReport.Division.DivisionCode, rCode: $scope.frmBonusReport.Region.RegionCode, aCode: $scope.frmBonusReport.Area.AreaCode }
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
            params: { dCode: $scope.frmBonusReport.Division.DivisionCode, rCode: $scope.frmBonusReport.Region.RegionCode, aCode: $scope.frmBonusReport.Area.AreaCode, tCode: $scope.frmBonusReport.Territory.TerritoryCode }
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
            params: { dCode: $scope.frmBonusReport.Division.DivisionCode, rCode: $scope.frmBonusReport.Region.RegionCode, aCode: $scope.frmBonusReport.Area.AreaCode, tCode: $scope.frmBonusReport.Territory.TerritoryCode, cCode: $scope.frmBonusReport.Customer.CustomerCode }
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
        $scope.gridBonusReport.data = [];
        $scope.isDisabled = false;
        $scope.ReportType = "";
        $scope.frmBonusReport.Division = undefined;
        $scope.frmBonusReport.Region = undefined;
        $scope.frmBonusReport.Area = undefined;
        $scope.frmBonusReport.Territory = undefined;
        $scope.frmBonusReport.Customer = undefined;

        $scope.frmBonusReport.RepType = undefined;


        $scope.Divisions = [];
        $scope.Regions = [];
        $scope.Areas = [];
        $scope.Territories = [];
        $scope.Customers = [];


    };

});