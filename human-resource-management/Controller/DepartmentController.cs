using System;
using System.Collections.Generic;
using human_resource_management.Data;
using human_resource_management.Model;
using human_resource_management.utils;

namespace human_resource_management.Controller
{
    public class DepartmentController
    {
        private readonly DepartmentRepository departmentRepository;
        private readonly DepartmentData departmentData = new DepartmentData();

        public DepartmentController(DepartmentRepository repository)
        {
            foreach (var item in departmentData.departments)
            {
                repository.Add(item);
            }

            departmentRepository = repository;
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

            Console.Write("Nhập mã nhân viên: ");
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
    }

}