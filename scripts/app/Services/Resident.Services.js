/// <reference path="../app.js" />

angular.module('barangaymanagementsystem.services').factory('ResidentServices', ['$http', '$q', '$filter', 'url', function ($http, $q, $filter, url) {
    var _url = url;
    var service = {};
   

    service.getResidents = function () {

        return $q(function (resolve,reject) {
            $http.get('/'+ url +'/GetResidents', {}).then(
                function (response) {
                    resolve(response.data);
                    
                },
                function (response) {
                    reject("Error" + response);
                });
        });
    };
    service.getResidentsById = function (_ResidentId) {

        return $q(function (resolve, reject) {
            $http.get('/' + url + '/GetResidentById?ResidentId=' + _ResidentId, {}).then(
                function(response){
                    resolve(response.data);
                },
                function(response){
                    reject("Error" + response);
                });
        });
    };
    service.AddUpdateResident = function (_listValues) {


        var data = "{'item':" + _listValues + "}";
        return $q(function (resolve, reject) {
            $http.post('/' + url + '/AddUpdateResident', data, { headers: { 'Content-Type': 'application/json' }, 'dataType': 'json' }).then(function (response) {
                resolve(response.data);
            }, function (response) {
                reject("Error" + response);
            });

        });
    };


    service.ImageUpload = function(_file, _ResidentId){
        var formData = new FormData();
        formData.append("file", _file);
        formData.append("ResidentId", _ResidentId);

        return $q(function (resolve, reject) {

            $http.post('/' + url + '/SaveImages', formData,{ headers: { 'Content-Type': undefined }, transformRequest: angular.identity }).then(function (response) {
                resolve(response.data);
            }, function (response) {
                reject("Error" + response);
            });
        });

    }
         
       
    return service;

}]);