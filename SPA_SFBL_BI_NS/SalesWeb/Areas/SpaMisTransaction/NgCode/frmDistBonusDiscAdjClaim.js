app.controller("DistBonusDiscAdjClaimCtrl", function ($scope, $http, uiGridConstants) {

    $scope.btnSaveValue = "View";

    var columnSearch = [
        { name: 'ProcessNo', displayName: "Process No", width: 150, aggregationType: uiGridConstants.aggregationTypes.count, footerCellFilter: 'number:0'},
        { name: 'ProcessDate', displayName: "Process Date", width: "100", visible: true },
        { name: 'BonusDiscStartDate', displayName: "Bonus Disc Start Date", width: "120", visible: true },
        { name: 'BonusDiscEndDate', displayName: "Bonus Disc End Date", width: "120", visible: true },
        { name: 'ClaimNo', displayName: "Claim No", width: "100", visible: true },
        { name: 'ClaimDate', displayName: "Claim Date", width: "100", visible: true },
        { name: 'CustomerCode', displayName: "Customer Code", width: "120", visible: true, cellClass: 'grid-align' },
        { name: 'CustomerName', displayName: "Customer Name", width: "150", visible: true },
        { name: 'FactoryCode', displayName: "Factory Code", width: "100", visible: true, cellClass: 'grid-align' },
        { name: 'FactoryName', displayName: "Factory Name", width: "100", visible: true },
        { name: 'ApprovedDate', displayName: "Approved Date", width: "100", visible: true },
        { name: 'ProcessRunDate', displayName: "Process Run Date", width: "120", visible: true },        
        { name: 'BonusDiscAmt', displayName: "Bonus Disc Amt", width: 120, cellFilter: 'number:2', aggregationType: uiGridConstants.aggregationTypes.sum, cellClass: 'grid-align', footerCellFilter: 'number:2' }
    ];
    $scope.gridSearch = {
        //showGridFooter: true,
        showColumnFooter: true,
        enableFiltering: true,
        enableSorting: true,
        enableGridMenu: true,
        enableFiltering: true,
        exporterCsvFilename: 'Distributor_Wise_Bonus_Disc_Adjustment_Claim_Report.csv',
        columnDefs: columnSearch
    };

    $scope.GetProcessList = function () {
        $http({
            method: "GET",
            url: MyApp.rootPath + "DistBonusDiscAdjClaim/GetProcessList"
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
        $scope.ProcessNo = $scope.frmDistBonusDiscAdjClaim.Process.ProcessNo;
        $scope.ProcessDate = $scope.frmDistBonusDiscAdjClaim.Process.ProcessDate;
        $scope.BonusStartDate = $scope.frmDistBonusDiscAdjClaim.Process.BonusStartDate;
        $scope.BonusEndDate = $scope.frmDistBonusDiscAdjClaim.Process.BonusEndDate;
        $scope.ApprovedStatus = $scope.frmDistBonusDiscAdjClaim.Process.ApprovedStatus;
        $scope.ApprovedDate = $scope.frmDistBonusDiscAdjClaim.Process.ApprovedDate;
        $scope.ProcessRunStatus = $scope.frmDistBonusDiscAdjClaim.Process.ProcessRunStatus;
        $scope.ProcessRunDate = $scope.frmDistBonusDiscAdjClaim.Process.ProcessRunDate;
        $scope.GetCustomerList();
    }

    $scope.GetCustomerList = function () {
        $http({
            method: "GET",
            url: MyApp.rootPath + "DistBonusDiscAdjClaim/GetCustomerList",
            params: { param: " AND PROCESS_NO = '" + $scope.ProcessNo + "' " }
        }).then(function (response) {
            if (response.data.Status === "" || response.data.Status === null) {
                if (response.data.Data.length > 0) {
                    $scope.Customers = response.data.Data;
                } else {
                    toastr.warning("No Data Found!", { timeOut: 2000 });
                }
            } else {
                toastr.warning(response.data.Status, { timeOut: 2000 });
            }
        }, function (response) {
            if (response.status === 404) {
                toastr.warning("Error Loading Customer List!", { timeOut: 2000 });
            }
        });
    }

    $scope.OnCustomerClick = function () {
        $http({
            method: "GET",
            url: MyApp.rootPath + "DistBonusDiscAdjClaim/GetData",
            params: { ProcessNo: $scope.ProcessNo, CustomerCode: $scope.frmDistBonusDiscAdjClaim.Customer.CustomerCode }
        }).then(function (response) {
            if (response.data.length > 0) {
                $scope.gridSearch.data = response.data;
            } else {
                toastr.warning("No Data Found!", { timeOut: 2000 });
            }
        }, function (response) {
            if (response.status === 404) {
                toastr.warning("Error Loading Grid Data!", { timeOut: 2000 });
            }
        });
    }


    $scope.Reset = function () {
        $scope.btnSaveValue = "View";

        $scope.frmDistBonusDiscAdjClaim.Process = undefined;
        $scope.frmDistBonusDiscAdjClaim.Customer = undefined;
        $scope.Customers = [];

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
        $scope.gridSearch.data = [];
    };

});