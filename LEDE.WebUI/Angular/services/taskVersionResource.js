angular.module('facultyApp').factory('taskVersionResource', function ($http, apiUrl) {
    var taskVersionUrl = apiUrl + 'TaskVersion/';
    var TaskVersionResource = function () {
        this.GetTaskVersion = function (VersID) {
            return $http.get(taskVersionUrl + VersID)
        };
        this.MarkTaskVersionAsRated = function (VersID) {
           return $http.put(taskVersionUrl + VersID)
        };
    };
    return new TaskVersionResource();
});
