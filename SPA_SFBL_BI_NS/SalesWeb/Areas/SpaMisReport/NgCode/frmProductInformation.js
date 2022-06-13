app.controller("ProductInformationCtrl", function ($scope, $http, uiGridConstants, uiGridExporterService) {

    $scope.isDisabledFromDate = true;

    var methodName = "";




    $scope.EventPerm(22);
    $scope.Status = 'ALL';

    $scope.GetReportDownLoadStatus = function () {
        $http({
            method: "GET",
            url: MyApp.rootPath + "RetailerIms/GetReportDownLoadStatus",
            datatype: "json",
            params: { url: window.location.pathname }
        }).then(function (response) {
            if (response.data.length > 0) {
                $scope.gridProductInformation.enableGridMenu = response.data[0].DownLoadStatus;
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
            params: { SlNo: "(13,14)" }
        }).then(function (response) {
            if (response.data.Data.length > 0) {
                $scope.RepTypes = response.data.Data;
            } else {
                toastr.warning("No Data Found!", { timeOut: 2000 });
            }

        }, function (response) {
            if (response.status === 404) {
                toastr.warning("Error Loading Report Type List!", { timeOut: 2000 });
            }
        });

    }
    $scope.GetReportTypeList();


    var columnProductInformation = [
        //{ name: 'SlNo', displayName: "Sl. No", type: 'number', width: 100, cellClass: 'grid-align', footerCellFilter: 'number:2' },
        { name: 'ProductCode', displayName: "Product Code", width: 150, aggregationType: uiGridConstants.aggregationTypes.count, footerCellFilter: 'number:0'},
        { name: 'ProductName', displayName: "Product Name", width: 250 },
        { name: 'ProductNameBn', displayName: "Product Name (Bangla)", width: 250 },
        { name: 'PackSize', displayName: "Pack Size", width: 150 },
        { name: 'BaseProductCode', displayName: "Base Product Code", width: 150 },
        { name: 'BaseProductName', displayName: "Base Product Name", width: 150 },
        { name: 'BrandCode', displayName: "Brand Code", width: 150 },
        { name: 'BrandName', displayName: "Brand Name", width: 150 },
        { name: 'CategoryCode', displayName: "Product Category Code", width: 150 },
        { name: 'CategoryName', displayName: "Product Category Name", width: 150 },
        { name: 'BonusAllow', displayName: "Bonus Allow", width: 150 },
        { name: 'DiscountAllow', displayName: "Discount Allow", width: 150 },
        { name: 'DiscountType', displayName: "Discount Type", width: 150 },
        { name: 'DiscountVal', displayName: "Discount Val", width: 150, cellClass: 'grid-align' },
        { name: 'ShipperQty', displayName: "Shipper Qty", width: 150 },
        { name: 'Status', displayName: "Status", width: 150},
        { name: 'CpFlag', displayName: "CP Flag", width: 150},
        { name: 'UnitTp', displayName: "Unit Tp", width: 150, cellClass: 'grid-align'},
        { name: 'UnitVat', displayName: "Unit Vat", width: 150, cellClass: 'grid-align'},
        { name: 'Mrp', displayName: "Mrp", width: 150, cellClass: 'grid-align' },

        { name: 'FirstInvoiceDate', displayName: "First Invoice Date", width: 150 },
        { name: 'LastInvoiceDate',  displayName: "Last Invoice Date", width: 150 }
    ];



    $scope.gridProductInformation = {
        showColumnFooter: true,
        //showColumnFooter: true,
        enableFiltering: true,
        enableSorting: true,
        columnDefs: columnProductInformation,
        enableGridMenu: true,
        enableSelectAll: true,
        exporterCsvFilename: 'Product_information.csv',
        exporterMenuPdf: false,
        exporterCsvLinkElement: angular.element(document.querySelectorAll(".custom-csv-link-location")),
        onRegisterApi: function (gridApi) {
            $scope.gridApi = gridApi;
        }
    };

    $scope.OnReportTypeChange = function () {


        if ($scope.frmProductInformation.RepType.ReportTypeValue == "ProductInformation") {

            $scope.isDisabledFromDate = true;
            $scope.FromDate = "";

        }
        else {
            $scope.isDisabledFromDate = false;
            $scope.FromDate = "";

        }
    };


    $scope.GetProductInformation = function () {

        if ($scope.frmProductInformation.RepType.ReportTypeValue == "ProductInformation") {

            methodName = "GetProductInformationList";

        }


        if ($scope.frmProductInformation.RepType.ReportTypeValue == "DeadProduct") {

            methodName = "GetDeadProductInformationList";

        }


        $http({
            method: "POST",
            url: MyApp.rootPath + "ProductInformation/" + methodName,
            data: {
                LastInvoiceDate: $scope.FromDate,
                base_product_code: $scope.frmProductInformation.BaseProduct.BaseProductCode,
                brand_code: $scope.frmProductInformation.Brand.BrandCode,
                product_category: $scope.frmProductInformation.ProductCategory.CategoryCode,
                status: $scope.Status

            }
        }).then(function (response) {
            if (response.data.length > 0) {
                $scope.gridProductInformation.data = response.data;

            }
            else {
                toastr.warning("No Data Found!", '');
                $scope.gridProductInformation.data = [];
            }
        }, function (response) {
            toastr.error("Error!");
        });
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


    $scope.Reset = function () {
        $scope.FromDate = "";
        $scope.ToDate = "";
        $scope.isDisabledFromDate = true;
        $scope.isDisabledToDate = true;
        $scope.ReportType = "";

        $scope.frmProductInformation.RepType = undefined;
        $scope.frmProductInformation.Division = undefined;
        $scope.frmProductInformation.Region = undefined;
        $scope.frmProductInformation.Area = undefined;
        $scope.frmProductInformation.Territory = undefined;
        $scope.frmProductInformation.Customer = undefined;
        $scope.frmProductInformation.Market = undefined;
        $scope.frmProductInformation.Category = undefined;
        $scope.RetailerType = 'ALL';
        $scope.RetailerLocationType = 'ALL';
        $scope.Status = 'ALL';


        $scope.Divisions = [];
        $scope.Regions = [];
        $scope.Areas = [];
        $scope.Territories = [];
        $scope.Customers = [];
        $scope.Markets = [];
        //$scope.Categories = [];

        $scope.FormDate = "From Date";
        $scope.gridProductInformation.data = [];


    };

});