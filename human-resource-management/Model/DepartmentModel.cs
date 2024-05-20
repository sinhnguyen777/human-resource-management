using System.Collections.Generic;

namespace human_resource_management.Model
{
    public class DepartmentModel
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public int TeamSize { get; set; }
        public List<int>? Employees { get; set; }
        public int? Manager { get; set; }
    }
}