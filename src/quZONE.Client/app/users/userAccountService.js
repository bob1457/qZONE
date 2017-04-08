(function () {
    'use strict';

    angular
        .module('qzone')
        .factory('userAccountService', userAccountService);

    userAccountService.$inject = ['$http', 'serverBase'];

    function userAccountService($http, serverBase) {
        //var service = {
        //    getData: getData
        //};

        //return service;

        //function getData() { }

        var serviceBase = serverBase;

        var userAccount = {};

        userAccount.getAllUsers = function () {

            var promise = $http.get(serviceBase + 'api/profile/allusers/');

            return promise;
        };

        userAccount.getUsersByOrg = function (id) {

            var promise = $http.get(serviceBase + 'api/profile/users/' + id);

            return promise;
        };


        return userAccount;
    }
})();