using System;
using System.Threading;
using System.Threading.Tasks;

namespace lab2
{
    internal static class Task2Demo
    {
        public static void Run()
        {
            Console.WriteLine("--- Task 2 ---");

            // Створюємо три окремі задачі з однаковим тілом виконання.
            Task task1 = new Task(CountToFive);
            Task task2 = new Task(CountToFive);
            Task task3 = new Task(CountToFive);

            // Показуємо Id кожної створеної задачі.
            Console.WriteLine("Task 1 Id = " + task1.Id);
            Console.WriteLine("Task 2 Id = " + task2.Id);
            Console.WriteLine("Task 3 Id = " + task3.Id);

            // Запускаємо всі задачі паралельно.
            task1.Start();
            task2.Start();
            task3.Start();

            // Чекаємо завершення всіх трьох задач.
            Task.WaitAll(task1, task2, task3);
        }

        private static void CountToFive()
        {
            // Усередині задачі показуємо Task.CurrentId та поточний крок.
            for (int step = 1; step <= 5; step++)
            {
                Console.WriteLine("Running task CurrentId = " + Task.CurrentId + ", step = " + step);
                Thread.Sleep(200);
            }
        }
    }
}
