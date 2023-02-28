/// <reference path="../View/Index.html" />
/// <reference path="../View/Index.html" />
/// <reference path="../Services/Resident.Services.js" />
angular.module('barangaymanagementsystem.controller').controller('modalCtrl', ['$scope', '$timeout', '$state', '$window', '$timeout','$interval', '$filter', 'ResidentServices', function ($scope, $timeout, $state, $window, $timeout,$interval, $filter, ResidentServices) {

    $scope.Message = "";
    $scope.FileInvalidMessage = "";
    $scope.SelectedFileForUpload = null;
    $scope.FileDescription = "";
    $scope.IsFormSubmitted = false;
    $scope.IsFileValid = false;
    $scope.IsFormValid = false;



    $scope.$watch("f1.$valid", function (Isvalid) {
        $scope.IsFormValid = Isvalid;
    });
 
    $scope.CheckfileValid = function (file) {
        var isValid = false;
        if (file != null) {
            if ((file.type == 'image/png' || file.type == 'image/jpeg' || file.type == 'image/gif') && file.size <= (512 * 1024)) {
                $scope.FileInvalidMessage = "";
                isValid = true;
            }
            else {
                $scope.FileInvalidMessage = "Selected file is Invalid. (only file type png, jpeg and gif and 512 kb size allowed)";
            }
        } else {
            $scope.FileInvalidMessage = "Image required!";
        }
        $scope.IsFileValid = isValid;
    }
    
    $scope.selectFileforUpload = function (event) {
        var file = event.target.files;
        for (var i = 0 ; i < file.length; i++) {
            $scope.SelectedFileForUpload = file[i];

            var reader = new FileReader();
            reader.onload = $scope.imageIsLoaded;
            reader.readAsDataURL($scope.SelectedFileForUpload);

        }

    }


    $scope.imageIsLoaded = function (e) {
        $scope.$apply(function () {
            $scope.img = e.target.result;
        });
    }
    $scope.SaveFile = function (id) {
        $scope.IsFormSubmitted = true;
        $scope.CheckfileValid($scope.SelectedFileForUpload);

        if ($scope.IsFormValid && $scope.IsFileValid) {

            ResidentServices.ImageUpload($scope.SelectedFileForUpload, id).then(function (result) {
                if (result.status == true) {

                    $scope.ClearForm();

                    alert("Profile update successfully.");
                }
            }, function (e) {
                alert(e);
            });



        }
    }
    $scope.CancelUpload = function () {
        $scope.ClearForm();
    }
    $scope.ClearForm = function () {
        angular.forEach(angular.element("input[type='file']"), function (inputElem) {
            angular.element(inputElem).val(null);
        });
        $scope.barModal = angular.element('#UploadImageModal');
        $scope.barModal.modal("hide");
        $scope.f1.$setPristine();
        $scope.IsFormSubmitted = false;
    }


}]);