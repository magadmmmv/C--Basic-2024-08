//3. В каждый файл записать его имя в кодировке UTF8. Учесть, что файл может быть удален, либо отсутствовать права на запись.

using System.Text;

namespace DZ11_3
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

                    if (CanWriteToFile(filePath))
                    {
                        File.WriteAllText(filePath, file.Name, Encoding.UTF8);
                        Console.WriteLine("The text '{0}' has been successfully written to the file {1}.", file.Name, filePath);
                    }
                    else
                        Console.WriteLine("You do NOT have write access to the file {0}", filePath);
                }

                foreach (var file in dir2.GetFiles())
                {
                    string filePath = Path.Combine(dir2.FullName, file.Name);

                    if (CanWriteToFile(filePath))
                    {
                        File.WriteAllText(filePath, file.Name, Encoding.UTF8);
                        Console.WriteLine("The text '{0}' has been successfully written to the file {1}.", file.Name, filePath);
                    }
                    else
                        Console.WriteLine("You do NOT have write access to the file {0}", filePath);
                }
            }
            catch (DirectoryNotFoundException)
            {
                Console.WriteLine("Please run first program DZ11_1 to add directories {0}, {1}", dir1.FullName, dir2.FullName);
            }
        }

        public static bool CanWriteToFile(string filePath)
        {
            try
            {
                using (FileStream fs = new FileStream(filePath, FileMode.Open, FileAccess.Write, FileShare.None))
                {
                    return true;
                }
            }
            catch (UnauthorizedAccessException)
            {
                return false;
            }
            catch (IOException)
            {
                return false;
            }
        }
    }
}
