//5. Прочитать все файлы и вывести на консоль: имя_файла: текст + дополнение.

namespace DZ11_5
{
    internal class Program
    {
        static void Main(string[] args)
        {
            DirectoryInfo dir1 = new DirectoryInfo("c:\\Otus\\TestDir1");
            DirectoryInfo dir2 = new DirectoryInfo("c:\\Otus\\TestDir2");

            try
            {
                foreach (var file in dir1.GetFiles())
                {
                    string filePath = Path.Combine(dir1.FullName, file.Name);
                    var content = File.ReadAllText(filePath);

                    if (content.Length > 0)
                    {
                        Console.WriteLine("{0}: {1}'", file.Name, content);
                    }
                    else
                        Console.WriteLine("The file {0} is empty. Please run third program DZ11_3 to add text '{1}'", filePath, file.Name);
                }

                foreach (var file in dir2.GetFiles())
                {
                    string filePath = Path.Combine(dir2.FullName, file.Name);
                    var content = File.ReadAllText(filePath);

                    if (content.Length > 0)
                    {
                        Console.WriteLine("{0}: {1}'", file.Name, content);
                    }
                    else
                        Console.WriteLine("The file {0} is empty. Please run third program DZ11_3 to add text '{1}'", filePath, file.Name);
                }
            }
            catch (DirectoryNotFoundException)
            {
                Console.WriteLine("Please run first program DZ11_1 to add directories {0}, {1}", dir1.FullName, dir2.FullName);
            }
        }
    }
}
