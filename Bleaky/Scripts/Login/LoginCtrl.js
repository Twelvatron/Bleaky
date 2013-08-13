﻿var BleakyApp = angular.module('BleakyApp');

BleakyApp.controller('LoginCtrl', function ($scope, _login) {
    $scope.verifyRegistration = function () {
        $scope.hasError = false;

        if (!$scope.isValidEmail($scope.inputEmail)) {
            $scope.hasError = true;
            $scope.errorMessage = 'The email you entered is not valid, please try again.';
            return;
        }

        if ($scope.inputPassword != $scope.inputConfirmPassword) {
            $scope.hasError = true;
            $scope.errorMessage = 'The passwords you entered differ, please try again.';
            return;
        }
        
        _login.register($scope.inputEmail, $scope.inputPassword);
    };

    $scope.isValidEmail = function (email) {
        var regEx = /^(([^<>()[\]\\.,;:\s@\"]+(\.[^<>()[\]\\.,;:\s@\"]+)*)|(\".+\"))@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\])|(([a-zA-Z\-0-9]+\.)+[a-zA-Z]{2,}))$/;
        return regEx.test(email);
    };
});