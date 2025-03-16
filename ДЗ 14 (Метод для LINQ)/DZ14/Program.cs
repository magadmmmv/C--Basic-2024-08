/*
1)
Напишите свой метод расширения с названием "Top" для коллекции IEnumerable, принимающий значение Х от 1 до 100 и 
возвращающий заданное количество процентов от выборки с округлением количества элементов в большую сторону.
То есть для списка var list = new List{1,2,3,4,5,6,7,8,9};
list.Top(30) должно вернуть 30% элементов от выборки по убыванию значений, то есть [9,8,7] (33%), а не [9,8] (22%).
Если переданное значение больше 100 или меньше 1, то выбрасывать ArgumentException.

Создайте дженерик метод расширения для IEnumerable, возвращающий коллекцию, на которой был вызван;
Ограничьте количество элементов выходной коллекции;

2)
Напишите перегрузку для метода "Top", которая принимает ещё и поле, по которому будут отбираться топ Х элементов. 
Например, для var list = new List{...}, вызов list.Top(30, person => person.Age) должен вернуть 30% пользователей 
с наибольшим возрастом в порядке убывания оного.

Создайте дженерик перегрузку метода Top, добавив для этого одним из параметров функцию, принимающую T и возвращающую int;
*/

namespace DZ14
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var list = new List<int> { 1, 2, 3, 4, 5, 6, 7, 8, 9 };

            var result_list = list.Top(30);

            List<Person> persons = new List<Person>
            {
                new Person{ Age = 50, Name = "Tom"},
                new Person{ Age = 10, Name = "Alice"},
                new Person{ Age = 40, Name = "Bob"},
                new Person{ Age = 30, Name = "John"},
                new Person{ Age = 45, Name = "Jerry"},
                new Person{ Age = 70, Name = "Mark"},
                new Person{ Age = 75, Name = "Kate"},
                new Person{ Age = 35, Name = "John"},
                new Person{ Age = 55, Name = "Will"},
            };

            var result_persons = persons.Top(30, person => person.Age);
            
            Console.WriteLine();
        }
    }

    public static class IEnumerableExtensions
    {
        public static IEnumerable<T> Top<T>(this IEnumerable<T> collection, int X)
        {
            if (X < 1 || X > 100)
                throw new ArgumentException();

            int countInChunk = (int)Math.Ceiling((decimal)collection.Count() * X / 100);
            IEnumerable<T> result = collection.Reverse().Chunk(countInChunk).First();

            return result;
        }

        public static IEnumerable<T> Top<T>(this IEnumerable<T> collection, int X, Func<T,int> func)
        {
            if (X < 1 || X > 100)
                throw new ArgumentException();

            int countInChunk = (int)Math.Ceiling((decimal)collection.Count() * X / 100);
            IEnumerable<T> result = collection.OrderByDescending(func).Chunk(countInChunk).First();

            return result;
        }
    }
}
