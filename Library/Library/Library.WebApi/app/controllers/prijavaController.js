//Define tournaments controller
angular.module('LibraryModule').controller('prijavaController', ['$scope', '$http', '$stateParams', '$window', '$state','AuthenticationService', prijavaController]);

function prijavaController($scope, $http, $stateParams, $window, $state, AuthenticationService) {

    provjeriJeLiPrijavljen();
    isAdmin();
    isKorisnik();

    function provjeriJeLiPrijavljen() {
        AuthenticationService.CheckIsStoraged();
        if (AuthenticationService.Check())
            $scope.prijavljen = true;
        else
            $scope.prijavljen = false;
    }

    $scope.userData = {
        Username: undefined,
        Password: undefined
    }

    $scope.$on('LOAD', function () {
        $scope.loading = true;
    })
    $scope.$on('UNLOAD', function () {
        $scope.loading = false;
    })

    $scope.login = function () {
        var userToLogin = {
            Username: $scope.userData.Username,
            Password: $scope.userData.Password
        }

        $scope.$emit('LOAD');

        AuthenticationService.Login(userToLogin.Username, userToLogin.Password, function (result) {

            if (result === true) {
                $scope.$emit('UNLOAD');
                $window.alert("Prijavljeni ste.");
                location.reload(true);
                $state.go('knjiznica');
            } else {
                $scope.$emit('UNLOAD');
                $window.alert("Can't log in.");
            }
        });
    }

    $scope.Check = function () {
        AuthenticationService.CheckIsStoraged();
        if (AuthenticationService.Check()) {
            $scope.prijavljen = true;
        }
        else {
            $scope.prijavljen = false;
        }
    }

    $scope.LogOut = function () {
        if ($window.confirm('Jeste sigurni da se želite odjaviti?')) {
            AuthenticationService.Logout();
            location.reload(true);
            $state.transitionTo('knjiznica', null, { 'reload': true });
        };
    }

     function isAdmin() {
        if (AuthenticationService.GetRole() == 'Admin') {
            $scope.isAdmin = true;
        }
        else {
            $scope.isAdmin = false;
        }
     }

     function isKorisnik() {
         if (AuthenticationService.GetRole() == 'Korisnik') {
             $scope.isKorisnik = true;
         }
         else {
             $scope.isKorisnik = false;
         }
     }

}