using System;
using System.Globalization;

namespace human_resource_management.utils
{
    public class DateValidator
    {
        public static DateTime GetValidDateOfBirth()
        {
            while (true)
            {
                Console.Write("Nhập ngày sinh (dd/MM/yyyy): ");
                string input = Console.ReadLine() ?? string.Empty;

                if (DateTime.TryParseExact(input, "dd/MM/yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None,
                        out DateTime date))
                {
                    if (IsValidDateOfBirth(date))
                    {
                        return date;
                    }
                    else
                    {
                        Console.WriteLine("Năm sinh phải từ năm 1950 và nhân viên phải lớn hơn 18 tuổi.");
                    }
                }
                else
                {
                    Console.WriteLine("Ngày sinh không hợp lệ. Vui lòng nhập lại theo định dạng dd/MM/yyyy.");
                }
            }
        }

        private static bool IsValidDateOfBirth(DateTime date)
        {
            DateTime now = DateTime.Now;
            DateTime minDate = new DateTime(1950, 1, 1);
            DateTime maxDate = now.AddYears(-18);

            return date >= minDate && date <= maxDate && (now.Year - date.Year > 18 ||
                                                          (now.Year - date.Year == 18 &&
                                                           now.DayOfYear >= date.DayOfYear));
        }
    }
}