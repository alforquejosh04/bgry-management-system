angular.module('barangaymanagementsystem.services').factory('BarangayServices', ['$http', '$q', '$filter', function ($http, $q, $filter) {

    var services = {};

    services.Getbarangay= function () {
        return $q(function (resolve, reject) {
            $http.get('/Barangay/GetBarangay', {}).then
                (function (response) {
                    resolve(response.data);
                }, function (response) {
                    reject("Error" + response);
                });
        });
    }
    return services;
}]);