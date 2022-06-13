app.controller("DayWiseProductImsCtrl", function ($scope, $http, $interval, uiGridConstants, $filter) {

    var xl_file_name = "";
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
                $scope.gridDayWiseProductIms.enableGridMenu = response.data[0].DownLoadStatus;
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

    //Grid
    var columnDayWiseProductIms = [
        //{ name: 'SlNo', displayName: "Sl. No", type: 'number', width: 100, cellClass: 'grid-align', footerCellFilter: 'number:2' },
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
        { name: 'MarketCode', displayName: "Market Code", width: 150 },
        { name: 'MarketName', displayName: "Market Name", width: 150 },
        { name: 'ProductCode', displayName: "Product Code", width: 150 },
        { name: 'ProductName', displayName: "Product Name", width: 150 },
        { name: 'PackSize', displayName: "PackSize", width: 150 },
        { name: 'Day01', displayName: "Day 01", width: 100, cellClass: 'grid-align' },
        { name: 'Day02', displayName: "Day 02", width: 100, cellClass: 'grid-align' },
        { name: 'Day03', displayName: "Day 03", width: 100, cellClass: 'grid-align' },
        { name: 'Day04', displayName: "Day 04", width: 100, cellClass: 'grid-align' },
        { name: 'Day05', displayName: "Day 05", width: 100, cellClass: 'grid-align' },
        { name: 'Day06', displayName: "Day 06", width: 100, cellClass: 'grid-align' },
        { name: 'Day07', displayName: "Day 07", width: 100, cellClass: 'grid-align' },
        { name: 'Day08', displayName: "Day 08", width: 100, cellClass: 'grid-align' },
        { name: 'Day09', displayName: "Day 09", width: 100, cellClass: 'grid-align' },
        { name: 'Day10', displayName: "Day 10", width: 100, cellClass: 'grid-align' },
        { name: 'Day11', displayName: "Day 11", width: 100, cellClass: 'grid-align' },
        { name: 'Day12', displayName: "Day 12", width: 100, cellClass: 'grid-align' },
        { name: 'Day13', displayName: "Day 13", width: 100, cellClass: 'grid-align' },
        { name: 'Day14', displayName: "Day 14", width: 100, cellClass: 'grid-align' },
        { name: 'Day15', displayName: "Day 15", width: 100, cellClass: 'grid-align' },
        { name: 'Day16', displayName: "Day 16", width: 100, cellClass: 'grid-align' },
        { name: 'Day17', displayName: "Day 17", width: 100, cellClass: 'grid-align' },
        { name: 'Day18', displayName: "Day 18", width: 100, cellClass: 'grid-align' },
        { name: 'Day19', displayName: "Day 19", width: 100, cellClass: 'grid-align' },
        { name: 'Day20', displayName: "Day 20", width: 100, cellClass: 'grid-align' },
        { name: 'Day21', displayName: "Day 21", width: 100, cellClass: 'grid-align' },
        { name: 'Day22', displayName: "Day 22", width: 100, cellClass: 'grid-align' },
        { name: 'Day23', displayName: "Day 23", width: 100, cellClass: 'grid-align' },
        { name: 'Day24', displayName: "Day 24", width: 100, cellClass: 'grid-align' },
        { name: 'Day25', displayName: "Day 25", width: 100, cellClass: 'grid-align' },
        { name: 'Day26', displayName: "Day 26", width: 100, cellClass: 'grid-align' },
        { name: 'Day27', displayName: "Day 27", width: 100, cellClass: 'grid-align' },
        { name: 'Day28', displayName: "Day 28", width: 100, cellClass: 'grid-align' },
        { name: 'Day29', displayName: "Day 29", width: 100, cellClass: 'grid-align' },
        { name: 'Day30', displayName: "Day 30", width: 100, cellClass: 'grid-align' },
        { name: 'Day31', displayName: "Day 31", width: 100, cellClass: 'grid-align' },
        { name: 'TotalQty', displayName: "Total Qty", width: 100, cellClass: 'grid-align' },
        { name: 'TargetQty', displayName: "Target Qty", width: 100, cellClass: 'grid-align' }

    ];
    $scope.gridDayWiseProductIms = {
        showColumnFooter: true,
        enableFiltering: true,
        enableSorting: true,
        columnDefs: columnDayWiseProductIms,
        enableGridMenu: true,
        enableSelectAll: true,
        exporterCsvFilename: "Day_Wise_Product_IMS.csv",
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
            params: { SlNo: "(9)" }
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
        if ($scope.frmDayWiseProductIms.RepType.ReportTypeValue == "Today" || $scope.frmDayWiseProductIms.RepType.ReportTypeValue == "Yesterday" || $scope.frmDayWiseProductIms.RepType.ReportTypeValue == "LastSevendays" || $scope.frmDayWiseProductIms.RepType.ReportTypeValue == "LastThirtydays" || $scope.frmDayWiseProductIms.RepType.ReportTypeValue == "CurrentMonth" || $scope.frmDayWiseProductIms.RepType.ReportTypeValue == "LastMonth" || $scope.frmDayWiseProductIms.RepType.ReportTypeValue == "MonthOnMonthCy" || $scope.frmDayWiseProductIms.RepType.ReportTypeValue == "MonthOnMonthLy") {
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

    $scope.GetDayWiseProductIms = function () {

        if ($scope.frmDayWiseProductIms.RepType.ReportTypeValue == "CustomDate") {
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
                    methodName = "GetDayWiseProductIms";

                }
            }
        }

        $http({
            method: "POST",
            url: MyApp.rootPath + "DayWiseProductIms/" + methodName,
            data: {
                fDate: $scope.FromDate,
                tDate: $scope.ToDate,
                dCode: $scope.frmDayWiseProductIms.Division.DivisionCode,
                rCode: $scope.frmDayWiseProductIms.Region.RegionCode,
                aCode: $scope.frmDayWiseProductIms.Area.AreaCode,
                tCode: $scope.frmDayWiseProductIms.Territory.TerritoryCode,
                cCode: $scope.frmDayWiseProductIms.Customer.CustomerCode,
                pCode: $scope.frmDayWiseProductIms.Product.ProductCode
            }
        }).then(function (response) {
            if (response.data.length > 0) {
                $scope.gridDayWiseProductIms.data = response.data;
            }
            else {
                toastr.warning("No Data Found!", '');
                $scope.gridDayWiseProductIms.data = [];
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
            params: { dCode: $scope.frmDayWiseProductIms.Division.DivisionCode }
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
            params: { dCode: $scope.frmDayWiseProductIms.Division.DivisionCode, rCode: $scope.frmDayWiseProductIms.Region.RegionCode }
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
            params: { dCode: $scope.frmDayWiseProductIms.Division.DivisionCode, rCode: $scope.frmDayWiseProductIms.Region.RegionCode, aCode: $scope.frmDayWiseProductIms.Area.AreaCode }
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
            params: { dCode: $scope.frmDayWiseProductIms.Division.DivisionCode, rCode: $scope.frmDayWiseProductIms.Region.RegionCode, aCode: $scope.frmDayWiseProductIms.Area.AreaCode, tCode: $scope.frmDayWiseProductIms.Territory.TerritoryCode }
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



    $scope.Reset = function () {
        $scope.FromDate = "";
        $scope.ToDate = "";
        $scope.gridDayWiseProductIms.data = [];
        $scope.isDisabled = false;
        $scope.ReportType = "";
        $scope.frmDayWiseProductIms.RepType = undefined;
        $scope.frmDayWiseProductIms.Division = undefined;
        $scope.frmDayWiseProductIms.Region = undefined;
        $scope.frmDayWiseProductIms.Area = undefined;
        $scope.frmDayWiseProductIms.Territory = undefined;
        $scope.frmDayWiseProductIms.Customer = undefined;
        $scope.frmDayWiseProductIms.Product = undefined;
        $scope.Divisions = [];
        $scope.Regions = [];
        $scope.Areas = [];
        $scope.Territories = [];
        $scope.Customers = [];
        $scope.Products = [];
    };

});