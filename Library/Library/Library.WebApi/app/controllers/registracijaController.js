//Define tournaments controller
angular.module('LibraryModule').controller('registracijaController', ['$scope', '$http', '$stateParams', '$window', '$state','$location', registracijaController]);

function registracijaController($scope, $http, $stateParams, $window, $state, $location) {

    $scope.ConfirmedPassword = undefined;

    $scope.userData = {
        Ime: undefined,
        Prezime: undefined,
        Email: undefined,
        Username: undefined,
        Password: undefined
    }

    $scope.$on('LOAD', function () {
        $scope.loading = true;
    })
    $scope.$on('UNLOAD', function () {
        $scope.loading = false;
    })

    $scope.registration = function () {
        if ($scope.userData.Password.length < 6)
            $window.alert("Lozinka je prekratka.");
        else if ($scope.userData.Password != $scope.ConfirmedPassword)
            $window.alert("Lozinka i potvrđena lozinka se ne slažu.");
        else
        {
            var korisnik = {
                Ime: $scope.userData.Ime,
                Prezime: $scope.userData.Prezime,
                Email: $scope.userData.Email,
                Username: $scope.userData.Username,
                Password: $scope.userData.Password
            }
            $scope.$emit('LOAD');
            $http.post('/api/Korisnik/Add', korisnik)
                        .then(function (response) {
                            $scope.$emit('UNLOAD');
                            $window.alert("Registracija uspješna");
                            $location.path('/prijava');
                        }, function (response) {
                            $scope.$emit('UNLOAD');
                            $window.alert("Pogreska: " + response.data.Message);
                        });
        }


       
    }


}