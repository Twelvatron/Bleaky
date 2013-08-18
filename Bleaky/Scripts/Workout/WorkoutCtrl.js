var BleakyApp = angular.module('BleakyApp');

BleakyApp.controller('WorkoutCtrl', function ($scope) {
    $scope.AddWorkout = function () {
        $scope.showAddWorkout = true;
    };
});