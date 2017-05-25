//Define tournaments controller
angular.module('LibraryModule').controller('registriranikorisniciController', ['$scope', '$http', '$stateParams', '$window', '$state','$rootScope', '$q', registriranikorisniciController]);

function registriranikorisniciController($scope, $http, $stateParams, $window, $state, $rootScope, $q) {

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


    getBorrowedBooksFromUser = function (id) {

        $http.get('/api/PosudenaKnjiga/GetByUserId?id=' + id).then(
                function (response) {

                    var books = [];

                    for(var i = 0; i < response.data.length; i++){
                        if(response.data[i].Vracena == true)
                            continue;
                        books.push(response.data[i]);
                    }

                    for(var i = 0; i < books.length; i++){

                        var oneDay = 24 * 60 * 60 * 1000; // hours*minutes*seconds*milliseconds
                        var firstDate = new Date();
                        var secondDate = new Date(books[i].IstekRokaDatum);
                        var diffDays;
                        if (firstDate.getTime() > secondDate.getTime())
                            diffDays = Math.round(Math.abs((secondDate.getTime() - firstDate.getTime()) / (oneDay)));
                        else
                            diffDays = 0;

                        books[i]['Zakasnina'] = diffDays;
                    }

                    $scope.knjige = books;
                },
                function (data) {
                    return null;
                })
    }


    $scope.getUserInfo = function() {

        var userID = $stateParams.Id;
        getBorrowedBooksFromUser(userID);

    }


}