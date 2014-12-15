'use strict';

angular.module('facultyApp')
  .controller('RateCtrl', function ($scope, $filter, $resource, $timeout, $q, ratingModels, apiUrl) {

      var CoreRatingsResource = $resource(apiUrl + 'CoreRating/:VersID', { VersID: 1109 }, {
          get: { method: 'GET', isArray: true },
          update: { method: 'PUT' }
      })
      var TaskVersionResource = $resource(apiUrl + 'TaskVersion/:VersID', { VersID: 1109 })

      $scope.ratingModels = ratingModels
      $scope.TaskRatings = []
      $scope.OtherRatings = []

      TaskVersionResource.get({ VersID: 1109 }).$promise.then(function (TaskVersion) {
          $scope.TaskVersion = TaskVersion
          return CoreRatingsResource.get({ VersID: 1109 }).$promise
      })
      .then(function (CoreRatings) {
          $scope.CoreRatings = CoreRatings
          $scope.TaskRatings = $filter('filter')(CoreRatings, { SeminarID: $scope.TaskVersion.SeminarID })
          $scope.OtherRatings = $filter('filter')(CoreRatings, function (rating) { return rating.SeminarID !== $scope.TaskVersion.SeminarID })
      })
      .catch(function (error) {
          console.log('error: ' + error)
      })

      $scope.SubmitRating = function () {
          $scope.SaveStatus = 'pending'
          var RatingPromises = [];
          $scope.CoreRatings.forEach(function (rating) {
              RatingPromises.push(GetRestPromise(rating))
          })
          $q.all(RatingPromises).then(function (data) {
              $scope.SaveStatus = 'success'
          }, function (error) {
              console.log(error)
          })
      }

      var GetRestPromise = function (rating) {
          if (rating.RatingID === 0) {
              return rating.$save().$promise
          }
          else {
              return rating.$update().$promise
          }
      }
  });






