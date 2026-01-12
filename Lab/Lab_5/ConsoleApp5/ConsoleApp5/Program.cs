using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace ConsoleApp5
{
    public class ErrorEventArgs : EventArgs
    {
        public string Message { get; }
        public ErrorEventArgs(string message)
        {
            Message = message;
        }
    }
    
    class Program
        {
            static List<Institute> institutes = new List<Institute>();
            static ErrorManager errorManager = new ErrorManager();

            static void Main(string[] args)
            {
                errorManager.ErrorOccurred += ShowError;

                SeedTestData();

                bool exit = false;
                while (!exit)
                {
                    Console.WriteLine("\n===== МЕНЮ =====");
                    Console.WriteLine("1. Вывести институты и курсы со средним баллом >= 3.5");
                    Console.WriteLine("2. Демонстрация обработки ошибок");
                    Console.WriteLine("3. Выход");
                    Console.Write("Выберите пункт: ");
                    string choice = Console.ReadLine();

                    switch (choice)
                    {
                        case "1":
                            ShowAndSaveResults();
                            break;
                        case "2":
                            DemonstrateErrors();
                            break;
                        case "3":
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
                try
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
                catch (IOException ex)
                {
                    errorManager.RaiseError($"Ошибка ввода-вывода: {ex.Message}");
                }
            }

            static void DemonstrateErrors()
            {
                Console.WriteLine("\n--- Демонстрация обработки исключений ---");

                try
                {
                    int x = 10, y = 0;
                    int z = x / y;
                }
                catch (DivideByZeroException ex)
                {
                    errorManager.RaiseError($"DivideByZeroException: {ex.Message}");
                }

                try
                {
                    int[] arr = new int[3];
                    arr[5] = 10;
                }
                catch (IndexOutOfRangeException ex)
                {
                    errorManager.RaiseError($"IndexOutOfRangeException: {ex.Message}");
                }

                try
                {
                    object str = "hello";
                    int num = (int)str;
                }
                catch (InvalidCastException ex)
                {
                    errorManager.RaiseError($"InvalidCastException: {ex.Message}");
                }

                try
                {
                    checked
                    {
                        int big = int.MaxValue;
                        big++;
                    }
                }
                catch (OverflowException ex)
                {
                    errorManager.RaiseError($"OverflowException: {ex.Message}");
                }

                try
                {
                    Array arr = Array.CreateInstance(typeof(string), 2);
                    arr.SetValue(123, 0);
                }
                catch (ArrayTypeMismatchException ex)
                {
                    errorManager.RaiseError($"ArrayTypeMismatchException: {ex.Message}");
                }

                try
                {
                    throw new OutOfMemoryException("Недостаточно памяти для выполнения операции");
                }
                catch (OutOfMemoryException ex)
                {
                    errorManager.RaiseError($"OutOfMemoryException: {ex.Message}");
                }

                try
                {
                    throw new StackOverflowException("Переполнение стека вызовов (имитация)");
                }
                catch (StackOverflowException ex)
                {
                    errorManager.RaiseError($"StackOverflowException: {ex.Message}");
                }
            }

            static void ShowError(object sender, ErrorEventArgs e)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine($"[СОБЫТИЕ] Ошибка: {e.Message}");
                Console.ResetColor();
            }

            static void SeedTestData()
            {            
                var student1 = new Student { LastName = "Литвиненко", Marks = new List<int> { 5, 5, 5, 5 } };
                var student2 = new Student { LastName = "Прохоров", Marks = new List<int> { 5, 5, 5, 5 } };
                var student3 = new Student { LastName = "Токарева", Marks = new List<int> { 5, 5, 5, 5 } };
                var student4 = new Student { LastName = "Кузнецов", Marks = new List<int> { 5, 5, 5, 5 } };

                var group1 = new Group { Name = "16/1", Students = new List<Student> { student1, student2 } };
                var group2 = new Group { Name = "16/2", Students = new List<Student> { student3, student4 } };

                var course1 = new Course { Number = 1, Groups = new List<Group> { group1 } };
                var course2 = new Course { Number = 2, Groups = new List<Group> { group2 } };

                var inst1 = new Institute { Name = "КубГУ", Courses = new List<Course> { course1, course2 } };

                institutes.Add(inst1);
            }
        }
}