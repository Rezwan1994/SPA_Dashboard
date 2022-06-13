app.controller("MomRetailProdTrendCtrl", function ($scope, $http, uiGridConstants) {

    var xl_file_name = "";
    var methodName = "";
    $scope.EventPerm(22);
    $scope.isDisabled = true;

    $scope.GetReportDownLoadStatus = function () {
        $http({
            method: "GET",
            url: MyApp.rootPath + "RetailerIms/GetReportDownLoadStatus",
            datatype: "json",
            params: { url: window.location.pathname }
        }).then(function (response) {
            if (response.data.length > 0) {
                $scope.gridMomRetailProdTrend.enableGridMenu = response.data[0].DownLoadStatus;
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


    $scope.GetBaseProductList = function () {
        $http({
            method: "GET",
            url: MyApp.rootPath + "ProductInformation/GetBaseProductList"
        }).then(function (response) {
            if (response.data.Status === "" || response.data.Status === null) {
                if (response.data.Data.length > 0) {
                    $scope.BaseProducts = response.data.Data;
                } else {
                    toastr.warning("No Data Found!", { timeOut: 2000 });
                }
            } else {
                toastr.warning(response.data.Status, { timeOut: 2000 });
            }
        }, function (response) {
            if (response.status === 404) {
                toastr.warning("Error Loading Base Product List!", { timeOut: 2000 });
            }
        });
    }
    $scope.GetBaseProductList();

    $scope.GetBrandList = function () {
        $http({
            method: "GET",
            url: MyApp.rootPath + "ProductInformation/GetBrandList"
        }).then(function (response) {
            if (response.data.Status === "" || response.data.Status === null) {
                if (response.data.Data.length > 0) {
                    $scope.Brands = response.data.Data;
                } else {
                    toastr.warning("No Data Found!", { timeOut: 2000 });
                }
            } else {
                toastr.warning(response.data.Status, { timeOut: 2000 });
            }
        }, function (response) {
            if (response.status === 404) {
                toastr.warning("Error Loading Brand List!", { timeOut: 2000 });
            }
        });
    }
    $scope.GetBrandList();

    $scope.GetProductCategoryList = function () {
        $http({
            method: "GET",
            url: MyApp.rootPath + "ProductInformation/GetProductCategoryList"
        }).then(function (response) {
            if (response.data.Status === "" || response.data.Status === null) {
                if (response.data.Data.length > 0) {
                    $scope.ProductCategories = response.data.Data;                    
                } else {
                    toastr.warning("No Data Found!", { timeOut: 2000 });
                }
            } else {
                toastr.warning(response.data.Status, { timeOut: 2000 });
            }
        }, function (response) {
            if (response.status === 404) {
                toastr.warning("Error Loading Product Category List!", { timeOut: 2000 });
            }
        });
    }
    $scope.GetProductCategoryList();

    $scope.OnBaseProductClick = function () {
        $scope.frmMomRetailProdTrend.Product = undefined;
        $scope.Products = [];
        $scope.GetProductList();
    };

    $scope.OnBrandClick = function () {
        $scope.frmMomRetailProdTrend.Product = undefined;
        $scope.Products = [];
        $scope.GetProductList();
    };


    $scope.OnProductCategoryClick = function () {
        $scope.frmMomRetailProdTrend.Product = undefined;
        $scope.Products = [];
        $scope.GetProductList();
    };

    $scope.GetProductList = function () {
        $http({
            method: "GET",
            url: MyApp.rootPath + "MomRetailProdTrend/GetProductList",
            params: {
                bpCode: $scope.frmMomRetailProdTrend.BaseProduct.BaseProductCode,
                bCode: $scope.frmMomRetailProdTrend.Brand.BrandCode,
                pcCode: $scope.frmMomRetailProdTrend.ProductCategory.CategoryCode
            }
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
    //$scope.GetProductList();

    $scope.GetMomRetailProdTrend = function () {

        if ($scope.frmMomRetailProdTrend.RepType.ReportTypeValue == "MomRetailProductTrend") {
            methodName = "GetMomRetailProdTrend";
        }

        if ($scope.frmMomRetailProdTrend.RepType.ReportTypeValue == "MomRetailProductTrendLy") {
            methodName = "GetMomRetailProdTrendLy";
        }

        $http({
            method: "POST",
            url: MyApp.rootPath + "MomRetailProdTrend/" + methodName,
            data: {
                fDate: $scope.FromDate,
                tDate: $scope.ToDate,
                dCode: $scope.frmMomRetailProdTrend.Division.DivisionCode,
                rCode: $scope.frmMomRetailProdTrend.Region.RegionCode,
                aCode: $scope.frmMomRetailProdTrend.Area.AreaCode,
                tCode: $scope.frmMomRetailProdTrend.Territory.TerritoryCode,
                cCode: $scope.frmMomRetailProdTrend.Customer.CustomerCode,
                bpCode: $scope.frmMomRetailProdTrend.BaseProduct.BaseProductCode,
                bCode: $scope.frmMomRetailProdTrend.Brand.BrandCode,
                pcCode: $scope.frmMomRetailProdTrend.ProductCategory.CategoryCode,
                pCode: $scope.frmMomRetailProdTrend.Product.ProductCode
            }
        }).then(function (response) {
            if (response.data.length > 0) {
                $scope.gridMomRetailProdTrend.data = response.data;
            }
            else {
                toastr.warning("No Data Found!", '');
                $scope.gridMomRetailProdTrend.data = [];
            }
        }, function (response) {
            //alert(response);
            toastr.error("Error!");
        });
    };


    $scope.OnReportTypeChange = function () {
        if ($scope.frmMomRetailProdTrend.RepType.ReportTypeValue == "Today" || $scope.frmMomRetailProdTrend.RepType.ReportTypeValue == "Yesterday" || $scope.frmMomRetailProdTrend.RepType.ReportTypeValue == "LastSevendays" || $scope.frmMomRetailProdTrend.RepType.ReportTypeValue == "LastThirtydays" || $scope.frmMomRetailProdTrend.RepType.ReportTypeValue == "CurrentMonth" || $scope.frmMomRetailProdTrend.RepType.ReportTypeValue == "LastMonth" || $scope.frmMomRetailProdTrend.RepType.ReportTypeValue == "MomCy" || $scope.frmMomRetailProdTrend.RepType.ReportTypeValue == "MomRetailProductTrend" || $scope.frmMomRetailProdTrend.RepType.ReportTypeValue == "MomRetailProductTrendLy") {
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
        $scope.dCode = $scope.frmMomRetailProdTrend.Division.DivisionCode;
        //Region List
        $http({
            method: "GET",
            url: MyApp.rootPath + "DistSrSales/GetRegionList",
            params: { dCode: $scope.frmMomRetailProdTrend.Division.DivisionCode }
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
        $scope.rCode = $scope.frmMomRetailProdTrend.Region.RegionCode;
        //Area List
        $http({
            method: "GET",
            url: MyApp.rootPath + "DistSrSales/GetAreaList",
            params: { dCode: $scope.frmMomRetailProdTrend.Division.DivisionCode, rCode: $scope.frmMomRetailProdTrend.Region.RegionCode }
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
        $scope.aCode = $scope.frmMomRetailProdTrend.Area.AreaCode;
        //Territory List
        $http({
            method: "GET",
            url: MyApp.rootPath + "DistSrSales/GetTerritoryList",
            params: { dCode: $scope.frmMomRetailProdTrend.Division.DivisionCode, rCode: $scope.frmMomRetailProdTrend.Region.RegionCode, aCode: $scope.frmMomRetailProdTrend.Area.AreaCode }
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
        $scope.tCode = $scope.frmMomRetailProdTrend.Territory.TerritoryCode;
        //Customer List
        $http({
            method: "GET",
            url: MyApp.rootPath + "DistSrSales/GetCustomerList",
            params: { dCode: $scope.frmMomRetailProdTrend.Division.DivisionCode, rCode: $scope.frmMomRetailProdTrend.Region.RegionCode, aCode: $scope.frmMomRetailProdTrend.Area.AreaCode, tCode: $scope.frmMomRetailProdTrend.Territory.TerritoryCode }
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
        $scope.cCode = $scope.frmMomRetailProdTrend.Customer.CustomerCode;
        //SR List
        $http({
            method: "GET",
            url: MyApp.rootPath + "DistSrSales/GetSrList",
            params: { dCode: $scope.frmMomRetailProdTrend.Division.DivisionCode, rCode: $scope.frmMomRetailProdTrend.Region.RegionCode, aCode: $scope.frmMomRetailProdTrend.Area.AreaCode, tCode: $scope.frmMomRetailProdTrend.Territory.TerritoryCode, cCode: $scope.frmMomRetailProdTrend.Customer.CustomerCode }
        }).then(function (response) {
            if (response.data.Status === "" || response.data.Status === null) {
                if (response.data.Data.length > 0) {
                    $scope.Srs = response.data.Data;
                } else {
                    toastr.warning("No Data Found!", { timeOut: 2000 });
                }
            } else {
                toastr.warning(response.data.Status, { timeOut: 2000 });
            }
        }, function (response) {
            if (response.status === 404) {
                toastr.warning("Error Loading SR List!", { timeOut: 2000 });
            }
        });


    }



    $scope.GetReportTypeList = function () {
        $http({
            method: "GET",
            url: MyApp.rootPath + "DistSrSales/GetReportTypeList",
            params: { SlNo: "(16,31)" }
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

    //Grid
    var columnMomRetailProdTrend = [
        { name: 'DivisionCode', displayName: "Division Code", width: 150, aggregationType: uiGridConstants.aggregationTypes.count, footerCellFilter: 'number:0' },
        { name: 'DivisionName', displayName: "Division Name", width: 150 },
        { name: 'RegionCode', displayName: "Region Code", width: 150 },
        { name: 'RegionName', displayName: "Region Name", width: 150 },
        { name: 'AreaCode', displayName: "Area Code", width: 150 },
        { name: 'AreaName', displayName: "Area Name", width: 150 },
        { name: 'TerritoryCode', displayName: "Territory Code", width: 150 },
        { name: 'TerritoryName', displayName: "Territory Name", width: 150 },
        { name: 'MarketCode', displayName: "Market Code", width: 150 },
        { name: 'MarketName', displayName: "Market Name", width: 150 },
        { name: 'CustomerCode', displayName: "Distributor Code", width: 150 },
        { name: 'CustomerName', displayName: "Distributor Name", width: 150 },
        { name: 'DbLocation', displayName: "DB Location", width: 150 },
        { name: 'RouteCode', displayName: "Route Code", width: 150 },
        { name: 'RouteName', displayName: "Route Name", width: 150 },
        { name: 'RetailerCode', displayName: "Retailer Code", width: 150 },
        { name: 'RetailerName', displayName: "Retailer Name", width: 150 },
        { name: 'ProductCode', displayName: "Product Code", width: 150 },
        { name: 'ProductName', displayName: "Product Name", width: 150 },
        { name: 'PackSize', displayName: "Pack Size", width: 150 },
        //{ name: 'ProductPrice', displayName: "Product Price", width: 120, cellFilter: 'number:2', cellClass: 'grid-align', footerCellFilter: 'number:2' },

        { name: 'JanSalesQty', displayName: "Jan IMS Qty", width: 120, cellFilter: 'number:2', cellClass: 'grid-align', footerCellFilter: 'number:0', aggregationType: uiGridConstants.aggregationTypes.sum },
        { name: 'JanSalesVal', displayName: "Jan IMS Val", width: 120, cellFilter: 'number:2', cellClass: 'grid-align', footerCellFilter: 'number:2', aggregationType: uiGridConstants.aggregationTypes.sum },
        
        { name: 'FebSalesQty', displayName: "Feb IMS Qty", width: 120, cellFilter: 'number:2', cellClass: 'grid-align', footerCellFilter: 'number:0', aggregationType: uiGridConstants.aggregationTypes.sum },
        { name: 'FebSalesVal', displayName: "Feb IMS Val", width: 120, cellFilter: 'number:2', cellClass: 'grid-align', footerCellFilter: 'number:2', aggregationType: uiGridConstants.aggregationTypes.sum },

        { name: 'MarSalesQty', displayName: "Mar IMS Qty", width: 120, cellFilter: 'number:2', cellClass: 'grid-align', footerCellFilter: 'number:0', aggregationType: uiGridConstants.aggregationTypes.sum },
        { name: 'MarSalesVal', displayName: "Mar IMS Val", width: 120, cellFilter: 'number:2', cellClass: 'grid-align', footerCellFilter: 'number:2', aggregationType: uiGridConstants.aggregationTypes.sum },

        { name: 'AprSalesQty', displayName: "Apr IMS Qty", width: 120, cellFilter: 'number:2', cellClass: 'grid-align', footerCellFilter: 'number:0', aggregationType: uiGridConstants.aggregationTypes.sum },
        { name: 'AprSalesVal', displayName: "Apr IMS Val", width: 120, cellFilter: 'number:2', cellClass: 'grid-align', footerCellFilter: 'number:2', aggregationType: uiGridConstants.aggregationTypes.sum },

        { name: 'MaySalesQty', displayName: "May IMS Qty", width: 120, cellFilter: 'number:2', cellClass: 'grid-align', footerCellFilter: 'number:0', aggregationType: uiGridConstants.aggregationTypes.sum },
        { name: 'MaySalesVal', displayName: "May IMS Val", width: 120, cellFilter: 'number:2', cellClass: 'grid-align', footerCellFilter: 'number:2', aggregationType: uiGridConstants.aggregationTypes.sum },

        { name: 'JunSalesQty', displayName: "Jun IMS Qty", width: 120, cellFilter: 'number:2', cellClass: 'grid-align', footerCellFilter: 'number:0', aggregationType: uiGridConstants.aggregationTypes.sum },
        { name: 'JunSalesVal', displayName: "Jun IMS Val", width: 120, cellFilter: 'number:2', cellClass: 'grid-align', footerCellFilter: 'number:2', aggregationType: uiGridConstants.aggregationTypes.sum },

        { name: 'JulSalesQty', displayName: "Jul IMS Qty", width: 120, cellFilter: 'number:2', cellClass: 'grid-align', footerCellFilter: 'number:0', aggregationType: uiGridConstants.aggregationTypes.sum },
        { name: 'JulSalesVal', displayName: "Jul IMS Val", width: 120, cellFilter: 'number:2', cellClass: 'grid-align', footerCellFilter: 'number:2', aggregationType: uiGridConstants.aggregationTypes.sum },

        { name: 'AugSalesQty', displayName: "Aug IMS Qty", width: 120, cellFilter: 'number:2', cellClass: 'grid-align', footerCellFilter: 'number:0', aggregationType: uiGridConstants.aggregationTypes.sum },
        { name: 'AugSalesVal', displayName: "Aug IMS Val", width: 120, cellFilter: 'number:2', cellClass: 'grid-align', footerCellFilter: 'number:2', aggregationType: uiGridConstants.aggregationTypes.sum },

        { name: 'SepSalesQty', displayName: "Sep IMS Qty", width: 120, cellFilter: 'number:2', cellClass: 'grid-align', footerCellFilter: 'number:0', aggregationType: uiGridConstants.aggregationTypes.sum },
        { name: 'SepSalesVal', displayName: "Sep IMS Val", width: 120, cellFilter: 'number:2', cellClass: 'grid-align', footerCellFilter: 'number:2', aggregationType: uiGridConstants.aggregationTypes.sum },

        { name: 'OctSalesQty', displayName: "Oct IMS Qty", width: 120, cellFilter: 'number:2', cellClass: 'grid-align', footerCellFilter: 'number:0', aggregationType: uiGridConstants.aggregationTypes.sum },
        { name: 'OctSalesVal', displayName: "Oct IMS Val", width: 120, cellFilter: 'number:2', cellClass: 'grid-align', footerCellFilter: 'number:2', aggregationType: uiGridConstants.aggregationTypes.sum },

        { name: 'NovSalesQty', displayName: "Nov IMS Qty", width: 120, cellFilter: 'number:2', cellClass: 'grid-align', footerCellFilter: 'number:0', aggregationType: uiGridConstants.aggregationTypes.sum },
        { name: 'NovSalesVal', displayName: "Nov IMS Val", width: 120, cellFilter: 'number:2', cellClass: 'grid-align', footerCellFilter: 'number:2', aggregationType: uiGridConstants.aggregationTypes.sum },

        { name: 'DecSalesQty', displayName: "Dec IMS Qty", width: 120, cellFilter: 'number:2', cellClass: 'grid-align', footerCellFilter: 'number:0', aggregationType: uiGridConstants.aggregationTypes.sum },
        { name: 'DecSalesVal', displayName: "Dec IMS Val", width: 120, cellFilter: 'number:2', cellClass: 'grid-align', footerCellFilter: 'number:2', aggregationType: uiGridConstants.aggregationTypes.sum },

        { name: 'TotalSalesQty', displayName: "Total IMS Qty", width: 120, cellFilter: 'number:2', cellClass: 'grid-align', footerCellFilter: 'number:0', aggregationType: uiGridConstants.aggregationTypes.sum },
        { name: 'TotalSalesVal', displayName: "Total IMS Val", width: 120, cellFilter: 'number:2', cellClass: 'grid-align', footerCellFilter: 'number:2', aggregationType: uiGridConstants.aggregationTypes.sum }
    ];

    $scope.gridMomRetailProdTrend = {
        showColumnFooter: true,
        enableFiltering: true,
        enableSorting: true,
        columnDefs: columnMomRetailProdTrend,
        enableGridMenu: true,
        enableSelectAll: true,
        exporterCsvFilename: "MOM Retail Product Trend.csv",
        exporterMenuPdf: false,
        exporterCsvLinkElement: angular.element(document.querySelectorAll(".custom-csv-link-location")),
        onRegisterApi: function (gridApi) {
            $scope.gridApi = gridApi;
        }
    };

    $scope.Reset = function () {
        $scope.FromDate = "";
        $scope.ToDate = "";
        $scope.gridMomRetailProdTrend.data = [];
        $scope.isDisabled = false;
        $scope.ReportType = "";
        $scope.frmMomRetailProdTrend.RepType = undefined;
        $scope.frmMomRetailProdTrend.Division = undefined;
        $scope.frmMomRetailProdTrend.Region = undefined;
        $scope.frmMomRetailProdTrend.Area = undefined;
        $scope.frmMomRetailProdTrend.Territory = undefined;
        $scope.frmMomRetailProdTrend.Customer = undefined;
        $scope.Divisions = [];
        $scope.Regions = [];
        $scope.Areas = [];
        $scope.Territories = [];
        $scope.Customers = [];
        $scope.isDisabled = true;
    };

});