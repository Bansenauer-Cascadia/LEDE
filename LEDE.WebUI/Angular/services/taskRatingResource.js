angular.module('facultyApp').factory('taskRatingResource', function($resource, apiUrl) {
    var coreRatingsResource = $resource(apiUrl + 'CoreRating/:VersID', null, {
        get: { method: 'GET', isArray: true },
        delete: {method: 'DELETE'},
        update: { method: 'PUT' }
    });
    var impactRatingResource = $resource(apiUrl + 'ImpactRating/:VersID', null, {
        update: {method: 'PUT'}
    });
    var reflectionResource = $resource(apiUrl + 'ReflectionRating/:VersID', null, {
        update: {method: 'PUT'}
    });
    var logResource = $resource(apiUrl + 'LogRating/:VersID', null, {update: {method: 'PUT'}});

    var taskRatingResource = function(resource) {
        this.GetAll = function(VersID) {
            return resource.get({VersID: VersID}).$promise
        };

        this.Update = function(TaskRating) {
            return TaskRating.$update().$promise
        };

        this.Save = function(TaskRating) {
            return TaskRating.$save().$promise
        };

        this.Delete = function(TaskRating) {
            return resource.delete({VersID: TaskRating.RatingID}).$promise
        };
    };

    return {
        coreRatingResource: new taskRatingResource(coreRatingsResource),
        impactRatingResource: new taskRatingResource(impactRatingResource),
        reflectionResource: new taskRatingResource(reflectionResource),
        logResource: new taskRatingResource(logResource)
    };
});
