app.controller("DistBonusProcessRunCtrl", function ($scope, $http) {

    $scope.Status = "Active";
    $scope.btnSaveValue = "Process Run";

    $scope.isDisabled = false;

    $scope.isSaveDisable = false;
    $scope.isDisabled = true;
    //$scope.ApprovedStatus = "";


    $scope.GetProcessList = function () {
        $http({
            method: "GET",
            url: MyApp.rootPath + "DistBonusProcessRun/GetProcessList"
        }).then(function (response) {
            if (response.data.Status === "" || response.data.Status === null) {
                if (response.data.Data.length > 0) {
                    $scope.Process = response.data.Data;
                }
            } else {
                toastr.warning(response.data.Status, { timeOut: 2000 });
            }
        }, function (response) {
            if (response.status === 404) {
                toastr.warning("Error Loading Process List!", { timeOut: 2000 });
            }
        });
    }
    $scope.GetProcessList();

    $scope.OnProcessNoClick = function () {
        $scope.ProcessSlno = $scope.frmDistBonusProcessRun.Process.ProcessSlno;
        $scope.ProcessNo = $scope.frmDistBonusProcessRun.Process.ProcessNo;
        $scope.ProcessDate = $scope.frmDistBonusProcessRun.Process.ProcessDate;
        $scope.BonusStartDate = $scope.frmDistBonusProcessRun.Process.BonusStartDate;
        $scope.BonusEndDate = $scope.frmDistBonusProcessRun.Process.BonusEndDate;
        $scope.ApprovedStatus = $scope.frmDistBonusProcessRun.Process.ApprovedStatus;
        $scope.ApprovedDate = $scope.frmDistBonusProcessRun.Process.ApprovedDate;

        $scope.ProcessRunStatus = "Yes";
        $scope.ProcessRunDate = CurrentDate;
    }



    var columnSearch = [
        { name: 'ProcessSlno', displayName: "Process Sl.No", visible: false },
        { name: 'ProcessNo', displayName: "Process No", width:"100", visible: true },
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
        columnDefs: columnSearch
    };

    $scope.SearchData = function () {
        $http({
            method: "GET",
            url: MyApp.rootPath + "DistBonusProcessRun/SearchData",
        }).then(function (response) {
            if (response.data.Data.length > 0) {
                $scope.gridSearch.data = response.data.Data;
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
            url: MyApp.rootPath + "DistBonusProcessRun/ProcessRun",
            data: { process_slno: $scope.ProcessSlno, process_run_date: $scope.ProcessRunDate }
        }).then(function (response) {
            if (response.data.Status === "Ok") {
                $scope.isSaveDisable = true;
                toastr.success(response.data.Message, { timeOut: 2000 });
                $scope.Process = [];
                $scope.GetProcessList();            
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

        $scope.frmDistBonusProcessRun.Process = undefined;

    };

});