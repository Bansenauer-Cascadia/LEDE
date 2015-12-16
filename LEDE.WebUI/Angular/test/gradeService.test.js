'use strict';

describe('Service: ratingResource', function () {
    var ratingResource, taskVersionResource, taskRating, asyncSuccess, asyncError, $rootScope, testService,
        coreData, impactData, logData, reflectionData, taskVersionData, error,
        testVersID = 1;

    beforeEach(module('facultyApp'));

    beforeEach(inject(function ($q) {
        asyncSuccess = function (successData) {
            return function () {
                var deferred = $q.defer();
                deferred.resolve(successData);
                return deferred.promise;
            };
        };

        asyncError = function (errorData) {
            return function () {
                var deferred = $q.defer();
                deferred.reject(errorData);
                return deferred.promise;
            }
        };
    }));

    beforeEach(inject(function (_$rootScope_, _ratingResource_, _taskVersionResource_, _gradeService_, _taskRating_) {
        $rootScope = _$rootScope_;
        ratingResource = _ratingResource_;
        taskVersionResource = _taskVersionResource_;
        taskRating = _taskRating_;
        testService = _gradeService_.Create(testVersID);
    }));

    describe('GetGrade()', function () {
        beforeEach(function () {
            coreData = [{ RatingID: 1, CScore: 2, SScore: 2, PScore: null }];
            impactData = { RatingID: 2, SScore: null, PScore: 1, LScore: 1 };
            logData = { VersID: 1, NumEntries: 2 };
            reflectionData = { VersID: 1, NumHours: 2.1 };
            taskVersionData = { SeminarID: 1 };
            error = 'There was an error retrieving the data';
        });
        describe('Behavior when called Successfully', function () {
            beforeEach(function () {
                spyOn(ratingResource.core, "GetAll").and.callFake(asyncSuccess(coreData));
                spyOn(ratingResource.impact, "GetAll").and.callFake(asyncSuccess(impactData));
                spyOn(ratingResource.log, "GetAll").and.callFake(asyncSuccess(logData));
                spyOn(ratingResource.reflection, "GetAll").and.callFake(asyncSuccess(reflectionData));
                spyOn(taskVersionResource, 'GetTaskVersion').and.callFake(asyncSuccess(taskVersionData));
            });
            it('Calls all resources with the correct VersID', function () {
                testService.GetGrade();

                expect(taskVersionResource.GetTaskVersion).toHaveBeenCalledWith(testVersID);
                expect(ratingResource.core.GetAll).toHaveBeenCalledWith(testVersID);
                expect(ratingResource.impact.GetAll).toHaveBeenCalledWith(testVersID);
                expect(ratingResource.log.GetAll).toHaveBeenCalledWith(testVersID);
                expect(ratingResource.reflection.GetAll).toHaveBeenCalledWith(testVersID);
            });
            it('Returns the correct grade object', function () {
                var returnedGrade;

                testService.GetGrade().then(function (grade) {
                    returnedGrade = grade;
                });
                $rootScope.$apply();

                expect(returnedGrade.TaskVersion).toEqual(taskVersionData);

                expect(returnedGrade.CoreRatings[0].data).toEqual(coreData[0]);
                expect(returnedGrade.CoreRatings[0] instanceof taskRating).toBeTruthy();

                expect(returnedGrade.ImpactRating.data).toEqual(impactData);
                expect(returnedGrade.ImpactRating instanceof taskRating).toBeTruthy();

                expect(returnedGrade.Log.data).toEqual(logData);
                expect(returnedGrade.Log instanceof taskRating).toBeTruthy();

                expect(returnedGrade.Reflection.data).toEqual(reflectionData);
                expect(returnedGrade.Reflection instanceof taskRating).toBeTruthy();
            });
        })
    });

    describe('SaveGrade(): All 4 components of the grade are individually created, updated, or deleted depending on their data', function () {
        var gradeToSave, createGradeFromRating;
        beforeEach(function () {
            coreData = { coreData: true };
            impactData = { impactData: true };
            logData = { logData: true };
            reflectionData = { reflectionData: true };

            createGradeFromRating = function (rating) {
                return {
                    CoreRatings: [new rating(coreData)],
                    ImpactRating: new rating(impactData),
                    Reflection: new rating(reflectionData),
                    Log: new rating(logData)
                }
            }
        });
        describe('Non-empty ratings that already exist should be updated', function () {
            var ratingThatShouldBeUpdated = function (data) {
                this.data = data;
                this.IsEmpty = function () {
                    return false;
                };
                this.IsExistingRating = function () {
                    return true;
                }
            };
            beforeEach(function () {
                angular.forEach(ratingResource, function (resource) {
                    spyOn(resource, 'Update').and.callFake(asyncSuccess());
                });
                gradeToSave = createGradeFromRating(ratingThatShouldBeUpdated);
            });
            it('Correctly calls Update for all rating resources', function () {
                testService.SaveGrade(gradeToSave);

                expect(ratingResource.core.Update).toHaveBeenCalledWith(coreData);
                expect(ratingResource.impact.Update).toHaveBeenCalledWith(impactData);
                expect(ratingResource.log.Update).toHaveBeenCalledWith(logData);
                expect(ratingResource.reflection.Update).toHaveBeenCalledWith(reflectionData);
            });
        });
        describe('Non-empty ratings that do not already exist should be created', function () {
            var ratingThatShouldBeCreated = function (data) {
                this.data = data;
                this.IsEmpty = function () {
                    return false;
                };
                this.IsExistingRating = function () {
                    return false;
                };
            };
            beforeEach(function () {
                angular.forEach(ratingResource, function (resource) {
                    spyOn(resource, 'Create').and.callFake(asyncSuccess());
                });
                gradeToSave = createGradeFromRating(ratingThatShouldBeCreated);
            });
            it('Correctly calls Create for all rating resources', function () {
                testService.SaveGrade(gradeToSave);

                expect(ratingResource.core.Create).toHaveBeenCalledWith(coreData);
                expect(ratingResource.impact.Create).toHaveBeenCalledWith(impactData);
                expect(ratingResource.log.Create).toHaveBeenCalledWith(logData);
                expect(ratingResource.reflection.Create).toHaveBeenCalledWith(reflectionData);
            });
        });
        describe('null data with a non-zero primary key should be deleted', function () {
            var ratingThatShouldBeDeleted = function (data) {
                this.data = data;
                this.IsEmpty = function () {
                    return true;
                };
                this.IsExistingRating = function () {
                    return true;
                };
            };
            beforeEach(function () {
                angular.forEach(ratingResource, function (resource) {
                    spyOn(resource, 'Delete').and.callFake(asyncSuccess());
                });
                gradeToSave = createGradeFromRating(ratingThatShouldBeDeleted);
            });
            it('Correctly calls Delete for all rating resources', function () {
                testService.SaveGrade(gradeToSave);

                expect(ratingResource.core.Delete).toHaveBeenCalledWith(coreData);
                expect(ratingResource.impact.Delete).toHaveBeenCalledWith(impactData);
                expect(ratingResource.log.Delete).toHaveBeenCalledWith(logData);
                expect(ratingResource.reflection.Delete).toHaveBeenCalledWith(reflectionData);
            });
            it('Marks ratingStatus as "complete" after successful rating save', function () {
                spyOn(taskVersionResource, 'MarkTaskVersionAsRated').and.callFake(asyncSuccess());
                testService.SaveGrade(gradeToSave);
                $rootScope.$apply();

                expect(taskVersionResource.MarkTaskVersionAsRated).toHaveBeenCalledWith(testVersID);
            });
        });
    });
});
