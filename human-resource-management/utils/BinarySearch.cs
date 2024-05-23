using System;
using human_resource_management.Model;

namespace human_resource_management.utils
{
    public static class BinarySearch
    {
        public static int BinarySearchByName<T>(List<T> list, string value) where T : class
        {
            int left = 0;
            int right = list.Count - 1;
            while (left <= right)
            {
                int mid = left + (right - left) / 2;
                var midValue = typeof(T).GetProperty("Name").GetValue(list[mid]) as string;
                int result = string.Compare(midValue, value);

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
    }
}