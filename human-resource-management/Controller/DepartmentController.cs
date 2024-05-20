using System;
using System.Collections.Generic;
using human_resource_management.Model;
using human_resource_management.Data;

namespace human_resource_management.Controller
{
    public class DepartmentController
    {
        private readonly DepartmentRepository departmentRepository;

        public DepartmentController(DepartmentRepository repository)
        {
            departmentRepository = repository;
        }

        public void GetAllDepartments()
        {
            List<DepartmentModel> departments = departmentRepository.GetAll();
            Console.WriteLine("Danh sách phòng ban:");

            if (departments.Count == 0)
            {
                Console.WriteLine("Danh sách rỗng, hiện tại chưa có phòng ban nào \n");
            }
            else
            {
                foreach (var item in departments)
                {
                    Console.WriteLine($"ID: {item.Id}, Tên: {item.Name} \n");
                }
            }
        }

        public void AddDepartment()
        {
            DepartmentModel department = new DepartmentModel();

            Console.Write("Nhập tên phòng ban: ");
            department.Name = Console.ReadLine() ?? string.Empty;

            Console.Write("Nhập số lượng nhân viên: ");
            department.TeamSize = int.Parse(Console.ReadLine() ?? string.Empty);

            departmentRepository.Add(department);
        }

        public void DeleteDepartment()
        {
            Console.Write("Nhập ID phòng ban cần xóa: ");
            int id = int.Parse(Console.ReadLine() ?? string.Empty);

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
    }

}