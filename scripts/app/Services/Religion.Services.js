angular.module('barangaymanagementsystem.services').factory('ReligionServices', ['$http', '$q', '$filter', function ($http, $q, $filter) {

    var services = {};

    services.GetReligion = function () {
        return $q(function (resolve, reject) {
            $http.get('/Religion/GetNationality', {}).then
                (function (response) {
                    resolve(response.data);
                }, function (response) {
                    reject("Error" + response);
                });
        });
    }
    return services;
}]);