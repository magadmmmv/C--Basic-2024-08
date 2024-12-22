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

Equation();

static void Equation()
{
    Console.WriteLine("a * x^2 + b * x + c = 0");
    bool IsCorrectVars = false;

    while (!IsCorrectVars)
    {
        Console.ResetColor();
        Console.WriteLine();
        Console.WriteLine("Введите значение a: ");
        string a = Console.ReadLine()!;
        Console.WriteLine("Введите значение b: ");
        string b = Console.ReadLine()!;
        Console.WriteLine("Введите значение c: ");
        string c = Console.ReadLine()!;
        Console.WriteLine();
        IsCorrectVars = FormatData(a, b, c);
    }
}

static bool FormatData(string a, string b, string c)
{
    bool IsOutputVar = false;
    try
    {
        Console.ForegroundColor = ConsoleColor.Black;
        try
        {
            int.Parse(a);
        }
        catch (FormatException)
        {
            Console.BackgroundColor = ConsoleColor.Red;
            Console.WriteLine(new string('-', 28));
            Console.WriteLine("Неверный формат параметра a");
            Console.WriteLine(new string('-', 28));
            IsOutputVar = true;
        }
        catch (OverflowException)
        {
            Console.BackgroundColor = ConsoleColor.DarkGreen;
            Console.WriteLine(new string('-', 85));
            Console.WriteLine($"Параметр a не входит в диапазон, введите значение от −2 147 483 648 до 2 147 483 647");
            Console.WriteLine(new string('-', 85));
            IsOutputVar = true;
        }
        try
        {
            int.Parse(b);
        }
        catch (FormatException)
        {
            Console.BackgroundColor = ConsoleColor.Red;
            Console.WriteLine(new string('-', 28));
            Console.WriteLine("Неверный формат параметра b");
            Console.WriteLine(new string('-', 28));
            IsOutputVar = true;
        }
        catch (OverflowException)
        {
            Console.BackgroundColor = ConsoleColor.DarkGreen;
            Console.WriteLine(new string('-', 85));
            Console.WriteLine($"Параметр b не входит в диапазон, введите значение от −2 147 483 648 до 2 147 483 647");
            Console.WriteLine(new string('-', 85));
            IsOutputVar = true;
        }
        try
        {
            int.Parse(c);
        }
        catch (FormatException)
        {
            Console.BackgroundColor = ConsoleColor.Red;
            Console.WriteLine(new string('-', 28));
            Console.WriteLine("Неверный формат параметра c");
            Console.WriteLine(new string('-', 28));
            IsOutputVar = true;
        }
        catch (OverflowException)
        {
            Console.BackgroundColor = ConsoleColor.DarkGreen;
            Console.WriteLine(new string('-', 85));
            Console.WriteLine($"Параметр c не входит в диапазон, введите значение от −2 147 483 648 до 2 147 483 647");
            Console.WriteLine(new string('-', 85));
            IsOutputVar = true;
        }
    }
    finally
    {
        Console.BackgroundColor = ConsoleColor.Red;
        if (IsOutputVar)
        {
            Console.WriteLine($"a = {a}");
            Console.WriteLine($"b = {b}");
            Console.WriteLine($"c = {c}");
        }
        Console.ResetColor();
    }

    try
    {
        var d = Math.Pow(Convert.ToInt32(b), 2) - 4 * Convert.ToInt32(a) * Convert.ToInt32(c);
        if (d < 0)
            throw new MyEquationException("Вещественных значений не найдено");
        else if (d == 0)
        {
            var x = -Convert.ToInt32(b) / 2 * Convert.ToInt32(a);
            Console.WriteLine($"x = {x}");
        }
        else
        {
            var x1 = (-Convert.ToInt32(b) + Math.Pow(Convert.ToInt32(d), 0.5)) / 2 * Convert.ToInt32(a);
            var x2 = (-Convert.ToInt32(b) - Math.Pow(Convert.ToInt32(d), 0.5)) / 2 * Convert.ToInt32(a);
            Console.WriteLine($"x1 = {x1}, x2 = {x2}");
        }
    }
    catch (MyEquationException e)
    {
        Console.BackgroundColor = ConsoleColor.DarkYellow;
        Console.ForegroundColor = ConsoleColor.Black;
        Console.WriteLine(e.Message);
        Console.ResetColor();
    }
    catch (Exception)
    {
        return false;
    }

    return true;
}

public class MyEquationException : Exception
{
    //public MyEquationException() : base() { }
    public MyEquationException(string message) : base(message)
    {
    }
}





//static double DivideByZero(int a, int b)
//{
//    try
//    {
//        if (b == 0)
//            throw new DivideByZeroException("Делим на ноль");
//        return a / b;
//    }
//    catch (Exception e)
//    {
//        Console.WriteLine("Произошла ошибка");
//        return 0.0;
//    }
//    finally
//    {
//        Console.WriteLine("Я блок finally ");
//    }
//}

//Console.WriteLine("Введите значение a: ");
////int a = Convert.ToInt32(Console.ReadLine());
//var a = Console.ReadLine();
//Console.WriteLine("Введите значение b: ");
//int b = Convert.ToInt32(Console.ReadLine());
////var b = Console.ReadLine();

//try
//{
//    if (b == 0)
//        throw new DivideByZeroException("Делим на ноль");
//    return Convert.ToInt32(a) / b;
//}
//catch (Exception e)
//{
//    Console.WriteLine("Произошла ошибка");
//    return 0;
//}
//finally
//{
//    Console.WriteLine("Я блок finally ");
//}

//DivideByZero(a, b);


//using System.Threading.Channels;

//Person tom = new Person();
//Person bob = new Person("Bob");
//Person saw = new Person("Saw", 25);

//tom.Print();
//bob.Print();
//saw.Print();

//Employee mike = new Employee("Mike", "Bork");
//Employee nick = new Employee("Nick", "Xiaomi");

//mike.PPrint();
//nick.PPrint();

//Employee magad = new Employee("magad", 34);
//magad.PPrint();

//Employee employee = new Employee("it one");
//employee.PPrint();


//class Person
//{
//    public string name;
//    public int age;
//    public Person() : this("Неизвестно")
//    { }
//    public Person(string name) : this(name, 18)
//    { }
//    public Person(string name, int age)
//    {
//        this.name = name;
//        this.age = age;
//    }
//    public void Print() => Console.WriteLine($"Имя: {name}  Возраст: {age}");
//}

//class Employee : Person
//{
//    public string Company { get; set; }
//    public Employee() : this("Company")
//    { }
//    public Employee(string company) : this("Name", 20, company)
//    { }
//    public Employee(string name, int age, string company) : base(name, age)
//    {
//        Company = company;
//    }

//    public void PPrint() => Console.WriteLine($"Имя: {name}  Возраст: {age}  Компания {Company}");
//}

