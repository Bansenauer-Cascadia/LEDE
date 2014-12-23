angular.module('facultyApp').factory('taskRatingServiceFactory', function(taskRatingResource, taskRatingService, taskRatingFactory){
    return {
        Core: function(){ return new taskRatingService(taskRatingFactory.CoreRating, taskRatingResource.coreRatingResource)},
        Impact: function() {return new taskRatingService(taskRatingFactory.ImpactRating, taskRatingResource.impactRatingResource)},
        Log: function() {return new taskRatingService(taskRatingFactory.LogRating, taskRatingResource.logResource)},
        Reflection: function() {return new taskRatingService(taskRatingFactory.ReflectionRating, taskRatingResource.reflectionResource)}
    }
});
