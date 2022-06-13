app.controller("DistBonusAdjClaimCtrl", function ($scope, $http) {

    $scope.btnSaveValue = "View";

    var columnSearch = [
        { name: 'ProcessNo', displayName: "Process No", width:"100", visible: true },
        { name: 'ProcessDate', displayName: "Process Date", width: "100", visible: true },
        { name: 'ClaimNo', displayName: "Claim No", width: "100", visible: true },
        { name: 'ClaimDate', displayName: "Claim Date", width: "100", visible: true },
        { name: 'BonusStartDate', displayName: "Bonus Start Date", width: "120", visible: true },
        { name: 'BonusEndDate', displayName: "Bonus End Date", width: "120", visible: true },
        { name: 'CustomerCode', displayName: "Customer Code", width: "120", visible: true, cellClass: 'grid-align'},
        { name: 'CustomerName', displayName: "Customer Name", width: "150", visible: true },
        { name: 'FactoryCode', displayName: "Factory Code", width: "100", visible: true, cellClass: 'grid-align' },
        { name: 'FactoryName', displayName: "Factory Name", width: "100", visible: true },
        { name: 'ApprovedDate', displayName: "Approved Date", width: "100", visible: true },
        { name: 'ProcessRunDate', displayName: "Process Run Date", width: "120", visible: true }, 
        { name: 'ProductCode', displayName: "Product Code", width: "100", visible: true },
        { name: 'ProductName', displayName: "Product Name", width: "150",  visible: true },
        { name: 'PackSize', displayName: "Pack Size", width: "100",  visible: true },
        { name: 'BonusQty', displayName: "Bonus Qty", width: "100", visible: true, cellClass: 'grid-align' }
    ];
    $scope.gridSearch = {
        showGridFooter: true,
        enableFiltering: true,
        enableSorting: true,
        enableGridMenu: true,
        enableFiltering: true,
        exporterCsvFilename: 'Distributor_Wise_Bonus_Adjustment_Claim_Report.csv',
        columnDefs: columnSearch
    };

    $scope.GetProcessList = function () {
        $http({
            method: "GET",
            url: MyApp.rootPath + "DistBonusAdjClaim/GetProcessList"
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
        $scope.ProcessNo = $scope.frmDistBonusAdjClaim.Process.ProcessNo;
        $scope.ProcessDate = $scope.frmDistBonusAdjClaim.Process.ProcessDate;
        $scope.BonusStartDate = $scope.frmDistBonusAdjClaim.Process.BonusStartDate;
        $scope.BonusEndDate = $scope.frmDistBonusAdjClaim.Process.BonusEndDate;
        $scope.ApprovedStatus = $scope.frmDistBonusAdjClaim.Process.ApprovedStatus;
        $scope.ApprovedDate = $scope.frmDistBonusAdjClaim.Process.ApprovedDate;
        $scope.ProcessRunStatus = $scope.frmDistBonusAdjClaim.Process.ProcessRunStatus;
        $scope.ProcessRunDate = $scope.frmDistBonusAdjClaim.Process.ProcessRunDate;
        $scope.GetCustomerList();
    }

    $scope.GetCustomerList = function () {
        $http({
            method: "GET",
            url: MyApp.rootPath + "DistBonusAdjClaim/GetCustomerList",
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
            url: MyApp.rootPath + "DistBonusAdjClaim/GetData",
            params: { ProcessNo: $scope.ProcessNo, CustomerCode: $scope.frmDistBonusAdjClaim.Customer.CustomerCode }
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

        $scope.frmDistBonusAdjClaim.Process = undefined;
        $scope.frmDistBonusAdjClaim.Customer = undefined;        
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