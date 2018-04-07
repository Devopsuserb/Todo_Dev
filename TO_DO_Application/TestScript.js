/// <reference path="scripts/angular.js" />

var TestDemo = angular.module("TestModule", []);

TestDemo.controller("TestController", function ($scope) {
    $scope.message = "God bless me";
});

var MainControllerVariable = angular.module("MainModule", ['ngRoute']);
MainControllerVariable.controller("MainController", function ($scope) {
    $scope.message = "God bless me and God bless America";
    var tasks = [{ Name: "Get up early", Status: "Active" }, { Name: "Go to work", Status: "Active" },
        { Name: "Sleep to night", Status: "Active" }];
        $scope.tasks = tasks;
        var employees = [{ Name: "Vamsi", LastName: "Palli", Age: 25 },
{ Name: "Krishna", LastName: "Vamsi", Age: 26 },
        { Name: "Veena", LastName: "Puranam", Age: 29 }];
    var ButtonText = 
    $scope.Employees = employees;

});


MainControllerVariable.config(['$routeProvider', function ($routeProvider) {
    $routeProvider.
        when('/Details', {
            templateUrl: 'Views/Details.html',
            controller: 'DetailsController'
        }).
        when('/Charts', {
            templateUrl: 'Views/Charts.html',
            controller: 'ChartsController'
        }).
        when('/History', {
            templateUrl: 'Views/History.html',
            controller: 'HistoryController'
        }).
        otherwise({
            redirect: 'Home'
        });
}]);

MainControllerVariable.controller("DetailsController", function ($scope) {
    $scope.message = "In Details page";

});


MainControllerVariable.controller("ChartsController", function ($scope) {
    $scope.message = "In Charts page";

});

MainControllerVariable.controller("HistoryController", function ($scope) {
    $scope.message = "In History page";

});
