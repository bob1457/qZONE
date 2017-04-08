(function () {
    'use strict';

    angular
        .module('qzone')
        .controller('loginController', loginController);

    loginController.$inject = ['$scope','$location', 'authService'];

    function loginController($scope, $location, authService) {
        /* jshint validthis:true */

        //var vm = this;
        //vm.title = 'loginController';

        $scope.loginData = {
            userName: "",
            password: ""
        };
       

        //vm.state = "unauthorized";
        //$scope.isAuthorized = false;

        $scope.isDisabled = false;

        $scope.login = function () {

            $scope.isDisabled = true;
            $scope.loading = true;

             authService.login($scope.loginData).then(function (response) {
                    //debugger;
                        //$scope.Model.loginData.userName = "";
                        //$scope.Model.loginData.password = "";
                 //$scope.isAuthorized = authService.authentication;

                        $scope.loading = false;

                        if($scope.loginData.userName === "admin")
                         {
                             $location.path('/dashboard');
                        } else {
                            $location.path('/userdash');
                        }

                       

                    },
                     function (err) {
                         $scope.message = err.error_description;
                         $scope.isDisabled = false;
                         $scope.loading = false;
                     });
            
        };


        
       



        activate();

        function activate() { }
    }
})();
