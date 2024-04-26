using System.Collections.Generic;

namespace human_resource_management.Modal
{
    public class ProjectModal
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int TeamSize { get; set; }
        public List<EmployeeModal> Employees { get; set; }
    }
}