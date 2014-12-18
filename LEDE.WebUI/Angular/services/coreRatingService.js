angular.module('facultyApp').factory('coreRatingService', function (coreRatingResource, coreRating, $q) {
    this.GetAll = function (VersID) {
        var coreRatingDefer = $q.defer();
        coreRatingResource.GetAll(VersID)
            .then(function (CoreRatingData) {
                coreRatingDefer.resolve(CreateModelArray(CoreRatingData));
            });
        return coreRatingDefer.promise;
    };

    var CreateModelArray = function (CoreRatingData) {
        var CoreRatingModels = [];
        CoreRatingData.forEach(function (RatingData) {
            CoreRatingModels.push(ConvertToModel(RatingData))
        });
        return CoreRatingModels
    };
    var ConvertToModel = function (RatingData) {
        return coreRating.Create(RatingData);
    };

    this.SaveAll = function (CoreRatingModels, callback) {
        var ratingPromises = [];
        CoreRatingModels.ForEach(function (RatingModel) {
            var ratingPromise = Save(RatingModel);
            ratingPromises.push(ratingPromise);
        });
        $q.all(ratingPromises).then(function () {
            callback(null)
        })
    };

    var Save = this.Save = function (RatingModel) {
        var data = RatingModel.data;
        if (RatingModel.IsDeletedRating()) {
            return coreRatingResource.Delete(data)
        }
        else if (RatingModel.IsNewRating()) {
            return coreRatingResource.Save(data)
        }
        else if (RatingModel.IsExistingRating()) {
            return coreRatingResource.Update(data)
        }
    };

    return {
        SaveAll: this.SaveAll,
        GetAll: this.GetAll
    }
});
