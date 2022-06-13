app.controller("DistBonusAdjProcessFinalizeCtrl", function ($scope, $http) {

    $scope.Status = "Active";
    $scope.btnSaveValue = "Save";
    $scope.isDisabled = false;
    $scope.isSaveDisable = false;
    $scope.isDisabled = true;


    $scope.GetProcessList = function () {
        $http({
            method: "GET",
            url: MyApp.rootPath + "DistBonusAdjProcessFinalize/GetProcessList"
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
        $scope.ProcessSlno = $scope.frmDistBonusAdjProcessFinalize.Process.ProcessSlno;
        $scope.ProcessNo = $scope.frmDistBonusAdjProcessFinalize.Process.ProcessNo;
        $scope.ProcessDate = $scope.frmDistBonusAdjProcessFinalize.Process.ProcessDate;
        $scope.BonusStartDate = $scope.frmDistBonusAdjProcessFinalize.Process.BonusStartDate;
        $scope.BonusEndDate = $scope.frmDistBonusAdjProcessFinalize.Process.BonusEndDate;

        $scope.ApprovedStatus = $scope.frmDistBonusAdjProcessFinalize.Process.ApprovedStatus;
        $scope.ApprovedDate = $scope.frmDistBonusAdjProcessFinalize.Process.ApprovedDate;

        $scope.ProcessRunStatus = $scope.frmDistBonusAdjProcessFinalize.Process.ApprovedDate;
        $scope.ProcessRunDate = $scope.frmDistBonusAdjProcessFinalize.Process.ApprovedDate;

        $scope.ProcessFinalizeStatus = "Yes";
        $scope.ProcessFinalizeDate = CurrentDate;
    }






    $scope.SaveData = function () {

        if ($scope.ProcessFinalizeDate === "" || typeof $scope.ProcessFinalizeDate === "undefined") {

            toastr.warning("Process Finalize Date Cannot be empty!");
            return false;
        }

        $scope.SaveDb = {};
        $scope.SaveDb.ProcessSlno = $scope.ProcessSlno;
        $scope.SaveDb.ProcessNo = $scope.ProcessNo;   
        $scope.SaveDb.ProcessFinalizeStatus = $scope.ProcessFinalizeStatus;
        $scope.SaveDb.ProcessFinalizeDate = $scope.ProcessFinalizeDate;

        if ($scope.ProcessSlno === "" || typeof $scope.ProcessSlno === "undefined") {
            $http({
                method: "POST",
                url: MyApp.rootPath + "DistBonusAdjProcessFinalize/InsertDistBonusAdjProcessFinalize",
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
                url: MyApp.rootPath + "DistBonusAdjProcessFinalize/UpdateDistBonusAdjProcessFinalize",
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


    var columnSearch = [
        { name: 'ProcessSlno', displayName: "Process Sl.No", visible: true },
        { name: 'ProcessNo', displayName: "Process No", visible: true },
        { name: 'ProcessDate', displayName: "Process Date", visible: true },
        { name: 'BonusStartDate', displayName: "Bonus Start Date", visible: true },
        { name: 'BonusEndDate', displayName: "Bonus End Date", visible: true },
        { name: 'ApprovedStatus', displayName: "Approved Status", visible: true },
        { name: 'ApprovedDate', displayName: "Approved Date", visible: true },

        { name: 'ProcessRunStatus', displayName: "Process Run Status", visible: true },
        { name: 'ProcessRunDate', displayName: "Process Run Date", visible: true },

        { name: 'ProcessFinalizeStatus', displayName: "Process Finalize Status", visible: true },
        { name: 'ProcessFinalizeDate', displayName: "Process Finalize Date", visible: true }
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
            url: MyApp.rootPath + "DistBonusAdjProcessFinalize/SearchData",
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


    $scope.Reset = function () {
        $scope.frmDistBonusAdjProcessFinalize.Process = undefined;
        $scope.Process = [];
        $scope.GetProcessList();
        $scope.btnSaveValue = "Save";
        $scope.ProcessSlno = "";
        $scope.ProcessNo = "";
        $scope.ProcessDate = "";
        $scope.BonusStartDate = "";
        $scope.BonusEndDate = "";
        $scope.isDisabled = true;
        $scope.isSaveDisable = false;
        
        $scope.ApprovedStatus = "";
        $scope.ApprovedDate = "";
        $scope.ProcessRunStatus = "";
        $scope.ProcessRunDate = "";
        $scope.ProcessFinalizeStatus = "";
        $scope.ProcessFinalizeDate = "";

    };

});