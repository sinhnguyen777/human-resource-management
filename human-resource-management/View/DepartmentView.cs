using System;
using human_resource_management.Controller;

namespace human_resource_management.View
{
    public class DepartmentView
    {
        public static void ManageDepartments()
        {
            while (true)
            {
                Console.WriteLine("======= Quản lý phòng ban =======");
                Console.WriteLine("1. Xem danh sách phòng ban");
                Console.WriteLine("2. Thêm phòng ban mới");
                Console.WriteLine("3. Xóa phòng ban");
                Console.WriteLine("4. Sửa thông tin phòng ban");
                Console.WriteLine("5. Thêm nhân viên vào phòng ban");
                Console.WriteLine("6. Chỉ định trưởng phòng");
                Console.WriteLine("0. Quay lại");
                Console.WriteLine();

                Console.Write("Chọn chức năng: ");
                string choice = Console.ReadLine() ?? string.Empty;

                switch (choice)
                {
                    case "1":
                        DepartmentController.DisplayDepartments();
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