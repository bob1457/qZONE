(function() {
    'use strict';

    angular
        .module('qzone')
        .directive('sideBar', sideBar);

    sideBar.$inject = ['$window'];
    
    function sideBar ($window) {
        // Usage:
        //     <sideBarDirective></sideBarDirective>
        // Creates:
        // 
        var directive = {
            templateUrl: 'app/layout/side.html',
            link: link,
            restrict: 'EA'
        };
        return directive;

        function link(scope, element, attrs) {
        }
    }

})();