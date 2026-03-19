using System;
using System.Diagnostics;
using System.Threading.Tasks;

namespace ParallelForEachExamples
{
    internal class Program
    {
        // Дані для прикладу з достроковим виходом через Break().
        private static double[] breakData;

        private static void Main(string[] args)
        {
            // 1) Порівняння часу послідовної та паралельної обробки через ForEach.
            RunTimingExample();
            Console.WriteLine();
            // 2) Демонстрація дострокового завершення ForEach при знаходженні умови.
            RunBreakExample();
            Console.ReadKey();
        }

        private static void RunTimingExample()
        {
            double[] data = BuildData(800000);
            // Клонуємо масив для коректного порівняння двох підходів.
            double[] seq = (double[])data.Clone();
            double[] par = (double[])data.Clone();

            Stopwatch swSeq = Stopwatch.StartNew();
            for (int i = 0; i < seq.Length; i++)
            {
                seq[i] = Math.Exp(Math.PI * seq[i]) / Math.Pow(seq[i], Math.PI);
            }

            swSeq.Stop();

            Stopwatch swPar = Stopwatch.StartNew();
            // ForEach із доступом до index, щоб змінювати елементи вихідного масиву.
            Parallel.ForEach(par, (value, state, index) =>
            {
                par[index] = Math.Exp(Math.PI * value) / Math.Pow(value, Math.PI);
            });
            swPar.Stop();

            Console.WriteLine("ForEach example (complex calculation):");
            Console.WriteLine("Sequential: {0} ms", swSeq.ElapsedMilliseconds);
            Console.WriteLine("Parallel: {0} ms", swPar.ElapsedMilliseconds);
        }

        private static void RunBreakExample()
        {
            breakData = new double[200000];
            // Ініціалізуємо масив невід'ємними значеннями.
            for (int i = 0; i < breakData.Length; i++)
            {
                breakData[i] = i;
            }

            // Додаємо від'ємний елемент, який має викликати Break().
            breakData[100000] = -10;

            ParallelLoopResult loopResult = Parallel.ForEach(breakData, MyTransformWithBreak);

            Console.WriteLine("ForEach break example:");
            if (!loopResult.IsCompleted)
            {
                // LowestBreakIteration показує найменший індекс, на якому спрацював Break().
                Console.WriteLine(
                    "Parallel.ForEach was aborted with negative value on iteration {0}",
                    loopResult.LowestBreakIteration);
            }
            else
            {
                Console.WriteLine("Parallel.ForEach completed all iterations.");
            }
        }

        private static void MyTransformWithBreak(double value, ParallelLoopState state, long index)
        {
            // Як тільки знайдено від'ємне значення — перериваємо подальші ітерації.
            if (value < 0)
            {
                state.Break();
            }
        }

        private static double[] BuildData(int size)
        {
            double[] array = new double[size];
            Random random = new Random(777);
            for (int i = 0; i < array.Length; i++)
            {
                array[i] = 1.0 + random.NextDouble() * 9.0;
            }

            return array;
        }
    }
}
