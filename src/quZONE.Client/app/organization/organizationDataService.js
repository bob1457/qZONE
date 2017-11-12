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



        //debugger;
        organizationData.getAllTrialRequests = function() {
            var promise = $http.get(serviceBase + 'api/profile/requests/');
            return promise;
        };


        organizationData.getRequestById = function (id) {
            var promise = $http.get(serviceBase + 'api/profile/request/' + id);
            return promise;
        };

        organizationData.getOrgAccount = function(id) {
            var promise = $http.get(serviceBase + 'api/profile/account/' + id);
            return promise;
        }

        organizationData.getOrgWaitList = function (id) {
            var promise = $http.get(serviceBase + 'api/waitlist/list/' + id);
            return promise;
        }

        organizationData.getOrgWaitListByMonthYear = function (id, monthYear) {
            var promise = $http.get(serviceBase + 'api/waitlist/list/' + id + "/" + monthYear); // monthYear format MMYYYY, e.g. 07/2017 and 11/2017
            return promise;
        }


        organizationData.getOrgPayment = function (id) {
            var promise = $http.get(serviceBase + 'api/profile/payments/' + id);
            return promise;
        }

        return organizationData;
    }
})();