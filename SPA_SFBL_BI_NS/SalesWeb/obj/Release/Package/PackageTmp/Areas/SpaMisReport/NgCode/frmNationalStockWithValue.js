app.controller("NationalStockWithValueCtrl", function ($scope, $http, uiGridConstants) {

    var methodName = "";
    $scope.EventPerm(22);

    $scope.GetReportDownLoadStatus = function () {
        $http({
            method: "GET",
            url: MyApp.rootPath + "RetailerIms/GetReportDownLoadStatus",
            datatype: "json",
            params: { url: window.location.pathname }
        }).then(function (response) {
            if (response.data.length > 0) {
                $scope.gridNationalStockWithValue.enableGridMenu = response.data[0].DownLoadStatus;
            }
            //else {
            //    toastr.warning("No Data Found!", { timeOut: 2000 });
            //}
        }, function (response) {
            if (response.status === 404) {
                toastr.warning("Error Loading Report Download Status!", { timeOut: 2000 });
            }
        });
    };
    $scope.GetReportDownLoadStatus();

    var columnNationalStockWithValue = [
        //{ name: 'SlNo', displayName: "Sl. No", type: 'number', cellClass: 'grid-align', footerCellFilter: 'number:2' },
        { name: 'ProductCode', displayName: "Product Code", aggregationType: uiGridConstants.aggregationTypes.count, footerCellFilter: 'number:0'},
        { name: 'ProductName', displayName: "Product Name" },
        { name: 'PackSize', displayName: "Pack Size"},
        { name: 'UnitTp', displayName: "Unit TP", cellFilter: 'number:2', cellClass: 'grid-align', footerCellFilter: 'number:2' },
        { name: 'StockQty', displayName: "Stock Qty", cellClass: 'grid-align', footerCellFilter: 'number:2' },
        { name: 'StockValue', displayName: "Stock Value", cellClass: 'grid-align', footerCellFilter: 'number:2' }
    ];
    $scope.gridNationalStockWithValue = {
        showColumnFooter: true,
        enableFiltering: true,
        enableSorting: true,
        columnDefs: columnNationalStockWithValue,
        enableGridMenu: true,
        enableSelectAll: true,
        exporterCsvFilename: 'National_Stock_With_Value.csv',
        exporterMenuPdf: false,
        exporterCsvLinkElement: angular.element(document.querySelectorAll(".custom-csv-link-location")),
        onRegisterApi: function (gridApi) {
            $scope.gridApi = gridApi;
        }
    };


    $scope.GetNationalStockWithValue = function () {

        methodName = "GetNationalStockWithValue";

  

        $http({
            method: "POST",
            url: MyApp.rootPath + "NationalStockWithValue/" + methodName
        }).then(function (response) {
            if (response.data.length > 0) {
                $scope.gridNationalStockWithValue.data = response.data;

            }
            else {
                toastr.warning("No Data Found!", '');
                $scope.gridNationalStockWithValue.data = [];
            }
        }, function () {
            toastr.error("Error!");
        });
    };


    $scope.Reset = function () {
       
        $scope.gridNationalStockWithValue.data = [];

        $scope.ReportType = "";


    };

});