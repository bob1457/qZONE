(function() {
    'use strict';

    angular
        .module('qzone')
        .directive('pageHeader', pageHeader);

    pageHeader.$inject = ['$http', 'serverBase'];
    
    function pageHeader($http, serverBase) {
        // Usage:
        //     <pageHeaderDirective></pageHeaderDirective>
        // Creates:
        // 

        var serviceBase = serverBase;


        

        var directive = {
            templateUrl: "app/layout/pageHeaderTemplate.html",
            scope: false,
            link: link,
            restrict: 'EA'
        };
        return directive;

        function link(scope, element, attrs) {
            //debugger;
            //scope.companyInfo = scope.$parent.authentication; 

            //console.log(scope.companyInfo);

            //// call server to get custom data to populate directive elements
            ////
            //debugger;

            //var data = scope.companyInfo.orgId;
            //console.log(data);

            ////


            //$http.get(serviceBase + 'api/profile/organization/' + data).success(function (response) {
            //    //scope.profile = response;
            //    //scope.$parent.authentication.userAvatarImgUrl = scope.profile.avatarImgUrl;
            //    //scope.$parent.authentication.orgId = scope.profile.orgainzationId;

            //    scope.orgInfo = response;
            //    console.log(scope.orgInfo);
            //    //$scope.$parent.authentication.orgId = response.orgainzationId;

            //});


        }
    }

})();