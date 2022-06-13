app.controller("DistStockConsumptionCtrl", function ($scope, $http, uiGridConstants) {

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
                $scope.gridDistStockConsumption.enableGridMenu = response.data[0].DownLoadStatus;
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
        if ($scope.frmDistStockConsumption.RepType.ReportTypeValue == "CurrentMonth") {
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
  
    var columnDistStockConsumption = [
        //{ name: 'SlNo', displayName: "Sl. No", type: 'number', width: 100, cellClass: 'grid-align', footerCellFilter: 'number:2' },
        { name: 'DivisionCode', displayName: "Division Code", width: 150, aggregationType: uiGridConstants.aggregationTypes.count, footerCellFilter: 'number:0' },
        { name: 'DivisionName', displayName: "Division Name", width: 150 },
        { name: 'RegionCode', displayName: "Region Code", width: 150 },
        { name: 'RegionName', displayName: "Region Name", width: 150 },
        { name: 'AreaCode', displayName: "Area Code", width: 150 },
        { name: 'AreaName', displayName: "Area Name", width: 150 },
        { name: 'TerritoryCode', displayName: "Territory Code", width: 150 },
        { name: 'TerritoryName', displayName: "Territory Name", width: 150 },
        { name: 'DBLocation', displayName: "DB Loaction", width: 150 },
        { name: 'DistributorCode', displayName: "Distributor Code", width: 150 },
        { name: 'DistributorName', displayName: "Distributor Name", width: 150 },
        { name: 'DistributorAdd', displayName: "Distributor Address", width: 150 },
        { name: 'ProductCode', displayName: "Product Code", width: 150 },
        { name: 'ProductName', displayName: "Product Name", width: 200 },
        { name: 'PackSize', displayName: "Pack Size", width: 150 },
        { name: 'ProductPrice', displayName: "Product Price", width: 120, cellFilter: 'number:2', cellClass: 'grid-align', footerCellFilter: 'number:2' },
        { name: 'OpeningQty', displayName: "Opening Qty", width: 120, cellClass: 'grid-align', footerCellFilter: 'number:2' },
        { name: 'ReceiveQty', displayName: "'Receive Qty'", width: 120, cellClass: 'grid-align', footerCellFilter: 'number:2' },
        { name: 'ReplaceRcvQty', displayName: "Replace Rcv.Qty", width: 120,  cellClass: 'grid-align', footerCellFilter: 'number:2' },
        { name: 'PreviousReturnReceiveQty', displayName: "Previous Return Receive Qty", width: 120, cellClass: 'grid-align', footerCellFilter: 'number:2' },
        { name: 'ReturnReceiveQty', displayName: "Return Receive Qty", width: 120, cellClass: 'grid-align', footerCellFilter: 'number:2' },
        { name: 'PrevReplaceRetReceiveQty', displayName: "Prev Replace Ret Receive Qty", width: 150,   cellClass: 'grid-align', footerCellFilter: 'number:2' },
        { name: 'ReplaceRetReceiveQty', displayName: "Replace Ret Receive Qty", width: 120,  cellClass: 'grid-align', footerCellFilter: 'number:2' },
        { name: 'GainQty', displayName: "Gain Qty", width: 120, cellClass: 'grid-align', footerCellFilter: 'number:2' },
        { name: 'TotalInQty', displayName: "Total In Qty", width: 120, cellClass: 'grid-align', footerCellFilter: 'number:2' },
        { name: 'IssuedQty', displayName: "Issued Qty", width: 120,   cellClass: 'grid-align', footerCellFilter: 'number:2' },
        { name: 'ReplaceIssueQty', displayName: "Replace Issue Qty", width: 120,   cellClass: 'grid-align', footerCellFilter: 'number:2' },
        { name: 'DispatchQty', displayName: "Dispatch Qty", width: 120,   cellClass: 'grid-align', footerCellFilter: 'number:2' },
        { name: 'BonusQty', displayName: "Bonus Qty", width: 120,  cellClass: 'grid-align', footerCellFilter: 'number:2' },
        { name: 'TradeBonusQty', displayName: "Trade Bonus Qty", width: 120,  cellClass: 'grid-align', footerCellFilter: 'number:2' },
        { name: 'ComboBonusQty', displayName: "Combo Bonus Qty'", width: 120,  cellClass: 'grid-align', footerCellFilter: 'number:2' },
        { name: 'LossQty', displayName: "Loss Qty", width: 120,  cellClass: 'grid-align', footerCellFilter: 'number:2' },
        { name: 'RequiReturnQty', displayName: "Requi Return Qty", width: 120,  cellClass: 'grid-align', footerCellFilter: 'number:2' },
        { name: 'DamageStockTransferQty', displayName: "Damage Stock Transfer Qty", width: 120,  cellClass: 'grid-align', footerCellFilter: 'number:2' },
        { name: 'TotalOutQty', displayName: "Total Out Qty", width: 120,  cellClass: 'grid-align', footerCellFilter: 'number:2' },
        { name: 'ClosingQty', displayName: "Closing Qty", width: 120, cellClass: 'grid-align', footerCellFilter: 'number:2' },
        { name: 'ClosingValue', displayName: "Closing Value", width: 120, cellFilter: 'number:2', cellClass: 'grid-align', aggregationType: uiGridConstants.aggregationTypes.sum, footerCellFilter: 'number:2' },
        { name: 'TargetQty', displayName: "Target Qty", width: 120, cellClass: 'grid-align', footerCellFilter: 'number:2' },
        { name: 'TargetVal', displayName: "Target Value", width: 120, cellFilter: 'number:2', cellClass: 'grid-align', aggregationType: uiGridConstants.aggregationTypes.sum, footerCellFilter: 'number:2' }
    ];
    $scope.gridDistStockConsumption = {
        showColumnFooter: true,
        enableFiltering: true,
        enableSorting: true,
        columnDefs: columnDistStockConsumption,
        //rowTemplate: rowTemplate(),
        enableGridMenu: true,
        enableSelectAll: true,
        exporterCsvFilename: 'Distributor_Wise_Stock_Consumption.csv',
        exporterMenuPdf: false,
        exporterCsvLinkElement: angular.element(document.querySelectorAll(".custom-csv-link-location")),
        onRegisterApi: function (gridApi) {
            $scope.gridApi = gridApi;
        }
    };    










    $scope.GetDistStockConsumption = function () {

        methodName = "";

        if ($scope.frmDistStockConsumption.RepType.ReportTypeValue == "CurrentMonth") {

            methodName = "GetDistStockConsumption";

        }

        $http({
            method: "POST",
            url: MyApp.rootPath + "DistStockConsumption/" + methodName,
            data: {

                dCode: $scope.frmDistStockConsumption.Division.DivisionCode,
                rCode: $scope.frmDistStockConsumption.Region.RegionCode,
                aCode: $scope.frmDistStockConsumption.Area.AreaCode,
                tCode: $scope.frmDistStockConsumption.Territory.TerritoryCode,
                cCode: $scope.frmDistStockConsumption.Customer.CustomerCode

            }
        }).then(function (response) {
            if (response.data.length > 0) {
                $scope.gridDistStockConsumption.data = response.data;

            }
            else {
                toastr.warning("No Data Found!", '');
                $scope.gridDistStockConsumption.data = [];
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
            params: { dCode: $scope.frmDistStockConsumption.Division.DivisionCode }
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
            params: { dCode: $scope.frmDistStockConsumption.Division.DivisionCode, rCode: $scope.frmDistStockConsumption.Region.RegionCode }
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
            params: { dCode: $scope.frmDistStockConsumption.Division.DivisionCode, rCode: $scope.frmDistStockConsumption.Region.RegionCode, aCode: $scope.frmDistStockConsumption.Area.AreaCode }
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
            params: { dCode: $scope.frmDistStockConsumption.Division.DivisionCode, rCode: $scope.frmDistStockConsumption.Region.RegionCode, aCode: $scope.frmDistStockConsumption.Area.AreaCode, tCode: $scope.frmDistStockConsumption.Territory.TerritoryCode }
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
        $scope.gridDistStockConsumption.data = [];
        $scope.isDisabled = false;
        $scope.ReportType = "";
        $scope.frmDistStockConsumption.Division = undefined;
        $scope.frmDistStockConsumption.Region = undefined;
        $scope.frmDistStockConsumption.Area = undefined;
        $scope.frmDistStockConsumption.Territory = undefined;
        $scope.frmDistStockConsumption.Customer = undefined;
      

        $scope.Divisions = [];
        $scope.Regions = [];
        $scope.Areas = [];
        $scope.Territories = [];
        $scope.Customers = [];
        $scope.Srs = [];

    };

});