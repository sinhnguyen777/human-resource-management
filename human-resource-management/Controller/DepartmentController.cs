using System;
using System.Collections.Generic;
using human_resource_management.Model;
using human_resource_management.utils;

namespace human_resource_management.Controller
{
    public class DepartmentController
    {
        private readonly DepartmentRepository departmentRepository;
        private readonly EmployeeRepository employeeRepository;
        public List<DepartmentModel> GetAllDepartmentsList()
        {
            return departmentRepository.GetAll();
        }

        public DepartmentController(DepartmentRepository departmentRepository, EmployeeRepository employeeRepository)
        {
            this.departmentRepository = departmentRepository;
            this.employeeRepository = employeeRepository;
        }

        public void GetAllDepartments()
        {
            List<DepartmentModel> departments = departmentRepository.GetAll();
            Console.WriteLine("Danh sách phòng ban:");
            Console.WriteLine("{0, -20}| {1, -28}| {2, -25}| {3, -25}",
            "Mã phòng ban",
            "Tên phòng ban",
            "Số Nhân viên tối đa",
            "Số Nhân viên hiện có");
            Console.WriteLine("---------------------------------------------------------------------------------------------------");

            if (departments.Count == 0)
            {
                Console.WriteLine("Danh sách rỗng, hiện tại chưa có phòng ban nào \n");
            }
            else
            {
                foreach (DepartmentModel item in departments)
                {
                    Console.WriteLine("{0, -20}| {1, -28}| {2, -25}| {3, -25}",
                        item.Id,
                        item.Name,
                        item.TeamSize,
                        item.ListEmployees != null ? item.ListEmployees.Count() : 0
                    );
                }

            }
        }

        public void AddDepartment()
        {
            DepartmentModel department = new DepartmentModel();

            Console.Write("Nhập mã phòng ban: ");
            department.DepartmentCode = Console.ReadLine();

            Console.Write("Nhập tên phòng ban: ");
            department.Name = InputValidator.stringValidate();

            Console.Write("Nhập số lượng nhân viên: ");
            department.TeamSize = int.Parse(InputValidator.intValidate());

            // int id = departments.Last().Id;

            // department.Id = ++id;

            departmentRepository.Add(department);
        }

        public void DeleteDepartment()
        {
            Console.Write("Nhập ID phòng ban cần xóa: ");
            int id = int.Parse(InputValidator.intValidate());

            DepartmentModel department = departmentRepository.GetById(id);

            if (department == null)
            {
                Console.WriteLine("Không tìm thấy phòng ban cần xóa!");
            }
            else
            {
                departmentRepository.Delete(department);
                Console.WriteLine("Xóa phòng ban thành công!");
            }
        }

        public void GetEmployeesByDepartment()
        {
            List<DepartmentModel> departments = departmentRepository.GetAll();

            Console.WriteLine("Chọn phòng ban:");
            for (int i = 0; i < departments.Count; i++)
            {
                Console.WriteLine($"{i + 1}. {departments[i].Name}");
            }

            Console.Write("Lựa chọn của bạn (nhập số): ");
            int choice;
            while (!int.TryParse(Console.ReadLine(), out choice) || choice <= 0 || choice > departments.Count)
            {
                Console.WriteLine("Lựa chọn không hợp lệ. Vui lòng nhập lại.");
                Console.Write("Lựa chọn của bạn (nhập số): ");
            }

            DepartmentModel selectedDepartment = departments[choice - 1];

            // Lấy danh sách nhân viên từ EmployeeRepository
            List<EmployeeModel> employees = employeeRepository.GetAll();

            // Tạo danh sách để lưu trữ nhân viên của phòng ban đã chọn
            List<EmployeeModel> employeesInDepartment = employees
                .Where(e => e.IdDepartment == selectedDepartment.Id)
                .ToList();

            if (employeesInDepartment.Count == 0)
            {
                Console.WriteLine($"Phòng ban '{selectedDepartment.Name}' hiện tại chưa có nhân viên nào \n");
            }
            else
            {
                Console.WriteLine($"Danh sách nhân viên trong phòng ban '{selectedDepartment.Name}': \n");

                Console.WriteLine("{0, -18}| {1, -25}| {2, -18}| {3, -20}| {4, -20}| {5, -10}",
                    "Mã nhân viên",
                    "Tên nhân viên",
                    "Ngày sinh",
                    "Giới tính",
                    "Lương",
                    "Vị trí");
                Console.WriteLine("------------------------------------------------------------------------------------------------------------------");
                foreach (var item in employeesInDepartment)
                {
                    Console.WriteLine("{0, -18}| {1, -25}| {2, -18}| {3, -20}| {4, -20}| {5, -10}",
                        item.Id,
                        item.Name,
                        item.Birthday.ToShortDateString(),
                        item.Sex.ToVietnameseString(),
                        $"{item.Salary} VNĐ",
                        item.Position);
                }
            }
        }
    }
}