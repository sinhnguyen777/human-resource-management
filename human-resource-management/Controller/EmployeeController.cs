using System;
using System.Collections.Generic;
using System.Globalization;
using human_resource_management.Model;
using human_resource_management.utils;
using System.Text;


namespace human_resource_management.Controller
{
    public class EmployeeController
    {
        private readonly EmployeeRepository employeeRepository;
        private readonly DepartmentRepository departmentRepository;
        public EmployeeController(EmployeeRepository repository, DepartmentRepository departmentRepository)
        {
            this.employeeRepository = repository;
            this.departmentRepository = departmentRepository;
        }
        public void GetAllListEmployees()
        {
            List<EmployeeModel> employees = employeeRepository.GetAll();

            if (employees.Count == 0)
            {
                Console.WriteLine("Danh sách rỗng, hiện tại chưa có nhân viên nào \n");
            }
            else
            {
                Console.WriteLine("Danh sách nhân viên: \n");

                Console.WriteLine("{0, -18}| {1, -25}| {2, -18}| {3, -20}| {4, -20}| {5, -10}| {6, -10}",
                "Mã nhân viên",
                "Tên nhân viên",
                "Ngày sinh",
                "Giới tính",
                "Lương",
                "Vị trí",
                "Phòng ban");
                Console.WriteLine("------------------------------------------------------------------------------------------------------------------------------------");
                foreach (var item in employees)
                {
                    string departmentName = departmentRepository.GetDepartmentNameById(item.IdDepartment ?? 0);
                    Console.WriteLine("{0, -18}| {1, -25}| {2, -18}| {3, -20}| {4, -20}| {5, -10}| {6, -10}",
                    item.Id,
                    item.Name,
                    item.Birthday.ToShortDateString(),
                    item.Sex.ToVietnameseString(),
                    $"{item.Salary} VNĐ",
                    item.Position,
                    departmentName);
                }
            }
        }

        public void AddEmployee()
        {
            EmployeeModel employee = new EmployeeModel();

            Console.Write("Nhập tên nhân viên: ");
            employee.Name = InputValidator.stringValidate();

            employee.Birthday = DateValidator.GetValidDateOfBirth();

            employee.Sex = InputGender();

            Console.Write("Nhập lương: ");
            int salaryInput = int.Parse(InputValidator.intValidate());
            employee.Salary = salaryInput.ToString("N0", new CultureInfo("vi-VN"));

            Console.Write("Vị trí làm việc: ");
            employee.Position = InputValidator.stringValidate();

            int departmentId = InputDepartment();
            employee.IdDepartment = departmentId;

            employeeRepository.Add(employee);
            int newEmployeeId = employee.Id;

            DepartmentModel department = departmentRepository.GetById(departmentId);
            if (department != null)
            {
                if (department.ListEmployees == null)
                {
                    department.ListEmployees = new List<int>();
                }
                department.ListEmployees.Add(newEmployeeId);
                departmentRepository.Update(department);
            }
            Console.WriteLine("Thêm nhân viên thành công.");
        }

        public void DeleteEmployee()
        {
            Console.Write("Nhập ID nhân viên cần xóa: ");
            int id = int.Parse(InputValidator.intValidate());
            EmployeeModel employee = employeeRepository.GetById(id);
            if (employee == null)
            {
                Console.WriteLine("Không tìm thấy nhân viên có ID: " + id);
            }
            else
            {
                employeeRepository.Delete(employee);
                Console.WriteLine("Xóa nhân viên thành công.");
            }

        }

        public void UpdateEmployee(EmployeeModel employee)
        {
            employeeRepository.Update(employee);
            Console.WriteLine("Cập nhật nhân viên thành công.");
        }
        public void DeleteEmployee(EmployeeModel employee)
        {
            employeeRepository.Delete(employee);
            Console.WriteLine("Xóa nhân viên thành công.");
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
            Console.Write("Nhập tên nhân viên cần tìm: ");
            string name = InputValidator.stringValidate();
            Console.WriteLine();
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
                        $"Phòng ban: {employees[index].IdDepartment}"
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
                int choice = int.Parse(InputValidator.intValidate());

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

        private int InputDepartment()
        {
            List<DepartmentModel> departments = departmentRepository.GetAll();
            Console.WriteLine("Chọn phòng ban:");
            int index = 1;
            foreach (var department in departments)
            {
                Console.WriteLine($"{index}. {department.Name}");
                index++;
            }
            int selectedDepartment = -1;
            bool validChoice = false;
            while (!validChoice)
            {
                Console.Write("Lựa chọn của bạn (nhập số): ");
                if (int.TryParse(Console.ReadLine(), out int choice) && choice > 0 && choice <= departments.Count)
                {
                    selectedDepartment = departments[choice - 1].Id;
                    validChoice = true;
                }
                else
                {
                    Console.WriteLine("Lựa chọn không hợp lệ. Vui lòng nhập lại.");
                }
            }
            return selectedDepartment;
        }

        public void ExportDataEmployeesToFile()
        {
            Console.Write("Nhập tên file để xuất dữ liệu (ví dụ: employees.txt): ");
            string fileName = InputValidator.stringValidate();

            if (string.IsNullOrWhiteSpace(fileName))
            {
                fileName = "employees.txt";
            }

            string userDocumentsPath = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
            string filePath = Path.Combine(userDocumentsPath, fileName);

            List<EmployeeModel> employees = employeeRepository.GetAll();

            using (StreamWriter writer = new StreamWriter(filePath, false, Encoding.Unicode))
            {
                writer.WriteLine("Danh sách nhân viên:");

                writer.WriteLine("{0, -18}| {1, -25}| {2, -18}| {3, -20}| {4, -20}| {5, -10}| {6, -10}",
                "Mã nhân viên",
                "Tên nhân viên",
                "Ngày sinh",
                "Giới tính",
                "Lương",
                "Vị trí",
                "Phòng ban");
                writer.WriteLine("------------------------------------------------------------------------------------------------------------------------------------");
                foreach (var item in employees)
                {
                    string departmentName = departmentRepository.GetDepartmentNameById(item.IdDepartment ?? 0);
                    writer.WriteLine("{0, -18}| {1, -25}| {2, -18}| {3, -20}| {4, -20}| {5, -10}| {6, -10}",
                    item.Id,
                    item.Name,
                    item.Birthday.ToShortDateString(),
                    item.Sex.ToVietnameseString(),
                    $"{item.Salary} VNĐ",
                    item.Position,
                    departmentName);
                }
            }

            Console.WriteLine($"Dữ liệu nhân viên đã được xuất ra file: {filePath}");
        }
    }
}