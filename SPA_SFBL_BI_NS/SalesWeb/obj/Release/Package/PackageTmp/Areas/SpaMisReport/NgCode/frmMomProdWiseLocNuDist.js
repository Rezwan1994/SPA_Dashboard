app.controller("MomProdWiseLocNuDistCtrl", function ($scope, $http, $interval, uiGridConstants, $filter) {

    $scope.isDisabled = true;
    var methodName = "";
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
                $scope.gridMomProdWiseLocNuDist.enableGridMenu = response.data[0].DownLoadStatus;
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
            params: { SlNo: "(22,26)" }
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
        if ($scope.frmMomProdWiseLocNuDist.RepType.ReportTypeValue == "MomLocationWiseProductNumericDist" ||$scope.frmMomProdWiseLocNuDist.RepType.ReportTypeValue == "MomLocationWiseProductNumericDistLy") {
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

    $scope.GetMomProdWiseLocNuDist = function () {
        if ($scope.frmMomProdWiseLocNuDist.RepType.ReportTypeValue == "MomLocationWiseProductNumericDist") {
            $http({
                method: "POST",
                url: MyApp.rootPath + "MomProdWiseLocNuDist/GetMomProdWiseLocNuDist",
                data: {
                    dCode: $scope.frmMomProdWiseLocNuDist.Division.DivisionCode,
                    rCode: $scope.frmMomProdWiseLocNuDist.Region.RegionCode,
                    aCode: $scope.frmMomProdWiseLocNuDist.Area.AreaCode,
                    tCode: $scope.frmMomProdWiseLocNuDist.Territory.TerritoryCode,
                    cCode: $scope.frmMomProdWiseLocNuDist.Customer.CustomerCode,
                    pCode: $scope.frmMomProdWiseLocNuDist.Product.Code
                }
            }).then(function (response) {
                if (response.data.length > 0) {
                    $scope.gridMomProdWiseLocNuDist.data = response.data;
                }
                else {
                    toastr.warning("No Data Found!", '');
                    $scope.gridMomProdWiseLocNuDist.data = [];
                }
            }, function (response) {
                toastr.error("Error!");
            });
        }

        if ($scope.frmMomProdWiseLocNuDist.RepType.ReportTypeValue == "MomLocationWiseProductNumericDistLy") {
            $http({
                method: "POST",
                url: MyApp.rootPath + "MomProdWiseLocNuDist/GetMomProdWiseLocNuDistLy",
                data: {
                    dCode: $scope.frmMomProdWiseLocNuDist.Division.DivisionCode,
                    rCode: $scope.frmMomProdWiseLocNuDist.Region.RegionCode,
                    aCode: $scope.frmMomProdWiseLocNuDist.Area.AreaCode,
                    tCode: $scope.frmMomProdWiseLocNuDist.Territory.TerritoryCode,
                    cCode: $scope.frmMomProdWiseLocNuDist.Customer.CustomerCode,
                    pCode: $scope.frmMomProdWiseLocNuDist.Product.Code
                }
            }).then(function (response) {
                if (response.data.length > 0) {
                    $scope.gridMomProdWiseLocNuDist.data = response.data;
                }
                else {
                    toastr.warning("No Data Found!", '');
                    $scope.gridMomProdWiseLocNuDist.data = [];
                }
            }, function (response) {
                toastr.error("Error!");
            });
        }

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
            params: { dCode: $scope.frmMomProdWiseLocNuDist.Division.DivisionCode }
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
            params: { dCode: $scope.frmMomProdWiseLocNuDist.Division.DivisionCode, rCode: $scope.frmMomProdWiseLocNuDist.Region.RegionCode }
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
            params: { dCode: $scope.frmMomProdWiseLocNuDist.Division.DivisionCode, rCode: $scope.frmMomProdWiseLocNuDist.Region.RegionCode, aCode: $scope.frmMomProdWiseLocNuDist.Area.AreaCode }
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
            params: { dCode: $scope.frmMomProdWiseLocNuDist.Division.DivisionCode, rCode: $scope.frmMomProdWiseLocNuDist.Region.RegionCode, aCode: $scope.frmMomProdWiseLocNuDist.Area.AreaCode, tCode: $scope.frmMomProdWiseLocNuDist.Territory.TerritoryCode }
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

    //Grid
    var columnMomProdWiseLocNuDist = [
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
    $scope.gridMomProdWiseLocNuDist = {
        showColumnFooter: true,
        enableFiltering: true,
        enableSorting: true,
        columnDefs: columnMomProdWiseLocNuDist,
        enableGridMenu: true,
        enableSelectAll: true,
        exporterCsvFilename: "Location Wise Product Numeric Distribution(MOM).csv",
        exporterMenuPdf: false,
        exporterCsvLinkElement: angular.element(document.querySelectorAll(".custom-csv-link-location")),
        onRegisterApi: function (gridApi) {
            $scope.gridApi = gridApi;
        }
    };


    $scope.Reset = function () {
        $scope.FromDate = "";
        $scope.ToDate = "";
        $scope.gridMomProdWiseLocNuDist.data = [];
        $scope.isDisabled = true;
        $scope.ReportType = "";
        $scope.frmMomProdWiseLocNuDist.RepType = undefined;
        $scope.frmMomProdWiseLocNuDist.Division = undefined;
        $scope.frmMomProdWiseLocNuDist.Region = undefined;
        $scope.frmMomProdWiseLocNuDist.Area = undefined;
        $scope.frmMomProdWiseLocNuDist.Territory = undefined;
        $scope.frmMomProdWiseLocNuDist.Customer = undefined;
        $scope.frmMomProdWiseLocNuDist.Product = undefined;
        $scope.Divisions = [];
        $scope.Regions = [];
        $scope.Areas = [];
        $scope.Territories = [];
        $scope.Customers = [];
        //$scope.Products = [];
    };

});