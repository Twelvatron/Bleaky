var BleakyApp = angular.module('BleakyApp');

BleakyApp.factory('_login', function ($http) {
    return {
        register: function(userName, password) {
            $http.post('/Login/Register', { UserName: userName, Password: password });
        }
    };
});