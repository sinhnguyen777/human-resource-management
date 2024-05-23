using System;
using human_resource_management.repositories;
using human_resource_management.Controller;
using human_resource_management.Model;

namespace human_resource_management.View
{
    public class DepartmentView
    {
        public static readonly DepartmentRepository _departmentRepository = new DepartmentRepository();
        public static readonly EmployeeRepository _employeeRepository = EmployeeView._employeeRepository;
        public static readonly DepartmentController _departmentController = new DepartmentController(_departmentRepository, _employeeRepository);


        public static void ManageDepartments()
        {
            while (true)
            {
                Console.WriteLine();
                Console.WriteLine("---------- Quản lý phòng ban ----------");
                Console.WriteLine("1. Xem danh sách phòng ban");
                Console.WriteLine("2. Thêm phòng ban mới");
                Console.WriteLine("3. Xóa phòng ban");
                Console.WriteLine("4. Sửa thông tin phòng ban");
                Console.WriteLine("5. Sắp xếp phòng ban theo ID");
                Console.WriteLine("6. Sắp xếp phòng ban theo tên");
                Console.WriteLine("7. Tìm kiếm phòng ban theo tên");
                Console.WriteLine("8. Thêm nhân viên vào phòng ban");
                Console.WriteLine("9. Chỉ định trưởng phòng");
                Console.WriteLine("10. Xem danh sách nhân viên theo phòng ban");
                Console.WriteLine("0. Quay lại");
                Console.WriteLine();

                Console.Write("Chọn chức năng: ");
                string choice = Console.ReadLine() ?? string.Empty;

                switch (choice)
                {
                    case "1":
                        _departmentController.GetAllDepartments();
                        break;
                    case "2":
                        _departmentController.AddDepartment();
                        break;
                    case "3":
                        _departmentController.DeleteDepartment();
                        break;
                    case "4":
                        _departmentController.UpdateDepartment();
                        break;
                    case "5":
                        _departmentController.SortDepartmentBy(department => department.Id);
                        _departmentController.GetAllDepartments();
                        break;
                    case "6":
                        _departmentController.SortDepartmentBy(department => department.Name);
                        _departmentController.GetAllDepartments();
                        break;
                    case "7":
                        _departmentController.FilterDepartment();
                        break;
                    case "9":
                        _departmentController.AssignManager();
                        break;
                    case "10":
                        _departmentController.GetEmployeesByDepartment();
                        break;
                    case "0":
                        return;
                    default:
                        Console.WriteLine("Chức năng không hợp lệ!");
                        break;
                }
            }
        }
    }
}