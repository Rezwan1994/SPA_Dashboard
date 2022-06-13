app.controller("MomSkuWiseNtlNumericDistributionCtrl", function ($scope, $http, $interval, uiGridConstants, $filter) {

    $scope.isDisabled = true;
    var xl_file_name = "";
    var methodName = "";
    var ProductLis = "";
    $scope.EventPerm(22);

    $scope.FromDate = "";


    $scope.GetReportDownLoadStatus = function () {
        $http({
            method: "GET",
            url: MyApp.rootPath + "RetailerIms/GetReportDownLoadStatus",
            datatype: "json",
            params: { url: window.location.pathname }
        }).then(function (response) {
            if (response.data.length > 0) {
                $scope.gridMomSkuWiseNtlNumericDistribution.enableGridMenu = response.data[0].DownLoadStatus;
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
            params: { SlNo: "(18,28)" }
        }).then(function (response) {
            if (response.data.Data.length > 0) {
                $scope.RepTypes = response.data.Data;
            } else {
                toastr.warning("No Data Found!", { timeOut: 2000 });
            }
        }, function (response) {
            if (response.status === 404) {
                toastr.warning("Error Loading Division List!", { timeOut: 2000 });
            }
        });
    }
    $scope.GetReportTypeList();

    $scope.OnReportTypeChange = function () {
        if ($scope.frmMomSkuWiseNtlNumericDistribution.RepType.ReportTypeValue == "MomSKUWiseNtlNumericDistribution" || $scope.frmMomSkuWiseNtlNumericDistribution.RepType.ReportTypeValue == "MomSKUWiseNtlNumericDistributionLy") {
            $scope.isDisabled = true;
            $scope.FromDate = "";
            $scope.ToDate = "";
        }
        else {
            $scope.FromDate = "";
            $scope.ToDate = "";
            $scope.isDisabled = false;
        }
        $scope.GetDivisionList();
    };

    $scope.GetSkuWiseNumericDistNtlMom = function () {

        if ($scope.frmMomSkuWiseNtlNumericDistribution.RepType.ReportTypeValue == "MomSKUWiseNtlNumericDistribution") {
            methodName = "GetMomSkuWiseNtlNumericDistribution";

        }
        if ($scope.frmMomSkuWiseNtlNumericDistribution.RepType.ReportTypeValue == "MomSKUWiseNtlNumericDistributionLy") {
            methodName = "GetMomSkuWiseNtlNumericDistributionLy";

        }
        $http({
            method: "POST",
            url: MyApp.rootPath + "MomSkuWiseNtlNumericDistribution/" + methodName,
            data: {
                pCode: $scope.frmMomSkuWiseNtlNumericDistribution.Product.ProductCode
            }
        }).then(function (response) {
            if (response.data.length > 0) {
                $scope.gridMomSkuWiseNtlNumericDistribution.data = response.data;
            }
            else {
                toastr.warning("No Data Found!", '');
                $scope.gridMomSkuWiseNtlNumericDistribution.data = [];
            }
        }, function (response) {
            toastr.error("Error!");
        });
    };

 
    $scope.GetProductList = function () {

        $http({
            method: "GET",
            url: MyApp.rootPath + "ProductBonus/GetProductList"
        }).then(function (response) {
            if (response.data.Data.length > 0) {
                $scope.Products = response.data.Data;
            } else {
                toastr.warning("No Data Found!", { timeOut: 2000 });
            }
        }, function (response) {
            if (response.status === 404) {
                toastr.warning("Error Loading Product List!", { timeOut: 2000 });
            }
        });
    }

    $scope.GetProductList();

    //Grid
    var columnMomSkuWiseNtlNumericDistribution = [

        { name: 'ProductCode', displayName: "Product Code", width: 150, aggregationType: uiGridConstants.aggregationTypes.count, footerCellFilter: 'number:0'},
        { name: 'ProductName', displayName: "Product Name", width: 150 },
        { name: 'PackSize', displayName: "PackSize", width: 150 },
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
    $scope.gridMomSkuWiseNtlNumericDistribution = {
        showColumnFooter: true,
        enableFiltering: true,
        enableSorting: true,
        columnDefs: columnMomSkuWiseNtlNumericDistribution,
        enableGridMenu: true,
        enableSelectAll: true,
        exporterCsvFilename: "Product Wise National Numeric Distribution(MOM).csv",
        exporterMenuPdf: false,
        exporterCsvLinkElement: angular.element(document.querySelectorAll(".custom-csv-link-location")),
        onRegisterApi: function (gridApi) {
            $scope.gridApi = gridApi;
        }
    };


    $scope.Reset = function () {
        $scope.FromDate = "";
        $scope.ToDate = "";
        $scope.gridMomSkuWiseNtlNumericDistribution.data = [];
        $scope.isDisabled = true;
        $scope.ReportType = "";
        $scope.frmMomSkuWiseNtlNumericDistribution.RepType = undefined;
        //$scope.frmMomSkuWiseNtlNumericDistribution.Division = undefined;
        //$scope.frmMomSkuWiseNtlNumericDistribution.Region = undefined;
        //$scope.frmMomSkuWiseNtlNumericDistribution.Area = undefined;
        //$scope.frmMomSkuWiseNtlNumericDistribution.Territory = undefined;
        //$scope.frmMomSkuWiseNtlNumericDistribution.Customer = undefined;
        $scope.frmMomSkuWiseNtlNumericDistribution.Product = undefined;
        //$scope.Divisions = [];
        //$scope.Regions = [];
        //$scope.Areas = [];
        //$scope.Territories = [];
        //$scope.Customers = [];
        //$scope.Products = [];
    };

});