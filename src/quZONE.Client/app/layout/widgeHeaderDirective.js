(function() {
    'use strict';

    angular
        .module('qzone')
        .directive('widgeHeader', widgeHeader);

    widgeHeader.$inject = ['$window'];
    
    function widgeHeader ($window) {
        // Usage:
        //     <widgeHeaderDirective></widgeHeaderDirective>
        // Creates:
        // 
        var directive = {
            link: link,
            restrict: 'EA',
            scope: {
                'title': '@',
                'subtitle': '@',
                'rightText': '@',
                'allowCollapse': '@'
            },
            templateUrl: '/app/layout/widgetheader.html'
            
        };
        return directive;

        function link(scope, element, attrs) {
        }
    }

})();