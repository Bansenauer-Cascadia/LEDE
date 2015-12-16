'use strict';

angular.module('facultyApp')
    .controller('RateCtrl', function ($scope, $location, $timeout, $window, gradeService) {
        var params = $location.search();
        $scope.VersID = params.VersID;
        $scope.UploadMessage = params.Message;

        var gradeResource = gradeService.Create($scope.VersID);

        this.GetGrade = function () {
            $scope.errorFetchingGrade = undefined;
            gradeResource.GetGrade().then(function (grade) {
                $scope.errorFetchingGrade = false;
                $scope.SeminarID = grade.TaskVersion.data;
                $scope.grade = grade;                
            }).catch(function(){
                $scope.errorFetchingGrade = true;
            });
        };

        $scope.SaveGrade = function () {
            $scope.errorSavingGrade = undefined;
            gradeResource.SaveGrade($scope.grade).then(function () {
                $scope.errorSavingGrade = false;
                $timeout(redirectToHome, 500);
            }).catch(function () {
                $scope.errorSavingGrade = true;
            })
        };

        var redirectToHome = function () {
            $window.location.href = '/';
        }

        this.GetGrade();
    });






