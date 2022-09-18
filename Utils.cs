using System.IO;

namespace GoogleHashCode19
{
    public class Utils
    {
        public static string[] ReadFromFile(string fileName)
        {
            return (File.Exists(fileName) ? File.ReadAllText(fileName).Split('\n') : null);
        }

        public static void WriteToFile(string fileName, string textContent)
        {
            File.WriteAllText(fileName, textContent);
        }
    }
}
