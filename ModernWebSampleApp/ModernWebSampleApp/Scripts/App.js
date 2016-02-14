var module = angular.module('OrganizationApp', ['ngRoute', 'DepartmentControllers', 'EmployeeControllers']);
module.config(function ($routeProvider) {
    $routeProvider.
        when('/departments', {
        templateUrl: 'Views/DepartmentList.html',
        controller: 'DepartmentListController'
    }).
        when('/departments/new', {
        templateUrl: 'Views/DepartmentNewItem.html',
        controller: 'DepartmentItemController'
    }).
        when('/departments/:departmentId', {
        templateUrl: 'Views/DepartmentItem.html',
        controller: 'DepartmentItemController'
    }).
        when('/employees', {
        templateUrl: 'Views/EmployeeList.html',
        controller: 'EmployeeListController'
    }).
        when('/employees/new', {
        templateUrl: 'Views/EmployeeNewItem.html',
        controller: 'EmployeeItemController'
    }).
        when('/employees/:employeeId', {
        templateUrl: 'Views/EmployeeItem.html',
        controller: 'EmployeeItemController'
    }).
        otherwise({
        redirectTo: '/departments'
    });
});
module.controller('NavigationController', function ($scope, $location) {
    $scope.isActive = function (path) {
        return $location.path().indexOf(path) == 0;
    };
});
module.controller('EmployeeListController', function ($scope, $http) {
    $http.get('api/Employees')
        .then(function (response) {
        $scope.employees = response.data;
    });
});
//# sourceMappingURL=App.js.map