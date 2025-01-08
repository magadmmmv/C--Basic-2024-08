/*
0  1  2  3  4  5  6   7   8   9  10  11   12   13   14   15   16    17    18    19    20     21     22
0, 1, 1, 2, 3, 5, 8, 13, 21, 34, 55, 89, 144, 233, 377, 610, 987, 1597, 2584, 4181, 6765, 10946, 17711
 
1. Реализовать метод нахождения n-го члена последовательности Фибоначчи по формуле F(n) = F(n-1) + F(n-2) с помощью рекурсивных вызовов.
2. Реализовать метод нахождения n-го члена последовательности Фибоначчи по формуле F(n) = F(n-1) + F(n-2) с помощью цикла.
3. Добавить подсчёт времени на выполнение рекурсивного и итеративного методов с помощью Stopwatch и написать сколько времени для значений 5, 10 и 20.
*/

using System.Diagnostics;

namespace DZ8
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            Stopwatch sw = new Stopwatch();

            // 5
            int Value1 = 5;
            Console.WriteLine($"Подсчёт времени {Value1} члена последовательности");
            sw.Start();
            var resultRecursion1 = fibonacciRecursion(Value1);
            sw.Stop();
            Console.WriteLine($"Время поиска {Value1} члена последовательности со значением {resultRecursion1} с помощью рекурсии: " + sw.Elapsed);

            sw.Restart();
            var resultCycle1 = fibonacciCycle(Value1);
            sw.Stop();
            Console.WriteLine($"Время поиска {Value1} члена последовательности со значением {resultCycle1} с помощью цикла: " + sw.Elapsed);
            Console.WriteLine();

            // 10
            int Value2 = 10;
            Console.WriteLine($"Подсчёт времени {Value2} члена последовательности");
            sw.Start();
            var resultRecursion2 = fibonacciRecursion(Value2);
            sw.Stop();
            Console.WriteLine($"Время поиска {Value2} члена последовательности со значением {resultRecursion2} с помощью рекурсии: " + sw.Elapsed);

            sw.Restart();
            var resultCycle2 = fibonacciCycle(Value2);
            sw.Stop();
            Console.WriteLine($"Время поиска {Value2} члена последовательности со значением {resultCycle2} с помощью цикла: " + sw.Elapsed);
            Console.WriteLine();

            // 20
            int Value3 = 20;
            Console.WriteLine($"Подсчёт времени {Value3} члена последовательности");
            sw.Start();
            var resultRecursion3 = fibonacciRecursion(Value3);
            sw.Stop();
            Console.WriteLine($"Время поиска {Value3} члена последовательности со значением {resultRecursion3} с помощью рекурсии: " + sw.Elapsed);

            sw.Restart();
            var resultCycle3 = fibonacciCycle(Value3);
            sw.Stop();
            Console.WriteLine($"Время поиска {Value3} члена последовательности со значением {resultCycle3} с помощью цикла: " + sw.Elapsed);
        }

        public static int fibonacciRecursion(int n)
        {
            if ( n <= 0 )
                return 0;
            if (n == 1 || n == 2)
                return 1;

            return fibonacciRecursion(n - 1) + fibonacciRecursion(n - 2);
        }

        public static int fibonacciCycle(int n)
        {
            if (n <= 0)
                return 0;
            if (n == 1 || n == 2) 
                return 1;

            int result = 0;
            int x = 1;
            int y = 1;
            int i = 2;
            while (i < n)
            {
                i++;
                result = x + y;
                x = y;
                y = result;
            }
            return result;
        }
    }
}