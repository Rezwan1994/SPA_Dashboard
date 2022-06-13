app.controller("MomCatWiseNumericDistributionCtrl", function ($scope, $http, uiGridConstants) {
    $scope.isDisabled = true;
    $scope.EventPerm(22);


    $scope.GetReportDownLoadStatus = function () {
        $http({
            method: "GET",
            url: MyApp.rootPath + "RetailerIms/GetReportDownLoadStatus",
            datatype: "json",
            params: { url: window.location.pathname }
        }).then(function (response) {
            if (response.data.length > 0) {
                $scope.gridMomCatWiseNumericDistribution.enableGridMenu = response.data[0].DownLoadStatus;
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
            params: { SlNo: "(19,27)" }
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
        if ($scope.frmMomCatWiseNumericDistribution.RepType.ReportTypeValue == "MomCategoryWiseNumericDistribution" || $scope.frmMomCatWiseNumericDistribution.RepType.ReportTypeValue == "MomCategoryWiseNumericDistributionLy") {
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
    $scope.GetDivisionList = function () {
        $http({
            method: "GET",
            url: MyApp.rootPath + "DistSrSales/GetDivisionList",
        }).then(function (response) {
            if (response.data.Data.length > 0) {
                $scope.Divisions = response.data.Data;
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
        $http({
            method: "GET",
            url: MyApp.rootPath + "DistSrSales/GetRegionList",
            params: { dCode: $scope.frmMomCatWiseNumericDistribution.Division.DivisionCode }
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
        $http({
            method: "GET",
            url: MyApp.rootPath + "DistSrSales/GetAreaList",
            params: { dCode: $scope.frmMomCatWiseNumericDistribution.Division.DivisionCode, rCode: $scope.frmMomCatWiseNumericDistribution.Region.RegionCode }
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
            params: { dCode: $scope.frmMomCatWiseNumericDistribution.Division.DivisionCode, rCode: $scope.frmMomCatWiseNumericDistribution.Region.RegionCode, aCode: $scope.frmMomCatWiseNumericDistribution.Area.AreaCode }
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
        $http({
            method: "GET",
            url: MyApp.rootPath + "DistSrSales/GetCustomerList",
            params: { dCode: $scope.frmMomCatWiseNumericDistribution.Division.DivisionCode, rCode: $scope.frmMomCatWiseNumericDistribution.Region.RegionCode, aCode: $scope.frmMomCatWiseNumericDistribution.Area.AreaCode, tCode: $scope.frmMomCatWiseNumericDistribution.Territory.TerritoryCode }
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
    $scope.GetProductCategoryList = function () {
        $http({
            method: "GET",
            url: MyApp.rootPath + "CatNumericSalesAnalysis/GetProductCategoryList",
        }).then(function (response) {
            if (response.data.Data.length > 0) {
                $scope.Categoris = response.data.Data;
            } else {
                toastr.warning("No Data Found!", { timeOut: 2000 });
            }

        }, function (response) {
            if (response.status === 404) {
                toastr.warning("Error Loading Category List!", { timeOut: 2000 });
            }
        });
    }
    $scope.GetProductCategoryList();

    var columnCatNumericDist = [        
        { name: 'DivisionCode', displayName: "Division Code", width: 150, aggregationType: uiGridConstants.aggregationTypes.count, footerCellFilter: 'number:0' },
        { name: 'DivisionName', displayName: "Division Name", width: 150 },
        { name: 'RegionCode', displayName: "Region Code", width: 150 },
        { name: 'RegionName', displayName: "Region Name", width: 150 },
        { name: 'AreaCode', displayName: "Area Code", width: 150 },
        { name: 'AreaName', displayName: "Area Name", width: 150 },
        { name: 'TerritoryCode', displayName: "Territory Code", width: 150 },
        { name: 'TerritoryName', displayName: "Territory Name", width: 150 },

        { name: 'CustomerCode', displayName: "Distributor Code", width: 150 },
        { name: 'CustomerName', displayName: "Distributor Name", width: 150 },
        { name: 'DbLocation', displayName: "DB Location", width: 150 },

        { name: 'MarketCode', displayName: "Market Code", width: 150 },
        { name: 'MarketName', displayName: "Market Name", width: 150 },

        { name: 'ProductCategoryCode', displayName: "Category Code", width: 150 },
        { name: 'ProductCategoryName', displayName: "Category Name", width: 150 },

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
    $scope.gridMomCatWiseNumericDistribution = {
        showColumnFooter: true,
        enableFiltering: true,
        enableSorting: true,
        columnDefs: columnCatNumericDist,
        enableGridMenu: true,
        enableSelectAll: true,
        exporterCsvFilename: "Category_Wise_Numeric_Distribution(MOM).csv",
        exporterMenuPdf: false,
        exporterCsvLinkElement: angular.element(document.querySelectorAll(".custom-csv-link-location")),
        onRegisterApi: function (gridApi) {
            $scope.gridApi = gridApi;
        }
    };

    $scope.GetMomCatWiseNumericDistribution = function () {


        if ($scope.frmMomCatWiseNumericDistribution.RepType.ReportTypeValue == "MomCategoryWiseNumericDistribution") {

            methodName = "GetMomCatWiseNumericDistribution";
        }

        if ($scope.frmMomCatWiseNumericDistribution.RepType.ReportTypeValue == "MomCategoryWiseNumericDistributionLy") {

            methodName = "GetMomCatWiseNumericDistributionLy";
        }

        $http({
            method: "POST",
            url: MyApp.rootPath + "MomCatWiseNumericDistribution/" + methodName,
            data: {
                dCode: $scope.frmMomCatWiseNumericDistribution.Division.DivisionCode,
                rCode: $scope.frmMomCatWiseNumericDistribution.Region.RegionCode,
                aCode: $scope.frmMomCatWiseNumericDistribution.Area.AreaCode,
                tCode: $scope.frmMomCatWiseNumericDistribution.Territory.TerritoryCode,
                cCode: $scope.frmMomCatWiseNumericDistribution.Customer.CustomerCode,
                pcCode: $scope.frmMomCatWiseNumericDistribution.Category.CategoryCode

            }
        }).then(function (response) {
            if (response.data.length > 0) {
                $scope.gridMomCatWiseNumericDistribution.data = response.data;
            }
            else {
                $scope.gridMomCatWiseNumericDistribution.data = [];
            }
        }, function (response) {
            toastr.error("Error!");
        });
    };


    $scope.Reset = function () {
        $scope.FromDate = "";
        $scope.ToDate = "";
        $scope.gridMomCatWiseNumericDistribution.data = [];
        $scope.isDisabled = true;
        $scope.frmMomCatWiseNumericDistribution.RepType = undefined;
        $scope.frmMomCatWiseNumericDistribution.Division = undefined;
        $scope.frmMomCatWiseNumericDistribution.Region = undefined;
        $scope.frmMomCatWiseNumericDistribution.Area = undefined;
        $scope.frmMomCatWiseNumericDistribution.Territory = undefined;
        $scope.frmMomCatWiseNumericDistribution.Customer = undefined;
        $scope.frmMomCatWiseNumericDistribution.Category = undefined;
        $scope.Divisions = [];
        $scope.Regions = [];
        $scope.Areas = [];
        $scope.Territories = [];
        $scope.Customers = [];
    };

});