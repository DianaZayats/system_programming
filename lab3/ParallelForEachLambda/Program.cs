using System;
using System.Threading.Tasks;

namespace ParallelForEachLambda
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            // Невеликий масив зручний для наочного виводу результатів у консоль.
            double[] data = BuildData(25);

            // Тіло паралельного циклу задаємо лямбда-виразом через Action + Func.
            Action<double[], Func<double, double>> runParallel = (array, transform) =>
                Parallel.ForEach(array, (value, state, index) => array[index] = transform(value));

            // Окрема лямбда-функція з формулою перетворення елемента.
            Func<double, double> operation = x => Math.Exp(Math.PI * x) / Math.Pow(x, Math.PI);

            // Запускаємо паралельну обробку масиву з переданою формулою.
            runParallel(data, operation);

            Console.WriteLine("Parallel.ForEach with lambda loop body:");
            for (int i = 0; i < data.Length; i++)
            {
                Console.WriteLine("data[{0}] = {1:F6}", i, data[i]);
            }

            Console.ReadKey();
        }

        private static double[] BuildData(int size)
        {
            double[] array = new double[size];
            // Фіксоване зерно -> однакові вхідні дані при кожному запуску.
            Random random = new Random(101);
            for (int i = 0; i < array.Length; i++)
            {
                array[i] = 1.0 + random.NextDouble() * 4.0;
            }

            return array;
        }
    }
}
