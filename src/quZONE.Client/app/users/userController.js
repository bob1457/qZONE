(function () {
    'use strict';

    angular
        .module('qzone')
        .controller('userController', userController);

    userController.$inject = ['$scope', '$location', '$http', 'userService', 'ngDialog', 'userAccountService', 'userProfileService', 'serverBase'];

    function userController($scope, $location, $http, userService, ngDialog, userAccountService, userProfileService, serverBase) {
        /* jshint validthis:true */
        var vm = this;
        vm.title = 'userController';


        var serviceBase = serverBase;

        //debugger;

       

  

        $scope.userData = {
            userName: "",
            firstName: "",
            lastName: "",
            email: "",
            organizationId: "",
            positionId: "",
            password: "",
            confirmPassword: ""

        };

        //$scope.userData.organizationId = $scope.userData.selectedCompany.id;

        $scope.userData.selectedCompany = {};

        //$scope.userData.selectedCompany.id;

        //var company = "";

        //var company = $scope.selected;

        //console.log(company);

        //$scope.userData.companyId = 2;// $scope.userData.selectedCompany.id;

        
         
        //$scope.selectedCompany.id;

        $scope.getOrgId = function () {
            //debugger;
            $scope.userData.organizationId = $scope.userData.selectedCompany.id;
        };

        console.log($scope.userData.organizationId);


        var refresh = function () {

            var promise = userAccountService.getAllUsers();

            promise.success(function (data) {
                //$scope.users = data;
                $scope.userData.list = data;

                $scope.$parent.NumberOfUsers = data.length;

                $scope.$emit('sendUserNumber', {
                    ulist: data.length
                });

            });

        };




        $scope.addUser = function () {
            debugger;

            $scope.isDisabled = true;
            $scope.loading = true;

            //userService.addUser($scope.userData).then(function (response) {
            //    $scope.loading = false;

            //    $location.path('/dashboard');
            //});

            var data = $scope.userData;

            $scope.userData.positionId = 1;

            //ngDialog.close();

            if ($scope.userData.password === $scope.userData.confirmPassword) {
                $http.post(serviceBase + 'api/accounts/create', $scope.userData).success(function (response) {

                    $scope.loading = false;

                    $scope.$parent.userData.list.push($scope.userData);

                    refresh();

                    $scope.$parent.$broadcast('userAdd', "");

                    ngDialog.close();

                //$location.path('/dashboard');
                });
            } else {
                $scope.errorMsg = "Confirmed password does not match, please try again!";
                $scope.loading = false;
                $scope.isDisabled = false;
            }


            
        };


        $scope.$on('userAdd', function (event, data) {
            //alert(data + " is received in dinning table controller for waitlist updates!");

            refresh();
            //console.log(data + " is received in dinning table controller for waitlist updates!");
        });



        $scope.showAddUser = function () {
            ngDialog.open(
                {
                    appendTo: '#userDialog',
                    templateUrl: 'app/users/addNew.html',
                    className: 'ngdialog-theme-plain',
                    scope: $scope,
                    controller: 'userController'
                }
            );
        };

        $scope.closeDialog = function () {
            ngDialog.close();
        };

        
        $scope.refresh = function () {
            //debugger;
            refresh();
        };


        $scope.getUserInfo = function(theData) {

            var promise = userProfileService.getUserProfile(theData);

            promise.success(function (data) {
                //$scope.users = data;
                $scope.userInfo = data;

                console.log($scope.userInfo);

                ngDialog.open(
                {
                    appendTo: '#userDialog',
                    templateUrl: 'app/users/userDetails.html',
                    className: 'ngdialog-theme-plain',
                    scope: $scope,
                    controller: 'userController',
                    data: $scope.$parent.userInfo
                }
            );





            });
        };




        activate();

        function activate() {
            //var serviceBase = 'http://localhost:1282/';

            //Retrieve all organizations and bind to UI for creating user accounts  -- Not In Use at the moment!!!!!!!!!!!!!!!
            //
            $http.get(serviceBase + 'api/profile/organizations/').success(function (response) {

                //console.log($scope.$parent.authentication.isAdmin);
                $scope.selectedCompany = null;
                $scope.data = response;
                //console.log($scope.data);
            });

            //$http.get(serviceBase + 'api/profile/allusers/').success(function (response) {
            //    debugger;
            //    //console.log($scope.$parent.authentication.isAdmin);
            //    $scope.users = response;
            //    console.log($scope.users);
            //});

            var promise = userAccountService.getAllUsers();

            promise.success(function(data) {
                //$scope.users = data;
                $scope.userData.list = data;

                $scope.$parent.NumberOfUsers = data.length;

                console.log($scope.userData.list);

                $scope.$emit('sendUserNumber', {
                    ulist: data.length
                });

            });
        }
    }
})();
