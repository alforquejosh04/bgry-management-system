/// <reference path="../app.js" />

angular.module('barangaymanagementsystem.services').factory('NationalityServices', ['$http', '$q', '$filter', function ($http, $q, $filter) {

    var services = {};

    services.GetNationality = function () {
        return $q(function (resolve, reject) {
            $http.get('/Nationality/GetNationality', {}).then
                (function (response) {
                    resolve(response.data);
                }, function (response) {
                    reject("Error" + response);
                });
        });
    }
    return services;
}]);