var BleakyApp = angular.module('BleakyApp');

BleakyApp.controller('WorkoutCtrl', function ($scope) {

    $scope.days = [1, 2, 3, 4, 5, 6, 7];
    $scope.inputWorkoutFrequency = 1;

    $scope.AddWorkout = function () {
        var addWorkoutWizDiv = $('#blAddWorkoutWiz');
        addWorkoutWizDiv.slideToggle();
    };

    $scope.AddDays = function () {
        var addWorkoutDiv = $('.blAddWorkoutDiv');
        var addDaysDiv = $('.blAddDaysDiv');
        addWorkoutDiv.slideToggle();
        addDaysDiv.slideToggle();
    };
});