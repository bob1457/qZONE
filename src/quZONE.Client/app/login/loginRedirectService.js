(function () {
    'use strict';

    angular
        .module('qzone')
        .factory('loginRedirectService', loginRedirectService);

    loginRedirectService.$inject = ['$q', '$location'];

    function loginRedirectService($q, $location) {
        //var service = {
        //    getData: getData
        //};

        //return service;

        //function getData() { }

        var lastPath = "/";

        var responseError = function(response) {
            if (response.status == 401) {
                lastPath = $location.path();
                $location.path = "/";
            }
        };

        return {
            responseError: responseError,
            redidrectPostLogin: loginRedirectService
        };
    }
})();