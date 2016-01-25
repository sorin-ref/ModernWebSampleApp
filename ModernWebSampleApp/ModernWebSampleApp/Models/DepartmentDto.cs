namespace ModernWebSampleApp.Models
{
    public class DepartmentDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int EmployeeCount { get; internal set; }
    }
}