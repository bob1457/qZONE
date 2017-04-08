(function () {
    'use strict';

    angular
        .module('ezQ')
        .controller('homeController', homeController);

    homeController.$inject = ['$scope', '$routeParams', '$http', '$location', 'restaurantService', 'serverBase' ];

    function homeController($scope, $routeParams, $http, $location, restaurantService, serverBase) {
        /* jshint validthis:true */
        var vm = this;
        vm.title = 'homeController';

        var serviceBase = serverBase;

        var promise = restaurantService.getAllRestaurants();

        promise.success(function (data) {
            //$scope.organizations = data;

            $scope.restaurants = data;
            console.log($scope.restaurants);

        });

        $scope.baseUrl = serverBase;

        $scope.getGuestDetails = function (id) {

            var promise = restaurantService.getRestaurantnById(id);

            promise.success(function (res) {

                $scope.restaurantDetails = res;

                $scope.tab = 1;

                console.log($scope.restaurantDetails);
            });
        }

        $scope.listData = {
            guestFirstName: "",
            guestLastName: "",
            guestContactTel: "",
            guestGroupSize: "",
            waitingStatus: "Waiting",
            notes: ""
            //organizationId: ""
        }

        $scope.addToWaitList = function (id) {
            debugger;

            $scope.listData.organizationId = id;
            
            $scope.loading = true;
            

            $http.post(serviceBase + 'api/waitlist/list/add', $scope.listData).success(function (response) {

                $scope.loading = false;

                $scope.Res = 0;

                //clear table
                $scope.addForm.$setPristine();

                $scope.listData.guestFirstName = '';
                $scope.listData.guestLastName = '';
                $scope.listData.guestContactTel = '';
                $scope.listData.guestGroupSize = '';


            });


            //refresh($scope.listData.organizationId);

            //console.log($scope.listData.lists);

        }

        $scope.cancelForm = function() {
            
            $scope.addForm.$setPristine();

            $scope.listData.guestFirstName = '';
            $scope.listData.guestLastName = '';
            $scope.listData.guestContactTel = '';
            $scope.listData.guestGroupSize = '';
        }


        $scope.getRestaurantDetails = function (id) {

            $scope.restaurant_id = $routeParams.id;

            var promise = restaurantService.getRestaurantnById(id);

            promise.success(function (res) {

                $scope.restaurantDetails = res;

                console.log($scope.restaurantDetails);
            });

        };


        

        activate();

        function activate() {
            //$scope.loading = true; //testing...
            debugger;

            $scope.restaurant_id = $routeParams.id;

            var promise = restaurantService.getRestaurantnById($scope.restaurant_id);

            promise.success(function (res) {

                $scope.restaurantDetails = res;

                console.log($scope.restaurantDetails);
            });

            //$scope.currentUrl = $location.absUrl();
            //alert($scope.currentUrl);
            //console.log($scope.currentUrl);

        }
    }
})();
