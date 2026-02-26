using System;

namespace Lab1.Task2
{
    internal class Program
    {
        static void Main(string[] args)
        {
            const int N = 10;

            var counter1 = new CounterThread("CounterThread-1", N);
            var counter2 = new CounterThread("CounterThread-2", N);
            var background = new BackgroundThread();

            counter1.Start();
            counter2.Start();
            background.Start();

            counter1.Join();
            counter2.Join();  // тільки foreground — background не чекаємо

            Console.WriteLine();
            Console.WriteLine("Foreground threads finished. Background thread has IsBackground=true,");
            Console.WriteLine("so it will be stopped when the process exits (e.g. after pressing Enter).");
            Console.WriteLine("Press Enter to exit...");
            Console.ReadLine();  // без Enter background крутився б далі
        }
    }
}
