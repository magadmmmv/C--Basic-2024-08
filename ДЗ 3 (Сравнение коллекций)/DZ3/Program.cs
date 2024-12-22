//Домашнее задание
//Сравнение коллекций

//Цель:
//Сделать сравнение по скорости работы List, ArrayList и LinkedList.


//Описание/Пошаговая инструкция выполнения домашнего задания:
//Создать коллекции List, ArrayList и LinkedList.
//С помощью цикла for добавить в каждую 1 000 000 случайных значений с помощью класса Random.
//С помощью Stopwatch.Start() и Stopwatch.Stop() замерить длительность заполнения каждой коллекции и вывести значения на экран.
//Найти 496753-ий элемент, замерить длительность этого поиска и вывести на экран.
//Вывести на экран каждый элемент коллекции, который без остатка делится на 777. Вывести длительность этой операции для каждой коллекции.
//Укажите сколько времени вам понадобилось, чтобы выполнить это задание.


using System.Collections;
using System.Diagnostics;

var Count = 1000000;
var MaxRandom = 1000000;
var n = 496753;

//List
Console.WriteLine();
Console.WriteLine("List");
Console.WriteLine();

var l = new List<int>();
Random rand = new();
Stopwatch sw = new Stopwatch();

sw.Start();
for (int i = 0; i < Count; i++)
{
    l.Add(rand.Next(MaxRandom));
}
sw.Stop();
Console.WriteLine($"Время заполнения {Count} чисел в List: " + sw.Elapsed);

sw.Restart();
var k = l[n];
sw.Stop();
Console.WriteLine($"Время поиска {n} (значение {k}) элемента в List: " + sw.Elapsed);

Console.WriteLine();
sw.Restart();
for (int i = 0; i <= Count; i++)
{
    if (i % 777 == 0)
    {
        Console.Write(i + " ");
    }
}
sw.Stop();
Console.WriteLine();
Console.WriteLine();
Console.WriteLine($"Длительность вывода элементов кратных 777 в List: " + sw.Elapsed);


//ArrayList
Console.WriteLine();
Console.WriteLine("ArrayList");
Console.WriteLine();

var al = new ArrayList();

sw.Restart();
for (int i = 0; i < Count; i++)
{
    al.Add(rand.Next(MaxRandom));
}
sw.Stop();
Console.WriteLine($"Время заполнения {Count} чисел в ArrayList: " + sw.Elapsed);

sw.Restart();
k = 0;
int al_value = 0;
foreach (int value in al)
{
    k++;
    if (k == n)
    {
        al_value = value;
        break;
    }
}
sw.Stop();
Console.WriteLine($"Время поиска {n} (значение {al_value}) элемента в ArrayList: " + sw.Elapsed);

Console.WriteLine();
sw.Restart();
foreach (int value in al)
{
    if (value % 777 == 0)
    {
        Console.Write(value + " ");
    }
}
sw.Stop();
Console.WriteLine();
Console.WriteLine();
Console.WriteLine($"Длительность вывода элементов кратных 777 в ArrayList: " + sw.Elapsed);


//LinkedList
Console.WriteLine();
Console.WriteLine("LinkedList");
Console.WriteLine();
var ll = new LinkedList<int>();

sw.Restart();
for (int i = 0;i < Count; i++)
{
    ll.AddLast(rand.Next(MaxRandom));
}
sw.Stop();
Console.WriteLine($"Время заполнения {Count} чисел в LinkedList: " + sw.Elapsed);

sw.Restart();
k = 0;
int ll_value = 0;
foreach (int value in ll)
{
    k++;
    if (k == n)
    {
        ll_value = value;
        break;
    }
}
sw.Stop();
Console.WriteLine($"Время поиска {n} элемента (значение {ll_value}) в LinkedList: " + sw.Elapsed);
Console.WriteLine();

sw.Restart();
foreach (int value in al)
{
    if (value % 777 == 0)
    {
        Console.Write(value + " ");
    }
}
sw.Stop();
Console.WriteLine();
Console.WriteLine();
Console.WriteLine($"Длительность вывода элементов кратных 777 в LinkedList: " + sw.Elapsed);