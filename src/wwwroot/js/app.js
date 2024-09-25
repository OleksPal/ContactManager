var contactManagerApp = angular.module("contactManagerApp", []);

contactManagerApp.controller("ContactManagerController", ["$scope", "$http", function ($scope, $http) {

    $http.get("/Contact/GetAllContacts/").then(successCallback, errorCallback);

    function successCallback(response) {
        $scope.contacts = response.data;
    }
    function errorCallback(error) {
        console.log(error);
    }
}])