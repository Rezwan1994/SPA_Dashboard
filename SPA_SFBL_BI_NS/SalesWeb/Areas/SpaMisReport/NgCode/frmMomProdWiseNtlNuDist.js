app.controller("MomProdWiseNtlNuDistCtrl", function ($scope, $http, $interval, uiGridConstants, $filter) {

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
                $scope.gridMomProdWiseNtlNuDist.enableGridMenu = response.data[0].DownLoadStatus;
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

    $scope.GetReportTypeList = function () {
        $http({
            method: "GET",
            url: MyApp.rootPath + "DistSrSales/GetReportTypeList",
            params: { SlNo: "(21,25)" }
        }).then(function (response) {
            if (response.data.Data.length > 0) {
                $scope.RepTypes = response.data.Data;
            } else {
                toastr.warning("No Data Found!", { timeOut: 2000 });
            }
        }, function (response) {
            if (response.status === 404) {
                toastr.warning("Error!", { timeOut: 2000 });
            }
        });
    }
    $scope.GetReportTypeList();

    $scope.GetBaseProductList = function () {
        $http({
            method: "GET",
            url: MyApp.rootPath + "MomProdWiseNtlNuDist/GetBaseProductList"
        }).then(function (response) {
            if (response.data.Data.length > 0) {
                $scope.Products = response.data.Data;
            } else {
                toastr.warning("No Data Found!", { timeOut: 2000 });
            }

        }, function (response) {
            if (response.status === 404) {
                toastr.warning("Error Loading Type List!", { timeOut: 2000 });
            }
        });
    }
    $scope.GetBaseProductList();

    $scope.GetMomProdWiseNtlNuDist = function () {

        if ($scope.frmMomProdWiseNtlNuDist.RepType.ReportTypeValue == "MomProductWiseNtlNumericDistribution") {

            $http({
                method: "POST",
                url: MyApp.rootPath + "MomProdWiseNtlNuDist/GetMomProdWiseNtlNuDist",
                data: {
                    BaseProductCode: $scope.frmMomProdWiseNtlNuDist.Product.Code
                }
            }).then(function (response) {
                if (response.data.length > 0) {
                    $scope.gridMomProdWiseNtlNuDist.data = response.data;
                }
                else {
                    toastr.warning("No Data Found!", '');
                    $scope.gridMomProdWiseNtlNuDist.data = [];
                }
            }, function (response) {
                toastr.error("Error!");
            });
        }

        if ($scope.frmMomProdWiseNtlNuDist.RepType.ReportTypeValue == "MomProductWiseNtlNumericDistributionLy") {
            $http({
                method: "POST",
                url: MyApp.rootPath + "MomProdWiseNtlNuDist/GetMomProdWiseNtlNuDistLy",
                data: {
                    BaseProductCode: $scope.frmMomProdWiseNtlNuDist.Product.Code
                }
            }).then(function (response) {
                if (response.data.length > 0) {
                    $scope.gridMomProdWiseNtlNuDist.data = response.data;
                }
                else {
                    toastr.warning("No Data Found!", '');
                    $scope.gridMomProdWiseNtlNuDist.data = [];
                }
            }, function (response) {
                toastr.error("Error!");
            });
        }
    };




    //Grid
    var columnMomProdWiseNtlNuDist = [

        { name: 'ProductCode', displayName: "Product Code", width: 150, aggregationType: uiGridConstants.aggregationTypes.count, footerCellFilter: 'number:0' },
        { name: 'ProductName', displayName: "Product Name", width: 150 },
        { name: 'Jan', displayName: "January", width: 120, cellFilter: 'number:0', cellClass: 'grid-align', footerCellFilter: 'number:0', aggregationType: uiGridConstants.aggregationTypes.sum },
        { name: 'Feb', displayName: "Febuary", width: 120, cellFilter: 'number:0', cellClass: 'grid-align', footerCellFilter: 'number:0', aggregationType: uiGridConstants.aggregationTypes.sum },
        { name: 'Mar', displayName: "March", width: 120, cellFilter: 'number:0', cellClass: 'grid-align', footerCellFilter: 'number:0', aggregationType: uiGridConstants.aggregationTypes.sum },
        { name: 'Apr', displayName: "April", width: 120, cellFilter: 'number:0', cellClass: 'grid-align', footerCellFilter: 'number:0', aggregationType: uiGridConstants.aggregationTypes.sum },
        { name: 'May', displayName: "May", width: 120, cellFilter: 'number:0', cellClass: 'grid-align', footerCellFilter: 'number:0', aggregationType: uiGridConstants.aggregationTypes.sum },
        { name: 'Jun', displayName: "June", width: 120, cellFilter: 'number:0', cellClass: 'grid-align', footerCellFilter: 'number:0', aggregationType: uiGridConstants.aggregationTypes.sum },
        { name: 'Jul', displayName: "July", width: 120, cellFilter: 'number:0', cellClass: 'grid-align', footerCellFilter: 'number:0', aggregationType: uiGridConstants.aggregationTypes.sum },
        { name: 'Aug', displayName: "August", width: 120, cellFilter: 'number:0', cellClass: 'grid-align', footerCellFilter: 'number:0', aggregationType: uiGridConstants.aggregationTypes.sum },
        { name: 'Sep', displayName: "September", width: 120, cellFilter: 'number:0', cellClass: 'grid-align', footerCellFilter: 'number:0', aggregationType: uiGridConstants.aggregationTypes.sum },
        { name: 'Oct', displayName: "October", width: 120, cellFilter: 'number:0', cellClass: 'grid-align', footerCellFilter: 'number:0', aggregationType: uiGridConstants.aggregationTypes.sum },
        { name: 'Nov', displayName: "November", width: 120, cellFilter: 'number:0', cellClass: 'grid-align', footerCellFilter: 'number:0', aggregationType: uiGridConstants.aggregationTypes.sum },
        { name: 'Dec', displayName: "December", width: 120, cellFilter: 'number:0', cellClass: 'grid-align', footerCellFilter: 'number:0', aggregationType: uiGridConstants.aggregationTypes.sum },
        { name: 'Total', displayName: "Total", width: 120, cellFilter: 'number:0', cellClass: 'grid-align', footerCellFilter: 'number:0', aggregationType: uiGridConstants.aggregationTypes.sum }

    ];
    $scope.gridMomProdWiseNtlNuDist = {
        showColumnFooter: true,
        enableFiltering: true,
        enableSorting: true,
        columnDefs: columnMomProdWiseNtlNuDist,
        enableGridMenu: true,
        enableSelectAll: true,
        exporterCsvFilename: "SKU Wise Numeric Distribution(MOM).csv",
        exporterMenuPdf: false,
        exporterCsvLinkElement: angular.element(document.querySelectorAll(".custom-csv-link-location")),
        onRegisterApi: function (gridApi) {
            $scope.gridApi = gridApi;
        }
    };


    $scope.Reset = function () {
        $scope.FromDate = "";
        $scope.ToDate = "";
        $scope.gridMomProdWiseNtlNuDist.data = [];
        $scope.isDisabled = true;
        $scope.ReportType = "";
        $scope.frmMomProdWiseNtlNuDist.RepType = undefined;
        $scope.frmMomProdWiseNtlNuDist.Product = undefined;
    };

});