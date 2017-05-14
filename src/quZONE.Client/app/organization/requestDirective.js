(function() {
    'use strict';

    angular
        .module('qzone')
        .directive('requestDirective', requestDirective);

    requestDirective.$inject = ['ngDialog'];
    
    function requestDirective (ngDialog) {
        // Usage:
        //     <request-directive></request-directive>
        // Creates:
        // 
        var directive = {
            link: link,
            templateUrl: 'app/organization/request.html',
            restrict: 'EA'
        };
        return directive;

        function link(scope, element, attrs) {
        }
    }

})();