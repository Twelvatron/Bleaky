var BleakyApp = angular.module('BleakyApp');

BleakyApp.factory('_login', function ($http) {
    return {
        register: function(email, password) {
            return $http.post('/login/register', { Email: email, Password: password });
        }
    };
});