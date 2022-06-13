app.controller("NationalProductSalesCtrl", function ($scope, $http, uiGridConstants) {

    $scope.isDisabled = true;

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
                $scope.gridNationalProductSales.enableGridMenu = response.data[0].DownLoadStatus;
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

    var columnNationalProductSales = [

        { name: 'BaseProductCode', displayName: "Base Product Code", width: 150, aggregationType: uiGridConstants.aggregationTypes.count, footerCellFilter: 'number:0' },
        { name: 'BaseProductName', displayName: "Base Product Name", width: 200 },

        { name: 'CategoryCode', displayName: "Category Code", width: 150 },
        { name: 'CategoryName', displayName: "Category Name", width: 200 },

        { name: 'BrandCode', displayName: "Brand Code", width: 150 },
        { name: 'BrandName', displayName: "Brand Name", width: 200 },

        { name: 'ProductCode', displayName: "Product Code", width: 150},
        { name: 'ProductName', displayName: "Product Name", width: 200 },
        { name: 'PackSize', displayName: "Pack Size", width: 150 },

        { name: 'CurrentMonthSalesVal', displayName: "Current Month Sales Value", width: 200, cellFilter: 'number:2', cellClass: 'grid-align', footerCellFilter: 'number:2', aggregationType: uiGridConstants.aggregationTypes.sum },
        { name: 'CurrentMonthReturnVal', displayName: "Current Month Return Value", width: 200, cellFilter: 'number:2', cellClass: 'grid-align', footerCellFilter: 'number:2', aggregationType: uiGridConstants.aggregationTypes.sum },
        { name: 'CurrentMonthNetSalesVal', displayName: "Current Month NetSales Value", width: 200, cellFilter: 'number:2', cellClass: 'grid-align', footerCellFilter: 'number:2', aggregationType: uiGridConstants.aggregationTypes.sum },
        { name: 'CurrentMonthSalesQty', displayName: "Current Month Sales Qty", width: 200, cellClass: 'grid-align', footerCellFilter: 'number:2' },
        { name: 'CurrentMonthReturnQty', displayName: "Current Month Return Qty", width: 200, cellClass: 'grid-align', footerCellFilter: 'number:2' },
        { name: 'CurrentMonthNetSalesQty', displayName: "Current Month NetSales Qty", width: 250, cellClass: 'grid-align', footerCellFilter: 'number:2' },
        { name: 'LastMonthSalesVal', displayName: "Last Month Sales Value", width: 200, cellFilter: 'number:2', cellClass: 'grid-align', footerCellFilter: 'number:2', aggregationType: uiGridConstants.aggregationTypes.sum  },
        { name: 'LastMonthReturnVal', displayName: "Last Month Return Value", width: 200, cellFilter: 'number:2', cellClass: 'grid-align', footerCellFilter: 'number:2', aggregationType: uiGridConstants.aggregationTypes.sum },
        { name: 'LastMonthNetSalesVal', displayName: "Last Month Net Sales Value", width: 200, cellFilter: 'number:2', cellClass: 'grid-align', footerCellFilter: 'number:2', aggregationType: uiGridConstants.aggregationTypes.sum },
        { name: 'LastMonthSalesQty', displayName: "Last Month Sales Qty", width: 200, cellClass: 'grid-align', footerCellFilter: 'number:2' },
        { name: 'LastMonthReturnQty', displayName: "Last Month Return Qty", width: 200, cellClass: 'grid-align', footerCellFilter: 'number:2' },
        { name: 'LastMonthNetSalesQty', displayName: "Last Month Net Sales Qty", width: 200, cellClass: 'grid-align', footerCellFilter: 'number:2' },
        { name: 'CurrentMonthTargetQty', displayName: "Current Month Target Qty", width: 200, cellClass: 'grid-align', footerCellFilter: 'number:2' },
        { name: 'CurrentMonthTargetVal', displayName: "Current Month Target Value", width: 200, cellFilter: 'number:2', cellClass: 'grid-align', footerCellFilter: 'number:2', aggregationType: uiGridConstants.aggregationTypes.sum },
        { name: 'CurrentMonthAch', displayName: "Current Month Ach", width: 200, cellFilter: 'number:2', cellClass: 'grid-align', footerCellFilter: 'number:2' },
        { name: 'CurrentMonthGrowth', displayName: "Current Month Growth", width: 200, cellFilter: 'number:2', cellClass: 'grid-align', footerCellFilter: 'number:2' },
        { name: 'CurrentYearImsVal', displayName: "Current Year Ims Value", width: 200, cellFilter: 'number:2', cellClass: 'grid-align', footerCellFilter: 'number:2', aggregationType: uiGridConstants.aggregationTypes.sum },      
        { name: 'LastYearImsVal', displayName: "Last Year Ims Value", width: 200, cellFilter: 'number:2', cellClass: 'grid-align', footerCellFilter: 'number:2', aggregationType: uiGridConstants.aggregationTypes.sum }
    ];
    $scope.gridNationalProductSales = {
        showColumnFooter: true,
        enableFiltering: true,
        enableSorting: true,
        columnDefs: columnNationalProductSales,
        enableGridMenu: true,
        enableSelectAll: true,
        exporterCsvFilename: 'National_Product_Sales.csv',
        exporterMenuPdf: false,
        exporterCsvLinkElement: angular.element(document.querySelectorAll(".custom-csv-link-location")),
        onRegisterApi: function (gridApi) {
            $scope.gridApi = gridApi;
        }
    };
    $scope.GetReportTypeList = function () {
        $http({
            method: "GET",
            url: MyApp.rootPath + "DistSrSales/GetReportTypeList",
            params: { SlNo: "(4,9)" }
        }).then(function (response) {
            if (response.data.Data.length > 0) {
                $scope.RepTypes = response.data.Data;
            } else {
                toastr.warning("No Report Type Data Found!", { timeOut: 2000 });
            }

        }, function (response) {
            if (response.status === 404) {
                toastr.warning("Error Loading Report Type List!", { timeOut: 2000 });
            }
        });

    }
    $scope.GetReportTypeList();

    $scope.OnReportTypeChange = function () {

        if ($scope.frmNationalProductSales.RepType.ReportTypeValue == "Today" || $scope.frmNationalProductSales.RepType.ReportTypeValue == "Yesterday" || $scope.frmNationalProductSales.RepType.ReportTypeValue == "LastSevendays" || $scope.frmNationalProductSales.RepType.ReportTypeValue == "LastThirtydays" || $scope.frmNationalProductSales.RepType.ReportTypeValue == "CurrentMonth" || $scope.frmNationalProductSales.RepType.ReportTypeValue == "LastMonth" || $scope.frmNationalProductSales.RepType.ReportTypeValue == "MonthOnMonthCy" || $scope.frmNationalProductSales.RepType.ReportTypeValue == "MonthOnMonthLy") {
            $scope.isDisabled = true;
            $scope.FromDate = "";
            $scope.ToDate = "";
        }
        else {
            $scope.FromDate = "";
            $scope.ToDate = "";
            $scope.isDisabled = false;
        }

    };

    $scope.GetNationalProductSales = function () {

        $scope.gridNationalProductSales.data = [];

        if ($scope.frmNationalProductSales.RepType.ReportTypeValue == "CurrentMonth") {
            methodName = "GetNtlProductSalesCurMonth";

        }


        if ($scope.frmNationalProductSales.RepType.ReportTypeValue == "CustomDate") {


            if ($scope.ToDate == "" || $scope.ToDate == undefined || $scope.ToDate == null) {

                toastr.warning("To Date  Cannot be empty !");
                return false;


            } else {

                var fromDate = $scope.FromDate.split("/");
                var convertedFromDate = new Date(+fromDate[2], fromDate[1] - 1, +fromDate[0]);

                var todayDate = new Date(new Date().getFullYear(), new Date().getMonth(), new Date().getDate());

                var endDate = $scope.ToDate.split("/");
                var convertedEndDate = new Date(+endDate[2], endDate[1] - 1, +endDate[0]);
                var eDate = new Date(convertedEndDate.getFullYear(), convertedEndDate.getMonth(), convertedEndDate.getDate());


                var from_month = convertedFromDate.getMonth();
                var to_month = convertedEndDate.getMonth();

                if (from_month != to_month) {

                    toastr.warning("Please select same month date range !");
                    return false;

                }


                if (eDate >= todayDate) {
                    toastr.warning("To Date  Less Than Current Date !");
                    return false;
                } else {
                    methodName = "GetNtlProductSalesDateRange";
                }
            }
        }



        $http({
            method: "POST",
            url: MyApp.rootPath + "NationalProductSales/" + methodName,
            data: {
                fDate: $scope.FromDate,
                tDate: $scope.ToDate
            }
        }).then(function (response) {
            if (response.data.length > 0) {
                $scope.gridNationalProductSales.data = response.data;
            }
            else {
                toastr.warning("No Data Found!", '');
                $scope.gridNationalProductSales.data = [];
            }
        }, function () {
            toastr.error("Error!");
        });
    };


    $scope.Reset = function () {
        $scope.isDisabled = true;
        $scope.FromDate = "";
        $scope.ToDate = "";
        
        $scope.frmNationalProductSales.RepType = undefined;

        $scope.gridNationalProductSales.data = [];
    };

});