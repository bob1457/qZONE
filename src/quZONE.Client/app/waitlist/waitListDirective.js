(function() {
    'use strict';

    angular
        .module('qzone')
        .directive('waitList', waitListDirective);

    waitListDirective.$inject = ['ngDialog'];
    
    function waitListDirective(ngDialog) {
        // Usage:
        //     <waitListDirective></waitListDirective>
        // Creates:
        // 
        var directive = {
            templateUrl: 'app/waitlist/waitlistTemplate.html',
            //scope: {
            //    listData: "=" 
            //},
            link: link,
            restrict: 'EA'
        };
        return directive;

        function link(scope, element, attrs) {

            //scope.showAdd = function () {
            //    ngDialog.open(
            //        {
            //            template: 'app/waitlist/addNew.html',
            //            className: 'ngdialog-theme-plain'
            //        }
            //    );
            //};

            //scope.showDetails = function () {
            //    ngDialog.open(
            //        {
            //            template: 'app/waitlist/entryDetails.html',
            //            className: 'ngdialog-theme-default'
            //        }
            //    );
            //};
        }
    }

})();