using System;
using System.Diagnostics;
using System.Threading;

namespace Lab1.Task4
{
    public class WorkerThread
    {
        public string Name { get; }
        public ThreadPriority Priority { get; }
        public long Iterations => _currentCount;
        public long ElapsedMs { get; private set; }
        public Thread Thread { get; }
        public bool IsRunning => Thread.IsAlive;
        public string State => Thread.IsAlive ? "Running" : "Stopped";

        private long _currentCount;
        private readonly ManualResetEvent _startSignal;
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

        public long GetCurrentCount()  // ProgressMonitor читає з іншого потоку
        {
            return Interlocked.Read(ref _currentCount);
        }

        private void Run()
        {
            _startSignal.WaitOne();  // старт разом з іншими

            var sw = Stopwatch.StartNew();
            for (long i = 0; i < Limit; i++)
            {
                Interlocked.Exchange(ref _currentCount, i + 1);  // атомарно для читання з ProgressMonitor
            }
            sw.Stop();
            ElapsedMs = sw.ElapsedMilliseconds;
        }
    }
}
