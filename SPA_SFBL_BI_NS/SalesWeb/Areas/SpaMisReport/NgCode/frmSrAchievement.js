app.controller("SrAchievementCtrl", function ($scope, $http, uiGridConstants) {

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
                $scope.gridSrAchievement.enableGridMenu = response.data[0].DownLoadStatus;
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

        if ($scope.frmSrAchievement.RepType.ReportTypeValue == "Yesterday" || $scope.frmSrAchievement.RepType.ReportTypeValue == "LastSevendays" || $scope.frmSrAchievement.RepType.ReportTypeValue == "LastThirtydays" || $scope.frmSrAchievement.RepType.ReportTypeValue == "CurrentMonth" || $scope.frmSrAchievement.RepType.ReportTypeValue == "LastMonth" || $scope.frmSrAchievement.RepType.ReportTypeValue == "MonthOnMonthCy" || $scope.frmSrAchievement.RepType.ReportTypeValue == "MonthOnMonthLy") {
            $scope.isDisabled = true;
            $scope.FromDate = "";
            $scope.ToDate = "";

            $scope.frmSrAchievement.Division = undefined;
            $scope.frmSrAchievement.Region = undefined;
            $scope.frmSrAchievement.Area = undefined;
            $scope.frmSrAchievement.Territory = undefined;
            $scope.frmSrAchievement.Customer = undefined;

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

            $scope.frmSrAchievement.Division = undefined;
            $scope.frmSrAchievement.Region = undefined;
            $scope.frmSrAchievement.Area = undefined;
            $scope.frmSrAchievement.Territory = undefined;
            $scope.frmSrAchievement.Customer = undefined;

            $scope.Divisions = [];
            $scope.Regions = [];
            $scope.Areas = [];
            $scope.Territories = [];
            $scope.Customers = [];

        }
        $scope.GetDivisionList();
    };
    //-------------Grid------------------//

    var columnSrAchievement = [
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
        { name: 'SrCode', displayName: "Sr Code", width: 150 },
        { name: 'SrName', displayName: "Sr Name", width: 150 },
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
    $scope.gridSrAchievement = {
        showColumnFooter: true,
        enableFiltering: true,
        enableSorting: true,
        columnDefs: columnSrAchievement,
        enableGridMenu: true,
        enableSelectAll: true,
        exporterCsvFilename: 'Distributor_Wise_Sales_Achievement.csv',
        exporterMenuPdf: false,
        exporterCsvLinkElement: angular.element(document.querySelectorAll(".custom-csv-link-location")),
        onRegisterApi: function (gridApi) {
            $scope.gridApi = gridApi;
        }
    };


    $scope.GetSrAchievement = function () {


        $scope.gridSrAchievement.data = [];

        if ($scope.frmSrAchievement.RepType.ReportTypeValue == "Yesterday") {
            methodName = "GetSrAchievementYesterday";
        }
        if ($scope.frmSrAchievement.RepType.ReportTypeValue == "LastSevendays") {
            methodName = "GetSrAchievementLastSevendays";
        }
        if ($scope.frmSrAchievement.RepType.ReportTypeValue == "LastThirtydays") {
            methodName = "GetSrAchievementLastThirtydays";
        }
        if ($scope.frmSrAchievement.RepType.ReportTypeValue == "CurrentMonth") {
            methodName = "GetSrAchievementCurrentMonth";
        }
        if ($scope.frmSrAchievement.RepType.ReportTypeValue == "LastMonth") {
            methodName = "GetSrAchievementLastMonth";
        }
        if ($scope.frmSrAchievement.RepType.ReportTypeValue == "CustomDate") {
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
                    methodName = "GetSrAchievementCustomDate";
                }
            }
        }

        $http({
            method: "POST",
            url: MyApp.rootPath + "SrAchievement/" + methodName,
            data: {
                fDate: $scope.FromDate,
                tDate: $scope.ToDate,
                dCode: $scope.frmSrAchievement.Division.DivisionCode,
                rCode: $scope.frmSrAchievement.Region.RegionCode,
                aCode: $scope.frmSrAchievement.Area.AreaCode,
                tCode: $scope.frmSrAchievement.Territory.TerritoryCode,
                cCode: $scope.frmSrAchievement.Customer.CustomerCode,
                sCode: $scope.frmSrAchievement.Sr.SrCode
            }
        }).then(function (response) {
            if (response.data.length > 0) {
                $scope.gridSrAchievement.data = response.data;

            }
            else {
                toastr.warning("No Data Found!", '');
                $scope.gridSrAchievement.data = [];
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
            params: { dCode: $scope.frmSrAchievement.Division.DivisionCode }
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
            params: { dCode: $scope.frmSrAchievement.Division.DivisionCode, rCode: $scope.frmSrAchievement.Region.RegionCode }
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
            params: { dCode: $scope.frmSrAchievement.Division.DivisionCode, rCode: $scope.frmSrAchievement.Region.RegionCode, aCode: $scope.frmSrAchievement.Area.AreaCode }
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
            params: { dCode: $scope.frmSrAchievement.Division.DivisionCode, rCode: $scope.frmSrAchievement.Region.RegionCode, aCode: $scope.frmSrAchievement.Area.AreaCode, tCode: $scope.frmSrAchievement.Territory.TerritoryCode }
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
            params: { dCode: $scope.frmSrAchievement.Division.DivisionCode, rCode: $scope.frmSrAchievement.Region.RegionCode, aCode: $scope.frmSrAchievement.Area.AreaCode, tCode: $scope.frmSrAchievement.Territory.TerritoryCode, cCode: $scope.frmSrAchievement.Customer.CustomerCode }
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
        $scope.gridSrAchievement.data = [];
        $scope.isDisabled = false;
        $scope.frmSrAchievement.RepType = undefined;
        $scope.frmSrAchievement.Division = undefined;
        $scope.frmSrAchievement.Region = undefined;
        $scope.frmSrAchievement.Area = undefined;
        $scope.frmSrAchievement.Territory = undefined;
        $scope.frmSrAchievement.Customer = undefined;

        $scope.Divisions = [];
        $scope.Regions = [];
        $scope.Areas = [];
        $scope.Territories = [];
        $scope.Customers = [];


    };

});