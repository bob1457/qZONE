(function () {
    'use strict';

    angular
        .module('qzone')
        .factory('organizationService', organizationService);

    organizationService.$inject = ['$http', 'serverBase'];

    function organizationService($http, serverBase) {


        //debugger;
        var service = {
            getData: getData
        };

        return service;

        function getData() {

            var serviceBase = serverBase;

            

            var organizations = function () {

                $http.get(serviceBase + 'api/profile/organizations/').success(function (response) {
                
                    return  response;
                    //console.log($scope.organizations);
                });

                //return response;
            };
            

        }
    }
})();