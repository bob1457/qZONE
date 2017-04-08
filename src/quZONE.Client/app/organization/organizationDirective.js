(function() {
    'use strict';

    angular
        .module('qzone')
        .directive('organizationDirective', organizationDirective);

    organizationDirective.$inject = ['ngDialog'];
    
    function organizationDirective(ngDialog) {
        // Usage:
        //     <organizationDirective></organizationDirective>
        // Creates:
        // 
        var directive = {
            templateUrl: 'app/organization/organization.html',
            link: link,
            restrict: 'EA'
        };
        return directive;

        function link(scope, element, attrs) {
            
            //scope.showAddCompany = function () {
            //    ngDialog.open(
            //        {
            //            appendTo: '#modalDialog',
            //            template: 'app/organization/addNew.html',
            //            scope: $scope,
            //            className: 'ngdialog-theme-plain',
            //            controller: 'organizationController'
            //        }
            //    );
            //};

            //scope.showDetails = function () {
            //    ngDialog.open(
            //        {
            //            template: 'app/organization/orgDetails.html',
            //            className: 'ngdialog-theme-default'
            //        }
            //    );
            //};
        }
    }

})();