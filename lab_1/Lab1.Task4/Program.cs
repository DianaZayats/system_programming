using System;
using System.Threading;

namespace Lab1.Task4
{
    internal class Program
    {
        private const long Limit = 100_000_000;

        static ThreadPriority ParsePriority(string input)  // рядок -> пріоритет
        {
            switch (input?.ToLowerInvariant())
            {
                case "highest": return ThreadPriority.Highest;
                case "abovenormal": return ThreadPriority.AboveNormal;
                case "normal": return ThreadPriority.Normal;
                case "belownormal": return ThreadPriority.BelowNormal;
                case "lowest": return ThreadPriority.Lowest;
                default: return ThreadPriority.Normal;
            }
        }

        static void Main(string[] args)
        {
            int n;
            do
            {
                Console.Write("Enter N (1..12): ");
                string line = Console.ReadLine();
                if (!int.TryParse(line, out n) || n < 1 || n > 12)  // перевірка діапазону
                {
                    Console.WriteLine("Invalid. Enter a number from 1 to 12.");
                    continue;
                }
                break;
            } while (true);

            var workers = new WorkerThread[n];
            var startSignal = new ManualResetEvent(false);

            Console.WriteLine("Enter priority for each thread: Highest, AboveNormal, Normal, BelowNormal, Lowest");
            for (int i = 0; i < n; i++)
            {
                Console.Write($"Thread {i + 1} priority: ");
                string prioStr = Console.ReadLine();
                var prio = ParsePriority(prioStr);
                string name = $"Worker-{i + 1}";
                workers[i] = new WorkerThread(name, prio, startSignal);
            }

            var progressThread = new Thread(() =>  // realtime прогрес кожні 500 мс
            {
                while (true)
                {
                    Thread.Sleep(500);
                    bool anyAlive = false;
                    Console.WriteLine();
                    foreach (var w in workers)
                    {
                        if (w.Thread.IsAlive) anyAlive = true;
                        Console.WriteLine("  {0,-12} Priority={1,-12} State={2,-8} CurrentCount={3}",
                            w.Name, w.Priority, w.State, w.GetCurrentCount());
                    }
                    if (!anyAlive) break;
                }
            })
            {
                IsBackground = true,
                Name = "ProgressMonitor"
            };

            foreach (var w in workers)
                w.Start();

            progressThread.Start();
            startSignal.Set();  // усі воркери стартують одночасно

            foreach (var w in workers)
                w.Join();

            Thread.Sleep(600);  // щоб останній вивід прогресу встиг
            Console.WriteLine();
            Console.WriteLine("All threads finished.");
            Console.WriteLine();
            Console.WriteLine("| {0,-12} | {1,-12} | {2,12} | {3,14} |", "Name", "Priority", "Iterations", "ElapsedTime(ms)");
            Console.WriteLine("|--------------|--------------|--------------|----------------|");

            foreach (var w in workers)
            {
                Console.WriteLine("| {0,-12} | {1,-12} | {2,12} | {3,14} |",
                    w.Name, w.Priority, w.Iterations, w.ElapsedMs);
            }

            double sumScores = 0;
            foreach (var w in workers)
                sumScores += 1.0 / w.ElapsedMs;

            Console.WriteLine();
            Console.WriteLine("CPU time distribution (%):");
            foreach (var w in workers)
            {
                double score = 1.0 / w.ElapsedMs;
                double percent = (score / sumScores) * 100;
                Console.WriteLine("  {0}: {1:F2}%", w.Name, percent);
            }
        }
    }
}
