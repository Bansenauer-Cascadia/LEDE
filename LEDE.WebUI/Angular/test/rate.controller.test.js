describe("test: rate controller", function () {
    beforeEach(module('facultyApp'));
    var $controller, successAsync, errorAsync, rateCtrl, $scope, mockSuccessData, mockError;
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
        $provide.value('taskGradeService', taskGradeService);
    }));

    beforeEach(inject(function (_$controller_, $q, $rootScope) {
        $controller = _$controller_;
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
        rateCtrl = $controller('RateCtrl', {$scope: $scope});
    }));

    it("Creates taskGradeService with appropriate VersID", function () {
        expect(taskGradeService.Create).toHaveBeenCalledWith(mockVersID);
    });

    describe("GetGrade is called successfully", function () {
        beforeEach(function () {
            spyOn(taskGrade, "GetGrade").and.callFake(successAsync);
            $scope.VersID = mockVersID;
            mockSuccessData = {success: true, fakeData: 'also true'};
        });
        it("sets $scope.grade to the correct value", function () {
            rateCtrl.GetGrade();
            $scope.$apply();
            expect($scope.grade).toBe(mockSuccessData);
        });
    });

    describe("GetGrade is called unsuccessfully", function () {
        beforeEach(function () {
            spyOn(taskGrade, "GetGrade").and.callFake(errorAsync);
            mockError = 'getGradeERR'
        });
        it("Sets errorFetchingGrade to true if there's an error fetching the grade", function () {
            rateCtrl.GetGrade();
            $scope.$apply();
            expect($scope.errorFetchingGrade).toBeTruthy();
        });
    });

    describe("SaveGrade is called successfully", function () {
        beforeEach(function () {
            spyOn(taskGrade, "SaveGrade").and.callFake(successAsync);
        });
        it("sets errorSavingGrade to false if there's no error saving the grade", function () {
            $scope.SaveGrade();
            $scope.$apply();
            expect($scope.errorSavingGrade).toBeDefined();
            expect($scope.errorSavingGrade).toBeFalsy();
        });
    });
    describe("SaveGrade is called unsuccessfully", function(){
       beforeEach(function(){
           spyOn(taskGrade, "SaveGrade").and.callFake(errorAsync);
       });
        it("sets errorSavingGrade to true if there's an error saving the grade", function(){
            $scope.SaveGrade();
            $scope.$apply();
            expect($scope.errorSavingGrade).toBeTruthy();
        })
    });
});
