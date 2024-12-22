//1. Есть функция(назовем ее WriteTable(int n, string s))
//2. Функция принимает на вход параметр n (целое число), и строку s. Если число меньше 1 или больше 6 - выводим текст введите число от 1 до 6 и выходим из функции. Если текст пустой - выводим сообщение текст не может быть пустым и выходим из функции.
//3. Функция на основе значений n и строки s должна вывести три полоски на основе ASCII-арта
//пример работы программы
//Требования к программе
//4. Общая ширина не должна превышать 40 символов
//5. Границы полосок - символ +
//6. Ширина полосок (каждой) зависит от числа n и длины введенной строки из п.2.
//7. Вывести 1ю строку с текстом s, введенным в п.2., который находится на расстоянии n-1 от каждой из границ строки.
//8. Вывести 2ю строку. Она имеет ту же высоту, что и строка 1, и представляет собой набор символов +, чередующихся в шахматном порядке.
//9. Вывести 3ю строку таблицы. Она должна быть квадратной, "перечеркнутая" символом + по диагоналям
//В программе должны использоваться циклы do while, while и for и ?: и if
//Примечания
//Для получения длины строки используйте свойство Length, например s.Length
//Для Вывода строки с переносом на следующую строку воспользуйтесь функцией Console.WriteLine
//Пример
//// Console.WriteLine("Privet");
//// Console.WriteLine("Otus");
//Privet
//Otus
//Для Вывода строки без переноса на следующую строку воспользуйтесь функцией Console.Write
//Пример
//// Console.Write("Privet");
//// Console.Write("Otus");
//PrivetOtus

static void WriteTable(int n, string s)
{
    if (n < 1 || n > 6)
    {
        Console.WriteLine("Необходимо ввести число от 1 до 6");
        return;
    }
    else if (s == "")
    {
        Console.WriteLine("Текст не может быть пустым");
        return;
    }

    Console.WriteLine();
    int Maximum = 40;

    s = 2 * n + s.Length + 2 > Maximum
        ? s[..(Maximum - 2 * n - 2)]
        : s;

    for (int i = 0; i < 3; i++)
    {
        switch (i)
        {
            case 0:
                FirstLine(n, s);
                break;
            case 1:
                SecondLine(n, s);
                break;
            case 2:
                ThirdLine(n, s);
                break;
        }
    }
}
//1 строка
static void FirstLine(int n, string s)
{
    var k = 0;
    while (k < 2 * n + 1)
    {
        if (k == 0 || k == 2 * n)
        {
            Console.Write(new string('+', 2 * n + s.Length + 2));
            Console.WriteLine();
        }
        else if (k == n)
        {
            Console.Write('+' + new string(' ', n) + s + new string(' ', n) + '+');
            Console.WriteLine();
        }
        else
        {
            Console.Write('+' + new string(' ', 2 * n + s.Length) + '+');
            Console.WriteLine();
        }
        k++;
    }
}

//2 строка
static void SecondLine(int n, string s)
{
    var k = 0;
    do
    {
        Console.Write('+');
        for (int j = 0; j < 2 * n + s.Length; j++)
        {
            if (((k % 2) == 0 && (j % 2) == 0) || ((k % 2) != 0 && (j % 2) != 0))
            {
                Console.Write(' ');
            }
            else
            {
                Console.Write("+");
            }
        }
        Console.WriteLine('+');
        k++;
    } while (k < 2 * n - 1);
}

//3 строка
static void ThirdLine(int n, string s)
{
    for (int i = 0; i < 2 * n + s.Length + 2; i++)
    {
        Console.Write('+');
        for (int j = 0; j < 2 * n + s.Length; j++)
        {
            if (i == 0 || i == 2 * n + s.Length + 1 || i == j + 1 || i == 2 * n + s.Length - j)
            {
                Console.Write('+');
            }
            else
            {
                Console.Write(" ");
            }
        }
        Console.WriteLine('+');
    }
}



Console.Write("Введите количество отступов n от 1 до 6: ");
int Tab = Convert.ToInt32(Console.ReadLine());

Console.Write("Введите слово: ");
string Word = Console.ReadLine()!;

WriteTable(Tab, Word);