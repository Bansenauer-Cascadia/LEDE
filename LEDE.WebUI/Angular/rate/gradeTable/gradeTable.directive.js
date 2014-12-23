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
                };

                scope.EnterContractedMode = function () {
                    scope.expanded = false;
                };

                scope.IsRatingVisible = function (rating) {
                    if (rating === undefined) return false; 
                    var existing = rating.IsExistingRating();
                    var empty = rating.IsEmpty();
                    return rating.IsExistingRating() || !rating.IsEmpty() || scope.expanded;
                };

                var partialsUrl = '../Angular/rate/gradeTable/partials/'
                scope.headerUrl = partialsUrl + 'coreRatingHeader.html';
                scope.rowUrl = partialsUrl + 'coreRatingRow.html';

                scope.EnterContractedMode();
            }
        };
    });
