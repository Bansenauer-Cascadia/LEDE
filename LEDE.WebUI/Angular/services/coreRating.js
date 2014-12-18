'use strict';

angular.module('facultyApp')
    .factory('coreRating', function () {
        var CoreRating = this.CoreRating = function (data) {
            if (data) this.data = data;
            else this.data = {};
        };
        CoreRating.prototype.Clear = function () {
            this.data.CScore = this.data.SScore = this.data.PScore = null
        };
        CoreRating.prototype.IsEmpty = function () {
            return this.data.SScore === null && this.data.PScore === null && this.data.CScore === null
        };
        CoreRating.prototype.IsExistingRating = function() {
            return this.data.RatingID > 0;
        };

        CoreRating.prototype.RatingStatus = function() {
            var ratingStatus;
            if(this.IsExistingRating()) {
                if(this.IsEmpty()) ratingStatus = 'deleted';
                else ratingStatus = 'updated';
            }
            else if(!this.IsEmpty()) {
                ratingStatus = 'created';
            }
            return ratingStatus;
        };

        return {
            Create: function(data) {
                return new CoreRating(data)
            }
        };
    });
