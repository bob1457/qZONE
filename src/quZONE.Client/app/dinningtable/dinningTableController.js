(function () {
    'use strict';

    angular
        .module('qzone')
        .controller('dinningTableController', dinningTableController);

    dinningTableController.$inject = ['$scope', '$location', 'ngDialog', 
        'tableService', 'userProfileService', 'authService', '$http', 'serverBase'];

    function dinningTableController($scope, $location, ngDialog, tableService, 
        userProfileService, authService, $http, serverBase) {
        /* jshint validthis:true */
        var vm = this;
        vm.title = 'dinningTableController';

        var serviceBase = serverBase;

        $scope.fromCurrentUser = authService.authentication;

        var data = $scope.fromCurrentUser.userName;

        var promise = userProfileService.getUserProfile(data);

        promise.success(function (profileData) {

            $scope.profile = profileData;

            console.log($scope.profile);

            console.log($scope.profile.orgainzationId);

            //Call service to get waitlist
            //
            var companyId = $scope.profile.orgainzationId;

            $scope.orgId = $scope.profile.orgainzationId;

            console.log($scope.orgId);

            var promise = tableService.getAllTablesForOrg($scope.orgId);

            $scope.loading = true;
            promise.success(function (res) {
                $scope.tableData.tables = res;
                $scope.loading = false;
                console.log($scope.tableData.tables);

                $scope.$emit('sendTableNumber', {
                    tlist: $scope.tableData.tables.length
                });

            });

            //debugger;

            $scope.tableData = {
                tableNumber: "",
                tableSize: "",
                maxSeatsCapacity: "",
                //guestGroupSize: "",
                //waitingStatus: "Waiting",
                tableStatus: "Available",
                organizationId: ""
            };

            $scope.tableData.organizationId = companyId;

            console.log($scope.orgId);

            

        });

        var refreshData = function (theData) {
            var promise = tableService.getAllTablesForOrg(theData);
            //console.log(theData + " from calling method");
            //debugger;
            promise.success(function (res) {
                $scope.tableData.tables = res;
                //alert("ok");
                console.log("refereshed: " + $scope.tableData.tables);

                console.log($scope.tableData.tables);

                $scope.$emit('sendTableNumber', {
                    tlist: $scope.tableData.tables.length
                });

            });
        };

        
        $scope.assignTable = function (tNo) { //For assigning an empty table to anonymour walk-in guests ONLY!

            //debugger;

            $scope.loading = true;

            var oid = $scope.listData.organizationId;

            $http.post(serviceBase + 'api/tablemanager/table/updateStatus/' + tNo + "/" + oid).success(function (response) {
                $scope.loading = false;

                //$scope.$broadcast('tableUpdate', { message: "table status updated!" });

                refreshData($scope.profile.orgainzationId);

                ngDialog.close();
            });


        };






        $scope.addNewTable = function () {
//debugger;
            $scope.isDisabled = true;
            $scope.loading = true;

            //tableService.addNewTable.then(function () {

            //    $scope.loading = false;

            //    $scope.$parent.tableData.tables.push($scope.tableData);

            //    console.log($scope.$parent.tableData.tables);

            //    ngDialog.close();

            //});
            
            $http.post(serviceBase + 'api/tablemanager/table/add', $scope.tableData).success(function (response) {

                

                $scope.loading = false;
                $scope.isDisabled = false;

                $scope.tableData.tables.push($scope.tableData);

                refreshData($scope.profile.orgainzationId);

                console.log($scope.tableData.tables);
                //debugger;
                //ngDialog.close();
                //$scope.addTableForm.clear(); //not working
                
                $scope.addTableForm.$setPristine();
                document.getElementById("addTable").reset();
                refreshData($scope.tableData.organizationId);
                //$scope.tableData.tableSize = '';
                //$scope.tableData.maxSeatsCapacity = '';
                //$scope.tableData.tableNumber = '';



               

            });


        };

        $scope.getGuest = function (theData) { // Get guests for the table when occupied
            //debugger;
            var promise = tableService.getGuestDetails(theData + "/" + $scope.tableData.organizationId);
            promise.success(function(res) {
                $scope.tableGuestInfo = res;

                if (res === null) {
                    $scope.showGuuests = false;
                } else {
                    $scope.showGuuests = true;
                }

                //$scope.show = true;
                console.log($scope.tableGuestInfo);
            });
        };
        

        //debugger;

        $scope.$on('listUpdate', function (event, data) {
            //alert(data + " is received in dinning table controller for waitlist updates!");

            refreshData(data);
            //console.log(data + " is received in dinning table controller for waitlist updates!");
        });


        //testing event listener
        //
        $scope.$on('test', function (event, data) {
            alert(data + " received in dinning table controller!");
            console.log(data + "received in dinning table controller!");
            refreshData($scope.tableData.organizationId);
            alert("refresh done!");
            console.log("refresh done!");
        });

        
        //Receve and handle tabe status update broadcast
        //
//        $scope.$on("tableUpdate", function (event, args) {
//debugger;
//            refreshData($scope.tableData.organizationId);

//        //var promise = tableService.getAllTablesForOrg($scope.tableData.organizationId);
//        //    //console.log(theData + " from calling method");
//        //    //debugger;
//        //    promise.success(function (res) {
//        //        $scope.tableData.tables = res;
//        //        alert("ok inside event");
//        //        console.log("refereshed on event: " + $scope.tableData.tables);

//        //        console.log($scope.tableData.tables);

               

//        //    });


//            console.log($scope.tableData.tables); //data is not  refreshed or before refresh!
//        });


        //Refresh table contents - manual
        //
        $scope.refreshTableView = function () {
            //debugger;
            refreshData($scope.tableData.organizationId);
        };

        
        function clearTable(tableNo, oid ) {
            $http.post(serviceBase + 'api/tablemanager/table/clear/' + tableNo + "/" + oid).success(function (response) {
                
            });
        }

        $scope.cancelAddTable = function () {

            //Clear the form fields
            //$scope.addTableForm.clear(); //not working
            //document.getElementById("addTable").reset();
//debugger;
            $scope.addTableForm.$setPristine();

            $scope.tableData.tableSize = '';
            $scope.tableData.maxSeatsCapacity = '';
            $scope.tableData.tableNumber = '';

        };

        
        $scope.updateTable = function(tableDetails, newStatus) {

            //debugger;

            $scope.tableDetails = tableDetails;

            $scope.tableDetails.tableStatus = newStatus;

            var id = $scope.tableDetails.id;

            console.log($scope.tableDetails);

            $http.post(serviceBase + 'api/tablemanager/table/update/' + id, $scope.tableDetails).success(function (response) {

                //call a function to clear table number in the wait list in the right organiztion if the new status is Available
                if (newStatus === "Available") {
                    clearTable(tableDetails.tableNumber, $scope.orgId);
                }

                refreshData($scope.tableData.organizationId);
            });


        };

        
        $scope.getTalbeInfo = function (tableNumber) {

            var theData = tableNumber + "/" + $scope.orgId;

            var promise = tableService.getTableDetails(theData);

            promise.success(function(response) {

                $scope.tableGuestInfo = response;

                console.log($scope.tableGuestInfo);
            });


        };

        //Not in use
        $scope.editTalbeInfo = function (tableInfo) {

            //debugger;

            var id = $scope.orgId;

            $scope.tableInfo = tableInfo;

            console.log($scope.tableInfo);

            

            $http.post(serviceBase + 'api/tablemanager/table/update/' + id, $scope.tableInfo).success(function (response) {
                alert("updated successfully!");
                refreshData($scope.tableData.organizationId);

            });

        };

        $scope.popoverInfoBox = {
            //content: '',
            //animation: "false",
            //placement: "auto",
            
            templateUrl: 'popover-content.html'
        };
        


        activate();

        function activate() {
            
        }
    }
})();
