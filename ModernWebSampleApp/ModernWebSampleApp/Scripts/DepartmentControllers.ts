declare var angular;
var module = angular.module('DepartmentControllers', ['ngRoute']);
module.controller('DepartmentListController', function ($scope, $http) {
    $http.get('api/Departments')
        .then((response) => {
            $scope.departments = response.data;
        });
});
module.controller('DepartmentItemController', function ($scope, $http, $routeParams, $location) {
    var departmentId = $routeParams.departmentId;
    if (departmentId) {
        $http.get('api/Departments?departmentId=' + departmentId)
            .then((response) => {
                $scope.department = response.data;
            });
        $http.get('api/Employees?departmentId=' + departmentId)
            .then((response) => {
                $scope.departmentEmployees = response.data;
            });
    }
    $scope.saveDepartment = () => {
        if (departmentId) {
            $http.post('api/Departments?departmentId=' + departmentId, $scope.department)
                .then(() => {
                    $location.path('/departments');
                });
        }
        else {
            $http.put('api/Departments', $scope.department)
                .then(() => {
                    $location.path('/departments');
                });
        }
    };
    $scope.deleteDepartment = () => {
        if (departmentId && confirm('Are you sure you want to delete this department?')) {
            $http.delete('api/Departments?departmentId=' + departmentId)
                .then(() => {
                    $location.path('/departments');
                });
        }
    };
});
