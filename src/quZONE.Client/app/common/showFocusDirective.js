(function() {
    'use strict';

    angular
        .module('qzone')
        .directive('showFocus', showFocusDirective);

    showFocusDirective.$inject = ['$timeout'];
    
    function showFocusDirective ($timeout) {
        // Usage:
        //     <showFocusDirective></showFocusDirective>
        // Creates:
        // 
        var directive = {
            link: link,
            restrict: 'EA'
        };
        return directive;

        function link(scope, element, attrs) {
            scope.$watch(attrs.showFocus, function(newValue) {
                $timeout(function() {
                    newValue && element.focus();
                });
            }, true);

        }
    }

})();