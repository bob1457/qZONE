(function () {
    'use strict';

    angular
        .module('qzone')
        .controller('dashboardController', dashboardController);

    dashboardController.$inject = ['$scope', '$location', '$http', 'organizationDataService', 'serverBase'];

    function dashboardController($scope, $location, $http, organizationDataService, serverBase) {
        /* jshint validthis:true */
        var vm = this;
        vm.title = 'dashboardController';

 //debugger;
 var serviceBase = serverBase;

        var data = $scope.$parent.authentication.userName;

        //var orgs = organizationDataService.getAllOrganizations().success(function(data) {
        //    $scope.NumberOfOrgs = data.length;
        //});





        //var profileData = profileService.getData(data);

        //console.log($scope.$parent.authentication);
        // possibly: 
        // $scope.profile = profileSerive.getData(data);
        // $scope.$parent.authentication.userAvatarImgUrl = $scope.profile.avatarImgUrl;
        //
        // profileServie needs to be injected, and add the servie link in index.html, though.

        //$http.get(serviceBase + 'api/profile/userProfile/' + data).success(function(response) {
        //    $scope.profile = response;
        //    $scope.$parent.authentication.userAvatarImgUrl = $scope.profile.avatarImgUrl;

        //    if ($scope.profile.orgainzationId == 0) {
        //        $scope.$parent.authentication.isAdmin = true;
        //        //$scope.$parent.authentication.orgId = $scope.profile.orgainzationId;
        //    } else {
        //        $scope.$parent.authentication.isAdmin = false;
        //        //$scope.$parent.authentication.orgId = $scope.profile.orgainzationId;
        //    }

        //    $scope.$parent.authentication.orgId = $scope.profile.orgainzationId;

        //    //testing code
        //    $scope.msg = $scope.$parent.authentication.isAdmin;
        //    $scope.msg2 = $scope.$parent.authentication.orgId;
        //    console.log($scope.msg2);
        //    //console.log($scope.$parent.authentication.isAdmin);
        //});
        //debugger;

        //$scope.userData.selectedCompany = {}

        //$scope.userData = {
        //    userName: "",
        //    firstName: "",
        //    lastName: "",
        //    email: "",
        //    companyId: "",
        //    password: "",
        //    confirmPassword: ""

        //}
        
        //$scope.userData.companyId = $scope.userData.selectedCompany.id;
       
        //console.log(profileData.AvatarImgUrl);

        activate();
       
        function activate() {
            //profileService.getData();
            $http.get(serviceBase + 'api/profile/userProfile/' + data).success(function(response) {
                $scope.profile = response;
                $scope.$parent.authentication.userAvatarImgUrl = $scope.profile.avatarImgUrl;

                if ($scope.profile.orgainzationId === 1) {
                    $scope.$parent.authentication.isAdmin = true;
                    //$scope.$parent.authentication.orgId = $scope.profile.orgainzationId;
                } else {
                    $scope.$parent.authentication.isAdmin = false;
                    //$scope.$parent.authentication.orgId = $scope.profile.orgainzationId;
                }

                $scope.$parent.authentication.orgId = $scope.profile.orgainzationId;

                //testing code
                //$scope.msg = $scope.$parent.authentication.isAdmin;
                //$scope.msg2 = $scope.$parent.authentication.orgId;
                //console.log($scope.msg2);
            //console.log($scope.$parent.authentication.isAdmin);
            });

            //$http.get(serviceBase + 'api/profile/organizations/').success(function (response) {
            
            //    //console.log($scope.$parent.authentication.isAdmin);
            //    $scope.organizations = response;
            //    console.log($scope.organizations);
            //});

            



        }


        
    }
})();
