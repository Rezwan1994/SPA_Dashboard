app.controller("DistBonusProcessApprovedCtrl", function ($scope, $http) {

    $scope.Status = "Active";
    $scope.btnSaveValue = "Save";

    $scope.isDisabled = false;

    $scope.isSaveDisable = false;
    $scope.isDisabled = true;


    $scope.GetProcessList = function () {
        $http({
            method: "GET",
            url: MyApp.rootPath + "DistBonusProcessApproved/GetProcessList"
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
        $scope.ProcessSlno = $scope.frmDistBonusProcessApproved.Process.ProcessSlno;
        $scope.ProcessNo = $scope.frmDistBonusProcessApproved.Process.ProcessNo;
        $scope.ProcessDate = $scope.frmDistBonusProcessApproved.Process.ProcessDate;
        $scope.BonusStartDate = $scope.frmDistBonusProcessApproved.Process.BonusStartDate;
        $scope.BonusEndDate = $scope.frmDistBonusProcessApproved.Process.BonusEndDate;
        $scope.ApprovedStatus = "Approved";
        $scope.ApprovedDate = CurrentDate;
    }


    $scope.SaveData = function () {

        if ($scope.ApprovedDate === "" || typeof $scope.ApprovedDate === "undefined") {

            toastr.warning("Approved Date Cannot be empty!");
            return false;
        }

        $scope.SaveDb = {};
        $scope.SaveDb.ProcessSlno = $scope.ProcessSlno;
        $scope.SaveDb.ProcessNo = $scope.ProcessNo;   
        $scope.SaveDb.ApprovedStatus = $scope.ApprovedStatus;
        $scope.SaveDb.ApprovedDate = $scope.ApprovedDate;

        if ($scope.ProcessSlno === "" || typeof $scope.ProcessSlno === "undefined") {
            $http({
                method: "POST",
                url: MyApp.rootPath + "DistBonusProcessApproved/InsertDistBonusProcessApproved",
                datatype: "json",
                data: JSON.stringify($scope.SaveDb)
            }).then(function (response) {
                if (response.data.Status === "Ok") {
                    $scope.ProcessSlno = response.data.Id;
                    $scope.ProcessNo = response.data.Code;                   
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
                url: MyApp.rootPath + "DistBonusProcessApproved/UpdateDistBonusProcessApproved",
                datatype: "json",
                data: JSON.stringify($scope.SaveDb)
            }).then(function (response) {
                if (response.data.Status === "Ok") {
                    $scope.isSaveDisable = true;
                    $scope.Process = [];
                    $scope.GetProcessList();
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
        { name: 'ApprovedDate', displayName: "Approved Date", visible: true }
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
            url: MyApp.rootPath + "DistBonusProcessApproved/SearchData",
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
        $scope.btnSaveValue = "Save";
        $scope.ProcessSlno = "";
        $scope.ProcessNo = "";
        $scope.ProcessDate = "";
        $scope.BonusStartDate = "";
        $scope.BonusEndDate = "";
        $scope.isDisabled = true;
        $scope.isSaveDisable = false;
        $scope.frmDistBonusProcessApproved.Process = undefined;
        $scope.ApprovedStatus = "";
        $scope.ApprovedDate = "";
    };

});