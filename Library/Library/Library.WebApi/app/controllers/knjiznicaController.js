//Define tournaments controller
angular.module('LibraryModule').controller('knjiznicaController', ['$scope', '$http', '$stateParams', '$window', '$state', 'AuthenticationService', 'LibraryService', knjiznicaController]);

function knjiznicaController($scope, $http, $stateParams, $window, $state, AuthenticationService, LibraryService) {

    initController();   

    function checkIsKnjiznicaAdded() {
        $http.get('api/Knjiznica/GetAll')
            .then(function (response) {
                if (response.data == '')
                    $scope.isKnjiznicaAdded = false;
                else {
                    $scope.isKnjiznicaAdded = true;
                    $scope.knjiznica = response.data;
                }
            },
            function () {
                $scope.isKnjiznicaAdded = false;
                console.log(response.data.Message);
            })
    }

    function initController() {
        $scope.isAdminAndLogged = (AuthenticationService.GetRole() == 'Admin')
            && AuthenticationService.GetRole() ? true : false;

        checkIsKnjiznicaAdded();
    }

    $scope.addknjiznica = function () {
        //Provjera podataka
        if ($scope.Naziv == null || $scope.Adresa == null) {
            $window.alert("Molimo unesite podatke");
            return;
        }
        //Model
        var knjiznica = {
            Naziv: $scope.Naziv,
            Adresa: $scope.Adresa,
            BrojOdjela: 0,
            BrojUclanjenih: 0
        };

        //Post request
        $http.post('/api/Knjiznica/Add', knjiznica).then(
           function (response) {
               $scope.error = false;
               $scope.response = response.data;
               console.log(response.data);
               location.reload(true);
           },
           function (data) {
               //Errors
               console.log("Error");
               console.log("Error message: " + data.errorMessage);
               $scope.error = true;
               $scope.errorMessage = data.errorMessage;
           })

        //Očisti model i formu
        $scope.addknjiznicaform.$setPristine(true);
        $scope.Naziv = null;
        $scope.Adresa = null;

        $state.go('knjiznica');
        location.reload(true);

    }
    $scope.urediknjiznica = function () {

        //Provjera podataka
        if ($scope.Naziv == null || $scope.Adresa == null) {
            $window.alert("Molimo unesite podatke");
            return;
        }
        //Model
        var knjiznica = {
            Id: $scope.knjiznica[0].Id,
            Naziv: $scope.Naziv,
            Adresa: $scope.Adresa,
            BrojUclanjenih: $scope.knjiznica[0].BrojUclanjenih
        };

        //Update request
        $http.put('/api/Knjiznica/Update', knjiznica).then(
           function (response) {
               $scope.error = false;
               $scope.response = response.data;
               console.log(response.data);
               location.reload(true);
               //Reload page to remove form
           },
           function (data) {
               //Errors
               console.log("Error");
               console.log("Error message: " + data.errorMessage);
               $scope.error = true;
               $scope.errorMessage = data.errorMessage;
           })

        //Očisti model i formu
        $scope.urediknjiznicaform.$setPristine(true);
        $scope.Naziv = null;
        $scope.Adresa = null;

        initController();
        $state.go('knjiznica');
        location.reload(true);

    }

    $scope.obrisiknjiznica = function (knjiznicaId) {
        if (knjiznicaId == undefined) {
            console.log("Nepoznat id knjiznice za brisanje.");
        }
        else {
            if ($window.confirm('Jeste li sigurni da želite obrisati knjižnicu?')) {
                $http.delete('/api/Knjiznica/Delete?id=' + knjiznicaId)
                    .then(function (response) {
                        $window.alert("Knjižnica uspješno obrisana.");
                    }, function (response) {
                        $window.alert("Greška:" + response.data.Message);
                    })
            }
        }
        $state.go('knjiznica');
        location.reload(true);
    }

}