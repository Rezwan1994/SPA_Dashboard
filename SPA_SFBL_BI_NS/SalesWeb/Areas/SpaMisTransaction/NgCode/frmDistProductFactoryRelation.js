app.controller("DistProductFactoryRelationCtrl", function ($scope, $http) {

    var index = null;
    $scope.btnSaveValue = "Save";
    $scope.isDisablebtnSaveValue = false;

    $scope.btnAddVaue = "Add Item";
    $scope.isDisableAddItem = false;

    $scope.GetCustomerList = function () {
        $http({
            method: "GET",
            url: MyApp.rootPath + "DistProductFactoryRelation/GetCustomerList"
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
    $scope.GetCustomerList();

    $scope.GetProductList = function () {
        $http({
            method: "GET",
            url: MyApp.rootPath + "DistProductFactoryRelation/GetProductList"
        }).then(function (response) {
            if (response.data.Data.length > 0) {
                $scope.Products = response.data.Data;
            } else {
                toastr.warning("No Data Found!", { timeOut: 2000 });
            }
        }, function (response) {
            if (response.status === 404) {
                toastr.warning("Error Loading Product List!", { timeOut: 2000 });
            }
        });
    }
    $scope.GetProductList();


    $scope.GetFactoryList = function () {
        $http({
            method: "GET",
            url: MyApp.rootPath + "DistProductFactoryRelation/GetFactoryList"
        }).then(function (response) {
            if (response.data.Status === "" || response.data.Status === null) {
                if (response.data.Data.length > 0) {
                    $scope.factories = response.data.Data;
                } else {
                    toastr.warning("No Data Found!", { timeOut: 2000 });
                }
            } else {
                toastr.warning(response.data.Status, { timeOut: 2000 });
            }
        }, function (response) {
            if (response.status === 404) {
                toastr.warning("Error Loading Factory List!", { timeOut: 2000 });
            }
        });
    }
    $scope.GetFactoryList();



    var gridColumn = [
        { name: 'MstId', displayName: "MstId", visible: true },
        { name: 'DtlId', displayName: "DtlId", visible: true },
        { name: 'CustomerCode', displayName: "Customer Code", visible: true },
        { name: 'CustomerName', displayName: "Customer Name", visible: true },
        { name: 'ProductCode', displayName: "Product Code", visible: true },
        { name: 'ProductName', displayName: "Product Name", visible: true },
        { name: 'PackSize', displayName: "PackSize", visible: true },
        { name: 'FactoryCode', displayName: "FactoryCode", visible: true },
        { name: 'FactoryName', displayName: "FactoryName", visible: true },
        {
            name: 'Delete ', enableFiltering: false, enableSorting: false,
            cellTemplate: '<div style="text-align:center;position: relative;width:100%;padding:2px 2px 2px 6px;"><button  class=" btn-danger " ng-click="grid.appScope.DeleteProduct(row)"><i class="fa fa-remove"></i>&nbspDelete</button></div>'
        }
    ];
    $scope.DistProductFactoryRelationGrid = {
        showGridFooter: true,
        enableFiltering: true,
        enableSorting: true,
        enableFiltering: true,
        columnDefs: gridColumn
    };

    $scope.AddItem = function () {

        if (index !== null) {
            $scope.DistProductFactoryRelationGrid.data.splice(index, 1);
        } else {
            for (var i = 0, l = $scope.DistProductFactoryRelationGrid.data.length; i < l; i += 1) {
                if ($scope.DistProductFactoryRelationGrid.data[i].ProductCode == $scope.frmDistProductFactoryRelation.Product.ProductCode) {
                    toastr.warning("Alredy exist!", { timeOut: 2000 });
                    return false;
                }
            }
        }

        $scope.DistProductFactoryRelationGrid.data.push({
            CustomerCode: $scope.frmDistProductFactoryRelation.Customer.CustomerCode,
            CustomerName: $scope.frmDistProductFactoryRelation.Customer.CustomerName,
            ProductCode: $scope.frmDistProductFactoryRelation.Product.ProductCode,
            ProductName: $scope.frmDistProductFactoryRelation.Product.ProductName,
            PackSize: $scope.frmDistProductFactoryRelation.Product.PackSize,
            FactoryCode: $scope.frmDistProductFactoryRelation.Factory.FactoryCode,
            FactoryName: $scope.frmDistProductFactoryRelation.Factory.FactoryName,
        });
        index = null;
        $scope.frmDistProductFactoryRelation.Product = undefined;
        $scope.frmDistProductFactoryRelation.Factory = undefined;
    }

    $scope.DeleteProduct = function (row) {

        if (row.entity.MstId === "" || row.entity.MstId === null || row.entity.MstId === undefined) {
            index = $scope.DistProductFactoryRelationGrid.data.indexOf(row.entity);
            $scope.DistProductFactoryRelationGrid.data.splice(index, 1);
        } else {
            $http({
                method: "POST",
                url: MyApp.rootPath + "DistProductFactoryRelation/DeleteProduct",
                data: { DtlId: row.entity.DtlId, MstId: row.entity.MstId }
            }).then(function (response) {
                if (response.data.Status === "Ok") {
                    //$scope.GetUserProduct();
                    index = $scope.DistProductFactoryRelationGrid.data.indexOf(row.entity);
                    $scope.DistProductFactoryRelationGrid.data.splice(index, 1);

                    toastr.success(response.data.Message, { timeOut: 2000 });
                    index = null;

                } else {
                    console.log(response);
                    toastr.warning(response.data.Status, { timeOut: 2000 });
                }
            }, function (response) {
                if (response.status === 404) {
                    toastr.warning("Error Deleting Data!", { timeOut: 2000 });
                }
            });
        }
    };

    $scope.DeleteMstDtl = function (row) {

        if ($scope.MstId === "" || typeof $scope.MstId === "undefined") {

            toastr.warning("Data not fond!", { timeOut: 2000 });

        } else {

            $http({
                method: "POST",
                url: MyApp.rootPath + "DistProductFactoryRelation/DeleteMstDtl",
                params: { MstId: $scope.MstId }
            }).then(function (response) {
                if (response.data.Status === "Ok") {
                    $scope.Reset();
                    toastr.success(response.data.Message, { timeOut: 2000 });
                } else {
                    toastr.warning(response.data.Status, { timeOut: 2000 });
                }
            }, function (response) {
                if (response.status === 404) {
                    toastr.warning("Error Deleting Data!", { timeOut: 2000 });
                }
            });

        }


    };


    var methodName = "";
    $scope.SaveData = function () {

        var gridData = $scope.DistProductFactoryRelationGrid.data;

        if (gridData.length <= 0) {
            toastr.warning("Please enter data properly!");
            return false;
        }

        $scope.SaveDb = {};
        $scope.SaveDb.MstId = $scope.MstId;
        $scope.SaveDb.CustomerCode = $scope.frmDistProductFactoryRelation.Customer.CustomerCode;

        if ($scope.MstId === "" || typeof $scope.MstId === "undefined") {

            $http({
                method: "POST",
                url: MyApp.rootPath + "DistProductFactoryRelation/InsertData",
                data: { mstData: $scope.SaveDb, dtlData: gridData }
            }).then(function (response) {
                if (response.data.Status === "Ok") {
                    $scope.MstId = response.data.Id;
                    $scope.GetSearchProduct();
                    $scope.isDisablebtnSaveValue = true;
                    //$scope.btnSaveValue = "Update";
                    toastr.success(response.data.Message, { timeOut: 2000 });
                } else {
                    toastr.warning(response.data.Status, { timeOut: 2000 });
                }
            }, function (response) {
                if (response.status === 404) {
                    toastr.warning("Error inserting Data!", { timeOut: 2000 });
                }
            });


        }
        else {

            $http({
                method: "POST",
                url: MyApp.rootPath + "DistProductFactoryRelation/UpdateData",
                data: { mstData: $scope.SaveDb, dtlData: gridData }
            }).then(function (response) {
                if (response.data.Status === "Ok") {                    
                    $scope.GetSearchProduct();
                    $scope.isDisablebtnSaveValue = true;
                    //$scope.btnSaveValue = "Update";
                    toastr.success(response.data.Message, { timeOut: 2000 });
                } else {
                    //console.log(response);
                    //toastr.warning(response.data.Status, { timeOut: 2000 });
                    toastr.warning("No changes to save", { timeOut: 2000 });
                }
            }, function (response) {
                if (response.status === 404) {
                    toastr.warning("Error updating Data!", { timeOut: 2000 });
                }
            });

        }


    };


    $scope.GetSearchProduct = function () {

        $http({
            method: "GET",
            url: MyApp.rootPath + "DistProductFactoryRelation/GetSearchProduct",
            params: { param: " AND MST_ID = " + $scope.MstId + " " }
        }).then(function (response) {
            if (response.data.Data.length > 0) {
                $scope.DistProductFactoryRelationGrid.data = response.data.Data;
            } else {
                $scope.DistProductFactoryRelationGrid.data = [];
                //toastr.warning("No Data Found!", { timeOut: 2000 });
            }
        }, function (response) {
            if (response.status === 404) {
                toastr.warning("Error Loading data!", { timeOut: 2000 });
            }
        });
    }



    $scope.SearchMstData = function () {

        $http({
            method: "GET",
            url: MyApp.rootPath + "DistProductFactoryRelation/SearchMstData"
        }).then(function (response) {
            if (response.data.Data.length > 0) {
                $scope.gridSearch.data = response.data.Data;
                $('#SearchModal').modal('show');
            } else {
                toastr.warning("No Data Found!", { timeOut: 2000 });
            }
        }, function (response) {
            if (response.status === 404) {
                toastr.warning("Error Loading User List!", { timeOut: 2000 });
            }
        });

    }

    var gridSearchColumn = [
        { name: 'MstId', displayName: "MstId", visible: true },
        { name: 'CustomerCode', displayName: "Customer Code", visible: true },
        { name: 'CustomerName', displayName: "Customer Name", visible: true }
    ];
    $scope.gridSearch = {
        showGridFooter: true,
        enableFiltering: true,
        enableSorting: true,
        enableFiltering: true,
        columnDefs: gridSearchColumn,
        rowTemplate: rowTemplate()
    };
    function rowTemplate() {
        return '<div ng-dblclick="grid.appScope.rowDblClick(row)" >' +
            '  <div ng-repeat="(colRenderIndex, col) in colContainer.renderedColumns track by col.colDef.name" class="ui-grid-cell" ng-class="{ \'ui-grid-row-header-cell\': col.isRowHeader }"  ui-grid-cell></div></div>';
    }
    $scope.rowDblClick = function (row) {
        $scope.MstId = row.entity.MstId;
        $scope.frmDistProductFactoryRelation.Customer = row.entity;
        $scope.GetSearchProduct();
        $('#SearchModal').modal('hide');
    }



    $scope.Reset = function () {

        $scope.MstId = "";
        $scope.btnSaveValue = "Save";
        $scope.isDisablebtnSaveValue = false;
        $scope.frmDistProductFactoryRelation.Customer = undefined;
        $scope.frmDistProductFactoryRelation.Product = undefined;
        $scope.frmDistProductFactoryRelation.Factory = undefined;
        $scope.DistProductFactoryRelationGrid.data = [];
    };

});