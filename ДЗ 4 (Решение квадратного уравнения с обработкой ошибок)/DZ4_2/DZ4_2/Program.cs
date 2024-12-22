namespace DZ4_2
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("a * x^2 + b * x + c = 0");
            bool IsCorrectVars = false;
            IDictionary<string, string> outputData = new Dictionary<string, string>();

            while (!IsCorrectVars)
            {
                var data = InputData();

                IsCorrectVars = true;

                foreach (var Key in data.Keys)
                {
                    try
                    {
                        int.Parse(data[Key]);
                    }
                    catch (FormatException)
                    {
                        FormatData($"Неверный формат параметра {Key}", Severity.Error, data);
                        IsCorrectVars = false;
                        break;
                    }
                    catch (OverflowException)
                    {
                        FormatData($"Параметр {Key} не входит в диапазон, введите значение от -2 147 483 648 до 2 147 483 647", Severity.Info, data);
                        IsCorrectVars = false;
                        break;
                    }
                    outputData[Key] = data[Key];
                }
            }

            try
            {
                RootCalculation(outputData);
            }
            catch (MyEquationException e)
            {
                FormatData(e.Message, Severity.Warning, outputData);
            }
            catch (Exception ee)
            {
                FormatData(ee.Message, Severity.Error, outputData);
            }
        }

        static IDictionary<string, string> InputData()
        {
            IDictionary<string, string> keyValuePairs = new Dictionary<string, string>();

            Console.WriteLine();
            Console.WriteLine("Введите значение a: ");
            keyValuePairs["a"] = Console.ReadLine()!;
            Console.WriteLine("Введите значение b: ");
            keyValuePairs["b"] = Console.ReadLine()!;
            Console.WriteLine("Введите значение c: ");
            keyValuePairs["c"] = Console.ReadLine()!;
            Console.WriteLine();

            return keyValuePairs;
        }

        static void RootCalculation(IDictionary<string, string> data)
        {
            int a = Convert.ToInt32(data["a"]);
            int b = Convert.ToInt32(data["b"]);
            int c = Convert.ToInt32(data["c"]);

            var d = Convert.ToInt32(Math.Pow(b, 2) - 4 * a * c);
            if (d < 0)
                throw new MyEquationException("Вещественных значений не найдено");
            else if (d == 0)
            {
                var x = -b / 2 * a;
                Console.WriteLine($"x = {x}");
            }
            else
            {
                var x1 = (-b + Math.Pow(d, 0.5)) / 2 * a;
                var x2 = (-b - Math.Pow(d, 0.5)) / 2 * a;
                Console.WriteLine($"x1 = {x1}, x2 = {x2}");
            }

        }

        static void FormatData(string message, Severity severity, IDictionary<string, string> data)
        {
            switch (severity)
            {
                case Severity.Warning:
                    Console.ForegroundColor = ConsoleColor.Black;
                    Console.BackgroundColor = ConsoleColor.DarkYellow;
                    Console.WriteLine(new string('-', 50));
                    Console.WriteLine(message);
                    Console.WriteLine(new string('-', 50));
                    foreach (var item in data)
                    {
                        Console.WriteLine($"{item.Key} = {item.Value}");
                    }
                    Console.ResetColor();
                    break;
                case Severity.Error:
                    Console.ForegroundColor = ConsoleColor.White;
                    Console.BackgroundColor = ConsoleColor.Red;
                    Console.WriteLine(new string('-', 50));
                    Console.WriteLine(message);
                    Console.WriteLine(new string('-', 50));
                    foreach (var item in data)
                    {
                        Console.WriteLine($"{item.Key} = {item.Value}");
                    }
                    Console.ResetColor();
                    break;
                case Severity.Info:
                    Console.ForegroundColor = ConsoleColor.Black;
                    Console.BackgroundColor = ConsoleColor.DarkGreen;
                    Console.WriteLine(new string('-', 50));
                    Console.WriteLine(message);
                    Console.WriteLine(new string('-', 50));
                    foreach (var item in data)
                    {
                        Console.WriteLine($"{item.Key} = {item.Value}");
                    }
                    Console.ResetColor();
                    break;
            }
        }

        enum Severity
        {
            Warning,
            Error,
            Info
        }

        public class MyEquationException : Exception
        {
            public MyEquationException() : base() { }
            public MyEquationException(string message) : base(message)
            {
            }
        }
    }
}
