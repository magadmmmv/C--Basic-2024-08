/*
Программа 1.
Создать четыре объекта анонимного типа для описания планет Солнечной системы со свойствами "Название", "Порядковый номер от Солнца", "Длина экватора", "Предыдущая планета" (ссылка на объект - предыдущую планету):

Венера
Земля
Марс
Венера (снова)
Данные по планетам взять из открытых источников.
Вывести в консоль информацию обо всех созданных "планетах". Рядом с информацией по каждой планете вывести эквивалентна ли она Венере.

Программа 2.
Написать обычный класс "Планета" со свойствами "Название", "Порядковый номер от Солнца", "Длина экватора", "Предыдущая планета" (ссылка на предыдущую Планету).
Написать класс "Каталог планет". В нем должен быть список планет - при создании экземпляра класса сразу заполнять его тремя планетами: Венера, Земля, Марс.

Добавить в класс "Каталог планет" метод "получить планету", который на вход принимает название планеты, а на выходе дает три поля: первые два поля порядковый
номер планеты от Солнца и длину ее экватора, когда планета найдена, а последнее поле - для ошибки. В случае, если планету по названию найти не удалось, то этот
метод должен возвращать строку "Не удалось найти планету" (именно строку, не Exception). На каждый третий вызов метод "получить планету" должен возвращать строку "Вы спрашиваете слишком часто".

В main-методе Вашей программы создать экземпляр "Каталога планет". У этого каталога вызвать метод "получить планету", передав туда последовательно названия Земля, Лимония, Марс.
Для найденных планет в консоль выводить их название, порядковый номер и длину экватора. А для ненайденных выводить строку ошибки, которую вернул метод "получить планету".
*/

namespace DZ7
{
    internal class Program
    {
        public static void AnonimousTypes()
        {
            var venera = new
            {
                Name = "Венера",
                Number = 2,
                Equator = 38_025,
                PreviousPlanet = "null"
            };
            var earth = new
            {
                Name = "Земля",
                Number = 3,
                Equator = 40_075,
                PreviousPlanet = venera
            };
            var mars = new
            {
                Name = "Марс",
                Number = 4,
                Equator = 21_344,
                PreviousPlanet = earth
            };
            var venera2 = new
            {
                Name = "Венера",
                Number = 2,
                Equator = 38_025,
                PreviousPlanet = "null"
            };
            Console.WriteLine("Программа 1");
            Console.WriteLine($"{venera}; Equals venera = {venera.Equals(venera)}");
            Console.WriteLine($"{earth}; Equals venera = {earth.Equals(venera)}");
            Console.WriteLine($"{mars}; Equals venera = {mars.Equals(venera)}");
            Console.WriteLine($"{venera2}; Equals venera = {venera2.Equals(venera)}");
            Console.WriteLine();
        }
        class Planet
        {
            public string? Name { get; set; }
            public int Number { get; set; }
            public int Equator { get; set; }
            public Planet? PreviousPlanet { get; set; }
        }
        public class PlanetCatalog
        {
            private Planet[] _catalogPlanet;
            private int Count { get; set; }
            public PlanetCatalog()
            {
                Count = 0;
                Planet venera = new Planet
                {
                    Name = "Венера",
                    Number = 2,
                    Equator = 38_025,
                    PreviousPlanet = null
                };
                Planet earth = new Planet
                {
                    Name = "Земля",
                    Number = 3,
                    Equator = 40_075,
                    PreviousPlanet = venera
                };
                var mars = new Planet
                {
                    Name = "Марс",
                    Number = 4,
                    Equator = 21_344,
                    PreviousPlanet = earth
                };
                _catalogPlanet = [venera, earth, mars];
            }
            public (int? number, int? equator, string? exeption) GetPlanet(string name)
            {
                Count++;

                foreach (var planet in _catalogPlanet)
                    if (planet.Name == name)
                        return (number: planet.Number, equator: planet.Equator, exeption: Count % 3 == 0 ? "Вы спрашиваете слишком часто" : null);

                return (number: null, equator: null, exeption: Count % 3 == 0 ? "Вы спрашиваете слишком часто" : "Не удалось найти планету");
            }
        }
        static void Main(string[] args)
        {
            // Программа 1.
            AnonimousTypes();

            // Программа 2.
            Console.WriteLine("Программа 2");
            var planets = new PlanetCatalog();
            Console.WriteLine("Земля: " + planets.GetPlanet("Земля"));
            Console.WriteLine("Лимония: " + planets.GetPlanet("Лимония"));
            Console.WriteLine("Марс: " + planets.GetPlanet("Марс"));
        }
    }
}
