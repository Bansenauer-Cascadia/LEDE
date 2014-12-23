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
        var taskRatingWithData = new taskRating(taskRatingData);

        var data = taskRatingWithData.data;

        expect(data).toEqual(taskRatingData);
    });
    it("sets data to empty object if none is provided", function(){
        var taskRatingWithoutData = new taskRating();
        var emptyData = {};

        var data = taskRatingWithoutData.data;

        expect(data).toEqual(emptyData);
    });
    it("correctly determines if a rating is existing rating", function(){
        var existingTaskRatingData = {RatingID: 1};
        var existingTaskRating = new taskRating(existingTaskRatingData, 'RatingID', []);
        var newTaskRatingData = {RatingID: 0};
        var newTaskRating = new taskRating(newTaskRatingData, 'RatingID', []);

        var isExistingRatingExisting = existingTaskRating.IsExistingRating();
        var isNewRatingExisting = newTaskRating.IsExistingRating();

        expect(isExistingRatingExisting).toBeTruthy();
        expect(isNewRatingExisting).toBeFalsy();
    });
    it("sets all scores to null when Clear() is called", function () {
        var sampleRatingData = {RatingID: 1, CScore: 1, SScore: 3, PScore: 1};
        var taskRatingToClear = new taskRating(sampleRatingData, mockKey, mockFields);

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

        var emptyTaskRating = new taskRating(emptyRatingData, mockKey, mockFields);
        var isRatingEmpty = emptyTaskRating.IsEmpty();

        expect(isRatingEmpty).toBeTruthy();
    });
    it("returns false for IsEmpty() when at least 1 score isn't null", function () {
        var NonEmptyRatingData = [{CScore: 1}, {PScore: 1}, {SScore: 1}];
        var NonEmptyRatingModels = [];

        NonEmptyRatingData.forEach(function (data) {
            NonEmptyRatingModels.push(new taskRating(data, mockKey, mockFields))
        });

        NonEmptyRatingModels.forEach(function (model) {
            expect(model.IsEmpty()).toBeFalsy();
        })
    });
    it("RatingStatus() correctly returns 'deleted'", function () {
        var deletedTaskRating = new taskRating();
        spyOn(deletedTaskRating, "IsExistingRating").and.returnValue(true);
        spyOn(deletedTaskRating, "IsEmpty").and.returnValue(true);

        var DeletedStatus = deletedTaskRating.RatingStatus();

        expect(DeletedStatus).toEqual("deleted");
    });
    it("RatingStatus() correctly returns 'updated'", function () {
        var updatedTaskRating = new taskRating();
        spyOn(updatedTaskRating, "IsExistingRating").and.returnValue(true);
        spyOn(updatedTaskRating, "IsEmpty").and.returnValue(false);

        var UpdatedStatus = updatedTaskRating.RatingStatus();

        expect(UpdatedStatus).toEqual("updated");
    });
    it("RatingStatus() correctly returns 'created'", function () {
        var createdRating = new taskRating();
        spyOn(createdRating, "IsExistingRating").and.returnValue(false);
        spyOn(createdRating, "IsEmpty").and.returnValue(false);

        var CreatedStatus = createdRating.RatingStatus();

        expect(CreatedStatus).toEqual("created");
    });
    it("returns undefined for RatingStatus()", function () {
        var noActionRating = new taskRating();
        spyOn(noActionRating, "IsExistingRating").and.returnValue(false);
        spyOn(noActionRating, "IsEmpty").and.returnValue(true);

        var noStatus = noActionRating.RatingStatus();

        expect(noStatus).toBeUndefined()
    });
});
