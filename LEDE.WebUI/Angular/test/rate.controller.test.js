describe("test: rate controller", function () {
    beforeEach(module('facultyApp'));
    var $controller, $scope, $rootScope;
    var successAsync, errorAsync, rateCtrl, mockSuccessData, mockError;

    var taskGrade = {
        GetGrade: function () {
        },
        SaveGrade: function () {
        }
    };
    var taskGradeService = {
        Create: function () {
        }
    };
    var mockVersID = 1;

    beforeEach(module(function ($provide) {
        $provide.value('gradeService', taskGradeService);
    }));

    beforeEach(inject(function (_$controller_, $q, _$rootScope_, _$location_) {
        $controller = _$controller_;
        $rootScope = _$rootScope_;
        $scope = $rootScope.$new();
        $scope.VersID = mockVersID;

        successAsync = function () {
            var deferred = $q.defer();
            deferred.resolve(mockSuccessData);
            return deferred.promise;
        };

        errorAsync = function () {
            var defer = $q.defer();
            defer.reject(mockError);
            return defer.promise;
        };
        spyOn(taskGradeService, "Create").and.callFake(function () {
            return taskGrade
        });

        spyOn(_$location_, "search").and.returnValue({VersID: mockVersID});
    }));

    it("Creates taskGradeService with appropriate VersID", function () {
        spyOn(taskGrade, "GetGrade").and.callFake(successAsync);
        rateCtrl = $controller('RateCtrl', {$scope: $scope});
        expect(taskGradeService.Create).toHaveBeenCalledWith(mockVersID);
    });

    describe("GetGrade is called successfully", function () {
        beforeEach(function () {
            spyOn(taskGrade, "GetGrade").and.callFake(successAsync);
            $scope.VersID = mockVersID;
            mockSuccessData = {success: true, TaskVersion: {}};
            rateCtrl = $controller('RateCtrl', {$scope: $scope});
        });
        it("sets $scope.grade to the correct value", function () {
            $rootScope.$apply();
            expect($scope.grade).toBe(mockSuccessData);
        });
    });

    describe("GetGrade is called unsuccessfully", function () {
        beforeEach(function () {
            spyOn(taskGrade, "GetGrade").and.callFake(errorAsync);
            mockError = 'getGradeERR';
            rateCtrl = $controller('RateCtrl', {$scope: $scope});
        });
        it("Sets errorFetchingGrade to true if there's an error fetching the grade", function () {
            $rootScope.$apply();
            expect($scope.errorFetchingGrade).toBeTruthy();
        });
    });

    describe("SaveGrade is called successfully", function () {
        beforeEach(function () {
            spyOn(taskGrade, "SaveGrade").and.callFake(successAsync);
            spyOn(taskGrade, "GetGrade").and.callFake(successAsync);
            rateCtrl = $controller('RateCtrl', {$scope: $scope});
        });
        it("sets errorSavingGrade to false if there's no error saving the grade", function () {
            $scope.SaveGrade();
            $rootScope.$apply();
            expect($scope.errorSavingGrade).toBeDefined();
            expect($scope.errorSavingGrade).toBeFalsy();
        });
    });
    describe("SaveGrade is called unsuccessfully", function () {
        beforeEach(function () {
            spyOn(taskGrade, "SaveGrade").and.callFake(errorAsync);
            spyOn(taskGrade, "GetGrade").and.callFake(successAsync);
            rateCtrl = $controller('RateCtrl', {$scope: $scope});
        });
        it("sets errorSavingGrade to true if there's an error saving the grade", function () {
            $scope.SaveGrade();
            $rootScope.$apply();
            expect($scope.errorSavingGrade).toBeTruthy();
        })
    });
});
