var contactManagerApp = angular.module("contactManagerApp", []);

angular.module("contactManagerApp").config(function () {
    angular.lowercase = angular.$$lowercase;
});

contactManagerApp.controller("ContactManagerController", ["$scope", "$http", function ($scope, $http) {

    $http.get("/Contact/GetAllContacts/").then(getAllContactssuccessCallback, getAllContactsErrorCallback);

    function getAllContactssuccessCallback(response) {
        $scope.contacts = response.data;
    }
    function getAllContactsErrorCallback(error) {
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

    $scope.deleteContact = function (contact) {
        var removedContact = $scope.contacts.indexOf(contact);
        $scope.contacts.splice(removedContact, 1);

        $http.delete("/Contact/DeleteContact/" + contact.id).then(deleteContactSuccessCallback, deleteContactsErrorCallback);

        function deleteContactSuccessCallback(response) {
            $scope.deleteContactStatus = response.data;
        }
        function deleteContactsErrorCallback(error) {
            $scope.deleteContactStatus = response.data;
            console.log(error);
        }
    }
}])