'use strict';

angular.module('facultyApp')
    .directive('coreRatingRow', function () {
        return {
            templateUrl: 'Angular/directives/coreRatingRow.html',
            scope: {
                rating: '='
            },
            restrict: 'A',
            replace: true,
        }
    });