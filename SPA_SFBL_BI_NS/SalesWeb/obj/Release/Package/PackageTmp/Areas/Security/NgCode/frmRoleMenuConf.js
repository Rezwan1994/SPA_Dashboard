app.controller("roleMenuConfCtrl", function ($scope, $http) {
    $scope.btnSaveValue = "Save";
   
    $scope.GetRoleList = function () {

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

    }
    $scope.GetRoleList();


    $scope.GetMenuList = function () {
        $scope.Id = "";
        $scope.ChildMenus = [];
        $http({
            method: "POST",
            url: MyApp.rootPath + "RoleMenuConf/GetChildMenuList",
            datatype: "json",
            data: { roleId: $scope.frmRoleMenuConf.RoleInfo.RoleId }
        }).then(function (response) {
            if (response.data.Status === null || response.data.Status === undefined) {
                if (response.data.length > 0) {
                    $scope.ChildMenus = response.data;

                }
            } else {
                toastr.warning(response.data.Status, { timeOut: 2000 });
            }
        },
            function (response) {
                console.log(response);
                toastr.warning("Error Occurred!", { timeOut: 2000 });
            });
    };


    $scope.OnRoleListClick = function () {
        $scope.Id = "";
        $scope.ChildMenus = [];
        $http({
            method: "POST",
            url: MyApp.rootPath + "RoleMenuConf/GetChildMenuList",
            datatype: "json",
            data: { roleId: $scope.frmRoleMenuConf.RoleInfo.RoleId}
        }).then(function (response) {
            $scope.GetRoleMenuConfList($scope.frmRoleMenuConf.RoleInfo.RoleId);
            if (response.data.Status === null || response.data.Status === undefined) {
                if (response.data.length > 0) {
                    $scope.ChildMenus = response.data;

                }
            } else {
                toastr.warning(response.data.Status, { timeOut: 2000 });
            }
        },
            function (response) {
                console.log(response);
                toastr.warning("Error Occurred!", { timeOut: 2000 });
            });
    };
    $scope.OnChildMenuClick = function () {
        $http({
            method: "POST",
            url: MyApp.rootPath + "RoleMenuConf/IsParentMenuMapped",
            datatype: "json",
            data: { roleId: $scope.frmRoleMenuConf.RoleInfo.RoleId, parentId: $scope.frmRoleMenuConf.ChildMenu.ParentMenuId }
        }).then(function (response) {
            if (response.data.Status === null || response.data.Status === undefined) {
                if (response.data.isValid !== "Yes") {
                    $scope.frmRoleMenuConf.ChildMenu = undefined;
                    toastr.warning("Parent menu is not mapped", { timeOut: 2000 });
                }
            } else {
                console.log(response.data.Status);
                toastr.warning("Error Occurred!", { timeOut: 2000 });
            }
        },
            function (response) {
                console.log(response);
                toastr.warning("Error Occurred!", { timeOut: 2000 });
            });
    };


    //---------------Grid-----------------//
    var columnRMCList = [
        { name: 'SlNo', displayName: "Sl No", width: 100 },
        { name: 'Id', displayName: "Id", visible: false },
        { name: 'McId', displayName: "McId", visible: false },
        { name: 'ChildMenuId', displayName: "ChildMenuId", visible: false },
        { name: 'ChildMenuName', displayName: "Child Menu Name", width: 300 },
        { name: 'ParentMenuId', displayName: "ParentMenuId", visible: false },
        { name: 'ParentMenuName', displayName: "Parent Menu Name", width: 300 },
        { name: 'RoleId', displayName: "Role Id", visible: false },
        { name: 'RoleName', displayName: "Role Name", width: 200 },
        //{ name: 'SaveStatus', displayName: "Save Status", width: 100 },
        //{ name: 'ViewStatus', displayName: "View Status", width: 100 },
        //{ name: 'DeleteStatus', displayName: "Delete Status", width: 100 },
        { name: 'btnDelete', enableFiltering: false, enableSorting: false, displayName: "Action", cellTemplate: '<div style="text-align:center;position: relative;width:100%;padding:2px 2px 2px 6px;"><button type="button" ng-click="grid.appScope.DeleteGridRMCRow(row.entity)">Delete</button></div>' }

    ];
    $scope.gridRMCOptions = {
        enableFiltering: true,
        enableSorting: true,
        columnDefs: columnRMCList,
        rowTemplate: rowTemplate(),
        onRegisterApi: function (gridApi) {
            $scope.gridRMCOptions = gridApi;
        }
    };

    function rowTemplate() {
        return '<div ng-dblclick="grid.appScope.rowDblClickComp(row)" >' +
            '  <div ng-repeat="(colRenderIndex, col) in colContainer.renderedColumns track by col.colDef.name" class="ui-grid-cell" ng-class="{ \'ui-grid-row-header-cell\': col.isRowHeader }"  ui-grid-cell></div></div>';
    }

    $scope.rowDblClickComp = function (row) {
        $scope.Id = row.entity.Id;
        $scope.frmRoleMenuConf.RoleInfo = row.entity;
        $scope.frmRoleMenuConf.ChildMenu = row.entity;
    };
    //----------------Delete-------------------//
    $scope.DeleteGridRMCRow = function (row) {
        $http({
            method: "post",
            url: MyApp.rootPath + "RoleMenuConf/DeleteGridRMCRow",
            datatype: "json",
            data: { Id: row.Id }
        }).then(function (response) {
            if (response.data.Status === "Ok") {
                toastr.success(response.data.Message, { timeOut: 2000 });
                $scope.GetRoleMenuConfList($scope.frmRoleMenuConf.RoleInfo.RoleId);
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

    //------------------Search-----------------//
    $scope.GetRoleMenuConfList = function (roleId) {
        var param = "";
        if (roleId !== "" && roleId !== undefined) {
            param = "AND A.ROLE_ID=" + roleId;
        }
        $http({
            method: "post",
            url: MyApp.rootPath + "RoleMenuConf/GetRoleMenuConfList",
            datatype: "json",
            data: { param: param }
        }).then(function (response) {
            if (response.data.Status === null || response.data.Status === undefined) {
                if (response.data.length > 0) {
                    $scope.gridRMCOptions.data = response.data;
                    
                } else {
                    $scope.gridRMCOptions.data = [];
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

    };
    //------------------Insert?update-----------------//
    var methodName = "";
    $scope.SaveData = function () {
        $scope.SaveDb = {};
        $scope.SaveDb.Id = $scope.Id;
        $scope.SaveDb.McId = $scope.frmRoleMenuConf.ChildMenu.McId;
        $scope.SaveDb.RoleId = $scope.frmRoleMenuConf.RoleInfo.RoleId;
        $scope.SaveDb.SaveStatus = "Yes";
        $scope.SaveDb.ViewStatus = "Yes";
        $scope.SaveDb.DeleteStatus = "Yes";
       

        //if ($scope.Id === '' || typeof $scope.Id === 'undefined') {
            methodName = "InsertRoleMenuConf";
        //} else {
         //   methodName = "UpdateRoleMenuConf";
        //}
        $http({
            method: "post",
            url: MyApp.rootPath + "RoleMenuConf/" + methodName,
            datatype: "json",
            data: JSON.stringify($scope.SaveDb)
        }).then(function (response) {
            if (response.data.Status === "Ok") {
                $scope.Id = response.data.ID;
                //$scope.btnSaveValue = "Update";
                $scope.GetRoleMenuConfList($scope.frmRoleMenuConf.RoleInfo.RoleId);
                $scope.frmRoleMenuConf.ChildMenu = undefined;
                $scope.GetMenuList();
                //$scope.OnChildMenuClick();
                //$scope.ChildMenus = [];
                toastr.success(response.data.Message, { timeOut: 2000 });
            } else {
                console.log(response);
                toastr.warning(response.data.Status, { timeOut: 2000 });
            }
        },
            function (response) {
                console.log(response);
                toastr.warning(response.data.Status, { timeOut: 2000 });
            });
    };
    //------------------Reset-----------------//
    $scope.Reset = function () {

        $scope.Id = "";
        $scope.frmRoleMenuConf.RoleInfo = undefined;

        $scope.frmRoleMenuConf.ChildMenu = undefined;
        $scope.ChildMenus = [];


        $scope.gridRMCOptions.data = [];

       

        $scope.btnSaveValue = "Save";
    };
});