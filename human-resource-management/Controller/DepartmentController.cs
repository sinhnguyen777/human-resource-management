using System;
using System.Collections.Generic;
using human_resource_management.Model;
using human_resource_management.Data;

namespace human_resource_management.Controller
{
    public class DepartmentController
    {
        public static DepartmentData departmentData = new DepartmentData();
        public static List<DepartmentModel> departments = departmentData.departments;

        public static void DisplayDepartments()
        {
            Console.WriteLine("Danh sách phòng ban:");

            foreach (DepartmentModel department in departments)
            {
                Console.WriteLine($"ID: {department.Id}, Tên: {department.Name} \n");
            }
        }
     public static void AddDepartment()
    {
        DepartmentModel department = new DepartmentModel();

        Console.Write("Nhập tên phòng ban: ");
        department.Name = Console.ReadLine() ?? string.Empty;

        Console.Write("Nhập số lượng nhân viên: ");
        department.TeamSize = int.Parse(Console.ReadLine() ?? string.Empty);

        DepartmentController.departments.Add(department);
    }
    }
}
