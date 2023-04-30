using System;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Security.Cryptography.X509Certificates;

namespace FinalTask
{
    public class Program
    {
        public static void Main(string[] args)
        {
            try
            {
                DirectoryInfo directory = new DirectoryInfo(@"C:\Users\balak\OneDrive\Рабочий стол\Students");   // создаем папку Students на рабочем столе
                if (!directory.Exists)
                {
                    directory.Create();
                }

                BinaryFormatter formatter = new BinaryFormatter();
                using (FileStream fs = new FileStream(@"C:\Users\balak\OneDrive\Рабочий стол\Students.dat", FileMode.Open))
                {
                    Student[] students = (Student[])formatter.Deserialize(fs);        // получаем массив студентов из бинарного файла
                    foreach (Student student in students)
                    {
                        WriteStudents(student);   // вызываем метод записи студента в текст
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Возникло исключение: {ex.Message}");
            }
        }

        public static async void WriteStudents(Student student)
        {
            using (StreamWriter writer = new StreamWriter(@$"C:\Users\balak\OneDrive\Рабочий стол\Students\{student.Group}.txt", false))
            {
                await writer.WriteLineAsync($"Student: {student.Name}, Birthday: {student.DateOfBirth}");
            }
        }
    }

    [Serializable]
    public class Student
    {
        public string Name { get; set; }
        public string Group { get; set; }
        public DateTime DateOfBirth { get; set; }
        public Student(string name, string group, DateTime dateOfBirth)
        {
            Name = name;
            Group = group;
            DateOfBirth = dateOfBirth;
        }
    }
}