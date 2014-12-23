angular.module('facultyApp').factory('taskGradeService', function (taskRatingServiceFactory, $q) {

    function TaskGradeService(VersID) {
        this.grade = {};
        var Core = this.Core = taskRatingServiceFactory.Core();
        var Impact = this.Impact = taskRatingServiceFactory.Impact();
        var Log = this.Log = taskRatingServiceFactory.Log();
        var Reflection = this.Reflection = taskRatingServiceFactory.Reflection();

        this.GetGrade = function () {
            var deferred = $q.defer();
            $q.all([Core.GetAll(VersID), Impact.GetSingle(VersID), Reflection.GetSingle(VersID), Log.GetSingle(VersID)])
                .then(function (ratings) {
                    this.grade = {
                        CoreRatings: ratings[0],
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
                    deferred.resolve();
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
