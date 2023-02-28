/// <reference path="../angular.min.js" />
/// <reference path="../angular-route.min.js" />
/// <reference path="../angular-ui-router.js" />
/// <reference path="app.js" />

var app = angular.module("barangaymanagementsystemapp");
app.value('url', "Resident");
app.filter("ageFilter", function () {

    return function (birthday) {
        var birthday = new Date(birthday);
        var today = new Date();
        var age = ((today - birthday) / (31557600000));
        var age = Math.floor(age);
        return age;
    }
});
app.filter('startFrom', function () {
    return function (input, start) {
        start = +start;
        return input.slice(start);
    }
});
