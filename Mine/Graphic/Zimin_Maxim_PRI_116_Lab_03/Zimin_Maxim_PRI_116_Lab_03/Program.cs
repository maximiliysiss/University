using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;

namespace Zimin_Maxim_PRI_116_Lab_03
{
    /// <summary>
    /// n = 20
    /// a = -5
    /// b = 5
    /// expression = (x^3)*exp(-abs(x+1)) - ln(5*tg(x)) - exp(7*sqrt(x)) + 0.3*(x^3 + x^2 -1)
    /// </summary>
    class Program
    {
        static double a = -5;
        static double b = 5;
        static int n = 20;

        static void Main(string[] args)
        {
            List<Task<double>> tasks = new List<Task<double>>();
            for (int i = 0; i < n; i++)
            {
                // Для захвата контекста, иначе будет захват для всех потоков i = 20
                int cpy = i;
                tasks.Add(Task.Run(() => PartProcess(cpy)));
            }
            tasks.Select((x, i) => new { x, i }).ToList().ForEach(async x =>
            {
                double xi = a + x.i * ((b - a) / n);
                Console.WriteLine($"i={x.i}|x={xi}|res={await x.x}");
            });
            Console.ReadLine();
        }


        static double PartProcess(int i)
        {
            double xi = a + i * ((b - a) / n);
            return Math.Pow(xi, 3) * Math.Exp(Math.Abs(xi + 1)) - Math.Log(5 * Math.Tan(xi * 180 / Math.PI)) - Math.Exp(7 * Math.Sqrt(xi)) + 0.3 * (Math.Pow(xi, 3) + xi * xi - 1);
        }
    }
}
