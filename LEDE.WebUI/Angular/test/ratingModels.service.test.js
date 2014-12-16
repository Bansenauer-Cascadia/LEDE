'use strict';

describe('Service: ratingModels', function () {

  // load the service's module
  beforeEach(module('facultyApp'));

  // instantiate service
  var ratingModels;
  beforeEach(inject(function (_ratingModels_) {
    ratingModels = _ratingModels_
  }));

  describe('TaskRating Behavior', function () {
    it('Should have null scores when cleared', function () {
      var TaskRatingData = {
        "CScore": 1,
        "SScore": 3,
        "PScore": 1
      };
      var TaskRating = new ratingModels.TaskRating(TaskRatingData);

      TaskRating.Clear();

      expect(TaskRating.data.CScore).toBeNull();
      expect(TaskRating.data.SScore).toBeNull();
      expect(TaskRating.data.PScore).toBeNull();
    });
    it('Should be visible', function () {
      var TaskRating = new ratingModels.TaskRating();

      expect(TaskRating.IsVisible()).toBeTruthy();
    });
    it('Should detect when its empty', function() {
      var TaskRatingData = {
        CScore: null,
        PScore: null,
        SScore: null
      }

      var EmptyTaskRating = new ratingModels.TaskRating(TaskRatingData)
      var isRatingEmpty = EmptyTaskRating.IsEmpty();

      expect(isRatingEmpty).toBeTruthy();
    })
  });

  describe('OtherRating Behavior', function () {
    it('Should not be visible if it is an empty rating', function(){
      var OtherRatingData = {
        CScore: null,
        SScore: null,
        PScore: null
      }

      var OtherRating = new ratingModels.OtherRating(OtherRatingData);

      expect(OtherRating.IsVisible()).toBeFalsy();
    })
  });

  describe('ImpactRating Behavior', function() {
    it('Should be visible if its non empty', function() {
      var ImpactRatingData = {
        LScore: 2,
        SScore: null,
        PScore: null
      }

      var ImpactRating = new ratingModels.ImpactRating(ImpactRatingData)

      expect(ImpactRating.IsVisible()).toBeTruthy();
    })
    it('Should return true for IsEmpty if all ratings are null', function() {
      var emptyRatingData = {
        LScore: null,
        PScore: null,
        SScore: null
      }

      var emptyImpactRating = new ratingModels.ImpactRating(emptyRatingData);

      expect(emptyImpactRating.IsEmpty()).toBeTruthy();
    })
  })

});
