var BleakyApp = angular.module('BleakyApp', []);

BleakyApp.controller('LoginCtrl', function ($scope) {
    $scope.verifyRegistration = function () {
        console.log($scope.inputEmail);
        console.log($scope.inputPassword);
        console.log($scope.inputConfirmPassword);
    };
});