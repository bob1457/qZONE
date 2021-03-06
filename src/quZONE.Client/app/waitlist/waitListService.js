﻿(function () {
    'use strict';

    angular
        .module('qzone')
        .factory('waitListService', waitListService);

    waitListService.$inject = ['$http', 'serverBase'];

    function waitListService($http, serverBase) {
        //var service = {
        //    getData: getData
        //};

        //return service;

        //function getData() { }

        var serviceBase = serverBase + 'api/waitlist/';

        var waitListServiceFactory = {};

        var _getWaitList = function (data) {

            var result = $http.get(serviceBase + 'list/' + data).success(function (response) {

                //console.log($scope.$parent.authentication.isAdmin);
                //$scope.lists = response;
                //console.log($scope.lists);
                console.log(response);
                return response;
            });

            return result;
        };

        var _addToWaitList = function(data) {

            var result = $http.post(serviceBase + 'api/waitlist/list/add', data);

            return result;
        };

        var _getWaitGuest = function(data) {
            
            var result = $http.get(serviceBase + 'list/' + data.id + '/guest/' + data.gid);

            return result;
        };


        waitListServiceFactory.getWaitList = _getWaitList;
        waitListService.addToWaitList = _addToWaitList;
        waitListServiceFactory.getWaitGuest = _getWaitGuest;

        return waitListServiceFactory;


    }
})();