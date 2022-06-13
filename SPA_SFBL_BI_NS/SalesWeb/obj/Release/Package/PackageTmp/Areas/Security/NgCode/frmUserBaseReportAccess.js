app.controller("UserBaseReportAccessCtrl", function ($scope, $http, uiGridConstants) {

    $scope.isDisbleUser = false;
    $scope.isDisbleProductType = false;

    $scope.isDisableAddItem = false;
    $scope.btnSaveValue = "Save";
    $scope.btnAddVaue = "Add Item";
    $scope.ProductCategory = "Base Product List";
    var index = null;

    

    $scope.GetUserList = function () {
        $http({
            method: "GET",
            url: MyApp.rootPath + "UserBaseReportAccess/GetUserList",
        }).then(function (response) {
            if (response.data.Data.length > 0) {
                $scope.users = response.data.Data;
            } else {
                toastr.warning("No Data Found!", { timeOut: 2000 });
            }
        }, function (response) {
            if (response.status === 404) {
                toastr.warning("Error Loading User List!", { timeOut: 2000 });
            }
        });
    }
    $scope.GetUserList();

    $scope.OnUserClick = function () {

        $scope.ProductType = "";
        $scope.frmUserBaseReportAccess.TypeList = undefined;
        $scope.TypeLists = [];

    }

    $scope.OnProductTypeClick = function () {

        $scope.frmUserBaseReportAccess.TypeList = undefined;
        $scope.TypeLists = [];


        if ($scope.ProductType === "BASE_PRODUCT") {
            $scope.ProductCategory = "Base Product List";
        }
        else if ($scope.ProductType === "BRAND") {
            $scope.ProductCategory = "Brand List";
        }
        else if ($scope.ProductType === "CATEGORY") {
            $scope.ProductCategory = "Category List";
        }

        $http({
            method: "GET",
            url: MyApp.rootPath + "UserBaseReportAccess/GetTypeList",
            params: { pType: $scope.ProductType}
        }).then(function (response) {
            if (response.data.Data.length > 0) {
                $scope.TypeLists = response.data.Data;
            } else {
                toastr.warning("No Data Found!", { timeOut: 2000 });
            }

        }, function (response) {
            if (response.status === 404) {
                toastr.warning("Error Loading Type List!", { timeOut: 2000 });
            }
        });

    }

    $scope.AddItem = function () {

        if (index !== null) {
            $scope.gridProductType.data.splice(index, 1);
        } else {
            for (var i = 0, l = $scope.gridProductType.data.length; i < l; i += 1) {
                if ($scope.gridProductType.data[i].ProductTypeCode == $scope.frmUserBaseReportAccess.TypeList.Code) {
                    toastr.warning("Alredy exist!", { timeOut: 2000 });
                    return false;
                }
            }
        }

        $scope.gridProductType.data.push({
            ProductTypeCode: $scope.frmUserBaseReportAccess.TypeList.Code,
            ProductTypeName: $scope.frmUserBaseReportAccess.TypeList.Name
        });
        index = null;
        $scope.frmUserBaseReportAccess.TypeList = undefined;
    }

    var columnProductType = [
        { name: 'ProductTypeId', displayName: "ProductTypeId", visible: false },

        { name: 'ProductTypeId', displayName: "ProductTypeId", visible: false },        
        { name: 'MstId', displayName: "MstId", visible: false},        
        { name: 'UserId', displayName: "UserId", visible: false},
        { name: 'ProductTypeCode', displayName: "Product Type Code" },
        { name: 'ProductTypeName', displayName: "Product Type Nmae" },
        { name: 'TypeName', displayName: "Type Name", visible: false },
        {
            name: 'Delete ', enableFiltering: false, enableSorting: false,
            cellTemplate: '<div style="text-align:center;position: relative;width:100%;padding:2px 2px 2px 6px;"><button  class=" btn-danger " ng-click="grid.appScope.DeleteProductType(row)"><i class="fa fa-remove"></i>&nbspDelete</button></div>'
        }
    ];
    $scope.gridProductType = {
        showGridFooter: true,
        //enableFiltering: true,
        enableSorting: true,
        columnDefs: columnProductType,
        enableSelectAll: true,
        enableRowSelection: true,
        onRegisterApi: function (gridApi) {
            $scope.gridProductTypeApi = gridApi;
        }
    };



    var methodName = "";
    $scope.SaveData = function () {

        var ProductTypeData = $scope.gridProductType.data;

        if (ProductTypeData.length <= 0) {
            toastr.warning("Please enter order product Type properly!");
            return false;
        }

        $scope.SaveDb = {};

        $scope.SaveDb.MstId = $scope.MstId;
        $scope.SaveDb.UserId = $scope.frmUserBaseReportAccess.User.UserId;
        $scope.SaveDb.TypeName = $scope.ProductType;


        if ($scope.MstId === "" || typeof $scope.MstId === "undefined") {

            $http({
                method: "POST",
                url: MyApp.rootPath + "UserBaseReportAccess/InsertUserBaseReportAccess",
                data: { mstData: $scope.SaveDb, productTypeData: ProductTypeData }
            }).then(function (response) {
                if (response.data.Status === "Ok") {
                    $scope.MstId = response.data.Id;
                    $scope.GetUserProductType();
                    $scope.GetUserProduct();
                    $scope.btnSaveValue = "Update";
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
                url: MyApp.rootPath + "UserBaseReportAccess/UpdateUserBaseReportAccess",
                data: { mstData: $scope.SaveDb, productTypeData: ProductTypeData }
            }).then(function (response) {
                if (response.data.Status === "Ok") {
                    $scope.GetUserProductType();
                    $scope.GetUserProduct();
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



    var columnSearch = [
        { name: 'MstId', displayName: "MstId", visible: false },
        { name: 'UserId', displayName: "UserId", visible: false },
        { name: 'UserName', displayName: "User Name", visible: true },
        { name: 'EmployeeCode', displayName: "Employee Code", visible: true },
        { name: 'EmployeeName', displayName: "Employee Name", visible: true },
        { name: 'TypeName', displayName: "Category", visible: true }
    ];
    $scope.gridSearch = {
        showGridFooter: true,
        //enableFiltering: true,
        enableSorting: true,
        enableFiltering: true,
        columnDefs: columnSearch,
        rowTemplate: rowTemplate()
    };
    function rowTemplate() {
        return '<div ng-dblclick="grid.appScope.rowDblClick(row)" >' +
            '  <div ng-repeat="(colRenderIndex, col) in colContainer.renderedColumns track by col.colDef.name" class="ui-grid-cell" ng-class="{ \'ui-grid-row-header-cell\': col.isRowHeader }"  ui-grid-cell></div></div>';
    }
    $scope.rowDblClick = function (row) {
        $scope.MstId = row.entity.MstId;
        $scope.frmUserBaseReportAccess.User = row.entity;
        $scope.ProductType = row.entity.TypeName;

        $scope.OnProductTypeClick();
        $scope.GetUserProductType();
        $scope.GetUserProduct();

        $('#SearchModal').modal('hide');
        $scope.isDisbleUser = true;
        $scope.isDisbleProductType = true;
    }

    $scope.SearchData = function () {

        $http({
            method: "GET",
            url: MyApp.rootPath + "UserBaseReportAccess/SearchData",
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

    $scope.GetUserProductType = function () {

        $http({
            method: "GET",
            url: MyApp.rootPath + "UserBaseReportAccess/GetUserProductType",
            params: { param: " AND USER_ID = " + $scope.frmUserBaseReportAccess.User.UserId+ " "}
        }).then(function (response) {
            if (response.data.Data.length > 0) {
                $scope.gridProductType.data = response.data.Data;                
            } else {
                toastr.warning("No Data Found!", { timeOut: 2000 });
            }
        }, function (response) {
            if (response.status === 404) {
                toastr.warning("Error Loading User List!", { timeOut: 2000 });
            }
        });

    }




    var columnProduct = [
        { name: 'ProductDtlId', displayName: "Product Dt lId", visible: false },
        { name: 'ProductTypeId', displayName: "Product Type Id", visible: false },
        { name: 'UserId', displayName: "UserId", visible: false },

        { name: 'ProductTypeCode', displayName: "Product Type Code", visible: false },
        { name: 'ProductTypeName', displayName: "Product Type Name", visible: true },
        { name: 'TypeName', displayName: "Type Name", visible: false },

        { name: 'ProductCode', displayName: "Product Code" },
        { name: 'ProductName', displayName: "Product Name" },
        { name: 'PackSize', displayName: "Pack Size" },

        {
            name: 'Delete ', enableFiltering: false, enableSorting: false,
            cellTemplate: '<div style="text-align:center;position: relative;width:100%;padding:2px 2px 2px 6px;"><button  class=" btn-danger " ng-click="grid.appScope.DeleteProduct(row)"><i class="fa fa-remove"></i>&nbspDelete</button></div>'
        }


    ];
    $scope.gridProduct = {
        showGridFooter: true,
        //enableFiltering: true,
        enableSorting: true,
        columnDefs: columnProduct
    };


    $scope.GetUserProduct = function () {

        $http({
            method: "GET",
            url: MyApp.rootPath + "UserBaseReportAccess/GetUserProduct",
            params: { param: " AND USER_ID = " + $scope.frmUserBaseReportAccess.User.UserId + " " }
        }).then(function (response) {
            if (response.data.Data.length > 0) {
                $scope.gridProduct.data = response.data.Data;
            } else {
                $scope.gridProduct.data = [];
                //toastr.warning("No Data Found!", { timeOut: 2000 });
            }
        }, function (response) {
            if (response.status === 404) {
                toastr.warning("Error Loading User List!", { timeOut: 2000 });
            }
        });
    }

    $scope.DeleteProduct = function (row) {

        if (row.entity.ProductDtlId === "" || row.entity.ProductDtlId === null || row.entity.ProductDtlId === undefined) {
            index = $scope.gridProduct.data.indexOf(row.entity);
            $scope.gridProduct.data.splice(index, 1);
        } else {
            $http({
                method: "POST",
                url: MyApp.rootPath + "UserBaseReportAccess/DeleteProduct",
                data: { productDtlId: row.entity.ProductDtlId }
            }).then(function (response) {
                if (response.data.Status === "Ok") {
                    //$scope.GetUserProduct();
                    index = $scope.gridProduct.data.indexOf(row.entity);
                    $scope.gridProduct.data.splice(index, 1);

                    toastr.success(response.data.Message, { timeOut: 2000 });
                    index = null;

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



    $scope.DeleteProductType = function (row) {

        if (row.entity.ProductTypeId === "" || row.entity.ProductTypeId === null || row.entity.ProductTypeId === undefined) {
            index = $scope.gridProductType.data.indexOf(row.entity);
            $scope.gridProductType.data.splice(index, 1);
        } else {
            $http({
                method: "POST",
                url: MyApp.rootPath + "UserBaseReportAccess/DeleteProductType",
                data: { typeId: row.entity.ProductTypeId }
            }).then(function (response) {
                if (response.data.Status === "Ok") {
                    $scope.GetUserProduct();
                    index = $scope.gridProductType.data.indexOf(row.entity);
                    $scope.gridProductType.data.splice(index, 1);
                    
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

    $scope.DeleteUserAccess = function (row) {

        $http({
            method: "POST",
            url: MyApp.rootPath + "UserBaseReportAccess/DeleteUserAccess",
            params: { UserID: $scope.frmUserBaseReportAccess.User.UserId}
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
    };


    $scope.Reset = function () {

        $scope.isDisbleUser = false;
        $scope.isDisbleProductType = false;
        $scope.btnSaveValue = "Save";
        $scope.isDisableAddItem = false;
        $scope.ProductType = "";
        $scope.MstId = "";
        $scope.frmUserBaseReportAccess.TypeList = undefined;
        $scope.frmUserBaseReportAccess.User = undefined;
        $scope.gridProductType.data = [];
        $scope.gridProduct.data = [];
    }

});