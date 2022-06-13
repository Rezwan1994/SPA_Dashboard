app.controller("MaterializedViewRefreshCtrl", function ($scope, $http, uiGridConstants) {



    var columnMaterializedViewRefresh = [

        { name: 'MaterializedViewName', displayName: "Materialized View Name",width:300 },
        { name: 'RunDate', displayName: "Run Date" },
        { name: 'RunDuration', displayName: "Run Duration" },
        { name: 'RefreshStatus', displayName: "Refresh Status" },

        {
            name: 'Refresh ', enableFiltering: false, enableSorting: false,
            cellTemplate: '<div style="text-align:center;position: relative;width:100%;padding:2px 2px 2px 6px;"><button  class=" btn-danger " ng-click="grid.appScope.RefreshMaterializedView(row)"><i class="fa fa-refresh"></i>&nbspRefresh View</button></div>'
        }


    ];
    $scope.gridMaterializedViewRefresh = {
        showGridFooter: true,
        enableFiltering: true,
        enableSorting: true,
        columnDefs: columnMaterializedViewRefresh
    };


    $scope.GetMaterializedView = function () {

        $http({
            method: "POST",
            url: MyApp.rootPath + "MaterializedViewRefresh/GetMaterializedView"
        }).then(function (response) {
            if (response.data.length > 0) {
                $scope.gridMaterializedViewRefresh.data = response.data;
            } else {
               $scope.gridMaterializedViewRefresh.data = [];
            }
        }, function (response) {
            if (response.status === 404) {
                toastr.warning("Error Loading Materialized View List!", { timeOut: 2000 });
            }
        });
    }
    $scope.GetMaterializedView();


    $scope.RefreshMaterializedView = function (row) {

        $http({
            method: "POST",
            url: MyApp.rootPath + "MaterializedViewRefresh/RefreshMaterializedView",
            params: { JobName: row.entity.MaterializedViewName }
        }).then(function (response) {
            if (response.status === 200) {               
                $scope.GetMaterializedView();
                //toastr.success("Refresh Complete", { timeOut: 2000 });
                toastr.success(response.data.Message, { timeOut: 2000 });
            } else {
                toastr.success(response.data.status, { timeOut: 2000 });
                //toastr.warning("Refresh Error", { timeOut: 2000 });
            }
        }, function (response) {
            if (response.status === 404) {
                toastr.warning("Error Refreshing view!", { timeOut: 2000 });
            }
        });

    };


    $scope.Reset = function () {
        $scope.GetMaterializedView();
    }

});