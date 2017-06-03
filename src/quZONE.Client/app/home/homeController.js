(function () {
    'use strict';

    angular
        .module('qzone')
        .controller('homeController', homeController);

    homeController.$inject = [
        '$rootScope', "$scope", '$location', 'authService', 'organizationDataService', 'userAccountService', 'userProfileService', 'serverBase'];

    function homeController($rootScope, $scope, $location, authService, organizationDataService, userAccountService, userProfileService, serverBase) {
        /* jshint validthis:true */
        var vm = this;
        vm.title = 'homeController';

        $scope.activeItem = 'Dashboard';

        $scope.logOut = function () {
            authService.logOut();
            $location.path('/');
        };

        //$scope.isAuthorized = false;

        /* Get current date */
        $scope.date = new Date();

        //$scope.login = function() {
        //    $scope.state = "authorized";
        //};
       
        $scope.baseUrl = serverBase;
        //$scope.login = function () {

        //debugger;
        //    $scope.isAuthorized = true;
        //    $location.path('/dashboard');
        //}

        //debugger;

        //$scope.NumberOfOrgs = 5;
        //var orgs = organizationDataService.getAllOrganizations();

        //orgs.success(function(data) {
        //    $scope.NumberOfOrgs = data.length;
        //});

        //var usrs = userAccountService.getAllUsers().success(function(res) {
        //    $scope.NumberOfUserss = res.length;
        //});

        //var data = authService.authentication.userName;

        //var promise = userProfileService.getUserProfile(data);

        //promise.success(function(profileData) {
        //    $scope.profile = profileData;

        //    console.log($scope.profile);

        //    console.log($scope.profile.position);
        //});
        


        $scope.$on('sendWaitListNumber', function (event, args) {
            $scope.NumberOflists = args.wlist;
            console.log($scope.NumberOflists);


            var data = authService.authentication.userName;

            var promise = userProfileService.getUserProfile(data);

            promise.success(function (profileData) {
                $scope.profile = profileData;

                console.log($scope.profile);

                console.log($scope.profile.position);

                if ($scope.profile.level === 0) {
                    $scope.trial2 = true;
                } else {
                    $scope.trial2 = false;
                }

            });


        });

        $scope.$on('sendOrgNumber', function (event, args) {
            $scope.NumberOfOrgs = args.olist;
            
        });
        
        $scope.$on('sendUserNumber', function (event, args) {
            $scope.NumberOfUsers = args.ulist;
            
        });

        $scope.$on('orgUserAdded', function (event, args) {
            //debugger;
            $scope.isManager = args.position;

        });

        $scope.$on('sendTableNumber', function (event, args) {
            $scope.NumberOfTables = args.tlist;
            console.log($scope.NumberOfTables);
        });


        //s = organizationDataService.getAllOrganizations().length;
        //

        $scope.authentication = authService.authentication; //it is also the information of the currently logged-in user

        $scope.companyId = authService.authentication.orgId;


        if ($scope.authentication.level === 0) {
            $scope.trial2 = true;
        } else {
            $scope.trial2 = false;
        }

        console.log($scope.trial2);

        //console.log($scope.companyId);

       $scope.currentUser = authService.authentication;

       console.log($scope.currentUser);
       console.log($scope.authentication);

        //console.log($scope.authentication);


       

//debugger;
//        $scope.addToWaitList = function () {
            

//            $scope.isDisabled = true;
//            $scope.loading = true;

//            $http.post(serviceBase + 'list/add', $scope.listData).success(function (response) {

//                $scope.loading = false;

//                ngDialog.close();

//                loadRecord();

//                $location.path('/userdash');
//            });


//        }






        activate();

        function activate() { }
    }
})();
