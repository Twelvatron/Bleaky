var BleakyApp = angular.module('BleakyApp');

BleakyApp.controller('LoginCtrl', function ($scope, $window, _login) {
    $scope.verifyRegistration = function () {
        $scope.hasError = false;

        if (!$scope.isValidEmail($scope.inputEmail)) {
            $scope.hasError = true;
            $scope.errorMessage = 'The email you entered is not valid, please try again.';
            return;
        }

        if ($scope.inputPassword.length < 7) {
            $scope.hasError = true;
            $scope.errorMessage = 'Please enter a password that is longer than 6 characters';
            return;
        }

        if ($scope.inputPassword != $scope.inputConfirmPassword) {
            $scope.hasError = true;
            $scope.errorMessage = 'The passwords you entered differ, please try again.';
            return;
        }

        console.log(_login.register);
        _login.register($scope.inputEmail, $scope.inputPassword).success(function (result) {
            if (result.Success == false) {
                $scope.hasError = true;
                $scope.errorMessage = result.Message;
            }
            else {
                $window.location.href = '/workouts';
            }
        }).error(function () {
        });
    };

    $scope.isValidEmail = function (email) {
        var regEx = /^(([^<>()[\]\\.,;:\s@\"]+(\.[^<>()[\]\\.,;:\s@\"]+)*)|(\".+\"))@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\])|(([a-zA-Z\-0-9]+\.)+[a-zA-Z]{2,}))$/;
        return regEx.test(email);
    };
});