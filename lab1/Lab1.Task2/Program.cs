using System;
using System.Threading;

namespace Lab1.Task2
{
    internal class Program
    {
        static void Main(string[] args)
        {
            const int N = 10;

            var cts = new CancellationTokenSource();
            var counter1 = new CounterThread("CounterThread-1", N);
            var counter2 = new CounterThread("CounterThread-2", N);
            var background = new BackgroundThread(cts.Token);

            counter1.Start();
            counter2.Start();
            background.Start();

            counter1.Join();
            counter2.Join();  // чекаємо обидва foreground

            cts.Cancel();  // даємо сигнал background — foreground закінчились, виходимо з циклу

            background.Join();  // чекаємо, поки background коректно завершиться

            Console.WriteLine();
            Console.WriteLine("All threads finished. Press Enter to exit...");
            Console.ReadLine();
        }
    }
}
