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
                    
                    arrPromises.push($http.get('/api/Knjiga/Get?id=' + response.data[i].KnjigaID))
                }


                // when all requests finish, push data to posudeneKnjige
                $q.all(arrPromises).then(function (ret) {

                    for (var i = 0; i < ret.length; i++) {
                        var knjiga = ret[i].data;
                        knjiga['PosudenaDatum'] = response.data[i].PosudenaDatum;
                        knjiga['IstekRokaDatum'] = response.data[i].IstekRokaDatum;
                        knjiga['vracena'] = response.data[i].Vracena;
                        
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
                        
                        if (response.data[i].Vracena == false)
                            knjige.push(knjiga)
                    }

                    $scope.posudeneKnjige = knjige;
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

        // set vraceno to true
        //Update request
        $http.put('/api/PosudenaKnjiga/UpdateToReturned?id=' + posudenaKnjigaId).then(
           function (response) {
               $scope.error = false;
               $scope.response = response.data;
               console.log("Update to returned:");
               console.log(response.data);

               //+1 book 
               $http.put('/api/Knjiga/UpdatePlusOne?id=' + knjigaID).then(
              function (response) {
                  $scope.error = false;

                  location.reload(true);
              },
              function (data) {
                  //Errors
                  $scope.error = true;
              })

           },
           function (data) {
               //Errors
               console.log("Error");
               console.log("Error message: " + data.errorMessage);
               $scope.error = true;
               $scope.errorMessage = data.errorMessage;
           })




    }



    $scope.sort = function (keyname) {
        $scope.sortKey = keyname;   //set the sortKey to the param passed
        $scope.reverse = !$scope.reverse; //if true make it false and vice versa
    }

}