app.controller("RetBnsSlabAdjCtrl", function ($scope, $http, uiGridConstants) {

    var xl_file_name = "";
    var methodName = "";

    $scope.BonusType = "";
    $scope.isDisabled = true;

    $scope.EventPerm(22);

    $scope.GetReportDownLoadStatus = function () {
        $http({
            method: "GET",
            url: MyApp.rootPath + "RetailerIms/GetReportDownLoadStatus",
            datatype: "json",
            params: { url: window.location.pathname}
        }).then(function (response) {
            if (response.data.length > 0) {
                $scope.gridRetBnsSlabAdj.enableGridMenu = response.data[0].DownLoadStatus;
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



    $scope.GetRetBnsSlabAdj = function () {

        if ($scope.frmRetBnsSlabAdj.RepType.ReportTypeValue == "Today") {
            methodName = "GetRetBnsSlabAdjToday";
        }

        if ($scope.frmRetBnsSlabAdj.RepType.ReportTypeValue == "Yesterday") {
            methodName = "GetRetBnsSlabAdjYesterday";
        }
        if ($scope.frmRetBnsSlabAdj.RepType.ReportTypeValue == "LastSevendays") {
            methodName = "GetRetBnsSlabAdjLastSevendays";
        }
        if ($scope.frmRetBnsSlabAdj.RepType.ReportTypeValue == "LastThirtydays") {
            methodName = "GetRetBnsSlabAdjLastThirtydays";
        }
        if ($scope.frmRetBnsSlabAdj.RepType.ReportTypeValue == "CurrentMonth") {
            methodName = "GetRetBnsSlabAdjCurrentMonth";
        }
        if ($scope.frmRetBnsSlabAdj.RepType.ReportTypeValue == "LastMonth") {
            methodName = "GetRetBnsSlabAdjLastMonth";
        }
        if ($scope.frmRetBnsSlabAdj.RepType.ReportTypeValue == "MonthOnMonthCy") {
            methodName = "GetRouteBrandImsMonthOnMonthCy";
        }
        if ($scope.frmRetBnsSlabAdj.RepType.ReportTypeValue == "MonthOnMonthLy") {
            methodName = "GetRetBnsSlabAdjMonthOnMonthLy";
        }
        if ($scope.frmRetBnsSlabAdj.RepType.ReportTypeValue == "MonthOnMonthLpy") {
            methodName = "GetRetBnsSlabAdjMonthOnMonthLpy";
        }
        if ($scope.frmRetBnsSlabAdj.RepType.ReportTypeValue == "CustomDate") {
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
                    methodName = "GetRetBnsSlabAdjCustomDate";

                }
            }
        }

        $http({
            method: "POST",
            url: MyApp.rootPath + "RetBnsSlabAdj/" + methodName,
            data: {
                fromDate: $scope.FromDate,
                toDate: $scope.ToDate,
                dCode: $scope.frmRetBnsSlabAdj.Division.DivisionCode,
                rCode: $scope.frmRetBnsSlabAdj.Region.RegionCode,
                aCode: $scope.frmRetBnsSlabAdj.Area.AreaCode,
                tCode: $scope.frmRetBnsSlabAdj.Territory.TerritoryCode,
                cCode: $scope.frmRetBnsSlabAdj.Customer.CustomerCode,
                bType: $scope.BonusType
            }
        }).then(function (response) {
            if (response.data.length > 0) {
                $scope.gridRetBnsSlabAdj.data = response.data;
            }
            else {
                toastr.warning("No Data Found!", '');
                $scope.gridRetBnsSlabAdj.data = [];
            }
            }, function (response) {
                //alert(response);
                toastr.error("Error!");
        });
    };


    $scope.OnReportTypeChange = function () {
        if ($scope.frmRetBnsSlabAdj.RepType.ReportTypeValue == "Today" || $scope.frmRetBnsSlabAdj.RepType.ReportTypeValue == "LastSevendays" || $scope.frmRetBnsSlabAdj.RepType.ReportTypeValue == "LastThirtydays" || $scope.frmRetBnsSlabAdj.RepType.ReportTypeValue == "CurrentMonth" || $scope.frmRetBnsSlabAdj.RepType.ReportTypeValue == "LastMonth" || $scope.frmRetBnsSlabAdj.RepType.ReportTypeValue == "MonthOnMonthCy" || $scope.frmRetBnsSlabAdj.RepType.ReportTypeValue == "MonthOnMonthLy") {
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
            params: { dCode: $scope.frmRetBnsSlabAdj.Division.DivisionCode }
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
            params: { dCode: $scope.frmRetBnsSlabAdj.Division.DivisionCode, rCode: $scope.frmRetBnsSlabAdj.Region.RegionCode }
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
            params: { dCode: $scope.frmRetBnsSlabAdj.Division.DivisionCode, rCode: $scope.frmRetBnsSlabAdj.Region.RegionCode, aCode: $scope.frmRetBnsSlabAdj.Area.AreaCode }
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
            params: { dCode: $scope.frmRetBnsSlabAdj.Division.DivisionCode, rCode: $scope.frmRetBnsSlabAdj.Region.RegionCode, aCode: $scope.frmRetBnsSlabAdj.Area.AreaCode, tCode: $scope.frmRetBnsSlabAdj.Territory.TerritoryCode }
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
            params: { dCode: $scope.frmRetBnsSlabAdj.Division.DivisionCode, rCode: $scope.frmRetBnsSlabAdj.Region.RegionCode, aCode: $scope.frmRetBnsSlabAdj.Area.AreaCode, tCode: $scope.frmRetBnsSlabAdj.Territory.TerritoryCode, cCode: $scope.frmRetBnsSlabAdj.Customer.CustomerCode }
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
        //Report Type List
        $http({
            method: "GET",
            url: MyApp.rootPath + "DistSrSales/GetReportTypeList",
            params: { SlNo: "(9,0)" }
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

    //Grid
    var columnRetBnsSlabAdj = [
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


        { name: 'InvoiceNo', displayName: "Invoice No", width: 150 },
        { name: 'InvoiceDate', displayName: "Invoice Date", width: 150 },

        { name: 'ProductCode', displayName: "Product Code", width: 150 },
        { name: 'ProductName', displayName: "Product Name", width: 150 },
        { name: 'PackSize', displayName: "Pack Size", width: 150 },
        { name: 'BonusSlabType', displayName: "Bonus Slab Type", width: 150 },
        { name: 'BonusSlabQty', displayName: "Bonus Slab Qty", width: 150 },

        { name: 'NormalDecSlab', displayName: "Normal Dec Slab", width: 120, cellClass: 'grid-align', footerCellFilter: 'number:2' },
        { name: 'IssuedQty', displayName: "Issued Qty", width: 120, cellClass: 'grid-align', footerCellFilter: 'number:2' },
        { name: 'ReturnQty', displayName: "Return Qty", width: 120, cellClass: 'grid-align', footerCellFilter: 'number:2' },
        { name: 'TotalImsQty', displayName: "Total IMS Qty", width: 120, cellClass: 'grid-align', footerCellFilter: 'number:2' },
        { name: 'NormalImsQty', displayName: "Normal IMS Qty", width: 120, cellClass: 'grid-align', footerCellFilter: 'number:2' },
        { name: 'ImsNormalBnsQty', displayName: "IMS Normal Bonus Qty", width: 120, cellClass: 'grid-align', footerCellFilter: 'number:2' },
        { name: 'SlabImsQty', displayName: "Slab IMS Qty", width: 120, cellClass: 'grid-align', footerCellFilter: 'number:2' },
        { name: 'ImsSlabBnsQty', displayName: "Ims Slab Bonus Qty", width: 120, cellClass: 'grid-align', footerCellFilter: 'number:2' },


        { name: 'BonusRatio', displayName: "Bonus Ratio", width: 120, cellClass: 'grid-align', footerCellFilter: 'number:2' },
        { name: 'AdjustableSlab', displayName: "Adjustable Slab", width: 120, cellClass: 'grid-align', footerCellFilter: 'number:2' }

     ];
    $scope.gridRetBnsSlabAdj = {
        showGridFooter: true,
        enableFiltering: true,
        enableSorting: true,
        columnDefs: columnRetBnsSlabAdj,
        enableGridMenu: true,
        enableSelectAll: true,
        exporterCsvFilename: "Ret_Bonus_Slab_Adjustment.csv",
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
        $scope.gridRetBnsSlabAdj.data = [];
        $scope.isDisabled = true;
        //$scope.ReportType = "";
        $scope.frmRetBnsSlabAdj.RepType = undefined;
        $scope.frmRetBnsSlabAdj.Division = undefined;
        $scope.frmRetBnsSlabAdj.Region = undefined;
        $scope.frmRetBnsSlabAdj.Area = undefined;
        $scope.frmRetBnsSlabAdj.Territory = undefined;
        $scope.frmRetBnsSlabAdj.Customer = undefined;
        $scope.Divisions = [];
        $scope.Regions = [];
        $scope.Areas = [];
        $scope.Territories = [];
        $scope.Customers = [];
    };

});