angular.module('facultyApp').factory('taskGradeService', function (taskRatingServiceFactory, taskVersionService ,$q, $filter) {

    function TaskGradeService(VersID) {
        this.grade = {};
        var Core = this.Core = taskRatingServiceFactory.Core();
        var Impact = this.Impact = taskRatingServiceFactory.Impact();
        var Log = this.Log = taskRatingServiceFactory.Log();
        var Reflection = this.Reflection = taskRatingServiceFactory.Reflection();
        var SeminarID; 

        var TaskRating = function (value) {
            return value.data.SeminarID === SeminarID;
        };

        var OtherRating = function (value) {
            return value.data.SeminarID !== SeminarID;
        };

        this.GetGrade = function () {
            var deferred = $q.defer();
            $q.all([Core.GetAll(VersID), Impact.GetSingle(VersID), Reflection.GetSingle(VersID), Log.GetSingle(VersID),
                taskVersionService.GetSeminarID(VersID)])
                .then(function (ratings) {
                    SeminarID = ratings[4].data;                   
                    this.grade = {
                        TaskCoreRatings: $filter('filter')(ratings[0], TaskRating),
                        OtherCoreRatings: $filter('filter')(ratings[0], OtherRating),
                        ImpactRating: ratings[1][0],
                        ReflectionRating: ratings[2][0],
                        LogRating: ratings[3][0]
                    };
                    deferred.resolve(this.grade);
                }.bind(this))
                .catch(function (error) {
                    deferred.reject(error);
                });
            return deferred.promise;
        };

        this.SaveGrade = function () {
            var deferred = $q.defer();
            $q.all([Core.SaveAll(), Impact.SaveAll(), Reflection.SaveAll(), Log.SaveAll()])
                .then(function () {
                    taskVersionService.CompleteRating(VersID).then(function () {
                        deferred.resolve();
                    })
                    .catch(function () {
                        deferred.reject();
                    })
                })
                .catch(function (error) {
                    deferred.reject(error);
                });
            return deferred.promise;
        }
    }

    return {
        Create: function (VersID) {
            return new TaskGradeService(VersID)
        }
    }
});
