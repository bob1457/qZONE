(function () {
    'use strict';

    angular
        .module('qzone')
        .factory('smsMessagingService', smsMessagingService);

    smsMessagingService.$inject = ['$http'];

    function smsMessagingService($http) {
        //var service = {
        //    getData: getData
        //};

        //return service;

        var smsServiceFactory = {};

        var _sendMessage = function() {

            var accountSid = "";
            var authToken = "";

            var testEndPoint = "https://api.twilio.com/2010-04-01/Accounts/' + accountSid + '/SMS/Messages.json";
            var liveEndPoint = "https://api.twilio.com/2010-04-01/Accounts/' + accountSid + '/Messages.json";

            var data = {
                To: to,
                From: from,
                Body: body
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


        };


        smsServiceFactory.sendMessage = _sendMessage;


        return smsServiceFactory;

        //function getData() { }
    }
})();