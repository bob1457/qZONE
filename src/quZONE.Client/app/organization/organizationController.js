﻿/// <reference path="../common/modalDialog.html" />
(function () {
    'use strict';

    angular
        .module('qzone')
        .controller('organizationController', organizationController);

    organizationController.$inject = ['$scope', '$location', '$http', 'ngDialog', 'organizationDataService', 'serverBase'];

    function organizationController($scope, $location, $http, ngDialog, organizationDataService, serverBase) {
        /* jshint validthis:true */
        var vm = this;
        vm.title = 'organizationController';

        var serviceBase = serverBase + 'api/profile/organization/';

        //var data = $scope.$parent.$parent.authentication.userName;

        //debugger;

        $scope.editing = false;
        //alert($scope.editing);

        $scope.addOrgnization = function() {
            $scope.isDisabled = true;
            $scope.loading = true;
        };

        $scope.orgData = {
            Name: "",
            Description: "",
            OrganizationType: "",
            AddressLine1: "",
            AddressLine2: "",
            City: "",
            ProvState: "",
            PostZipCode: "",
            Telephone: "",
            MessageCode:"",
            Notes: ""

        };

        $scope.requestData = {
            organizationName: "",
            orgAddressLine1: "",
            orgAddressCity: "",
            orgAddressProState: "",
            orgAddressPostZipCodeva:"",
            contactFirstName: "",
            contactLastName:"",
            contactEmail:"",
            contactTel:"",
            notes: "",
            isProcessed:"",
            createDate:""
        };



        var refresh = function () {

            var promise = organizationDataService.getAllOrganizations();

            promise.success(function (data) {
                //$scope.organizations = data;
                $scope.orgData.list = data;

                $scope.$parent.NumberOfOrgs = data.length;

                $scope.$emit('sendOrgNumber', {
                    olist: data.length
                });
            });


        };




        $scope.addCompany = function() {
            //debugger;

            $scope.isDisabled = true;
            $scope.loading = true;

            $http.post(serviceBase + 'create', $scope.orgData).success(function (response) {

                $scope.loading = false;

                $scope.$parent.orgData.list.push($scope.orgData);

                refresh();

                $scope.$parent.$broadcast('orgAdd', "");

                ngDialog.close();

                //$location.path('/dashboard');
            });

            
        };


        $scope.orgDetails = {
            Name: "",
            Description: "",
            OrganizationType: "",
            AddressLine1: "",
            AddressLine2: "",
            City: "",
            ProvState: "",
            PostZipCode: "",
            Telephone: "",
            MessageCode: "",
            Notes: ""

        };

        $scope.accountDetails = {
            id: "",
            isActive: "",
            notes: "",
            organizationId:"",
            paymentOption: ""

        };
        
        $scope.$on('orgAdd', function (event, data) {
            //alert(data + " is received in dinning table controller for waitlist updates!");

            refresh();
            //console.log(data + " is received in dinning table controller for waitlist updates!");
        });


        $scope.refresh = function() {
            refresh();
        };



        //Modal Dialog

        $scope.showAddCompany = function () {
            ngDialog.open(
                {
                    appendTo: '#modalDialog',
                    template: 'app/organization/addNew.html',
                    scope: $scope,
                    className: 'ngdialog-theme-plain',
                    controller: 'organizationController'
                }
            );
        };

        $scope.showDetails = function (oId) {
            //debugger;
            var promise = organizationDataService.getAllOrganizationById(oId);

            promise.success(function (res) {

                $scope.orgDetails = res;

                console.log($scope.orgDetails);
            });


            ngDialog.open(
                {
                    template: 'app/organization/orgDetails.html',
                    className: 'ngdialog-theme-default',
                    scope: $scope,
                    data: $scope.$parent.orgDetails
                }
            );
        };


        $scope.showReqDetails = function (rId) {
            //debugger;
            var promise = organizationDataService.getRequestById(rId);

            promise.success(function (res) {

                $scope.reqDetails = res;

                console.log($scope.reqDetails);
            });


            ngDialog.open(
                {
                    template: 'app/organization/reqDetails.html',
                    className: 'ngdialog-theme-default',
                    scope: $scope,
                    data: $scope.$parent.reqDetails
                }
            );
        };

        $scope.createTrialAcct = function (rId) {

            debugger;

            $scope.isDisabled = true;
            $scope.loading = true;


            $http.post(serverBase + 'api/profile/request/create/' + rId).success(function (res) {

                //make a new http request to create user account
                //
                $http.post(serverBase + 'api/profile/createtrial/create' + rId).success(function (response) {

                    $scope.loading = false;

                    $scope.$parent.userData.list.push($scope.userData);

                    refresh(); //refresh organization data
                   

                    $scope.$parent.$broadcast('userAdd', "");

                    
                });

                //refresh();

                //update request status
                //

                $scope.$parent.$broadcast('orgAdd', "");

            });

            ngDialog.close();
        }

        $scope.updateOrg = function (oId) {

            //debugger;

            $scope.isDisabled = true;
            $scope.loading = true;

            $http.post(serviceBase + 'update/' + oId, $scope.orgDetails).success(function (response) {

                $scope.loading = false;
                $scope.isDisabled = false;

                $scope.$parent.orgData = response;

                refresh();

                //$scope.$parent.$broadcast('orgAdd', "");

                ngDialog.close();

                //$location.path('/dashboard');
            });

            
        };

        debugger;

        $scope.showAccountDetails = function (oId) {
            var promise = organizationDataService.getOrgAccount(oId);

            promise.success(function(respond) {
                $scope.accountDetails = respond;

                //make another http call(s) to retrieve usage and payment data

                console.log($scope.accountDetails);
            });

            ngDialog.open(
                {
                    template: 'app/organization/accountDetails.html',
                    className: 'ngdialog-theme-default',
                    scope: $scope,
                    data: $scope.$parent.accountDetails
                }
            );
        }
       



        activate();

        
            function activate() {
                //profileService.getData();
                //$http.get(serviceBase + 'api/profile/userProfile/' + data).success(function (response) {
                //    $scope.profile = response;
                //    $scope.$parent.$parent.authentication.userAvatarImgUrl = $scope.profile.avatarImgUrl;

                //    if ($scope.profile.orgainzationId == 0) {
                //        $scope.$parent.$parent.authentication.isAdmin = true;
                //        //$scope.$parent.authentication.orgId = $scope.profile.orgainzationId;
                //    } else {
                //        $scope.$parent.$parent.authentication.isAdmin = false;
                //        //$scope.$parent.authentication.orgId = $scope.profile.orgainzationId;
                //    }

                //    $scope.$parent.$parent.authentication.orgId = $scope.profile.orgainzationId;

                //    //testing code
                //    //$scope.msg = $scope.$parent.authentication.isAdmin;
                //    //$scope.msg2 = $scope.$parent.authentication.orgId;
                //    //console.log($scope.msg2);
                //    //console.log($scope.$parent.authentication.isAdmin);
                //});

                //var serviceBase = 'http://localhost:1282/';

                //$http.get(serviceBase + 'api/profile/organizations/').success(function (response) {

                //    //console.log($scope.$parent.authentication.isAdmin);
                //    $scope.organizations = response;
                //    //console.log($scope.organizations);
                //});
                //debugger;

                var promise = organizationDataService.getAllOrganizations();

                promise.success(function(data) {
                    //$scope.organizations = data;

                    $scope.orgData.list = data;

                    //console.log($scope.orgData.list);

                    $scope.$parent.NumberOfOrgs = data.length;

                    $scope.$emit('sendOrgNumber', {
                        olist: data.length
                    });
                });


                //debugger;

                var promise_requet = organizationDataService.getAllTrialRequests();

                promise_requet.success(function (response) {

                    $scope.requestData.list = response;

                    console.log($scope.requestData.list);

                });


            }


           
       
    }
})();
