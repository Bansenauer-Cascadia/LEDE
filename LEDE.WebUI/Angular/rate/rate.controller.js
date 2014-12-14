'use strict';

angular.module('facultyApp')
  .controller('RateCtrl', function ($scope, $filter, $resource, $timeout, ratingModels) {

      var CoreRatingsResource = $resource('http://localhost:65127/api/CoreRating/:VersID', { VersID: 1109 }, {
          'get': { method: 'GET', isArray: true }
      })
      var TaskVersionResource = $resource('http://localhost:65127/api/TaskVersion/:VersID', { VersID: 1109 })

      $scope.ratingModels = ratingModels
      $scope.TaskRatings = []
      $scope.OtherRatings = []

      $scope.TaskVersion = TaskVersionResource.get({ VersID: 1109 })
      $scope.TaskVersion.$promise.then(function(TaskVersion) {
          $scope.SeminarID = TaskVersion.SeminarID
      })

      var CoreRatings = CoreRatingsResource.get({ VersID: 1109 })

      CoreRatings.$promise.then(function (CoreRatings) {
          $scope.TaskRatings = $filter('filter')(CoreRatings, { SeminarID: $scope.SeminarID })
          $scope.OtherRatings = $filter('filter')(CoreRatings, function (rating) { return rating.SeminarID !== $scope.SeminarID })
      })

      $scope.SubmitRating = function () {
          CoreRatings.forEach(function (rating) {
              rating.$save(function (rating, putResponseHeaders) {

              })
          })
      }
  });






