'use strict';

angular.module('facultyApp')
  .controller('RateCtrl', function ($scope, $filter, $resource, apiUrl, coreRatings) {

      var TaskVersionResource = $resource(apiUrl + 'TaskVersion/:VersID', { VersID: 1109 });

      $scope.TaskRatings = [];
      $scope.OtherRatings = [];

      TaskVersionResource.get({ VersID: 1109 }).$promise.then(function (TaskVersion) {
          $scope.TaskVersion = TaskVersion;
          coreRatings.GetAll(TaskVersion.VersID, function(CoreRatings) {
              $scope.CoreRatings = CoreRatings;
              $scope.TaskRatings = $filter('filter')(CoreRatings, { SeminarID: $scope.TaskVersion.SeminarID });
              $scope.OtherRatings = $filter('filter')(CoreRatings, function (rating) {
                  return rating.SeminarID !== $scope.TaskVersion.SeminarID
              });
          })
      });

        $scope.SubmitRating = function () {
          coreRatings.SaveAll($scope.CoreRatings, function() {
              $scope.SaveStatus = 'complete';
          })
      }

  });






