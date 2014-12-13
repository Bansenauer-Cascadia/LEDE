'use strict';

angular.module('facultyApp')
  .factory('ratingModels', function () {
    var TaskRating = this.TaskRating = function (data) {
      if(data) this.data = data
      else this.data = {}
      this.visible = true
    }
    TaskRating.prototype.Clear = function () {
      this.data.CScore = this.data.SScore = this.data.PScore = null
    }
    TaskRating.prototype.IsEmpty = function() {
      return this.data.SScore === null && this.data.PScore === null && this.data.CScore === null
    }
    TaskRating.prototype.IsVisible = function() {
      return this.visible || !this.IsEmpty()
    }

    var OtherRating = this.OtherRating = function(data) {
      TaskRating.call(this, data)
      this.visible = false
    }
    OtherRating.prototype = Object.create(TaskRating.prototype)
    OtherRating.prototype.constructor = OtherRating

    var ImpactRating = this.ImpactRating = function(data) {
      OtherRating.call(this, data)
    }
    ImpactRating.prototype = Object.create(OtherRating.prototype)
    ImpactRating.prototype.constructor = ImpactRating
    ImpactRating.prototype.Clear = function() {
      this.data.SScore = null
      this.data.PScore = null
      this.data.LScore = null
    }
    ImpactRating.prototype.IsEmpty = function() {
     return this.data.SScore == null && this.data.PScore == null && this.data.LScore == null
    }

    return {
      TaskRating: TaskRating,
      OtherRating: OtherRating,
      ImpactRating: ImpactRating
    };
  });
