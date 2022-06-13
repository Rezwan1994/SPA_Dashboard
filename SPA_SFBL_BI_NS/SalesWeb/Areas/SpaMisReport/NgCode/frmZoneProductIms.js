app.controller("ZoneProductImsCtrl", function ($scope, $http, uiGridConstants) {

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
                $scope.gridDivisionProductIms.enableGridMenu = response.data[0].DownLoadStatus;
            }
        }, function (response) {
            if (response.status === 404) {
                toastr.warning("Error Loading Report Download Status!", { timeOut: 2000 });
            }
        });
    };
    $scope.GetReportDownLoadStatus();


    var columnDivisionProductIms = [      
        { name: 'DivisionCode', displayName: "Division Code", width: 150, aggregationType: uiGridConstants.aggregationTypes.count, footerCellFilter: 'number:0' },
        { name: 'DivisionName', displayName: "Division Name", width: 150 },        
        { name: 'ProductCode', displayName: "Product Code", width: 150 },
        { name: 'ProductName', displayName: "Product Name", width: 150 },
        { name: 'PackSize', displayName: "Pack Size", width: 150 },
        { name: 'ImsSalesQty', displayName: "IMS Sales Qty", width: 120, cellFilter: 'number:2', cellClass: 'grid-align', footerCellFilter: 'number:2', aggregationType: uiGridConstants.aggregationTypes.sum },
        { name: 'TargetQty', displayName: "Target Qty", width: 120, cellFilter: 'number:2', cellClass: 'grid-align', footerCellFilter: 'number:2', aggregationType: uiGridConstants.aggregationTypes.sum },
        { name: 'LastYearAsOnDateSalesQty', displayName: "Last Year As On Date Sales Qty", width: 120, cellFilter: 'number:2', cellClass: 'grid-align', footerCellFilter: 'number:2', aggregationType: uiGridConstants.aggregationTypes.sum },
        { name: 'Achievement', displayName: "Achievement", width: 120, cellFilter: 'number:2', cellClass: 'grid-align', footerCellFilter: 'number:2', aggregationType: uiGridConstants.aggregationTypes.sum },
        { name: 'Growth', displayName: "Growth", width: 120, cellFilter: 'number:2', cellClass: 'grid-align', footerCellFilter: 'number:2', aggregationType: uiGridConstants.aggregationTypes.sum },
 
    ];
    $scope.gridDivisionProductIms = {
        showColumnFooter: true,
        enableFiltering: true,
        enableSorting: true,
        columnDefs: columnDivisionProductIms,
        enableGridMenu: true,
        enableSelectAll: true,
        exporterCsvFilename: "Division_Product_IMS.csv",
        exporterMenuPdf: false,
        exporterCsvLinkElement: angular.element(document.querySelectorAll(".custom-csv-link-location")),
        onRegisterApi: function (gridApi) {
            $scope.gridApi = gridApi;
        }
    };

    $scope.OnReportTypeChange = function () {
        if ($scope.frmZoneProductIms.RepType.ReportTypeValue == "Today" || $scope.frmZoneProductIms.RepType.ReportTypeValue == "Yesterday" || $scope.frmZoneProductIms.RepType.ReportTypeValue == "LastSevendays" || $scope.frmZoneProductIms.RepType.ReportTypeValue == "LastThirtydays" || $scope.frmZoneProductIms.RepType.ReportTypeValue == "CurrentMonth" || $scope.frmZoneProductIms.RepType.ReportTypeValue == "LastMonth" || $scope.frmZoneProductIms.RepType.ReportTypeValue == "MonthOnMonthCy" || $scope.frmZoneProductIms.RepType.ReportTypeValue == "MonthOnMonthLy") {
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

    $scope.GetZoneProductIms = function () {

        
        if ($scope.frmZoneProductIms.RepType.ReportTypeValue == "CustomDate") {
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
                    methodName = "GetZoneProductImsDateRange";
                }
            }
        }

        $http({
            method: "POST",
            url: MyApp.rootPath + "ZoneProductIms/" + methodName,
            data: {
                fDate: $scope.FromDate,
                tDate: $scope.ToDate,
                dCode: $scope.frmZoneProductIms.Division.DivisionCode
            }
        }).then(function (response) {
            if (response.data.length > 0) {
                $scope.gridDivisionProductIms.data = response.data;

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
        $scope.gridDivisionMarketIms.data = [];
        $scope.isDisabled = false;
        $scope.ReportType = "";
        $scope.frmZoneProductIms.RepType = undefined;
        $scope.frmZoneProductIms.Division = undefined;
        $scope.Divisions = [];
        $scope.isDisabled = true;


        $scope.LocationType = "ALL";
    };

});