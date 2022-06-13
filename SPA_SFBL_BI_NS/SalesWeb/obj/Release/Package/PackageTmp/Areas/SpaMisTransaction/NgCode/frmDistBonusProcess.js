app.controller("DistBonusProcessCtrl", function ($scope, $http) {

    $scope.Status = "Active";
    $scope.btnSaveValue = "Save";

    $scope.isDisabled = false;

    $scope.isSaveDisable = false;
    $scope.isDisabled = true;

    $scope.ProcessDate = CurrentDate;

    $scope.SaveData = function () {

        if ($scope.BonusStartDate === "" || typeof $scope.BonusStartDate === "undefined") {

            toastr.warning("Bonus Start Date Cannot be empty!");
            return false;
        }

        if ($scope.BonusEndDate === "" || typeof $scope.BonusEndDate === "undefined") {

            toastr.warning("Bonus End Date Cannot be empty!");
            return false;
        }

        if ($scope.ProcessDate === "" || typeof $scope.ProcessDate === "undefined") {

            toastr.warning("Bonus Process Date Cannot be empty!");
            return false;
        }

        var splitProcessDate = new Date($scope.ProcessDate.split("/").reverse().join("-"));
        var pDate = new Date(splitProcessDate.getFullYear(), splitProcessDate.getMonth(), splitProcessDate.getDate());

        var splitStartDate = new Date($scope.BonusStartDate.split("/").reverse().join("-"));
        var sDate = new Date(splitStartDate.getFullYear(), splitStartDate.getMonth(), splitStartDate.getDate());

        var splitEndDate = new Date($scope.BonusEndDate.split("/").reverse().join("-"));
        var eDate = new Date(splitEndDate.getFullYear(), splitEndDate.getMonth(), splitEndDate.getDate());

        var todayDate = new Date(new Date().getFullYear(), new Date().getMonth(), new Date().getDate());

        

        if (sDate > eDate) {
            toastr.warning("Start Date Cannot be Greater Than End Date !");
            return false;
        }

        if (sDate > todayDate) {
            toastr.warning("Start Date Cannot be Greater Than Current Date !");
            return false;
        }


        if (eDate > todayDate) {
            toastr.warning("End Date Cannot be Greater Than Current Date !");
            return false;
        }


        if (pDate < todayDate ) {
            toastr.warning("Process Date Cannot be Lesser Than Current Date !");
            return false;
        }

        if ( eDate > pDate ) {
            toastr.warning("End Date Cannot be Greater Than Process Date  !");
            return false;
        }

        $scope.SaveDb = {};
        $scope.SaveDb.ProcessSlno = $scope.ProcessSlno;
        $scope.SaveDb.ProcessNo = $scope.ProcessNo;
        $scope.SaveDb.ProcessDate = $scope.ProcessDate;
        $scope.SaveDb.BonusStartDate = $scope.BonusStartDate;
        $scope.SaveDb.BonusEndDate = $scope.BonusEndDate;

        if ($scope.ProcessSlno === "" || typeof $scope.ProcessSlno === "undefined") {
            $http({
                method: "POST",
                url: MyApp.rootPath + "DistBonusProcess/InsertDistBonusProcess",
                datatype: "json",
                data: JSON.stringify($scope.SaveDb)
            }).then(function (response) {
                if (response.data.Status === "Ok") {
                    $scope.ProcessSlno = response.data.Id;
                    $scope.ProcessNo = response.data.Code;
                    $scope.ApprovedStatus = "Not Approved";                    
                    $scope.SearchData();
                    $scope.isSaveDisable = true;
                    toastr.success(response.data.Message, { timeOut: 2000 });
                } else {
                    console.log(response);
                    toastr.warning("Error Occurred!", { timeOut: 2000 });
                }
            });
        }
        else {

            $http({
                method: "POST",
                url: MyApp.rootPath + "DistBonusProcess/UpdateDistBonusProcess",
                datatype: "json",
                data: JSON.stringify($scope.SaveDb)
            }).then(function (response) {
                if (response.data.Status === "Ok") {
                    $scope.isSaveDisable = true;
                    $scope.SearchData();
                    toastr.success(response.data.Message, { timeOut: 2000 });
                } else {
                    console.log(response);
                    toastr.warning("Error Occurred!", { timeOut: 2000 });
                }
            });
        }
    };


    $scope.DeleteProcess = function () {


        if ($scope.ProcessNo === "" || typeof $scope.ProcessNo === "undefined") {

            toastr.warning("Please select bonus process!");
            return false;
        }
        

        if ($scope.ApprovedStatus == "Not Approved") {

            $http({
                method: "POST",
                url: MyApp.rootPath + "DistBonusProcess/DeleteProcess",
                params: { processSlno: $scope.ProcessSlno }
            }).then(function (response) {
                if (response.data.Status === "Ok") {
                    $scope.Reset();
                    $scope.gridSearch.data = [];
                    $scope.SearchData();
                    $scope.isSaveDisable = true;
                    toastr.success(response.data.Message, { timeOut: 2000 });
                } else {
                    toastr.warning(response.data.Status, { timeOut: 2000 });
                }
            }, function (response) {
                if (response.status === 404) {
                    toastr.warning("Error Deleting Data!", { timeOut: 2000 });
                }
            });

        }
        else {

            toastr.warning("Process Already Approved!", { timeOut: 2000 });
        };


    };



    var columnSearch = [
        { name: 'ProcessSlno', displayName: "Process Sl.No", visible: true },
        { name: 'ProcessNo', displayName: "Process No", visible: true },
        { name: 'ProcessDate', displayName: "Process Date", visible: true },
        { name: 'BonusStartDate', displayName: "Bonus Start Date", visible: true },
        { name: 'BonusEndDate', displayName: "Bonus End Date", visible: true },
        { name: 'ApprovedStatus', displayName: "Approved Status", visible: true },
        { name: 'ApprovedDate', displayName: "Approved Date", visible: true }
    ];
    $scope.gridSearch = {
        showGridFooter: true,
        enableFiltering: true,
        enableSorting: true,
        enableFiltering: true,
        columnDefs: columnSearch,
        rowTemplate: rowTemplate()
    };

    function rowTemplate() {
        return '<div ng-dblclick="grid.appScope.rowDblClick(row)" >' +
            '  <div ng-repeat="(colRenderIndex, col) in colContainer.renderedColumns track by col.colDef.name" class="ui-grid-cell" ng-class="{ \'ui-grid-row-header-cell\': col.isRowHeader }"  ui-grid-cell></div></div>';
    }
    $scope.rowDblClick = function (row) {

        if (row.entity.ApprovedStatus === "Approved") {

            toastr.warning("Already approved, you can not modify!", { timeOut: 2000 });
            return false;

        }
        else {
            $scope.ProcessSlno = row.entity.ProcessSlno;
            $scope.ProcessNo = row.entity.ProcessNo;
            $scope.ProcessDate = row.entity.ProcessDate;
            $scope.BonusStartDate = row.entity.BonusStartDate;
            $scope.BonusEndDate = row.entity.BonusEndDate;
            $scope.ApprovedStatus = row.entity.ApprovedStatus;
            $scope.ApprovedDate = row.entity.ApprovedDate;
            $scope.btnSaveValue = "Save";
            $scope.isDisabled = true;
            $scope.isSaveDisable = true;
        //$('#SearchModal').modal('hide');
        }


    }
    $scope.SearchData = function () {
        $http({
            method: "GET",
            url: MyApp.rootPath + "DistBonusProcess/SearchData",
        }).then(function (response) {
            if (response.data.Data.length > 0) {
                $scope.gridSearch.data = response.data.Data;
            } else {
                toastr.warning("No Data Found!", { timeOut: 2000 });
            }
        }, function (response) {
            if (response.status === 404) {
                toastr.warning("Error Loading User List!", { timeOut: 2000 });
            }
        });
    }
    $scope.SearchData();


    $scope.GetLastBonusProcessData = function () {
        $http({
            method: "GET",
            url: MyApp.rootPath + "DistBonusProcess/GetLastBonusProcessData"
        }).then(function (response) {
            if (response.data.Status === "" || response.data.Status === null) {
                if (response.data.Data.length > 0) {

                    $scope.BonusStartDate = (response.data.Data[0].BonusStartDate);
                    $scope.BonusEndDate = (response.data.Data[0].BonusEndDate);
                }
            } else {
                toastr.warning(response.data.Status, { timeOut: 2000 });
            }
        }, function (response) {
            if (response.status === 404) {
                toastr.warning("Error Loading Customer Discunt List!", { timeOut: 2000 });
            }
        });
    };
    $scope.GetLastBonusProcessData();

    $scope.Reset = function () {
        $scope.GetLastBonusProcessData();
        $scope.ProcessDate = CurrentDate;
        $scope.btnSaveValue = "Save";
        $scope.ProcessSlno = "";
        $scope.ProcessNo = "";
        $scope.BonusStartDate = "";
        $scope.BonusEndDate = "";
        $scope.ApprovedDate = "";
        $scope.ApprovedStatus = "";
        $scope.isDisabled = true;
        $scope.isSaveDisable = false;
    };

});