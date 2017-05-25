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

        $http.get('/api/PosudenaKnjiga/GetByUserId?id=' + userId).then(
            function (response) {
                $scope.error = false;

                var books = [];

                //First remove returned books
                for (var i = 0; i < response.data.length; i++) {

                    if (response.data[i].Vracena == true) {                      
                        continue;
                    }

                    books.push(response.data[i]);

                }
               
                for (var i = 0; i < books.length; i++) {
                                       
                    var oneDay = 24 * 60 * 60 * 1000; // hours*minutes*seconds*milliseconds
                    var firstDate = new Date();
                    var secondDate = new Date(books[i].IstekRokaDatum);
                    var diffDays;
                    if (firstDate.getTime() > secondDate.getTime())
                        diffDays = Math.round(Math.abs((secondDate.getTime() - firstDate.getTime()) / (oneDay)));
                    else
                        diffDays = 0;

                    books[i]['Zakasnina'] = diffDays
                }

                $scope.posudeneKnjige = books;
               
            },
            function (data) {
                //Errors
                console.log("Error");
                console.log("Error message: " + data.errorMessage);
                $scope.error = true;
            })



    }



    $scope.vrati = function (ID, knjigaID) {

        // set vraceno to true
        //Update request
        $http.put('/api/PosudenaKnjiga/UpdateToReturned?id=' + ID).then(
           function (response) {
               $scope.error = false;
               $scope.response = response.data;
               console.log("Update to returned:");
               console.log(response.data);

               //+1 book 
               if (response.data == 0) {
                   $window.alert("Can't return book");
                   return null;
               }


               $http.put('/api/Knjiga/UpdatePlusOne?id=' + knjigaID).then(
              function (response) {
                  $scope.error = false;

                  initController();
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