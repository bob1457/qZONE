(function() {
    'use strict';

    angular
        .module('qzone')
        .directive('dinningTable', dinningTableDirective);

    dinningTableDirective.$inject = ['$window'];
    
    function dinningTableDirective ($window) {
        // Usage:
        //     <dinningTalbeDirective></dinningTalbeDirective>
        // Creates:
        // 
        var directive = {
            templateUrl: 'app/dinningtable/tablesTemplate.html',
            link: link,
            restrict: 'EA'
        };
        return directive;

        function link(scope, element, attrs) {
        }
    }

})();