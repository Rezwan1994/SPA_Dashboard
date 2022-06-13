app.controller("userInfoCtrl", function ($scope, $http) {

    $scope.isDisableAccessLoc = true;
    $scope.isLocationDisable = false;
    $scope.btnSaveValue = "Save";
    $scope.Status = "Active";
    $scope.AccessLocation = "National";

    var columnRList = [
        //{ name: 'SlNo', displayName: "Sl. No." },
        { name: 'UserId', displayName: "User Code",visible:false },
        { name: 'UserName', displayName: "User Name" },
        { name: 'Password', displayName: "Password" },
        { name: 'EmployeeId', displayName: "Emplyee Id", visible: false },
        { name: 'EmployeeCode', displayName: "Emplyee Code" },
        { name: 'EmployeeName', displayName: "Emplyee Name" },
        { name: 'AccessLocation', displayName: "Access Location", visible: false},
        { name: 'LocationId', displayName: "Location Id", visible: false},
        { name: 'LocationName', displayName: "Location Name", visible: false},
        { name: 'Status', displayName: "Status" }
    ];
    $scope.gridUserInfo = {

        showColumnFooter: true,
        enableFiltering: true,
        enableSorting: true,
        columnDefs: columnRList,
        enableGridMenu: true,
        enableSelectAll: true,
        exporterMenuPdf: false,
        exporterCsvLinkElement: angular.element(document.querySelectorAll(".custom-csv-link-location")),
        exporterCsvFilename: 'User_Info.csv',
        onRegisterApi: function (gridApi) {
            $scope.gridApi = gridApi;
        }
    };
    function rowTemplate() {
        return '<div ng-dblclick="grid.appScope.rowDblClickComp(row)" >' +
            '  <div ng-repeat="(colRenderIndex, col) in colContainer.renderedColumns track by col.colDef.name" class="ui-grid-cell" ng-class="{ \'ui-grid-row-header-cell\': col.isRowHeader }"  ui-grid-cell></div></div>';
    }
    $scope.rowDblClickComp = function (row) {
        $scope.UserId = row.entity.UserId;
        $scope.UserName = row.entity.UserName;
        $scope.Password = row.entity.Password;
        $scope.AccessLocation = row.entity.AccessLocation;
        $scope.Status = row.entity.Status;
        $scope.btnSaveValue = "Update";
        $scope.frmUserInfo.Employee = row.entity;
        $scope.frmUserInfo.Location = row.entity;
        $scope.GetLocationRowDblClick();

    };

    var LocMethodName = "";
    var ErrMessage = "";

    $scope.GetLocation = function () {
        $http({
            method: "POST",
            url: MyApp.rootPath + "UserInfo/GetDepotList",
        }).then(function (response) {
            if (response.data.Status === "" || response.data.Status === null) {
                if (response.data.Data.length > 0) {

                    if (response.data.Data.length === 1) {

                        $scope.frmUserInfo.Location = response.data.Data[0];
                        $scope.isLocationDisable = true;
                    }
                }
                else {
                    $scope.Locations = [];
                }
            } else {
                toastr.warning(response.data.Status, { timeOut: 2000 });
            }
        }, function (response) {
            if (response.status === 404) {
                toastr.warning(ErrMessage, { timeOut: 2000 });
            }
        });
    };

    $scope.GetLocation();

    $scope.GetEmployeeList = function () {
        $http({
            method: "POST",
            url: MyApp.rootPath + "UserInfo/GetEmployeeList",
            params: { param: "AND EMPLOYEE_STATUS='Active'" }
        }).then(function (response) {
            if (response.data.Status === "" || response.data.Status === null || response.data.Status === undefined) {

                if (response.data.Data.length > 0) {

                    $scope.Employees = response.data.Data;

                } else {
                    toastr.warning("No Data Found!", { timeOut: 2000 });
                }
            }
            else {
                toastr.warning(response.data.Status, { timeOut: 2000 });
            }
        }, function () {
            toastr.warning("Error Loading Employee List!", { timeOut: 2000 });
        });
    };
   
    $scope.GetEmployeeList();


    var methodName = "";
    $scope.SaveData = function () {
        $scope.SaveDb = {};
        $scope.SaveDb.UserId = $scope.UserId;
        $scope.SaveDb.Status = $scope.Status;
        $scope.SaveDb.UserName = $scope.UserName;
        $scope.SaveDb.Password = $scope.Password;
        $scope.SaveDb.EmployeeId = $scope.frmUserInfo.Employee.EmployeeId;
        $scope.SaveDb.AccessLocation = $scope.AccessLocation;
        $scope.SaveDb.LocationId = $scope.frmUserInfo.Location.LocationId;


        if ($scope.UserId === '' || typeof $scope.UserId === 'undefined') {
            methodName = "InsertUserInfo";
        } else {
            methodName = "UpdateUserInfo";
        }
        $http({
            method: "post",
            url: MyApp.rootPath + "UserInfo/" + methodName,
            datatype: "json",
            data: JSON.stringify($scope.SaveDb)
        }).then(function (response) {
            if (response.data.Status === "Ok") {
                $scope.UserId = response.data.ID;
                $scope.btnSaveValue = "Update";
                $scope.SearchData();
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


    $scope.SearchData = function () {
        $http({
            method: "POST",
            url: MyApp.rootPath + "UserInfo/GetUserList",
            datatype: "json"
        }).then(function (response) {
            if (response.data.Status === "" || response.data.Status === null || response.data.Status === undefined) {
                if (response.data.length > 0) {
                    //response.data.forEach(function (row, index) {
                    //    row.SlNo = index + 1;
                    //});
                    $scope.gridUserInfo.data = response.data;

                } else {
                    toastr.warning("No Data Found!", { timeOut: 2000 });
                }
            }
            else {
                toastr.warning(response.data.Status, { timeOut: 2000 });
            }
        }, function () {
            toastr.warning("Error Loading User List!", { timeOut: 2000 });
        });
    };
    $scope.SearchData();


    $scope.Reset = function () {

        $scope.btnSaveValue = "Save";
        $scope.Status = "Active";
        $scope.UserId = "";
        $scope.UserName = "";
        $scope.Password = "";
        //$scope.AccessLocation = "";
        $scope.frmUserInfo.Employee = undefined;
        //$scope.frmUserInfo.Location = undefined;


        //$scope.frmUserInfo.Depot = undefined;
        //$scope.frmUserInfo.Zone = undefined;
        //$scope.frmUserInfo.Region = undefined;
        //$scope.frmUserInfo.Area = undefined;
        //$scope.frmUserInfo.Territory = undefined;

        //$scope.Locations = [];

    }


});