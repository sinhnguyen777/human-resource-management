using human_resource_management.Model;

namespace human_resource_management.Data
{
    public class DepartmentData
    {
        public List<DepartmentModel> departments = new List<DepartmentModel>() {
            new DepartmentModel() {
                Id = 1,
                Name = "Information Technology",
                TeamSize = 10,
                IdManager = 5,
                ListEmployees = new List<int>{5},
            },
            new DepartmentModel() {
                Id = 2,
                Name = "Financial Planning",
                TeamSize = 1,
                IdManager = 3,
                ListEmployees = new List<int>{3},
            },
            new DepartmentModel() {
                Id = 3,
                Name = "Technical",
                TeamSize = 5,
                IdManager = 4,
                ListEmployees = new List<int>{4},
            },
            new DepartmentModel() {
                Id = 4,
                Name = "Security",
                TeamSize = 3,
                IdManager = 2,
                ListEmployees = new List<int>{2},
            },
            new DepartmentModel() {
                Id = 5,
                Name = "Human Resource",
                TeamSize = 3,
                IdManager = 1,
                ListEmployees = new List<int>{1},
            },
            new DepartmentModel() {
                Id = 6,
                Name = "Marketing",
                TeamSize = 7,
            },
        };
    }
}