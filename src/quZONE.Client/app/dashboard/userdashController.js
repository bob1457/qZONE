(function () {
    'use strict';

    angular
        .module('qzone')
        .controller('userdashController', userdashController);

    userdashController.$inject = ['$scope', '$location', '$http',
        'ngDialog', '$uibModal', 'waitListService',
        'userProfileService', 'authService', 'tableService', 'Twilio', 'smsMessagingService', '$interval', 'serverBase'];

    function userdashController($scope, $location, $http,
        ngDialog, $uibModal, waitListService,
        userProfileService, authService, tableService, Twilio, smsMessagingService, $interval, serverBase) {
        
        var vm = this;
        vm.title = 'userdashController';

        var serviceBase = serverBase;

        $scope.baseUrl = serverBase;

        //debugger;

        $scope.fromCurrentUser = authService.authentication; //$scope.currentUser;

        //console.log($scope.fromCurrentUser);

        //$scope.listData = {};

        //Get full user profile and load existing waitlist
        //
        var data = $scope.fromCurrentUser.userName;

        var promise = userProfileService.getUserProfile(data);

        promise.success(function (profileData) {

            $scope.profile = profileData;

            //console.log($scope.profile);

            //console.log($scope.profile.orgainzationId);

            //Call service to get waitlist
            //
            var companyId = $scope.profile.orgainzationId;

            //$scope.orgId = $scope.profile.orgainzationId;

            //console.log($scope.orgId);

            var promise = waitListService.getWaitList(companyId);

            $scope.loading = true;

            promise.success(function (data) {
                $scope.listData.lists = data;
                $scope.loading = false;
                console.log($scope.listData.lists.length);
                //alert("current scope: " + $scope.listData.lists);

                $scope.$emit('sendWaitListNumber', {
                    wlist: data.length
                    });      
                
            });

            $scope.inHome = "true";

            $scope.listData = {
                guestFirstName: "",
                guestLastName: "",
                guestContactTel: "",
                guestGroupSize: "",
                waitingStatus: "Waiting",
                notes: "",
                organizationId: ""//,
                //createDate:"" //added attribute
            };

            $scope.tableData = {
                
            };

            $scope.listData.organizationId = companyId;

        });

       
        var refresh = function (theData) {

            var promise = waitListService.getWaitList(theData);

            promise.success(function (data) {
                $scope.listData.lists = data;
                console.log($scope.listData.lists.length);

                console.log($scope.listData.lists);
                $scope.$emit('sendWaitListNumber', {
                    wlist: data.length
                });
 
            });

           
        };


        $scope.addToWaitList = function () {
            //debugger;

            $scope.isDisabled = true;
            $scope.loading = true;

            $http.post(serviceBase + 'api/waitlist/list/add', $scope.listData).success(function (response) {

                $scope.loading = false;

                //

                var currentDateTime = new Date();

                $scope.date = currentDateTime; //.toLocaleString();

                $scope.listData.createDate = currentDateTime;

                $scope.$parent.listData.lists.push($scope.listData);
                
                refresh($scope.listData.organizationId);

                console.log($scope.$parent.listData.lists);

                $scope.$emit('sendWaitListNumber', {
                    wlist: $scope.$parent.listData.lists.length
                });

                $scope.$parent.$broadcast('listAdd', $scope.listData.organizationId);

                ngDialog.close();


                //$location.path('/userdash');
            });

            
            refresh($scope.listData.organizationId);

            console.log($scope.listData.lists);

        };

        function callAtInterval() {
            refresh($scope.listData.organizationId);
            console.log("refereshed called...");
        }


        $interval(callAtInterval, 300000);

        
        $scope.updateWaitList = function () {

            //alert("clicked");
 //debugger;
            $scope.isDisabled = true;
            $scope.loading = true;


            var status = $scope.guestDetails.waitingStatus;
            var guestId = $scope.guestDetails.guestId;

            if (status === 'Served') {
                $scope.guestDetails.servedTableNumber = $scope.guestDetails.selectedTable.tableNumber;
            } else {
                $scope.guestDetails.servedTableNumber = "";
            }
            

            //console.log($scope.guestDetails.servedTableNumber);

            /**/
            $http.post(serviceBase + 'api/waitlist/list/update/' + guestId, $scope.guestDetails).success(function (response) {
                $scope.loading = false;

                console.log($scope.guestDetails);

                //Refresh the list by reloading from database via $http call
                //
                //refresh($scope.listData.organizationId);
                $scope.$parent.$broadcast('refreshList', $scope.listData.organizationId);

                console.log($scope.listData.lists);
                //broadcast event
                //
                //console.log($scope.listData.organizationId);
                //alert($scope.listData.organizationId);
                $scope.$parent.$broadcast('listUpdate', $scope.listData.organizationId);



                //$scope.$broadcast('test', 'testing msg');
                //alert('event triggered from updating waitlist method!');

                ngDialog.close();
            });

            refresh($scope.listData.organizationId);
            console.log($scope.listData.lists);
            //alert($scope.listData.organizationId);
            //console.log($scope.listData.organizationId);
            //alert($scope.listData.organizationId);
            //$scope.$parent.$broadcast('listUpdate', $scope.listData.organizationId);
            
            //ngDialog.close();
        };

        //debugger;
        

        //debugger;
        var refreshTable = function (theData) {

            var promise = waitListService.getWaitList(theData);

            promise.success(function (data) {

                console.log($scope.listData.lists);

                $scope.listData.lists = data;
                console.log($scope.listData.lists.length);

                console.log($scope.listData.lists);
                //$scope.$emit('sendWaitListNumber', {
                //    wlist: data.length
                //});

            });
        };

        var refreshData = function (theData) { //retrieve all tables for the organization
            var promise = tableService.getAllTablesForOrg(theData);
            //console.log(theData + " from calling method");
            //debugger;
            promise.success(function (res) {
                $scope.tableData.tables = res;

                console.log("refereshed: " + $scope.tableData.tables);


                //$scope.$emit('sendTableNumber', {
                //    tlist: $scope.tableData.tables.length
                //});

            });
        };


        //debugger;

        $scope.refresh = function() { //Refresh the listing content manually

            refresh($scope.profile.orgainzationId);
        };


        //Test event sender
        //
        $scope.sendEvent = function() {
            //debugger;

            $scope.$broadcast('test', 'testing msg');
            alert('event triggered!');
            console.log('event triggered!');
        };


        $scope.$on('listUpdate', function (event, data) {
            //alert(data + " is received in dinning table controller for waitlist updates!");

            refresh(data);




            //console.log(data + " is received in dinning table controller for waitlist updates!");
        });


        $scope.$on('listAdd', function (event, data) {
            //alert(data + " is received in dinning table controller for waitlist updates!");

            refresh(data);
            //console.log(data + " is received in dinning table controller for waitlist updates!");
        });


        $scope.sendMsg = function () {  //Send SMS on client side, but not secure, so not being used at this moment
            //debugger;
            //Twilio.create('Messages', {
            //    from: '+16042434804',
            //    to: '+16046195810',
            //    body: 'Hi sms message from angularjs....'
            //});

            var accountSid = "";
            var authToken = "";

            var testEndPoint = "https://api.twilio.com/2010-04-01/Accounts/" + accountSid + "/SMS/Messages.json";
            var liveEndPoint = "https://api.twilio.com/2010-04-01/Accounts/" + accountSid + "/Messages.json";

            var data = {
                To: '+16042434804',
                From: '+16046195810',
                Body: 'Hi sms message from angularjs....'
            };

            $http({
                method: 'POST',
                url: testEndPoint,
                data: data,
                dataType: 'json',
                //contentType: 'application/x-www-form-urlencoded',
                transformRequest: function(obj) {
                    var str = [];
                    for (var p in obj)
                        str.push(encodeURIComponent(p) + "=" + encodeURIComponent(obj[p]));
                    return str.join("&");
                },
                headers: {
                    'Authorization': 'Basic ' + btoa(accountSid + ':' + authToken),
                    'Content-Type': 'application/x-www-form-urlencoded'
                }
            }).success(function (response) {
                //Send message
                //
                console.log(response);


            }).error(function(error) {
                console.log(error);
            });


        


            //return  smsMessagingService.sendMsg('+16042434804', '+16046195810', 'Hi sms message from angularjs....');
        };

        $scope.callGuest = function(to) {

            //debugger;

            $scope.isDisabled = true;
            $scope.loading = true;

            var gname = $scope.$parent.guestDetails.guestFirstName;

            console.log(gname);

            var data = {
                to: to,
                //msg: "Dear " + $scope.guestDetails.getuestFirstName + ", a table is available to you, please come to report to the reception. Thanks."
                name: gname
            };


            var guest = JSON.stringify(data);
            //$http.post(serviceBase + 'api/waitlist/list/msg/' + data).success(function(response) { //Production or live testing
            $http.post(serviceBase + 'api/waitlist/list/call/', $scope.guestDetails).success(function (response) { //dummy testing only for now
                $scope.loading = false;

                console.log("message sent to" + $scope.guestDetails.guestContactTel);
                //alert("message sent to " + $scope.guestDetails.guestContactTel);

                ngDialog.close();
            });
        };


        $scope.assignTable = function (tNo) { //For assigning an empty table to anonymour walk-in guests ONLY!

            //debugger;

            $scope.loading = true;

            var oid = $scope.listData.organizationId;

            $http.post(serviceBase + 'api/tablemanager/table/updateStatus/' + tNo + "/" + oid).success(function (response) {
                $scope.loading = false;

                $scope.$broadcast('tableUpdate', { message: "table status updated!" });

                //refreshData($scope.profile.orgainzationId);

                ngDialog.close();
            });


        };




        //*************************************
        //Modal Dialogs
        //*************************************


        //Show add waitlis
        //
        $scope.showAdd = function () {
            ngDialog.open(
                {
                    appendTo: '#listDialog',
                    templateUrl: 'app/waitlist/addNew.html',
                    className: 'ngdialog-theme-plain',
                    //controller: 'userdashController'
                    scope: $scope,
                    controller: 'userdashController'
                }
            );
        };

        //Show add walkin guests

        $scope.showWalkin = function () {

            var uname = $scope.fromCurrentUser.userName;

            var profile = userProfileService.getUserProfile(uname);

            profile.success(function (profileData) {

                $scope.profile = profileData;

                var theData = $scope.profile.orgainzationId;

                var tables = tableService.getAvailableTablesForOrg(theData);

                tables.success(function (response) {
                    $scope.emptyTables = response;
                    console.log($scope.emptyTables);
                });

            });

            ngDialog.open(
                {
                    appendTo: '#listDialog',
                    templateUrl: 'app/waitlist/wGuest.html',
                    className: 'ngdialog-theme-plain',
                    //controller: 'userdashController'
                    scope: $scope,
                    //controller: 'userdashController'
                    controller: 'dinningTableController'
                }
            );
        };


        //$scope.msg = "testing....";

        //$scope.clickMe = function() {
        //    alert("clicked");
        //};

        //Details of wait list entry
        //
        $scope.showDetails = function (gId) {

            var uname = $scope.fromCurrentUser.userName;

            var profile = userProfileService.getUserProfile(uname);

            profile.success(function (profileData) {

                var data = {
                    id: "",
                    gid: ""
                };

                $scope.profile = profileData;

                data.id = $scope.profile.orgainzationId;

                data.gid = gId;

                var promise = waitListService.getWaitGuest(data);

                promise.success(function (res) {

                    $scope.guestDetails = res;

                    console.log($scope.guestDetails);
                });

                var theData = $scope.profile.orgainzationId;

                var tables = tableService.getAvailableTablesForOrg(theData);

                tables.success(function(response) {
                    $scope.emptyTables = response;
                    console.log($scope.emptyTables);
                });

            });

            ngDialog.open(
                {
                    appendTo: '#listDialog',
                    templateUrl: 'app/waitlist/entryDetails.html',
                    className: 'ngdialog-theme-default',
                    controller: 'userdashController',
                    scope: $scope,
                    data: $scope.$parent.gusetDetails
                }
            );
        };

        //debugger;

        



        //$scope.$parent.authentication.orgId = $scope.companyInfo.orgId;

        //var companyId = $scope.profile.orgainzationId;


        //Load/Re-load data to referesh the view
        //

        //$scope.showAddTable = function () {
        //    ngDialog.open(
        //        {
        //            appendTo: '#tableDialog',
        //            templateUrl: 'app/dinningtable/addNew.html',
        //            className: 'ngdialog-theme-plain',
        //            //controller: 'userdashController'
        //            scope: $scope,
        //            controller: 'dinningTableController'
        //        }
        //    );
        //};


        //$scope.addNewTable = function () {

        //    $scope.isDisabled = true;
        //    $scope.loading = true;



        //};

        $scope.showCallGuest = function (gId) {
            //debugger;
            var uname = $scope.fromCurrentUser.userName;

            var profile = userProfileService.getUserProfile(uname);

            profile.success(function (profileData) {

                var data = {
                    id: "",
                    gid: ""
                };

                $scope.profile = profileData;

                data.id = $scope.profile.orgainzationId;

                data.gid = gId;

                var promise = waitListService.getWaitGuest(data);

                promise.success(function (res) {

                    $scope.guestDetails = res;

                    console.log($scope.guestDetails);
                });
            });

            ngDialog.open(
                {
                    appendTo: '#listDialog',
                    templateUrl: 'app/waitlist/callGuest.html',
                    className: 'ngdialog-theme-plain',
                    controller: 'userdashController',
                    scope: $scope,
                    data: $scope.$parent.gusetDetails
                }
            );
        };
        
        

        activate();

        function activate() { }
    }


})();
