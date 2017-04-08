(function () {
    'use strict';

    angular
        .module('qzone')
        .factory('checkService', checkService);

    checkService.$inject = ['authService','$location'];

    function checkService(authService, $location) {
        //var service = {
        //    getData: getData
        //};

        //return service;

        //function getData() { }
        var result = {};

            this.access = false;

            result = authService.authentication;

            return result;

    }
})();