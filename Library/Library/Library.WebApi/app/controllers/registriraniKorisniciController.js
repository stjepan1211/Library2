//Define tournaments controller
angular.module('LibraryModule').controller('registriranikorisniciController', ['$scope', '$http', '$stateParams', '$window', '$state', registriranikorisniciController]);

function registriranikorisniciController($scope, $http, $stateParams, $window, $state) {

    // Get all users function
    $scope.getAllRegisteredUsers = function()
    {
        $http.get('/api/Korisnik/GetAll').then(
            function (response) {
                $scope.error = false;
                $scope.allUsers = response.data;
            },
            function(data){
                //Errors
                console.log("Error");
                console.log("Error message: " + data.errorMessage);
                $scope.error = true;
                $scope.errorMessage = data.errorMessage;
            })
    };



}