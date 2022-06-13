app.controller("RouteWiseImsCtrl", function ($scope, $http, uiGridConstants) {

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
                $scope.gridRouteWiseIms.enableGridMenu = response.data[0].DownLoadStatus;
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
        //{ name: 'SlNo', displayName: "Sl. No", type: 'number', width: 100, cellClass: 'grid-align', footerCellFilter: 'number:2'},
        { name: 'DivisionCode', displayName: "Division Code", width: 150, aggregationType: uiGridConstants.aggregationTypes.count, footerCellFilter: 'number:0'},
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
        { name: 'NoOfInv', displayName: "No Of Invoice", width: 120, cellFilter: 'number:2', cellClass: 'grid-align', footerCellFilter: 'number:2', aggregationType: uiGridConstants.aggregationTypes.sum},
        { name: 'TotalInvAmt', displayName: "Total Invoice Amount", width: 120, cellClass: 'grid-align', footerCellFilter: 'number:2', aggregationType: uiGridConstants.aggregationTypes.sum},
        { name: 'SlabAdjustment', displayName: "Slab Adjustment", width: 120, cellClass: 'grid-align', footerCellFilter: 'number:2', aggregationType: uiGridConstants.aggregationTypes.sum},
        { name: 'NetInvAmount', displayName: "Net Invoice Amount", width: 120, cellFilter: 'number:2', cellClass: 'grid-align', footerCellFilter: 'number:2', aggregationType: uiGridConstants.aggregationTypes.sum},
        { name: 'ReturnValue', displayName: "Return Value", width: 120, cellClass: 'grid-align', footerCellFilter: 'number:2', aggregationType: uiGridConstants.aggregationTypes.sum},
        { name: 'ReturnSlabAdjust', displayName: "Return Slab Adjustment", width: 120, cellClass: 'grid-align', footerCellFilter: 'number:2', aggregationType: uiGridConstants.aggregationTypes.sum},
        { name: 'NetReturnVal', displayName: "Net Return Value", width: 150, cellFilter: 'number:2', cellClass: 'grid-align', footerCellFilter: 'number:2', aggregationType: uiGridConstants.aggregationTypes.sum},
        { name: 'NetIms', displayName: "Net IMS", width: 120, cellFilter: 'number:2', cellClass: 'grid-align', footerCellFilter: 'number:2', aggregationType: uiGridConstants.aggregationTypes.sum},
        { name: 'NoOfReplaceInv', displayName: "No Of Replace Invoice", width: 120, cellClass: 'grid-align', footerCellFilter: 'number:2', aggregationType: uiGridConstants.aggregationTypes.sum},
        { name: 'ReplaceInvAmt', displayName: "Replace Invoice Amount", width: 120, cellClass: 'grid-align', footerCellFilter: 'number:2', aggregationType: uiGridConstants.aggregationTypes.sum}
        
    ];
    $scope.gridRouteWiseIms = {
        showColumnFooter: true,
        enableFiltering: true,
        enableSorting: true,
        columnDefs: columnRouteWiseIms,
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


    $scope.GetRouteWiseIms = function () {


        if ($scope.frmRouteWiseIms.RepType.ReportTypeValue == "Today") {

            methodName = "GeRouteWiseImsToday";

        }


        if ($scope.frmRouteWiseIms.RepType.ReportTypeValue == "Yesterday") {

            methodName = "GeRouteWiseImsYesterday";

        }
        if ($scope.frmRouteWiseIms.RepType.ReportTypeValue == "LastSevendays") {

            methodName = "GeRouteWiseImsLastSevendays";

        }
        if ($scope.frmRouteWiseIms.RepType.ReportTypeValue == "LastThirtydays") {

            methodName = "GeRouteWiseImsLastThirtydays";

        }       
        if ($scope.frmRouteWiseIms.RepType.ReportTypeValue == "CurrentMonth") {
            methodName = "GeRouteWiseImsCurrentMonth";           
        }
        if ($scope.frmRouteWiseIms.RepType.ReportTypeValue == "LastMonth") {
            methodName = "GeRouteWiseImsLastMonth";
        }
        if ($scope.frmRouteWiseIms.RepType.ReportTypeValue == "MonthOnMonthCy") {
            methodName = "GeRouteWiseImsMonthOnMonthCy";
        }


        if ($scope.frmRouteWiseIms.RepType.ReportTypeValue == "MonthOnMonthLy") {
            methodName = "GeRouteWiseImsMonthOnMonthLy";
        }

        if ($scope.frmRouteWiseIms.RepType.ReportTypeValue == "MonthOnMonthLpy") {
            methodName = "GeRouteWiseImsMonthOnMonthLpy";
        }

        if ($scope.frmRouteWiseIms.RepType.ReportTypeValue  == "CustomDate") {
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
                    methodName = "GeRouteWiseImsDateRange";
                }
            }  
        }

        $http({
            method: "POST",
            url: MyApp.rootPath + "RouteWiseIms/" + methodName,
            data: {
                fDate: $scope.FromDate,
                tDate: $scope.ToDate,
                dCode: $scope.frmRouteWiseIms.Division.DivisionCode,
                rCode: $scope.frmRouteWiseIms.Region.RegionCode,
                aCode: $scope.frmRouteWiseIms.Area.AreaCode,
                tCode: $scope.frmRouteWiseIms.Territory.TerritoryCode,
                cCode: $scope.frmRouteWiseIms.Customer.CustomerCode

            }
        }).then(function (response) {
            if (response.data.length > 0) {
                $scope.gridRouteWiseIms.data = response.data;

            }
            else {
                toastr.warning("No Data Found!", '');
                $scope.gridRouteWiseIms.data = [];
            }
        }, function (response) {
            //alert(response.data);
            toastr.error("Error!");
        });
    };


    $scope.OnReportTypeChange = function () {
        
        if ( $scope.frmRouteWiseIms.RepType.ReportTypeValue == "Today" || $scope.frmRouteWiseIms.RepType.ReportTypeValue == "Yesterday" || $scope.frmRouteWiseIms.RepType.ReportTypeValue == "LastSevendays" || $scope.frmRouteWiseIms.RepType.ReportTypeValue == "LastThirtydays" || $scope.frmRouteWiseIms.RepType.ReportTypeValue == "CurrentMonth" || $scope.frmRouteWiseIms.RepType.ReportTypeValue == "LastMonth" || $scope.frmRouteWiseIms.RepType.ReportTypeValue == "MonthOnMonthCy" || $scope.frmRouteWiseIms.RepType.ReportTypeValue == "MonthOnMonthLy") {
      

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
    $scope.GetDivisionList= function () {

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
            params: { dCode: $scope.frmRouteWiseIms.Division.DivisionCode }
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
            params: { dCode: $scope.frmRouteWiseIms.Division.DivisionCode, rCode: $scope.frmRouteWiseIms.Region.RegionCode }
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
            params: { dCode: $scope.frmRouteWiseIms.Division.DivisionCode, rCode: $scope.frmRouteWiseIms.Region.RegionCode, aCode: $scope.frmRouteWiseIms.Area.AreaCode }
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
            params: { dCode: $scope.frmRouteWiseIms.Division.DivisionCode, rCode: $scope.frmRouteWiseIms.Region.RegionCode, aCode: $scope.frmRouteWiseIms.Area.AreaCode, tCode: $scope.frmRouteWiseIms.Territory.TerritoryCode }
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
            params: { dCode: $scope.frmRouteWiseIms.Division.DivisionCode, rCode: $scope.frmRouteWiseIms.Region.RegionCode, aCode: $scope.frmRouteWiseIms.Area.AreaCode, tCode: $scope.frmRouteWiseIms.Territory.TerritoryCode, cCode: $scope.frmRouteWiseIms.Customer.CustomerCode }
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
            params: { SlNo: "(0,1,2,3,4,5,9)"}
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
        $scope.gridRouteWiseIms.data = [];
        $scope.isDisabled = true;
        $scope.ReportType = "";
        $scope.frmRouteWiseIms.RepType = undefined;
        $scope.frmRouteWiseIms.Division = undefined;
        $scope.frmRouteWiseIms.Region = undefined;
        $scope.frmRouteWiseIms.Area = undefined;
        $scope.frmRouteWiseIms.Territory = undefined;
        $scope.frmRouteWiseIms.Customer = undefined;

        
        $scope.Divisions = [];
        $scope.Regions = [];
        $scope.Areas = [];
        $scope.Territories = [];
        $scope.Customers = [];


    };

   
    

});