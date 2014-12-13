'use strict';

angular.module('facultyApp')
  .config(function ($stateProvider) {
    $stateProvider
      .state('rate', {
        url: '/rate',
        templateUrl: 'app/rate/rate.html',
        controller: 'RateCtrl'
      });
  });
