(function () {
    'use strict';

    angular
        .module('qzone')
        .factory('profileService', profileService);

    profileService.$inject = ['$http', '$q', 'serviceBase'];

    //debugger;

    function profileService($http, $q, serviceBase) {

        var _userProfile = {
            //firstName: "",
            //lastName: "",
            //email: "",
            //phoneNumber: "",
            //avarUrl: "",
            //organization: "",
            //position:""
        };

        var service = {
            getData: getData
        };

        return service;

        //debugger;

        function getData(data) {
            var serviceBase = serviceBase; //To be finalized for accessing backend api services - for testing purpose
            var profileServiceFactory = {};

            //var deferred = $q.defer();

            $http.get(serviceBase + 'api/profile/userProfile/' + data).success(function (response) {

                //deferred.resolve(response);

                //_userProfile.firstName = response.firstName;
                //_userProfile.lastName = response.lastName;
                //_userProfile.email = response.email;
                //_userProfile.phoneNumber = response.phoneNumber;
                //_userProfile.avarUrl = response.avatarImgUrl;
                //_userProfile.organization = response.organiztion;
                //_userProfile.position = response.position;

                _userProfile = response;
                //return deferred.promise;

            }).error(function (err, status) {
                //_logOut();
                deferred.reject(err);
            });

            //return deferred.promise;

            //return result;
        }

    }
})();