using System;
using human_resource_management.Controller;
using human_resource_management.Model;

namespace human_resource_management.View
{
    public class EmployeeView
    {
        private static EmployeeRepository _employeeRepository = new EmployeeRepository();
        private static EmployeeController _employeeController = new EmployeeController(_employeeRepository);

        public static void ManageEmployees()
        {
            while (true)
            {
                Console.WriteLine("---------- Quản lý nhân viên ----------");
                Console.WriteLine("1. Xem danh sách nhân viên");
                Console.WriteLine("2. Thêm nhân viên mới");
                Console.WriteLine("3. Xóa nhân viên");
                Console.WriteLine("4. Sửa thông tin nhân viên");
                Console.WriteLine("5. Tìm kiếm nhân viên theo tên");
                Console.WriteLine("0. Quay lại");
                Console.WriteLine();

                Console.Write("Chọn chức năng: ");

                int choice = int.Parse(Console.ReadLine() ?? string.Empty);

                switch (choice)
                {
                    case 1:
                        _employeeController.GetAllListEmployees();
                        break;
                    case 2:
                        _employeeController.AddEmployee();
                        break;
                    case 3:
                        break;
                    case 4:
                        break;
                    case 5:
                        _employeeController.FilterEmployee();
                        break;
                    case 0:
                        return;
                    default:
                        Console.WriteLine("Chức năng không hợp lệ!");
                        break;
                }
            }
        }
    }
}