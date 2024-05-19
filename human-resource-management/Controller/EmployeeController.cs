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
            this.employeeRepository = repository;
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
                        $"Vị trí: {item.Position}, " +
                        $"Phòng ban: {item.Department}"
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

            employee.Sex = InputGender();

            Console.Write("Nhập lương: ");
            int salaryInput = int.Parse(Console.ReadLine() ?? string.Empty);
            employee.Salary = salaryInput.ToString("N0", new CultureInfo("vi-VN"));

            Console.Write("Vị trí làm việc: ");
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

        public void SortEmployeesBy(Func<EmployeeModel, IComparable> keySelector)
        {
            List<EmployeeModel> employees = employeeRepository.GetAll();
            for (int i = 0; i < employees.Count - 1; i++)
            {
                for (int j = 0; j < employees.Count - i - 1; j++)
                {
                    if (keySelector(employees[j]).CompareTo(keySelector(employees[j + 1])) > 0)
                    {
                        var temp = employees[j];
                        employees[j] = employees[j + 1];
                        employees[j + 1] = temp;
                    }
                }
            }
        }

        public void SortEmployeesByLastName()
        {
            SortEmployeesBy(employee => GetLastName(employee.Name));
        }

        private static string GetLastName(string fullName)
        {
            if (string.IsNullOrEmpty(fullName))
            {
                return string.Empty;
            }
            var parts = fullName.Split(' ');
            return parts.Length > 0 ? parts[^1] : string.Empty;
        }

        public void FilterEmployee()
        {
            Console.Write("Nhập tên nhân viên cần cập nhật: ");
            string name = Console.ReadLine() ?? string.Empty;
            List<EmployeeModel> employees = employeeRepository.GetAll();

            employees.Sort((x, y) => string.Compare(x.Name, y.Name));

            int index = BinarySearchByName(employees, name);

            if (index == -1)
            {
                Console.WriteLine($"Không tìm thấy người có tên '{name}' trong danh sách.");
            }
            else
            {
                Console.WriteLine($"Tìm thấy người có tên '{name}' tại vị trí {employees[index].Id}");
                Console.WriteLine(
                        $"Mã nhân viên: {employees[index].Id}, " +
                        $"Tên nhân viên: {employees[index].Name}, " +
                        $"Ngày sinh: {employees[index].Birthday.ToShortDateString()}, " +
                        $"Giới tính: {employees[index].Sex.ToVietnameseString()}, " +
                        $"Lương: {employees[index].Salary}, " +
                        $"Vị trí: {employees[index].Position}, " +
                        $"Phòng ban: {employees[index].Department}"
                    );

            }

        }

        private static int BinarySearchByName(List<EmployeeModel> employees, string value)
        {
            int left = 0;
            int right = employees.Count - 1;
            while (left <= right)
            {
                int mid = left + (right - left) / 2;
                int result = string.Compare(employees[mid].Name, value);

                if (result == 0)
                {
                    return mid;
                }
                else if (result < 0)
                {
                    left = mid + 1;
                }
                else
                {
                    right = mid - 1;
                }

            }

            return -1;
        }

        private static GenderEnum InputGender()
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