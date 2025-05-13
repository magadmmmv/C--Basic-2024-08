//2. В каждой директории создать несколько файлов File1...File10 с помощью класса File.

namespace DZ11_2
{
    internal class Program
    {
        static void Main(string[] args)
        {
            DirectoryInfo dir1 = new DirectoryInfo("c:\\Otus\\TestDir1");
            DirectoryInfo dir2 = new DirectoryInfo("c:\\Otus\\TestDir2");

            try
            {
                for (int i = 1; i <= 10; i++)
                {
                    string file1 = dir1 + Path.DirectorySeparatorChar.ToString() + "File" + i;
                    if (!File.Exists(file1))
                    {
                        File.Create(file1).Dispose();
                        Console.WriteLine("File {0} successfully created", file1);
                    }
                    else
                        Console.WriteLine("File {0} already exists", file1);

                    string file2 = dir2 + Path.DirectorySeparatorChar.ToString() + "File" + i;
                    if (!File.Exists(file2))
                    {
                        File.Create(file2).Dispose();
                        Console.WriteLine("File {0} successfully created", file2);
                    }
                    else
                        Console.WriteLine("File {0} already exists", file2);
                }
            }
            catch (DirectoryNotFoundException)
            {
                Console.WriteLine("Please run first program DZ11_1 to add directories {0}, {1}", dir1.FullName, dir2.FullName);
            }
        }
    }
}
