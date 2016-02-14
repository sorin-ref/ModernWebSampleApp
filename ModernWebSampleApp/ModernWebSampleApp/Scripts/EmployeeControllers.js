var module = angular.module('EmployeeControllers', ['ngRoute']);
module.controller('EmployeeListController', function ($scope, $http) {
    $http.get('api/Employees')
        .then(function (response) {
        $scope.employees = response.data;
    });
});
module.controller('EmployeeItemController', function ($scope, $http, $routeParams, $location) {
    $http.get('api/Departments')
        .then(function (response) {
        var departments = response.data;
        departments.splice(0, 0, { Id: null, Name: '(None)' });
        $scope.departments = departments;
    });
    $scope.employee = { DepartmentId: null };
    var employeeId = $routeParams.employeeId;
    if (employeeId) {
        $http.get('api/Employees?employeeId=' + employeeId)
            .then(function (response) {
            var employee = response.data;
            employee.DayOfBirth = employee.DayOfBirth ? employee.DayOfBirth.substr(0, 10) : null;
            $scope.employee = employee;
        });
    }
    $scope.saveEmployee = function () {
        if (employeeId) {
            $http.post('api/Employees?employeeId=' + employeeId, $scope.employee)
                .then(function () {
                $location.path('/employees');
            });
        }
        else {
            $http.put('api/Employees', $scope.employee)
                .then(function () {
                $location.path('/employees');
            });
        }
    };
    $scope.deleteEmployee = function () {
        if (employeeId && confirm('Are you sure you want to delete this employee?')) {
            $http.delete('api/Employees?employeeId=' + employeeId)
                .then(function () {
                $location.path('/employees');
            });
        }
    };
});
