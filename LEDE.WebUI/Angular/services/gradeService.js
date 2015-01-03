angular.module('facultyApp').factory('gradeService', function (ratingResource, taskVersionResource, taskRating, $q) {

    function TaskGradeService(VersID) {

        this.grade = {};

        this.GetGrade = function () {
            var deferred = $q.defer(),
                getGradeActions = GetTaskRatings().concat(GetTaskVersion());

            $q.all(getGradeActions)
                .then(function () {
                    deferred.resolve(this.grade);
                }.bind(this))
                .catch(function (error) {
                    deferred.reject(error);
                });
            return deferred.promise;
        };

        this.SaveGrade = function (grade) {
            var saveGradeActions = SaveTaskRatings(grade).concat(MarkTaskAsRated());
            return $q.all(saveGradeActions);
        };

        var GetTaskRatings = function () {
            var ratingPromises = [];

            ratingPromises.push(ratingResource.core.GetAll(VersID).then(function (data) {
                this.grade.CoreRatings = data.map(function (rating) { return new coreRating(rating); });
            }.bind(this)));
            ratingPromises.push(ratingResource.impact.GetAll(VersID).then(function (data) {
                this.grade.ImpactRating = new impactRating(data);
            }.bind(this)));
            ratingPromises.push(ratingResource.reflection.GetAll(VersID).then(function (data) {
                this.grade.Reflection = new reflectionRating(data);
            }.bind(this)));
            ratingPromises.push(ratingResource.log.GetAll(VersID).then(function (data) {
                this.grade.Log = new logRating(data);
            }.bind(this)));

            return ratingPromises;
        }.bind(this);

        var GetTaskVersion = function () {
            taskVersionResource.GetTaskVersion(VersID).then(function (data) {
                this.grade.TaskVersion = data;
            }.bind(this))
        }.bind(this);

        var SaveTaskRatings = function (grade) {
            savePromises = [];
            grade.CoreRatings.forEach(function (coreRating) {
                savePromises.push(SaveIndividualRating(coreRating, ratingResource.core))
            });
            savePromises.push(SaveIndividualRating(grade.ImpactRating, ratingResource.impact));
            savePromises.push(SaveIndividualRating(grade.Reflection, ratingResource.reflection));
            savePromises.push(SaveIndividualRating(grade.Log, ratingResource.log));
            return savePromises;
        };

        var SaveIndividualRating = function (rating, resource) {
            if (rating.IsExistingRating() && rating.IsEmpty()) return resource.Delete(rating.data);
            if (rating.IsExistingRating() && !rating.IsEmpty()) return resource.Update(rating.data);
            if (!rating.IsExistingRating() && !rating.IsEmpty()) return resource.Create(rating.data);
        };

        var MarkTaskAsRated = function () {
            return taskVersionResource.CompleteRating(VersID);
        };

        var coreRating = taskRating.bind(null, 'RatingID', ['CScore', 'SScore', 'PScore']);
        var impactRating = taskRating.bind(null, 'RatingID', ['SScore', 'PScore', 'LScore']);
        var reflectionRating = taskRating.bind(null, 'VersID', ['NumHours']);
        var logRating = taskRating.bind(null, 'VersID', ['NumEntries']);
    }

    return {
        Create: function (VersID) {
            return new TaskGradeService(VersID)
        }
    };
});
