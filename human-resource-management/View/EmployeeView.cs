using System;
using human_resource_management.Controller;
using human_resource_management.Model;
namespace human_resource_management.View
{
    public class EmployeeView
    {
        public static readonly EmployeeRepository _employeeRepository = new EmployeeRepository();
        private static readonly DepartmentRepository _departmentRepository = DepartmentView._departmentRepository;
        private static readonly EmployeeController _employeeController = new EmployeeController(_employeeRepository, _departmentRepository);



        public static void ManageEmployees()
        {

            while (true)
            {
                Console.WriteLine();
                Console.WriteLine("---------- Quản lý nhân viên ----------");
                Console.WriteLine("1. Xem danh sách nhân viên");
                Console.WriteLine("2. Thêm nhân viên mới");
                Console.WriteLine("3. Xóa nhân viên");
                Console.WriteLine("4. Sửa thông tin nhân viên");
                Console.WriteLine("5. Tìm kiếm nhân viên theo tên");
                Console.WriteLine("6. Sắp xếp nhân viên theo mã nhân viên");
                Console.WriteLine("7. Sắp xếp nhân viên theo tên");
                Console.WriteLine("8. Sắp xếp nhân viên theo giới tính");
                Console.WriteLine("9. Sắp xếp nhân viên theo vị trí");
                Console.WriteLine("10. Xuất file dữ liệu nhân viên (ghi file)");
                Console.WriteLine("11. Nhập file dữ liệu nhân viên (đọc file)");
                Console.WriteLine("0. Quay lại");
                Console.WriteLine();

                Console.Write("Chọn chức năng: ");

                string choice = Console.ReadLine() ?? string.Empty;

                switch (choice)
                {
                    case "1":
                        _employeeController.GetAllListEmployees();
                        break;
                    case "2":
                        _employeeController.AddEmployee();
                        break;
                    case "3":
                        _employeeController.DeleteEmployee();
                        break;
                    case "4":
                        _employeeController.UpdateEmployee();
                        break;
                    case "5":
                        _employeeController.FilterEmployee();
                        break;
                    case "6":
                        _employeeController.SortEmployeesBy(employee => employee.Id);
                        _employeeController.GetAllListEmployees();
                        break;
                    case "7":
                        _employeeController.SortEmployeesByLastName();
                        _employeeController.GetAllListEmployees();
                        break;
                    case "8":
                        _employeeController.SortEmployeesBy(employee => employee.Sex);
                        _employeeController.GetAllListEmployees();
                        break;
                    case "9":
                        _employeeController.SortEmployeesBy(employee => employee.Position);
                        _employeeController.GetAllListEmployees();
                        break;
                    case "10":
                        _employeeController.ExportDataEmployeesToFile();
                        break;
                    case "11":
                        _employeeController.ImportDataEmpoyeesToFile();
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