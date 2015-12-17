'use strict';

describe('Service: taskRating', function () {

    // load the service's module
    beforeEach(module('facultyApp'));

    // instantiate service
    var taskRating;
    var mockFields = ['CScore', 'SScore', 'PScore'];
    var mockKey = 'RatingID';
    beforeEach(inject(function (_taskRating_) {
        taskRating = _taskRating_;
    }));

    it("sets data to parameter provided", function(){
        var taskRatingData = {foo: 'asdf', bar: 'fdsa'};
        var taskRatingWithData = new taskRating(null, null, taskRatingData);

        var data = taskRatingWithData.data;

        expect(data).toEqual(taskRatingData);
    });
    it("correctly determines if a rating is existing rating", function(){
        var existingTaskRatingData = {RatingID: 1};
        var existingTaskRating = new taskRating('RatingID', [], existingTaskRatingData);
        var newTaskRatingData = {RatingID: 0};
        var newTaskRating = new taskRating('RatingID', [], newTaskRatingData);

        var isExistingRatingExisting = existingTaskRating.IsExistingRating();
        var isNewRatingExisting = newTaskRating.IsExistingRating();

        expect(isExistingRatingExisting).toBeTruthy();
        expect(isNewRatingExisting).toBeFalsy();
    });
    it("sets all scores to null when Clear() is called", function () {
        var sampleRatingData = {RatingID: 1, CScore: 1, SScore: 3, PScore: 1};
        var taskRatingToClear = new taskRating(mockKey, mockFields, sampleRatingData);

        taskRatingToClear.Clear();

        expect(taskRatingToClear.data.CScore).toBeNull();
        expect(taskRatingToClear.data.SScore).toBeNull();
        expect(taskRatingToClear.data.PScore).toBeNull();
    });
    it("returns true for IsEmpty() when all scores are null", function () {
        var emptyRatingData = {
            RatingID: 1,
            CScore: null,
            PScore: null,
            SScore: null
        };

        var emptyTaskRating = new taskRating(mockKey, mockFields, emptyRatingData);
        var isRatingEmpty = emptyTaskRating.IsEmpty();

        expect(isRatingEmpty).toBeTruthy();
    });
    it("returns false for IsEmpty() when at least 1 score isn't null", function () {
        var NonEmptyRatingData = [{CScore: 1}, {PScore: 1}, {SScore: 1}];
        var NonEmptyRatingModels = [];

        NonEmptyRatingData.forEach(function (data) {
            NonEmptyRatingModels.push(new taskRating(mockKey, mockFields, data))
        });

        NonEmptyRatingModels.forEach(function (model) {
            expect(model.IsEmpty()).toBeFalsy();
        })
    });
});
