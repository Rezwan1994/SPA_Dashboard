app.controller("DistWiseBonusAdjustmentCtrl", function ($scope, $http, $interval, uiGridConstants, $filter) {

    var xl_file_name = "";
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
                $scope.gridDistWiseBonusAdjustment.enableGridMenu = response.data[0].DownLoadStatus;
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
            params: { SlNo: "(4,5,9)" }
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
        if ($scope.frmDistWiseBonusAdjustment.RepType.ReportTypeValue == "CurrentMonth" || $scope.frmDistWiseBonusAdjustment.RepType.ReportTypeValue == "LastMonth" ) {
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

    $scope.GetDistWiseBonusAdjustment = function () {
        
        if ($scope.frmDistWiseBonusAdjustment.RepType.ReportTypeValue == "CurrentMonth") {
            methodName = "GetDistWiseBonusAdjustmentCMonth";
        }
        if ($scope.frmDistWiseBonusAdjustment.RepType.ReportTypeValue == "LastMonth") {
            methodName = "GetDistWiseBonusAdjustmentLMonth";
        }
        
        if ($scope.frmDistWiseBonusAdjustment.RepType.ReportTypeValue == "CustomDate") {
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
                    methodName = "GetDistWiseBonusAdjustmentDt";

                }
            }
        }

        $http({
            method: "POST",
            url: MyApp.rootPath + "DistWiseBonusAdjustment/" + methodName,
            data: {
                fDate: $scope.FromDate,
                tDate: $scope.ToDate,
                dCode: $scope.frmDistWiseBonusAdjustment.Division.DivisionCode,
                rCode: $scope.frmDistWiseBonusAdjustment.Region.RegionCode,
                aCode: $scope.frmDistWiseBonusAdjustment.Area.AreaCode,
                tCode: $scope.frmDistWiseBonusAdjustment.Territory.TerritoryCode,
                cCode: $scope.frmDistWiseBonusAdjustment.Customer.CustomerCode
            }
        }).then(function (response) {
            if (response.data.length > 0) {
                $scope.gridDistWiseBonusAdjustment.data = response.data;
            }
            else {
                toastr.warning("No Data Found!", '');
                $scope.gridDistWiseBonusAdjustment.data = [];
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
            params: { dCode: $scope.frmDistWiseBonusAdjustment.Division.DivisionCode }
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
            params: { dCode: $scope.frmDistWiseBonusAdjustment.Division.DivisionCode, rCode: $scope.frmDistWiseBonusAdjustment.Region.RegionCode }
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
            params: { dCode: $scope.frmDistWiseBonusAdjustment.Division.DivisionCode, rCode: $scope.frmDistWiseBonusAdjustment.Region.RegionCode, aCode: $scope.frmDistWiseBonusAdjustment.Area.AreaCode }
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
            params: { dCode: $scope.frmDistWiseBonusAdjustment.Division.DivisionCode, rCode: $scope.frmDistWiseBonusAdjustment.Region.RegionCode, aCode: $scope.frmDistWiseBonusAdjustment.Area.AreaCode, tCode: $scope.frmDistWiseBonusAdjustment.Territory.TerritoryCode }
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




    //Grid
    var columnDistWiseBonusAdjustment = [
        //{ name: 'SlNo', displayName: "Sl. No", type: 'number', width: 100, cellClass: 'grid-align', footerCellFilter: 'number:2' },
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
        { name: 'DBLocation', displayName: "DB Loaction", width: 150 },
        { name: 'ProductCode', displayName: "Product Code", width: 150 },
        { name: 'ProductName', displayName: "Product Name", width: 200 },
        { name: 'PackSize', displayName: "Pack Size", width: 150 },
        { name: 'BonusQty', displayName: "Bonus Qty", width: 120, cellClass: 'grid-align'},
        { name: 'BonusVal', displayName: "Bonus Value", width: 120, cellFilter: 'number:2', aggregationType: uiGridConstants.aggregationTypes.sum, cellClass: 'grid-align', footerCellFilter: 'number:2' },
        { name: 'RetBnsQty', displayName: "Ret Bonus Qty", width: 120, cellFilter: 'number:0', cellClass: 'grid-align'},
        { name: 'RetBnsVal', displayName: "Ret Bonus Value", width: 120, cellFilter: 'number:2', aggregationType: uiGridConstants.aggregationTypes.sum, cellClass: 'grid-align', footerCellFilter: 'number:2' },
        { name: 'ActualBnsQty', displayName: "Actual Bonus Qty", width: 120, cellFilter: 'number:0', cellClass: 'grid-align'},
        { name: 'ActualBnsVal', displayName: "Actual Bonus Value", width: 120, cellFilter: 'number:2', aggregationType: uiGridConstants.aggregationTypes.sum, cellClass: 'grid-align', footerCellFilter: 'number:2' },
        { name: 'BonusPriceDiscount', displayName: "Bonus Price Discount", width: 150, cellFilter: 'number:2', aggregationType: uiGridConstants.aggregationTypes.sum, cellClass: 'grid-align', footerCellFilter: 'number:2' }
        


    ];
    $scope.gridDistWiseBonusAdjustment = {
        showColumnFooter: true,
        enableFiltering: true,
        enableSorting: true,
        columnDefs: columnDistWiseBonusAdjustment,
        enableGridMenu: true,
        enableSelectAll: true,
        exporterCsvFilename: "Distributor_Wise_Bonus_Adjustment.csv",
        exporterMenuPdf: false,
        exporterCsvLinkElement: angular.element(document.querySelectorAll(".custom-csv-link-location")),
        onRegisterApi: function (gridApi) {
            $scope.gridApi = gridApi;
        }
    };

    $scope.Reset = function () {
        $scope.FromDate = "";
        $scope.ToDate = "";
        $scope.gridDistWiseBonusAdjustment.data = [];
        $scope.isDisabled = false;
        $scope.ReportType = "";
        $scope.frmDistWiseBonusAdjustment.RepType = undefined;
        $scope.frmDistWiseBonusAdjustment.Division = undefined;
        $scope.frmDistWiseBonusAdjustment.Region = undefined;
        $scope.frmDistWiseBonusAdjustment.Area = undefined;
        $scope.frmDistWiseBonusAdjustment.Territory = undefined;
        $scope.frmDistWiseBonusAdjustment.Customer = undefined;
        $scope.Divisions = [];
        $scope.Regions = [];
        $scope.Areas = [];
        $scope.Territories = [];
        $scope.Customers = [];
    };

});