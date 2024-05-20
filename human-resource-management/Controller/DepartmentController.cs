using System;
using System.Collections.Generic;
using human_resource_management.Model;
using human_resource_management.Data;
using human_resource_management.utils;

namespace human_resource_management.Controller
{
    public class DepartmentController
    {
        public static DepartmentData departmentData = new DepartmentData();
        public static List<DepartmentModel> departments = departmentData.departments;

        public static void DisplayDepartments()
        {
            Console.WriteLine("Danh sách phòng ban:");
            Console.WriteLine("{0, -20}| {1, -28}| {2, -25}| {3, -25}",
            "Mã phòng ban",
            "Tên phòng ban",
            "Số Nhân viên tối đa",
            "Số Nhân viên hiện có");
            Console.WriteLine("---------------------------------------------------------------------------------------------------");

            foreach (DepartmentModel department in departments)
            {
                Console.WriteLine("{0, -20}| {1, -28}| {2, -25}| {3, -25}",
                department.Id,
                department.Name,
                department.TeamSize,
                department.Employees != null ? department.Employees.Count() : 0
                );
            }
        }
        public static void AddDepartment()
        {
            DepartmentModel department = new DepartmentModel();

            Console.Write("Nhập tên phòng ban: ");
            department.Name = InputValidator.stringValidate();

            Console.Write("Nhập số lượng nhân viên: ");
            department.TeamSize = int.Parse(InputValidator.intValidate());

            int id = departments.Last().Id;

            department.Id = ++id;

            DepartmentController.departments.Add(department);
        }
        public static void DeleteDepartment()
        {
            Console.Write("Nhập ID phòng ban cần xóa: ");
            int id = int.Parse(InputValidator.intValidate());

            DepartmentModel department = DepartmentController.departments.Find(d => d.Id == id);

            if (department == null)
            {
                Console.WriteLine("Không tìm thấy phòng ban cần xóa!");
            }
            else
            {
                DepartmentController.departments.Remove(department);
                Console.WriteLine("Xóa phòng ban thành công!");
            }
        }
    }

}