using ModernWebSampleApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;

namespace ModernWebSampleApp.Controllers
{
    public class DepartmentsController : ApiController
    {
        OrganizationDatabaseEntities context = new OrganizationDatabaseEntities();

        public IEnumerable<DepartmentDto> GetDepartments(int pageIndex = 0, int pageSize = int.MaxValue)
        {
            return context.Departments
                .OrderBy(d => d.Name)
                .Skip(pageIndex * pageSize).Take(pageSize)
                .Select(d => new DepartmentDto
                {
                    Id = d.Id,
                    Name = d.Name,
                    EmployeeCount = d.Employees.Count
                });
        }

        public DepartmentDto GetDepartment(int departmentId)
        {
            var department = context.Departments.Single(d => d.Id == departmentId);
            return new DepartmentDto
            {
                Id = department.Id,
                Name = department.Name,
                EmployeeCount = department.Employees.Count
            };
        }

        [HttpPut]
        public IHttpActionResult AddDepartment(DepartmentDto newDepartmentDto)
        {
            var newDepartment = new Department
            {
                Name = newDepartmentDto.Name,
                AvailableSince = DateTime.Now
            };
            context.Departments.Add(newDepartment);
            context.SaveChanges();
            newDepartmentDto.Id = newDepartment.Id;
            return Ok(newDepartmentDto);
        }

        [HttpPost]
        public IHttpActionResult UpdateDepartment(int departmentId, DepartmentDto updatedDepartmentDto)
        {
            var department = context.Departments
                .Single(d => d.Id == departmentId);
            department.Name = updatedDepartmentDto.Name;
            context.SaveChanges();
            return Ok(updatedDepartmentDto);
        }

        [HttpDelete]
        public IHttpActionResult DeleteDepartment(int departmentId)
        {
            var department = context.Departments
                .Single(d => d.Id == departmentId);
            foreach (var employee in department.Employees.ToArray())
                department.Employees.Remove(employee);
            context.Departments.Remove(department);
            context.SaveChanges();
            return Ok();
        }
    }
}
