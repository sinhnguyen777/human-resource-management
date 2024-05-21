using System;
using System.Text;
using human_resource_management.View;
using human_resource_management.Data;
using human_resource_management.Model;

namespace human_resource_management
{
    internal class Program
    {
        private static EmployeeData employeeData = new EmployeeData();
        private static DepartmentData departmentData = new DepartmentData();
        public static void Main(string[] args)
        {
            Console.InputEncoding = Encoding.Unicode;
            Console.OutputEncoding = Encoding.Unicode;
            foreach (EmployeeModel employee in employeeData.employees)
            {
                EmployeeView._employeeRepository.Add(employee);
            }
            foreach (DepartmentModel department in departmentData.departments)
            {
                DepartmentView._departmentRepository.Add(department);
            }

            while (true)
            {
                Console.WriteLine("---------- Hệ thống quản lý nhân sự ----------");
                Console.WriteLine("1. Quản lý phòng ban");
                Console.WriteLine("2. Quản lý nhân viên");
                Console.WriteLine("3. Quản lý dự án");
                Console.WriteLine("0. Thoát");
                Console.Write("Chọn chức năng: ");

                string choice = Console.ReadLine() ?? string.Empty;

                switch (choice)
                {
                    case "1":
                        DepartmentView.ManageDepartments();
                        break;
                    case "2":
                        EmployeeView.ManageEmployees();
                        break;
                    case "3":
                        break;
                    case "0":
                        Environment.Exit(0);
                        break;
                    default:
                        Console.WriteLine("Chức năng không hợp lệ!");
                        break;
                }
            }
        }
    }
}