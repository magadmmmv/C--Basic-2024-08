/*
Программа 3.

Скопировать решение из программы 2, но переделать метод "получить планету" так, чтобы он на вход принимал еще один параметр, описывающий способ защиты от слишком частых вызовов - делегат PlanetValidator
(можно вместо него использовать Func), который на вход принимает название планеты, а на выходе дает строку с ошибкой. Метод "получить планету" теперь не должен проверять сколько вызовов делалось ранее.
Вместо этого он должен просто вызвать PlanetValidator и передать в него название планеты, поиск которой производится. И если PlanetValidator вернул ошибку - передать ее на выход из метода третьим полем.

Из main-метода при вызове "получить планету" в качестве нового параметра передавать лямбду, которая делает всё ту же проверку, которая была и ранее - на каждый третий вызов она возвращает строку
"Вы спрашиваете слишком часто" (в остальных случаях возвращает null). Результат исполнения программы должен получиться идентичный программе 2.

(*) Дописать main-метод так, чтобы еще раз проверять планеты "Земля", "Лимония" и "Марс", но передавать другую лямбду так, чтобы она для названия "Лимония" возвращала ошибку "Это запретная планета",
 а для остальных названий - null. Убедиться, что в этой серии проверок ошибка появляется только для Лимонии.
*/

using static DZ7_3.Program.PlanetCatalog;

namespace DZ7_3
{
    internal class Program
    {
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
            public int Count { get; set; }
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

            public delegate string PlanetValidator(string name);

            public (int? number, int? equator, string? exeption) GetPlanet(string name, PlanetValidator planetValidator)
            {
                Count++;

                foreach (var planet in _catalogPlanet)
                    if (planet.Name == name)
                        return (number: planet.Number, equator: planet.Equator, exeption: planetValidator(name));

                return (number: null, equator: null, exeption: planetValidator(name) != null ? planetValidator(name) : "Не удалось найти планету");
            }
        }
        static void Main(string[] args)
        {
            // Программа 3.
            Console.WriteLine("Программа 3");
            var planets = new PlanetCatalog();
            PlanetValidator lambda1 = 
                y => planets.Count % 3 == 0 ? "Вы спрашиваете слишком часто" : null!;

            Console.WriteLine("Земля: " + planets.GetPlanet("Земля", lambda1));
            Console.WriteLine("Лимония: " + planets.GetPlanet("Лимония", lambda1));
            Console.WriteLine("Марс: " + planets.GetPlanet("Марс", lambda1));

            Console.WriteLine();

            // (*)
            Console.WriteLine("Доп. задание (*)");
            PlanetValidator lambda2 = 
                y => y == "Лимония" ? "Это запретная планета" : null!;

            Console.WriteLine("Земля: " + planets.GetPlanet("Земля", lambda2));
            Console.WriteLine("Лимония: " + planets.GetPlanet("Лимония", lambda2));
            Console.WriteLine("Марс: " + planets.GetPlanet("Марс", lambda2));
        }
    }
}
