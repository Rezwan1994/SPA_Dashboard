app.controller("RetailerDetailsCtrl", function ($scope, $http, uiGridConstants, uiGridExporterService) {
    $scope.isDisabledFromDate = true;
    $scope.isDisabledToDate = true;
    var methodName = "";

    $scope.FormDate = "From Date";

    $scope.EventPerm(22);
    $scope.Status = 'ALL';
    $scope.RetailerType = 'ALL';
    $scope.RetailerLocationType = 'ALL';


    $scope.GetReportDownLoadStatus = function () {
        $http({
            method: "GET",
            url: MyApp.rootPath + "RetailerIms/GetReportDownLoadStatus",
            datatype: "json",
            params: { url: window.location.pathname }
        }).then(function (response) {
            if (response.data.length > 0) {
                $scope.gridRetailerDetails.enableGridMenu = response.data[0].DownLoadStatus;
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
            params: { SlNo: "(11,12)" }
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


        if ($scope.frmRetailerDetails.RepType.ReportTypeValue == "RetailerDetails") {
            $scope.isDisabledFromDate = true;
            $scope.isDisabledToDate = true;
            $scope.FormDate = "From Date";
            $scope.FromDate = "";
            $scope.ToDate = "";
        }
        else {
            $scope.isDisabledFromDate = true;
            $scope.isDisabledToDate = true;
            $scope.FormDate = "Last Invoice Date";
            $scope.FromDate = "";
            $scope.ToDate = "";
        }
        $scope.GetDivisionList();
    };

    var columnRetailerDetails = [
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
        { name: 'MarketCode', displayName: "Market Code", width: 150 },
        { name: 'MarketName', displayName: "Market Name", width: 150 },
        { name: 'RouteCode', displayName: "Route Code", width: 150 },
        { name: 'RouteName', displayName: "Route Name", width: 200 },
        { name: 'MarketRouteRelStatus', displayName: "Market Route Relation Status", width: 200 },
        { name: 'RetailerCode', displayName: "Retailer Code", width: 150 },
        { name: 'RetailerName', displayName: "Retailer Name", width: 200 },
        { name: 'RetailerNameBn', displayName: "Retailer Name Bangla", width: 200 },
        { name: 'RouteRetailerRelStatus', displayName: "Route Retailer Relation Status", width: 200 },
        { name: 'RetailerStatus', displayName: "Retailer Status", width: 200 },
        { name: 'Address', displayName: "Address", width: 300 },
        { name: 'RetailerAddressBn', displayName: "Address Bangla", width: 300 },
        { name: 'ContactPerson', displayName: "Contact Person", width: 100 },
        { name: 'ContactNo', displayName: "Contact No", width: 100 },
        { name: 'RetailerType', displayName: "Retailer Type", width: 100 },
        //{ name: 'RetailerCategoryCode', displayName: "Retaile rCategory Code", width: 100 },
        { name: 'RetailerCategoryDesc', displayName: "Retaile rCategory Desc", width: 100 },
        { name: 'LocationType', displayName: "Location Type", width: 100 },
        { name: 'RetailerEntryDate', displayName: "Retailer Entry Date", width: 100 },
        //{ name: 'RecommendStatusDesc', displayName: "Recommend Status Desc", width: 100 },
        //{ name: 'RecommendBy', displayName: "Recommend By", width: 100 },
        //{ name: 'RecommendDate', displayName: "Recommend Date", width: 100 },
        { name: 'ApprovedStatusDesc', displayName: "Approved Status Desc", width: 100 },
        //{ name: 'ApprovedBy', displayName: "Approved By", width: 100 },
        { name: 'ApprovedDate', displayName: "Approved Date", width: 100 },
        { name: 'MonthleAvgSales', displayName: "Monthle Avg Sales", width: 100 },
        { name: 'FirstInvoiceDate', displayName: "Firs tInvoice Date", width: 130 },
        { name: 'LastInvoiceDate', displayName: "Last Invoice Date", width: 130 },
        //{ name: 'LastInvoiceDay', displayName: "Last Invoice(Days)", width: 60, cellClass: 'grid-align' }
    ];
    $scope.gridRetailerDetails = {
        showGridFooter: true,
        //showColumnFooter: true,
        enableFiltering: true,
        enableSorting: true,
        //flatEntityAccess: true,
        //fastWatch: true,
        columnDefs: columnRetailerDetails,
        enableGridMenu: true,
        enableSelectAll: true,
        exporterCsvFilename: 'Retailer_Details_information.csv',
        exporterMenuPdf: false,
        exporterCsvLinkElement: angular.element(document.querySelectorAll(".custom-csv-link-location")),
        onRegisterApi: function (gridApi) {
            $scope.gridApi = gridApi;
        }

    };


    $scope.GetRetailerDetails = function () {


        if ($scope.frmRetailerDetails.RepType.ReportTypeValue == "RetailerDetails") {

            methodName = "GetRetailerDetails";

        }


        if ($scope.frmRetailerDetails.RepType.ReportTypeValue == "DeadRetailer") {
            //if ($scope.FromDate == "" || $scope.FromDate == undefined || $scope.FromDate == null) {
            //    toastr.warning("Last Invoice Date  Cannot be empty !");
            //    return false;
            //}
            methodName = "GetDeadRetailer";

        }


        if ($scope.frmRetailerDetails.RepType.ReportTypeValue == "CustomDate") {
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
                    methodName = "GetRetailerDetailsDateRange";

                }
            }
        }

        $http({
            method: "POST",
            url: MyApp.rootPath + "RetailerDetails/" + methodName,
            data: {
                fDate: $scope.FromDate,
                tDate: $scope.ToDate,
                dCode: $scope.frmRetailerDetails.Division.DivisionCode,
                rCode: $scope.frmRetailerDetails.Region.RegionCode,
                aCode: $scope.frmRetailerDetails.Area.AreaCode,
                tCode: $scope.frmRetailerDetails.Territory.TerritoryCode,
                cCode: $scope.frmRetailerDetails.Customer.CustomerCode,
                mCode: $scope.frmRetailerDetails.Market.MarketCode,
                rCatCode: $scope.frmRetailerDetails.Category.RetailerCategoryCode,
                rType: $scope.RetailerType,
                rlocType: $scope.RetailerLocationType,
                status: $scope.Status

            }
        }).then(function (response) {
            debugger;
            if (response.data.length > 0) {

                $scope.gridRetailerDetails.data = response.data;
            }

            else {
                toastr.warning("No Data Found!", '');
                $scope.gridRetailerDetails.data = [];
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
            params: { dCode: $scope.frmRetailerDetails.Division.DivisionCode }
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
            params: { dCode: $scope.frmRetailerDetails.Division.DivisionCode, rCode: $scope.frmRetailerDetails.Region.RegionCode }
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
            params: { dCode: $scope.frmRetailerDetails.Division.DivisionCode, rCode: $scope.frmRetailerDetails.Region.RegionCode, aCode: $scope.frmRetailerDetails.Area.AreaCode }
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
            params: { dCode: $scope.frmRetailerDetails.Division.DivisionCode, rCode: $scope.frmRetailerDetails.Region.RegionCode, aCode: $scope.frmRetailerDetails.Area.AreaCode, tCode: $scope.frmRetailerDetails.Territory.TerritoryCode }
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
        //Market_List
        $http({
            method: "GET",
            url: MyApp.rootPath + "RetailerDetails/GetMarketList",
            params: { dCode: $scope.frmRetailerDetails.Division.DivisionCode, rCode: $scope.frmRetailerDetails.Region.RegionCode, aCode: $scope.frmRetailerDetails.Area.AreaCode, tCode: $scope.frmRetailerDetails.Territory.TerritoryCode, cCode: $scope.frmRetailerDetails.Customer.CustomerCode}
        }).then(function (response) {
            if (response.data.Status === "" || response.data.Status === null) {
                if (response.data.Data.length > 0) {
                    $scope.Markets = response.data.Data;
                } else {
                    toastr.warning("No Data Found!", { timeOut: 2000 });
                }
            } else {
                toastr.warning(response.data.Status, { timeOut: 2000 });
            }
        }, function (response) {
            if (response.status === 404) {
                toastr.warning("Error Loading Market List!", { timeOut: 2000 });
            }
        });
    }

    $scope.GetRetailerCategoryList = function () {
        $http({
            method: "GET",
            url: MyApp.rootPath + "RetailerDetails/GetRetailerCategoryList"            
        }).then(function (response) {
            if (response.data.Status === "" || response.data.Status === null) {
                if (response.data.Data.length > 0) {
                    $scope.Categories = response.data.Data;
                } else {
                    toastr.warning("No Data Found!", { timeOut: 2000 });
                }
            } else {
                toastr.warning(response.data.Status, { timeOut: 2000 });
            }
        }, function (response) {
            if (response.status === 404) {
                toastr.warning("Error Loading Category List!", { timeOut: 2000 });
            }
        });
    }
    $scope.GetRetailerCategoryList();

    $scope.Reset = function () {
        $scope.FromDate = "";
        $scope.ToDate = "";       
        $scope.isDisabledFromDate = true;
        $scope.isDisabledToDate = true;
        $scope.ReportType = "";

        $scope.frmRetailerDetails.RepType = undefined;
        $scope.frmRetailerDetails.Division = undefined;
        $scope.frmRetailerDetails.Region = undefined;
        $scope.frmRetailerDetails.Area = undefined;
        $scope.frmRetailerDetails.Territory = undefined;
        $scope.frmRetailerDetails.Customer = undefined;
        $scope.frmRetailerDetails.Market = undefined;
        $scope.frmRetailerDetails.Category = undefined;
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
        $scope.gridRetailerDetails.data = [];
        
        
    };

});