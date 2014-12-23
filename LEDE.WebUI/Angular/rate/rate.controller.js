'use strict';

angular.module('facultyApp')
    .controller('RateCtrl', function ($scope, $filter, taskGradeService) {
        $scope.VersID = 1067;
        $scope.SeminarID = 15;
        var gradeService = taskGradeService.Create($scope.VersID);

        this.GetGrade = function() {
            gradeService.GetGrade().then(function (grade) {
                $scope.grade = grade;
                $scope.grade.TaskCoreRatings = $filter('filter')(grade.CoreRatings, TaskRating);
                $scope.grade.OtherCoreRatings = $filter('filter')(grade.CoreRatings, OtherRating);
            }).catch(function(){
                $scope.errorFetchingGrade = true;
            });
        };

        $scope.SaveGrade = function () {
            gradeService.SaveGrade().then(function (grade) {
                $scope.errorSavingGrade = false;
            }).catch(function (error) {
                $scope.errorSavingGrade = true;
            })
        };

        var TaskRating = function (value) {
            return value.data.SeminarID === $scope.SeminarID;
        }

        var OtherRating = function (value) {
            return value.data.SeminarID !== $scope.SeminarID;
        }

        this.GetGrade();
    });






