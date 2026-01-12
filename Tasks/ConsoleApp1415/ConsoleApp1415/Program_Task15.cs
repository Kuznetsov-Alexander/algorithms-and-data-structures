using System;
using System.IO;
using MyCollectionsDeque;

namespace Task15
{
    class Program
    {
        static void Main(string[] args)
        {
            string input = "input.txt";
            string sorted = "sorted.txt";

            if (!File.Exists(input))
            {
                Console.WriteLine("Файл input.txt не найден.");
                return;
            }

            string[] lines = File.ReadAllLines(input);
            MyArrayDeque<string> deque = new MyArrayDeque<string>();

            foreach (var line in lines)
            {
                if (deque.IsEmpty())
                {
                    deque.AddLast(line);
                    continue;
                }

                int digitsLine = CountDigits(line);
                int digitsFirst = CountDigits(deque.GetFirst());

                if (digitsLine > digitsFirst)
                    deque.AddLast(line);
                else
                    deque.AddFirst(line);
            }
            
            using (StreamWriter sw = new StreamWriter(sorted))
            {
                object[] arr = deque.ToArray();
                foreach (var x in arr)
                    sw.WriteLine(x);
            }

            Console.WriteLine($"Готово: результат записан в {sorted}");
            
            Console.Write("Введите n: ");
            if (!int.TryParse(Console.ReadLine(), out int n)) n = 0;

            MyArrayDeque<string> filtered = new MyArrayDeque<string>();

            foreach (string s in deque.ToArray())
            {
                if (CountSpaces(s) <= n)
                    filtered.AddLast(s);
            }

            Console.WriteLine("\nОставшиеся строки:");
            foreach (string s in filtered.ToArray())
                Console.WriteLine(s);
        }
        static int CountDigits(string s)
        {
            int cnt = 0;
            foreach (char c in s)
                if (char.IsDigit(c)) cnt++;
            return cnt;
        }

        static int CountSpaces(string s)
        {
            int cnt = 0;
            foreach (char c in s)
                if (c == ' ') cnt++;
            return cnt;
        }
    }
}
