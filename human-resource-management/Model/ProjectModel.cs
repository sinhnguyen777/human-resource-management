using System.Collections.Generic;

namespace human_resource_management.Model
{
    public class ProjectModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int TeamSize { get; set; }
        public List<EmployeeModel> Employees { get; set; }
    }
}