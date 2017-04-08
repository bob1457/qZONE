(function () {
    'use strict';

    angular
        .module('ezQ')
        .factory('restaurantService', restaurantService);

    restaurantService.$inject = ['$http', 'serverBase'];

    function restaurantService($http, serverBase) {
        //var service = {
        //    getData: getData
        //};

        //return service;

        //function getData() { }

        var serviceBase = serverBase;

        var restaurants = {};

        restaurants.getAllRestaurants = function () {

            var promise = $http.get(serviceBase + 'api/profile/organizations/');

            return promise;
        };

        restaurants.getRestaurantnById = function (id) {
            var promise = $http.get(serviceBase + 'api/profile/organization/' + id);
            return promise;
        }

        return restaurants;
    }
})();