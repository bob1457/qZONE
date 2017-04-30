(function () {
    'use strict';

    angular
        .module('app')
        .service('adminService', adminService);

    adminService.$inject = ['$http', 'authService', 'serverBase'];

    function adminService($http, authService, serverBase) {

        var serviceBase = serverBase;

        var _getWaitList = function (data) {

            var result = $http.get(serviceBase + 'api/waitlist/alllist/' + data).success(function (response) {

                //console.log($scope.$parent.authentication.isAdmin);
                //$scope.lists = response;
                //console.log($scope.lists);
                console.log(response);
                return response;
            });

            return result;
        };



        this.getData = getData;

        function getData() { }
    }
})();