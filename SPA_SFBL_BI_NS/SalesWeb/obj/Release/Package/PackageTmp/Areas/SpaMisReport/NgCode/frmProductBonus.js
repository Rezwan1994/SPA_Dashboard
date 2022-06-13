app.controller("ProductBonusCtrl", function ($scope, $http, $interval, uiGridConstants, $filter) {

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
                $scope.gridProductBonus.enableGridMenu = response.data[0].DownLoadStatus;
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

    $scope.GetProductBonus = function () {

        methodName = "GetProductBonus";

        if ($scope.ToDate == "" || $scope.ToDate == undefined || $scope.ToDate == null) {
            toastr.warning("To Date  Cannot be empty !");
            return false;
        } 
  
        $http({
            method: "POST",
            url: MyApp.rootPath + "ProductBonus/" + methodName,
            data: {
                FromDate: $scope.FromDate,
                ToDate: $scope.ToDate,
                ProductCode: $scope.frmProductBonus.Product.ProductCode
            }
        }).then(function (response) {
            if (response.data.length > 0) {
                $scope.gridProductBonus.data = response.data;
            }
            else {
                toastr.warning("No Data Found!", '');
                $scope.gridProductBonus.data = [];
            }
        }, function (response) {
            toastr.error("Error!");
            });
    };

    //Grid
    var columnProductBonus = [
        //{ name: 'SlNo', displayName: "Sl. No", type: 'number', width: 100, cellClass: 'grid-align' },
        { name: 'SalesProductCode', displayName: "Sales Product Code", width: 150, aggregationType: uiGridConstants.aggregationTypes.count, footerCellFilter: 'number:0'},
        { name: 'SalesProductName', displayName: "Sales Product Name", width: 150 },
        { name: 'SalesProductPackSize', displayName: "Sales Product Pack Size", width: 150 },

        { name: 'EffectFormDate', displayName: "Effect Form Date", width: 150 },
        { name: 'EffectToDate', displayName: "Effect To Date", width: 150 },

        { name: 'BonusProductCode', displayName: "Bonus Product Code", width: 150 },
        { name: 'BonusProductName', displayName: "Bonus Product Name", width: 150 },
        { name: 'BonusProductPackSize', displayName: "Bonus Product Pack Size", width: 150 },

        { name: 'BonusSlabQty', displayName: "Bonus Slab Qty", width: 150 },
        { name: 'BonusPrdQty', displayName: "Bonus Product Qty", width: 150 },
        { name: 'BonusPriceDisc', displayName: "Bonus Price Disc", width: 150 },
        { name: 'BonusStatus', displayName: "Bonus Status", width: 150 },
        { name: 'PrdLocationTypeName', displayName: "Product Location Type Name", width: 150 },
        { name: 'BonusLocationCode', displayName: "Bonus Location Code", width: 150 },
        { name: 'BonusLocationName', displayName: "Bonus Location Name", width: 150 },

        { name: 'SalesQty', displayName: "Sales Qty", width: 150 },
        { name: 'PriceDiscount', displayName: "Price Discount", width: 120,  cellClass: 'grid-align' },
        { name: 'PriceStatus', displayName: "Price Status", width: 150 },
        { name: 'PriceLocationType', displayName: "Price Location Type", width: 120},
        { name: 'PriceLocationTypeName', displayName: "Price Location Type Name", width: 120},
        { name: 'PriceLocationCode', displayName: "Price Location Code", width: 120},
        { name: 'PriceLocationName', displayName: "Price Location Name", width: 120}
    ];
    $scope.gridProductBonus = {
        showColumnFooter: true,
        enableFiltering: true,
        enableSorting: true,
        columnDefs: columnProductBonus,
        enableGridMenu: true,
        enableSelectAll: true,
        exporterCsvFilename: "Product_Bonus.csv",
        exporterMenuPdf: false,
        exporterCsvLinkElement: angular.element(document.querySelectorAll(".custom-csv-link-location")),
        onRegisterApi: function (gridApi) {
            $scope.gridApi = gridApi;
        }
    };

    $scope.Reset = function () {
        $scope.FromDate = "";
        $scope.ToDate = "";            
        $scope.frmProductBonus.Product = undefined;

        $scope.gridProductBonus.data = [];   

    };

});