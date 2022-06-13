app.controller("MomSkuWiseNumericDistributionCtrl", function ($scope, $http, $interval, uiGridConstants, $filter) {

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
                $scope.gridMomSkuWiseNumericDistribution.enableGridMenu = response.data[0].DownLoadStatus;
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
            params: { SlNo: "(17,29)" }
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
        if ($scope.frmMomSkuWiseNumericDistribution.RepType.ReportTypeValue == "MomSKUWiseNumericDistribution" || $scope.frmMomSkuWiseNumericDistribution.RepType.ReportTypeValue == "MomSKUWiseNumericDistributionLy") {
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

    $scope.GetMomSkuWiseNumericDistribution = function () {

        if ($scope.frmMomSkuWiseNumericDistribution.RepType.ReportTypeValue == "MomSKUWiseNumericDistribution") {
            methodName = "GetMomSKUWiseNumericDistribution";

        }

        if ($scope.frmMomSkuWiseNumericDistribution.RepType.ReportTypeValue == "MomSKUWiseNumericDistributionLy") {
            methodName = "GetMomSKUWiseNumericDistributionLy";

        }

        $http({
            method: "POST",
            url: MyApp.rootPath + "MomSkuWiseNumericDistribution/" + methodName,
            data: {
                dCode: $scope.frmMomSkuWiseNumericDistribution.Division.DivisionCode,
                rCode: $scope.frmMomSkuWiseNumericDistribution.Region.RegionCode,
                aCode: $scope.frmMomSkuWiseNumericDistribution.Area.AreaCode,
                tCode: $scope.frmMomSkuWiseNumericDistribution.Territory.TerritoryCode,
                cCode: $scope.frmMomSkuWiseNumericDistribution.Customer.CustomerCode,
                pCode: $scope.frmMomSkuWiseNumericDistribution.Product.ProductCode
            }
        }).then(function (response) {
            if (response.data.length > 0) {
                $scope.gridMomSkuWiseNumericDistribution.data = response.data;
            }
            else {
                toastr.warning("No Data Found!", '');
                $scope.gridMomSkuWiseNumericDistribution.data = [];
            }
        }, function (response) {
            toastr.error("Error!");
        });
    };

    $scope.GetDivisionList = function () {

        //Division List
        $http({
            method: "GET",
            url: MyApp.rootPath + "DistSrSales/GetDivisionList",
        }).then(function (response) {
            if (response.data.Data.length > 0) {
                $scope.Divisions = response.data.Data;
                //$scope.ReportTypes = response.data.Data;
            } else {
                toastr.warning("No Data Found!", { timeOut: 2000 });
            }

        }, function (response) {
            if (response.status === 404) {
                toastr.warning("Error Loading Division List!", { timeOut: 2000 });
            }
        });

    }
    $scope.OnDivisionClick = function () {
        //Region List
        $http({
            method: "GET",
            url: MyApp.rootPath + "DistSrSales/GetRegionList",
            params: { dCode: $scope.frmMomSkuWiseNumericDistribution.Division.DivisionCode }
        }).then(function (response) {
            if (response.data.Data.length > 0) {
                $scope.Regions = response.data.Data;
            } else {
                toastr.warning("No Data Found!", { timeOut: 2000 });
            }
        }, function (response) {
            if (response.status === 404) {
                toastr.warning("Error Loading Region List!", { timeOut: 2000 });
            }
        });
    }
    $scope.OnRegionClick = function () {
        //Area List
        $http({
            method: "GET",
            url: MyApp.rootPath + "DistSrSales/GetAreaList",
            params: { dCode: $scope.frmMomSkuWiseNumericDistribution.Division.DivisionCode, rCode: $scope.frmMomSkuWiseNumericDistribution.Region.RegionCode }
        }).then(function (response) {
            if (response.data.Status === "" || response.data.Status === null) {
                if (response.data.Data.length > 0) {
                    $scope.Areas = response.data.Data;
                } else {
                    toastr.warning("No Data Found!", { timeOut: 2000 });
                }
            } else {
                toastr.warning(response.data.Status, { timeOut: 2000 });
            }
        }, function (response) {
            if (response.status === 404) {
                toastr.warning("Error Loading Area List!", { timeOut: 2000 });
            }
        });
    }
    $scope.OnAreaClick = function () {
        //Territory List
        $http({
            method: "GET",
            url: MyApp.rootPath + "DistSrSales/GetTerritoryList",
            params: { dCode: $scope.frmMomSkuWiseNumericDistribution.Division.DivisionCode, rCode: $scope.frmMomSkuWiseNumericDistribution.Region.RegionCode, aCode: $scope.frmMomSkuWiseNumericDistribution.Area.AreaCode }
        }).then(function (response) {
            if (response.data.Status === "" || response.data.Status === null) {
                if (response.data.Data.length > 0) {
                    $scope.Territories = response.data.Data;
                } else {
                    toastr.warning("No Data Found!", { timeOut: 2000 });
                }
            } else {
                toastr.warning(response.data.Status, { timeOut: 2000 });
            }
        }, function (response) {
            if (response.status === 404) {
                toastr.warning("Error Loading Territory List!", { timeOut: 2000 });
            }
        });
    }
    $scope.OnTerritoryClick = function () {
        //Customer List
        $http({
            method: "GET",
            url: MyApp.rootPath + "DistSrSales/GetCustomerList",
            params: { dCode: $scope.frmMomSkuWiseNumericDistribution.Division.DivisionCode, rCode: $scope.frmMomSkuWiseNumericDistribution.Region.RegionCode, aCode: $scope.frmMomSkuWiseNumericDistribution.Area.AreaCode, tCode: $scope.frmMomSkuWiseNumericDistribution.Territory.TerritoryCode }
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

    //Grid
    var columnMomSkuWiseNumericDistribution = [       
        { name: 'DivisionCode', displayName: "Division Code", width: 150, aggregationType: uiGridConstants.aggregationTypes.count, footerCellFilter: 'number:0' },
        { name: 'DivisionName', displayName: "Division Name", width: 150 },
        { name: 'RegionCode', displayName: "Region Code", width: 150 },
        { name: 'RegionName', displayName: "Region Name", width: 150 },
        { name: 'AreaCode', displayName: "Area Code", width: 150 },
        { name: 'AreaName', displayName: "Area Name", width: 150 },
        { name: 'TerritoryCode', displayName: "Territory Code", width: 150 },
        { name: 'TerritoryName', displayName: "Territory Name", width: 150 },
        { name: 'CustomerCode', displayName: "Customer Code", width: 150 },
        { name: 'CustomerName', displayName: "Customer Name", width: 150 },
        { name: 'DbLocation', displayName: "DB Location", width: 150 },
        { name: 'MarketCode', displayName: "Market Code", width: 150 },
        { name: 'MarketName', displayName: "Market Name", width: 150 },
        { name: 'ProductCode', displayName: "Product Code", width: 150 },
        { name: 'ProductName', displayName: "Product Name", width: 150 },
        { name: 'PackSize', displayName: "PackSize", width: 150 },

        { name: 'Jan',  displayName: "January",  width: 120, cellFilter: 'number:0', cellClass: 'grid-align', footerCellFilter: 'number:0', aggregationType: uiGridConstants.aggregationTypes.sum },
        { name: 'Feb',  displayName: "Febuary",  width: 120, cellFilter: 'number:0', cellClass: 'grid-align', footerCellFilter: 'number:0', aggregationType: uiGridConstants.aggregationTypes.sum },
        { name: 'Mar',  displayName: "March",  width: 120, cellFilter: 'number:0', cellClass: 'grid-align', footerCellFilter: 'number:0', aggregationType: uiGridConstants.aggregationTypes.sum },
        { name: 'Apr',  displayName: "April",  width: 120, cellFilter: 'number:0', cellClass: 'grid-align', footerCellFilter: 'number:0', aggregationType: uiGridConstants.aggregationTypes.sum },
        { name: 'May',  displayName: "May",  width: 120, cellFilter: 'number:0', cellClass: 'grid-align', footerCellFilter: 'number:0', aggregationType: uiGridConstants.aggregationTypes.sum },
        { name: 'Jun',  displayName: "June",  width: 120, cellFilter: 'number:0', cellClass: 'grid-align', footerCellFilter: 'number:0', aggregationType: uiGridConstants.aggregationTypes.sum },
        { name: 'Jul',  displayName: "July",  width: 120, cellFilter: 'number:0', cellClass: 'grid-align', footerCellFilter: 'number:0', aggregationType: uiGridConstants.aggregationTypes.sum },
        { name: 'Aug',  displayName: "August",  width: 120, cellFilter: 'number:0', cellClass: 'grid-align', footerCellFilter: 'number:0', aggregationType: uiGridConstants.aggregationTypes.sum },
        { name: 'Sep',  displayName: "September",  width: 120, cellFilter: 'number:0', cellClass: 'grid-align', footerCellFilter: 'number:0', aggregationType: uiGridConstants.aggregationTypes.sum },
        { name: 'Oct',  displayName: "October",  width: 120, cellFilter: 'number:0', cellClass: 'grid-align', footerCellFilter: 'number:0', aggregationType: uiGridConstants.aggregationTypes.sum },
        { name: 'Nov',  displayName: "November",  width: 120, cellFilter: 'number:0', cellClass: 'grid-align', footerCellFilter: 'number:0', aggregationType: uiGridConstants.aggregationTypes.sum },
        { name: 'Dec',  displayName: "December",  width: 120, cellFilter: 'number:0', cellClass: 'grid-align', footerCellFilter: 'number:0', aggregationType: uiGridConstants.aggregationTypes.sum },
        { name: 'Total',displayName: "Total",width: 120, cellFilter: 'number:0', cellClass: 'grid-align', footerCellFilter: 'number:0', aggregationType: uiGridConstants.aggregationTypes.sum }

    ];
    $scope.gridMomSkuWiseNumericDistribution = {
        showColumnFooter: true,
        enableFiltering: true,
        enableSorting: true,
        columnDefs: columnMomSkuWiseNumericDistribution,
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
        $scope.gridMomSkuWiseNumericDistribution.data = [];
        $scope.isDisabled = true;
        $scope.ReportType = "";
        $scope.frmMomSkuWiseNumericDistribution.RepType = undefined;
        $scope.frmMomSkuWiseNumericDistribution.Division = undefined;
        $scope.frmMomSkuWiseNumericDistribution.Region = undefined;
        $scope.frmMomSkuWiseNumericDistribution.Area = undefined;
        $scope.frmMomSkuWiseNumericDistribution.Territory = undefined;
        $scope.frmMomSkuWiseNumericDistribution.Customer = undefined;
        $scope.frmMomSkuWiseNumericDistribution.Product = undefined;
        $scope.Divisions = [];
        $scope.Regions = [];
        $scope.Areas = [];
        $scope.Territories = [];
        $scope.Customers = [];
        $scope.Products = [];
    };

});