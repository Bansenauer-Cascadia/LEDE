/**
 * Created by jonny on 12/17/14.
 */
angular.module('facultyApp').factory('impactRating', function () {
    var ImpactRating = this.ImpactRating = function (data) {
        TaskRating.call(this, data)
    };
    ImpactRating.prototype = Object.create(TaskRating.prototype);
    ImpactRating.prototype.constructor = ImpactRating;
    ImpactRating.prototype.Clear = function () {
        this.data.SScore = null
        this.data.PScore = null
        this.data.LScore = null
    };
    ImpactRating.prototype.IsEmpty = function () {
        return this.data.SScore == null && this.data.PScore == null && this.data.LScore == null
    };

    return ImpactRating;
});
