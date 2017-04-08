(function () {
    'use strict';

    var app = angular.module('ezQ', [
        // Angular modules 
        'ngAnimate',
        'ngRoute',

        // Custom modules 

        // 3rd Party Modules
        'angularUtils.directives.dirPagination',
        'uiGmapgoogle-maps'
    ]);

    app.config(function ($routeProvider) {
        $routeProvider
            .when("/", {
                templateUrl: 'app/home/home.html',
                controller: 'homeController'
            })
            .when("/userdash", {
                templateUrl: 'app/dashboard/userdash.html',
                controller: 'userdashController',
                resolve: {
                    "check": function (checkService, $location) {
                        if (checkService.isAuthorized === true) {
                            $location.path('/userdash');
                        } else {
                            $location.path('/login');
                        }
                    }
                }
            })
            .when("/about", {
                 templateUrl: 'app/about/about.html',
                 controller: 'aboutController'
            })
            .when("/details/:id", {
                templateUrl: 'app/home/details.html',
                controller: 'aboutController'
            })
            .otherwise({
                redirectTo: '/'
            });
    });

    app.constant('serverBase', 'http://localhost:1282/');

})();