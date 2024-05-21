using human_resource_management.Model;

namespace human_resource_management.Data
{
    public class EmployeeData
    {
        public List<EmployeeModel> employees = new List<EmployeeModel> {
            new EmployeeModel() {Name = "Nguyễn Hoàng Đạt", Birthday = new DateTime(2003, 06, 16),
            Sex = GenderEnum.Male, Salary = "3.000.000", Position = "IT", IdDepartment = 5},
            new EmployeeModel() {Name = "Nguyễn Tấn Sinh", Birthday = new DateTime(2001, 02, 04),
            Sex = GenderEnum.Male, Salary = "7.000.000", Position = "Web", IdDepartment = 4},
            new EmployeeModel() {Name = "Trần Lý Thủy Tiên", Birthday = new DateTime(2001, 07, 11),
            Sex = GenderEnum.Female, Salary = "5.000.000", Position = "IT", IdDepartment = 2},
            new EmployeeModel() {Name = "Mai Thành Phát", Birthday = new DateTime(1999, 03, 19),
            Sex = GenderEnum.Male, Salary = "10.000.000", Position = "DevOps", IdDepartment = 3},
            new EmployeeModel() {Name = "Lê Tiến Dũng", Birthday = new DateTime(2003, 08, 12),
            Sex = GenderEnum.Other, Salary = "8.000.000", Position = "Seo", IdDepartment = 1},
         };
    }
}