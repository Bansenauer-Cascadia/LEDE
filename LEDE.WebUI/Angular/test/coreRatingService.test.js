'use strict';

describe('coreRatingService', function () {
    var coreRatingService;
    var coreRating;
    var $rootScope, $q;

    beforeEach(module('facultyApp'));

    beforeEach(module(function ($provide) {
        var mockCoreRating = {
            Create: function (data) {
                return {isRatingObject: true, data: data}
            }
        };
        $provide.value('coreRating', mockCoreRating);
    }));

    beforeEach(inject(function (_coreRating_, _$q_, _$rootScope_) {
        coreRating = _coreRating_;
        $rootScope = _$rootScope_;
        $q = _$q_;
    }));

    describe('GetAll()', function () {
        var successVersID, errorVersID;
        var mockGetData, mockGetError;
        beforeEach(inject(function (_coreRatingService_, coreRatingResource) {
            mockGetData = [{RatingID: 1, CScore: 1, SScore: null, PScore: 2},
                {RatingID: 2, CScore: 3, SScore: 2, PScore: null}];
            mockGetError = 'server error';
            successVersID = 1;
            errorVersID = 2;

            spyOn(coreRatingResource, "GetAll").and.callFake(function (VersID) {
                var deferred = $q.defer();
                if (VersID == successVersID) {
                    deferred.resolve(mockGetData);
                }
                else if (VersID == errorVersID) {
                    deferred.reject(mockGetError);
                }
                return deferred.promise;
            });

            coreRatingService = _coreRatingService_; //start mods here tmrw
        }));
        it("returns a promise with the correct CoreRatingModels when calling coreRatingResource successfully",
            inject(function (coreRatingResource) {
                var expectedObjectsArray = mockGetData.map(function(data){
                    return coreRating.Create(data);
                });
                var returnedObjectsArray;

                coreRatingService.GetAll(successVersID).then(function (data) {
                    returnedObjectsArray = data;
                });
                $rootScope.$apply();

                expect(coreRatingResource.GetAll).toHaveBeenCalledWith(successVersID);
                expect(returnedObjectsArray).toEqual(expectedObjectsArray);
            }));
        it("returns a promise with the correct error when calling coreRatingResource unsuccessfully",
            inject(function (coreRatingResource) {
                var expectedError = mockGetError;


            }));
    })
});
