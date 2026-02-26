using System;
using System.Threading;

namespace Lab1.Task2
{
    public class BackgroundThread
    {
        private readonly Thread _thread;
        private static int _iterationCount = 0;

        public BackgroundThread()
        {
            _thread = new Thread(Run)
            {
                Name = "BackgroundThread",
                IsBackground = true  // зупиниться разом із процесом
            };
        }

        public void Start()
        {
            _thread.Start();
        }

        private void Run()
        {
            while (true)  // нескінченний цикл
            {
                _iterationCount++;
                Console.WriteLine($"[{Thread.CurrentThread.Name}] Background message (iteration {_iterationCount})");
                Thread.Sleep(500);
            }
        }
    }
}
