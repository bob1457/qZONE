(function () {
    'use strict';

    angular
        .module('qzone')
        .service('filteredListService', filteredListService);

    filteredListService.$inject = ['$http'];

    function filteredListService($http) {
        //this.getData = getData;

        //function getData() { }
        this.paged = function(itemLists, pageSize) {
            var retItem = [];
            for (var i = 0; i < itemLists.length; i++) {
                if (i % pageSize === 0) {
                    retItem[Math.floor(i / pageSize)] = [itemLists[i]];
                } else {
                    retItem[Math.floor(i / pageSize)].push(itemLists[i]);
                }
            }
            return retItem;
        };
    }
})();