app.controller("DistSalesSummaryCtrl", function ($scope, $http, uiGridConstants) {

    var methodName = "";
    $scope.EventPerm(22);

    $scope.isDisabled = true;
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

        if ($scope.frmDistSalesSummary.RepType.ReportTypeValue == "Today" || $scope.frmDistSalesSummary.RepType.ReportTypeValue == "Yesterday" || $scope.frmDistSalesSummary.RepType.ReportTypeValue == "LastSevendays" || $scope.frmDistSalesSummary.RepType.ReportTypeValue == "LastThirtydays" || $scope.frmDistSalesSummary.RepType.ReportTypeValue == "CurrentMonth" || $scope.frmDistSalesSummary.RepType.ReportTypeValue == "LastMonth" || $scope.frmDistSalesSummary.RepType.ReportTypeValue == "MonthOnMonthCy" || $scope.frmDistSalesSummary.RepType.ReportTypeValue == "MonthOnMonthLy") {
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
                $scope.gridDistSalesSummary.enableGridMenu = response.data[0].DownLoadStatus;
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

    var columnDistSalesSummary = [
        //{ name: 'SlNo', displayName: "Sl. No", type: 'number', width: 100, cellClass: 'grid-align', footerCellFilter: 'number:2' },
        { name: 'DivisionCode', displayName: "Division Code", width: 150, aggregationType: uiGridConstants.aggregationTypes.count, footerCellFilter: 'number:0'},
        { name: 'DivisionName', displayName: "Division Name", width: 150 },
        { name: 'RegionCode', displayName: "Region Code", width: 150 },
        { name: 'RegionName', displayName: "Region Name", width: 150 },
        { name: 'AreaCode', displayName: "Area Code", width: 150 },
        { name: 'AreaName', displayName: "Area Name", width: 150 },
        { name: 'TerritoryCode', displayName: "Territory Code", width: 150 },
        { name: 'TerritoryName', displayName: "Territory Name", width: 150 },
        { name: 'CustomerCode', displayName: "Distributor Code", width: 150 },
        { name: 'CustomerName', displayName: "Distributor Name", width: 150 },
        { name: 'DbLocation', displayName: "DB Loaction", width: 150 },
        //{ name: 'MarketCode', displayName: "Market Code", width: 150 },
        //{ name: 'MarketName', displayName: "Market Name", width: 150 },
        { name: 'ProductCode', displayName: "Product Code", width: 150 },
        { name: 'ProductName', displayName: "Product Name", width: 200 },
        { name: 'PackSize', displayName: "Pac kSize", width: 150 },
        { name: 'ProductPrice', displayName: "Product Price", width: 120, cellFilter: 'number:2', cellClass: 'grid-align', footerCellFilter: 'number:2' },

        { name: 'InvoiceQty', displayName: "Invoice Qty", width: 120, cellFilter: 'number:2', aggregationType: uiGridConstants.aggregationTypes.sum, cellClass: 'grid-align', footerCellFilter: 'number:2'},
        { name: 'InvBonusQty', displayName: "Bonus Qty", width: 120, cellFilter: 'number:2', aggregationType: uiGridConstants.aggregationTypes.sum, cellClass: 'grid-align', footerCellFilter: 'number:2'},
        { name: 'BonusPriceDiscount', displayName: "Bonus Discount", width: 120, cellFilter: 'number:2', aggregationType: uiGridConstants.aggregationTypes.sum, cellClass: 'grid-align', footerCellFilter: 'number:2'},
        { name: 'ImsSalesQty', displayName: "IMS Sales Qty", width: 120, cellFilter: 'number:2', aggregationType: uiGridConstants.aggregationTypes.sum, cellClass: 'grid-align', footerCellFilter: 'number:2'},
        { name: 'ImsBnsQty', displayName: "IMS Bonus Qty", width: 120, cellFilter: 'number:2', aggregationType: uiGridConstants.aggregationTypes.sum, cellClass: 'grid-align', footerCellFilter: 'number:2'},
        { name: 'InvoiceAmt', displayName: "Invoice Amt", width: 150, cellFilter: 'number:2', aggregationType: uiGridConstants.aggregationTypes.sum, cellClass: 'grid-align', footerCellFilter: 'number:2' },       
        { name: 'ReturnSalesQty', displayName: "Return Sales Qty", width: 120, cellFilter: 'number:2', aggregationType: uiGridConstants.aggregationTypes.sum, cellClass: 'grid-align', footerCellFilter: 'number:2'},
        { name: 'ReturnBnsQty', displayName: "Return Bonus Qty", width: 120, cellFilter: 'number:2', aggregationType: uiGridConstants.aggregationTypes.sum, cellClass: 'grid-align', footerCellFilter: 'number:2'},
        { name: 'BnsDiscRet', displayName: "Return Bonus Discount", width: 120, cellFilter: 'number:2', aggregationType: uiGridConstants.aggregationTypes.sum, cellClass: 'grid-align', footerCellFilter: 'number:2'},
        { name: 'ReturnValue', displayName: "Return Value", width: 120, cellFilter: 'number:2', aggregationType: uiGridConstants.aggregationTypes.sum, cellClass: 'grid-align', footerCellFilter: 'number:2' },
        { name: 'ImsSalesVal', displayName: "IMS Sales Value", width: 120, cellFilter: 'number:2', aggregationType: uiGridConstants.aggregationTypes.sum, cellClass: 'grid-align', footerCellFilter: 'number:2'},
        { name: 'ImsBnsVal', displayName: "IMS Bonus Value", width: 120, cellFilter: 'number:2', aggregationType: uiGridConstants.aggregationTypes.sum, cellClass: 'grid-align', footerCellFilter: 'number:2'},
        { name: 'NetIms', displayName: "Net IMS", width: 120, cellFilter: 'number:2', aggregationType: uiGridConstants.aggregationTypes.sum, cellClass: 'grid-align', footerCellFilter: 'number:2' },
        { name: 'LastYearAsOnDateImsQty', displayName: "Last Year As On Date IMS Qty", width: 120, cellFilter: 'number:2', cellClass: 'grid-align', footerCellFilter: 'number:2', aggregationType: uiGridConstants.aggregationTypes.sum },
        { name: 'LastYearAsOnDateImsVal', displayName: "Last Year As On Date IMS Val", width: 120, cellFilter: 'number:2', cellClass: 'grid-align', footerCellFilter: 'number:2', aggregationType: uiGridConstants.aggregationTypes.sum }
    ];
    $scope.gridDistSalesSummary = {
        showColumnFooter: true,
        enableFiltering: true,
        enableSorting: true,
        columnDefs: columnDistSalesSummary,
        //rowTemplate: rowTemplate(),
        enableGridMenu: true,
        enableSelectAll: true,
        exporterCsvFilename: 'Distributor_Wise_Sr_Sales.csv',
        exporterMenuPdf: false,
        exporterCsvLinkElement: angular.element(document.querySelectorAll(".custom-csv-link-location")),
        onRegisterApi: function (gridApi) {
            $scope.gridApi = gridApi;
        }
    };


    $scope.GetDistSalesSummary = function () {


        $scope.gridDistSalesSummary.data = [];
        if ($scope.frmDistSalesSummary.RepType.ReportTypeValue == "Today") {
            methodName = "GetDistProductImsToday";
        }
        if ($scope.frmDistSalesSummary.RepType.ReportTypeValue == "Yesterday") {
            methodName = "GetDistProductImsYesterday";
        }
        if ($scope.frmDistSalesSummary.RepType.ReportTypeValue == "LastSevendays") {
            methodName = "GetDistProductImsLastSevendays";
        }
        if ($scope.frmDistSalesSummary.RepType.ReportTypeValue == "LastThirtydays") {
            methodName = "GetDistProductImsLastThirtydays";
        }
        if ($scope.frmDistSalesSummary.RepType.ReportTypeValue == "CurrentMonth") {
            methodName = "GetDistProductImsCurrentMonth";
        }
        if ($scope.frmDistSalesSummary.RepType.ReportTypeValue == "LastMonth") {
            methodName = "GetDistProductImsLastMonth";
        }
        if ($scope.frmDistSalesSummary.RepType.ReportTypeValue == "CustomDate") {
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
                    methodName = "GetDistProductImsDateRange";
                }
            }
        }

        $http({
            method: "POST",
            url: MyApp.rootPath + "DistSalesSummary/" + methodName,
            data: {
                fDate: $scope.FromDate,
                tDate: $scope.ToDate,
                dCode: $scope.frmDistSalesSummary.Division.DivisionCode,
                rCode: $scope.frmDistSalesSummary.Region.RegionCode,
                aCode: $scope.frmDistSalesSummary.Area.AreaCode,
                tCode: $scope.frmDistSalesSummary.Territory.TerritoryCode,
                cCode: $scope.frmDistSalesSummary.Customer.CustomerCode
            }
        }).then(function (response) {
            if (response.data.length > 0) {
                $scope.gridDistSalesSummary.data = response.data;

            }
            else {
                toastr.warning("No Data Found!", '');
                $scope.gridDistSalesSummary.data = [];
            }
        }, function () {
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
            params: { dCode: $scope.frmDistSalesSummary.Division.DivisionCode }
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
            params: { dCode: $scope.frmDistSalesSummary.Division.DivisionCode, rCode: $scope.frmDistSalesSummary.Region.RegionCode }
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
            params: { dCode: $scope.frmDistSalesSummary.Division.DivisionCode, rCode: $scope.frmDistSalesSummary.Region.RegionCode, aCode: $scope.frmDistSalesSummary.Area.AreaCode }
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
            params: { dCode: $scope.frmDistSalesSummary.Division.DivisionCode, rCode: $scope.frmDistSalesSummary.Region.RegionCode, aCode: $scope.frmDistSalesSummary.Area.AreaCode, tCode: $scope.frmDistSalesSummary.Territory.TerritoryCode }
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
        $scope.isDisabled = true;
        $scope.FromDate = "";
        $scope.ToDate = "";
        $scope.gridDistSalesSummary.data = [];
        $scope.frmDistSalesSummary.RepType = "";
        $scope.frmDistSalesSummary.Division = undefined;
        $scope.frmDistSalesSummary.Region = undefined;
        $scope.frmDistSalesSummary.Area = undefined;
        $scope.frmDistSalesSummary.Territory = undefined;
        $scope.frmDistSalesSummary.Customer = undefined;

        $scope.Divisions = [];
        $scope.Regions = [];
        $scope.Areas = [];
        $scope.Territories = [];
        $scope.Customers = [];


    };

});