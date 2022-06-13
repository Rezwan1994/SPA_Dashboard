app.controller("roleReportConfCtrl", function ($scope, $http) {
    $scope.btnSaveValue = "Save";
   


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

    $scope.OnRoleInfoClick = function () {
        $scope.Id = "";
        $http({
            method: "POST",
            url: MyApp.rootPath + "RoleReportConf/GetReportListByRole",
            datatype: "json",
            data: { roleId: $scope.frmRoleReportConf.RoleInfo.RoleId}
        }).then(function (response) {
           $scope.GetRoleReportConfList($scope.frmRoleReportConf.RoleInfo.RoleId);
            if (response.data.Status === null || response.data.Status === undefined) {
                if (response.data.length > 0) {
                    $scope.Reports = response.data;

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
    


    //---------------Grid-----------------//
    var columnList = [
        { name: 'Id', displayName: "Id", visible: false },
        { name: 'ReportId', displayName: "ReportId", visible: false },
        { name: 'ReportName', displayName: "Report Name"},
        { name: 'RoleId', displayName: "Role Id", visible: false },
        { name: 'RoleName', displayName: "Role Name" },
        { name: 'btnDelete', enableFiltering: false, enableSorting: false, displayName: "Action", cellTemplate: '<div style="text-align:center;position: relative;width:100%;padding:2px 2px 2px 6px;"><button type="button" ng-click="grid.appScope.DeleteGridRMCRow(row.entity)">Delete</button></div>' }

    ];
    $scope.gridOptions = {
        enableFiltering: true,
        enableSorting: true,
        columnDefs: columnList,
        rowTemplate: rowTemplate(),
        onRegisterApi: function (gridApi) {
            $scope.gridOptions = gridApi;
        }
    };

    function rowTemplate() {
        return '<div ng-dblclick="grid.appScope.rowDblClickComp(row)" >' +
            '  <div ng-repeat="(colRenderIndex, col) in colContainer.renderedColumns track by col.colDef.name" class="ui-grid-cell" ng-class="{ \'ui-grid-row-header-cell\': col.isRowHeader }"  ui-grid-cell></div></div>';
    }

    $scope.rowDblClickComp = function (row) {
        $scope.Id = row.entity.Id;
        $scope.frmRoleReportConf.RoleInfo = row.entity;
        $scope.frmRoleReportConf.Report = row.entity;
    };
    //----------------Delete-------------------//
    $scope.DeleteGridRMCRow = function (row) {
        $http({
            method: "post",
            url: MyApp.rootPath + "RoleReportConf/DeleteGridRMCRow",
            datatype: "json",
            data: { Id: row.Id }
        }).then(function (response) {
            if (response.data.Status === "Ok") {
                toastr.success(response.data.Message, { timeOut: 2000 });
                $scope.GetRoleReportConfList();
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
    $scope.GetRoleReportConfList = function (roleId) {
        var param = "";
        if (roleId !== "" && roleId !== undefined) {
            param = "AND A.ROLE_ID=" + roleId;
        }
        $http({
            method: "post",
            url: MyApp.rootPath + "RoleReportConf/GetRoleReportConfList",
            datatype: "json",
            data: { param: param }
        }).then(function (response) {
            if (response.data.Status === null || response.data.Status === undefined) {
                if (response.data.length > 0) {
                    $scope.gridOptions.data = response.data;
                    
                } else {
                    $scope.gridOptions.data = [];
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
        $scope.SaveDb.ReportId = $scope.frmRoleReportConf.Report.ReportId;
        $scope.SaveDb.RoleId = $scope.frmRoleReportConf.RoleInfo.RoleId;
       

        if ($scope.Id === '' || typeof $scope.Id === 'undefined') {
            methodName = "InsertRoleReportConf";
        } else {
            methodName = "UpdateRoleReportConf";
        }
        $http({
            method: "post",
            url: MyApp.rootPath + "RoleReportConf/" + methodName,
            datatype: "json",
            data: JSON.stringify($scope.SaveDb)
        }).then(function (response) {
            if (response.data.Status === "Ok") {
                $scope.Id = response.data.ID;
                $scope.btnSaveValue = "Update";
                $scope.GetRoleReportConfList($scope.frmRoleReportConf.RoleInfo.RoleId);
                toastr.success(response.data.Message, { timeOut: 2000 });
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
    //------------------Reset-----------------//
    $scope.Reset = function () {
        $scope.Id = "";
        $scope.frmRoleReportConf.RoleInfo = undefined;
        $scope.frmRoleReportConf.Report = undefined;
        $scope.gridOptions.data = [];
    };
});