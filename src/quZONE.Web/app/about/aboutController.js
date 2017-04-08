(function () {
    'use strict';

    angular
        .module('ezQ')
        .controller('aboutController', aboutController);

    aboutController.$inject = ['$location']; 

    function aboutController($location) {
        /* jshint validthis:true */
        var vm = this;
        vm.title = 'aboutController';

        activate();

        function activate() { }
    }
})();
