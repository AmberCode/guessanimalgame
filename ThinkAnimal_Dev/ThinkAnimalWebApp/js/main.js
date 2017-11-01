//Set Think Animal Web Api Url
var webApiUrlBase = 'http://localhost:60489/Api/';

var thinkAnimalApp = angular.module('thinkAnimalApp', [
    'ngRoute',
    'thinkAnimalController',
    'thinkAnimalService'
]);

thinkAnimalApp.config(function ($routeProvider) {

    $routeProvider
    .when('/', {
        templateUrl: 'views/Home.html'
    });

});