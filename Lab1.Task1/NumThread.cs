using System;
using System.Threading;

namespace Lab1.Task1
{
    public class NumThread
    {
        private readonly Thread _thread;

        public NumThread()
        {
            _thread = new Thread(Run)
            {
                Name = "NumThread"  // для виводу в консоль
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
            for (int i = 1; i <= 40; i++)
            {
                Console.WriteLine($"[{Thread.CurrentThread.Name}] {i}");
                Thread.Sleep(200);  // 200 мс між виводами
            }
        }
    }
}
