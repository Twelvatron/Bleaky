var BleakyApp = angular.module('BleakyApp');

BleakyApp.factory('_login', function ($http) {
    return {
        register: function(email, password) {
            $http.post('/Login/Register', { Email: email, Password: password });
        }
    };
});