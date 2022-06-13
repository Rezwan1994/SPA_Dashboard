var applg = angular.module("myApp", []);
applg.directive('loading', ['$http', function ($http) {
    return {
        restrict: 'A',
        link: function (scope, element, attrs) {
            scope.isLoading = function () {
                return $http.pendingRequests.length > 0;
            };
            scope.$watch(scope.isLoading,
                function (value) {
                    if (value) {
                        element.removeClass('ng-hide');
                        //element.parent().addClass('blur');
                        $(".backGround-sales").addClass('blur');
                    } else {
                        element.addClass('ng-hide');
                        $(".backGround-sales").removeClass('blur');
                    }
                });
        }
    };
}]);
applg.directive('autocomplete', function () {

    return {

        restrict: 'A',
        link: function ($scope, el, attr) {

            el.bind('change',
                function (e) {
                    e.preventDefault();
                });
        }
    };

});
applg.controller("myCtrl", function ($scope, $http, $window) {

    $scope.isDisable = false;
    $scope.LoginSystem = function () {
        $scope.isDisable = true;
        if (($scope.Username === "" || $scope.Username === undefined) || ($scope.Password === undefined || $scope.Password === "")) {
            $scope.isDisable = false;
            toastr.error("User name or password can not be blank!", '');
            return;
        } else {
            $scope.SaveDb = {};
            $scope.SaveDb.Username = $scope.Username;
            $scope.SaveDb.Password = $scope.Password;

            $http({
                method: "post",
                url: MyApp.rootPath + "Home/TryLogin",
                datatype: "json",
                data: JSON.stringify($scope.SaveDb)
            }).then(function (response) {
                $scope.isDisable = false;
                if (response.data === "true") {
     
                    $window.location.href = '/Home/Index';
                } else {

                    toastr.error("Invalid User Name Or Password!", '');
                }
            }, function (response) {
                $scope.isDisable = false;
                if (response.status === 404) {
                    toastr.warning("Error Loading Category!", { timeOut: 2000 });
                }
            });
        }

    };
});