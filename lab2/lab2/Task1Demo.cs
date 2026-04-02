using System;
using System.Threading;
using System.Threading.Tasks;

namespace lab2
{
    internal static class Task1Demo
    {
        public static void Run()
        {
            Console.WriteLine("--- Task 1 ---");

            // Створюємо дві окремі задачі через конструктор Task.
            Task task1 = new Task(PrintNumbers);
            Task task2 = new Task(PrintLetters);

            // Явно запускаємо обидві задачі методом Start().
            task1.Start();
            task2.Start();

            // Очікуємо завершення обох задач.
            Task.WaitAll(task1, task2);
            Console.WriteLine("Both tasks have completed.");
        }

        private static void PrintNumbers()
        {
            // Вивід чисел від 1 до 10 із паузою 200 мс.
            for (int i = 1; i <= 10; i++)
            {
                Console.WriteLine("Numbers Task: " + i);
                Thread.Sleep(200);
            }
        }

        private static void PrintLetters()
        {
            // Вивід літер від A до J із паузою 200 мс.
            for (char letter = 'A'; letter <= 'J'; letter++)
            {
                Console.WriteLine("Letters Task: " + letter);
                Thread.Sleep(200);
            }
        }
    }
}
