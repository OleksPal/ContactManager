var contactManagerApp = angular.module("contactManagerApp", []);

angular.module("contactManagerApp").config(function () {
    angular.lowercase = angular.$$lowercase;
});

contactManagerApp.controller("ContactManagerController", ["$scope", "$http", function ($scope, $http) {

    $http.get("/Contact/GetAllContacts/").then(successCallback, errorCallback);

    function successCallback(response) {
        $scope.contacts = response.data;
    }
    function errorCallback(error) {
        console.log(error);
    }

    $scope.searchWord = {};
    $scope.contactFields = ["name", "dateOfBirth", "married", "phone", "salary"];

    $scope.order = '';
    $scope.changeOrder = function (value) {
        if ($scope.order === value) {
            $scope.order = '-' + value;
        }
        else if ($scope.order === '-' + value) {
            $scope.order = value;
        }
        else {
            $scope.order = value;
        }
    }
}])