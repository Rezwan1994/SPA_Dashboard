app.controller("InvoiceProductImsCtrl", function ($scope, $http, uiGridConstants) {

    var methodName = "";
    $scope.EventPerm(22);
    $scope.isDisabled = true;
    $scope.LocationType = "ALL";

    $scope.GetReportDownLoadStatus = function () {
        $http({
            method: "GET",
            url: MyApp.rootPath + "RetailerIms/GetReportDownLoadStatus",
            datatype: "json",
            params: { url: window.location.pathname }
        }).then(function (response) {
            if (response.data.length > 0) {
                $scope.gridInvoiceProductIms.enableGridMenu = response.data[0].DownLoadStatus;
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


    var columnDivisionMarketIms = [        
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
        { name: 'BaseProductCode', displayName: "Product Code", width: 150 },
        { name: 'BaseProductName', displayName: "Product Name", width: 150 },
        { name: 'BrandCode', displayName: "Brand Code", width: 150 },
        { name: 'BrandName', displayName: "Brand Name", width: 150 },
        { name: 'ProductCategoryCode', displayName: "Product Category Code", width: 150 },
        { name: 'ProductCategoryName', displayName: "Product Category Name", width: 150 },
        { name: 'InvoiceNo', displayName: "InvoiceNo", width: 150 },
        { name: 'InvoiceDate', displayName: "InvoiceDate", width: 150 },
        { name: 'ProductCode', displayName: "SKU Code", width: 150 },
        { name: 'ProductName', displayName: "SKU Name", width: 150 },
        { name: 'PackSize', displayName: "Pack Size", width: 150 },
        { name: 'ProductPrice', displayName: "Product Price", width: 120, cellFilter: 'number:2', cellClass: 'grid-align', footerCellFilter: 'number:2' },
        { name: 'InvoiceAmt', displayName: "Invoice Amt", width: 120, cellFilter: 'number:2', cellClass: 'grid-align', footerCellFilter: 'number:2', aggregationType: uiGridConstants.aggregationTypes.sum },
        { name: 'SalesQty', displayName: "Sales Qty", width: 120, cellFilter: 'number:2', cellClass: 'grid-align', footerCellFilter: 'number:2', aggregationType: uiGridConstants.aggregationTypes.sum },
        { name: 'SalesBonusQty', displayName: "Sales Bonus Qty", width: 120, cellFilter: 'number:2', cellClass: 'grid-align', footerCellFilter: 'number:2', aggregationType: uiGridConstants.aggregationTypes.sum },
        { name: 'BonusPriceDiscount', displayName: "Bonus Price Discount", width: 120, cellFilter: 'number:2', cellClass: 'grid-align', footerCellFilter: 'number:2', aggregationType: uiGridConstants.aggregationTypes.sum },
        { name: 'ReplaceQty', displayName: "Replace Qty", width: 120, cellFilter: 'number:2', cellClass: 'grid-align', footerCellFilter: 'number:2', aggregationType: uiGridConstants.aggregationTypes.sum },
        { name: 'ReturnSalesQty', displayName: "Return Sales Qty", width: 120, cellFilter: 'number:2', cellClass: 'grid-align', footerCellFilter: 'number:2', aggregationType: uiGridConstants.aggregationTypes.sum },
        { name: 'ReturnBnsQty', displayName: "Return Bonus Qty", width: 150, cellFilter: 'number:2', cellClass: 'grid-align', footerCellFilter: 'number:2', aggregationType: uiGridConstants.aggregationTypes.sum },
        { name: 'ImsSalesQty', displayName: "IMS Sales Qty", width: 120, cellFilter: 'number:2', cellClass: 'grid-align', footerCellFilter: 'number:2', aggregationType: uiGridConstants.aggregationTypes.sum },
        { name: 'ImsBnsQty', displayName: "IMS Bonus Qty", width: 150, cellFilter: 'number:2', cellClass: 'grid-align', footerCellFilter: 'number:2', aggregationType: uiGridConstants.aggregationTypes.sum },
        { name: 'ReturnValue', displayName: "Return Value", width: 120, cellFilter: 'number:2', cellClass: 'grid-align', footerCellFilter: 'number:2', aggregationType: uiGridConstants.aggregationTypes.sum },
        { name: 'BnsDiscRet', displayName: "Return Bonus Discount", width: 120, cellFilter: 'number:2', cellClass: 'grid-align', footerCellFilter: 'number:2', aggregationType: uiGridConstants.aggregationTypes.sum },
        { name: 'DiscountVal', displayName: "Discount Value", width: 120, cellFilter: 'number:2', cellClass: 'grid-align', footerCellFilter: 'number:2', aggregationType: uiGridConstants.aggregationTypes.sum },
        { name: 'NetIms', displayName: "Net IMS", width: 120, cellFilter: 'number:2', cellClass: 'grid-align', footerCellFilter: 'number:2', aggregationType: uiGridConstants.aggregationTypes.sum },        
        { name: 'TargetQty', displayName: "Target Qty", width: 120, cellFilter: 'number:2', cellClass: 'grid-align', footerCellFilter: 'number:2', aggregationType: uiGridConstants.aggregationTypes.sum }

    ];
    $scope.gridInvoiceProductIms = {
        showColumnFooter: true,
        enableFiltering: true,
        enableSorting: true,
        columnDefs: columnDivisionMarketIms,
        enableGridMenu: true,
        enableSelectAll: true,
        exporterCsvFilename: "Invoice_Product_IMS.csv",
        exporterMenuPdf: false,
        exporterCsvLinkElement: angular.element(document.querySelectorAll(".custom-csv-link-location")),
        onRegisterApi: function (gridApi) {
            $scope.gridApi = gridApi;
        }
    };



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

    $scope.OnBrandClick = function () {

        $scope.GetProductList();
    }


    $scope.OnCategoryClick = function () {

        $scope.GetProductList();
    }


    $scope.OnBaseProductClick = function () {

        $scope.GetProductList();
    }


    $scope.GetProductList = function () {
        $http({
            method: "GET",
            url: MyApp.rootPath + "InvoiceProductIms/GetProductList",
            params: {
                baseProductCode: $scope.frmInvoiceProductIms.BaseProduct.BaseProductCode,
                brandCode: $scope.frmInvoiceProductIms.Brand.BrandCode,
                categoryCode: $scope.frmInvoiceProductIms.ProductCategory.CategoryCode
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






    $scope.GetInvoiceProductIms = function () {

        if ($scope.frmInvoiceProductIms.RepType.ReportTypeValue == "Today") {
            methodName = "GetInvoiceProductImsToday";
        }

        if ($scope.frmInvoiceProductIms.RepType.ReportTypeValue == "CurrentMonth") {
            methodName = "GetInvoiceProductImsCurrentMonth";
        }
        if ($scope.frmInvoiceProductIms.RepType.ReportTypeValue == "LastMonth") {
            methodName = "GetInvoiceProductImsLastMonth";
        }       
        if ($scope.frmInvoiceProductIms.RepType.ReportTypeValue == "CustomDate") {
            if ($scope.ToDate == "" || $scope.ToDate == undefined || $scope.ToDate == null) {
                toastr.warning("To Date  Cannot be empty !");
                return false;
            } else {
                var todayDate = new Date(new Date().getFullYear(), new Date().getMonth(), new Date().getDate());
                var endDate = $scope.ToDate.split("/");
                var convertedEndDate = new Date(+endDate[2], endDate[1] - 1, +endDate[0]);
                var eDate = new Date(convertedEndDate.getFullYear(), convertedEndDate.getMonth(), convertedEndDate.getDate());
                if (eDate >= todayDate) {
                    toastr.warning("To Date  Less Than Current Date !");
                    return false;
                } else {
                    methodName = "GetInvoiceProductImsDateRange";

                }
            }
        }

        $http({
            method: "POST",
            url: MyApp.rootPath + "InvoiceProductIms/" + methodName,
            data: {
                fDate: $scope.FromDate,
                tDate: $scope.ToDate,
                dCode: $scope.frmInvoiceProductIms.Division.DivisionCode,
                rCode: $scope.frmInvoiceProductIms.Region.RegionCode,
                aCode: $scope.frmInvoiceProductIms.Area.AreaCode,
                tCode: $scope.frmInvoiceProductIms.Territory.TerritoryCode,
                cCode: $scope.frmInvoiceProductIms.Customer.CustomerCode,
                pBaseProductCode: $scope.frmInvoiceProductIms.BaseProduct.BaseProductCode,
                pBrandCode: $scope.frmInvoiceProductIms.Brand.BrandCode,
                pCategoryCode: $scope.frmInvoiceProductIms.ProductCategory.CategoryCode,
                pCode: $scope.frmInvoiceProductIms.Product.ProductCode
            }
        }).then(function (response) {
            if (response.data.length > 0) {
                $scope.gridInvoiceProductIms.data = response.data;
            }
            else {
                toastr.warning("No Data Found!", '');
                $scope.gridInvoiceProductIms.data = [];
            }
        }, function (response) {
            //alert(response);
            toastr.error("Error!");
        });
    };


    $scope.OnReportTypeChange = function () {
        if ($scope.frmInvoiceProductIms.RepType.ReportTypeValue == "CurrentMonth" || $scope.frmInvoiceProductIms.RepType.ReportTypeValue == "LastMonth" || $scope.frmInvoiceProductIms.RepType.ReportTypeValue == "Today") {
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
        $scope.dCode = $scope.frmInvoiceProductIms.Division.DivisionCode;
        //Region List
        $http({
            method: "GET",
            url: MyApp.rootPath + "DistSrSales/GetRegionList",
            params: { dCode: $scope.frmInvoiceProductIms.Division.DivisionCode }
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
        $scope.rCode = $scope.frmInvoiceProductIms.Region.RegionCode;
        //Area List
        $http({
            method: "GET",
            url: MyApp.rootPath + "DistSrSales/GetAreaList",
            params: { dCode: $scope.frmInvoiceProductIms.Division.DivisionCode, rCode: $scope.frmInvoiceProductIms.Region.RegionCode }
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
        $scope.aCode = $scope.frmInvoiceProductIms.Area.AreaCode;
        //Territory List
        $http({
            method: "GET",
            url: MyApp.rootPath + "DistSrSales/GetTerritoryList",
            params: { dCode: $scope.frmInvoiceProductIms.Division.DivisionCode, rCode: $scope.frmInvoiceProductIms.Region.RegionCode, aCode: $scope.frmInvoiceProductIms.Area.AreaCode }
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
        $scope.tCode = $scope.frmInvoiceProductIms.Territory.TerritoryCode;
        //Customer List
        $http({
            method: "GET",
            url: MyApp.rootPath + "DistSrSales/GetCustomerList",
            params: { dCode: $scope.frmInvoiceProductIms.Division.DivisionCode, rCode: $scope.frmInvoiceProductIms.Region.RegionCode, aCode: $scope.frmInvoiceProductIms.Area.AreaCode, tCode: $scope.frmInvoiceProductIms.Territory.TerritoryCode }
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
        $scope.cCode = $scope.frmInvoiceProductIms.Customer.CustomerCode;
        //SR List
        $http({
            method: "GET",
            url: MyApp.rootPath + "DistSrSales/GetSrList",
            params: { dCode: $scope.frmInvoiceProductIms.Division.DivisionCode, rCode: $scope.frmInvoiceProductIms.Region.RegionCode, aCode: $scope.frmInvoiceProductIms.Area.AreaCode, tCode: $scope.frmInvoiceProductIms.Territory.TerritoryCode, cCode: $scope.frmInvoiceProductIms.Customer.CustomerCode }
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
            params: { SlNo: "(4,5,9,0)" }
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



    $scope.Reset = function () {
        $scope.FromDate = "";
        $scope.ToDate = "";
        $scope.gridInvoiceProductIms.data = [];
        $scope.isDisabled = false;
        $scope.ReportType = "";
        $scope.frmInvoiceProductIms.RepType = undefined;
        $scope.frmInvoiceProductIms.Division = undefined;
        $scope.frmInvoiceProductIms.Region = undefined;
        $scope.frmInvoiceProductIms.Area = undefined;
        $scope.frmInvoiceProductIms.Territory = undefined;
        $scope.frmInvoiceProductIms.Customer = undefined;

        $scope.frmInvoiceProductIms.BaseProduct = undefined;
        $scope.frmInvoiceProductIms.Brand = undefined;
        $scope.frmInvoiceProductIms.ProductCategory = undefined;


        $scope.frmInvoiceProductIms.Product = undefined;
        $scope.Divisions = [];
        $scope.Regions = [];
        $scope.Areas = [];
        $scope.Territories = [];
        $scope.Customers = [];
        $scope.Products = [];
        $scope.isDisabled = true;


        $scope.LocationType = "ALL";
    };

});