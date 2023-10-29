using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UtilityBot.Utilities
{
    public class NumSum
    {
        public static int Sum(string message)
        {
            try
            {
                string[] numbers = message.Split(' ');
                int sum = 0;
                foreach (string number in numbers)
                {
                    if (!string.IsNullOrWhiteSpace(number))
                    {
                        if (int.TryParse(number, out int num))
                        {
                            sum += num;
                        }
                        else
                        {
                            throw new Exception("Неверный формат числа");
                        }
                    }
                }
                return sum;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Ошибка: {ex.Message}");
                return 0;
            }
        }
    }
}
