using System;
using System.Threading.Tasks;

namespace lab2
{
    internal static class Task3Demo
    {
        public static void Run()
        {
            Console.WriteLine("--- Task 3 ---");
            Console.WriteLine("Enter N:");

            string input = Console.ReadLine();
            // Перевіряємо, що введено додатне ціле число.
            if (!int.TryParse(input, out int n) || n <= 0)
            {
                Console.WriteLine("Invalid value.");
                return;
            }

            Console.WriteLine("Calculating the sum from 1 to N...");

            // Основна задача обчислює суму і повертає int-результат.
            Task<int> sumTask = new Task<int>(() => CalculateSum(n));
            // Продовження (лямбда) виконується після sumTask і друкує результат.
            // previousTask — це посилання на завершену попередню задачу.
            Task continuationTask = sumTask.ContinueWith(previousTask =>
            {
                Console.WriteLine("Result: " + previousTask.Result);
            });

            // Запускаємо головну задачу; continuation стартує автоматично після неї.
            sumTask.Start();

            // Очікуємо і обчислення, і continuation.
            Task.WaitAll(sumTask, continuationTask);
        }

        private static int CalculateSum(int n)
        {
            // Явне обчислення суми циклом від 1 до N.
            // Такий підхід прозоріший для навчальної демонстрації.
            int sum = 0;
            for (int i = 1; i <= n; i++)
            {
                sum += i;
            }

            return sum;
        }
    }
}
