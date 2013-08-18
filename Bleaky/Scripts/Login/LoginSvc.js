var BleakyApp = angular.module('BleakyApp');

BleakyApp.factory('_login', function ($http) {
    return {
        logIn: function (email, password) {
            return $http.post('/login?returnUrl=/workout', { Email: email, Password: password });
        },
        register: function(email, password) {
            return $http.post('/login/register?returnUrl=/workout', { Email: email, Password: password });
        }
    };
});