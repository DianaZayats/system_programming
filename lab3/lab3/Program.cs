using System;
using System.Diagnostics;
using System.Threading.Tasks;

namespace lab3
{
    internal class Program
    {
        private const double Pi = Math.PI;

        static void Main(string[] args)
        {
            // Набір розмірів масивів для порівняння впливу обсягу даних на швидкодію.
            int[] sizes = { 100000, 500000, 1000000 };

            Console.WriteLine("Experiments: Parallel.For() vs sequential loop");
            Console.WriteLine(new string('-', 106));
            Console.WriteLine(
                "{0,-8} | {1,9} | {2,-24} | {3,10} | {4,12}",
                "Type",
                "Count",
                "Operation",
                "Seq., ms",
                "Par., ms");
            Console.WriteLine(new string('-', 106));

            foreach (int size in sizes)
            {
                // Для кожного розміру окремо тестуємо int і double.
                RunIntExperiments(size);
                RunDoubleExperiments(size);
            }

            Console.WriteLine(new string('-', 106));
            Console.ReadKey();
        }

        private static void RunIntExperiments(int size)
        {
            // Створюємо початкові дані для int-експериментів.
            int[] source = BuildIntArray(size);

            // Від простих до складніших обчислень, щоб оцінити вплив "ваги" формули.
            MeasureAndPrint("int", size, "x = x / 10", source, x => x / 10);
            MeasureAndPrint("int", size, "x = x / pi", source, x => (int)(x / Pi));
            MeasureAndPrint("int", size, "x = e^x / x^pi", source, x => SafeInt(Math.Exp(x) / Math.Pow(x, Pi)));
            MeasureAndPrint("int", size, "x = e^(pi*x)/x^pi", source, x => SafeInt(Math.Exp(Pi * x) / Math.Pow(x, Pi)));
        }

        private static void RunDoubleExperiments(int size)
        {
            // Створюємо початкові дані для double-експериментів.
            double[] source = BuildDoubleArray(size);

            // Для double бачимо більш "природну" поведінку дробових обчислень.
            MeasureAndPrint("double", size, "x = x / 10", source, x => x / 10.0);
            MeasureAndPrint("double", size, "x = x / pi", source, x => x / Pi);
            MeasureAndPrint("double", size, "x = e^x / x^pi", source, x => Math.Exp(x) / Math.Pow(x, Pi));
            MeasureAndPrint("double", size, "x = e^(pi*x)/x^pi", source, x => Math.Exp(Pi * x) / Math.Pow(x, Pi));
        }

        private static int[] BuildIntArray(int size)
        {
            int[] array = new int[size];
            // Фіксоване зерно генератора робить експеримент відтворюваним.
            Random random = new Random(42 + size);
            for (int i = 0; i < array.Length; i++)
            {
                array[i] = random.Next(1, 10);
            }

            return array;
        }

        private static double[] BuildDoubleArray(int size)
        {
            double[] array = new double[size];
            // Фіксоване зерно генератора робить експеримент відтворюваним.
            Random random = new Random(84 + size);
            for (int i = 0; i < array.Length; i++)
            {
                array[i] = 1.0 + random.NextDouble() * 9.0;
            }

            return array;
        }

        private static void MeasureAndPrint(
            string typeName,
            int size,
            string operationName,
            int[] source,
            Func<int, int> operation)
        {
            // Клонуємо масив, щоб послідовний і паралельний тести працювали з однаковими даними.
            int[] sequentialData = (int[])source.Clone();
            int[] parallelData = (int[])source.Clone();

            long sequentialTime = MeasureSequential(sequentialData, operation);
            long parallelTime = MeasureParallel(parallelData, operation);

            Console.WriteLine(
                "{0,-8} | {1,9} | {2,-24} | {3,10} | {4,12}",
                typeName,
                size,
                operationName,
                sequentialTime,
                parallelTime);
        }

        private static void MeasureAndPrint(
            string typeName,
            int size,
            string operationName,
            double[] source,
            Func<double, double> operation)
        {
            // Клонуємо масив, щоб послідовний і паралельний тести працювали з однаковими даними.
            double[] sequentialData = (double[])source.Clone();
            double[] parallelData = (double[])source.Clone();

            long sequentialTime = MeasureSequential(sequentialData, operation);
            long parallelTime = MeasureParallel(parallelData, operation);

            Console.WriteLine(
                "{0,-8} | {1,9} | {2,-24} | {3,10} | {4,12}",
                typeName,
                size,
                operationName,
                sequentialTime,
                parallelTime);
        }

        private static long MeasureSequential(int[] data, Func<int, int> operation)
        {
            Stopwatch stopwatch = Stopwatch.StartNew();
            // Базовий (еталонний) варіант без паралелізму.
            for (int i = 0; i < data.Length; i++)
            {
                data[i] = operation(data[i]);
            }

            stopwatch.Stop();
            return stopwatch.ElapsedMilliseconds;
        }

        private static long MeasureParallel(int[] data, Func<int, int> operation)
        {
            Stopwatch stopwatch = Stopwatch.StartNew();
            // Розпаралелюємо обробку: кожен індекс i обчислюється незалежно.
            Parallel.For(0, data.Length, i => data[i] = operation(data[i]));
            stopwatch.Stop();
            return stopwatch.ElapsedMilliseconds;
        }

        private static long MeasureSequential(double[] data, Func<double, double> operation)
        {
            Stopwatch stopwatch = Stopwatch.StartNew();
            // Базовий (еталонний) варіант без паралелізму.
            for (int i = 0; i < data.Length; i++)
            {
                data[i] = operation(data[i]);
            }

            stopwatch.Stop();
            return stopwatch.ElapsedMilliseconds;
        }

        private static long MeasureParallel(double[] data, Func<double, double> operation)
        {
            Stopwatch stopwatch = Stopwatch.StartNew();
            // Розпаралелюємо обробку: кожен індекс i обчислюється незалежно.
            Parallel.For(0, data.Length, i => data[i] = operation(data[i]));
            stopwatch.Stop();
            return stopwatch.ElapsedMilliseconds;
        }

        private static int SafeInt(double value)
        {
            // Захист від некоректних або надто великих значень під час перетворення в int.
            if (double.IsNaN(value))
            {
                return 0;
            }

            if (value > int.MaxValue)
            {
                return int.MaxValue;
            }

            if (value < int.MinValue)
            {
                return int.MinValue;
            }

            return (int)value;
        }
    }
}
