using System;

namespace Lab1.Task1
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var numThread = new NumThread();
            var letterThread = new LetterThread();

            numThread.Start();
            letterThread.Start();

            numThread.Join();   // чекаємо обидва
            letterThread.Join();

            Console.WriteLine("Task 1 done");
        }
    }
}
