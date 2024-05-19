using human_resource_management.Model;

namespace human_resource_management.Data
{
    public class EmployeeData
    {
        public List<EmployeeModel> employees = new List<EmployeeModel> {
            new EmployeeModel() {Name = "Nguyen Hoang Dat", Birthday = new DateTime(2003, 06, 16),
            Sex = GenderEnum.Male, Salary = "3000000", Position = "IT"},
            new EmployeeModel() {Name = "Nguyen Tan Sinh", Birthday = new DateTime(2001, 02, 04),
            Sex = GenderEnum.Male, Salary = "7000000", Position = "Web"},
            new EmployeeModel() {Name = "Tran Ly Thuy Tien", Birthday = new DateTime(2001, 07, 11),
            Sex = GenderEnum.Female, Salary = "5000000", Position = "IT"},
            new EmployeeModel() {Name = "Mai Thanh Phat", Birthday = new DateTime(1999, 03, 19),
            Sex = GenderEnum.Male, Salary = "10000000", Position = "DevOps"},
            new EmployeeModel() {Name = "Le Tien Dung", Birthday = new DateTime(2003, 08, 12),
            Sex = GenderEnum.Other, Salary = "8000000", Position = "Seo"},
         };
    }
}