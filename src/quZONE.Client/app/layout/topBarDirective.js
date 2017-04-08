(function() {
    'use strict';

    angular
        .module('qzone')
        .directive('topBar', topBar);

    topBar.$inject = ['$window'];
    
    function topBar ($window) {
        // Usage:
        //     <topBarDirective></topBarDirective>
        // Creates:
        // 
        var directive = {
            templateUrl: 'app/layout/header.html',
            link: link,
            restrict: 'EA'
        };
        return directive;

        function link(scope, element, attrs) {
        }
    }

})();