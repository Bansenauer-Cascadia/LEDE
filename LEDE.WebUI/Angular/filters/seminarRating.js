angular.module('facultyApp')
    .filter('seminarRating', function () {
        return function (ratings, SeminarID) {
            if (ratings === undefined || SeminarID === undefined) return;
            var seminarRatings = [];
            ratings.forEach(function (rating) {
                if (rating.IsSeminarRating(SeminarID)) {
                    seminarRatings.push(rating);
                }
            });
            return seminarRatings;
        }
    })
    .filter('otherRating', function () {
        return function (ratings, SeminarID, showEmptyRatings) {
            if (ratings === undefined || SeminarID === undefined) return;
            var otherRatings = [];
            ratings.forEach(function (rating) {
                if (!rating.IsSeminarRating(SeminarID) && (showEmptyRatings || !rating.IsEmpty())) {
                    otherRatings.push(rating);
                }
            });
            return otherRatings;
        }
    });