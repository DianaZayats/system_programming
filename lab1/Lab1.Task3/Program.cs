using System;
using System.Threading;

namespace Lab1.Task3
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var startSignal = new ManualResetEvent(false);  // спочатку не сигналізовано

            var workers = new[]
            {
                new WorkerThread("T-Lowest", ThreadPriority.Lowest, startSignal),
                new WorkerThread("T-Above", ThreadPriority.AboveNormal, startSignal),
                new WorkerThread("T-Below", ThreadPriority.BelowNormal, startSignal),
                new WorkerThread("T-Highest", ThreadPriority.Highest, startSignal)
            };

            foreach (var w in workers)
                w.Start();

            startSignal.Set();  // усі виходять з WaitOne і починають

            foreach (var w in workers)
                w.Join();

            Console.WriteLine();
            Console.WriteLine("| {0,-10} | {1,-12} | {2,12} | {3,14} |", "Name", "Priority", "Iterations", "ElapsedTime(ms)");
            Console.WriteLine("|------------|--------------|--------------|----------------|");

            foreach (var w in workers)
            {
                Console.WriteLine("| {0,-10} | {1,-12} | {2,12} | {3,14} |",
                    w.Name, w.Priority, w.Iterations, w.ElapsedMs);
            }

            double sumScores = 0;
            foreach (var w in workers)
                sumScores += 1.0 / w.ElapsedMs;  // score = 1/час

            Console.WriteLine();
            Console.WriteLine("CPU time distribution (%):");
            foreach (var w in workers)
            {
                double score = 1.0 / w.ElapsedMs;
                double percent = (score / sumScores) * 100;  // % від загального score
                Console.WriteLine("  {0}: {1:F2}%", w.Name, percent);
            }
        }
    }
}
