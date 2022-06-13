app.controller("ReportViewTrackingCtrl", function ($scope, $http, uiGridConstants) {


    $scope.GetUserList = function () {
        $http({
            method: "GET",
            url: MyApp.rootPath + "ReportViewTracking/GetUserList",
        }).then(function (response) {
            if (response.data.Data.length > 0) {
                $scope.users = response.data.Data;
            } else {
                toastr.warning("No Data Found!", { timeOut: 2000 });
            }
        }, function (response) {
            if (response.status === 404) {
                toastr.warning("Error Loading User List!", { timeOut: 2000 });
            }
        });
    }
    $scope.GetUserList();


    var columnReportViewTracking = [
        { name: 'SlNo', displayName: "Sl No", width: 70},
        { name: 'UserId', displayName: "User Id", visible: false },
        { name: 'UserName', displayName: "User Name", width: 100},

        { name: 'EmployeeId', displayName: "Employee Id", visible: false },
        { name: 'EmployeeName', displayName: "Employee Name" },

        { name: 'ReportName', displayName: "Report Name",width:300},

        { name: 'ReportViewDate', displayName: "Date"},
        { name: 'ReportViewTerminal', displayName: "Terminal", visible: false},
        { name: 'ReportViewIp', displayName: "IP" }
    ];
    $scope.gridReportViewTracking = {
        showGridFooter: true,
        enableFiltering: true,
        enableSorting: true,
        enableGridMenu: true,
        exporterMenuPdf: false,
        columnDefs: columnReportViewTracking,
        exporterCsvFilename: 'Report_View_Tracking.csv',        
        exporterCsvLinkElement: angular.element(document.querySelectorAll(".custom-csv-link-location")),
        onRegisterApi: function (gridApi) {
            $scope.gridApi = gridApi;
        }
    };



    $scope.GetUserViewReportList = function () {

        $http({
            method: "POST",
            url: MyApp.rootPath + "ReportViewTracking/GetUserViewReportList",
            params: {
                userId: $scope.frmReportViewTracking.User.UserId,                 
                fDate: $scope.FromDate,
                tDate: $scope.ToDate,
            }
        }).then(function (response) {
            if (response.data.length > 0) {
                $scope.gridReportViewTracking.data = response.data;
            } else {
                $scope.gridReportViewTracking.data = [];
            }
        }, function (response) {
            if (response.status === 404) {
                toastr.warning("Error Loading User Report List!", { timeOut: 2000 });
            }
        });

    }



    $scope.Reset = function () {

        $scope.frmReportViewTracking.User = undefined;
        $scope.gridReportViewTracking.data = [];
        $scope.FromDate = "";
        $scope.ToDate = "";
    }

});