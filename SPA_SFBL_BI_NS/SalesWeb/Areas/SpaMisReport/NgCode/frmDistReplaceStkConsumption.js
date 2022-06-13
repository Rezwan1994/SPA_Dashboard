app.controller("DistReplaceStkConsumptionCtrl", function ($scope, $http, uiGridConstants) {

    var methodName = "";
    $scope.EventPerm(22);
    $scope.isDisabled = true;

    $scope.GetReportDownLoadStatus = function () {
        $http({
            method: "GET",
            url: MyApp.rootPath + "RetailerIms/GetReportDownLoadStatus",
            datatype: "json"
        }).then(function (response) {
            if (response.data.length > 0) {
                $scope.gridDistReplaceStkConsumption.enableGridMenu = response.data[0].DownLoadStatus;
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

    //-------------Grid------------------//

    var columnDistReplaceStkConsumption = [
        //{ name: 'SlNo', displayName: "Sl. No", type: 'number', width: 100, cellClass: 'grid-align', footerCellFilter: 'number:2' },
        { name: 'DivisionCode', displayName: "Division Code", width: 150, aggregationType: uiGridConstants.aggregationTypes.count, footerCellFilter: 'number:0'},
        { name: 'DivisionName', displayName: "Division Name", width: 150 },
        { name: 'RegionCode', displayName: "Region Code", width: 150 },
        { name: 'RegionName', displayName: "Region Name", width: 150 },
        { name: 'AreaCode', displayName: "Area Code", width: 150 },
        { name: 'AreaName', displayName: "Area Name", width: 150 },
        { name: 'TerritoryCode', displayName: "Territory Code", width: 150 },
        { name: 'TerritoryName', displayName: "Territory Name", width: 150 },
        { name: 'DBLocation', displayName: "DB Loaction", width: 150 },
        { name: 'CustomerCode', displayName: "Distributor Code", width: 150 },
        { name: 'CustomerName', displayName: "Distributor Name", width: 150 },
        { name: 'Address', displayName: "Distributor Address", width: 150 },
        { name: 'ProductCode', displayName: "Product Code", width: 150 },
        { name: 'ProductName', displayName: "Product Name", width: 200 },
        { name: 'PackSize', displayName: "Pack Size", width: 150 },
        { name: 'ProductPrice', displayName: "Product Price", width: 120, cellFilter: 'number:2', cellClass: 'grid-align', footerCellFilter: 'number:2' },
        { name: 'OpeningReplaceQty', displayName: "Opening Replace Qty", width: 120, cellClass: 'grid-align', footerCellFilter: 'number:2' },
        { name: 'ReplaceRecvRetQty', displayName: "Replace Ret. Recv. Qty", width: 120, cellClass: 'grid-align', footerCellFilter: 'number:2' },
        { name: 'TotalQty', displayName: "Total Qty", width: 120, cellClass: 'grid-align', footerCellFilter: 'number:2' },
        { name: 'ReplaceReturnQty', displayName: "Replace Ret.Return Qty", width: 120, cellClass: 'grid-align', footerCellFilter: 'number:2' },
        { name: 'ReplaceFactoryQty', displayName: "Replace Fac. Claim Qty", width: 120, cellClass: 'grid-align', footerCellFilter: 'number:2' },
        { name: 'TotalDeductQty', displayName: "Total Deduct Qty", width: 120, cellClass: 'grid-align', footerCellFilter: 'number:2' },
        { name: 'ClosingReplaceQty', displayName: "Closing Replace Qty", width: 120, cellClass: 'grid-align', footerCellFilter: 'number:2' }

       
        
    ];
    $scope.gridDistReplaceStkConsumption = {
        showColumnFooter: true,
        enableFiltering: true,
        enableSorting: true,
        columnDefs: columnDistReplaceStkConsumption,
        //rowTemplate: rowTemplate(),
        enableGridMenu: true,
        enableSelectAll: true,
        exporterCsvFilename: 'Distributor_Wise_Replace_Stock_Consumption.csv',
        exporterMenuPdf: false,
        exporterCsvLinkElement: angular.element(document.querySelectorAll(".custom-csv-link-location")),
        onRegisterApi: function (gridApi) {
            $scope.gridApi = gridApi;
        }
    };


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
        if ($scope.frmDistReplaceStkConsumption.RepType.ReportTypeValue == "CurrentMonth") {
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







    $scope.GetDistReplaceStkConsumption = function () {

        methodName = "";
        if ($scope.frmDistReplaceStkConsumption.RepType.ReportTypeValue == "CurrentMonth") {

            methodName = "GetDistReplaceStkConsumption";

        }


        $http({
            method: "POST",
            url: MyApp.rootPath + "DistReplaceStkConsumption/" + methodName,
            data: {

                dCode: $scope.frmDistReplaceStkConsumption.Division.DivisionCode,
                rCode: $scope.frmDistReplaceStkConsumption.Region.RegionCode,
                aCode: $scope.frmDistReplaceStkConsumption.Area.AreaCode,
                tCode: $scope.frmDistReplaceStkConsumption.Territory.TerritoryCode,
                cCode: $scope.frmDistReplaceStkConsumption.Customer.CustomerCode

            }
        }).then(function (response) {
            if (response.data.length > 0) {
                $scope.gridDistReplaceStkConsumption.data = response.data;

            }
            else {
                toastr.warning("No Data Found!", '');
                $scope.gridDistReplaceStkConsumption.data = [];
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
            params: { dCode: $scope.frmDistReplaceStkConsumption.Division.DivisionCode }
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
            params: { dCode: $scope.frmDistReplaceStkConsumption.Division.DivisionCode, rCode: $scope.frmDistReplaceStkConsumption.Region.RegionCode }
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
            params: { dCode: $scope.frmDistReplaceStkConsumption.Division.DivisionCode, rCode: $scope.frmDistReplaceStkConsumption.Region.RegionCode, aCode: $scope.frmDistReplaceStkConsumption.Area.AreaCode }
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
            params: { dCode: $scope.frmDistReplaceStkConsumption.Division.DivisionCode, rCode: $scope.frmDistReplaceStkConsumption.Region.RegionCode, aCode: $scope.frmDistReplaceStkConsumption.Area.AreaCode, tCode: $scope.frmDistReplaceStkConsumption.Territory.TerritoryCode }
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
        $scope.gridDistReplaceStkConsumption.data = [];
        $scope.isDisabled = false;
        $scope.ReportType = "";
        $scope.frmDistReplaceStkConsumption.Division = undefined;
        $scope.frmDistReplaceStkConsumption.Region = undefined;
        $scope.frmDistReplaceStkConsumption.Area = undefined;
        $scope.frmDistReplaceStkConsumption.Territory = undefined;
        $scope.frmDistReplaceStkConsumption.Customer = undefined;


        $scope.Divisions = [];
        $scope.Regions = [];
        $scope.Areas = [];
        $scope.Territories = [];
        $scope.Customers = [];
        $scope.Srs = [];

    };

});