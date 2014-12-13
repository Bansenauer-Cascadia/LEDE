'use strict';


angular.module('facultyApp')
  .controller('RateCtrl', function ($scope, $filter, ratingModels) {
    var CreateRatingArray = this.CreateRatingArray = function(ratingData, ratingType) {
      var ratings = []
      for (var i = 0; i < ratingData.length; i++) {
        var data = ratingData[i]
        var rating = new ratingType(data)
        ratings.push(rating)
      }
      return ratings
    }

    this.CreateGrade = function(gradeData) {
      var grade = {}
      grade.TaskRatings = CreateRatingArray(gradeData.TaskCoreRatings, ratingModels.TaskRating)
      grade.OtherRatings = CreateRatingArray(gradeData.OtherCoreRatings, ratingModels.OtherRating)
      grade.ImpactRating = [new ratingModels.ImpactRating(gradeData.ImpactRating)] //table directive expects an array
      return grade
    }

    var gradeData = { TaskCoreRatings: [], OtherCoreRatings: [], ImpactRating: {} }
    gradeData = angular.fromJson(jsonString); 
    $scope.grade = this.CreateGrade(gradeData)
  });


var jsonString = {
  "TaskCoreRatings": [{
    "CoreTopicID": 2,
    "CoreTopicTitle": "1.1 Purposeful self-development",
    "RatingID": 1258,
    "CScore": 1,
    "SScore": 3,
    "PScore": 1
  }, {
    "CoreTopicID": 3,
    "CoreTopicTitle": "1.2 Leadership commitments and personal integrity",
    "RatingID": 0,
    "CScore": null,
    "SScore": null,
    "PScore": null
  }, {
    "CoreTopicID": 4,
    "CoreTopicTitle": "1.3 Interpersonal communication and conflict management",
    "RatingID": 2546,
    "CScore": null,
    "SScore": 3,
    "PScore": null
  }, {
    "CoreTopicID": 5,
    "CoreTopicTitle": "1.4 Self-management and well-being",
    "RatingID": 0,
    "CScore": null,
    "SScore": null,
    "PScore": null
  }, {
    "CoreTopicID": 6,
    "CoreTopicTitle": "1.5 Using theory and research to enhance leadership practice",
    "RatingID": 0,
    "CScore": null,
    "SScore": null,
    "PScore": null
  }],
  "OtherCoreRatings": [{
    "CoreTopicID": 7,
    "CoreTopicTitle": "2.1 Tiered interventions to support student learning",
    "RatingID": 1270,
    "CScore": 2,
    "SScore": 1,
    "PScore": 3
  }, {
    "CoreTopicID": 8,
    "CoreTopicTitle": "2.2 Leadership content knowledge",
    "RatingID": 2539,
    "CScore": null,
    "SScore": null,
    "PScore": null
  }, {
    "CoreTopicID": 9,
    "CoreTopicTitle": "2.3 Supporting teachers' instruction",
    "RatingID": 2540,
    "CScore": 3,
    "SScore": null,
    "PScore": null
  }, {
    "CoreTopicID": 10,
    "CoreTopicTitle": "2.4 Assesment of learning and progress monitoring",
    "RatingID": 2541,
    "CScore": null,
    "SScore": null,
    "PScore": null
  }, {
    "CoreTopicID": 11,
    "CoreTopicTitle": "2.5 Instructional adaptations",
    "RatingID": 2542,
    "CScore": null,
    "SScore": null,
    "PScore": null
  }, {
    "CoreTopicID": 12,
    "CoreTopicTitle": "3.1 Referral, eligibility and assesment",
    "RatingID": 2543,
    "CScore": null,
    "SScore": 2,
    "PScore": null
  }, {
    "CoreTopicID": 13,
    "CoreTopicTitle": "3.2 Development of Individualized Educational Programs",
    "RatingID": 2544,
    "CScore": null,
    "SScore": null,
    "PScore": null
  }, {
    "CoreTopicID": 14,
    "CoreTopicTitle": "3.3 Student climate and behavioral support",
    "RatingID": 2545,
    "CScore": null,
    "SScore": null,
    "PScore": null
  }, {
    "CoreTopicID": 15,
    "CoreTopicTitle": "3.4 Related services and assistive technology",
    "RatingID": 2547,
    "CScore": null,
    "SScore": null,
    "PScore": null
  }, {
    "CoreTopicID": 16,
    "CoreTopicTitle": "3.5 Coordinating instruction with families",
    "RatingID": 0,
    "CScore": null,
    "SScore": null,
    "PScore": null
  }],
  "ImpactRating": {"RatingID": 2548, "SScore": 3, "PScore": 3, "LScore": 3}
};




