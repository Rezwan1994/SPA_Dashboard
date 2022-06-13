app.controller("RetailerImsCtrl", function ($scope, $http, uiGridConstants, uiGridExporterService) {
    $scope.isDisabled =true;
    var methodName = "";
    $scope.EventPerm(22);


    $scope.GetReportDownLoadStatus = function () {
        $http({
            method: "GET",
            url: MyApp.rootPath + "RetailerIms/GetReportDownLoadStatus",
            datatype: "json",
            params: { url: window.location.pathname}
        }).then(function (response) {
            if (response.data.length > 0) {
                $scope.gridRetailerIms.enableGridMenu = response.data[0].DownLoadStatus;
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

    var columnRetailerIms = [
            { name: 'DivisionCode', displayName: "Division Code", width: 150, aggregationType: uiGridConstants.aggregationTypes.count, footerCellFilter: 'number:0' },
            { name: 'DivisionName', displayName: "Division Name", width: 150 },
            { name: 'RegionCode', displayName: "Region Code", width: 150 },
            { name: 'RegionName', displayName: "Region Name", width: 150 },
            { name: 'AreaCode', displayName: "Area Code", width: 150 },
            { name: 'AreaName', displayName: "Area Name", width: 150 },
            { name: 'TerritoryCode', displayName: "Territory Code", width: 150 },
            { name: 'TerritoryName', displayName: "Territory Name", width: 150 },
            { name: 'CustomerCode', displayName: "Distributor Code", width: 150 },
            { name: 'CustomerName', displayName: "Distributor Name", width: 150 },
            { name: 'DBLoaction', displayName: "DB Loaction", width: 150 },
            { name: 'MarketCode', displayName: "Market Code", width: 150 },
            { name: 'MarketName', displayName: "Market Name", width: 150 },
            { name: 'RouteCode', displayName: "Route Code", width: 150 },
            { name: 'RouteName', displayName: "Route Name", width: 200 },
            { name: 'RetailerCode', displayName: "Retailer Code", width: 150 },
            { name: 'RetailerName', displayName: "Retailer Name", width: 200 },
            { name: 'NoOfInvoice', displayName: "No Of Invoice", width: 120, cellClass: 'grid-align', footerCellFilter: 'number:2', aggregationType: uiGridConstants.aggregationTypes.sum },
            { name: 'TotalInvoiceAmount', displayName: "Total Invoice Amount", width: 120, cellClass: 'grid-align', footerCellFilter: 'number:2', aggregationType: uiGridConstants.aggregationTypes.sum },
            { name: 'SlabAdjustment', displayName: "Slab Adjustment", width: 120, cellClass: 'grid-align', footerCellFilter: 'number:2', aggregationType: uiGridConstants.aggregationTypes.sum },
            { name: 'NetInvoiceAmount', displayName: "Net Invoice Amount", width: 120, cellFilter: 'number:2', cellClass: 'grid-align', footerCellFilter: 'number:2', aggregationType: uiGridConstants.aggregationTypes.sum },
            { name: 'ReturnValue', displayName: "Return Value", width: 120, cellClass: 'grid-align', footerCellFilter: 'number:2', aggregationType: uiGridConstants.aggregationTypes.sum },
            { name: 'ReturnSlabAdjustment', displayName: "Return Slab Adjustment", width: 120, cellClass: 'grid-align', footerCellFilter: 'number:2', aggregationType: uiGridConstants.aggregationTypes.sum },
            { name: 'NetReturnValue', displayName: "Net Return Value", width: 150, cellFilter: 'number:2', cellClass: 'grid-align', footerCellFilter: 'number:2', aggregationType: uiGridConstants.aggregationTypes.sum },
            { name: 'NetIms', displayName: "Net Ims", width: 120, cellFilter: 'number:2', cellClass: 'grid-align', footerCellFilter: 'number:2', aggregationType: uiGridConstants.aggregationTypes.sum },
            { name: 'NoOfReplaceInvoice', displayName: "No Of Replace Invoice", width: 120, cellClass: 'grid-align', footerCellFilter: 'number:2', aggregationType: uiGridConstants.aggregationTypes.sum },
            { name: 'ReplaceInvoiceAmount', displayName: "Replace Invoice Amount", width: 120, cellClass: 'grid-align', footerCellFilter: 'number:2', aggregationType: uiGridConstants.aggregationTypes.sum }
        ];
    $scope.gridRetailerIms = {
            showColumnFooter: true,
            enableFiltering: true,
            enableSorting: true,
            columnDefs: columnRetailerIms,
            enableGridMenu: true,
            enableSelectAll: true,
            exporterMenuPdf: false,
            exporterCsvLinkElement: angular.element(document.querySelectorAll(".custom-csv-link-location")),
            exporterCsvFilename: 'Retailer_Wise_Ims.csv',
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


        if ($scope.frmRetailerIms.RepType.ReportTypeValue == "Today" || $scope.frmRetailerIms.RepType.ReportTypeValue == "Yesterday" || $scope.frmRetailerIms.RepType.ReportTypeValue == "LastSevendays" || $scope.frmRetailerIms.RepType.ReportTypeValue == "LastThirtydays" ||$scope.frmRetailerIms.RepType.ReportTypeValue == "CurrentMonth" || $scope.frmRetailerIms.RepType.ReportTypeValue == "LastMonth") {
            $scope.isDisabled = true;
            $scope.FromDate = "";
            $scope.ToDate = "";

            
        }
        else {
            $scope.isDisabled = false;
            $scope.FromDate = "";
            $scope.ToDate = "";
        }
        $scope.GetDivisionList();
        
    };

    $scope.GetRetailerIms = function () {

        if ($scope.frmRetailerIms.RepType.ReportTypeValue == "Today") {

            methodName = "GetRetailerImsToDay";

        }

        if ($scope.frmRetailerIms.RepType.ReportTypeValue == "Yesterday") {

            methodName = "GeRetailerWiseImsYesterday";

        }
        if ($scope.frmRetailerIms.RepType.ReportTypeValue == "LastSevendays") {

            methodName = "GeRetailerWiseImsLastSevendays";

        }
        if ($scope.frmRetailerIms.RepType.ReportTypeValue == "LastThirtydays") {

            methodName = "GeRetailerWiseImsLastThirtydays";

        }       

        if ($scope.frmRetailerIms.RepType.ReportTypeValue == "CurrentMonth") {
            methodName = "GetRetailerImsUptoCurMonth";
        }
        if ($scope.frmRetailerIms.RepType.ReportTypeValue == "LastMonth") {
            methodName = "GetRetailerImsUptoPrevMonth";
        }

        if ($scope.frmRetailerIms.RepType.ReportTypeValue == "CustomDate") {
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
                    methodName = "GetRetailerImsAnyDate";

                }
            }
        }

        $http({
            method: "POST",
            url: MyApp.rootPath + "RetailerIms/" + methodName,
            data: {
                fDate: $scope.FromDate,
                tDate: $scope.ToDate,
                dCode: $scope.frmRetailerIms.Division.DivisionCode,
                rCode: $scope.frmRetailerIms.Region.RegionCode,
                aCode: $scope.frmRetailerIms.Area.AreaCode,
                tCode: $scope.frmRetailerIms.Territory.TerritoryCode,
                cCode: $scope.frmRetailerIms.Customer.CustomerCode

            }
        }).then(function (response) {
            if (response.data.length > 0) {
                //$scope.GetReportDownLoadStatus(); 
                //$scope.GenerateGrid();
                $scope.gridRetailerIms.data = response.data;

            }
            else {
                toastr.warning("No Data Found!", '');
                $scope.gridRetailerIms.data = [];
            }
        }, function (response) {
            toastr.error("Error!");
        });
    };

    $scope.GetDivisionList= function () {

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
            params: { dCode: $scope.frmRetailerIms.Division.DivisionCode }
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
            params: { dCode: $scope.frmRetailerIms.Division.DivisionCode, rCode: $scope.frmRetailerIms.Region.RegionCode }
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
            params: { dCode: $scope.frmRetailerIms.Division.DivisionCode, rCode: $scope.frmRetailerIms.Region.RegionCode, aCode: $scope.frmRetailerIms.Area.AreaCode }
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
            params: { dCode: $scope.frmRetailerIms.Division.DivisionCode, rCode: $scope.frmRetailerIms.Region.RegionCode, aCode: $scope.frmRetailerIms.Area.AreaCode, tCode: $scope.frmRetailerIms.Territory.TerritoryCode }
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
            params: { dCode: $scope.frmRetailerIms.Division.DivisionCode, rCode: $scope.frmRetailerIms.Region.RegionCode, aCode: $scope.frmRetailerIms.Area.AreaCode, tCode: $scope.frmRetailerIms.Territory.TerritoryCode, cCode: $scope.frmRetailerIms.Customer.CustomerCode }
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
        $scope.gridRetailerIms.data = [];
        $scope.isDisabled = false;
        $scope.ReportType = "";
        $scope.frmRetailerIms.Division = undefined;
        $scope.frmRetailerIms.Region = undefined;
        $scope.frmRetailerIms.Area = undefined;
        $scope.frmRetailerIms.Territory = undefined;
        $scope.frmRetailerIms.Customer = undefined;

        $scope.frmRetailerIms.RepType = undefined;


        $scope.Divisions = [];
        $scope.Regions = [];
        $scope.Areas = [];
        $scope.Territories = [];
        $scope.Customers = [];


    };

});