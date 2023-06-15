using System.IO;
using System.Linq;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;

namespace FinalTask
{
    [Serializable]
    class Student
    {
        public string Name { get; set; }
        public string Group { get; set; }
        public DateTime DateOfBirth { get; set; }

    }
    class Program
    {
        const string SettingsFileName = "C:/Users/SEO/Desktop/Students.dat";
        const string OutputDirectory = "C:/Users/SEO/Desktop/Students";
        static void Main(string[] args)
        {
            ReadValues();
            Console.ReadLine();
        }
        static void ReadValues()
        {
            string StringValue;

            if (File.Exists(SettingsFileName))
            {
                BinaryFormatter binaryFormatter = new BinaryFormatter();
                using (var fs = new FileStream(SettingsFileName, FileMode.OpenOrCreate))
                {
                    var Students = (Student[])binaryFormatter.Deserialize(fs);
                    foreach (var student in Students)
                    {
                        try
                        {
                            string GroupDir = Path.Combine(OutputDirectory, student.Group + ".txt");
                            using (StreamWriter studentGroups = new StreamWriter(GroupDir, true))
                            {
                                studentGroups.WriteLine($"Имя: {student.Name} --- Дата рождения: {student.DateOfBirth}");
                            }
                        }
                        catch (Exception e)
                        {
                            Console.WriteLine($"Ошибка: {e}");
                        }
                        
                    }                    
                }
            } else
            {
                Console.WriteLine("Ошибка");
            }
            if (!Directory.Exists(OutputDirectory))
            {
                try
                {
                    Directory.CreateDirectory(OutputDirectory);
                }
                catch (Exception e)
                {
                    Console.WriteLine($"Ошибка: {e}");
                }
                
            } 
        }
    }
}