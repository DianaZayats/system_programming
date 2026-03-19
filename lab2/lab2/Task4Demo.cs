using System;
using System.Threading;
using System.Threading.Tasks;

namespace lab2
{
    internal static class Task4Demo
    {
        public static void Run()
        {
            Console.WriteLine("--- Task 4 ---");
            Console.WriteLine("Enter number for factorial:");
            string factorialInput = Console.ReadLine();

            Console.WriteLine("Enter N for sum:");
            string sumInput = Console.ReadLine();

            // Обидва значення мають бути коректними додатними цілими числами.
            if (!int.TryParse(factorialInput, out int factorialNumber) ||
                !int.TryParse(sumInput, out int sumNumber) ||
                factorialNumber <= 0 ||
                sumNumber <= 0)
            {
                Console.WriteLine("Invalid value.");
                return;
            }

            // Паралельно запускаємо три незалежні методи.
            // Invoke повернеться лише тоді, коли завершаться всі три делегати.
            Parallel.Invoke(
                () => CalculateFactorial(factorialNumber),
                () => CalculateSum(sumNumber),
                PrintMessages
            );

            Console.WriteLine("All methods have completed.");
        }

        private static void CalculateFactorial(int number)
        {
            // Для факторіала використовуємо long, щоб уникнути дуже раннього переповнення int.
            long result = 1;
            for (int i = 1; i <= number; i++)
            {
                result *= i;
            }

            Console.WriteLine("Factorial(" + number + ") = " + result);
        }

        private static void CalculateSum(int n)
        {
            // Обчислюємо суму від 1 до N звичайним циклом.
            int sum = 0;
            for (int i = 1; i <= n; i++)
            {
                sum += i;
            }

            Console.WriteLine("Sum 1.." + n + " = " + sum);
        }

        private static void PrintMessages()
        {
            // Допоміжний метод, що імітує окрему роботу з паузою.
            // Дає змогу візуально побачити одночасне виконання з іншими методами.
            for (int i = 0; i < 5; i++)
            {
                Console.WriteLine("Message method is running...");
                Thread.Sleep(300);
            }
        }
    }
}
