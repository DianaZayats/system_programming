using System;
using System.Threading;

namespace Lab1.Task2
{
    public class CounterThread
    {
        private readonly Thread _thread;
        private readonly int _countTo;
        private readonly string _threadName;

        public CounterThread(string name, int countTo)
        {
            _threadName = name;
            _countTo = countTo;
            _thread = new Thread(Run)
            {
                Name = name,
                IsBackground = false  // foreground — процес чекає завершення
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
            for (int i = 0; i <= _countTo; i++)
            {
                Console.WriteLine($"[{Thread.CurrentThread.Name}] Count: {i}");
                Thread.Sleep(100);
            }
            Console.WriteLine($"[{Thread.CurrentThread.Name}] Finished. Counted to {_countTo}");
        }
    }
}
