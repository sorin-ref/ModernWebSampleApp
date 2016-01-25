using ModernWebSampleApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;

namespace ModernWebSampleApp.Controllers
{
    public class EmployeesController : ApiController
    {
        OrganizationDatabaseEntities context = new OrganizationDatabaseEntities();

        public IEnumerable<EmployeeDto> GetEmployees(int pageIndex = 0, int pageSize = int.MaxValue)
        {
            return context.Employees
                .OrderBy(e => e.LastName).ThenBy(e => e.FirstName)
                .Skip(pageIndex * pageSize).Take(pageSize).
                Select(e => new EmployeeDto
                {
                    Id = e.Id,
                    FirstName = e.FirstName,
                    LastName = e.LastName,
                    DayOfBirth = e.DayOfBirth,
                    EmailAddress = e.EmailAddress,
                    DepartmentId = e.DepartmentId,
                    DepartmentName = e.Department != null ? e.Department.Name : null
                });
        }

        public IEnumerable<EmployeeDto> GetEmployeesForDepartment(int departmentId, int pageIndex = 0, int pageSize = int.MaxValue)
        {
            return context.Employees
                .Where(e => e.DepartmentId == departmentId)
                .OrderBy(d => d.LastName).ThenBy(d => d.FirstName)
                .Skip(pageIndex * pageSize).Take(pageSize)
                .Select(e => new EmployeeDto
                {
                    Id = e.Id,
                    FirstName = e.FirstName,
                    LastName = e.LastName,
                    DayOfBirth = e.DayOfBirth,
                    EmailAddress = e.EmailAddress,
                    DepartmentId = e.DepartmentId,
                    DepartmentName = e.Department != null ? e.Department.Name : null
                });
        }

        public EmployeeDto GetEmployee(int employeeId)
        {
            var employee = context.Employees
                .Single(e => e.Id == employeeId);
            return new EmployeeDto
            {
                Id = employee.Id,
                FirstName = employee.FirstName,
                LastName = employee.LastName,
                DayOfBirth = employee.DayOfBirth,
                EmailAddress = employee.EmailAddress,
                DepartmentId = employee.DepartmentId,
                DepartmentName = employee.Department?.Name
            };
        }

        [HttpPut]
        public IHttpActionResult AddEmployee(EmployeeDto newEmployeeDto)
        {
            var newEmployee = new Employee
            {
                FirstName = newEmployeeDto.FirstName,
                LastName = newEmployeeDto.LastName,
                DayOfBirth = newEmployeeDto.DayOfBirth,
                EmailAddress = newEmployeeDto.EmailAddress,
                DepartmentId = newEmployeeDto.DepartmentId,
                AvailableSince = DateTime.Now
            };
            context.Employees.Add(newEmployee);
            context.SaveChanges();
            return Ok(newEmployeeDto);
        }

        [HttpPost]
        public IHttpActionResult UpdateEmployee(int employeeId, EmployeeDto updatedEmployeeDto)
        {
            var employee = context.Employees
                .Single(e => e.Id == employeeId);
            employee.FirstName = updatedEmployeeDto.FirstName;
            employee.LastName = updatedEmployeeDto.LastName;
            employee.DayOfBirth = updatedEmployeeDto.DayOfBirth;
            employee.EmailAddress = updatedEmployeeDto.EmailAddress;
            employee.DepartmentId = updatedEmployeeDto.DepartmentId;
            context.SaveChanges();
            return Ok(updatedEmployeeDto);
        }

        [HttpDelete]
        public IHttpActionResult DeleteEmployee(int employeeId)
        {
            var employee = context.Employees
                .Single(e => e.Id == employeeId);
            context.Employees.Remove(employee);
            context.SaveChanges();
            return Ok();
        }
    }
}
