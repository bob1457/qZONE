(function () {
    'use strict';

    var app = angular.module('qzone', [
        // Angular modules 
        'ngAnimate',        // animations
        'ngRoute',          // routing
        //'ngSanitize',       // sanitizes html bindings (ex: sidebar.js)
        'ui.bootstrap',      // ui-bootstrap (ex: carousel, pagination, dialog)
        // Custom modules 
        //'common',           // common functions, logger, spinner
        //'common.bootstrap', // bootstrap dialog wrapper functions
        //'authService',


        // 3rd Party Modules
        'angularUtils.directives.dirPagination',
        'ngDialog',
        'mcwebb.twilio',
        'ds.clock', //angular clock
        'ngFileUpload',
        'LocalStorageModule'
        
    ]);

    //debugger;

    app.config(function($routeProvider) {
        $routeProvider
            .when("/", {
                templateUrl: 'app/login/login.html',
                controller: 'loginController',
                resolve: {
                    "check": function (checkService, $location) {
                        if (checkService.isAuthorized === true) {
                            $location.path('/dashboard');
                        } else {
                            //$location.path('/login');
                        }
                    }
                }
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
            .when("/dashboard", {
                templateUrl: 'app/dashboard/dashboard.html',
                controller: 'dashboardController',
                resolve: {
                    "check": function(checkService, $location) {
                        if (checkService.isAuthorized === true) {

                        } else {
                            $location.path('/login');
                        }
                    }
                } 
            })
            .when("/reservation", {
                templateUrl: 'app/reservation/reservation.html',
                controller: 'userdashController',
                resolve: {
                    "check": function (checkService, $location) {
                        if (checkService.isAuthorized === true) {

                        } else {
                            $location.path('/login');
                        }
                    }
                }
            })
            .when("/waitlist", {
                templateUrl: 'app/waitlist/waitlist.html',
                controller: 'dashboardController',
                resolve: {
                    "check": function (checkService, $location) {
                        if (checkService.isAuthorized === true) {

                        } else {
                            $location.path('/login');
                        }
                    }
                }
            })
            .when("/administration", {
                templateUrl: 'app/administration/administration.html',
                controller: 'userdashController',
                resolve: {
                    "check": function (checkService, $location) {
                        if (checkService.isAuthorized === true) {

                        } else {
                            $location.path('/login');
                        }
                    }
                }
            })
            .when("/table", {
                templateUrl: 'app/dinningtable/table.html',
                controller: 'userdashController',
                resolve: {
                    "check": function (checkService, $location) {
                        if (checkService.isAuthorized === true) {

                        } else {
                            $location.path('/login');
                        }
                    }
                }
            })
            .when("/togo", {
                templateUrl: 'app/togo/foodToGo.html',
                controller: 'userdashController',
                resolve: {
                    "check": function (checkService, $location) {
                        if (checkService.isAuthorized === true) {

                        } else {
                            $location.path('/login');
                        }
                    }
                }
            })
            .when("/profile", {
                templateUrl: 'app/profile/profile.html',
                controller: 'profileController',
                resolve: {
                    "check": function (checkService, $location) {
                        if (checkService.isAuthorized === true) {

                        } else {
                            $location.path('/login');
                        }
                    }
                }
            })
            .when("/org", {
                templateUrl: 'app/admin/organization/organizations.html',
                controller: 'adminController',
                resolve: {
                    "check": function (checkService, $location) {
                        if (checkService.isAuthorized === true) {

                        } else {
                            $location.path('/login');
                        }
                    }
                }
            })
            .when("/account", {
                templateUrl: 'app/admin/account/account.html',
                controller: 'adminController',
                resolve: {
                    "check": function (checkService, $location) {
                        if (checkService.isAuthorized === true) {

                        } else {
                            $location.path('/login');
                        }
                    }
                }
            })
            .when("/reporting", {
                templateUrl: 'app/admin/reporting/reporting.html',
                controller: 'adminController',
                resolve: {
                    "check": function (checkService, $location) {
                        if (checkService.isAuthorized === true) {

                        } else {
                            $location.path('/login');
                        }
                    }
                }
            })
            .when("/support", {
                templateUrl: 'app/admin/support/support.html',
                controller: 'adminController',
                resolve: {
                    "check": function (checkService, $location) {
                        if (checkService.isAuthorized === true) {

                        } else {
                            $location.path('/login');
                        }
                    }
                }
            })
            .otherwise({
                redirectTo: '/'
        
        
        });
    });

    app.constant('serverBase', 'http://localhost:1282/'); //Development mode
    //app.constant('serverBase', 'http://localhost/ezQ/'); //Deployed testing mode
    //app.constant('serverBase', '/ezQ/'); //one site deployment

    //Twilio sms gateway
    app.config(function(TwilioProvider) {
        TwilioProvider.setCredentials({
            accountSid: 'AC77d88a49c272f063fe810c7501361d4c',
            authToken: '425fd6d9cb1b5667e8fe49bde2d6fe73'
        });
    });




    // Handle routing errors and success events
    app.run(['$route', function ($route) {
        // Include $route to kick start the router.
    }]);


    //The following two funcitons check the authentication status

    app.config(function ($httpProvider) {
        $httpProvider.interceptors.push('authInterceptorService');
    });

    app.run(['authService', function (authService) {
        authService.fillAuthData();
    }]);

    //app.factory('checkService', function (checkService) {

    //    var result = {};

    //    this.access = false;

    //    result = checkService.fillAuthData();

    //    return result;
    //});


})();