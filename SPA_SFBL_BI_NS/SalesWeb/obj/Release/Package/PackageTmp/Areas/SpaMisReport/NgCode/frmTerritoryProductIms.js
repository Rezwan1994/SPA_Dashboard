app.controller("TerritoryProductImsCtrl", function ($scope, $http, uiGridConstants) {

    var xl_file_name = "";
    var methodName = "";
    $scope.EventPerm(22);
    $scope.isDisabled = true;
    $scope.LocationType = "ALL";

    $scope.GetReportDownLoadStatus = function () {
        $http({
            method: "GET",
            url: MyApp.rootPath + "RetailerIms/GetReportDownLoadStatus",
            datatype: "json",
            params: { url: window.location.pathname }
        }).then(function (response) {
            if (response.data.length > 0) {
                $scope.gridTerritoryProductIms.enableGridMenu = response.data[0].DownLoadStatus;
            }
        }, function (response) {
            if (response.status === 404) {
                toastr.warning("Error Loading Report Download Status!", { timeOut: 2000 });
            }
        });
    };
    $scope.GetReportDownLoadStatus();


    var columnTerritoryProductIms = [
        { name: 'DivisionCode', displayName: "Division Code", width: 150, aggregationType: uiGridConstants.aggregationTypes.count, footerCellFilter: 'number:0' },
        { name: 'DivisionName', displayName: "Division Name", width: 150 },
        { name: 'AreaCode', displayName: "Area Code", width: 150 },
        { name: 'AreaName', displayName: "Area Name", width: 150 },
        { name: 'TerritoryCode', displayName: "Territory Code", width: 150 },
        { name: 'TerritoryName', displayName: "Territory Name", width: 150 },
        { name: 'ProductCode', displayName: "Product Code", width: 150 },
        { name: 'ProductName', displayName: "Product Name", width: 150 },
        { name: 'PackSize', displayName: "Pack Size", width: 150 },
        { name: 'ImsSalesQty', displayName: "IMS Sales Qty", width: 120, cellFilter: 'number:2', cellClass: 'grid-align', footerCellFilter: 'number:2', aggregationType: uiGridConstants.aggregationTypes.sum },
        { name: 'TargetQty', displayName: "Target Qty", width: 120, cellFilter: 'number:2', cellClass: 'grid-align', footerCellFilter: 'number:2', aggregationType: uiGridConstants.aggregationTypes.sum },
        { name: 'LastYearAsOnDateSalesQty', displayName: "Last Year As On Date Sales Qty", width: 120, cellFilter: 'number:2', cellClass: 'grid-align', footerCellFilter: 'number:2', aggregationType: uiGridConstants.aggregationTypes.sum },
        { name: 'Achievement', displayName: "Achievement", width: 120, cellFilter: 'number:2', cellClass: 'grid-align', footerCellFilter: 'number:2', aggregationType: uiGridConstants.aggregationTypes.sum },
        { name: 'Growth', displayName: "Growth", width: 120, cellFilter: 'number:2', cellClass: 'grid-align', footerCellFilter: 'number:2', aggregationType: uiGridConstants.aggregationTypes.sum },
    ];
    $scope.gridTerritoryProductIms = {
        showColumnFooter: true,
        enableFiltering: true,
        enableSorting: true,
        columnDefs: columnTerritoryProductIms,
        enableGridMenu: true,
        enableSelectAll: true,
        exporterCsvFilename: "Territory_Product_IMS.csv",
        exporterMenuPdf: false,
        exporterCsvLinkElement: angular.element(document.querySelectorAll(".custom-csv-link-location")),
        onRegisterApi: function (gridApi) {
            $scope.gridApi = gridApi;
        }
    };

    $scope.OnReportTypeChange = function () {
        if ($scope.frmTerritoryProductIms.RepType.ReportTypeValue == "Today" || $scope.frmTerritoryProductIms.RepType.ReportTypeValue == "Yesterday" || $scope.frmTerritoryProductIms.RepType.ReportTypeValue == "LastSevendays" || $scope.frmTerritoryProductIms.RepType.ReportTypeValue == "LastThirtydays" || $scope.frmTerritoryProductIms.RepType.ReportTypeValue == "CurrentMonth" || $scope.frmTerritoryProductIms.RepType.ReportTypeValue == "LastMonth" || $scope.frmTerritoryProductIms.RepType.ReportTypeValue == "MonthOnMonthCy" || $scope.frmTerritoryProductIms.RepType.ReportTypeValue == "MonthOnMonthLy") {
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
            url: MyApp.rootPath + "DistSrSales/GetAreaList",
            params: { dCode: $scope.frmTerritoryProductIms.Division.DivisionCode, rCode: "ALL" }
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
            params: { dCode: $scope.frmTerritoryProductIms.Division.DivisionCode, rCode: "ALL", aCode: $scope.frmTerritoryProductIms.Area.AreaCode }
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

    $scope.GetTerritoryProductIms = function () {


        if ($scope.frmTerritoryProductIms.RepType.ReportTypeValue == "CustomDate") {
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
                    methodName = "GetTerritoryProductImsDateRange";
                }
            }
        }

        $http({
            method: "POST",
            url: MyApp.rootPath + "TerritoryProductIms/" + methodName,
            data: {
                fDate: $scope.FromDate,
                tDate: $scope.ToDate,
                dCode: $scope.frmTerritoryProductIms.Division.DivisionCode,
                aCode: $scope.frmTerritoryProductIms.Area.AreaCode,
                tCode: $scope.frmTerritoryProductIms.Territory.TerritoryCode
            }
        }).then(function (response) {
            if (response.data.length > 0) {
                $scope.gridTerritoryProductIms.data = response.data;

            }
            else {
                toastr.warning("No Data Found!", '');
                $scope.gridDivisionProductIms.data = [];
            }
        }, function () {
            alert(response);
            toastr.error("Error!");
        });
    };


    $scope.Reset = function () {
        $scope.FromDate = "";
        $scope.ToDate = "";
        $scope.gridTerritoryProductIms.data = [];
        $scope.isDisabled = false;
        $scope.ReportType = "";
        $scope.frmTerritoryProductIms.RepType = undefined;
        $scope.frmTerritoryProductIms.Division = undefined;
        $scope.frmTerritoryProductIms.Area = undefined;
        $scope.frmTerritoryProductIms.Territory = undefined;

        $scope.Divisions = [];
        $scope.Areas = [];
        $scope.Territories = [];

        $scope.isDisabled = true;
        $scope.LocationType = "ALL";
    };

});