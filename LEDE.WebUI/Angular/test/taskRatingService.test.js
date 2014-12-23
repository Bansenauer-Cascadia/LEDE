'use strict';

describe('taskRatingService', function () {
    var taskRatingService;
    var ratingModel, ratingDataResource;
    var $rootScope, $q;

    beforeEach(module('facultyApp'));

    beforeEach(function () {
        ratingModel = function(data) {
            this.isRatingObject = true;
            this.data = data;
        };

        ratingDataResource = {
            GetAll: function (VersID) {
            },
            Save: function (taskRating) {
            },
            Update: function (taskRating) {
            },
            Delete: function (ratingID) {
            }
        }
    });

    beforeEach(inject(function (_$q_, _$rootScope_) {
        $rootScope = _$rootScope_;
        $q = _$q_;
    }));

    describe("GetAll", function () {
        var successParameter, errorParameter, mockGetData, mockGetError;

        beforeEach(inject(function (_taskRatingService_) {
            mockGetData = [{RatingID: 1, CScore: 1, SScore: null, PScore: 2},
                {RatingID: 2, CScore: 3, SScore: 2, PScore: null}];
            mockGetError = 'server error';
            successParameter = 1;
            errorParameter = 2;

            spyOn(ratingDataResource, "GetAll").and.callFake(function (VersID) {
                var deferred = $q.defer();
                if (VersID == successParameter) {
                    deferred.resolve(mockGetData);
                }
                else if (VersID == errorParameter) {
                    deferred.reject(mockGetError);
                }
                return deferred.promise;
            });

            taskRatingService = new _taskRatingService_(ratingModel, ratingDataResource);
        }));
        it("sets and returns this.taskRatingModels when calling ratingDataResource successfully",
            function () {
                var expectedObjectsArray = mockGetData.map(function (data) {
                    return new ratingModel(data);
                });
                var returnedObjectsArray;

                taskRatingService.GetAll(successParameter).then(function (data) {
                    returnedObjectsArray = data;
                });
                $rootScope.$apply();

                expect(ratingDataResource.GetAll).toHaveBeenCalledWith(successParameter);
                expect(taskRatingService.taskRatingModels).toEqual(expectedObjectsArray);
            });
        it("returns an error when calling ratingDataResource unsuccessfully",
            function () {
                var expectedError = mockGetError;
                var returnedError;

                taskRatingService.GetAll(errorParameter).then(null, function (error) {
                    returnedError = error;
                });
                $rootScope.$apply();

                expect(returnedError).toEqual(expectedError);
                expect(ratingDataResource.GetAll).toHaveBeenCalledWith(errorParameter);
            });
        describe("GetSingle()", function(){
            beforeEach(function(){
               mockGetData = {RatingID: 1, CScore: 1, SScore: null, PScore: 2};
            });
            it("sets appropriate object when ratingDataResource is called successfully", function(){
                var expectedObject = new ratingModel(mockGetData);
                var returnedObject;

                taskRatingService.GetSingle(successParameter).then(function(data){
                    returnedObject = data
                });
                $rootScope.$apply();

                expect(ratingDataResource.GetAll).toHaveBeenCalledWith(successParameter);
                expect(taskRatingService.taskRatingModels).toEqual([expectedObject]);
            });
            it("returns an error when getSingle is called unsuccessfully", function(){
                var expectedError = mockGetError;
                var returnedError;

                taskRatingService.GetSingle(errorParameter).then(null, function(error) {
                    returnedError = error;
                });
                $rootScope.$apply();

                expect(returnedError).toEqual(expectedError);
            });
        });
    });
    describe("SaveAll()", function () {
        var validSaveRating, validUpdateRating, validDeleteRating, invalidRating, doNothingRating, mockSaveError;
        beforeEach(inject(function (_taskRatingService_) {
            validSaveRating = {
                data: {RatingID: 1}, RatingStatus: function () {
                    return 'created'
                }
            };
            validUpdateRating = {
                data: {RatingID: 2}, RatingStatus: function () {
                    return 'updated'
                }
            };
            validDeleteRating = {
                data: {RatingID: 2}, RatingStatus: function () {
                    return 'deleted'
                }
            };
            doNothingRating = {
              data:null, RatingStatus: function() {}
            };
            invalidRating = {data: {RatingID: 'invalid'}};
            mockSaveError = 'server error';

            spyOn(ratingDataResource, "Save").and.callFake(function (ratingData) {
                var deferred = $q.defer();
                if (ratingData == validSaveRating.data) {
                    deferred.resolve();
                }
                else if (ratingData == invalidRating.data) {
                    deferred.reject(mockSaveError);
                }
                return deferred.promise;
            });
            spyOn(ratingDataResource, "Update").and.callFake(function (ratingData) {
                var deferred = $q.defer();
                if (ratingData == validUpdateRating.data) {
                    deferred.resolve();
                }
                else if (ratingData == invalidRating.data) {
                    deferred.reject(mockSaveError);
                }
                return deferred.promise;
            });
            spyOn(ratingDataResource, "Delete").and.callFake(function (ratingID) {
                var deferred = $q.defer();
                if (ratingID == validDeleteRating.data.RatingID) {
                    deferred.resolve();
                }
                else if (ratingID == invalidRating.data.RatingID) {
                    deferred.reject(mockSaveError);
                }
                return deferred.promise;
            });
            taskRatingService = new _taskRatingService_(ratingModel, ratingDataResource)
        }));

        it("Calls appropriate ratingDataResource methods for data models", function () {
            taskRatingService.taskRatingModels = [validSaveRating, validUpdateRating, validDeleteRating, doNothingRating];

            taskRatingService.SaveAll();

            expect(ratingDataResource.Save).toHaveBeenCalledWith(validSaveRating.data);
            expect(ratingDataResource.Update).toHaveBeenCalledWith(validUpdateRating.data);
            expect(ratingDataResource.Delete).toHaveBeenCalledWith(validDeleteRating.data.RatingID);
        });
        it("Resolves returned promise when all resources are saved successfully", function () {
            taskRatingService.taskRatingModels = [validSaveRating, validUpdateRating, validDeleteRating, doNothingRating];
            var savedSuccessfully;

            taskRatingService.SaveAll()
                .then(function () {
                    savedSuccessfully = true;
                });
            $rootScope.$apply();

            expect(savedSuccessfully).toBeTruthy();
        });
        it("Rejects returned promise with error message when all resources aren't saved successfully", function () {
            invalidRating.RatingStatus = function () {
                return 'created'
            };
            taskRatingService.taskRatingModels = [validSaveRating, validUpdateRating, invalidRating ,validDeleteRating];
            var savedSuccessfully, errorMessage;

            taskRatingService.SaveAll()
                .then(function () {
                    savedSuccessfully = true;
                })
                .catch(function (error) {
                    savedSuccessfully = false;
                    errorMessage = error;
                });
            $rootScope.$apply();

            expect(errorMessage).toEqual(mockSaveError);
            expect(savedSuccessfully).toEqual(false);
        });
    });
});
