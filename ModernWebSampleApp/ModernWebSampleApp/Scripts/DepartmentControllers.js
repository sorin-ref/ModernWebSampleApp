var module = angular.module('DepartmentControllers', ['ngRoute']);
module.controller('DepartmentListController', function ($scope, $http) {
    $http.get('api/Departments')
        .then(function (response) {
        $scope.departments = response.data;
    });
});
module.controller('DepartmentItemController', function ($scope, $http, $routeParams, $location) {
    var departmentId = $routeParams.departmentId;
    if (departmentId) {
        $http.get('api/Departments?departmentId=' + departmentId)
            .then(function (response) {
            $scope.department = response.data;
        });
        $http.get('api/Employees?departmentId=' + departmentId)
            .then(function (response) {
            $scope.departmentEmployees = response.data;
        });
    }
    $scope.saveDepartment = function () {
        if (departmentId) {
            $http.post('api/Departments?departmentId=' + departmentId, $scope.department)
                .then(function () {
                $location.path('/departments');
            });
        }
        else {
            $http.put('api/Departments', $scope.department)
                .then(function () {
                $location.path('/departments');
            });
        }
    };
    $scope.deleteDepartment = function () {
        if (departmentId && confirm('Are you sure you want to delete this department?')) {
            $http.delete('api/Departments?departmentId=' + departmentId)
                .then(function () {
                $location.path('/departments');
            });
        }
    };
});
