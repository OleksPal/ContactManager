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
            $scope.contactStatus = response.data;
        }
        function deleteContactsErrorCallback(error) {
            $scope.contactStatus = response.data;
            console.log(error);
        }
    }

    $scope.selected = {};

    $scope.getTemplate = function (contact) {
        if (contact.id === $scope.selected.id) return 'edit';
        else return 'display';
    };

    $scope.editContact = function (contact) {
        $scope.selected = angular.copy(contact);
    }

    $scope.saveContact = function (idx) {
        var updatedContact = {};
        updatedContact.Name = $scope.selected.name;
        updatedContact.DateOfBirth = $scope.selected.dateOfBirth;
        updatedContact.Married = $scope.selected.married;
        updatedContact.Phone = $scope.selected.phone;
        updatedContact.Salary = $scope.selected.salary;

        $http.put("/Contact/UpdateContact/" + $scope.contacts[idx].id, updatedContact)
            .then(updateContactSuccessCallback, updateContactsErrorCallback);

        function updateContactSuccessCallback(response) {
            $scope.contactStatus = response.data;
        }
        function updateContactsErrorCallback(error) {
            $scope.contactStatus = response.data;
            console.log(error);
        }

        $scope.contacts[idx] = angular.copy($scope.selected);
        $scope.reset();
    };

    $scope.reset = function () {
        $scope.selected = {};
    };
}])