var app = angular.module("myApp", ['ngTouch', 'ui.grid.selection', 'ui.grid.autoResize', 'ui.grid', 'ui.grid.cellNav', 'ui.grid.edit', 'ui.grid.rowEdit', 'ui.grid.grouping', 'ui.grid.pinning', 'ui.grid.exporter', 'ui.grid.resizeColumns', 'ui.grid.exporter', 'ui.grid.pagination', 'ui.grid.moveColumns', 'ngSanitize', 'ui.grid.pagination', 'ui.select', 'ui.grid.pinning']);

app.run(function ($rootScope, $http, $window, $sce) {
    //var pathname = window.location.pathname; // Returns path only
    //var url = window.location.href;
    //alert(pathname);
    //alert(url);
    $rootScope.ViewPerm = "";
    $rootScope.SearchPerm = "";
    $rootScope.FormTitle = "";
    $rootScope.MenuName = "";
    $rootScope.NumberPattern = "/^[0-9]+(\.[0-9]{1,2})?$/";
    $rootScope.NumberPatternProdPrice = "/^[0-9]+(\.[0-9]{1,4})?$/";
    //$rootScope.NumberPattern = "/^[0-9]*(\.{1})?([0-91-9][1-9])?$/";
    $rootScope.EmailPattern = "/^[a-z]+[a-z0-9._]+@[a-z]+\.[a-z.]{2,5}$/";

    var url = window.location.pathname;
    $rootScope.EventPerm = function () {
        $http({
            method: "Post",
            url: MyApp.rootPath + "Home/EventPermission",
            datatype: "json",
            data: { url: url }
        }).then(function (res) {
            if (res.data.Data.length !== 0) {
                $rootScope.ViewPerm = res.data.Data[0].Sv;
                $rootScope.SearchPerm = res.data.Data[0].Dl;
                $rootScope.FormTitle = res.data.Data[0].DisplayName;
                $rootScope.MenuName = res.data.Data[0].MenuName;
            } else {
                $window.location.href = '/Home/Index';
            }
        });
    };
    if (url !== "/Security/ChangPass/frmChngPass") {
        $rootScope.EventPerm();
    }
    $rootScope.ReportRadioList = "";
    $rootScope.GetReportName = function () {
        $http({
            method: "Post",
            url: MyApp.rootPath + "Home/GetReportName",
            datatype: "json",
            data: { url: url }
        }).then(function (response) {

            //if (response.data.Status === "" || response.data.Status === null) {
            //    if (response.data.ReportNameStr.length > 0) {
            $rootScope.ReportRadioList = response.data.ReportRadioList;
            //var v = $rootScope.ReportRadioList[0].ReportName;
            //var a = $("input[name=ReportName]").val();
            //$("input[name=ReportName][value=" + $rootScope.ReportRadioList[0].ReportName + "]").prop('checked', true);
            //$("input[name=ReportName]").val([$rootScope.ReportRadioList[0].ReportName]);
            //$rootScope.ReportRadioStr = $sce.trustAsHtml(response.data);
            //    }
            //} else {
            //    toastr.warning(response.data.Status, { timeOut: 2000 });
            //}
        }, function (response) {
            if (response.status === 404) {
                toastr.warning("Error Loading Category!", { timeOut: 2000 });
            }
        });
    };



});
app.filter('propsFilter', function () {
    return function (items, props) {
        var out = [];

        if (angular.isArray(items)) {
            items.forEach(function (item) {
                var itemMatches = false;

                var keys = Object.keys(props);
                for (var i = 0; i < keys.length; i++) {
                    var prop = keys[i];
                    var text = props[prop].toLowerCase();
                    if (item[prop].toString().toLowerCase().indexOf(text) !== -1) {
                    //if (item[prop].indexOf(text) !== -1) {
                        itemMatches = true;
                        break;
                    }
                }
                if (itemMatches) {
                    out.push(item);
                }
            });
        } else {
            // Let the output be the input untouched
            out = items;
        }
        return out;
    };
});
app.filter('FullDateTime', function () {
    return function (value) {
        if (value === "ALL") { return value; }
        if (!value) { return ''; }

        var dt = new Date(parseInt(value.substr(6)));
        var month = ("0" + (dt.getMonth() + 1)).slice(-2);
        var day = ("0" + dt.getDate()).slice(-2);
        var year = dt.getFullYear();
        var hours = dt.getHours();
        var minutes = dt.getMinutes();
        var ampm = hours >= 12 ? 'PM' : 'AM';
        hours = hours % 12;
        hours = hours ? hours : 12; // the hour '0' should be '12'
        minutes = minutes < 10 ? '0' + minutes : minutes;
        var dtpDepEfct = day + '/' + month + '/' + dt.getFullYear();
        return dtpDepEfct;
    }
});
app.factory('Session', function ($http) {
    var Session = {
        data: {},
        saveSession: function () { /* save session data to db */ },
        updateSession: function () {
            /* load data from db */
            $http.get('session.json')
                .then(function (r) { return Session.data = r.data; });
        }
    };
    Session.updateSession();
    return Session;
});
app.directive('loading', ['$http', function ($http) {
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
                        $(".form-horizontal").addClass('blur');
                    } else {
                        element.addClass('ng-hide');
                        $(".form-horizontal").removeClass('blur');
                    }
                });
        }
    };
}]);
app.directive('tooltip', function () {
    return {
        restrict: 'A',
        link: function (scope, element, attrs) {
            element.hover(function () {
                // on mouseenter
                element.tooltip('show');
            }, function () {
                // on mouseleave
                element.tooltip('hide');
            });
        }
    };
});
app.filter('fractionFilter', function () {
    return function (value) {
        return value.toFixed(0);
    };
});
app.config(['$httpProvider', function ($httpProvider) {
    $httpProvider.defaults.headers.common["X-Requested-With"] = 'XMLHttpRequest';
    $httpProvider.interceptors.push(function ($q) {
        return {
            'responseError': function (rejection) {
                // do something on error
                //if (canRecover(rejection)) {
                //    return responseOrNewPromise
                //}
                if (rejection.status === 501) {
                    window.open(
                        '/Home/LogIn?param=SessionOut',
                        '_blank' // <- This is what makes it open in a new window.
                    );
                    toastr.error("Your session is out!", { timeOut: 20000 });
                }
                return $q.reject(rejection);
            }
        };
    });
    //$httpProvider.interceptors.push(function ($q) {
    //    return {
    //        'request': function (config) {
    //            config.url = config.url + '?id=123';
    //            return   $q.when(config);


    //        }

    //    }
    //});
}]);
app.directive('uiSelectChoices', ['$timeout', '$parse', '$compile', '$document', '$filter', function ($timeout, $parse, $compile, $document, $filter) {
    return function (scope, elm, attr) {
        var raw = elm[0];
        var scrollCompleted = true;
        if (!attr.allChoices) {
            throw new Error('ief:ui-select: Attribute all-choices is required in  ui-select-choices so that we can handle  pagination.');
        }

        scope.pagingOptions = {
            allOptions: scope.$eval(attr.allChoices)
        };

        attr.refresh = 'addMoreItems()';
        var refreshCallBack = $parse(attr.refresh);
        elm.bind('scroll', function (event) {
            var remainingHeight = raw.offsetHeight - raw.scrollHeight;
            var scrollTop = raw.scrollTop;
            var percent = Math.abs((scrollTop / remainingHeight) * 100);

            if (percent >= 80) {
                if (scrollCompleted) {
                    scrollCompleted = false;
                    event.preventDefault();
                    event.stopPropagation();
                    var callback = function () {
                        scope.addingMore = true;
                        refreshCallBack(scope, {
                            $event: event
                        });
                        scrollCompleted = true;

                    };
                    $timeout(callback, 100);
                }
            }
        });

        var closeDestroyer = scope.$on('uis:close', function () {
            var pagingOptions = scope.$select.pagingOptions || {};
            pagingOptions.filteredItems = undefined;
            pagingOptions.page = 0;
        });

        scope.addMoreItems = function (doneCalBack) {
            console.log('new addMoreItems');
            var $select = scope.$select;
            var allItems = scope.pagingOptions.allOptions;
            var moreItems = [];
            var itemsThreshold = 100;
            var search = $select.search;

            var pagingOptions = $select.pagingOptions = $select.pagingOptions || {
                page: 0,
                pageSize: 20,
                items: $select.items
            };

            if (pagingOptions.page === 0) {
                pagingOptions.items.length = 0;
            }
            if (!pagingOptions.originalAllItems) {
                pagingOptions.originalAllItems = scope.pagingOptions.allOptions;
            }
            console.log('search term=' + search);
            console.log('prev search term=' + pagingOptions.prevSearch);
            var searchDidNotChange = search && pagingOptions.prevSearch && search === pagingOptions.prevSearch;
            console.log('isSearchChanged=' + searchDidNotChange);
            if (pagingOptions.filteredItems && searchDidNotChange) {
                allItems = pagingOptions.filteredItems;
            }
            pagingOptions.prevSearch = search;
            if (search && search.length > 0 && pagingOptions.items.length < allItems.length && !searchDidNotChange) {
                //search


                if (!pagingOptions.filteredItems) {
                    //console.log('previous ' + pagingOptions.filteredItems);
                }

                pagingOptions.filteredItems = undefined;
                moreItems = $filter('filter')(pagingOptions.originalAllItems, search);
                //if filtered items are too many scrolling should occur for filtered items
                if (moreItems.length > itemsThreshold) {
                    if (!pagingOptions.filteredItems) {
                        pagingOptions.page = 0;
                        pagingOptions.items.length = 0;
                    } else {
                        pagingOptions.page = 0;
                        pagingOptions.items.length = 0;
                        allItems = pagingOptions.filteredItems = moreItems;
                    }

                } else {
                    allItems = moreItems;
                    pagingOptions.items.length = 0;
                    pagingOptions.filteredItems = undefined;
                }


            } else {
                console.log('plain paging');
            }
            pagingOptions.page++;
            if (pagingOptions.page * pagingOptions.pageSize < allItems.length) {
                moreItems = allItems.slice(pagingOptions.items.length, pagingOptions.page * pagingOptions.pageSize);
            }

            for (var k = 0; k < moreItems.length; k++) {
                pagingOptions.items.push(moreItems[k]);
            }

            scope.calculateDropdownPos();
            scope.$broadcast('uis:refresh');
            if (doneCalBack) doneCalBack();
        };
        scope.$on('$destroy', function () {
            elm.off('scroll');
            closeDestroyer();
        });
    };
}]);
app.filter('sumOfValue', function () {
    return function (data, key) {
        //debugger;
        if (angular.isUndefined(data) || angular.isUndefined(key))
            return 0;
        var sum = 0;

        angular.forEach(data, function (v, k) {
            var a = v[key];
            if (a === "") {
                a = 0;
            }
            sum = sum + parseFloat(a);
        });
        return sum.toFixed(2);
    };
});
function OperationMsg(mode) {

    if (mode === "I") {
        toastr.success("Saved Successfully!", '');
    }
    else if (mode === "U") {
        toastr.success('Updated Successfully!', '');
    }
    else if (mode === "No") {
        toastr.error('Not Saved!', '');
    }
    else if (mode === "D") {
        toastr.success('Deleted Successfully!', '');
    }
    else if (mode === "NoDel") {
        toastr.error('Not Deleted!', '');
    }
    else if (mode === "Unique") {
        toastr.error("Data Exists!", '');

    }
    else if (mode === "C") {
        toastr.success("Checked Successfully!", '');
    }
    else if (mode === "A") {
        toastr.success("Approved Successfully!", '');
    }
}
function CompareDate(fromDate, toDate, checkingMode) {
    //var fDate = new Date(fromDate);
    // var tDate = new Date(toDate);


    var startDate = new Date(fromDate.split("/").reverse().join("-"));
    startDate = new Date(startDate.getFullYear(), startDate.getMonth(), startDate.getDate());

    var sDate = (startDate.getMonth() + 1) + '/' + startDate.getDate() + '/' + startDate.getFullYear();
    var endDate = new Date(toDate.split("/").reverse().join("-"));
    endDate = new Date(endDate.getFullYear(), endDate.getMonth(), endDate.getDate());
    var eDate = (endDate.getMonth() + 1) + '/' + endDate.getDate() + '/' + endDate.getFullYear();
    var todayDate = new Date(new Date().getFullYear(), new Date().getMonth(), new Date().getDate());
    var tDate = (todayDate.getMonth() + 1) + '/' + todayDate.getDate() + '/' + todayDate.getFullYear();

    if (checkingMode === "greater") {
        if (startDate > endDate) {
            toastr.warning("Start Date Cannot be Greater Than End Date !");
            return false;
        }
        if (endDate > todayDate) {
            toastr.warning("End Date Cannot be Greater Than Present Date !");
            return false;
        }
    } if (checkingMode === "onlygreater") {
        if (startDate > endDate) {
            toastr.warning("Start Date Cannot be Greater Than End Date !");
            return false;
        }

    }
    if (checkingMode === "less") {
        if (startDate > endDate) {
            toastr.warning("Start Date Cannot be Greater Than End Date !");
            return false;
        }
        if (endDate > todayDate) {
            toastr.warning("End Date Cannot be Greater Than Present Date !");
            return false;
        }
    }


    return true;
}
function DateCheck(fromDate, toDate) {

    var todayDate = new Date(new Date().getFullYear(), new Date().getMonth(), new Date().getDate());
    var startDate = fromDate.split("/");
    var convertedStartDate = new Date(+startDate[2], startDate[1] - 1, +startDate[0]);
    var sDate = new Date(convertedStartDate.getFullYear(), convertedStartDate.getMonth(), convertedStartDate.getDate());
    if (toDate === 'null') {
        if (sDate > todayDate) {
            toastr.warning("Date  Cannot be Greater Than Current Date !");
            return false;
        }
        return true;
    }

    var endDate = toDate.split("/");
    var convertedEndDate = new Date(+endDate[2], endDate[1] - 1, +endDate[0]);
    var eDate = new Date(convertedEndDate.getFullYear(), convertedEndDate.getMonth(), convertedEndDate.getDate());
    if (parseInt(endDate[1], 10) !== parseInt(startDate[1], 10)) {
        toastr.warning("You cannot select different month!");
        return false;
    }


    var sMonth = new Date(convertedStartDate.getMonth());
    var eMonth = new Date(convertedEndDate.getMonth());

    if (eDate >= todayDate) {
        toastr.warning("To Date  less than Current Date !");
        return false;
    }

    if (sDate > eDate) {
        toastr.warning("From Date  Cannot be Greater Than To Date !");
        return false;
    }
    return true;
}
