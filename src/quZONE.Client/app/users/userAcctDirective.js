(function() {
    'use strict';

    angular
        .module('qzone')
        .directive('userAcct', userAcctDirective);

    userAcctDirective.$inject = ['$http', 'ngDialog', 'organizationService'];
    
    function userAcctDirective($http, ngDialog, organizationService) {
        // Usage:
        //     <userAcctDirective></userAcctDirective>
        // Creates:
        // 
        var directive = {
            templateUrl: 'app/users/userAccounts.html',
            link: link,
            restrict: 'EA'
        };
        return directive;

        function link(scope, element, attrs) {

            //Modal Dialog
            //
            scope.showAddUser = function () {
                ngDialog.open(
                    {
                        template: 'app/users/addNew.html',
                        className: 'ngdialog-theme-plain'
                    }
                );
            };

            scope.closeDialog = function() {
                ngDialog.close();
            };

            scope.addUser = function () {
                
            };





            //debugger;
            //Get organizaiton list -- will change to call service later...
            //
            //var serviceBase = 'http://localhost:1282/';

            
            //$http.get(serviceBase + 'api/profile/organizations/').success(function (response) {

            //    //console.log($scope.$parent.authentication.isAdmin);
            //    scope.organizations = response;
            //    console.log(scope.organizations);
            //});

        }
    }

})();