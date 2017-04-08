(function () {
    'use strict';

    angular
        .module('ezQ')
        .controller('detailsController', detailsController);

    detailsController.$inject = ['$scope', '$location'];

    function detailsController($scope, $location) {
        /* jshint validthis:true */
        var vm = this;
        vm.title = 'detailsController1';

        activate();

        function activate() {
            //$scope.currentUrl = $location.absUrl();
            //alert($scope.currentUrl);
            //console.log($scope.currentUrl);


        }
    }
})();
