app.controller("DistBonusAdjUnitSelectionCtrl", function ($scope, $http) {

    $scope.Status = "Active";
    $scope.btnSaveValue = "Process Run";

    $scope.isDisabled = false;

    $scope.isSaveDisable = false;
    $scope.isDisabled = true;
    $scope.ApprovedStatus = "";

    var columnSearch = [
        { name: 'ProcessSlno', displayName: "Process Sl.No", visible: false },
        { name: 'ProcessNo', displayName: "Process No", width: "100", visible: true },
        { name: 'ProcessDate', displayName: "Process Date", width: "100", visible: true },
        { name: 'BonusStartDate', displayName: "Bonus Start Date", visible: true },
        { name: 'BonusEndDate', displayName: "Bonus End Date", visible: true },
        { name: 'ApprovedStatus', displayName: "Approved Status", visible: true },
        { name: 'ApprovedDate', displayName: "Approved Date", visible: true },
        { name: 'ProcessRunStatus', displayName: "Process Run Status", visible: true },
        { name: 'ProcessRunDate', displayName: "Process Run Date", visible: true }
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

        if (row.entity.ProcessRunStatus === "Yes") {

            toastr.warning("Already process run, you can not modify!", { timeOut: 2000 });
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
            $scope.ProcessRunStatus = "Yes";
            $scope.ProcessRunDate = CurrentDate;
            $scope.btnSaveValue = "Process Run";
            $scope.isDisabled = true;
            $scope.isSaveDisable = false;
        }
    }

    $scope.SearchData = function () {
        $http({
            method: "GET",
            url: MyApp.rootPath + "DistBonusAdjUnitSelection/SearchData",
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


    $scope.ProcessRun = function () {

        $http({
            method: "POST",
            url: MyApp.rootPath + "DistBonusAdjUnitSelection/ProcessRun",
            data: { process_slno: $scope.ProcessSlno, process_run_date: $scope.ProcessRunDate }
        }).then(function (response) {
            if (response.data.Status === "Ok") {
                $scope.isSaveDisable = true;
                toastr.success(response.data.Message, { timeOut: 2000 });
                $scope.SearchData();
            } else {
                toastr.warning(response.data.Status, { timeOut: 2000 });
            }
        }, function (response) {
            if (response.status === 404) {
                toastr.warning("Error Process Run!", { timeOut: 2000 });
            }
        });

    };


    $scope.Reset = function () {
        $scope.btnSaveValue = "Process Run";
        $scope.ProcessSlno = "";
        $scope.ProcessNo = "";
        $scope.ProcessDate = "";
        $scope.BonusStartDate = "";
        $scope.BonusEndDate = "";
        $scope.ApprovedStatus = "";
        $scope.ApprovedDate = "";
        $scope.ProcessRunStatus = "";
        $scope.ProcessRunDate = "";
        $scope.isDisabled = true;
        $scope.isSaveDisable = false;
    };

});