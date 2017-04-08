(function() {
    'use strict';

    angular
        .module('qzone')
        .directive('togoOrders', togoOrdersDirective);

    togoOrdersDirective.$inject = ['$window'];
    
    function togoOrdersDirective ($window) {
        // Usage:
        //     <togo-orders-directive></togo-orders-directive>
        // Creates:
        // 
        var directive = {
            link: link,
            templateUrl: '/app/togo/togoOrders.html',
            restrict: 'EA'
        };
        return directive;

        function link(scope, element, attrs) {
        }
    }

})();