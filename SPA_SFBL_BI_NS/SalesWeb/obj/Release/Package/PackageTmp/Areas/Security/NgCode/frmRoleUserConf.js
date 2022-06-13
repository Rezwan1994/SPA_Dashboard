app.controller("roleUserConfCtrl", function ($scope, $http) {
    $scope.btnSaveValue = "Save";
    

    //-----------------Search------------------//
  
    $http({
        method: "get",
        url: MyApp.rootPath + "RoleInfo/GetRoleList",
        datatype: "json"
    }).then(function (response) {
        if (response.data.Status === null || response.data.Status === undefined) {
            if (response.data.length > 0) {
                $scope.Roles = response.data;
            }

        } else {
            toastr.warning(response.data.Status, { timeOut: 2000 });
        }
    },
        function (response) {
            console.log(response);
            toastr.warning("Error Occurred!", { timeOut: 2000 });
        });

    $scope.OnRoleListClick = function () {
        $scope.Id = "";
        $http({
            method: "POST",
            url: MyApp.rootPath + "RoleUserConf/GetUserInfoList",
            datatype: "json",
            data: { roleId: $scope.frmRoleUserConf.RoleInfo.RoleId }
        }).then(function (response) {
            if (response.data.Status === null || response.data.Status === undefined) {
                if (response.data.length > 0) {
                    $scope.Users = response.data;
                }
                $scope.GetRoleUserConfList($scope.frmRoleUserConf.RoleInfo.RoleId);
            } else {
                toastr.warning(response.data.Status, { timeOut: 2000 });
            }
        },
            function (response) {
                console.log(response);
                toastr.warning("Error Occurred!", { timeOut: 2000 });
            });
    };


    $scope.GetRoleUserConfList = function (roleId) {
        var param = "";
        if (roleId !== "" && roleId !== undefined) {
            param = " AND A.ROLE_ID=" + roleId;

            $http({
                method: "post",
                url: MyApp.rootPath + "RoleUserConf/GetRoleUserConfList",
                datatype: "json",
                data: { param: param }
            }).then(function (response) {
                if (response.data.Status === null || response.data.Status === undefined) {
                    if (response.data.length > 0) {
                        $scope.gridRUCOptions.data = response.data;

                    } else {
                        $scope.gridRUCOptions.data = [];
                        toastr.warning("No Data Found!");
                    }
                } else {
                    toastr.warning(response.data.Status, { timeOut: 2000 });
                }
            },
                function (response) {
                    console.log(response);
                    toastr.warning("Error Occurred!", { timeOut: 2000 });
                });
        }


    };


    //--------------Grid------------------//
    var columnRMCList = [
        { name: 'SlNo', displayName: "Sl No", width: 100 },
        { name: 'Id', displayName: "Id", visible: false },
        { name: 'UserId', displayName: "UserId", visible: false },
        { name: 'RoleId', displayName: "Role Id", visible: false },
        { name: 'RoleName', displayName: "Role Name", width: 280 },
        { name: 'EmployeeCode', displayName: "Employee Code", width: 280 },
        { name: 'EmployeeName', displayName: "Employee Name", width: 280 },
        { name: 'btnDelete', enableFiltering: false, enableSorting: false, displayName: "Action", cellTemplate: '<div style="text-align:center;position: relative;width:100%;padding:2px 2px 2px 6px;"><button type="button" ng-click="grid.appScope.DeletegridRUCRow(row.entity)">Delete</button></div>' }

    ];
    $scope.gridRUCOptions = {
        enableFiltering: true,
        enableSorting: true,
        columnDefs: columnRMCList,
        rowTemplate: rowTemplate(),
        onRegisterApi: function (gridApi) {
            $scope.gridRUCOptions = gridApi;
        }
    };

    function rowTemplate() {
        return '<div ng-dblclick="grid.appScope.rowDblClickComp(row)" >' +
            '  <div ng-repeat="(colRenderIndex, col) in colContainer.renderedColumns track by col.colDef.name" class="ui-grid-cell" ng-class="{ \'ui-grid-row-header-cell\': col.isRowHeader }"  ui-grid-cell></div></div>';
    }

    $scope.rowDblClickComp = function (row) {
        $scope.Id = row.entity.Id;
        $scope.frmRoleUserConf.RoleInfo = row.entity;
        $scope.frmRoleUserConf.UserInfo = row.entity;
    };
    //-----------------Delete------------------//
    $scope.DeletegridRUCRow = function (row) {
        //$scope.ID = row.ID;

        //if ($scope.Id !== '') {
        $http({
            method: "post",
            url: MyApp.rootPath + "RoleUserConf/DeletegridRUCRow",
            datatype: "json",
            data: { Id: row.Id }
        }).then(function (response) {
            if (response.data.Status === "Ok") {
                toastr.success(response.data.Message, { timeOut: 2000 });
                $scope.gridRUCOptions.data = [];
                $scope.GetRoleUserConfList($scope.frmRoleUserConf.RoleInfo.RoleId);
                //$scope.GetRoleUserConfList();
            } else {
                console.log(response);
                toastr.warning("Error Occurred!", { timeOut: 2000 });
            }
        },
            function (response) {
                console.log(response);
                toastr.warning("Error Occurred!", { timeOut: 2000 });
            });
    };


    //}

  
    //-----------------Search------------------//
    var methodName = "";
    $scope.SaveData = function () {
        $scope.SaveDb = {};
        $scope.SaveDb.Id = $scope.Id;
        $scope.SaveDb.UserId = $scope.frmRoleUserConf.UserInfo.UserId;
        $scope.SaveDb.RoleId = $scope.frmRoleUserConf.RoleInfo.RoleId;
        //$scope.SaveDb.SaveStatus = $scope.SaveStatus;
        //$scope.SaveDb.ViewStatus = $scope.ViewStatus;
        //$scope.SaveDb.DeleteStatus = $scope.DeleteStatus;

        //if ($scope.Id === '' || typeof $scope.Id === 'undefined') {
        //    methodName = "InsertRoleUserConf";
        //} else {
        //    methodName = "UpdateRoleUserConf";
        //}

        methodName = "InsertRoleUserConf";

        $http({
            method: "post",
            url: MyApp.rootPath + "RoleUserConf/" + methodName,
            datatype: "json",
            data: JSON.stringify($scope.SaveDb)
        }).then(function (response) {
            if (response.data.Status === "Ok") {
                //$scope.Id = response.data.ID;
                //$scope.btnSaveValue = "Update";
                $scope.GetRoleUserConfList($scope.frmRoleUserConf.RoleInfo.RoleId);
                toastr.success(response.data.Message, { timeOut: 2000 });
                //$scope.GetRoleUserConfList();
            } else {
                console.log(response);
                toastr.warning("Error Occurred!", { timeOut: 2000 });
            }
        },
            function (response) {
                console.log(response);
                toastr.warning("Error Occurred!", { timeOut: 2000 });
            });
    };
    //-----------------Reset------------------//
    $scope.Reset = function () {

        $scope.Id = "";
        $scope.frmRoleUserConf.RoleInfo = undefined;
        $scope.frmRoleUserConf.UserInfo = undefined;
        $scope.gridRUCOptions.data = [];
        $scope.btnSaveValue = "Save";
    };
});