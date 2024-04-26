using System;

namespace human_resource_management.Modal
{
    public class EmployeeModal
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public DateTime Birthday { get; set; }
        public string Sex { get; set; }
        public decimal Salary { get; set; }
    }
}