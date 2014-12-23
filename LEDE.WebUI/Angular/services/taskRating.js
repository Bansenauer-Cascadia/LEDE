'use strict';

angular.module('facultyApp').factory('taskRating', function(){
   return function(data, primaryKeyField, scoreFields) {
       if(!data) data = {};
       this.data = data;
       var primaryKey = this.primaryKey = primaryKeyField;
       this.scoreFields = scoreFields;

       this.IsExistingRating = function(){
           return this.data[primaryKey] > 0;
       };

       this.IsEmpty = function () {
           for(var i = 0; i < this.scoreFields.length; i ++) {
               if(this.data[scoreFields[i]] !== null) return false;
           }
           return true;
       };

       this.Clear = function(){
         this.scoreFields.forEach(function(field){
            this.data[field] = null;
         }.bind(this));
       };

       this.RatingStatus = function() {
           var ratingStatus;
           if(this.IsExistingRating()) {
               if(this.IsEmpty()) ratingStatus = 'deleted';
               else ratingStatus = 'updated';
           }
           else if(!this.IsEmpty()) {
               ratingStatus = 'created';
           }
           return ratingStatus;
       };
   };
});
