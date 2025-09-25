using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace LabWork3
{
    class Program
    {
        static List<Institute> institutes = new List<Institute>();

        static void Main(string[] args)
        {
            SeedTestData();
            bool exit = false;
            while (!exit)
            {
                Console.WriteLine("\n===== МЕНЮ =====");
                Console.WriteLine("1. Вывести институты и курсы со средним баллом >= 3.5");
                Console.WriteLine("2. Выход");
                Console.Write("Выберите пункт: ");
                string choice = Console.ReadLine();

                switch (choice)
                {
                    case "1":
                        ShowAndSaveResults();
                        break;
                    case "2":
                        exit = true;
                        break;
                    default:
                        Console.WriteLine("Неверный выбор!");
                        break;
                }
            }
        }

        static void ShowAndSaveResults()
        {
            string filePath = "result.txt";
            using (StreamWriter sw = new StreamWriter(filePath, false))
            {
                foreach (var inst in institutes)
                {
                    foreach (var course in inst.Courses)
                    {
                        if (course.AverageMark >= 3.5)
                        {
                            string line = $"Институт: {inst.Name}, Курс: {course.Number}, Ср. балл: {course.AverageMark:F2}";
                            Console.WriteLine(line);
                            sw.WriteLine(line);
                        }
                    }
                }
            }
            Console.WriteLine($"\nРезультаты сохранены в файл: {Path.GetFullPath("result.txt")}");
        }

        static void SeedTestData()
        {
            var student1 = new Student { LastName = "Литвиненко", Marks = new List<int> { 5, 4, 4, 5 } };
            var student2 = new Student { LastName = "Прохоров", Marks = new List<int> { 3, 4, 4, 3 } };
            var student3 = new Student { LastName = "Токарева", Marks = new List<int> { 2, 3, 2, 3 } };
            var student4 = new Student { LastName = "Кузнецов", Marks = new List<int> { 5, 5, 4, 5 } };

            var group1 = new Group { Name = "16/1", Students = new List<Student> { student1, student2 } };
            var group2 = new Group { Name = "16/2", Students = new List<Student> { student3, student4 } };

            var course1 = new Course { Number = 1, Groups = new List<Group> { group1 } };
            var course2 = new Course { Number = 2, Groups = new List<Group> { group2 } };

            var inst1 = new Institute { Name = "КубГУ", Courses = new List<Course> { course1, course2 } };

            institutes.Add(inst1);
        }
    }
}
