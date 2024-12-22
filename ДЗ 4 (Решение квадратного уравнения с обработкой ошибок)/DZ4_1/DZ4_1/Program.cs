/*
Домашнее задание
Решение квадратного уравнения с обработкой ошибок

Цель:
Цель домашнего задания - закрепить знания о механизме работы с исключениями, полученным в ходе вебинара. Студенты научатся писать код, обрабатывающий исключения познакомятся с различными примерами встроенных исключений.

Описание/Пошаговая инструкция выполнения домашнего задания:
Нужно написать программу, решающую квадратное уравнение формата
a * x^2 + b * x + c = 0
Пользователю нужно ввести целые значения a, b, c
и на основе введенных значений рассчитать корни/корень уравнения x
Шаги:

Вывести уравнение
a * x^2 + b * x + c = 0
Вывести текст
Введите значение a:
И считать значение a
Вывести текст
Введите значение b:
И считать значение b
Вывести текст
Введите значение c:
И считать значение c
Если какое-либо значение не является целым число
выбрасывается исключение, которое обрабатывается функцией FormatData (Приложение1)
с Severity = Error с выводом параметров, которые не прошли парсинг
пример работы программы
и возвращаемся к п.2
После этого нужно по формуле решения квадратных уравнений
рассчитать все возможные значения x
Если вещественных решений - два,
вывести ответ в виде
x1 = ответ_1, x2 = ответ_2
Если решение - одно
вывести ответ в виде
x = ответ
3 Если вещественных решений нет - выбросить Exception с текстом "Вещественных значений не найдено",
и обработать функцией FormatData c Severity = Warning (желтый фон)
Требования к коду
Исключения ввода данных и исключения ненахождения корней уравнения обрабатываются разными catch
Для исключения ненахождения ответов уравнения нужно использовать собственный класс
*/

//using System.Collections;
//using System.Collections.Generic;
//using static System.Runtime.InteropServices.JavaScript.JSType;


Equation();

static void Equation()
{
    Console.WriteLine("a * x^2 + b * x + c = 0");
    bool IsCorrectVars = false;

    while (!IsCorrectVars)
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

        IsCorrectVars = RootCalculation(keyValuePairs);
    }
}

static bool RootCalculation(IDictionary<string, string> data)
{
    IDictionary<string, int> dataInt = new Dictionary<string, int>();

    foreach (var Key in data.Keys)
    {
        try
        {
            dataInt[Key] = Convert.ToInt32(data[Key]);
        }
        catch (FormatException)
        {
            FormatData($"Неверный формат параметра {Key}", Severity.Error, data);
        }
        catch (OverflowException)
        {
            FormatData($"Параметр {Key} не входит в диапазон, введите значение от -2 147 483 648 до 2 147 483 647", Severity.Info, data);
        }
    }

    try
    {
        var d = Convert.ToInt32(Math.Pow(dataInt["b"], 2) - 4 * dataInt["a"] * dataInt["c"]);
        if (d < 0)
            throw new MyEquationException("Вещественных значений не найдено");
        else if (d == 0)
        {
            var x = -dataInt["b"] / 2 * dataInt["a"];
            Console.WriteLine($"x = {x}");
        }
        else
        {
            var x1 = (-dataInt["b"] + Math.Pow(d, 0.5)) / 2 * dataInt["a"];
            var x2 = (-dataInt["b"] - Math.Pow(d, 0.5)) / 2 * dataInt["a"];
            Console.WriteLine($"x1 = {x1}, x2 = {x2}");
        }
    }
    catch (MyEquationException e)
    {
        FormatData(e.Message, Severity.Warning, data);
    }
    catch (Exception)
    {
        //Console.WriteLine($"a = {data["a"]}");
        //Console.WriteLine($"b = {data["b"]}");
        //Console.WriteLine($"c = {data["c"]}");
        return false;
    }
    return true;
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