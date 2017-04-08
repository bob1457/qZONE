(function () {
    'use strict';

    angular
        .module('qzone')
        .controller('adminController', adminController);

    adminController.$inject = ['$location']; 

    function adminController($location) {
        /* jshint validthis:true */
        var vm = this;
        vm.title = 'adminController';

        activate();

        function activate() { }
    }
})();
