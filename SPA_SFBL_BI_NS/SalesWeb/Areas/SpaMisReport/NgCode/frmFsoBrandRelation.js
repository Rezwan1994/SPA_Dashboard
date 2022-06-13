app.controller("FsoBrandRelationCtrl", function ($scope, $http, uiGridConstants, uiGridExporterService) {
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
                $scope.gridFsoBrandRelation.enableGridMenu = response.data[0].DownLoadStatus;
            }
        }, function (response) {
            if (response.status === 404) {
                toastr.warning("Error Loading Report Download Status!", { timeOut: 2000 });
            }
        });
    };
    $scope.GetReportDownLoadStatus();

    var columnFsoBrandRelation = [
        { name: 'DivisionCode', displayName: "Division Code", width: 100, aggregationType: uiGridConstants.aggregationTypes.count, footerCellFilter: 'number:0' },
        { name: 'DivisionName', displayName: "Division Name", width: 150 },
        { name: 'RegionCode', displayName: "Region Code", width: 100 },
        { name: 'RegionName', displayName: "Region Name", width: 150 },
        { name: 'AreaCode', displayName: "Area Code", width: 100 },
        { name: 'AreaName', displayName: "Area Name", width: 150 },
        { name: 'TerritoryCode', displayName: "Territory Code", width: 100 },
        { name: 'TerritoryName', displayName: "Territory Name", width: 150 },
        { name: 'CustomerCode', displayName: "Distributor Code", width: 100 },
        { name: 'CustomerName', displayName: "Distributor Name", width: 150 },
        { name: 'DbLocation', displayName: "DB Location", width: 150 },
        { name: 'MarketCode', displayName: "Market Code", width: 100 },
        { name: 'MarketName', displayName: "Market Name", width: 150 },
        { name: 'FsoCode', displayName: "FSO Code", width: 100 },
        { name: 'FsoName', displayName: "FSO Name", width: 150 },
        { name: 'Status', displayName: "Status", width: 150 },
        { name: 'BrandCode', displayName: "Brand Code", width: 100 },
        { name: 'BrandName', displayName: "Brand Name", width: 150 },
    ];
    $scope.gridFsoBrandRelation = {
        showColumnFooter: true,
        enableFiltering: true,
        enableSorting: true,
        columnDefs: columnFsoBrandRelation,
        enableGridMenu: true,
        enableSelectAll: true,
        exporterMenuPdf: false,
        exporterCsvLinkElement: angular.element(document.querySelectorAll(".custom-csv-link-location")),
        exporterCsvFilename: 'FSO_Brand_Relation.csv',
        onRegisterApi: function (gridApi) {
            $scope.gridApi = gridApi;
        }
    };

    $scope.GetReportTypeList = function () {
        $http({
            method: "GET",
            url: MyApp.rootPath + "DistSrSales/GetReportTypeList",
            params: { SlNo: "(36)" }
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


        if ($scope.frmFsoBrandRelation.RepType.ReportTypeValue == "FSOBrandRelation" ) {
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

    $scope.GetFsoBrandRelation = function () {

        //if ($scope.frmFsoBrandRelation.RepType.ReportTypeValue == "CustomDate") {
        //    if ($scope.ToDate == "" || $scope.ToDate == undefined || $scope.ToDate == null) {
        //        toastr.warning("To Date  Cannot be empty !");
        //        return false;
        //    } else {
        //        var todayDate = new Date(new Date().getFullYear(), new Date().getMonth(), new Date().getDate());
        //        var endDate = $scope.ToDate.split("/");
        //        var convertedEndDate = new Date(+endDate[2], endDate[1] - 1, +endDate[0]);
        //        var eDate = new Date(convertedEndDate.getFullYear(), convertedEndDate.getMonth(), convertedEndDate.getDate());
        //        if (eDate >= todayDate) {
        //            toastr.warning("To Date  Less Than Current Date !");
        //            return false;
        //        } else {
        //            methodName = "GetFsoBrandRelationDateRange";

        //        }
        //    }
        //}

        $http({
            method: "POST",
            url: MyApp.rootPath + "FsoBrandRelation/GetFsoBrandRelation",// + methodName,
            data: {
                dCode: $scope.frmFsoBrandRelation.Division.DivisionCode,
                rCode: $scope.frmFsoBrandRelation.Region.RegionCode,
                aCode: $scope.frmFsoBrandRelation.Area.AreaCode,
                tCode: $scope.frmFsoBrandRelation.Territory.TerritoryCode,
                cCode: $scope.frmFsoBrandRelation.Customer.CustomerCode

            }
        }).then(function (response) {
            if (response.data.length > 0) {
                $scope.gridFsoBrandRelation.data = response.data;
            }
            else {
                toastr.warning("No Data Found!", '');
                $scope.gridFsoBrandRelation.data = [];
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
            params: { dCode: $scope.frmFsoBrandRelation.Division.DivisionCode }
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
            params: { dCode: $scope.frmFsoBrandRelation.Division.DivisionCode, rCode: $scope.frmFsoBrandRelation.Region.RegionCode }
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
            params: { dCode: $scope.frmFsoBrandRelation.Division.DivisionCode, rCode: $scope.frmFsoBrandRelation.Region.RegionCode, aCode: $scope.frmFsoBrandRelation.Area.AreaCode }
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
            params: { dCode: $scope.frmFsoBrandRelation.Division.DivisionCode, rCode: $scope.frmFsoBrandRelation.Region.RegionCode, aCode: $scope.frmFsoBrandRelation.Area.AreaCode, tCode: $scope.frmFsoBrandRelation.Territory.TerritoryCode }
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
    //$scope.OnCustomerClick = function () {

    //    //SR List
    //    $http({
    //        method: "GET",
    //        url: MyApp.rootPath + "DistSrSales/GetSrList",
    //        params: { dCode: $scope.frmFsoBrandRelation.Division.DivisionCode, rCode: $scope.frmFsoBrandRelation.Region.RegionCode, aCode: $scope.frmFsoBrandRelation.Area.AreaCode, tCode: $scope.frmFsoBrandRelation.Territory.TerritoryCode, cCode: $scope.frmFsoBrandRelation.Customer.CustomerCode }
    //    }).then(function (response) {
    //        if (response.data.Status === "" || response.data.Status === null) {
    //            if (response.data.Data.length > 0) {
    //                $scope.Srs = response.data.Data;
    //            } else {
    //                toastr.warning("No Data Found!", { timeOut: 2000 });
    //            }
    //        } else {
    //            toastr.warning(response.data.Status, { timeOut: 2000 });
    //        }
    //    }, function (response) {
    //        if (response.status === 404) {
    //            toastr.warning("Error Loading SR List!", { timeOut: 2000 });
    //        }
    //    });


    //}

    $scope.Reset = function () {
        $scope.FromDate = "";
        $scope.ToDate = "";
        $scope.gridFsoBrandRelation.data = [];
        $scope.isDisabled = false;
        $scope.ReportType = "";
        $scope.frmFsoBrandRelation.Division = undefined;
        $scope.frmFsoBrandRelation.Region = undefined;
        $scope.frmFsoBrandRelation.Area = undefined;
        $scope.frmFsoBrandRelation.Territory = undefined;
        $scope.frmFsoBrandRelation.Customer = undefined;

        $scope.frmFsoBrandRelation.RepType = undefined;


        $scope.Divisions = [];
        $scope.Regions = [];
        $scope.Areas = [];
        $scope.Territories = [];
        $scope.Customers = [];


    };

});