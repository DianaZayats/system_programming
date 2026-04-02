using System;
using System.Threading;
using System.Threading.Tasks;

namespace ParallelForBreak
{
    internal class Program
    {
        private const double TargetValue = 123.45;
        private const double Tolerance = 0.02;

        private static void Main(string[] args)
        {
            // Формуємо масив для демонстрації пошуку з достроковою зупинкою циклу.
            double[] data = BuildData(1000000);

            // foundIndex = -1 означає, що підходящий елемент ще не знайдено.
            int foundIndex = -1;
            double foundValue = 0.0;

            ParallelLoopResult result = Parallel.For(0, data.Length, (i, state) =>
            {
                double x = data[i];
                // Імітуємо обчислювальне навантаження для кожного елемента.
                data[i] = Math.Exp(x) / Math.Pow(x, Math.PI);

                // Якщо значення потрапляє
                if (Math.Abs(x - TargetValue) <= Tolerance)
                {
                    if (Interlocked.CompareExchange(ref foundIndex, i, -1) == -1)
                    {
                        foundValue = x;
                    }

                    state.Stop();
                }
            });

            Console.WriteLine("Searching for an element within a target neighborhood (Parallel.For + Stop):");
            Console.WriteLine("Target = {0}, Tolerance = {1}", TargetValue, Tolerance);
            // Якщо цикл зупинили достроково, IsCompleted буде false.
            Console.WriteLine("IsCompleted = {0}", result.IsCompleted);
            Console.WriteLine(
                foundIndex >= 0
                    ? string.Format("Found: index={0}, value={1:F5}", foundIndex, foundValue)
                    : "Element not found.");
            Console.ReadKey();
        }

        private static double[] BuildData(int size)
        {
            double[] array = new double[size];
            Random random = new Random(123);
            for (int i = 0; i < array.Length; i++)
            {
                array[i] = 1.0 + random.NextDouble() * 200.0;
            }

            // Гарантуємо, що хоча б один елемент потрапить в заданий окіл.
            array[size / 2] = TargetValue + Tolerance / 2.0;
            return array;
        }
    }
}
