'use strict';
var INTEGER_REGEXP = /^\-?\d+$/;
angular.module('facultyApp', ['ngResource', 'ngAnimate'])
    .constant('apiUrl', 'api/')
   .config(function ($locationProvider) {
       $locationProvider.html5Mode(true)
   })
.directive('integer', function () {
    return {
        require: 'ngModel',
        link: function (scope, elm, attrs, ctrl) {
            ctrl.$validators.integer = function (modelValue, viewValue) {
                if (ctrl.$isEmpty(modelValue)) {
                    return true;
                }

                if (INTEGER_REGEXP.test(viewValue)) {
                    return true;
                }

                return false;
            };
        }
    };
});

