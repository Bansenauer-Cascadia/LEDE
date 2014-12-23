'use strict';

angular.module('facultyApp')
    .factory('taskRatingFactory', function (taskRating) {
        var CoreRating = function (data) {
            taskRating.call(this, data, 'RatingID', ['CScore', 'SScore', 'PScore']);
        };
        var ImpactRating = function(data) {
            taskRating.call(this, data, 'RatingID', ['SScore', 'PScore', 'LScore']);
        };
        var LogRating = function(data) {
            taskRating.call(this, data, 'VersID', ['NumEntries']);
        };
        var ReflectionRating = function(data) {
            taskRating.call(this, data, 'VersID', ['NumHours']);
        };
        return {
            CoreRating: CoreRating,
            ImpactRating: ImpactRating,
            LogRating: LogRating,
            ReflectionRating: ReflectionRating
        };
    });
