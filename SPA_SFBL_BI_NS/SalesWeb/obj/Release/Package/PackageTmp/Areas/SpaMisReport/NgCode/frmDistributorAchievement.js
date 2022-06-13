app.controller("DistributorAchievementCtrl", function ($scope, $http, uiGridConstants) {

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
                $scope.gridDistributorAchievement.enableGridMenu = response.data[0].DownLoadStatus;
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
        //Report Type List
        $http({
            method: "GET",
            url: MyApp.rootPath + "DistSrSales/GetReportTypeList",
            params: { SlNo: "(4,5)" }
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

        if ($scope.frmDistributorAchievement.RepType.ReportTypeValue == "Yesterday" || $scope.frmDistributorAchievement.RepType.ReportTypeValue == "LastSevendays" || $scope.frmDistributorAchievement.RepType.ReportTypeValue == "LastThirtydays" || $scope.frmDistributorAchievement.RepType.ReportTypeValue == "CurrentMonth" || $scope.frmDistributorAchievement.RepType.ReportTypeValue == "LastMonth" || $scope.frmDistributorAchievement.RepType.ReportTypeValue == "MonthOnMonthCy" || $scope.frmDistributorAchievement.RepType.ReportTypeValue == "MonthOnMonthLy") {
            $scope.isDisabled = true;
            $scope.FromDate = "";
            $scope.ToDate = "";

            $scope.frmDistributorAchievement.Division = undefined;
            $scope.frmDistributorAchievement.Region = undefined;
            $scope.frmDistributorAchievement.Area = undefined;
            $scope.frmDistributorAchievement.Territory = undefined;
            $scope.frmDistributorAchievement.Customer = undefined;

            $scope.Divisions = [];
            $scope.Regions = [];
            $scope.Areas = [];
            $scope.Territories = [];
            $scope.Customers = [];

        }
        else {
            $scope.FromDate = "";
            $scope.ToDate = "";
            $scope.isDisabled = false;

            $scope.frmDistributorAchievement.Division = undefined;
            $scope.frmDistributorAchievement.Region = undefined;
            $scope.frmDistributorAchievement.Area = undefined;
            $scope.frmDistributorAchievement.Territory = undefined;
            $scope.frmDistributorAchievement.Customer = undefined;

            $scope.Divisions = [];
            $scope.Regions = [];
            $scope.Areas = [];
            $scope.Territories = [];
            $scope.Customers = [];

        }
        $scope.GetDivisionList();
    };

    //-------------Grid------------------//

    var columnDistributorAchievement = [
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
        { name: 'ProductCode', displayName: "Product Code", width: 150 },
        { name: 'ProductName', displayName: "Product Name", width: 200 },
        { name: 'PackSize', displayName: "Pac kSize", width: 150 },        
        { name: 'SalesQty', displayName: "Sales Qty", width: 120, cellClass: 'grid-align', footerCellFilter: 'number:2' },
        { name: 'SalesVal', displayName: "Sales Value", width: 120, cellFilter: 'number:2', aggregationType: uiGridConstants.aggregationTypes.sum, cellClass: 'grid-align', footerCellFilter: 'number:2' },
        { name: 'CummSalesQty', displayName: "Cumulative Sales Qty", width: 200, cellClass: 'grid-align', footerCellFilter: 'number:2' },
        { name: 'CummSalesVal', displayName: "Cumulative Sales Value", width: 200, cellFilter: 'number:2', aggregationType: uiGridConstants.aggregationTypes.sum, cellClass: 'grid-align', footerCellFilter: 'number:2' },
        { name: 'TargetQty', displayName: "Target Qty", width: 120, cellClass: 'grid-align', footerCellFilter: 'number:2' },
        { name: 'TargetVal', displayName: "Target Value", width: 120, cellFilter: 'number:2', aggregationType: uiGridConstants.aggregationTypes.sum, cellClass: 'grid-align', footerCellFilter: 'number:2' },
        { name: 'Achievement', displayName: "Achievement(%)", width: 120, cellFilter: 'number:2', cellClass: 'grid-align'}
       ];
    $scope.gridDistributorAchievement = {
        showColumnFooter: true,
        enableFiltering: true,
        enableSorting: true,
        columnDefs: columnDistributorAchievement,
        enableGridMenu: true,
        enableSelectAll: true,
        exporterCsvFilename: 'Distributor_Wise_Sales_Achievement.csv',
        exporterMenuPdf: false,
        exporterCsvLinkElement: angular.element(document.querySelectorAll(".custom-csv-link-location")),
        onRegisterApi: function (gridApi) {
            $scope.gridApi = gridApi;
        }
    };


    $scope.GetDistributorAchievement = function () {


        $scope.gridDistributorAchievement.data = [];

        if ($scope.frmDistributorAchievement.RepType.ReportTypeValue == "Yesterday") {
            methodName = "GetDistributorAchievementYesterday";
        }
        if ($scope.frmDistributorAchievement.RepType.ReportTypeValue == "LastSevendays") {
            methodName = "GetDistributorAchievementLastSevendays";
        }
        if ($scope.frmDistributorAchievement.RepType.ReportTypeValue == "LastThirtydays") {
            methodName = "GetDistributorAchievementLastThirtydays";
        }
        if ($scope.frmDistributorAchievement.RepType.ReportTypeValue == "CurrentMonth") {
            methodName = "GetDistributorAchievementCurrentMonth";
        }
        if ($scope.frmDistributorAchievement.RepType.ReportTypeValue == "LastMonth") {
            methodName = "GetDistributorAchievementLastMonth";
        }
        if ($scope.frmDistributorAchievement.RepType.ReportTypeValue == "CustomDate") {
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
                    methodName = "GetDistributorAchievementCustomDate";
                }
            }
        }

        $http({
            method: "POST",
            url: MyApp.rootPath + "DistributorAchievement/" + methodName,
            data: {
                fDate: $scope.FromDate,
                tDate: $scope.ToDate,
                dCode: $scope.frmDistributorAchievement.Division.DivisionCode,
                rCode: $scope.frmDistributorAchievement.Region.RegionCode,
                aCode: $scope.frmDistributorAchievement.Area.AreaCode,
                tCode: $scope.frmDistributorAchievement.Territory.TerritoryCode,
                cCode: $scope.frmDistributorAchievement.Customer.CustomerCode
            }
        }).then(function (response) {
            if (response.data.length > 0) {
                $scope.gridDistributorAchievement.data = response.data;

            }
            else {
                toastr.warning("No Data Found!", '');
                $scope.gridDistributorAchievement.data = [];
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
            params: { dCode: $scope.frmDistributorAchievement.Division.DivisionCode }
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
            params: { dCode: $scope.frmDistributorAchievement.Division.DivisionCode, rCode: $scope.frmDistributorAchievement.Region.RegionCode }
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
            params: { dCode: $scope.frmDistributorAchievement.Division.DivisionCode, rCode: $scope.frmDistributorAchievement.Region.RegionCode, aCode: $scope.frmDistributorAchievement.Area.AreaCode }
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
            params: { dCode: $scope.frmDistributorAchievement.Division.DivisionCode, rCode: $scope.frmDistributorAchievement.Region.RegionCode, aCode: $scope.frmDistributorAchievement.Area.AreaCode, tCode: $scope.frmDistributorAchievement.Territory.TerritoryCode }
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
        $scope.gridDistributorAchievement.data = [];
        $scope.isDisabled = false;
        $scope.frmDistributorAchievement.RepType = undefined;
        $scope.frmDistributorAchievement.Division = undefined;
        $scope.frmDistributorAchievement.Region = undefined;
        $scope.frmDistributorAchievement.Area = undefined;
        $scope.frmDistributorAchievement.Territory = undefined;
        $scope.frmDistributorAchievement.Customer = undefined;

        $scope.Divisions = [];
        $scope.Regions = [];
        $scope.Areas = [];
        $scope.Territories = [];
        $scope.Customers = [];


    };

});