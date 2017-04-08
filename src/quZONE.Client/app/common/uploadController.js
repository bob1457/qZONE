(function() {
    'use strict';

    angular
        .module('qzone')
        .controller('uploadController', uploadController);

    uploadController.$inject = ['$scope', '$http', '$timeout', 'Upload', 'userProfileService', 'serverBase'];

    function uploadController($scope, $http, $timeout, Upload, userProfileService, serverBase) {

        $scope.upload = [];
        $scope.fileUploadObje = {};

        var serviceBase = serverBase + 'api/';

        var uname = $scope.$parent.name;

        //$scope.loading = true;

        var refresh = function () {

            var promise = userProfileService.getUserProfile(uname);

            promise.success(function (data) {
                $scope.$parent.profile = data;
                $scope.$parent.$parent.authentication.userAvatarImgUrl = $scope.profile.avatarImgUrl;
            });
        };

        $scope.refresh = function () {
            //refresh();
        };

        $scope.uploadFiles = function (file, errFiles) {

            //debugger;

            $scope.loading = true;
            $scope.isDisabled = true;

            //$scope.f = file;
            $scope.errFile = errFiles && errFiles[0];
            if (file) {
                file.upload = Upload.upload({
                    url: serviceBase + 'profile/userProfile/upload/' + uname,
                    data: { file: file }
                });

                file.upload.then(function (response) {
                    $timeout(function () {
                        file.result = response.data;
                        $scope.loading = false;
                        $scope.isDisabled = false;
                        refresh();
                    });
                }, function (response) {
                    if (response.status > 0)
                        $scope.errorMsg = response.status + ': ' + response.data;
                }, function (evt) {
                    file.progress = Math.min(100, parseInt(100.0 *
                                             evt.loaded / evt.total));
                });
            }
        };

        

    }
})();