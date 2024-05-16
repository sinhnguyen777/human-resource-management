using System;
using human_resource_management.Model;

namespace human_resource_management.utils
{
    public static class ConvertGenderToVietNam
    {
        public static string ToVietnameseString(this GenderEnum gender)
        {
            return gender switch
            {
                GenderEnum.Male => "Nam",
                GenderEnum.Female => "Nữ",
                GenderEnum.Other => "Giới tính khác",
                _ => throw new ArgumentOutOfRangeException(nameof(gender), gender, null)
            };
        }
    }
}