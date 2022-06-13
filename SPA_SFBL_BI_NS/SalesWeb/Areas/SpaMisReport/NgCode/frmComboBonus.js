app.controller("ComboBonusCtrl", function ($scope, $http, uiGridConstants, uiGridExporterService) {

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
                $scope.gridComboBonus.enableGridMenu = response.data[0].DownLoadStatus;
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

        if ($scope.frmComboBonus.RepType.ReportTypeValue == "Today" ) {

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

    var columnComboBonus = [
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
        { name: 'InvoiceDate', displayName: "Invoice Date", width: 200 },
        { name: 'InvoiceNo', displayName: "Invoice No", width: 200, cellClass: 'grid-align' },
        { name: 'ComboBonusName', displayName: "Combo Bonus Name", width: 200, cellClass: 'grid-align' },
        { name: 'ComboBonusNo', displayName: "Combo Bonus No", width: 300 },
        { name: 'ProductCode', displayName: "Product Code", width: 200},
        { name: 'ProductName', displayName: "Product Name", width: 200},
        { name: 'PackSize', displayName: "PackSize", width: 200},
        { name: 'SalesQty', displayName: "Sales Qty", width: 200, cellClass: 'grid-align' },
        { name: 'ReturnSalesQty', displayName: "Return Sales Qty", width: 200, cellClass: 'grid-align' },
        { name: 'NetImsQty', displayName: "Net Ims Qty", width: 200, cellClass: 'grid-align'},
        { name: 'SalesValue', displayName: "Sales Value", width: 200, cellClass: 'grid-align', cellFilter: 'number:2', aggregationType: uiGridConstants.aggregationTypes.sum, footerCellFilter: 'number:2'  },
        { name: 'ReturnSalesValue', displayName: "Return Sales Value", width: 200, cellClass: 'grid-align', cellFilter: 'number:2', aggregationType: uiGridConstants.aggregationTypes.sum, footerCellFilter: 'number:2' },
        { name: 'NetImsValue', displayName: "Net Ims Value", width: 200, cellClass: 'grid-align', cellFilter: 'number:2', aggregationType: uiGridConstants.aggregationTypes.sum, footerCellFilter: 'number:2'  },
        { name: 'ComboBonusQty', displayName: "Combo Bonus Qty", width: 200, cellClass: 'grid-align' },
        { name: 'ReturnComboBonusQty', displayName: "Return Combo Bonus Qty", width: 200, cellClass: 'grid-align' },
        { name: 'NetComboBonusQty', displayName: "Net Combo Bonus Qty", width: 200, cellClass: 'grid-align' },
    ];


    $scope.gridComboBonus = {
        showColumnFooter: true,
        enableFiltering: true,
        enableSorting: true,
        columnDefs: columnComboBonus,
        enableGridMenu: true,
        enableSelectAll: true,
        exporterCsvFilename: 'Combo_Bonus_Report.csv',
        exporterMenuPdf: false,
        exporterCsvLinkElement: angular.element(document.querySelectorAll(".custom-csv-link-location")),
        onRegisterApi: function (gridApi) {
            $scope.gridApi = gridApi;
        }
    };



    $scope.GetComboBonus = function () {

        if ($scope.frmComboBonus.RepType.ReportTypeValue == "CustomDate") {

            methodName = "GetComboBonusCustomDate";

        }

        $http({
            method: "POST",
            url: MyApp.rootPath + "ComboBonus/" + methodName,
            data: {
                fDate: $scope.FromDate,
                tDate: $scope.ToDate,
                dCode: $scope.frmComboBonus.Division.DivisionCode,
                rCode: $scope.frmComboBonus.Region.RegionCode,
                aCode: $scope.frmComboBonus.Area.AreaCode,
                tCode: $scope.frmComboBonus.Territory.TerritoryCode,
                cCode: $scope.frmComboBonus.Customer.CustomerCode,
                cbNo: $scope.frmComboBonus.ComboBonus.ComboBonusNo,


            }
        }).then(function (response) {

            if (response.data.length > 0) {
                $scope.gridComboBonus.data = response.data;
            }
            else {
                toastr.warning("No Data Found!", '');
                $scope.gridComboBonus.data = [];
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
            params: { dCode: $scope.frmComboBonus.Division.DivisionCode }
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
            params: { dCode: $scope.frmComboBonus.Division.DivisionCode, rCode: $scope.frmComboBonus.Region.RegionCode }
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
            params: { dCode: $scope.frmComboBonus.Division.DivisionCode, rCode: $scope.frmComboBonus.Region.RegionCode, aCode: $scope.frmComboBonus.Area.AreaCode }
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
            params: { dCode: $scope.frmComboBonus.Division.DivisionCode, rCode: $scope.frmComboBonus.Region.RegionCode, aCode: $scope.frmComboBonus.Area.AreaCode, tCode: $scope.frmComboBonus.Territory.TerritoryCode }
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

    $scope.GetComboBonusList = function () {
        $http({
            method: "GET",
            url: MyApp.rootPath + "ComboBonus/GetComboBonusList"
        }).then(function (response) {
            if (response.data.Status === "" || response.data.Status === null) {
                if (response.data.Data.length > 0) {
                    $scope.combobonuses = response.data.Data;
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

    $scope.GetComboBonusList();


    $scope.Reset = function () {
        $scope.FromDate = "";
        $scope.ToDate = "";
        $scope.isDisabled = true;

        $scope.frmComboBonus.RepType = undefined;
        $scope.frmComboBonus.Division = undefined;
        $scope.frmComboBonus.Region = undefined;
        $scope.frmComboBonus.Area = undefined;
        $scope.frmComboBonus.Territory = undefined;
        $scope.frmComboBonus.Customer = undefined;
        $scope.frmComboBonus.ComboBonus = undefined;
        $scope.Divisions = [];
        $scope.Regions = [];
        $scope.Areas = [];
        $scope.Territories = [];
        $scope.Customers = [];
        $scope.gridComboBonus.data = [];
    };

});