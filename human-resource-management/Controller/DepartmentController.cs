using System;
using System.Collections.Generic;
using human_resource_management.Modal;

namespace human_resource_management.Controller
{
    public class DepartmentController
    {
        public static List<DepartmentModal> departments = new List<DepartmentModal>
        {
            new DepartmentModal() {
                Id = 1, 
                Name = "Development", 
                Manager = new EmployeeModal() {
                    Id = 1,
                    Name = "Lê Tiến Dũng",
                    Birthday = new DateTime(),
                    Salary = 15000,
                    Sex = "Nam",
                }, 
                TeamSize = 10,
                Employees = new List<EmployeeModal>
                {
                    new EmployeeModal()
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