'use strict';

angular.module('facultyApp').factory('taskRating', function () {
    var taskRating = function (PrimaryKey, ScoreFields, Data) {
        this.scoreFields = ScoreFields;
        this.primaryKey = PrimaryKey;
        this.data = Data;
    };

    taskRating.prototype = {

        IsExistingRating: function () {
            return this.data[this.primaryKey] > 0;
        },

        IsEmpty: function () {
            for (var i = 0; i < this.scoreFields.length; i++) {
                if (this.data[this.scoreFields[i]] !== null) return false;
            }
            return true;
        },

        Clear: function () {
            this.scoreFields.forEach(function (field) {
                this.data[field] = null;
            }.bind(this));
        },

        IsSeminarRating: function (SeminarID) {
            return this.data.SeminarID === SeminarID;
        }
    };

    return taskRating;
});
