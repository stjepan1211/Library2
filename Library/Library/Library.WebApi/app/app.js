var LibraryModule = angular.module('LibraryModule', ['ui.router', 'ngStorage', 'LocalStorageModule', 'angularUtils.directives.dirPagination']);

LibraryModule.config(function ($stateProvider, $urlRouterProvider, localStorageServiceProvider) {
    $urlRouterProvider.otherwise('/');

    $stateProvider
        //Root view
        .state('knjiznica', {
            url: '/knjiznica',
            views: {
                "root": {
                    templateUrl: 'app/views/knjiznica.html',
                }
            }
        })
        //Child views
        .state('knjiznica.add', {
            url: '/knjiznica.add',
            views: {
                "root": {
                    templateUrl: 'app/views/knjiznica.add.html',
                }
            }
        })
        .state('knjiznica.edit', {
            url: '/knjiznica.edit',
            views: {
                "root": {
                    templateUrl: 'app/views/knjiznica.edit.html',
                }
            }
        })
        .state('knjiznica.delete', {
            url: '/knjiznica.delete',
            views: {
                "root": {
                    templateUrl: 'app/views/knjiznica.delete.html',
                }
            }
        })
        //Root view
        .state('odjeli', {
            url: '/odjeli',
            views: {
                "root": {
                    templateUrl: 'app/views/odjeli.html'
                }
            }
        })
        //Child views
        .state('odjeli.add', {
            url: '/odjeli.add',
            views: {
                "root": {
                    templateUrl: 'app/views/odjeli.add.html',
                }
            }
        })
        .state('odjeli.edit', {
            url: '/odjeli.edit/:Id',
            views: {
                "odjeli.edit": {
                    templateUrl: 'app/views/odjeli.edit.html',
                }
            }
        })
        .state('odjeli.knjige', {
            url: '/odjeli.knjige/:Id',
            views: {
                "books": {
                    templateUrl: 'app/views/odjeli.knjige.html',
                }
            }
        })

        //Root view
        .state('popisknjiga', {
            url: '/popisknjiga',
            views: {
                "root": {
                    templateUrl: 'app/views/popisknjiga.html'
                }
            }
        })
        //Child views
        .state('popisknjiga.add', {
            url: '/popisknjiga.add',
            views: {
                "root": {
                    templateUrl: 'app/views/popisknjiga.add.html'
                }
            }
        })
        .state('popisknjiga.edit', {
            url: '/popisknjiga.edit/:Id',
            views: {
                "popisknjiga.edit": {
                    templateUrl: 'app/views/popisknjiga.edit.html',
                }
            }
        })
        //Root view
        .state('registriranikorisnici', {
            url: '/registriranikorisnici',
            views: {
                "root": {
                    templateUrl: 'app/views/registriranikorisnici.html'
                }
            }
        })
        //Root view
        .state('registracija', {
            url: '/registracija',
            views: {
                "root": {
                    templateUrl: 'app/views/registracija.html'
                }
            }
        })
        //Root view
        .state('prijava', {
            url: '/prijava',
            views: {
                "root": {
                    templateUrl: 'app/views/prijava.html'
                }
            }
        })
        //Root view
        //Root view
        .state('profil', {
            url: '/profil',
            views: {
                "root": {
                    templateUrl: 'app/views/profil.html',
                }
            }
        })


});

LibraryModule.run(function run($rootScope, $http, $location, $localStorage, $state, AuthenticationService) {
    AuthenticationService.CheckIsStoraged();
});