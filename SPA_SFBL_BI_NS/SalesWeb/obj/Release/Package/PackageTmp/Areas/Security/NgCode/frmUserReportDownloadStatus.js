app.controller("UserReportDownloadStatusCtrl", function ($scope, $http, uiGridConstants) {


    $scope.GetUserList = function () {
        $http({
            method: "GET",
            url: MyApp.rootPath + "UserBaseReportAccess/GetUserList",
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


    var columnUserReportDownloadStatus = [
        { name: 'UserId', displayName: "User Id", width: 80 },
        { name: 'MenuId', displayName: "Menu Id", width:80 },
        { name: 'ReportName', displayName: "Report Name"},
        { name: 'ReportDisplayName', displayName: "Report Display Name" },
        { name: 'DownloadStatus', displayName: "Download Status" },

        {
            name: 'ChangeDownloadStatus ', enableFiltering: false, enableSorting: false,
            cellTemplate: '<div style="text-align:center;position: relative;width:100%;padding:2px 2px 2px 6px;"><button  class=" btn-danger " ng-click="grid.appScope.InsertUpdateReportDownloadStatus(row)"><i class="fa fa-refresh"></i>&nbspChange Status</button></div>'
        }



    ];
    $scope.gridUserReportDownloadStatus = {
        showGridFooter: true,
        enableFiltering: true,
        enableSorting: true,
        columnDefs: columnUserReportDownloadStatus
    };


    $scope.InsertUpdateReportDownloadStatus = function (row) {

        $http({
            method: "POST",
            url: MyApp.rootPath + "UserReportDownloadStatus/InsertUpdateReportDownloadStatus",
            params: {
                pUserId: row.entity.UserId,
                pMenuId: row.entity.MenuId,
                pReportName: row.entity.ReportName,
                pDownloadStatus: row.entity.DownloadStatus
            }
        }).then(function (response) {
            if (response.status === 200) {
                $scope.OnUserClick();
                toastr.success(response.data.Message, { timeOut: 2000 });
            } else {
                toastr.success(response.data.status, { timeOut: 2000 });
            }
        }, function (response) {
            if (response.status === 404) {
                toastr.warning("Error Change Status!", { timeOut: 2000 });
            }
        });

    };

    $scope.OnUserClick = function () {

        $http({
            method: "POST",
            url: MyApp.rootPath + "UserReportDownloadStatus/GetUserReportList",
            params: { userId: $scope.frmUserReportDownloadStatus.User.UserId }
        }).then(function (response) {
            if (response.data.length > 0) {
                $scope.gridUserReportDownloadStatus.data = response.data;
            } else {
                $scope.gridUserReportDownloadStatus.data = [];
            }
        }, function (response) {
            if (response.status === 404) {
                toastr.warning("Error Loading User Report List!", { timeOut: 2000 });
            }
        });  

    }



    $scope.Reset = function () {

        $scope.frmUserReportDownloadStatus.User = undefined;
        $scope.gridUserReportDownloadStatus.data = [];
    }

});