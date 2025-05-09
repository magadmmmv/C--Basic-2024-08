//4. Каждый файл дополнить текущей датой (значение DateTime.Now) любыми способами: синхронно и\или асинхронно.

using System.Text;

namespace DZ11_4
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

                    if (CanWriteToFile(filePath))
                    {
                        if (content.Length > 0)
                        {
                            File.AppendAllTextAsync(filePath, " " + DateTime.Now.ToString(), Encoding.UTF8);
                            Console.WriteLine("The text '{0}' has been successfully appended to the file {1}.", DateTime.Now.ToString(), filePath);
                        }
                        else
                            Console.WriteLine("The file {0} is empty. Please run third program DZ11_3 to add text '{1}'", filePath, file.Name);
                    }
                    else
                        Console.WriteLine("You do NOT have write access to the file {0}", filePath);
                }

                foreach (var file in dir2.GetFiles())
                {
                    string filePath = Path.Combine(dir2.FullName, file.Name);
                    var content = File.ReadAllText(filePath);

                    if (CanWriteToFile(filePath))
                    {
                        if (content.Length > 0)
                        {
                            File.AppendAllTextAsync(filePath, " " + DateTime.Now.ToString(), Encoding.UTF8);
                            Console.WriteLine("The text '{0}' has been successfully appended to the file {1}.", DateTime.Now.ToString(), filePath);
                        }
                        else
                            Console.WriteLine("The file {0} is empty. Please run third program DZ11_3 to add text '{1}'", filePath, file.Name);
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
