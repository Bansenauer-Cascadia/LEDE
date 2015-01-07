//This class is responsible for getting grade data and converting it to taskRating models when GetGrade() is called
//and persisting this data when SaveGrade() is called.
angular.module('facultyApp').factory('gradeService', function (ratingResource, taskVersionResource, taskRating, $q) {
    var CoreRating, ImpactRating, ReflectionRating, LogRating,
        RatingRepository, RatingArrayRepository, TaskGradeService;

    //taskGradeService object that is created and returned by gradeService.Create()
    TaskGradeService = function (VersID) {

        this.CoreRatings = new RatingArrayRepository(ratingResource.core, CoreRating);
        this.ImpactRating = new RatingRepository(ratingResource.impact, ImpactRating);
        this.Reflection = new RatingRepository(ratingResource.reflection, ReflectionRating);
        this.Log = new RatingRepository(ratingResource.log, LogRating);

        this.GetGrade = function () {
            var getGradeActions = InitializeRatingsFromData().concat(InitializeTaskFromData());
            return $q.all(getGradeActions)
                .then(function () {
                    return {
                        CoreRatings: this.CoreRatings.ratings,
                        ImpactRating: this.ImpactRating.rating,
                        Reflection: this.Reflection.rating,
                        Log: this.Log.rating,
                        TaskVersion: this.TaskVersion
                    };
                }.bind(this));
        };

        this.SaveGrade = function (grade) {
            var saveGradeActions = SaveTaskRatings(grade).concat(taskVersionResource.MarkTaskVersionAsRated(VersID));
            return $q.all(saveGradeActions);
        };

        var InitializeRatingsFromData = function () {
            var ratingPromises = [];
            ratingPromises.push(this.CoreRatings.GetRatings(VersID));
            ratingPromises.push(this.ImpactRating.GetRating(VersID));
            ratingPromises.push(this.Reflection.GetRating(VersID));
            ratingPromises.push(this.Log.GetRating(VersID));
            return ratingPromises;
        }.bind(this);

        var InitializeTaskFromData = function () {
            return taskVersionResource.GetTaskVersion(VersID).then(function (data) {
                this.TaskVersion = data;
            }.bind(this))
        }.bind(this);

        var SaveTaskRatings = function (grade) {
            savePromises = [];
            savePromises.push(this.CoreRatings.SaveRatings(grade.CoreRatings));
            savePromises.push(this.ImpactRating.SaveRating(grade.ImpactRating));
            savePromises.push(this.Reflection.SaveRating(grade.Reflection));
            savePromises.push(this.Log.SaveRating(grade.Log));
            return savePromises;
        }.bind(this);
    };

    //class that persists a single taskRating model to a taskRating data source
    RatingRepository = function (ratingResource, ratingType) {
        this.rating = {};
        this.ratingResource = ratingResource;
        this.ratingType = ratingType;
    };
    RatingRepository.prototype = {
        GetRating: function (VersID) {
            return this.ratingResource.GetAll(VersID).then(function (ratingData) {
                this.rating = new this.ratingType(ratingData);
            }.bind(this))
        },
        SaveRating: function (rating) {
            if (rating.IsExistingRating() && rating.IsEmpty()) return this.ratingResource.Delete(rating.data);
            if (rating.IsExistingRating() && !rating.IsEmpty()) return this.ratingResource.Update(rating.data);
            if (!rating.IsExistingRating() && !rating.IsEmpty()) return this.ratingResource.Create(rating.data);
        }
    };

    //class that persists an array of taskRating models to a taskRating data source
    RatingArrayRepository = function (ratingResource, ratingType) {
        RatingRepository.call(this, ratingResource, ratingType);
        this.ratings = [];
    };
    RatingArrayRepository.prototype = {
        GetRatings: function (VersID) {
            return this.ratingResource.GetAll(VersID).then(function (ratingDataArray) {
                for (var i = 0; i < ratingDataArray.length; i++) {
                    this.ratings.push(new this.ratingType(ratingDataArray[i]))
                }
            }.bind(this))
        },
        SaveRatings: function (ratings) {
            var saveRatingActions = [];
            for (var i = 0; i < ratings.length; i++) {
                var saveRatingAction = RatingRepository.prototype.SaveRating.call(this, ratings[i]);
                saveRatingActions.push(saveRatingAction);
            }
            return $q.all(saveRatingActions);
        }
    };

    //There are 4 taskRating types that make up a grade. Here we create constructor functions for each type that
    //will call the taskRating constructor with the appropriate fields.
    CoreRating = taskRating.bind(null, 'RatingID', ['CScore', 'SScore', 'PScore']);
    ImpactRating = taskRating.bind(null, 'RatingID', ['SScore', 'PScore', 'LScore']);
    ReflectionRating = taskRating.bind(null, 'VersID', ['NumHours']);
    LogRating = taskRating.bind(null, 'VersID', ['NumEntries']);

    //constructor function returned by gradeService
    return {
        Create: function (VersID) {
            return new TaskGradeService(VersID)
        }
    };
});
