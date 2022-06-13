app.controller("RouteBrandWiseImsCtrl", function ($scope, $http, uiGridConstants) {

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
                $scope.gridRouteBrandWiseIms.enableGridMenu = response.data[0].DownLoadStatus;
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

    var columnRouteWiseIms = [
        //{ name: 'SlNo', displayName: "Sl. No", type: 'number', width: 100, cellClass: 'grid-align', footerCellFilter: 'number:2' },
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
        { name: 'DbLocation', displayName: "DB Loaction", width: 150 },
        { name: 'RouteCode', displayName: "Route Code", width: 150 },
        { name: 'RouteName', displayName: "Route Name", width: 200 },
        { name: 'BrandCode', displayName: "Brand Code", width: 150 },
        { name: 'BrandName', displayName: "Brand Name", width: 150 },
        { name: 'InvoiceAmount', displayName: "Invoice Amount", width: 120, cellClass: 'grid-align', footerCellFilter: 'number:2', aggregationType: uiGridConstants.aggregationTypes.sum },
        { name: 'ReplaceInvAmt', displayName: "Replace Invoice Amount", width: 120, cellClass: 'grid-align', footerCellFilter: 'number:2', aggregationType: uiGridConstants.aggregationTypes.sum },
        { name: 'ReturnValue', displayName: "Return Value", width: 120, cellClass: 'grid-align', footerCellFilter: 'number:2', aggregationType: uiGridConstants.aggregationTypes.sum },
        { name: 'NetIms', displayName: "Net IMS", width: 120, cellFilter: 'number:2', cellClass: 'grid-align', footerCellFilter: 'number:2', aggregationType: uiGridConstants.aggregationTypes.sum }


    ];
    $scope.gridRouteBrandWiseIms = {
        showColumnFooter: true,
        enableFiltering: true,
        enableSorting: true,
        columnDefs: columnRouteWiseIms,
        enableGridMenu: true,
        enableSelectAll: true,
        exporterCsvFilename: "Route_Brand_Wise_IMS_" + xl_file_name + ".csv",
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
            params: { SlNo: "(0,1,2,3,4,5,9)" }
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
        if ($scope.frmRouteBrandWiseIms.RepType.ReportTypeValue == "Today" ||$scope.frmRouteBrandWiseIms.RepType.ReportTypeValue == "Yesterday" || $scope.frmRouteBrandWiseIms.RepType.ReportTypeValue == "LastSevendays" || $scope.frmRouteBrandWiseIms.RepType.ReportTypeValue == "LastThirtydays" || $scope.frmRouteBrandWiseIms.RepType.ReportTypeValue == "CurrentMonth" || $scope.frmRouteBrandWiseIms.RepType.ReportTypeValue == "LastMonth" || $scope.frmRouteBrandWiseIms.RepType.ReportTypeValue == "MonthOnMonthCy" || $scope.frmRouteBrandWiseIms.RepType.ReportTypeValue == "MonthOnMonthLy") {
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
        //Region List
        $http({
            method: "GET",
            url: MyApp.rootPath + "DistSrSales/GetRegionList",
            params: { dCode: $scope.frmRouteBrandWiseIms.Division.DivisionCode }
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
            params: { dCode: $scope.frmRouteBrandWiseIms.Division.DivisionCode, rCode: $scope.frmRouteBrandWiseIms.Region.RegionCode }
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
            params: { dCode: $scope.frmRouteBrandWiseIms.Division.DivisionCode, rCode: $scope.frmRouteBrandWiseIms.Region.RegionCode, aCode: $scope.frmRouteBrandWiseIms.Area.AreaCode }
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
            params: { dCode: $scope.frmRouteBrandWiseIms.Division.DivisionCode, rCode: $scope.frmRouteBrandWiseIms.Region.RegionCode, aCode: $scope.frmRouteBrandWiseIms.Area.AreaCode, tCode: $scope.frmRouteBrandWiseIms.Territory.TerritoryCode }
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
            params: { dCode: $scope.frmRouteBrandWiseIms.Division.DivisionCode, rCode: $scope.frmRouteBrandWiseIms.Region.RegionCode, aCode: $scope.frmRouteBrandWiseIms.Area.AreaCode, tCode: $scope.frmRouteBrandWiseIms.Territory.TerritoryCode, cCode: $scope.frmRouteBrandWiseIms.Customer.CustomerCode }
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

    $scope.GetRouteBrandWiseIms = function () {



        if ($scope.frmRouteBrandWiseIms.RepType.ReportTypeValue == "Today") {

            methodName = "GetRouteBrandImsToday";

        }
        if ($scope.frmRouteBrandWiseIms.RepType.ReportTypeValue == "Yesterday") {

            methodName = "GetRouteBrandImsYesterday";

        }
        if ($scope.frmRouteBrandWiseIms.RepType.ReportTypeValue == "LastSevendays") {

            methodName = "GetRouteBrandImsLastSevendays";

        }
        if ($scope.frmRouteBrandWiseIms.RepType.ReportTypeValue == "LastThirtydays") {

            methodName = "GetRouteBrandImsLastThirtydays";

        }
        if ($scope.frmRouteBrandWiseIms.RepType.ReportTypeValue == "CurrentMonth") {
            methodName = "GetRouteBrandImsCurrentMonth";

        }
        if ($scope.frmRouteBrandWiseIms.RepType.ReportTypeValue == "LastMonth") {
            methodName = "GetRouteBrandImsLastMonth";

        }
        if ($scope.frmRouteBrandWiseIms.RepType.ReportTypeValue == "MonthOnMonthCy") {
            methodName = "GetRouteBrandImsMonthOnMonthCy";

        }


        if ($scope.frmRouteBrandWiseIms.RepType.ReportTypeValue == "MonthOnMonthLy") {
            methodName = "GetRouteBrandImsMonthOnMonthLy";


        }

        if ($scope.frmRouteBrandWiseIms.RepType.ReportTypeValue == "MonthOnMonthLpy") {
            methodName = "GetRouteBrandImsMonthOnMonthLpy";

        }

        if ($scope.frmRouteBrandWiseIms.RepType.ReportTypeValue == "CustomDate") {
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
                    methodName = "GetRouteBrandImsDateRange";

                }
            }
        }

        $http({
            method: "POST",
            url: MyApp.rootPath + "RouteBrandWiseIms/" + methodName,
            data: {
                fDate: $scope.FromDate,
                tDate: $scope.ToDate,
                dCode: $scope.frmRouteBrandWiseIms.Division.DivisionCode,
                rCode: $scope.frmRouteBrandWiseIms.Region.RegionCode,
                aCode: $scope.frmRouteBrandWiseIms.Area.AreaCode,
                tCode: $scope.frmRouteBrandWiseIms.Territory.TerritoryCode,
                cCode: $scope.frmRouteBrandWiseIms.Customer.CustomerCode

            }
        }).then(function (response) {
            if (response.data.length > 0) {
                $scope.gridRouteBrandWiseIms.data = response.data;

            }
            else {
                toastr.warning("No Data Found!", '');
                $scope.gridRouteWiseIms.data = [];
            }
        }, function (response) {
            //alert(response.data);
            toastr.error("Error!----------------");
        });
    };



    $scope.Reset = function () {
        $scope.FromDate = "";
        $scope.ToDate = "";
        $scope.gridRouteBrandWiseIms.data = [];
        $scope.isDisabled = false;
        $scope.ReportType = "";
        $scope.frmRouteBrandWiseIms.RepType = undefined;
        $scope.frmRouteBrandWiseIms.Division = undefined;
        $scope.frmRouteBrandWiseIms.Region = undefined;
        $scope.frmRouteBrandWiseIms.Area = undefined;
        $scope.frmRouteBrandWiseIms.Territory = undefined;
        $scope.frmRouteBrandWiseIms.Customer = undefined;
        $scope.Divisions = [];
        $scope.Regions = [];
        $scope.Areas = [];
        $scope.Territories = [];
        $scope.Customers = [];
    };

   
    

});