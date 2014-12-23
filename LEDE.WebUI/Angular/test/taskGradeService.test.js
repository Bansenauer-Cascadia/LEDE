'use strict';

describe('Service: taskGradeService', function () {
    var taskGradeService, taskRatingServiceFactory, $rootScope;
    var getSuccessPromiseFn, getErrorPromiseFn;
    var coreService, impactService, logService, reflectionService;
    var mockVersID;

    var generateEmptyService = function(){
      return {
          GetSingle: function() {},
          SaveAll: function() {}
      }
    };

    beforeEach(module('facultyApp'));

    beforeEach(module(function ($provide) {
        coreService = {
            GetAll: function() {},
            SaveAll: function() {}
        };
        impactService = generateEmptyService();
        logService = generateEmptyService();
        reflectionService = generateEmptyService();

        taskRatingServiceFactory = {
            Core: function () {
                return coreService
            },
            Impact: function () {
                return impactService
            },
            Log: function () {
                return logService
            },
            Reflection: function () {
                return reflectionService
            }
        };
        $provide.value('taskRatingServiceFactory', taskRatingServiceFactory)
    }));

    beforeEach(inject(function ($q) {
        getSuccessPromiseFn = function (successData) {
            return function () {
                var deferred = $q.defer();
                deferred.resolve(successData);
                return deferred.promise;
            };
        };

        getErrorPromiseFn = function (errorData) {
            return function () {
                var deferred = $q.defer();
                deferred.reject(errorData);
                return deferred.promise;
            }
        }
    }));

    beforeEach(inject(function (_taskGradeService_, _$rootScope_) {
        taskGradeService = _taskGradeService_;
        $rootScope = _$rootScope_;
    }));

    describe("GetGrade", function () {
        var CoreSuccessData, ImpactSuccessData, LogSuccessData, ReflectionSuccessData;
        beforeEach(function () {
            mockVersID = 1;
            CoreSuccessData = [{ratingSuccess: true}, {ratingSuccess: true}];
            ImpactSuccessData = {impactSuccess: true};
            LogSuccessData = {logSuccess: true};
            ReflectionSuccessData = {reflectionSuccess: true};

        });
        it("instantiates the grade when services are called successfully", function () {
            var grade;
            var gradeService = taskGradeService.Create(mockVersID);
            spyOn(coreService, "GetAll").and.callFake(getSuccessPromiseFn(CoreSuccessData));
            spyOn(impactService, "GetSingle").and.callFake(getSuccessPromiseFn([ImpactSuccessData]));
            spyOn(logService, "GetSingle").and.callFake(getSuccessPromiseFn([LogSuccessData]));
            spyOn(reflectionService, "GetSingle").and.callFake(getSuccessPromiseFn([ReflectionSuccessData]));


            gradeService.GetGrade().then(function () {
                grade = gradeService.grade;
            });
            $rootScope.$apply();

            expect(coreService.GetAll).toHaveBeenCalledWith(mockVersID);
            expect(impactService.GetSingle).toHaveBeenCalledWith(mockVersID);
            expect(logService.GetSingle).toHaveBeenCalledWith(mockVersID);
            expect(reflectionService.GetSingle).toHaveBeenCalledWith(mockVersID);

            expect(grade.CoreRatings).toEqual(CoreSuccessData);
            expect(grade.ImpactRating).toEqual(ImpactSuccessData);
            expect(grade.LogRating).toEqual(LogSuccessData);
            expect(grade.ReflectionRating).toEqual(ReflectionSuccessData);
        });
        it("returns appropriate error message when one or more services isn't called successfully", function () {
            var expectedError = 'there was an error';
            var error, data;
            var gradeService = taskGradeService.Create(mockVersID);
            spyOn(coreService, "GetAll").and.callFake(getSuccessPromiseFn(CoreSuccessData));
            spyOn(impactService, "GetSingle").and.callFake(getSuccessPromiseFn(ImpactSuccessData));
            spyOn(logService, "GetSingle").and.callFake(getSuccessPromiseFn(LogSuccessData));
            spyOn(reflectionService, "GetSingle").and.callFake(getErrorPromiseFn(expectedError));

            gradeService.GetGrade().then(function(returnedData){
                data = returnedData;
            }).catch(function(returnedError){
                error = returnedError;
            });
            $rootScope.$apply();

            expect(data).toBeUndefined();
            expect(error).toEqual(expectedError);
        });
    });
    describe("SaveGrade", function(){
        describe("Successful SaveGrade Operations", function(){
            beforeEach(function(){
                var mockVersID = 1;
                spyOn(coreService, "SaveAll").and.callFake(getSuccessPromiseFn());
                spyOn(impactService, "SaveAll").and.callFake(getSuccessPromiseFn());
                spyOn(logService, "SaveAll").and.callFake(getSuccessPromiseFn());
                spyOn(reflectionService, "SaveAll").and.callFake(getSuccessPromiseFn());
            });
            it("calls all SaveAll successfully for all services", function() {
                var gradeService = taskGradeService.Create(mockVersID);
                var saveSuccessful;

                gradeService.SaveGrade().then(function(){
                    saveSuccessful = true;
                });
                $rootScope.$apply();

                expect(coreService.SaveAll).toHaveBeenCalled();
                expect(impactService.SaveAll).toHaveBeenCalled();
                expect(logService.SaveAll).toHaveBeenCalled();
                expect(reflectionService.SaveAll).toHaveBeenCalled();
                expect(saveSuccessful).toBeTruthy();
            })
        });
        it("rejects returned promise when SaveAll isn't called succesfully for all services", function(){
            var mockVersID = 1;
            var expectedError = 'not saved properly :)';
            spyOn(coreService, "SaveAll").and.callFake(getSuccessPromiseFn());
            spyOn(impactService, "SaveAll").and.callFake(getSuccessPromiseFn());
            spyOn(logService, "SaveAll").and.callFake(getErrorPromiseFn(expectedError));
            spyOn(reflectionService, "SaveAll").and.callFake(getSuccessPromiseFn());
            var gradeService = taskGradeService.Create(mockVersID);
            var returnedError;

            gradeService.SaveGrade().catch(function(error) {
                returnedError = error;
            });
            $rootScope.$apply();

            expect(returnedError).toEqual(expectedError);
        });
    });
});
