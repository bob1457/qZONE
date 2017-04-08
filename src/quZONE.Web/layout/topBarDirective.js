(function() {
    'use strict';

    angular
        .module('ezQ')
        .directive('topBar', topBarDirective);

    topBarDirective.$inject = ['$window'];
    
    function topBarDirective ($window) {
        // Usage:
        //     <topBarDirective></topBarDirective>
        // Creates:
        // 
        var directive = {
            link: link,
            templateUrl: "layout/topBarTemplate.html",
            restrict: 'EA'
        };
        return directive;

        function link(scope, element, attrs) {
        }
    }

})();