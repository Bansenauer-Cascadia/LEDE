'use strict';

angular.module('facultyApp')
  .directive('gradeTable', function ($filter, ratingModels) {
      return {
          templateUrl: '/Angular/rate/gradeTable/gradeTable.html',
          scope: {
              allRatings: '=ratings',
              isImpactRating: '=',
              ratingType: '@',
          },
          restrict: 'EA',
          link: function (scope, element, attrs) {
              scope.EnterExpandedMode = function () {
                  scope.expanded = true
                  scope.visibleRatings = scope.allRatings
              }

              scope.EnterContractedMode = function () {
                  scope.expanded = false
                  scope.visibleRatings = $filter('filter')(scope.allRatings, function (rating) { return rating.IsVisible() }, true)
              }

              scope.NoRatingsToShow = function () {
                  return scope.allRatings.length === scope.visibleRatings.length
              }

              scope.NoRatingsToHide = function () {
                  var ratingsToHide = $filter('filter')(scope.allRatings, function (rating) { return !rating.IsVisible() }, true)
                  return ratingsToHide.length === 0
              }

              scope.IsHeaderVisible = function () {
                  return scope.expanded || scope.visibleRatings.length > 0
              }

              var FetchTableHtml = function () {
                  var partialsUrl = '/Angular/rate/gradeTable/partials/'
                  if (scope.isImpactRating) {
                      scope.headerUrl = partialsUrl + 'impactRatingHeader.html'
                      scope.rowUrl = partialsUrl + 'impactRatingRow.html'
                  }
                  else {
                      scope.headerUrl = partialsUrl + 'coreRatingHeader.html'
                      scope.rowUrl = partialsUrl + 'coreRatingRow.html'
                  }
              }

              var InitializeRatingObjects = function () {
                  var ratingObjects = []
                  scope.allRatings.forEach(function(rating) {
                      ratingObjects.push(new ratingModels[scope.ratingType](rating))
                  })
                  scope.allRatings = ratingObjects
                  scope.EnterContractedMode()
              }

              FetchTableHtml()
              InitializeRatingObjects()

              scope.$watch(function () { return scope.allRatings.length }, function (newval, oldval) {
                  InitializeRatingObjects()
              })
          }
      };
  });
