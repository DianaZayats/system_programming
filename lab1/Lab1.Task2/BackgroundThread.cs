using System;
using System.Threading;

namespace Lab1.Task2
{
    public class BackgroundThread
    {
        private readonly Thread _thread;
        private readonly CancellationToken _cancellationToken;
        private static int _iterationCount = 0;

        public BackgroundThread(CancellationToken cancellationToken)
        {
            _cancellationToken = cancellationToken;
            _thread = new Thread(Run)
            {
                Name = "BackgroundThread",
                IsBackground = true
            };
        }

        public void Start()
        {
            _thread.Start();
        }

        public void Join()
        {
            _thread.Join();
        }

        private void Run()
        {
            while (!_cancellationToken.IsCancellationRequested)  // виконується, поки foreground не закінчились
            {
                _iterationCount++;
                Console.WriteLine($"[{Thread.CurrentThread.Name}] Background message (iteration {_iterationCount})");
                Thread.Sleep(500);
            }
        }
    }
}
