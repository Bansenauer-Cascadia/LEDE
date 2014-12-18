angular.module('facultyApp').factory('coreRatingResource', function($resource, apiUrl) {
    var coreRatingsResource = $resource(apiUrl + 'CoreRating/:VersID', null, {
        get: { method: 'GET', isArray: true },
        update: { method: 'PUT' }
    });

    this.GetAll = function(VersID) {
        return coreRatingsResource.get({VersID: VersID}).$promise
    };

    this.Update = function(CoreRating) {
        return CoreRating.$update().$promise
    };

    this.Save = function(CoreRating) {
        return CoreRating.$save().$promise
    };

    this.Delete = function(RatingID) {
        return coreRatingsResource.$delete({id: RatingID}).$promise
    };

    return {
        GetAll: this.GetAll,
        Update: this.Update,
        Save: this.Save,
        Delete: this.Delete
    }
});
