'use strict';

angular.module('facultyApp')
    .controller('RateCtrl', function ($scope, $location, taskGradeService) {
        var params = $location.search();
        $scope.VersID = params.VersID;
        $scope.UploadMessage = params.Message;

        var gradeService = taskGradeService.Create($scope.VersID);

        this.GetGrade = function() {
            gradeService.GetGrade().then(function (grade) {
                $scope.grade = grade;                
            }).catch(function(){
                $scope.errorFetchingGrade = true;
            });
        };

        $scope.SaveGrade = function () {
            $scope.errorSavingGrade = undefined;
            gradeService.SaveGrade().then(function (grade) {
                $scope.errorSavingGrade = false;
            }).catch(function (error) {
                $scope.errorSavingGrade = true;
            })
        };        

        this.GetGrade();
    });






