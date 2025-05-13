//1. Создать директории c:\Otus\TestDir1 и c:\Otus\TestDir2 с помощью класса DirectoryInfo.

namespace DZ11_1
{
    internal class Program
    {
        static void Main(string[] args)
        {
            DirectoryInfo dir1 = new DirectoryInfo("c:\\Otus\\TestDir1");
            DirectoryInfo dir2 = new DirectoryInfo("c:\\Otus\\TestDir2");

            try
            {
                if (dir1.Exists)
                    Console.WriteLine($"Path {dir1} is already exists");
                else
                {
                    dir1.Create();
                    Console.WriteLine($"Path {dir1} successfully created");
                }

                if (dir2.Exists)
                    Console.WriteLine($"Path {dir2} is already exists");
                else
                {
                    dir2.Create();
                    Console.WriteLine($"Path {dir2} successfully created");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("The process failed: {0}", ex.ToString());
            }
        }
    }
}
