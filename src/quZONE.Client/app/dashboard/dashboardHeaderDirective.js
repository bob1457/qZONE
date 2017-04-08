(function() {
    'use strict';

    angular
        .module('qzone')
        .directive('dashboardHeader', dashboardHeaderDirective);

    dashboardHeaderDirective.$inject = ['$window'];
    
    function dashboardHeaderDirective ($window) {
        // Usage:
        //     <dashboardHeaderDirective></dashboardHeaderDirective>
        // Creates:
        // 
        var directive = {
            templateUrl: 'app/dashboard/dashboardHeader.html',
            link: link,
            restrict: 'EA'
        };
        return directive;

        function link(scope, element, attrs) {
        }
    }

})();