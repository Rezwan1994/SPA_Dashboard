app.controller("menuConfigureCtrl", function ($scope, $http) {
    $scope.btnSaveValue = "Save";
    $scope.ParentSeqReadonly = false;
    $scope.ChildSeqReadonly = false;
    $scope.GetParentMenuList = function () {
        $http({
            method: "GET",
            url: MyApp.rootPath + "MenuConfigure/GetParentMenuList"
        }).then(function (response) {
            if (response.data !== "") {
                $scope.ParentMenus = response.data;
            } else {
                toastr.warning("No Data Found!");
            }
        }, function () {
            toastr.warning("Error Occurred!");
        });
    };
    $scope.GetParentMenuList();
    $scope.GetChildMenuList = function (mId) {
        $http({
            method: "post",
            url: MyApp.rootPath + "MenuConfigure/GetChildMenuList",
            datatype: "json",
            data: { parentMenuId: mId }
        }).then(function (response) {
            if (response.data !== "") {
                $scope.ChildMenus = response.data;
            } else {
                toastr.warning("No Data Found!");
            }
        }, function () {
            toastr.warning("Error Occurred!");
        });
    };
    $scope.OnParentMenuClick = function () {
        $scope.frmMenuConfigure.ChildMenu = "";
        var mId = $scope.frmMenuConfigure.ParentMenu.ParentMenuId;
        $scope.GetChildMenuList(mId);
        $scope.GetMenuConfigureList(mId);
        var parentSeq = $scope.frmMenuConfigure.ParentMenu.ParentSeq;
        if (parentSeq === 0) {
        //    $scope.ParentSeqReadonly = false;
            $scope.ParentSeq = "";
        }
        else {
        //    $scope.ParentSeqReadonly = false;
            $scope.ParentSeq = parentSeq;
        }
    };
    $scope.OnChildMenuClick = function () {
        var childSeq = $scope.frmMenuConfigure.ChildMenu.ChildSeq;
        if (childSeq === 0) {
            //$scope.ChildSeqReadonly = false;
            $scope.ChildSeq = "";
        }
        else {
            //$scope.ChildSeqReadonly = false;
            $scope.ChildSeq = childSeq;
        }
    };
    //-------------Grid------------------//
    var columnMenuConfigureList = [
        { name: 'Id', displayName: "ID", visible: false },
        { name: 'ParentMenuId', displayName: "Parent Id", visible: false },
        { name: 'ParentMenuName', displayName: "Parent Name" },
        { name: 'ParentSeq', displayName: "Parent Sequence" },
        { name: 'ChildMenuId', displayName: "Child Id", visible: false },
        { name: 'ChildMenuName', displayName: "Child Name" },
        { name: 'ChildSeq', displayName: "Child Sequence" },
        { name: 'Url', displayName: "Url" },
        { name: 'btnDelete', enableFiltering: false, enableSorting: false, displayName: "Action", cellTemplate: '<div style="text-align:center;position: relative;width:100%;padding:2px 2px 2px 6px;"><button type="button"  ng-click="grid.appScope.DeleteAccess(row.entity)">Delete</button></div>' }

    ];
    $scope.gridMenuConfigureOptions = {
        enableFiltering: true,
        enableSorting: true,
        columnDefs: columnMenuConfigureList,
        rowTemplate: rowTemplate(),
        onRegisterApi: function (gridApi) {
            $scope.gridMenuConfigureOptions = gridApi;
        }
    };
    function rowTemplate() {
        return '<div ng-dblclick="grid.appScope.rowDblClickComp(row)" >' +
            '  <div ng-repeat="(colRenderIndex, col) in colContainer.renderedColumns track by col.colDef.name" class="ui-grid-cell" ng-class="{ \'ui-grid-row-header-cell\': col.isRowHeader }"  ui-grid-cell></div></div>';
    }
    $scope.rowDblClickComp = function (row) {
        $scope.Id = row.entity.Id;
        $scope.frmMenuConfigure.ParentMenu = row.entity;
        $scope.ParentSeq = row.entity.ParentSeq;
        $scope.frmMenuConfigure.ChildMenu = row.entity;
        $scope.ChildSeq = row.entity.ChildSeq;
        $scope.Url = row.entity.Url;
        $scope.btnSaveValue = "Update";
        $('#MenuConfigureListModal').modal('hide');
    };
    //-------------Search------------------//
    $scope.GetMenuConfigureList = function (mId) {
        $http({
            method: "post",
            url: MyApp.rootPath + "MenuConfigure/GetMenuConfigureList",
            datatype: "json",
            data: { parentId: mId }
        }).then(function (response) {
            $scope.gridMenuConfigureOptions.data = response.data;
           // $('#MenuConfigureListModal').modal('show');
        }, function () {
            alert("Error Loading Category");
        });
    };
    $scope.IsChildSeqExist = function () {
        $http({
            method: "post",
            url: MyApp.rootPath + "MenuConfigure/IsChildSeqExist",
            datatype: "json",
            data: { parentId: $scope.frmMenuConfigure.ParentMenu.ParentMenuId, parentSeq: $scope.ParentSeq }
        }).then(function (response) {
            if (response.data.Status!==null) {
                $scope.ParentSeq = response.data.Data;
                toastr.warning("Data Exist");
            }
            
        }, function () {
            alert("Error Loading Category");
        });
    };
    $scope.IsParentSeqExist = function () {
        $http({
            method: "post",
            url: MyApp.rootPath + "MenuConfigure/IsParentSeqExist",
            datatype: "json",
            data: { childId: $scope.frmMenuConfigure.ChildMenu.ChildMenuId, childSeq: $scope.ChildSeq }
        }).then(function (response) {
            if (response.data.Status!==null) {
                $scope.ParentSeq = response.data.Data;
                toastr.warning("Data Exist");
            }
            
        }, function () {
            alert("Error Loading Category");
        });
    };
    $scope.DeleteAccess = function (row) {
        $scope.Id = row.Id;
        var mId = $scope.frmMenuConfigure.ParentMenu.ParentMenuId;
        if ($scope.ID !== '') {
            $http({
                method: "post",
                url: MyApp.rootPath + "MenuConfigure/DeleteMenuConfigure",
                datatype: "json",
                data: { Id: $scope.Id }
            }).then(function (response) {
                $scope.Id = "";
                $scope.btnSaveValue = "Save";
                $scope.GetChildMenuList(mId);
                $scope.GetMenuConfigureList(mId);
                toastr.success(response.data.Message, { timeOut: 2000 });
            });
        }
    };
    //-------------Insert/Update------------------//
    var methodName = "";
    $scope.SaveData = function () {
        $scope.SaveDb = {};
        $scope.SaveDb.Id = $scope.Id;
        $scope.SaveDb.ParentMenuId = $scope.frmMenuConfigure.ParentMenu.ParentMenuId;
        $scope.SaveDb.ParentSeq = $scope.ParentSeq;
        $scope.SaveDb.ChildMenuId = $scope.frmMenuConfigure.ChildMenu.ChildMenuId;
        $scope.SaveDb.ChildSeq = $scope.ChildSeq;
        $scope.SaveDb.Url = $scope.Url;
        if ($scope.frmMenuConfigure.ChildMenu === null || $scope.frmMenuConfigure.ChildMenu === undefined|| $scope.frmMenuConfigure.ChildMenu === "") {
            methodName = "UpdateParent";
        } else if ($scope.Id === "" || typeof $scope.Id === "undefined") {
            methodName = "InsertMenuConfigure";
        } else {
            methodName = "UpdateMenuConfigure";
        }
        $http({
            method: "post",
            url: MyApp.rootPath + "MenuConfigure/" + methodName,
            datatype: "json",
            data: JSON.stringify($scope.SaveDb)
        }).then(function (response) {
            if (response.data.Status === "Ok") {
                var mId = $scope.frmMenuConfigure.ParentMenu.ParentMenuId;
                $scope.GetParentMenuList();
                $scope.GetChildMenuList(mId);
                $scope.GetMenuConfigureList(mId);
                $scope.Id = response.data.Id === 0 ? "" : response.data.Id;
                $scope.btnSaveValue = "Update";
                toastr.success(response.data.Message, { timeOut: 2000 });
            } else {
                console.log(response);
                toastr.warning("Error Occurred!", { timeOut: 2000 });
            }
        });
    };
    //-------------Reset------------------//
    $scope.Reset = function () {
        $scope.ParentSeqReadonly = false;
        $scope.ChildSeqReadonly = false;
        $scope.btnSaveValue = "Save";
        $scope.Id = "";
        $scope.frmMenuConfigure.ParentMenu = "";
        $scope.frmMenuConfigure.ChildMenu = "";
        $scope.ParentSeq = "";
        $scope.ChildSeq = "";
        $scope.Url = "";
        $scope.gridMenuConfigureOptions.data = [];
    };
});