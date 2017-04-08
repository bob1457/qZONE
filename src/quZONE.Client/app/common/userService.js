(function () {
    'use strict';

    angular
        .module('qzone')
        .factory('userService', userService);

    userService.$inject = ['$http', '$q', 'localStorageService', 'serverBase'];

    function userService($http, $q, localStorageService, serverBase) {

        var serviceBase = serverBase + 'api/accounts/'; //To be finalized for accessing backend api services - for testing purpose
        
        var addUser = function(data) {
            //return $http.post(serviceBase + 'create', data);
        };

        return userService;
    }
})();