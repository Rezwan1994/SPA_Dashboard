app.controller("myCtrl", function ($scope, $http) {
    $scope.btnSaveValue = "Save";
    $scope.passwordDisabled = true;
    $scope.CrntPassChq = false;
    $scope.validateCurrentPassword = false;
    $scope.passwordValidation = true;
    var chkCurrentPassword;
    var userId;
    $scope.GetCurrentPassword = function () {
        $http({
            method: "GET",
            url: MyApp.rootPath + "ChangPass/GetCurrentPassword"
        }).then(function (response) {
            if (response.data !== "") {
               // $scope.CurrentPassword = response.data;
                chkCurrentPassword = response.data.Password; 
                userId = response.data.UserId;
               // console.log("pass: " + chkCurrentPassword);
            } else {
                toastr.warning("No Data Found!");
            }
        }, function () {
            toastr.warning("Error Occurred!");
        });
    };
    $scope.GetCurrentPassword();

    $scope.CheckCurrentPassword = function () {
        if ($scope.CurrentPassword === chkCurrentPassword) {
            $scope.passwordDisabled = false;
            $scope.CrntPassChq = false;
            $scope.validateCurrentPassword = true;
        }
        else {
            $scope.passwordDisabled = true;
            $scope.CrntPassChq = true;
            $scope.validateCurrentPassword = false;
            $scope.passwordValidation = true;
            $scope.Password = "";
            $scope.RePassword = "";
        }
    };

    $scope.CheckPasswordValidity = function () {
        if ($scope.Password === $scope.RePassword) {
            $scope.passwordValidation = false;
        } else {
            $scope.passwordValidation = true;
        }
    };
    $scope.SaveData = function () {
        $http({
            method: "post",
            url: MyApp.rootPath + "ChangPass/UpdatePassword",
            datatype: "json",
            data: { Password: $scope.Password, UserId: userId }
        }).then(function (response) {
            if (response.data.Status === "Ok") {
                toastr.success(response.data.Message);
                $scope.Reset();
            } else {
                toastr.error("Failed!");
            }
        });
    };
    //reset
    $scope.Reset = function () {
        $scope.passwordDisabled = true;
        $scope.CrntPassChq = false;
        $scope.validateCurrentPassword = false;
        $scope.passwordValidation = true;
        $scope.CurrentPassword = "";
        $scope.Password = "";
        $scope.RePassword = "";
    };
});