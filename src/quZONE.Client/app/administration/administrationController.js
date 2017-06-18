(function () {
    'use strict';

    angular
        .module('qzone')
        .controller('administrationController', administrationController);

    administrationController.$inject = ['$scope', '$location', '$http', 'ngDialog', 'organizationDataService', 'authService', 'userProfileService', 'userAccountService', 'waitListService', 'serverBase'];

    function administrationController($scope, $location, $http, ngDialog, organizationDataService, authService, userProfileService, userAccountService, waitListService, serverBase) {
        /* jshint validthis:true */
        var vm = this;
        vm.title = 'administrationController';

       // debugger;

        var serviceBase = serverBase;

        $scope.baseUrl = serviceBase;

        console.log($scope.baseUrl);

        $scope.fromCurrentUser = authService.authentication; //$scope.currentUser;

        console.log($scope.fromCurrentUser);


        //Get full user profile and load existing waitlist
        //
        var data = $scope.fromCurrentUser.userName;
        
        $scope.userData = {
            userName: "",
            firstName: "",
            lastName: "",
            email: "",
            organizationId: "",
            positionId:"",
            password: "",
            confirmPassword: ""

        };


        var promise = userProfileService.getUserProfile(data);

        promise.success(function (profileData) {

            $scope.profile = profileData;

            console.log($scope.profile);

            console.log($scope.profile.orgainzationId);

            //Call service to get waitlist
            //
            var companyId = $scope.profile.orgainzationId;

            $scope.userData.organizationId = $scope.profile.orgainzationId;

            $scope.OrganizationId = companyId;
            //$scope.orgId = $scope.profile.orgainzationId;

            //console.log($scope.orgId);

            var promise = organizationDataService.getAllOrganizationById(companyId);

            promise.success(function (res) {

                $scope.orgDetails = res;
                $scope.profile = res;

                console.log($scope.orgDetails.id);
            });

            
            var result = userAccountService.getUsersByOrg(companyId);

            result.success(function(response) {

                $scope.usersInOrg = response;

                console.log($scope.usersInOrg);

            });



            var waitListPromise = waitListService.getAllWaitList(companyId);

            //console.log(companyId);

            waitListPromise.success(function (data) {
                $scope.listData.lists = data;
                console.log($scope.listData.lists);
            });

            $scope.listData = {
                getuestFirstName: "",
                guestLastName: "",
                guestContactTel: "",
                guestGroupSize: "",
                waitingStatus: "Waiting",
                waitTime: "",
                notes: "",
                organizationId: ""
            };

            $scope.listData.OrganizationId = companyId;

            console.log($scope.listData);
        });


        //debugger;
        

        $scope.showListDetails = function (gId) {

            var uname = $scope.fromCurrentUser.userName;

            var profile = userProfileService.getUserProfile(uname);

            profile.success(function (profileData) {

                var data = {
                    id: "",
                    gid: ""
                };

                $scope.profile = profileData;

                data.id = $scope.profile.orgainzationId;

                data.gid = gId;

                var promise = waitListService.getWaitGuest(data);

                promise.success(function (res) {

                    $scope.guestDetails = res;

                    console.log($scope.guestDetails);
                });

                var theData = $scope.profile.orgainzationId;

                var tables = tableService.getAvailableTablesForOrg(theData);

                tables.success(function (response) {
                    $scope.emptyTables = response;
                    console.log($scope.emptyTables);
                });

            });

            ngDialog.open(
                {
                    appendTo: '#listDialog',
                    templateUrl: 'app/waitlist/entryDetails2.html',
                    className: 'ngdialog-theme-default',
                    controller: 'userdashController',
                    scope: $scope,
                    data: $scope.$parent.gusetDetails
                }
            );
        };



        

        $scope.addUser = function() {
            //debugger;

            $scope.isDisabled = true;
            $scope.loading = true;

            $scope.userData.positionId = 2;

            if ($scope.userData.password === $scope.userData.confirmPassword) {
                    $http.post(serviceBase + 'api/accounts/create', $scope.userData).success(function (response) {

                    $scope.loading = false;

                    var currentDateTime = new Date();

                    $scope.date = currentDateTime; //.toLocaleString();

                    $scope.userData.joinDate = currentDateTime;

                    $scope.userData.avatarImgUrl = "content/images/avatars/avatar-default.png";


                    $scope.$parent.usersInOrg.push($scope.userData);

                    refresh($scope.userData.organizationId);

                    $scope.$parent.$broadcast('userAdd', "");

                    $scope.$emit('orgUserAdded', {
                        position: $scope.userData.positionId
                    });


                    ngDialog.close();

                
                });
            } else {
                $scope.errorMsg = "Confirmed password does not match, please try again!";
                $scope.loading = false;
                $scope.isDisabled = false;
            }

            
        };

        

        var refresh = function (id) {

            var promise = userAccountService.getUsersByOrg(id);

            promise.success(function (data) {
                //$scope.users = data;
                $scope.$parent.usersInOrg = data;

                $scope.$parent.NumberOfUsers = data.length;

                $scope.$emit('sendUserNumber', {
                    ulist: data.length
                });

            });

        };


        var refreshData = function (oId) {
            var promise = organizationDataService.getAllOrganizationById(oId);
            //debugger;
            promise.success(function (res) {

                $scope.profile = res;

                console.log($scope.profile);
            });

        };

        $scope.updateOrgInfo = function(data) {
            //debugger;
            var id = data.id;

            $scope.isDisabled = true;
            $scope.loading = true;

            $http.post(serviceBase + 'api/profile/organization/update/' + id, data).success(function (response) {
                //alert("updated successfully!");
                refreshData(id);
                $scope.loading = false;
                $scope.isDisabled = false;
            });
        };

        
        //Modal dialogs

        $scope.showAddUser = function () {
            ngDialog.open(
                {
                    appendTo: '#userDialog',
                    templateUrl: 'app/administration/addUser.html',
                    className: 'ngdialog-theme-plain',
                    scope: $scope,
                    controller: 'administrationController'
                }
            );
        };

        activate();

        function activate() {

            //$scope.getOrgDetails = function (oId) {
            //    debugger;
            //    var promise = organizationDataService.getAllOrganizationById(oId);

            //    promise.success(function (res) {

            //        $scope.orgDetails = res;

            //        console.log($scope.orgDetails);
            //    });

            //};

            //$scope.getUserInOrg = function () {

                

            //}



            // Emit the event
            $scope.$emit('orgUserAdded', {
                position: $scope.userData.positionId
            });


            //Get today's date

            var today = new Date();

            var dd = today.getDate();
            var mm = today.getMonth() + 1; //January is 0!
            var yyyy = today.getFullYear();

            $scope.tday = yyyy + "-" + mm + "-" + dd;
        }
    }
})();
