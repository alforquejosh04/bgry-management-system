/// <reference path="../View/Index.html" />
/// <reference path="../View/Index.html" />
/// <reference path="../Services/Resident.Services.js" />
angular.module('barangaymanagementsystem.controller').controller('barangayResidentCtrl', ['$scope', '$timeout', '$state', '$window', '$timeout', '$interval','$filter', 'ResidentServices', 'NationalityServices', 'BarangayServices', 'ReligionServices', 'uibDateParser', function ($scope, $timeout, $state, $window, $timeout,$interval, $filter, ResidentServices, NationalityServices, BarangayServices, ReligionServices, uibDateParser) {

    $scope.GetResidentResult = [];
    $scope.ShowResidentDetails = false;
    $scope.GetNationalityList = [];
    $scope.GetBarangayList = [];
    $scope.GetReligionList = [];
    $scope.Message = "";
    $scope.IsDataValid = false;
    $scope.OwnerShip = [];
    $scope.currentPage = 0;
    $scope.pageSize = 10;
    $scope.IsFormSubmitted = false;
    $scope.q = '';
    $scope.percentCount = 0;
    var stopped;
    var getResult = [];
    $scope.IsCancel = false;
    $scope.templates = [
               { url: "/scripts/app/View/modal.html" }
    ]


   

    $scope.GetResidentByIdResult = {
        'residential_id': 0,
        'first_Name': '',
        'last_Name': '',
        'Suffix': ' ',
        'birthdate': null,
        'gender': '',
        'marital_status': '',
        'contact_no': null,
        'email_add': ' ',
        'household_no': '',
        'zone_no': ' ',
        'barangay_id': '',
        'household_member': 0,
        'length_of_stay': 0,
        'religion_id': 0,
        'occupation': ' ',
        'nationality_id': '',
        'Philhealth_no': ' ',
        'voters': false,
        'residency_status': '',
    };
    $scope.OwnerShip = [{ 'value': 'owner', 'option': 'Owner' }, { 'value': 'tenant', 'option': 'Tenant' }]
    $scope.openCal = function ($event,dt) {
      
        $event.preventDefault();
        $event.stopPropagation();
        dt.opened = true;
    };

    $scope.dateOptions = {
        formatYear: 'yy',
        startingDay: 1
    };
    $scope.formats = ['dd-MMMM-yyyy', 'yyyy/MM/dd', 'dd.MM.yyyy', 'shortDate', 'MM/dd/yyyy'];
    $scope.format = $scope.formats[1];
    $scope.ResidentList = function () {
        $scope.ShowResident = true;
        ResidentServices.getResidents().then(function (result) {

            $scope.GetResidentResult = result;
         
        });
    }
    $scope.ResidentList();

    $scope.SearchResident = function (){
        return $filter('filter')($scope.GetResidentResult, $scope.q)
    }
    $scope.numberOfPages = function () {
        return Math.ceil($scope.SearchResident().length / $scope.pageSize);
    }
    $scope.$watch('q', function (newValue, oldValue) {
        if (oldValue != newValue) {
            $scope.currentPage = 0;

        }
  
    }, true);

    NationalityServices.GetNationality().then(function (result) {
         $scope.GetNationalityList = result;    
    })
    BarangayServices.Getbarangay().then(function (result) {
        $scope.GetBarangayList = result;
    });
    ReligionServices.GetReligion().then(function (result) {
        $scope.GetReligionList = result;
    });


    $scope.BtnViewResident = function (_id) {
        $scope.ShowResident = false;
     
       
        ResidentServices.getResidentsById(_id).then(function (result) {

                       GetResult = result;
                       var temp = {};
                       for (var i = 0 ; i < GetResult.length; i++) {  
                     
                           $scope.GetResidentByIdResult = GetResult[i];
                           if ($scope.GetResidentByIdResult != '') {
                               $scope.ShowResidentDetails = true;
                               $scope.GetResidentByIdResult.birthdate = uibDateParser.parse(GetResult[i].birthdate, 'yyyy/MM/dd')       
                               angular.forEach($scope.GetReligionList, function (value,key) {
                                   if ($scope.GetResidentByIdResult.religion_id == value.religion_id) {
                                       $scope.GetResidentByIdResult.religion_name = value.religion_name;
                                   }
                               });
                               angular.forEach($scope.GetBarangayList, function (brgyValue, brgykey) {
                                   if ($scope.GetResidentByIdResult.barangay_id == brgyValue.barangay_id) {
                                       $scope.GetResidentByIdResult.barangay_name = brgyValue.barangay_name;
                                   }
                               });
                               angular.forEach($scope.GetNationalityList, function (nValue, nkey) {
                         
                                   if ($scope.GetResidentByIdResult.nationality_id == nValue.Nationality_id)
                                   {
                                       $scope.GetResidentByIdResult.nationality_name = nValue.nationality_name;
                                   }
                         
                               });
                             
                          
                          }
                        
                       }

                   
        });
       

    }
    $scope.CheckResidentValid = function (data) {
        var isValid = false;
        if (data != null) {
            if (data.first_Name == null || data.last_Name == null || data.barangay_id == null) {
                isValid = false;
            } else {
                isValid = true;
            }
        }
        $scope.IsDataValid = isValid;
    }
 

    $scope.AddUpdateResident = {
        title: 'Submit',
        class: 'btn-primary',
        disabled: false,
        isLoading: false,
        click: function () {
            $scope.IsFormSubmitted = true;
            $scope.CheckResidentValid($scope.GetResidentByIdResult);
            if ($scope.GetResidentByIdResult.residential_id != 0) {

                    $scope.IsUpdate = true;
                    if ($scope.IsDataValid && $scope.IsUpdate) {
                        ResidentServices.AddUpdateResident(JSON.stringify($scope.GetResidentByIdResult)).then(function (result) {
                            if (result.status == true) {
                                alert("Successfully updated");
                                $scope.IsFormSubmitted = false;
                            } else {
                                alert("error Update");
                            }
                        });
                    }
                 
        
               }
             else {
                var answer = confirm("are you sure you want to add this record?");

                if (answer == true) {
                    $scope.IsSaved = true;
                    if ($scope.IsDataValid && $scope.IsSaved) {
                        
                       
                        ResidentServices.AddUpdateResident(JSON.stringify($scope.GetResidentByIdResult)).then(function (result) {
                            if (result.status == true) {
                                $scope.AddUpdateResident.isLoading = true; // start loading
                                $scope.AddUpdateResident.title = 'Submitting';
                                $scope.AddUpdateResident.isLoading = 0.1;
                                $scope.AddUpdateResident.class = 'btn-danger';
                                $scope.AddUpdateResident.disabled = true;

                                $scope.number = 0.1; //lower amount is slower loading
                                $scope.total = 1;
                                $scope.renmaining = 1;
                                var updateCount = function (new_count) {
                                    $scope.number = new_count;
                                    if (!$scope.$$phase) {
                                        $scope.$apply();
                                    }
                                };
                                var dataStatus = $interval(function () {
                                    if (isNaN($scope.number)) {
                                        $interval.cancel(dataStatus);
                                        $scope.number = 0.1;
                                        $scope.renmaining = 1;
                                        $scope.AddUpdateResident.title = 'Submit';
                                        $scope.AddUpdateResident.class = 'btn-primary';
                                    }
                                    else {
                                        $scope.renmaining = $scope.total - $scope.number;
                                        $scope.number = $scope.number + (0.5 * Math.pow($scope.total - Math.sqrt($scope.renmaining), 2));
                                        $scope.AddUpdateResident.isLoading = $scope.number;
                                        if ($scope.number >= 1) {
                                            $timeout(function () {
                                                $interval.cancel(dataStatus);
                                                $scope.AddUpdateResident.class = 'btn-success';
                                                $scope.AddUpdateResident.title = 'Submitted';
                                                $scope.number = 1;
                                              
                                            }, 100);

                                            $timeout(function () {
                                                $scope.AddUpdateResident.isLoading = false; // stop loading
                                                $scope.AddUpdateResident.class = 'btn-primary';
                                                $scope.AddUpdateResident.title = 'Submit';
                                                $scope.ClearResidentForm();
                                                alert("Successfully Inserted");
                                            }, 1500);
                                        }
                                        updateCount($scope.number);
                                   
                                    }
                                }, 100);

                               
                               
                            } else {
                                alert("error Insert");
                            }
                        });
                    }
                }
            }
     

          

        }
    }
    $scope.OnCancelBtn = function () {
        $scope.IsCancel = true;
    }
    $scope.CancelBtn = function () {
        ResidentServices.getResidentsById($scope.GetResidentByIdResult.residential_id).then(function (result) {
            angular.forEach(result, function (val,key) {
                $scope.GetResidentByIdResult = {
                    'residential_id': val.residential_id,
                    'first_Name': val.first_Name,
                    'last_Name': val.last_Name,
                    'Suffix': val.Suffix,
                    'birthdate': val.birthdate,
                    'gender': val.gender,
                    'marital_status': val.marital_status,
                    'contact_no': val.contact_no,
                    'email_add': val.email_add,
                    'household_no': val.household_no,
                    'zone_no': val.zone_no,
                    'barangay_id': val.barangay_id,
                    'household_member': val.household_member,
                    'length_of_stay':  val.length_of_stay,
                    'religion_id': val.religion_id,
                    'occupation':val.occupation,
                    'nationality_id': val.nationality_id,
                    'Philhealth_no': val.Philhealth_no,
                    'voters': val.voters,
                    'residency_status': val.residency_status,
                };
              
                $scope.IsCancel = false;
            });
          
        

        });
    }
    $scope.ClearResidentForm = function () {
        $scope.ResidentList();
        $scope.GetResidentByIdResult = {
            'residential_id': 0,
            'first_Name': '',
            'last_Name': '',
            'Suffix': ' ',
            'birthdate': null,
            'gender': '',
            'marital_status': '',
            'contact_no': null,
            'email_add': ' ',
            'household_no': '',
            'zone_no': ' ',
            'barangay_id': 0,
            'household_member': 0,
            'length_of_stay': 0,
            'religion_id': 0,
            'occupation': ' ',
            'nationality_id': 0,
            'Philhealth_no': ' ',
            'voters': false,
            'residency_status': '',
        };
        $scope.residentForm.$setPristine();
        $scope.IsFormSubmitted = false;
        $scope.barModal = angular.element('#AddResidentModal');
        $scope.barModal.modal("hide");

   
    }

    $scope.OnReturnPage = function () {
        $scope.GetResidentByIdResult = [];
        if ($scope.GetResidentByIdResult.length == 0) {
            $scope.ShowResidentDetails = false;
            $scope.ShowResident = true;

        }
    }
   

}]);