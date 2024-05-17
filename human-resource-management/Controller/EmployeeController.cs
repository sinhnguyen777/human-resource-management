using System;
using System.Collections.Generic;
using System.Globalization;
using human_resource_management.Model;
using human_resource_management.utils;

namespace human_resource_management.Controller
{
    public class EmployeeController
    {
        private readonly EmployeeRepository employeeRepository;

        public EmployeeController(EmployeeRepository repository)
        {
            this.employeeRepository = new EmployeeRepository();
        }

        public void GetAllListEmployees()
        {
            List<EmployeeModel> employees = employeeRepository.GetAll();

            if (employees.Count == 0)
            {
                Console.WriteLine("Danh sách rỗng hiện tại chưa có nhân viên nào \n");
            }
            else
            {
                Console.WriteLine("Danh sách nhân viên: \n");

                foreach (var item in employees)
                {
                    Console.WriteLine(
                        $"Mã nhân viên: {item.Id}, " +
                        $"Tên nhân viên: {item.Name}, " +
                        $"Ngày sinh: {item.Birthday.ToShortDateString()}, " +
                        $"Giới tính: {item.Sex.ToVietnameseString()}, " +
                        $"Lương: {item.Salary}, " +
                        $"Vị trí: {item.Position}"
                    );
                }
            }
        }

        public void AddEmployee()
        {
            EmployeeModel employee = new EmployeeModel();

            Console.Write("Nhập tên nhân viên: ");
            employee.Name = Console.ReadLine() ?? string.Empty;

            employee.Birthday = DateValidator.GetValidDateOfBirth();

            employee.Sex = GetSexFromUserInput();

            Console.Write("Nhập lương: ");
            int salaryInput = int.Parse(Console.ReadLine() ?? string.Empty);
            employee.Salary = salaryInput.ToString("N0", new CultureInfo("vi-VN"));

            Console.Write("Vị trí làm việc:");
            employee.Position = Console.ReadLine() ?? string.Empty;

            employeeRepository.Add(employee);
            Console.WriteLine("Thêm nhân viên thành công.");
        }

        public void UpdateEmployee(EmployeeModel employee)
        {
            employeeRepository.Update(employee);
            Console.WriteLine("Cập nhật nhân viên thành công.");
        }

        public void DeleteEmployee(EmployeeModel employee)
        {
            employeeRepository.Delete(employee);
            Console.WriteLine("Employee deleted successfully.");
        }

        private static GenderEnum GetSexFromUserInput()
        {
            while (true)
            {
                Console.WriteLine("Chọn giới tính: ");
                Console.WriteLine("1. Nam");
                Console.WriteLine("2. Nữ");
                Console.WriteLine("3. Giới tính khác");
                Console.Write("Lựa chọn của bạn: ");
                int choice = int.Parse(Console.ReadLine() ?? string.Empty);

                switch (choice)
                {
                    case 1:
                        return GenderEnum.Male;
                    case 2:
                        return GenderEnum.Female;
                    case 3:
                        return GenderEnum.Other;
                    default:
                        Console.WriteLine("Lựa chọn không hợp lệ, vui lòng chọn lại.");
                        break;
                }
            }
        }
    }
}