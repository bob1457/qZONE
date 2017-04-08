(function() {
    'use strict';

    angular
        .module('qzone')
        .directive('reservationList', reservationDirective);

    reservationDirective.$inject = ['$window'];
    
    function reservationDirective ($window) {
        // Usage:
        //     <reservation-directive></reservation-directive>
        // Creates:
        // 
        var directive = {
            link: link,
            templateUrl: '/app/reservation/reservationList.html',
            restrict: 'EA'
        };
        return directive;

        function link(scope, element, attrs) {
        }
    }

})();