(function() {
    'use strict';

    angular
        .module('ezQ')
        .directive('loadingSpinner', loadingSpinner);

    loadingSpinner.$inject = ['$window'];
    
    function loadingSpinner($window) {
        // Usage:
        //     <loadingDirective></loadingDirective>
        // Creates:
        // 
        var directive = {
            link: link,
            templateUrl: 'app/common/loadingSpinner.html',
            replace:true,
            restrict: 'E'
        };
        return directive;

        function link(scope, element, attrs) {
            scope.$watch('loading', function(val) {
                if (val)
                    $(element).show();
                else {
                    $(element).hide();
                }
            });
        }
    }

})();