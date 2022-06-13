app.controller("ComboBonusDecCtrl", function ($scope, $http, uiGridConstants, uiGridExporterService) {

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
                $scope.gridComboBonusDec.enableGridMenu = response.data[0].DownLoadStatus;
            }
        }, function (response) {
            if (response.status === 404) {
                toastr.warning("Error Loading Report Download Status!", { timeOut: 2000 });
            }
        });
    };
    $scope.GetReportDownLoadStatus();


    var columnComboBonusDec = [
        { name: 'ComboBonusNo', displayName: "Combo Bonus No", width: 150, aggregationType: uiGridConstants.aggregationTypes.count, footerCellFilter: 'number:0' },
        { name: 'ComboBonusName', displayName: "Combo Bonus Name", width: 150 },
        { name: 'EffectFromDate', displayName: "Effect From Date", width: 150 },
        { name: 'EffectToDate', displayName: "Effect To Date", width: 150 },
        { name: 'LocationType', displayName: "Location Type", width: 150 },
        { name: 'LocationCode', displayName: "Location Code", width: 150 },
        { name: 'LocationName', displayName: "Location Name", width: 150 },
        { name: 'LocationStatus', displayName: "Location Status", width: 150 },
        { name: 'ProductType', displayName: "Product Type", width: 150 },
        { name: 'ProductTypeCode', displayName: "Product Type Code", width: 150 },
        { name: 'ProductTypeName', displayName: "Product Type Name", width: 150 },
        { name: 'BonusType', displayName: "Bonus Type", width: 150 },
        { name: 'ProductCode', displayName: "Product Code", width: 150 },
        { name: 'ProductName', displayName: "Product Name", width: 150 },
        { name: 'PackSize', displayName: "Pack Size", width: 150 },
        { name: 'BonusProductCode', displayName: "Bonus Product Code", width: 150 },
        { name: 'BonusProductName', displayName: "Bonus Product Name", width: 200 },
        { name: 'BonusPackSize', displayName: "Bonus Pack Size", width: 150 },
        { name: 'PriorityNo', displayName: "PriorityNo", width: 200, cellClass: 'grid-align' },
        { name: 'SlabQty', displayName: "Slab Qty", width: 200, cellClass: 'grid-align' },
        { name: 'BonusQty', displayName: "Bonus Qty", width: 200, cellClass: 'grid-align' },
        { name: 'BonusDiscount', displayName: "Bonus Discount", width: 200, cellClass: 'grid-align' },
        { name: 'BonusStatus', displayName: "Bonus Status", width: 300 },
    ];


    $scope.gridComboBonusDec = {
        showColumnFooter: true,
        enableFiltering: true,
        enableSorting: true,
        columnDefs: columnComboBonusDec,
        enableGridMenu: true,
        enableSelectAll: true,
        exporterCsvFilename: 'Combo_Bonus_Report.csv',
        exporterMenuPdf: false,
        exporterCsvLinkElement: angular.element(document.querySelectorAll(".custom-csv-link-location")),
        onRegisterApi: function (gridApi) {
            $scope.gridApi = gridApi;
        }
    };



    $scope.GetComboBonusDeclaration = function () {


        $http({
            method: "POST",
            url: MyApp.rootPath + "ComboBonusDec/GetComboBonusDeclaration",
            data: {
                cbNo: $scope.frmComboBonusDec.ComboBonus.ComboBonusNo,
            }
        }).then(function (response) {

            if (response.data.length > 0) {
                $scope.gridComboBonusDec.data = response.data;
            }
            else {
                toastr.warning("No Data Found!", '');
                $scope.gridComboBonusDec.data = [];
            }
        }, function (response) {
            toastr.error("Error!");
        });
    };

    $scope.GetComboBonusList = function () {
        $http({
            method: "GET",
            url: MyApp.rootPath + "ComboBonus/GetComboBonusList"
        }).then(function (response) {
            if (response.data.Status === "" || response.data.Status === null) {
                if (response.data.Data.length > 0) {
                    $scope.ComboBonuses = response.data.Data;
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

    $scope.GetComboBonusList();


    $scope.Reset = function () {
        
        $scope.frmComboBonusDec.ComboBonus = undefined;
        
        $scope.gridComboBonusDec.data = [];
    };

});