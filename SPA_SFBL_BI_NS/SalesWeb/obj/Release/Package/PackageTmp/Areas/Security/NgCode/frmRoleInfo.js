app.controller("roleInfoCtrl", function($scope, $http) {
    $scope.Status = "Active";
    $scope.btnSaveValue = "Save";

    var columnRList = [
        { name: 'RoleId', displayName: "Role Code" },
        { name: 'RoleName', displayName: "Role Name" },
        { name: 'Status', displayName: "Status" }
    ];
    $scope.gridROptions = {
        enableFiltering: true,
        enableSorting: true,
        columnDefs: columnRList,
        rowTemplate: rowTemplate(),
        onRegisterApi: function(gridApi) {
            $scope.gridRCOptions = gridApi;
        }
    };
    function rowTemplate() {
        return '<div ng-dblclick="grid.appScope.rowDblClickComp(row)" >' +
            '  <div ng-repeat="(colRenderIndex, col) in colContainer.renderedColumns track by col.colDef.name" class="ui-grid-cell" ng-class="{ \'ui-grid-row-header-cell\': col.isRowHeader }"  ui-grid-cell></div></div>';
    }
    $scope.rowDblClickComp = function (row) {
        $scope.RoleId = row.entity.RoleId;
        $scope.RoleName = row.entity.RoleName;        
        $scope.Status = row.entity.Status;
        $scope.btnSaveValue = "Update";
       // $('#rModal').modal('hide');
    };

    $scope.GetRoleList = function() {
        $http({
            method: "get",
            url: MyApp.rootPath + "RoleInfo/GetRoleList",
            datatype: "json",
            data: JSON.stringify($scope.SaveDb)
        }).then(function(response) {
                if (response.data.Status === null || response.data.Status === undefined) {
                    if (response.data.length > 0) {
                        $scope.gridROptions.data = response.data;
                        //$('#rModal').modal("show");
                    } else {
                        toastr.warning("No Data Found!");
                    }
                } else {
                    toastr.warning(response.data.Status, { timeOut: 2000 });
                }
            },
            function(response) {
                console.log(response);
                toastr.warning("Error Occurred!", { timeOut: 2000 });
            });

    };

    $scope.GetRoleList();

    var methodName = "";
    $scope.SaveData = function() {
        $scope.SaveDb = {};
        $scope.SaveDb.RoleName = $scope.RoleName;
        $scope.SaveDb.RoleId = $scope.RoleId;
        $scope.SaveDb.Status = $scope.Status;

        if ($scope.RoleId === '' || typeof $scope.RoleId === 'undefined') {
            methodName = "InsertRoleInfo";
        } else {
            methodName = "UpdateRoleInfo";
        }
        $http({
            method: "post",
            url: MyApp.rootPath + "RoleInfo/" + methodName,
            datatype: "json",
            data: JSON.stringify($scope.SaveDb)
        }).then(function(response) {
                if (response.data.Status === "Ok") {
                    $scope.RoleId = response.data.ID;
                    $scope.btnSaveValue = "Update";
                    $scope.GetRoleList();
                    toastr.success(response.data.Message, { timeOut: 2000 });
                } else {
                    console.log(response);
                    toastr.warning("Error Occurred!", { timeOut: 2000 });
                }
            },
            function(response) {
                console.log(response);
                toastr.warning("Error Occurred!", { timeOut: 2000 });
            });
    };

    $scope.Reset = function() {
        $scope.RoleName = "";
        $scope.RoleId = "";
        $scope.Status = "Active";
        //$scope.gridROptions.data = [];
        $scope.btnSaveValue = "Save";
    }
});