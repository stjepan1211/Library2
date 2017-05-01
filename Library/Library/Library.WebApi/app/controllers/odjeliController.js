//Define tournaments controller
angular.module('LibraryModule').controller('odjeliController', ['$scope', '$http', '$stateParams', '$window', '$state', 'AuthenticationService', 'LibraryService', odjeliController]);

function odjeliController($scope, $http, $stateParams, $window, $state, AuthenticationService, LibraryService, $modal, $mdDialog) {

    initController();

    function initController() {
        checkIsKnjiznicaAdded();
        getAllOdijel();

        //$scope.error = true;

        //console.log("instanciro se");
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

    // Get all sections function
    function getAllOdijel() {
        $http.get('/api/Odjel/GetAll').then(
            function (response) {
                $scope.error = false;
                $scope.odjeli = response.data;
                
                angular.forEach($scope.odjeli, function (odjel, key) {
                    odjel.UkupanBroj = 0;
                    angular.forEach(odjel.Knjige, function (knjiga, key) {
                        //console.log(knjiga.UkupanBroj);
                        odjel.UkupanBroj = odjel.UkupanBroj + knjiga.UkupanBroj;                      
                    });
                    //console.log(odjel.UkupanBroj);
                });
                //console.log($scope.odjeli);
            },
            function (data) {
                //Errors
                console.log("Error");
                console.log("Error message: " + data.data.Message);
                $scope.error = true;
                $scope.errorMessage = data.data.Message;
            })
    };

    $scope.addOdjel = function () {
        //Provjera podataka
        if ($scope.Naziv == null) {
            $window.alert("Molimo unesite podatke");
            return;
        }

        //Model
        var odjel = {
            Naziv: $scope.Naziv
        };

        //Post request
        $http.post('/api/Odjel/Add', odjel).then(
           function (response) {
               $scope.error = false;
               $scope.response = response.data;
               //console.log(response.data);
           },
           function (data) {
               //Errors
               console.log("Error");
               console.log("Error message: " + data.data.Message);
               $scope.error = true;
               $scope.errorMessage = data.data.Message;
               $window.alert(data.data.Message);
           })

        //Očisti model i formu
        $scope.addodjeliform.$setPristine(true);
        $scope.Naziv = null;

        //Reload data
        initController();
        $state.go('odjeli');
        location.reload(true);
        console.log($scope.error);
    }

    $scope.getBooksForOdjel = function () {
        var odjelId = $stateParams.odjelId;

        $http.get('/api/Knjiga/GetAllKnjigaWhereOdjel?id='+odjelId).then(
           function (response) {
               $scope.error = false;
               $scope.odjelKnjige = response.data;
           },
           function (data) {
               //Errors
               console.log("Error");
               console.log("Error message: " + data.errorMessage);
               $scope.error = true;
               $scope.errorMessage = data.errorMessage;
           })


        
       
       
    }

    //Test
    $scope.AddKnjiga = function (odjel) {

        var knjiga = {
            OdjelId: odjel.Id,
            Naslov: "Test",
            Autor: "AutorTest",
            UkupanBroj: 5
        };

        //Post request
        $http.post('/api/Knjiga/Add', knjiga).then(
           function (response) {
               $scope.error = false;
               $scope.response = response.data;
               console.log(response.data);
           },
           function (data) {
               //Errors
               console.log("Error");
               console.log("Error message: " + data.errorMessage);
               $scope.error = true;
               $scope.errorMessage = data.errorMessage;
           })
    }

    $scope.obrisiodjel = function (odjelId) {
        if (odjelId == undefined) {
            console.log("Nepoznat id odjela za brisanje.");
        }
        else {
            if ($window.confirm('Jeste li sigurni da želite obrisati odjel?')) {
                $http.delete('/api/Odjel/Delete?id=' + odjelId)
                    .then(function (response) {
                        $window.alert("Odjel uspješno obrisan.");
                    }, function (response) {
                        $window.alert("Greška:" + response.data.Message);
                    })
            }
        }
        $state.go('odjeli');
        $state.reload();
        location.reload(true);
    }

    $scope.urediOdjel = function () {
        if ($scope.NazivEdit == undefined) {
            console.log("Neispravan naziv");
            $scope.erorrEdit = true;
            return;
        }
        else
        {
            $scope.erorrEdit = false;
            //Model
            var odjel = {
                Id: $stateParams.Id,
                Naziv: $scope.NazivEdit,
            };

            //Update request
            $http.put('/api/Odjel/Update', odjel).then(
               function (response) {
                   $scope.error = false;
                   $scope.response = response.data;
                   console.log(response.data);
                   //location.reload(true);
                   //Reload page to remove form
               },
               function (data) {
                   //Errors
                   console.log("Error");
                   console.log("Error message: " + data.errorMessage);
                   $scope.error = true;
                   $scope.errorMessage = data.errorMessage;
               })
        }

        //Očisti model i formu
        $scope.urediodjeliform.$setPristine(true);
        $scope.NazivEdit = null;
        location.reload(true);
    }

    $scope.getKnjigeByOdjel = function () {
        //console.log($stateParams.Id);
        $http.get('/api/Knjiga/GetByOdjel?Id=' + $stateParams.Id).then(
           function (response) {
               $scope.error = false;
               $scope.knjige = response.data;
               //console.log($scope.odjeli);
               //console.log($scope.odjeli.Object.ID);
           },
           function (data) {
               //Errors
               console.log("Error");
               console.log("Error message: " + data.data.Message);
               $scope.error = true;
               $scope.errorMessage = data.data.Message;
           })
    }

    $scope.sort = function (keyname) {
        $scope.sortKey = keyname;   //set the sortKey to the param passed
        $scope.reverse = !$scope.reverse; //if true make it false and vice versa
    }
}