using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace final_project_in_C_
{
    public static class InputUtility
    {
        public static int GetIntInput(string prompt, int minValue, int maxValue)
        {
            int val;
            do
            {
                Console.Write($"{prompt}: ");
                string input = Console.ReadLine();
                if (int.TryParse(input, out val) && val >= minValue && val <= maxValue)
                    break;
                else
                    Console.WriteLine($"Invalid input. Enter a number between {minValue} and {maxValue}.");
            } while (true);
            return val;
        }

        public static double GetDoubleInput(string prompt, double minValue)
        {
            double val;
            do
            {
                Console.Write($"{prompt}: ");
                string input = Console.ReadLine();
                if (double.TryParse(input, out val) && val >= minValue)
                    break;
                else
                    Console.WriteLine($"Invalid input. Enter a number greater or equal to {minValue}.");
            } while (true);
            return val;
        }

        public static string GetNonEmptyString(string prompt)
        {
            string input;
            do
            {
                Console.Write($"{prompt}: ");
                input = Console.ReadLine();
                if (!string.IsNullOrWhiteSpace(input))
                    break;
                else
                    Console.WriteLine($"{prompt} cannot be empty. Please try again.");
            } while (true);
            return input;
        }
    }
}
