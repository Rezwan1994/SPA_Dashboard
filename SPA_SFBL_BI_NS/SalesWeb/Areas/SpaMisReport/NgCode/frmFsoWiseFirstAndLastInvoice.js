app.controller("FsoWiseFirstAndLastInvoiceCtrl", function ($scope, $http, uiGridConstants) {

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
                $scope.gridFsoWiseFirstAndLastInvoice.enableGridMenu = response.data[0].DownLoadStatus;
            }
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
            params: { SlNo: "(34,35)" }
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


    var columnFsoWiseFirstAndLastInvoice = [
        { name: 'CustomerCode', displayName: "Customer Code",width:"150", aggregationType: uiGridConstants.aggregationTypes.count, footerCellFilter: 'number:0' },
        { name: 'CustomerName', displayName: "Customer Name", width: "150"},
        { name: 'DbLocation', displayName: "DbLocation", width: "150" },
        { name: 'MarketCode', displayName: "Market Code", width: "150"},
        { name: 'MarketName', displayName: "Market Name", width: "150"},
        { name: 'FsoCode', displayName: "Fso Code", width: "150"},
        { name: 'FsoName', displayName: "Fso Name", width: "150"},
        { name: 'JoiningDate', displayName: "Joining Date", width: "150" },
        { name: 'MobileNo', displayName: "Mobile No", width: "150"},
        { name: 'FirstInvoiceNo', displayName: "First Invoice No", width: "150" },
        { name: 'FirstInvoiceDate', displayName: "First Invoice Date", width: "150" },
        { name: 'LastInvoiceNo', displayName: "Last Invoice No", width: "150" },
        { name: 'LastInvoiceDate', displayName: "Last Invoice Date", width: "150" },
        { name: 'Status', displayName: "Status", width: "150"}
    ];
    $scope.gridFsoWiseFirstAndLastInvoice = {
        showColumnFooter: true,
        enableFiltering: true,
        enableSorting: true,
        columnDefs: columnFsoWiseFirstAndLastInvoice,
        enableGridMenu: true,
        enableSelectAll: true,
        exporterCsvFilename: 'Fso_Wise_First_And_Last_Invoice.csv',
        exporterMenuPdf: false,
        exporterCsvLinkElement: angular.element(document.querySelectorAll(".custom-csv-link-location")),
        onRegisterApi: function (gridApi) {
            $scope.gridApi = gridApi;
        }
    };


    $scope.GetFsoWiseFirstAndLastInvoice = function () {

        if ($scope.frmFsoWiseFirstAndLastInvoice.RepType.ReportTypeValue == "FSOWiseFirstandLastInvoiceRel") {
            methodName = "GetFsoWiseFirstAndLastInvoiceRel";
        }
        if ($scope.frmFsoWiseFirstAndLastInvoice.RepType.ReportTypeValue == "FSOWiseFirstandLastInvoiceAll") {
            methodName = "GetFsoWiseFirstAndLastInvoiceAll";
        }


        $http({
            method: "POST",
            url: MyApp.rootPath + "FsoWiseFirstAndLastInvoice/" + methodName
        }).then(function (response) {
            if (response.data.length > 0) {
                $scope.gridFsoWiseFirstAndLastInvoice.data = response.data;

            }
            else {
                toastr.warning("No Data Found!", '');
                $scope.gridFsoWiseFirstAndLastInvoice.data = [];
            }
        }, function () {
            toastr.error("Error!");
        });
    };


    $scope.Reset = function () {

        $scope.gridFsoWiseFirstAndLastInvoice.data = [];
        $scope.frmFsoWiseFirstAndLastInvoice.RepType = undefined;

    };

});