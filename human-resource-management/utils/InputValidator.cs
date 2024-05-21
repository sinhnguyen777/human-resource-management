namespace human_resource_management.utils
{
    public class InputValidator
    {
        public static string intValidate()
        {
            string input = Console.ReadLine();
            while (!int.TryParse(input, out _))
            {
                Console.Write("Vui lòng không nhập chữ cái hoặc ký tự, Nhập lại: ");
                input = Console.ReadLine();
            }
            return input;
        }
        public static string stringValidate()
        {
            string input = Console.ReadLine();
            while (input.Any(c => char.IsDigit(c)))
            {
                Console.Write("Vui lòng không nhập số, Nhập lại: ");
                input = Console.ReadLine();
            }
            return input;
        }
    }
}