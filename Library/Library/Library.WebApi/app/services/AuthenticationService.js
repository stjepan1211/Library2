angular.module('LibraryModule').factory('AuthenticationService', Service);

function Service($http, $localStorage, localStorageService) {

    var service = {};

    service.Login = Login;
    service.Logout = Logout;
    service.CheckIsStoraged = CheckIsStoraged;
    service.Check = Check;
    service.GetUsername = GetUsername;
    service.GetId = GetId;
    service.GetRole = GetRole;
    return service;

    function Login(username, password, callback) {
        var obj = {
            Username: username,
            Password: password
        };
        $http.post('/api/Korisnik/Login', obj)
        .then(function successCallback(response) {
            if (response.data.Role && response.data.Token && response.data.UserName) {
                // store username, id and token in local storage
                $localStorage.currentUser = {
                    UserName: response.data.UserName,
                    Token: response.data.Token,
                    Role: response.data.Role,
                    Id: response.data.Id
                };
                // add jwt token to auth header for all requests made by the $http service
                $http.defaults.headers.common.Authorization = 'Bearer ' + response.data.Token.TokenString;

                // execute callback with true to indicate successful login
                callback(true);
            } else {
                // execute callback with false to indicate failed login
                callback(false);
            }
        }, function errorCallback(response) {

            callback(response);
        });
    }

    function Logout() {
        // remove user from local storage and clear http auth header
        delete $localStorage.currentUser;
        $http.defaults.headers.common.Authorization = '';
    }

    function CheckIsStoraged() {
        var dateTime = new Date();
        var miliseconds = dateTime.getTime();

        if ($localStorage.currentUser != undefined) {
            //check if token has expired
            if ($localStorage.currentUser.Token.ExpirationTime < miliseconds) {
                Logout();
            }
        }
    }

    function Check() {
        if ($localStorage.currentUser != undefined && $http.defaults.headers.common.Authorization != '') {
            //console.log($localStorage.currentUser.UserName);
            return true;
        }
        else
            return false;
    }

    function GetUsername() {
        if ($localStorage.currentUser != undefined) {
            return $localStorage.currentUser.UserName;
        }
    }

    function GetRole() {
        if ($localStorage.currentUser != undefined) {
            return $localStorage.currentUser.Role;
        }
    }

    function GetId() {
        if ($localStorage.currentUser != undefined) {
            var id = $localStorage.currentUser.Id;
            return id;
        }
    }

}