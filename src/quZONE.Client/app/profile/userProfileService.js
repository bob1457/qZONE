(function () {
    'use strict';

    angular
        .module('qzone')
        .factory('userProfileService', userProfileService);

    userProfileService.$inject = ['$http', 'serverBase'];

    function userProfileService($http, serverBase) {
        //var service = {
        //    getData: getData
        //};

        //return service;

        //function getData() { }

        var serviceBase = serverBase;

        var userProfile = {};

        userProfile.getUserProfile = function (theData) {

            return $http.get(serviceBase + 'api/profile/userProfile/' + theData);

            
        };

        userProfile.getUserId = function(uname) {
            return $http.get(serviceBase + 'api/accounts/user/' + uname);
        };

        return userProfile;
    }
})();