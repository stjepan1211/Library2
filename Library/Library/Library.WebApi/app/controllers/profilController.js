//Define controller
angular.module('LibraryModule').controller('profilController', ['$scope', '$http', '$stateParams', '$window', '$state', 'AuthenticationService', 'LibraryService', '$rootScope', '$q', profilController]);

function profilController($scope, $http, $stateParams, $window, $state, AuthenticationService, LibraryService, $rootScope, $q) {

    initController();

    function initController() {
        $scope.isKorisnikAndLogged = (AuthenticationService.GetRole() == 'Korisnik')
            && AuthenticationService.GetRole() ? true : false;

        //Get books
        userId = AuthenticationService.GetId();
        $scope.Username = AuthenticationService.GetUsername();
        var knjige = [];
        var arrPromises = [];

        $http.get('/api/PosudenaKnjiga/GetByUserId?id=' + userId).then(
            function (response) {
                $scope.error = false;
               
                for (var i = 0; i < response.data.length; i++) {
                    
                    if (response.data[i].Vracena == false)
                        arrPromises.push($http.get('/api/Knjiga/Get?id=' + response.data[i].KnjigaID))
                }


                // when all requests finish, push data to posudeneKnjige
                $q.all(arrPromises).then(function (ret) {

                    for (var i = 0; i < ret.length; i++) {
                        var knjiga = ret[i].data;
                        knjiga['PosudenaDatum'] = response.data[i].PosudenaDatum;
                        knjiga['IstekRokaDatum'] = response.data[i].IstekRokaDatum;

                        
                        var oneDay = 24 * 60 * 60 * 1000; // hours*minutes*seconds*milliseconds
                        var firstDate = new Date();
                        var secondDate = new Date(response.data[i].IstekRokaDatum);
                        var diffDays;
                        if (firstDate.getTime() > secondDate.getTime())
                            diffDays = Math.round(Math.abs((secondDate.getTime() - firstDate.getTime()) / (oneDay)));
                        else
                            diffDays = 0;

                        knjiga['Zakasnina'] = diffDays
                        knjiga['PosudenaKnjigaID'] = response.data[i].ID;
                        

                        knjige.push(knjiga)
                    }

                    $scope.posudeneKnjige = knjige;
                    console.log(knjige);
                });

               
            },
            function (data) {
                //Errors
                console.log("Error");
                console.log("Error message: " + data.errorMessage);
                $scope.error = true;
            })



    }



    $scope.vrati = function (knjigaID, posudenaKnjigaId) {

        // TODO: set vraceno to true
        // TODO: +1 book 
        return null;
    }



    $scope.sort = function (keyname) {
        $scope.sortKey = keyname;   //set the sortKey to the param passed
        $scope.reverse = !$scope.reverse; //if true make it false and vice versa
    }

}