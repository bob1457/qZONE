(function () {
    'use strict';

    angular
        .module('qzone')
        .factory('organizationDataService', organizationDataService);

    organizationDataService.$inject = ['$http', 'serverBase'];

    function organizationDataService($http, serverBase) {
        //var service = {
        //    getData: getData
        //};

        //return service;

        //function getData() { }

        var serviceBase = serverBase;

        var organizationData = {};

        organizationData.getAllOrganizations = function() {
            var promise = $http.get(serviceBase + 'api/profile/organizations/');
            return promise;
        };


        organizationData.getAllOrganizationById = function (id) {
            var promise = $http.get(serviceBase + 'api/profile/organization/' + id);
            return promise;
        };


        organizationData.getAllTrialRequests = function() {
            var promise = $http.get(serviceBase + 'api/profile/organizations/');
            return promise;
        };


        return organizationData;
    }
})();