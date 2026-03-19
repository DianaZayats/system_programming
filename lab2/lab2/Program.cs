using System;

namespace lab2
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            // Головний цикл меню: працює, доки користувач не вибере вихід.
            while (true)
            {
                // Перед кожним введенням показуємо доступні пункти.
                Console.WriteLine("1 - Task 1");
                Console.WriteLine("2 - Task 2");
                Console.WriteLine("3 - Task 3");
                Console.WriteLine("4 - Task 4");
                Console.WriteLine("0 - Exit");
                Console.Write("Choose an option: ");

                string choice = Console.ReadLine();
                if (choice == "0")
                {
                    // Єдина умова повного завершення програми.
                    return;
                }

                // Запускаємо потрібне завдання за вибором користувача.
                switch (choice)
                {
                    case "1":
                        Task1Demo.Run();
                        break;
                    case "2":
                        Task2Demo.Run();
                        break;
                    case "3":
                        Task3Demo.Run();
                        break;
                    case "4":
                        Task4Demo.Run();
                        break;
                    default:
                        Console.WriteLine("Invalid choice. Try again.");
                        break;
                }

                // Пауза після виконання, щоб користувач встиг побачити результат.
                Console.WriteLine("Task completed.");
                Console.WriteLine("Press any key to return to the menu...");
                Console.ReadKey();
                Console.Clear();
            }
        }
    }
}
