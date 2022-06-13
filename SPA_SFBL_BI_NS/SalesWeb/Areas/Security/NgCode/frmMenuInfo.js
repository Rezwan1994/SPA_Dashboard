app.controller("menuCtrl", function ($scope, $http) {
    $scope.Status = "Active";
    $scope.btnSaveValue = "Save";
    //-------------Grid------------------//
    var columnMenuList = [
        { name: 'Id', displayName: "ID" ,visible:false},
        { name: 'Name', displayName: "Menu Name" },
        { name: 'DisplayName', displayName: "Display Name" },
        { name: 'MenuType', displayName: "Menu Type" },
        { name: 'Status', displayName: "Status" },
        //{ name: 'btnDelete', enableFiltering: false, enableSorting: false, displayName: "Action", cellTemplate: '<div style="text-align:center;position: relative;width:100%;padding:2px 2px 2px 6px;"><button type="button" ng-click="grid.appScope.DeleteGridRMCRow(row.entity)">Delete</button></div>' }
    ];
    $scope.gridMenuOptions = {
        enableFiltering: true,
        enableSorting: true,
        columnDefs: columnMenuList,
        rowTemplate: rowTemplate(),
        onRegisterApi: function (gridApi) {
            $scope.gridMHOptions = gridApi;
        }
    };
    function rowTemplate() {
        return '<div ng-dblclick="grid.appScope.rowDblClickComp(row)" >' +
            '  <div ng-repeat="(colRenderIndex, col) in colContainer.renderedColumns track by col.colDef.name" class="ui-grid-cell" ng-class="{ \'ui-grid-row-header-cell\': col.isRowHeader }"  ui-grid-cell></div></div>';
    }
    $scope.rowDblClickComp = function (row) {
        $scope.Name = row.entity.Name;
        $scope.Id = row.entity.Id;
        $scope.DisplayName = row.entity.DisplayName;
        $scope.MenuType = row.entity.MenuType;
        $scope.Status = row.entity.Status;
        $scope.btnSaveValue = "Update";
       // $('#MenuListModal').modal('hide');
    };
    //-------------Search------------------//
    $scope.GetMenuList = function () {
        $http({
            method: "GET",
            url: MyApp.rootPath + "MenuInfo/GetMenuList"
        }).then(function (response) {
            if (response.data.length > 0) {
                $scope.gridMenuOptions.data = response.data;
                //$('#MenuListModal').modal('show');
            } else {

                $scope.gridMenuOptions.data = [];
                toastr.warning("No Data Found!", { timeOut: 2000 });
            }
        }, function (response) {
            alert("Error Loading Menu Info");
        });
    };

    $scope.GetMenuList();
    //-------------Insert/update------------------//
    var methodName = "";
    $scope.SaveData = function () {
        $scope.SaveDb = {};
        $scope.SaveDb.Name = $scope.Name;
        $scope.SaveDb.Id = $scope.Id;
        $scope.SaveDb.DisplayName = $scope.DisplayName;
        $scope.SaveDb.MenuType = "Menu";//$scope.MenuType;
        $scope.SaveDb.Status = $scope.Status;
        if ($scope.Id === "" || typeof $scope.Id === "undefined") {
            methodName = "InsertMenuInfo";
        }
        else {
            methodName = "UpdateMenuInfo";
        }
        $http({
            method: "post",
            url: MyApp.rootPath + "MenuInfo/" + methodName,
            datatype: "json",
            data: JSON.stringify($scope.SaveDb)
        }).then(function (response) {
            if (response.data.Status === "Ok") {
                $scope.Id = response.data.Id;
                $scope.btnSaveValue = "Update";
                toastr.success(response.data.Message, { timeOut: 2000 });
                $scope.GetMenuList();
            } else {
                console.log(response);
                toastr.warning("Error Occurred!", { timeOut: 2000 });
            }
        });
    };
    //-------------Reset------------------//
    $scope.Reset = function () {
        $scope.btnSaveValue = "Save";
        $scope.Name = "";
        $scope.DisplayName = "";
        $scope.MenuType = "";
        $scope.Status = "Active";
        $scope.Id = "";
        //$scope.gridMenuOptions.data = [];
    };
});