angular.module('LibraryModule').factory('LibraryService', Service);

function Service($http, $localStorage, localStorageService) {

    var service = {};

    service.GetKnjiznica = GetKnjiznica;
    service.GetOdjel = GetOdjel;
    return service;


    function GetKnjiznica() {
        if ($localStorage.Knjiznica != undefined) {
            return $localStorage.Knjiznica;
        } else {

            $http.get('api/Knjiznica/GetAll')
            .then(function (response) {

                $localStorage.Knjiznica = response.data[0];
                return response.data[0];
            },
            function () {

            })

        }
    }

    function GetOdjel() {
        if ($localStorage.Odjel != undefined) {
            return $localStorage.Odjel;
        }
    }

}