'use strict';

describe('Service: coreRating', function () {

    // load the service's module
    beforeEach(module('facultyApp'));

    // instantiate service
    var taskRatingFactory;
    var desiredKey, desiredArgs, taskRating;
    var desiredData = {RatingID: 0, Score: 1};

    beforeEach(module(function ($provide) {
        taskRating = jasmine.createSpy('taskRatingSpy').and.callFake(function(){
            this.assignedResultsToThis = true;
        });
        $provide.value('taskRating', taskRating)
    }));

    beforeEach(inject(function (_taskRatingFactory_) {
        taskRatingFactory = _taskRatingFactory_;
    }));

    it("Calls taskRating constructor with correct fields for a Core Rating", function () {
        desiredKey = 'RatingID';
        desiredArgs = ['CScore', 'SScore', 'PScore'];

        var CoreRating = new taskRatingFactory.CoreRating(desiredData);
        var assignedResultsToThis = CoreRating.assignedResultsToThis;

        expect(taskRating).toHaveBeenCalledWith(desiredData, desiredKey, desiredArgs);
        expect(assignedResultsToThis).toBeTruthy();
    });
    it("Calls taskRating constructor with correct fields for an Impact Rating", function () {
        desiredKey = 'RatingID';
        desiredArgs = ['SScore', 'PScore', 'LScore'];

        var ImpactRating = new taskRatingFactory.ImpactRating(desiredData);
        var assignedResultsToThis = ImpactRating.assignedResultsToThis;

        expect(taskRating).toHaveBeenCalledWith(desiredData, desiredKey, desiredArgs);
        expect(assignedResultsToThis).toBeTruthy();
    });
    it("Calls taskRating constructor with correct fields for an Log Rating", function () {
        desiredKey = 'VersID';
        desiredArgs = ['NumEntries'];

        var LogRating = new taskRatingFactory.LogRating(desiredData);
        var assignedResultsToThis = LogRating.assignedResultsToThis;

        expect(taskRating).toHaveBeenCalledWith(desiredData, desiredKey, desiredArgs);
        expect(assignedResultsToThis).toBeTruthy();
    });
    it("Calls taskRating constructor with correct fields for an Reflection Rating", function () {
        desiredKey = 'VersID';
        desiredArgs = ['NumHours'];

        var ReflectionRating = new taskRatingFactory.ReflectionRating(desiredData);
        var assignedResultsToThis = ReflectionRating.assignedResultsToThis;

        expect(taskRating).toHaveBeenCalledWith(desiredData, desiredKey, desiredArgs);
        expect(assignedResultsToThis).toBeTruthy();
    });
});
