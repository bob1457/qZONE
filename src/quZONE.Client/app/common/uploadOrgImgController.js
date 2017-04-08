(function() {
    'use strict';

    angular
        .module('qzone')
        .controller('uploadOrgImgController', uploadOrgImgController);

    uploadOrgImgController.$inject = ['$scope', '$http', '$timeout', 'Upload', 'userProfileService', 'organizationDataService', 'serverBase'];

    function uploadOrgImgController($scope, $http, $timeout, Upload, userProfileService, organizationDataService, serverBase) {

        $scope.upload = [];
        $scope.fileUploadObje = {};

        var serviceBase = serverBase + 'api/';
        //debugger;
        var uname = $scope.$parent.name;
        
        //$scope.loading = true;

        var refresh = function () {

            var promise = organizationDataService.getAllOrganizationById($scope.$parent.OrganizationId); //need to change to refresh org info

            promise.success(function (data) {
                $scope.$parent.profile = data;
                //$scope.$parent.$parent.authentication.userAvatarImgUrl = $scope.profile.avatarImgUrl;
            });
        };

        $scope.refresh = function () {
           // debugger;
            refresh();
        };

        $scope.uploadFiles = function (file, errFiles) {

            //debugger;

            $scope.loading = true;
            $scope.isDisabled = true;

            $scope.f = file;
            $scope.errFile = errFiles && errFiles[0];
            if (file) {
                file.upload = Upload.upload({
                    url: serviceBase + 'profile/userProfile/uploadOrgImg/' + $scope.$parent.OrganizationId,
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