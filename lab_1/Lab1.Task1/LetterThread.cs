using System;
using System.Threading;

namespace Lab1.Task1
{
    public class LetterThread
    {
        private readonly Thread _thread;

        public LetterThread()
        {
            _thread = new Thread(Run)
            {
                Name = "LetterThread"
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
            for (char c = 'A'; c <= 'Z'; c++)
            {
                Console.WriteLine($"[{Thread.CurrentThread.Name}] {c}");
                Thread.Sleep(300);  // 300 мс
            }
        }
    }
}
