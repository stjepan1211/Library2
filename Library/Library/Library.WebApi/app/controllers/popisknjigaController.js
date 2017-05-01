//Define tournaments controller
angular.module('LibraryModule').controller('popisknjigaController', ['$scope', '$http', '$stateParams', '$window', '$state', 'AuthenticationService', 'LibraryService', popisknjigaController]);

function popisknjigaController($scope, $http, $stateParams, $window, $state, AuthenticationService, LibraryService) {

    initController();

    function initController() {
        checkIsKnjiznicaAdded();
        getAllKnjiga();
        getAllOdijel();

        $scope.isAdminAndLogged = (AuthenticationService.GetRole() == 'Admin')
           && AuthenticationService.GetRole() ? true : false;
    }

    function checkIsKnjiznicaAdded() {
        $http.get('api/Knjiznica/GetAll')
            .then(function (response) {

                $scope.isKnjiznicaAdded = true;
                $scope.knjiznica = response.data;
            },
            function () {
                $scope.isKnjiznicaAdded = false;
                console.log(response.data.Message);
            })
    }

    // Get all knjiga function
    function getAllKnjiga() {
        $http.get('/api/Knjiga/GetAll').then(
            function (response) {
                $scope.error = false;
                $scope.knjige = response.data;
            },
            function (data) {
                //Errors
                console.log("Error");
                console.log("Error message: " + data.errorMessage);
                $scope.error = true;
                $scope.errorMessage = data.errorMessage;
            })
    };
    
    // Get all sections function
    function getAllOdijel() {
        $http.get('/api/Odjel/GetAll').then(
            function (response) {
                $scope.error = false;
                $scope.odjeli = response.data;
            },
            function (data) {
                //Errors
                console.log("Error");
                console.log("Error message: " + data.errorMessage);
                $scope.error = true;
                $scope.errorMessage = data.errorMessage;
            })
    };

    $scope.addknjiga = function () {
        console.log($scope.Odjel);
        if ($scope.Naslov == null || $scope.Autor == null || $scope.UkupanBroj == null || $scope.Odjel == undefined) {
            $window.alert("Molimo unesite ispravne podatke");
            return;
        }
        console.log($scope.Odjel);
        console.log($scope.Odjel.ID);
        //Model
        var knjiga = {
            Naslov: $scope.Naslov,
            Autor: $scope.Autor,
            UkupanBroj: $scope.UkupanBroj,
            OdjelID: $scope.Odjel.ID
        };

        //Post request
        $http.post('/api/Knjiga/Add', knjiga).then(
           function (response) {
               $scope.error = false;
               $scope.response = response.data;
               console.log(response.data);
               //location.reload(true);
           },
           function (data) {
               //Errors
               console.log("Error");
               console.log("Error message: " + data.errorMessage);
               $scope.error = true;
               $scope.errorMessage = data.errorMessage;
               $window.alert(data.Message);

           })

        //Očisti model i formu
        $scope.addknjigaform.$setPristine(true);
        $scope.Naslov = null;
        $scope.Autor = null;
        $scope.UkupanBroj = null;

        $state.go('popisknjiga');
        location.reload(true);
    }

    $scope.obrisiknjigu = function (knjigaId) {
        if (knjigaId == undefined) {
            console.log("Nepoznat id knjige za brisanje.");
        }
        else {
            if ($window.confirm('Jeste li sigurni da želite obrisati knjigu?')) {
                $http.delete('/api/Knjiga/Delete?id=' + knjigaId)
                    .then(function (response) {
                        $window.alert("Knjiga uspješno obrisana.");
                    }, function (response) {
                        $window.alert("Greška:" + response.data.Message);
                    })
            }
        }
        $state.go('odjeli');
        $state.reload();
        location.reload(true);
    }

    $scope.urediPopisKnjiga = function () {
        if ($scope.NaslovEdit == undefined || $scope.AutorEdit == undefined || $scope.UkupanBrojEdit == undefined || $scope.OdjelEdit == undefined) {
            console.log("Neispravan unos");
            $scope.erorrEdit = true;
            return;
        }
        else {
            $scope.erorrEdit = false;
            //Model
            var knjiga = {
                ID: $stateParams.Id,
                Naslov: $scope.NaslovEdit,
                Autor: $scope.AutorEdit,
                UkupanBroj: $scope.UkupanBrojEdit,
                OdjelId: $scope.OdjelEdit.ID
            };

            //Update request
            $http.put('/api/Knjiga/Update', knjiga).then(
               function (response) {
                   $scope.error = false;
                   $scope.response = response.data;
                   console.log(response.data);
                   //location.reload(true);
                   //Reload page to remove form
               },
               function (data) {
                   //Errors
                   console.log(data);
                   console.log("Error");
                   console.log("Error message: " + data.data.errorMessage);
                   $scope.error = true;
                   $scope.errorMessage = data.data.errorMessage;
               })
        }

        //Očisti model i formu
        $scope.uredipopisknjigaform.$setPristine(true);
        $scope.NaslovEdit = null;
        $scope.AutorEdit = null;
        $scope.UkupanBrojEdit = null;
        $scope.OdjelEdit = null;
        location.reload(true);
    }
}