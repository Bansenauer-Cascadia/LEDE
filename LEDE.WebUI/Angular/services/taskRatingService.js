angular.module('facultyApp').factory('taskRatingService', function ($q) {

    function TaskRatingArray(taskRating, taskRatingResource){
        var data = this.taskRatingModels = [];

        this.GetAll = function (VersID) {
            return Get(VersID, true);
        };

        this.GetSingle = function(VersID) {
            return Get(VersID, false);
        };

        var Get = function(VersID, isArray) {
            var result = $q.defer();
            taskRatingResource.GetAll(VersID)
                .then(function (taskRatingData) {
                    HandleData(taskRatingData, isArray);
                    result.resolve(data);
                })
                .catch(function(error){
                    result.reject(error);
                });
            return result.promise;
        }.bind(this);

        var HandleData = function(taskRatingData, isArray) {
            if(isArray)
                taskRatingData.map(AddModel);
            else
                AddModel(taskRatingData);
        };

        var AddModel = function (RatingData) {
            data.push(new taskRating(RatingData));
        }.bind(this);

        this.SaveAll = function() {
            var saveDefer = $q.defer();
            var savePromises = this.taskRatingModels.map(Save);
            $q.all(savePromises).then(function(){
                saveDefer.resolve();
            }).catch(function(error){
               saveDefer.reject(error);
            });
            return saveDefer.promise;
        };

        var Save = function(model) {
            switch(model.RatingStatus()) {
                case 'updated':
                    return taskRatingResource.Update(model.data);
                case 'created':
                    return taskRatingResource.Save(model.data);
                case 'deleted':
                    return taskRatingResource.Delete(model.data);
            }
        }
    }

    return TaskRatingArray;

});
