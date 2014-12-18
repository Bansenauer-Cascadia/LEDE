'use strict';

describe('Service: coreRating', function () {

    // load the service's module
    beforeEach(module('facultyApp'));

    // instantiate service
    var coreRating;
    beforeEach(inject(function (_coreRating_) {
        coreRating = _coreRating_
    }));

    it("sets all scores to null when Clear() is called", function () {
        var CoreRatingData = {CScore: 1, SScore: 3, PScore: 1};
        var CoreRating = coreRating.Create(CoreRatingData);

        CoreRating.Clear();

        expect(CoreRating.data.CScore).toBeNull();
        expect(CoreRating.data.SScore).toBeNull();
        expect(CoreRating.data.PScore).toBeNull();
    });
    it("returns true for IsEmpty() when all scores are null", function () {
        var CoreRatingData = {
            CScore: null,
            PScore: null,
            SScore: null
        };

        var EmptyCoreRating = coreRating.Create(CoreRatingData);
        var isRatingEmpty = EmptyCoreRating.IsEmpty();

        expect(isRatingEmpty).toBeTruthy();
    });
    it("returns false for IsEmpty() when at least 1 score isn't null", function () {
        var NonEmptyRatingData = [{CScore: 1}, {PScore: 1}, {SScore: 1}];
        var NonEmptyRatingModels = [];

        NonEmptyRatingData.forEach(function (data) {
            NonEmptyRatingModels.push(coreRating.Create(data))
        });

        NonEmptyRatingModels.forEach(function (model) {
            expect(model.IsEmpty()).toBeFalsy();
        })
    });
    it("RatingStatus() correctly returns 'deleted'", function () {
        var DeletedRatingData = {RatingID: 1, CScore: null, PScore: null, SScore: null};
        var DeletedRatingModel = coreRating.Create(DeletedRatingData);

        var DeletedStatus = DeletedRatingModel.RatingStatus();

        expect(DeletedStatus).toEqual("deleted");
    });
    it("RatingStatus() correctly returns 'updated'", function () {
        var UpdatedRatingData = [{RatingID: 1, CScore: 1}, {RatingID: 1, SScore: 1}, {RatingID: 1, PScore: 1}];
        var UpdatedRatingModels = [];

        UpdatedRatingData.forEach(function (data) {
            UpdatedRatingModels.push(coreRating.Create(data));
        });

        UpdatedRatingModels.forEach(function (model) {
            expect(model.RatingStatus()).toEqual("updated");
        });
    });
    it("RatingStatus() correctly returns 'created'", function () {
        var CreatedRatingData = [{RatingID: 0, CScore: 1}, {RatingID: 0, SScore: 1}, {RatingID: 0, PScore: 1}];
        var CreatedRatingModels = [];

        CreatedRatingData.forEach(function (data) {
            CreatedRatingModels.push(coreRating.Create(data));
        });

        CreatedRatingModels.forEach(function (model) {
            expect(model.RatingStatus()).toEqual("created");
        });
    });
    it("returns undefined for RatingStatus()", function () {
        var EmptyRatingModel = coreRating.Create({RatingID: 0, CScore:null, SScore:null, PScore: null});

        var EmptyRatingStatus = EmptyRatingModel.RatingStatus();

        expect(EmptyRatingStatus).toBeUndefined();
    });
});
