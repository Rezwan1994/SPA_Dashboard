app.controller("FsoTimeKeepingCtrl", function ($scope, $http, uiGridConstants) {

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
                $scope.gridFsoTimeKeeping.enableGridMenu = response.data[0].DownLoadStatus;
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
    var columnFsoTimeKeeping = [
        { name: 'DivisionCode', displayName: "Division Code", width: 150, cellClass: 'grid-align', aggregationType: uiGridConstants.aggregationTypes.count, footerCellFilter: 'number:0' },
        { name: 'DivisionName', displayName: "Division Name", width: 150 },
        { name: 'RegionCode', displayName: "Region Code", width: 150, cellClass: 'grid-align'},
        { name: 'RegionName', displayName: "Region Name", width: 150 },
        { name: 'AreaCode', displayName: "Area Code", width: 150, cellClass: 'grid-align' },
        { name: 'AreaName', displayName: "Area Name", width: 150 },
        { name: 'TerritoryCode', displayName: "Territory Code", width: 150, cellClass: 'grid-align'},
        { name: 'TerritoryName', displayName: "Territory Name", width: 150 },        
        { name: 'CustomerCode', displayName: "Distributor Code", width: 150, cellClass: 'grid-align' },
        { name: 'CustomerName', displayName: "Distributor Name", width: 150 },
        { name: 'DbLocation', displayName: "DB Loaction", width: 150 },
        { name: 'MarketCode', displayName: "Market Code", width: 150, cellClass: 'grid-align' },
        { name: 'MarketName', displayName: "Market Name", width: 150 },
        { name: 'EmployeeCode', displayName: "SR Code", width: 150, cellClass: 'grid-align' },
        { name: 'EmployeeName', displayName: "SR Name", width: 150 },
        { name: 'RouteCode', displayName: "Route Code", width: 150, cellClass: 'grid-align'},
        { name: 'RouteName', displayName: "Route Name", width: 200 },
        { name: 'RetailerCode', displayName: "Retailer Code", width: 150, cellClass: 'grid-align' },
        { name: 'RetailerName', displayName: "Retailer Name", width: 200 },
        { name: 'OrderDate', displayName: "Order Date ", width: 150 },
        { name: 'DeliveryDate', displayName: "Delivery Date", width: 200 },
        { name: 'OrderTime', displayName: "Order Time", width: 200 },
        { name: 'OrderNo', displayName: "Order No", width: 200, cellClass: 'grid-align'},
        { name: 'OrderType', displayName: "Order Type", width: 200 },
        { name: 'InvoiceStatus', displayName: "Invoice Status", width: 200 },
        { name: 'NumberOfProduct', displayName: "Number Of Product", width: 200, cellClass: 'grid-align'},
        { name: 'OrderValue', displayName: "Order Value", width: 200, cellFilter: 'number:2', aggregationType: uiGridConstants.aggregationTypes.sum, cellClass: 'grid-align', footerCellFilter: 'number:2'},
        { name: 'TotalRouteRetailer', displayName: "Total Route Retailer", width: 200, cellClass: 'grid-align' },
        { name: 'FirstOrder', displayName: "First Order", width: 200, cellClass: 'grid-align' },
        { name: 'FirstOrderTime', displayName: "First Order Time", width: 200 },
        { name: 'LastOrder', displayName: "Last Order", width: 200, cellClass: 'grid-align'},
        { name: 'LastOrderTime', displayName: "Last Order Time", width: 200 }
    ];
    $scope.gridFsoTimeKeeping = {
        showColumnFooter: true,
        enableFiltering: true,
        enableSorting: true,
        columnDefs: columnFsoTimeKeeping,
        //rowTemplate: rowTemplate(),
        enableGridMenu: true,
        enableSelectAll: true,
        exporterCsvFilename: 'FSO_Wise_Order_Time_Keeping.csv',
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
                toastr.warning("Error Loading Report Type List!", { timeOut: 2000 });
            }
        });

    }
    $scope.GetReportTypeList();
    $scope.OnReportTypeChange = function () {

        if ($scope.frmFsoTimeKeeping.RepType.ReportTypeValue == "Today" || $scope.frmFsoTimeKeeping.RepType.ReportTypeValue == "Yesterday" || $scope.frmFsoTimeKeeping.RepType.ReportTypeValue == "LastSevendays" || $scope.frmFsoTimeKeeping.RepType.ReportTypeValue == "LastThirtydays" || $scope.frmFsoTimeKeeping.RepType.ReportTypeValue == "CurrentMonth" || $scope.frmFsoTimeKeeping.RepType.ReportTypeValue == "LastMonth" || $scope.frmFsoTimeKeeping.RepType.ReportTypeValue == "MonthOnMonthCy" || $scope.frmFsoTimeKeeping.RepType.ReportTypeValue == "MonthOnMonthLy") {
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
    $scope.GetDistSrSaels = function () {

        $scope.gridFsoTimeKeeping.data = [];
 
        if ($scope.frmFsoTimeKeeping.RepType.ReportTypeValue == "CustomDate") {
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
                    methodName = "GetFsoTimeKeepingDateRange";
                }
            }
        }
        $http({
            method: "POST",
            url: MyApp.rootPath + "FsoTimeKeeping/" + methodName,
            data: {
                fDate: $scope.FromDate,
                tDate: $scope.ToDate,
                dCode: $scope.frmFsoTimeKeeping.Division.DivisionCode,
                rCode: $scope.frmFsoTimeKeeping.Region.RegionCode,
                aCode: $scope.frmFsoTimeKeeping.Area.AreaCode,
                tCode: $scope.frmFsoTimeKeeping.Territory.TerritoryCode,
                cCode: $scope.frmFsoTimeKeeping.Customer.CustomerCode,
                sCode: $scope.frmFsoTimeKeeping.Sr.SrCode
            }
        }).then(function (response) {
            if (response.data.length > 0) {
                $scope.gridFsoTimeKeeping.data = response.data;
            }
            else {
                toastr.warning("No Data Found!", '');
                $scope.gridFsoTimeKeeping.data = [];
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
            params: { dCode: $scope.frmFsoTimeKeeping.Division.DivisionCode }
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
            params: { dCode: $scope.frmFsoTimeKeeping.Division.DivisionCode, rCode: $scope.frmFsoTimeKeeping.Region.RegionCode }
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
            params: { dCode: $scope.frmFsoTimeKeeping.Division.DivisionCode, rCode: $scope.frmFsoTimeKeeping.Region.RegionCode, aCode: $scope.frmFsoTimeKeeping.Area.AreaCode }
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
            params: { dCode: $scope.frmFsoTimeKeeping.Division.DivisionCode, rCode: $scope.frmFsoTimeKeeping.Region.RegionCode, aCode: $scope.frmFsoTimeKeeping.Area.AreaCode, tCode: $scope.frmFsoTimeKeeping.Territory.TerritoryCode }
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

        //SR List
        $http({
            method: "GET",
            url: MyApp.rootPath + "DistSrSales/GetSrList",
            params: { dCode: $scope.frmFsoTimeKeeping.Division.DivisionCode, rCode: $scope.frmFsoTimeKeeping.Region.RegionCode, aCode: $scope.frmFsoTimeKeeping.Area.AreaCode, tCode: $scope.frmFsoTimeKeeping.Territory.TerritoryCode, cCode: $scope.frmFsoTimeKeeping.Customer.CustomerCode }
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
    $scope.Reset = function () {
        $scope.FromDate = "";
        $scope.ToDate = "";
        $scope.gridFsoTimeKeeping.data = [];
        $scope.isDisabled = true;

        $scope.frmFsoTimeKeeping.RepType = undefined;

        $scope.frmFsoTimeKeeping.Division = undefined;
        $scope.frmFsoTimeKeeping.Region = undefined;
        $scope.frmFsoTimeKeeping.Area = undefined;
        $scope.frmFsoTimeKeeping.Territory = undefined;
        $scope.frmFsoTimeKeeping.Customer = undefined;
        $scope.frmFsoTimeKeeping.Sr = undefined;

        $scope.Divisions = [];
        $scope.Regions = [];
        $scope.Areas = [];
        $scope.Territories = [];
        $scope.Customers = [];
        $scope.Srs = [];

    };

});