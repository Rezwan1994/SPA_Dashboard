app.controller("DisplayProgramParticipationCtrl", function ($scope, $http, uiGridConstants) {

    var xl_file_name = "";
    var methodName = "";
    $scope.isDisabled = true;
    $scope.BonusType = "";


    $scope.EventPerm(22);


    $scope.GetReportDownLoadStatus = function () {
        $http({
            method: "GET",
            url: MyApp.rootPath + "RetailerIms/GetReportDownLoadStatus",
            datatype: "json",
            params: { url: window.location.pathname }
        }).then(function (response) {
            if (response.data.length > 0) {
                $scope.gridDisplayProgramParticipation.enableGridMenu = response.data[0].DownLoadStatus;
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

    $scope.GetDisplayProgramParticipation = function () {

        //if ($scope.frmDisplayProgramParticipation.RepType.ReportTypeValue == "DispalyProgram") {
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
        //            methodName = "GetDisplayProgramParticipationCustomDate";

        //        }
        //    }
        //}
        methodName = "GetDisplayProgramParticipation";
        $http({
            method: "POST",
            url: MyApp.rootPath + "DisplayProgramParticipation/" + methodName,
            data: {
                dCode: $scope.frmDisplayProgramParticipation.Division.DivisionCode,
                rCode: $scope.frmDisplayProgramParticipation.Region.RegionCode,
                aCode: $scope.frmDisplayProgramParticipation.Area.AreaCode,
                tCode: $scope.frmDisplayProgramParticipation.Territory.TerritoryCode,
                cCode: $scope.frmDisplayProgramParticipation.Customer.CustomerCode,
                dProgramNo: $scope.frmDisplayProgramParticipation.DisplayProgram.DisplayProgramNo
            }
        }).then(function (response) {
            if (response.data.length > 0) {
                $scope.gridDisplayProgramParticipation.data = response.data;
            }
            else {
                toastr.warning("No Data Found!", '');
                $scope.gridDisplayProgramParticipation.data = [];
            }
        }, function (response) {
            //alert(response);
            toastr.error("Error!");
        });
    };


    //$scope.OnReportTypeChange = function () {
    //    if ($scope.frmDisplayProgramParticipation.RepType.ReportTypeValue == "DisplayProgram" ) {
    //        $scope.FromDate = "";
    //        $scope.ToDate = "";
    //        $scope.isDisabled = false;
    //    }
    //    $scope.GetDivisionList();
    //};


   

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
    $scope.GetDivisionList();

    $scope.OnDivisionClick = function () {
        //Region List
        $http({
            method: "GET",
            url: MyApp.rootPath + "DistSrSales/GetRegionList",
            params: { dCode: $scope.frmDisplayProgramParticipation.Division.DivisionCode }
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
            params: { dCode: $scope.frmDisplayProgramParticipation.Division.DivisionCode, rCode: $scope.frmDisplayProgramParticipation.Region.RegionCode }
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
            params: { dCode: $scope.frmDisplayProgramParticipation.Division.DivisionCode, rCode: $scope.frmDisplayProgramParticipation.Region.RegionCode, aCode: $scope.frmDisplayProgramParticipation.Area.AreaCode }
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
            params: { dCode: $scope.frmDisplayProgramParticipation.Division.DivisionCode, rCode: $scope.frmDisplayProgramParticipation.Region.RegionCode, aCode: $scope.frmDisplayProgramParticipation.Area.AreaCode, tCode: $scope.frmDisplayProgramParticipation.Territory.TerritoryCode }
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
        //Display Program  List
        $http({
            method: "GET",
            url: MyApp.rootPath + "DistSrSales/GetDisplayProgramList",
            params: { dCode: $scope.frmDisplayProgramParticipation.Division.DivisionCode, rCode: $scope.frmDisplayProgramParticipation.Region.RegionCode, aCode: $scope.frmDisplayProgramParticipation.Area.AreaCode, tCode: $scope.frmDisplayProgramParticipation.Territory.TerritoryCode, cCode: $scope.frmDisplayProgramParticipation.Customer.CustomerCode }
        }).then(function (response) {
            if (response.data.Status === "" || response.data.Status === null) {
                if (response.data.Data.length > 0) {
                    $scope.DisplayPrograms = response.data.Data;
                } else {
                    toastr.warning("No Data Found!", { timeOut: 2000 });
                }
            } else {
                toastr.warning(response.data.Status, { timeOut: 2000 });
            }
        }, function (response) {
            if (response.status === 404) {
                toastr.warning("Error Loading Display Program  List!", { timeOut: 2000 });
            }
        });
    }


    //Grid
    var columnDisplayProgramParticipation = [
        //{ name: 'SlNo', displayName: "Sl. No", type: 'number', width: 100, cellClass: 'grid-align', footerCellFilter: 'number:2' },
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
        { name: 'DbLocation', displayName: "DB Location", width: 150 },
        { name: 'RouteCode', displayName: "Route Code", width: 150 },
        { name: 'RouteName', displayName: "Route Name", width: 150 },
        { name: 'RetailerCode', displayName: "Retailer Code", width: 150 },
        { name: 'RetailerName', displayName: "Retailer Name", width: 150 },
        { name: 'RetailerAddress', displayName: "Retailer Address", width: 150 },
        { name: 'RetailerContactNo', displayName: "Retailer Contact No", width: 150 },
        { name: 'DisplayProgramNo', displayName: "Display Program No", width: 150 },
        { name: 'DisplayProgramName', displayName: "Display Program Name", width: 250 },
        { name: 'ParticipationMonthCode', displayName: "Participation Month Code", width: 150 },
        { name: 'ParticipationMonth', displayName: "Participation Month", width: 150 },
        { name: 'ParticipationStatus', displayName: "Participation Status", width: 150 },
        { name: 'DiscontinueDate', displayName: "Discontinue Date", width: 150 },
        { name: 'DiscontinueReason', displayName: "Discontinue Reason", width: 250 },
        { name: 'ParticipateDate', displayName: "Participate Date", width: 150 },
        { name: 'InvoiceNo', displayName: "Invoice No", width: 150 },
        { name: 'InvoiceDate', displayName: "Invoice Date", width: 150 },
        { name: 'ProductCode', displayName: "Product Code", width: 150 },
        { name: 'ProductName', displayName: "Product Name", width: 250 },
        { name: 'PackSize', displayName: "Pack Size", width: 150 },
        { name: 'ProductPrice', displayName: "Product Price", width: 120, cellFilter: 'number:2', cellClass: 'grid-align', footerCellFilter: 'number:2'},
        { name: 'IssuedQty', displayName: "Issued Qty", width: 120, cellClass: 'grid-align', footerCellFilter: 'number:2' },
        { name: 'ReturnQty', displayName: "Return Qty", width: 120, cellClass: 'grid-align', footerCellFilter: 'number:2' },
        { name: 'ImsQty', displayName: "IMS Qty", width: 120, cellClass: 'grid-align', footerCellFilter: 'number:2' }
        
    ];
    $scope.gridDisplayProgramParticipation = {
        showColumnFooter: true,
        enableFiltering: true,
        enableSorting: true,
        columnDefs: columnDisplayProgramParticipation,
        enableGridMenu: true,
        enableSelectAll: true,
        exporterCsvFilename: "Display_Program_Participation.csv",
        exporterMenuPdf: false,
        exporterCsvLinkElement: angular.element(document.querySelectorAll(".custom-csv-link-location")),
        onRegisterApi: function (gridApi) {
            $scope.gridApi = gridApi;
        }
    };

    $scope.Reset = function () {

        $scope.BonusType = "";
        $scope.FromDate = "";
        $scope.ToDate = "";
        $scope.gridDisplayProgramParticipation.data = [];
        $scope.isDisabled = true;
        //$scope.frmDisplayProgramParticipation.RepType = undefined;
        $scope.frmDisplayProgramParticipation.Division = undefined;
        $scope.frmDisplayProgramParticipation.Region = undefined;
        $scope.frmDisplayProgramParticipation.Area = undefined;
        $scope.frmDisplayProgramParticipation.Territory = undefined;
        $scope.frmDisplayProgramParticipation.Customer = undefined;
        $scope.frmDisplayProgramParticipation.DisplayProgram = undefined;
        //$scope.Divisions = [];
        $scope.Regions = [];
        $scope.Areas = [];
        $scope.Territories = [];
        $scope.Customers = [];

    };
 
});