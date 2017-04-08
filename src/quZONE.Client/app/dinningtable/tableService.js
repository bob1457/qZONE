(function () {
    'use strict';

    angular
        .module('qzone')
        .factory('tableService', tableService);

    tableService.$inject = ['$http', 'serverBase'];

    function tableService($http, serverBase) {
        //var service = {
        //    getData: getData
        //};

        //return service;

        //function getData() { }

        var serviceBase = serverBase;

        var tableData = {};

        tableData.getAllTablesForOrg = function (theData) {
            var promise = $http.get(serviceBase + 'api/tablemanager/table/' +theData );
            return promise;
        };


        tableData.getAvailableTablesForOrg = function (theData) {
            var promise = $http.get(serviceBase + 'api/tablemanager/emptytable/' + theData);
            return promise;
        };

        tableData.addNewTable = function (theData) {
            var promise = $http.get(serviceBase + 'api/tablemanager/table/add' + theData);
            return promise;
        };

        tableData.getTableDetails = function(theData) {
            var promise = $http.get(serviceBase + 'api/tablemanager/table/details/' + theData);
            return promise;
        };

        tableData.getGuestDetails = function (theData) {
            var promise = $http.get(serviceBase + 'api/tablemanager/table/details/' + theData);
            return promise;
        };

        return tableData;
    }
})();