app.controller("ToDaysFactoryInvRecvCtrl", function ($scope, $http, uiGridConstants) {

    var xl_file_name = "";
    var methodName = "";

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
                $scope.gridToDaysFactoryInvRecv.enableGridMenu = response.data[0].DownLoadStatus;
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


    $scope.GetToDaysFactoryInvRecv = function () {

        
        if ($scope.frmToDaysFactoryInvRecv.RepType.ReportTypeValue == "CustomDate") {
            if ($scope.ToDate == "" || $scope.ToDate == undefined || $scope.ToDate == null) {
                toastr.warning("To Date  Cannot be empty !");
                return false;
            } else {
                //var todayDate = new Date(new Date().getFullYear(), new Date().getMonth(), new Date().getDate());
                //var endDate = $scope.ToDate.split("/");
                //var convertedEndDate = new Date(+endDate[2], endDate[1] - 1, +endDate[0]);
                //var eDate = new Date(convertedEndDate.getFullYear(), convertedEndDate.getMonth(), convertedEndDate.getDate());
                //if (eDate >= todayDate) {
                //    toastr.warning("To Date  Less Than Current Date !");
                //    return false;
                //} else {
                //    methodName = "GetToDaysFactoryInvRecvCustomDate";

                //}
                methodName = "GetToDaysFactoryInvRecvCustomDate";

            }
        }


        $http({
            method: "POST",
            url: MyApp.rootPath + "ToDaysFactoryInvRecv/" + methodName,
            data: {
                fromDate: $scope.FromDate,
                toDate: $scope.ToDate,
                dCode: $scope.frmToDaysFactoryInvRecv.Division.DivisionCode,
                rCode: $scope.frmToDaysFactoryInvRecv.Region.RegionCode,
                aCode: $scope.frmToDaysFactoryInvRecv.Area.AreaCode,
                tCode: $scope.frmToDaysFactoryInvRecv.Territory.TerritoryCode,
                cCode: $scope.frmToDaysFactoryInvRecv.Customer.CustomerCode,
                bType: $scope.BonusType
            }
        }).then(function (response) {
            if (response.data.length > 0) {
                $scope.gridToDaysFactoryInvRecv.data = response.data;
            }
            else {
                toastr.warning("No Data Found!", '');
                $scope.gridToDaysFactoryInvRecv.data = [];
            }
        }, function (response) {
            //alert(response);
            toastr.error("Error!");
        });
    };


    $scope.OnReportTypeChange = function () {
        if ($scope.frmToDaysFactoryInvRecv.RepType.ReportTypeValue == "Yesterday" || $scope.frmToDaysFactoryInvRecv.RepType.ReportTypeValue == "LastSevendays" || $scope.frmToDaysFactoryInvRecv.RepType.ReportTypeValue == "LastThirtydays" || $scope.frmToDaysFactoryInvRecv.RepType.ReportTypeValue == "CurrentMonth" || $scope.frmToDaysFactoryInvRecv.RepType.ReportTypeValue == "LastMonth" || $scope.frmToDaysFactoryInvRecv.RepType.ReportTypeValue == "MonthOnMonthCy" || $scope.frmToDaysFactoryInvRecv.RepType.ReportTypeValue == "MonthOnMonthLy") {
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
            params: { dCode: $scope.frmToDaysFactoryInvRecv.Division.DivisionCode }
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
            params: { dCode: $scope.frmToDaysFactoryInvRecv.Division.DivisionCode, rCode: $scope.frmToDaysFactoryInvRecv.Region.RegionCode }
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
            params: { dCode: $scope.frmToDaysFactoryInvRecv.Division.DivisionCode, rCode: $scope.frmToDaysFactoryInvRecv.Region.RegionCode, aCode: $scope.frmToDaysFactoryInvRecv.Area.AreaCode }
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
            params: { dCode: $scope.frmToDaysFactoryInvRecv.Division.DivisionCode, rCode: $scope.frmToDaysFactoryInvRecv.Region.RegionCode, aCode: $scope.frmToDaysFactoryInvRecv.Area.AreaCode, tCode: $scope.frmToDaysFactoryInvRecv.Territory.TerritoryCode }
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
    



    $scope.GetReportTypeList = function () {
        //Report Type List
        $http({
            method: "GET",
            url: MyApp.rootPath + "DistSrSales/GetReportTypeList",
            params: { SlNo: "(9)" }
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
    var columnToDaysFactoryInvRecv = [
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
        { name: 'DBLocation', displayName: "DB Location", width: 150 },
        { name: 'RequisitionNo', displayName: "Requisition No", width: 120, cellClass: 'grid-align' },
        { name: 'ReceiveDate', displayName: "ReceiveDate", width: 120, cellClass: 'grid-align' },
        { name: 'InvoiceNo', displayName: "Invoice No", width: 120, cellClass: 'grid-align' },
        { name: 'InvoiceDate', displayName: "Invoice Date", width: 120, cellClass: 'grid-align' },
        { name: 'InvTypeCode', displayName: "InvType Code", width: 120, cellClass: 'grid-align' },
        { name: 'InvTypeName', displayName: "InvType Name", width: 120, cellClass: 'grid-align' },
        { name: 'ProductCode', displayName: "Product Code", width: 120, cellClass: 'grid-align' },
        { name: 'ProductName', displayName: "Product Name", width: 120, cellClass: 'grid-align' },
        { name: 'PackSize', displayName: "Pack Size", width: 120, cellClass: 'grid-align' },
        { name: 'ProductPrice', displayName: "Product Price", width: 120, cellClass: 'grid-align' },
        { name: 'ProductBatchNo', displayName: "Product Batch No", width: 120, cellClass: 'grid-align' },
        { name: 'LotNo', displayName: "Lot No", width: 120, cellClass: 'grid-align' },
        { name: 'ReceiveQty', displayName: "Receive Qty", width: 120, cellClass: 'grid-align', footerCellFilter: 'number:2' }

       

    ];
    $scope.gridToDaysFactoryInvRecv = {
        showColumnFooter: true,
        enableFiltering: true,
        enableSorting: true,
        columnDefs: columnToDaysFactoryInvRecv,
        enableGridMenu: true,
        enableSelectAll: true,
        exporterCsvFilename: "ToDays_Factory_Invoice_Received.csv",
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
        $scope.gridToDaysFactoryInvRecv.data = [];
        $scope.isDisabled = false;
        //$scope.ReportType = "";
        $scope.frmToDaysFactoryInvRecv.RepType = undefined;
        $scope.frmToDaysFactoryInvRecv.Division = undefined;
        $scope.frmToDaysFactoryInvRecv.Region = undefined;
        $scope.frmToDaysFactoryInvRecv.Area = undefined;
        $scope.frmToDaysFactoryInvRecv.Territory = undefined;
        $scope.frmToDaysFactoryInvRecv.Customer = undefined;
        $scope.Divisions = [];
        $scope.Regions = [];
        $scope.Areas = [];
        $scope.Territories = [];
        $scope.Customers = [];
    };

});