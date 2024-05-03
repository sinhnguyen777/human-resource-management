using System;
using System.Collections.Generic;
using human_resource_management.Model;
using human_resource_management.Model;

namespace human_resource_management.Controller
{
    public class DepartmentController
    {
        public static List<DepartmentModel> departments = new List<DepartmentModel>
        {
            new DepartmentModel() {
                Id = 1, 
                Name = "Development", 
                Manager = new EmployeeModel() {
                    Id = 1,
                    Name = "Lê Tiến Dũng",
                    Birthday = new DateTime(),
                    Salary = 15000,
                    Sex = "Nam",
                }, 
                TeamSize = 10,
                Employees = new List<EmployeeModel>
                {
                    new EmployeeModel()
                    {
                        Id = 1,
                        Name = "Lê Tiến Dũng",
                        Birthday = new DateTime(),
                        Salary = 15000,
                        Sex = "Nam",
                    }
                }
            }
        }; 
        
        public static void DisplayDepartments()
        {
            Console.WriteLine("======= Danh sách phòng ban =======");

            foreach (var item in departments)
            {
                Console.WriteLine($"ID: {item.Id}, Tên: {item.Name}");
            }
        }
    }
}