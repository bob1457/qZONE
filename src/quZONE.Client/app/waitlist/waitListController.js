(function () {
    'use strict';

    angular
        .module('qzone')
        .controller('waitListController', waitListController);

    waitListController.$inject = ['$scope', '$location', '$http', 'userProfileService', 'authService', 'waitListService', 'ngDialog', 'Twilio', 'serverBase'];

    function waitListController($scope, $location, $http, userProfileService, authService, waitListService, ngDialog, Twilio, serverBase) {
        /* jshint validthis:true */
        var vm = this;
        vm.title = 'waitListController';
        
        //var serviceBase = serviceBase;

        //console.log("server base from waitlist controller " + serviceBase);
        //debugger;

        $scope.fromCurrentUser = authService.authentication; //$scope.currentUser;

        console.log($scope.fromCurrentUser);


        //Get full user profile and load existing waitlist
        //
        var data = $scope.fromCurrentUser.userName;

        var promise = userProfileService.getUserProfile(data);

        promise.success(function (profileData) {

            $scope.profile = profileData;

            console.log($scope.profile);

            console.log($scope.profile.orgainzationId);

            //Call service to get waitlist
            //
            var companyId = $scope.profile.orgainzationId;

            //$scope.orgId = $scope.profile.orgainzationId;

            //console.log($scope.orgId);

            var promise = waitListService.getWaitList(companyId);

            promise.success(function (data) {
                $scope.listData.lists = data;
            });

            $scope.listData = {
                getuestFirstName: "",
                guestLastName: "",
                guestContactTel: "",
                guestGroupSize: "",
                waitingStatus: "Waiting",
                notes: "",
                organizationId: ""
            };

            $scope.listData.OrganizationId = companyId;

        });

        //$scope.$on('test', function (event, data) {
        //    alert(data + "received in waitlist controller!");
        //    console.log(data + "received in waitlist controller!");
        //});

        var loadRecord = function (theData) {

            //var companyId = $scope.$parent.profile.orgainzationId;

            //console.log(companyId);

            debugger;
            console.log(theData);

            $http.get(serverBase + 'api/waitlist/list/' + theData).success(function (response) {
                //$http.get(serviceBase + 'api/waitlist/list/' + theData).success(function (response) {

                //console.log($scope.$parent.authentication.isAdmin);
                $scope.listData.lists = response;
                console.log($scope.listData.lists);
            });
        };
        
        $scope.$on('refreshList', function (event, data) {
            alert(data + "received in waitlist controller!");
            console.log(data + "received in waitlist controller!");
            loadRecord(data);

        });


        $scope.sendMsg = function() {
            //debugger;
            Twilio.create('Messages', {
                from: '+16042434804',
                to: '+6046195810',
                body: 'Hi sms message from angularjs....'
            });
        };



        activate();

        function activate() {}
    }
})();
