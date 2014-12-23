'use strict';

angular.module('facultyApp')
    .directive('gradeTable', function ($filter) {
        return {
            templateUrl: '/Angular/rate/gradeTable/gradeTable.html',
            scope: {
                allRatings: '=ratings'
            },
            restrict: 'EA',
            link: function (scope, element, attrs) {
                if (!scope.allRatings)
                    scope.allRatings = [];

                scope.EnterExpandedMode = function () {
                    scope.expanded = true;
                    scope.visibleRatings = scope.allRatings
                };

                scope.EnterContractedMode = function () {
                    scope.expanded = false;
                    scope.visibleRatings = $filter('filter')(scope.allRatings, scope.IsRatingVisible, true)
                };

                scope.NoRatingsToShow = function () {
                    return scope.allRatings.length === scope.visibleRatings.length
                };

                scope.NoRatingsToHide = function () {
                    var ratingsToHide = $filter('filter')(scope.allRatings, !scope.IsRatingVisible, true);
                    return ratingsToHide.length === 0
                };

                scope.IsHeaderVisible = function () {
                    return scope.expanded || scope.visibleRatings.length > 0
                };

                scope.IsRatingVisible = function(rating) {
                  return rating.IsExistingRating() || !rating.IsEmpty()
                };

                var partialsUrl = '../Angular/rate/gradeTable/partials/'
                scope.headerUrl = partialsUrl + 'coreRatingHeader.html';
                scope.rowUrl = partialsUrl + 'coreRatingRow.html';

                scope.EnterContractedMode();
            }
        };
    });
