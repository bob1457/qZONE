(function () {
    'use strict';

    angular
        .module('qzone')
        .controller('profileController', profileController);

    profileController.$inject = ["$scope", '$location', '$http', 'ngDialog', 'userProfileService', 'serverBase'];

    function profileController($scope, $location, $http, ngDialog, userProfileService, serverBase) {
        /* jshint validthis:true */
        var vm = this;
        vm.title = 'profileController';

        $scope.activeItem = 'Dashboard';

        var serviceBase = serverBase;

        var data = $scope.$parent.authentication.userName;
        
        $scope.name = data;

        //var profileData = profileService.getData(data);

        //$http.get(serviceBase + 'api/profile/userProfile/' + data).success(function (response) {
        //    $scope.profile = response;
        //    $scope.$parent.authentication.userAvatarImgUrl = $scope.profile.avatarImgUrl;
        //    console.log($scope.profile);
        //});

        $scope.isDisabled = false;
        $scope.loading = false;

        $scope.profile = {
            firstName: "",
            lastName: "",
            email: ""
        };

        $scope.upgradeAcct = function() {
            ngDialog.open(
                {
                    appendTo: '#modalDialog',
                    template: 'app/profile/upgrade.html',
                    scope: $scope,
                    className: 'ngdialog-theme-plain',
                    controller: 'profileController'
                }
            );
        };


        var promise = userProfileService.getUserProfile(data);

        promise.success(function(data) {
            $scope.profile = data;
            $scope.$parent.authentication.userAvatarImgUrl = $scope.profile.avatarImgUrl;
            console.log($scope.profile);

            if ($scope.profile.level === 0) {
                $scope.trial = true;
            } else {
                $scope.trial = false;
            }

            console.log($scope.trial);


        });

        $scope.updateProfile = function (uname) {

            //debugger;

            $scope.loading = true;
            $scope.isDisabled = true;

            $http.post(serviceBase + 'api/accounts/update/' + uname, $scope.profile).success(function (response) {

                $scope.loading = false;
                $scope.isDisabled = false;

                $scope.tab = 1;
            });
        };

        $scope.passInfo = {
            oldPassword: "",
            newPassword: "",
            confirmPassword: ""
        };

        $scope.chanegPassword = function(uname) {

            //debugger;

            //var userByUserName = userProfileService.getUserId(uname);

            if ($scope.passInfo.newPassword === $scope.passInfo.confirmPassword) {

                //userByUserName.success(function (res) {
                //    $scope.userId = res.id;

                //    console.log(res.id);

                    $http.post(serviceBase + 'api/accounts/ChangePassword/', $scope.passInfo).success(function (response) {

                    $scope.loading = false;
                    $scope.isDisabled = false;

                    $scope.okMsg = "Your password has been successfully changed!";

                    $scope.tab = 1;

                //});

                
            });
            } else {
                $scope.errorMsg = "Confirm password does not match, please try it again!";
            }

            
        };

        debugger;

        $scope.upgrade = function() {
            //alert("upgrade!");
            $scope.isDisabled = true;
            $scope.loading = true;

            //call web api to do the upgrade

            ngDialog.close();
        }

        activate();

        function activate() {
            var promise = userProfileService.getUserProfile(data);
            debugger;
            promise.success(function (data) {
                $scope.profile = data;
                $scope.$parent.authentication.userAvatarImgUrl = $scope.profile.avatarImgUrl;
                console.log($scope.profile);

                if ($scope.profile.level == 0) {
                    $scope.trial = true;
                } else {
                    $scope.trial = false;
                }

                console.log($scope.trial);


            });
        }
    }
})();
