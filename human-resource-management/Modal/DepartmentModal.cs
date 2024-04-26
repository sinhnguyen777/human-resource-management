using System.Collections.Generic;

namespace human_resource_management.Modal
{
    public class DepartmentModal
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int TeamSize { get; set; }
        public List<EmployeeModal> Employees { get; set; }
        public EmployeeModal Manager { get; set; }
    }
}