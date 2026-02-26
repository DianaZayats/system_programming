using System;
using System.Diagnostics;
using System.Threading;

namespace Lab1.Task3
{
    public class WorkerThread
    {
        public string Name { get; }
        public ThreadPriority Priority { get; }
        public long Iterations { get; private set; }
        public long ElapsedMs { get; private set; }
        public Thread Thread { get; }

        private readonly ManualResetEvent _startSignal;  // один на всіх — синхронний старт
        private const long Limit = 100_000_000;

        public WorkerThread(string name, ThreadPriority priority, ManualResetEvent startSignal)
        {
            Name = name;
            Priority = priority;
            _startSignal = startSignal;
            Thread = new Thread(Run)
            {
                Name = name,
                Priority = priority
            };
        }

        public void Start()
        {
            Thread.Start();
        }

        public void Join()
        {
            Thread.Join();
        }

        private void Run()
        {
            _startSignal.WaitOne();  // чекаємо сигнал — стартуємо разом

            var sw = Stopwatch.StartNew();
            long count = 0;
            for (long i = 0; i < Limit; i++)
            {
                count++;
            }
            sw.Stop();

            Iterations = count;
            ElapsedMs = sw.ElapsedMilliseconds;
        }
    }
}
