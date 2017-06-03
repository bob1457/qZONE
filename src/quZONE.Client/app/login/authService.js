(function () {
    'use strict';

    angular
        .module('qzone')
        .factory('authService', authService);

    authService.$inject = ['$http', '$q', 'localStorageService', 'userProfileService', 'serverBase'];

    function authService($http, $q, localStorageService, userProfileService, serverBase) {
        //var service = {
        //    getData: getData
        //};

        //return service;

        //function getData() { }

        var serviceBase = serverBase; //To be finalized for accessing backend api services - for testing purpose
        var authServiceFactory = {}; // Object to be returned by the service

        var _authentication = { //User profile information
            isAuthorized: false,
            userName: "",
            //Newly added
            //
            userAvatarImgUrl: "",
            isAdmin: false,
            orgId: "",
            level: "", // 0 - trial account, 3 - paid account
            firstName: "",
            position: "",
            joinDate: ""
        };

        var _saveRegistration = function (registration) {

            _logOut();

            return $http.post(serviceBase + 'account/register', registration).then(function (response) {
                return response;
            });

        };

        

        var _login = function (loginData) {
//debugger;
            var data = "grant_type=password&username=" + loginData.userName + "&password=" + loginData.password;

            var deferred = $q.defer();

            $http.post(serviceBase + 'oauth/token', data, { headers: { 'Content-Type': 'application/x-www-form-urlencoded' } }).success(function (response) {

                localStorageService.set('authorizationData', { token: response.access_token, userName: loginData.userName });

                //Get user profile properties after autenticated successfully
                //
                

                var profileResult = userProfileService.getUserProfile(loginData.userName);

                profileResult.success(function(res) {
                    _authentication.orgId = res.orgainzationId;
                    _authentication.level = res.level;
                    _authentication.firstName = res.firstName;
                    _authentication.position = res.position;
                    _authentication.joinDate = res.joinDate;

                    _authentication.userAvatarImgUrl = res.avatarImgUrl;

                    if (loginData.userName === "admin") {
                        _authentication.isAdmin = true;
                    } else {
                        _authentication.isAdmin = false;
                    }
                });

                console.log(_authentication); //it works, all properties of the object are showing up

                _authentication.isAuthorized = true;
                _authentication.userName = loginData.userName;
                //_authentication.userAvatarImgUrl = 'content/images/avatars/' + loginData.userName + ".jpg";

                //loginRedirectService.redidrectPostLogin();

                deferred.resolve(response);

            }).error(function (err, status) {
                _logOut();
                deferred.reject(err);
            });

            return deferred.promise;

        };

        var _logOut = function () {

            localStorageService.remove('authorizationData');

            _authentication.isAuthorized = false;
            _authentication.userName = "";

        };

        var _fillAuthData = function () {

            var authData = localStorageService.get('authorizationData');
            if (authData) {
                _authentication.isAuthorized = true;
                _authentication.userName = authData.userName;
                //_authentication.userAvatarImgUrl = 'content/images/avatars/' + authData.userName + ".jpg";
                _authentication.isAdmin = authData.isAdmin;
                _authentication.orgId = authData.orgId;
                _authentication.level = authData.level;
                _authentication.firstName = authData.firtName;
                _authentication.position = authData.position;
                _authentication.joinDate = authData.joinDate;

            }

        };

        authServiceFactory.saveRegistration = _saveRegistration;
        authServiceFactory.login = _login;
        authServiceFactory.logOut = _logOut;
        authServiceFactory.fillAuthData = _fillAuthData;
        authServiceFactory.authentication = _authentication;

        return authServiceFactory;

    }
})();