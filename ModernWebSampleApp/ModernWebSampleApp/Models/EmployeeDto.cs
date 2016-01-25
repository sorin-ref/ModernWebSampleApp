using System;

namespace ModernWebSampleApp.Models
{
    public class EmployeeDto
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string FullName { get { return $"{FirstName} {LastName}"; } }
        public DateTime? DayOfBirth { get; set; }
        public string EmailAddress { get; set; }
        public int? DepartmentId { get; set; }
        public string DepartmentName { get; internal set; }
    }
}