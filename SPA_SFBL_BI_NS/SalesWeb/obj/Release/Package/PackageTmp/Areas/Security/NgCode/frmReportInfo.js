app.controller("ReportInfoCtrl", function ($scope, $http) {
    $scope.btnSaveValue = "Save";
    //-------------Grid------------------//
    var columnReportList = [
        { name: 'ReportId', displayName: "ID", visible: false },
        { name: 'ReportName', displayName: "Name" },
        { name: 'DisplayName', displayName: "Display Name" },
        { name: 'FormName', displayName: "Form Name" },
        { name: 'FormUrl', displayName: "Display Name", visible: false}
    ];
    $scope.gridReportOptions = {
        enableFiltering: true,
        enableSorting: true,
        columnDefs: columnReportList,
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
        $scope.ReportName = row.entity.ReportName;
        $scope.ReportId = row.entity.ReportId;
        $scope.DisplayName = row.entity.DisplayName;
        $scope.frmReportInfo.Form = row.entity;
        $scope.btnSaveValue = "Update";
        $('#ReportListModal').modal('hide');
    };
    //-------------Search------------------//
    $scope.GetFormNameList = function () {
        $http({
            method: "post",
            url: MyApp.rootPath + "ReportInfo/GetFormNameList",
            datatype: "json"
        }).then(function (response) {
            if (response.data !== "") {
                $scope.FormNames = response.data;
            } else {
                toastr.warning("No Data Found!");
            }
        }, function () {
            toastr.warning("Error Occurred!");
        });
    };
    $scope.GetFormNameList();
    $scope.GetReportList = function () {
        $scope.gridReportOptions.data = [];
        $http({
            method: "GET",
            url: MyApp.rootPath + "ReportInfo/GetReportList"
        }).then(function (response) {
            if (response.data.length > 0) {
                $scope.gridReportOptions.data = response.data;
                $('#ReportListModal').modal('show');
            } else {
                toastr.warning("No Data Found!", { timeOut: 2000 });
            }
        }, function (response) {
            alert("Error Loading Category");
        });
    };

    //-------------Insert/update------------------//
    var methodName = "";
    $scope.SaveData = function () {
        $scope.SaveDb = {};
        $scope.SaveDb.ReportName = $scope.ReportName;
        $scope.SaveDb.ReportId = $scope.ReportId;
        $scope.SaveDb.DisplayName = $scope.DisplayName;
        $scope.SaveDb.FormName = $scope.frmReportInfo.Form.FormName;
        $scope.SaveDb.FormUrl = $scope.frmReportInfo.Form.FormUrl;
        if ($scope.ReportId === "" || typeof $scope.ReportId === "undefined") {
            methodName = "InsertReportInfo";
        }
        else {
            methodName = "UpdateReportInfo";
        }
        $http({
            method: "post",
            url: MyApp.rootPath + "ReportInfo/" + methodName,
            datatype: "json",
            data: JSON.stringify($scope.SaveDb)
        }).then(function (response) {
            if (response.data.Status === "Ok") {
                $scope.Id = response.data.Id;
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
        $scope.btnSaveValue = "Save";
        $scope.ReportName = "";
        $scope.DisplayName = "";
        $scope.ReportId = "";
        $scope.gridReportOptions.data = [];
        $scope.frmReportInfo.Form = undefined;
    };
});