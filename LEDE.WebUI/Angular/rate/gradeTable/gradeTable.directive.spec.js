'use strict';

describe('Directive: gradeTable', function () {

  // load the directive's module and view
  beforeEach(module('facultyApp'));
  beforeEach(module('app/rate/gradeTable/gradeTable.html'));

  var element, scope;

  beforeEach(inject(function ($rootScope) {
    scope = $rootScope.$new();
  }));
});
