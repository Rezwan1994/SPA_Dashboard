app.controller("MarketProductImsCtrl", function ($scope, $http, uiGridConstants) {
    $scope.isDisabled = true;
    var methodName = "";
    $scope.EventPerm(22);


    $scope.GetReportTypeList = function () {
        $http({
            method: "GET",
            url: MyApp.rootPath + "DistSrSales/GetReportTypeList",
            params: { SlNo: "(0,1,2,3,4,5,9)" }
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

    $scope.OnReportTypeChange = function () {

        if ($scope.frmMarketProductIms.RepType.ReportTypeValue == "Today" || $scope.frmMarketProductIms.RepType.ReportTypeValue == "Yesterday" || $scope.frmMarketProductIms.RepType.ReportTypeValue == "LastSevendays" || $scope.frmMarketProductIms.RepType.ReportTypeValue == "LastThirtydays" || $scope.frmMarketProductIms.RepType.ReportTypeValue == "CurrentMonth" || $scope.frmMarketProductIms.RepType.ReportTypeValue == "LastMonth" || $scope.frmMarketProductIms.RepType.ReportTypeValue == "MonthOnMonthCy" || $scope.frmMarketProductIms.RepType.ReportTypeValue == "MonthOnMonthLy") {
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


    $scope.GetReportDownLoadStatus = function () {
        $http({
            method: "GET",
            url: MyApp.rootPath + "RetailerIms/GetReportDownLoadStatus",
            datatype: "json",
            params: { url: window.location.pathname }
        }).then(function (response) {
            if (response.data.length > 0) {
                $scope.gridMarketProductIms.enableGridMenu = response.data[0].DownLoadStatus;
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

    //-------------Grid------------------//

    var columnMarketProductIms = [
        { name: 'DivisionCode', displayName: "Division Code", width: 150, aggregationType: uiGridConstants.aggregationTypes.count, footerCellFilter: 'number:0' },
        { name: 'DivisionName', displayName: "Division Name", width: 150 },
        { name: 'RegionCode', displayName: "Region Code", width: 150 },
        { name: 'RegionName', displayName: "Region Name", width: 150 },
        { name: 'AreaCode', displayName: "Area Code", width: 150 },
        { name: 'AreaName', displayName: "Area Name", width: 150 },
        { name: 'TerritoryCode', displayName: "Territory Code", width: 150 },
        { name: 'TerritoryName', displayName: "Territory Name", width: 150 },
        { name: 'DBLoaction', displayName: "DB Loaction", width: 150 },
        { name: 'CustomerCode', displayName: "Distributor Code", width: 150 },
        { name: 'CustomerName', displayName: "Distributor Name", width: 150 },
        { name: 'MarketCode', displayName: "Market Code", width: 150 },
        { name: 'MarketName', displayName: "Market Name", width: 150 },
        { name: 'ProductCode', displayName: "Product Code", width: 150 },
        { name: 'ProductName', displayName: "Product Name", width: 200 },
        { name: 'PackSize', displayName: "Pac kSize", width: 150 },
        { name: 'ProductPrice', displayName: "Product Price", width: 120, cellFilter: 'number:2', cellClass: 'grid-align', footerCellFilter: 'number:2' },

        { name: 'InvoiceQty', displayName: "Sales Qty", width: 120, cellFilter: 'number:2', cellClass: 'grid-align', footerCellFilter: 'number:2', aggregationType: uiGridConstants.aggregationTypes.sum},
        { name: 'ReturnSalesQty', displayName: "Return Sales Qty", width: 120, cellFilter: 'number:2', cellClass: 'grid-align', footerCellFilter: 'number:2', aggregationType: uiGridConstants.aggregationTypes.sum},
        { name: 'IMSSalesQty', displayName: "IMS Sales Qty", width: 120, cellFilter: 'number:2', cellClass: 'grid-align', footerCellFilter: 'number:2', aggregationType: uiGridConstants.aggregationTypes.sum},

        { name: 'InvBonusQty', displayName: "Bonus Qty", width: 120, cellFilter: 'number:2', cellClass: 'grid-align', footerCellFilter: 'number:2', aggregationType: uiGridConstants.aggregationTypes.sum},
        { name: 'ReturnBnsQty', displayName: "Return Bonus Qty", width: 120, cellFilter: 'number:2', cellClass: 'grid-align', footerCellFilter: 'number:2', aggregationType: uiGridConstants.aggregationTypes.sum},
        { name: 'IMSBnsQty', displayName: "IMS Bonus Qty", width: 120, cellFilter: 'number:2', cellClass: 'grid-align', footerCellFilter: 'number:2', aggregationType: uiGridConstants.aggregationTypes.sum},

        { name: 'InvoiceAmt', displayName: "Sales Value", width: 120, cellFilter: 'number:2', aggregationType: uiGridConstants.aggregationTypes.sum, cellClass: 'grid-align', footerCellFilter: 'number:2' },
        { name: 'ReturnValue', displayName: "Return Value", width: 120, cellFilter: 'number:2', aggregationType: uiGridConstants.aggregationTypes.sum, cellClass: 'grid-align', footerCellFilter: 'number:2' },
        { name: 'IMSSalesVal', displayName: "IMS Sales Value", width: 120, cellFilter: 'number:2', aggregationType: uiGridConstants.aggregationTypes.sum, cellClass: 'grid-align', footerCellFilter: 'number:2' },
        { name: 'IMSBnsVal', displayName: "IMS Bonus Value", width: 120, cellFilter: 'number:2', aggregationType: uiGridConstants.aggregationTypes.sum, cellClass: 'grid-align', footerCellFilter: 'number:2' },


        { name: 'BonusPriceDiscount', displayName: "Bonus Discount", width: 120, cellFilter: 'number:2', aggregationType: uiGridConstants.aggregationTypes.sum, cellClass: 'grid-align', footerCellFilter: 'number:2' },                
        { name: 'BnsDiscRet', displayName: "Return Bonus Discount", width: 150, cellFilter: 'number:2', aggregationType: uiGridConstants.aggregationTypes.sum, cellClass: 'grid-align', footerCellFilter: 'number:2' },

        { name: 'NetIMS', displayName: "Net IMS", width: 120, cellFilter: 'number:2', aggregationType: uiGridConstants.aggregationTypes.sum, cellClass: 'grid-align', footerCellFilter: 'number:2' },

        { name: 'BonusPer', displayName: "Bonus(%)", width: 120, cellFilter: 'number:2', cellClass: 'grid-align', footerCellFilter: 'number:2' },
        { name: 'TargetQty', displayName: "Target Qty", width: 120, cellFilter: 'number:2', cellClass: 'grid-align', footerCellFilter: 'number:2', aggregationType: uiGridConstants.aggregationTypes.sum},
        { name: 'TargetVal', displayName: "Target Value", width: 120, cellFilter: 'number:2', aggregationType: uiGridConstants.aggregationTypes.sum, cellClass: 'grid-align', footerCellFilter: 'number:2' },

        { name: 'LastYearAsOnDateImsQty', displayName: "Last Year As On Date IMS Qty", width: 120, cellFilter: 'number:2', cellClass: 'grid-align', footerCellFilter: 'number:2', aggregationType: uiGridConstants.aggregationTypes.sum },
        { name: 'LastYearAsOnDateImsVal', displayName: "Last Year As On Date IMS Val", width: 120, cellFilter: 'number:2', cellClass: 'grid-align', footerCellFilter: 'number:2', aggregationType: uiGridConstants.aggregationTypes.sum }
    ];
    $scope.gridMarketProductIms = {
        showColumnFooter: true,
        exporterMenuCsv: true,
        enableFiltering: true,
        enableSorting: true,
        columnDefs: columnMarketProductIms,
        enableGridMenu: true,
        enableSelectAll: true,
        exporterCsvFilename: 'Market_Wise_Product_IMS.csv',
        exporterMenuPdf: false,
        exporterCsvLinkElement: angular.element(document.querySelectorAll(".custom-csv-link-location")),
        onRegisterApi: function (gridApi) {
            $scope.gridApi = gridApi;
        }
    };


    $scope.GetMarketProductIms = function () {


        $scope.gridMarketProductIms.data = [];
        if ($scope.frmMarketProductIms.RepType.ReportTypeValue == "Today") {
            methodName = "GetMarketProductImsToday";
        }
        if ($scope.frmMarketProductIms.RepType.ReportTypeValue == "Yesterday") {
            methodName = "GetMarketProductImsYesterday";
        }
        if ($scope.frmMarketProductIms.RepType.ReportTypeValue == "LastSevendays") {
            methodName = "GetMarketProductImsLastSevendays";
        }
        if ($scope.frmMarketProductIms.RepType.ReportTypeValue == "LastThirtydays") {
            methodName = "GetMarketProductImsLastThirtydays";
        }
        if ($scope.frmMarketProductIms.RepType.ReportTypeValue == "CurrentMonth") {
            methodName = "GetMarketProductImsCurrentMonth";
        }
        if ($scope.frmMarketProductIms.RepType.ReportTypeValue == "LastMonth") {
            methodName = "GetMarketProductImsLastMonth";
        }
        if ($scope.frmMarketProductIms.RepType.ReportTypeValue == "CustomDate") {
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
                    methodName = "GetMarketProductImsDateRange";
                }
            }
        }

        $http({
            method: "POST",
            url: MyApp.rootPath + "MarketProductIms/" + methodName,
            data: {
                fDate: $scope.FromDate,
                tDate: $scope.ToDate,
                dCode: $scope.frmMarketProductIms.Division.DivisionCode,
                rCode: $scope.frmMarketProductIms.Region.RegionCode,
                aCode: $scope.frmMarketProductIms.Area.AreaCode,
                tCode: $scope.frmMarketProductIms.Territory.TerritoryCode,
                cCode: $scope.frmMarketProductIms.Customer.CustomerCode
            }
        }).then(function (response) {
            if (response.data.length > 0) {
                $scope.gridMarketProductIms.data = response.data;

            }
            else {
                toastr.warning("No Data Found!", '');
                $scope.gridMarketProductIms.data = [];
            }
            }, function () {
                alert(response);
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
            params: { dCode: $scope.frmMarketProductIms.Division.DivisionCode }
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
            params: { dCode: $scope.frmMarketProductIms.Division.DivisionCode, rCode: $scope.frmMarketProductIms.Region.RegionCode }
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
            params: { dCode: $scope.frmMarketProductIms.Division.DivisionCode, rCode: $scope.frmMarketProductIms.Region.RegionCode, aCode: $scope.frmMarketProductIms.Area.AreaCode }
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
            params: { dCode: $scope.frmMarketProductIms.Division.DivisionCode, rCode: $scope.frmMarketProductIms.Region.RegionCode, aCode: $scope.frmMarketProductIms.Area.AreaCode, tCode: $scope.frmMarketProductIms.Territory.TerritoryCode }
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


    $scope.Reset = function () {
        $scope.FromDate = "";
        $scope.ToDate = "";
        $scope.gridMarketProductIms.data = [];
        $scope.isDisabled = true;
        $scope.frmMarketProductIms.RepType = undefined;
        $scope.frmMarketProductIms.Division = undefined;
        $scope.frmMarketProductIms.Region = undefined;
        $scope.frmMarketProductIms.Area = undefined;
        $scope.frmMarketProductIms.Territory = undefined;
        $scope.frmMarketProductIms.Customer = undefined;

        $scope.Divisions = [];
        $scope.Regions = [];
        $scope.Areas = [];
        $scope.Territories = [];
        $scope.Customers = [];


    };

});