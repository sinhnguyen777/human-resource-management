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

        public DepartmentController(DepartmentRepository departmentRepository, EmployeeRepository employeeRepository)
        {
            this.departmentRepository = departmentRepository;
            this.employeeRepository = employeeRepository;
        }

        public void GetAllDepartments()
        {
            List<DepartmentModel> departments = departmentRepository.GetAll();
            Console.WriteLine("Danh sách phòng ban:");
            Console.WriteLine("{0, -20}| {1, -28}| {2, -25}| {3, -25}| {4, -20}",
            "Mã phòng ban",
            "Tên phòng ban",
            "Số Nhân viên tối đa",
            "Số Nhân viên hiện có",
            "Trưởng phòng");
            Console.WriteLine("-----------------------------------------------------------------------------------------------------------------------");

            if (departments.Count == 0)
            {
                Console.WriteLine("Danh sách rỗng, hiện tại chưa có phòng ban nào \n");
            }
            else
            {
                foreach (DepartmentModel item in departments)
                {
                    string manager;
                    manager = item.IdManager != null ? employeeRepository.GetById(item.IdManager ?? 0).Name : "Không có trưởng phòng";
                    Console.WriteLine("{0, -20}| {1, -28}| {2, -25}| {3, -25}| {4, -20}",
                        item.Id,
                        item.Name,
                        item.TeamSize,
                        item.ListEmployees != null ? item.ListEmployees.Count() : 0,
                        manager
                    );
                }

            }
        }


        public void AddDepartment()
        {
            DepartmentModel department = new DepartmentModel();

            Console.Write("Nhập tên phòng ban: ");
            department.Name = InputValidator.stringValidate();

            Console.Write("Nhập số lượng nhân viên: ");
            department.TeamSize = int.Parse(InputValidator.intValidate());

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

        public void UpdateDepartment()
        {
            DepartmentModel? department = null;
            int id;
            Console.Write("Nhập ID phòng ban cần sửa: ");
            id = int.Parse(InputValidator.intValidate());

            while ((department = departmentRepository.GetById(id)) == null)
            {
                Console.WriteLine("Không tìm thấy phòng ban cần sửa! Vui lòng thử lại.");
                Console.Write("Nhập ID phòng ban cần sửa: ");
                id = int.Parse(InputValidator.intValidate());
            }

            if (department.ListEmployees == null)
            {
                department.ListEmployees = new List<int>();
            }


            Console.Write("Nhập tên phòng ban: ");
            department.Name = InputValidator.stringValidate();

            int numberDepartment;

            while (true)
            {
                Console.Write("Nhập số lượng nhân viên: ");
                numberDepartment = int.Parse(InputValidator.intValidate());

                if (department.ListEmployees.Count > numberDepartment)
                {
                    Console.WriteLine("Số lượng nhân viên tối đa không được nhỏ hơn số lượng nhân viên hiện tại đang có trong phòng vui lòng thử lại");
                }
                else
                {
                    break;
                }
            }
            department.TeamSize = numberDepartment;
            departmentRepository.Update(department);
            Console.WriteLine("Cập nhật phòng ban thành công.");
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
        public void AssignManager()
        {
            List<EmployeeModel> employees = employeeRepository.GetAll();
            List<DepartmentModel> departments = departmentRepository.GetAll();
            Console.WriteLine();
            Console.WriteLine("Danh sách phòng ban:");
            int index = 1;
            foreach (DepartmentModel department in departments)
            {
                Console.WriteLine($"{index++}. {department.Name}");
            }
            int departmentChoice;
            while (true)
            {
                Console.Write("Chọn phòng ban cần chỉ định trưởng phòng: ");
                departmentChoice = int.Parse(InputValidator.intValidate());
                if (departmentChoice > 0 && departmentChoice < index)
                {
                    break;
                }
                Console.WriteLine();
                Console.WriteLine("Lựa chọn không phù hợp, vui lòng nhập lại.");

            }
            DepartmentModel selectedDepartment = departments[departmentChoice - 1];
            if (selectedDepartment.ListEmployees != null)
            {
                Console.WriteLine();
                Console.WriteLine($"Danh sách nhân viên thuộc phòng ban {selectedDepartment.Name}:");
                index = 1;
                List<EmployeeModel> selectedDepartmentEmployees = employees.FindAll(employee => employee.IdDepartment == selectedDepartment.Id);

                foreach (EmployeeModel employee in selectedDepartmentEmployees)
                {
                    Console.WriteLine($"{index++}. Tên: {employee.Name}, ID: {employee.Id}");
                }
                int employeeChoice;
                while (true)
                {
                    Console.Write("Chọn nhân viên làm trưởng phòng: ");
                    employeeChoice = int.Parse(InputValidator.intValidate());
                    if (employeeChoice > 0 && employeeChoice < index)
                    {
                        break;
                    }
                    Console.WriteLine();
                    Console.WriteLine("Lựa chọn không phù hợp, vui lòng nhập lại.");

                }
                EmployeeModel selectedEmployee = selectedDepartmentEmployees[employeeChoice - 1];
                selectedDepartment.IdManager = selectedEmployee.Id;
                Console.WriteLine();
                Console.Write($"Đã chỉ định nhân viên \"{selectedEmployee.Name}\" có ID: {selectedEmployee.Id} trở thành trưởng phòng của phòng ban {selectedDepartment.Name}.");
                Console.WriteLine();
            }
            else
            {
                Console.WriteLine();
                Console.WriteLine("Phòng ban chưa có nhân viên trực thuộc");
            }
        }
        public void FilterDepartment()
        {
            Console.Write("Nhập tên phòng ban cần tìm: ");
            string name = InputValidator.stringValidate();
            Console.WriteLine();
            List<DepartmentModel> departments = new List<DepartmentModel>(departmentRepository.GetAll());

            departments.Sort((x, y) => string.Compare(x.Name, y.Name));

            int index = BinarySearch.BinarySearchByName(departments, name);

            if (index == -1)
            {
                Console.WriteLine($"Không tìm thấy phòng ban có tên '{name}' trong danh sách.");
            }
            else
            {
                Console.WriteLine($"Tìm thấy phòng ban có tên '{name}' có ID: {departments[index].Id}");
                int departmentEmployeeCount = departments[index].ListEmployees != null ? departments[index].ListEmployees.Count() : 0;
                String? manager = departments[index].IdManager != null ? employeeRepository.GetById(departments[index].IdManager ?? 0).Name : "Không có trưởng phòng";
                Console.WriteLine(
                        $"Mã phòng ban: {departments[index].Id}, " +
                        $"Tên phòng ban: {departments[index].Name}, " +
                        $"Số nhân viên tối đa: {departments[index].TeamSize}, " +
                        $"Số nhân viên hiện có: {departmentEmployeeCount}, " +
                        $"Trưởng phòng: {manager}, "
                    );

            }

        }

        public void SortDepartmentBy(Func<DepartmentModel, IComparable> keySelector)
        {
            List<DepartmentModel> departments = departmentRepository.GetAll();
            int length = departments.Count();
            for (int i = 0; i < length - 1; i++)
            {
                int swap = i;
                for (int j = i + 1; j < length; j++)
                {
                    if (keySelector(departments[swap]).CompareTo(keySelector(departments[j])) > 0)
                    {
                        swap = j;
                    }
                }
                DepartmentModel temp = departments[i];
                departments[i] = departments[swap];
                departments[swap] = temp;
            }
        }
    }
}