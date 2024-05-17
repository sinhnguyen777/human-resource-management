using System.Collections.Generic;

namespace human_resource_management.Model
{
    public class ProjectModel
    {
        public int Id { get; set; }
        public required string Name { get; set; }
        public int TeamSize { get; set; }
        public required List<EmployeeModel> Employees { get; set; }
    }
}