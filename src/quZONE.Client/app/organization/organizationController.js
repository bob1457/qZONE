/// <reference path="../common/modalDialog.html" />
(function () {
    'use strict';

    angular
        .module('qzone')
        .controller('organizationController', organizationController);

    organizationController.$inject = ['$scope', '$location', '$http', 'ngDialog', 'organizationDataService', 'serverBase', 'uibDateParser'];

    function organizationController($scope, $location, $http, ngDialog, organizationDataService, serverBase, uibDateParser) {
        /* jshint validthis:true */
        var vm = this;
        vm.title = 'organizationController';

        var serviceBase = serverBase + 'api/profile/organization/';

        //var data = $scope.$parent.$parent.authentication.userName;

        //for date picker;
        //
        

        $scope.today = function () {
            $scope.dt = new Date();
        };
        $scope.today();

        $scope.inlineOptions = {
            //customClass: getDayClass,
            minDate: new Date(),
            showWeeks: true
        };

        $scope.dateOptions = {
           // dateDisabled: disabled,
            formatYear: 'yy',
            maxDate: new Date(2020, 5, 22),
            minDate: new Date(),
            startingDay: 1
        };

        $scope.open1 = function () {
            $scope.popup1.opened = true;
        };

        $scope.open2 = function () {
            $scope.popup2.opened = true;
        };

       

        $scope.setDate = function (year, month, day) {
            $scope.dt = new Date(year, month, day);
        };

        $scope.formats = ['dd-MMMM-yyyy', 'yyyy/MM/dd', 'dd.MM.yyyy', 'shortDate'];
        $scope.format = $scope.formats[0];
        $scope.altInputFormats = ['M!/d!/yyyy'];

        $scope.toggleMin = function () {
            $scope.inlineOptions.minDate = $scope.inlineOptions.minDate ? null : new Date();
            $scope.dateOptions.minDate = $scope.inlineOptions.minDate;
        };

        $scope.toggleMin();

        $scope.popup1 = {
            opened: false
        };

        $scope.popup2 = {
            opened: false
        };

        var tomorrow = new Date();
        tomorrow.setDate(tomorrow.getDate() + 1);
        var afterTomorrow = new Date();
        afterTomorrow.setDate(tomorrow.getDate() + 1);
        $scope.events = [
          {
              date: tomorrow,
              status: 'full'
          },
          {
              date: afterTomorrow,
              status: 'partially'
          }
        ];

        function getDayClass(data) {
            var date = data.date,
              mode = data.mode;
            if (mode === 'day') {
                var dayToCheck = new Date(date).setHours(0, 0, 0, 0);

                for (var i = 0; i < $scope.events.length; i++) {
                    var currentDay = new Date($scope.events[i].date).setHours(0, 0, 0, 0);

                    if (dayToCheck === currentDay) {
                        return $scope.events[i].status;
                    }
                }
            }

            return '';
        }


        //End of date pikcer








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
            paymentOption: ""//,
            //usageOption: "" //newly added

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


        $scope.mregister = {};
        $scope.mregister.monthId = "01";

        $scope.mregister.months = [
            {
                id: "01",
                name: "January"
            },
            {
                id: "02",
                name: "Februrary"
            },
            {
                id: "03",
                name: "March"
            },
            {
                id: "04",
                name: "April"
            },
            {
                id: "05",
                name: "May"
            },
            {
                id: "06",
                name: "June"
            },
            {
                id: "07",
                name: "July"
            },
            {
                id: "08",
                name: "August"
            },
            {
                id: "09",
                name: "September"
            },
            {
                id: "10",
                name: "October"
            },
            {
                id: "11",
                name: "November"
            },
             {
                 id: "12",
                 name: "December"
             }
        ];


        $scope.yregister = {};
        $scope.yregister.yearId = "2016";

        $scope.yregister.years = [
            {
                id: "2016",
                name: "2016"
            },
            {
                id: "2017",
                name: "2017"
            },
            {
                id: "2018",
                name: "2018"
            },
            {
                id: "2019",
                name: "2019"
            },
             {
                 id: "2020",
                 name: "2020"
             }
        ];

        

        // make a request to get waitlist by org and by month/year IF THE MONTH IS SELECTED!!!


        // conditions are put here!!!
        //if ($scope.value === "monthYear") {

        

        var data = $scope.mregister.monthId + $scope.yregister.yearId;

        // $scope.getMonthlyList = function(oId, data) {

        // var month = 
        $scope.getMonthlyList = function (oId, data) {

            debugger;

            /**/ var promise = organizationDataService.getOrgWaitListByMonthYear(oId, data);

            promise.success(function (res) {
                $scope.accoiuntListByMonthYear = res;

                console.log($scope.accoiuntListByMonthYea);

            });

        }




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

        var getAllWaitList = function (oId) {
            var promiseUsage = organizationDataService.getOrgWaitList(oId);

            promiseUsage.success(function (res) {
                $scope.accountWaitList = res; //ALL waitlist

                console.log($scope.accountWaitList);
            });
        }

        $scope.getAllWaitList = function(oId) {
            getAllWaitList(oId);
        }

        $scope.showAccountDetails = function (oId) {

            // delete $scope.accountDetails;

            var promise = organizationDataService.getOrgAccount(oId); //Get account details
            $scope.value = "all";
            promise.success(function(respond) {
                $scope.accountDetails = respond;

                debugger;

                //make another http call(s) to retrieve usage data (All wait list) on success of getting account details - this part needs to be REFACTORED!!!

                //if ($scope.value === "all") { //This is default selection, will change
                   /* */var promiseUsage = organizationDataService.getOrgWaitList(oId);

                    promiseUsage.success(function(res) {
                        $scope.accountWaitList = res; //ALL waitlist

                        console.log($scope.accountWaitList);
                    });
                              //This part has been moved to a function.

                    // getAllWaitList(oId);

                //}
                

                debugger;
                //Test radio button selection

                
                console.log($scope.value);

                //set select box for month and year

                
                        
                    //}

                    
                //}


                





                //make another http call(s) to retrieve payment data
                var promisePayment = organizationDataService.getOrgPayment(oId);

                promisePayment.success(function (resp) {
                    $scope.accountPaymentList = resp;
                });








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
