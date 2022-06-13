app.controller("TestCtrl", function ($scope, $http, uiGridConstants, $timeout, $window) {

   
    $scope.btnSaveValue = "Save";
    $scope.OrderStatus = "Active";
    $scope.isDisableProduct=false
    var index = null;


    //$scope.ShowMessage = "";
    //$scope.isCheck = false;


    //$scope.ShowConfirm = function () {

    //    if ($window.confirm("Please confirm?")) {

    //        $scope.Message = "You clicked YES.";

    //    } else {

    //        $scope.Message = "You clicked NO.";

    //    }
    //}

    $scope.GetCustomerList = function () {
        $http({
            method: "GET",
            url: MyApp.rootPath + "DistSrSales/GetCustomerList",
            params: { dCode: 'ALL', rCode: 'ALL', aCode: 'ALL', tCode: 'ALL' }
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
    };

    $scope.GetCustomerList();

    $scope.GetProductList = function () {
        $http({
            method: "GET",
            url: MyApp.rootPath + "Test/GetProductList"
        }).then(function (response) {
            if (response.data.Status === "" || response.data.Status === null) {
                if (response.data.Data.length > 0) {
                    $scope.Products = response.data.Data;
                } else {
                    $scope.frmTest.Product = undefined;
                    $scope.Products = [];
                }
            } else {
                toastr.warning(response.data.Status, { timeOut: 2000 });
            }
        }, function (response) {
            if (response.status === 404) {
                toastr.warning("Error Loading products!", { timeOut: 2000 });
            }
        });
    };
    $scope.GetProductList();

    $scope.AddItem = function () {
        if (index !== null) {
            $scope.gridOrderDtl.data.splice(index, 1);
        } else {
            for (var i = 0, l = $scope.gridOrderDtl.data.length; i < l; i += 1) {
                if ($scope.gridOrderDtl.data[i].ProductCode == $scope.frmTest.Product.ProductCode) {
                    toastr.warning("Product Already Exists!", { timeOut: 2000 });
                    return false;
                }
            }
        }
        $scope.gridOrderDtl.data.push({
            MstId: $scope.MasterId,
            DtlId: $scope.DetailId,
            ProductCode: $scope.frmTest.Product.ProductCode,
            ProductName: $scope.frmTest.Product.ProductName,
            PackSize: $scope.frmTest.Product.PackSize,
            OrderQty: $scope.OrderQty
        });
        index = null;
        $scope.frmTest.Product = undefined;
        $scope.OrderQty = "";
        $scope.MasterId = "";
        $scope.DetailId = "";
        $scope.isDisableProduct = false
    };


    var columnOrderDtl = [
        { name: 'MstId', enableCellEdit: false, displayName: "Mst Id" },
        { name: 'DtlId', enableCellEdit: false, displayName: "Dtl Id" },
        { name: 'ProductCode', enableCellEdit: false, displayName: "Product Code", aggregationType: uiGridConstants.aggregationTypes.count },
        { name: 'ProductName', enableCellEdit: false, displayName: "Product Name" },
        { name: 'PackSize', enableCellEdit: false, displayName: "Pack Size" },
        { name: 'OrderQty', enableCellEdit: false, displayName: "Order Qty" },
        {
            name: 'Edit ', enableFiltering: false, enableSorting: false,
            cellTemplate: '<div style="text-align:center;position: relative;width:100%;padding:2px 2px 2px 6px;"><button  class=" btn-success " ng-click="grid.appScope.editGridOptionsRow(row)"><i class="fa fa-edit"></i>&nbspEdit</button></div>'
        },
        {
            name: 'Delete ', enableFiltering: false, enableSorting: false,
            cellTemplate: '<div style="text-align:center;position: relative;width:100%;padding:2px 2px 2px 6px;"><button  class=" btn-danger " ng-click="grid.appScope.deleteGridOptionsRow(row)"><i class="fa fa-remove"></i>&nbspDelete</button></div>'
        }
    ];
    $scope.gridOrderDtl = {        
        exporterMenuCsv: false,
        showColumnFooter: true,
        //enableFiltering: true,
        //enableSorting: true,
        columnDefs: columnOrderDtl,
        enableGridMenu: true,
        enableSelectAll: true,
        onRegisterApi: function (gridApi) {
            $scope.gridApi = gridApi;
        }
    };

    $scope.deleteGridOptionsRow = function (row) {
        if (row.entity.DtlId === "" || row.entity.DtlId === null || row.entity.DtlId === undefined) {
            index = $scope.gridOrderDtl.data.indexOf(row.entity);
            $scope.gridOrderDtl.data.splice(index, 1);
        } 
    };


    $scope.editGridOptionsRow = function (row) {
        $scope.MasterId = row.entity.MstId;
        $scope.DetailId = row.entity.DtlId;
        $scope.frmTest.Product = row.entity;
        $scope.OrderQty = parseInt(row.entity.OrderQty);
        index = $scope.gridOrderDtl.data.indexOf(row.entity);
        $scope.isDisableProduct = true
    };

    var methodName = "";
    $scope.SaveData = function () {

        var OrderDtlData = $scope.gridOrderDtl.data;

        if (OrderDtlData.length <= 0) {
            toastr.warning("Add Product!", { timeOut: 2000 });
            return false;
        }

        $scope.SaveDb = {};

        $scope.SaveDb.MstId = $scope.MstId;
        $scope.SaveDb.CustomerCode = $scope.frmTest.Customer.CustomerCode;
        $scope.SaveDb.OrderStatus = $scope.OrderStatus;

        if ($scope.MstId === "" || typeof $scope.MstId === "undefined") {

            methodName = "InsertOrderEntry";

            $http({
                method: "POST",
                url: MyApp.rootPath + "Test/" + methodName,
                data: { OrderMst: $scope.SaveDb, OrderDtl: OrderDtlData }
            }).then(function (response) {
                if (response.data.Status === "Ok") {
                    $scope.MstId = response.data.Id;
                    $scope.GetOrderDtl();
                    $scope.btnSaveValue = "Update";

                    toastr.success(response.data.Message, { timeOut: 2000 });
                } else {
                    console.log(response);
                    toastr.warning(response.data.Status, { timeOut: 2000 });
                }
            }, function (response) {
                if (response.status === 404) {
                    toastr.warning("Error inserting Data!", { timeOut: 2000 });
                }
            });


        }
        else {

            methodName = "UpdateOrderEntry";

            $http({
                method: "POST",
                url: MyApp.rootPath + "Test/" + methodName,
                data: { OrderMst: $scope.SaveDb, OrderDtl: OrderDtlData }
            }).then(function (response) {
                if (response.data.Status === "Ok") {
                    $scope.GetOrderDtl();
                    $scope.btnSaveValue = "Update";
                    toastr.success(response.data.Message, { timeOut: 2000 });
                } else {
                    console.log(response);
                    toastr.warning(response.data.Status, { timeOut: 2000 });
                }
            }, function (response) {
                if (response.status === 404) {
                    toastr.warning("Error updating Data!", { timeOut: 2000 });
                }
            });

        }


    };


    $scope.ExcuteTestProcedure = function () {
        $scope.SaveDb = {};

        $http({
            method: "POST",
            url: MyApp.rootPath + "Test/ExcuteTestProcedure",
            datatype: "json",
            data: JSON.stringify($scope.SaveDb)
        }).then(function (response) {

                if (response.data.Message == "1") {

                    toastr.success("Data Saved Successfully", { timeOut: 2000 });

                }
                else if (response.data.Message == "2") {

                    toastr.success("Stock not available", { timeOut: 2000 });

                }
                else if (response.data.Message == "3") {

                    toastr.success("Error", { timeOut: 2000 });

                }


        }, function (response) {
            if (response.status === 404) {
                toastr.warning("Error Execute Procedure!", { timeOut: 2000 });
            }
        });

    };

    $scope.SearchData = function () {
        $http({
            method: "GET",
            url: MyApp.rootPath + "Test/GetOrderMst",
            params: { mstId: $scope.MstId }
        }).then(function (response) {
            if (response.data.Status === "" || response.data.Status === null) {
                if (response.data.Data.length > 0) {

                    response.data.Data.forEach(function (row, index) {
                        row.SlNo = index + 1;
                    });
                    $scope.gridOrderSearch.data = response.data.Data;
                    
                    $('#OrderSearchModal').modal('show');
                    
                } else {
                    toastr.warning("No data found!", { timeOut: 2000 });
                }
            } else {
                toastr.warning(response.data.Status, { timeOut: 2000 });
            }
        }, function (response) {
            if (response.status === 404) {
                toastr.warning("Error Loading Order Master Information!", { timeOut: 2000 });
            }
        });
    };



    $scope.GetOrderDtl = function () {
        $scope.gridOrderDtl.data = [];
        $http({
            method: "GET",
            url: MyApp.rootPath + "Test/GetOrderDtl",
            params: { mstId: $scope.MstId}
        }).then(function (response) {
            if (response.data.Status === "" || response.data.Status === null) {
                if (response.data.Data.length > 0) {
                    $scope.gridOrderDtl.data = response.data.Data;
                } else {
                    $scope.gridOrderDtl.data = [];
                    toastr.warning("No data found!", { timeOut: 2000 });
                }
            } else {
                toastr.warning(response.data.Status, { timeOut: 2000 });
            }
        }, function (response) {
            if (response.status === 404) {
                toastr.warning("Error Loading Order Detail Information!", { timeOut: 2000 });
            }
        });
    };








    var columnOrderSearch = [
        { name: 'SlNo', displayName: "Sl. No", aggregationType: uiGridConstants.aggregationTypes.count },
        { name: 'MstId', displayName: "Mst Id"},
        { name: 'CustomerCode',displayName: "Customer Code"},
        { name: 'CustomerName', displayName: "Customer Name" },
        { name: 'OrderStatus', displayName: "OrderStatus" }
    ];
    $scope.gridOrderSearch = {
        exporterMenuCsv: false,
        showColumnFooter: true,
        enableFiltering: true,
        enableSorting: true,
        columnDefs: columnOrderSearch,
        rowTemplate: rowTemplate(),
        enableGridMenu: true,
        enableSelectAll: true,
        onRegisterApi: function (gridApi) {
            $scope.gridApi = gridApi;
        }
    };
    function rowTemplate() {
        return '<div ng-dblclick="grid.appScope.rowDblClick(row)" >' +
            '  <div ng-repeat="(colRenderIndex, col) in colContainer.renderedColumns track by col.colDef.name" class="ui-grid-cell" ng-class="{ \'ui-grid-row-header-cell\': col.isRowHeader }"  ui-grid-cell></div></div>';
    }
    $scope.rowDblClick = function (row) {
        $scope.MstId = row.entity.MstId;
        $scope.frmTest.Customer = row.entity;
        $scope.OrderStatus = row.entity.OrderStatus;
        $scope.isDisableCustomer = true
        $('#OrderSearchModal').modal('hide');
        $scope.GetOrderDtl();
    }























    $scope.Reset = function () {

        $scope.ShowMessage = "";
        $scope.isCheck = false;

        $scope.MstId = "";
        $scope.isDisableProduct = false
        $scope.frmTest.Product = undefined;
        $scope.frmTest.Customer = undefined;
        $scope.OrderQty = "";
        $scope.gridOrderDtl.data = [];

        $scope.errorMeassage = "";


        $scope.MasterId = "";
        $scope.DetailId = "";

    };

});