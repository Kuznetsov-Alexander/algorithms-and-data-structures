using System;
using System.Diagnostics;
using MyCollectionsLinkedList;
using MyCollectionsList;   // твой MyArrayList лежит здесь

namespace Task17
{
    class Program
    {
        static void Main(string[] args)
        {
            int[] sizes = { 100_000, 1_000_000, 10_000_000 };

            Console.WriteLine("size;structure;operation;avg_ms");

            foreach (int sz in sizes)
            {
                // Инициализация
                MyArrayList<int> arr = new MyArrayList<int>();
                MyLinkedList<int> list = new MyLinkedList<int>();

                for (int i = 0; i < sz; i++)
                {
                    arr.Add(i);
                    list.Add(i);
                }

                TestGet(arr, list, sz);
                TestSet(arr, list, sz);
                TestAdd(arr, list, sz);
                TestInsert(arr, list, sz);
                TestRemove(arr, list, sz);
            }
        }

        static double Time(Action act)
        {
            Stopwatch sw = new Stopwatch();
            sw.Start();
            act();
            sw.Stop();
            return sw.Elapsed.TotalMilliseconds;
        }
        
        static void TestGet(MyArrayList<int> arr, MyLinkedList<int> list, int size)
        {
            double arrTime = 0, listTime = 0;
            Random rnd = new Random();

            for (int t = 0; t < 20; t++)
            {
                int index = rnd.Next(0, size);

                arrTime += Time(() => { var _ = arr.Get(index); });
                listTime += Time(() => { var _ = list.Get(index); });
            }

            Console.WriteLine($"{size};arraylist;get;{arrTime/20}");
            Console.WriteLine($"{size};linkedlist;get;{listTime/20}");
        }

        static void TestSet(MyArrayList<int> arr, MyLinkedList<int> list, int size)
        {
            double arrTime = 0, listTime = 0;
            Random rnd = new Random();

            for (int t = 0; t < 20; t++)
            {
                int index = rnd.Next(0, size);

                arrTime += Time(() => { arr.Set(index, 123); });
                listTime += Time(() => { list.Set(index, 123); });
            }

            Console.WriteLine($"{size};arraylist;set;{arrTime/20}");
            Console.WriteLine($"{size};linkedlist;set;{listTime/20}");
        }

        static void TestAdd(MyArrayList<int> arr, MyLinkedList<int> list, int size)
        {
            arr.Clear();
            list.Clear();

            double arrTime = 0, listTime = 0;

            for (int t = 0; t < 20; t++)
            {
                arr.Clear(); list.Clear();

                arrTime += Time(() => {
                    for (int i = 0; i < size; i++) arr.Add(i);
                });

                listTime += Time(() => {
                    for (int i = 0; i < size; i++) list.Add(i);
                });
            }

            Console.WriteLine($"{size};arraylist;add;{arrTime/20}");
            Console.WriteLine($"{size};linkedlist;add;{listTime/20}");
        }

        static void TestInsert(MyArrayList<int> arr, MyLinkedList<int> list, int size)
        {
            Random rnd = new Random();
            double arrTime = 0, listTime = 0;

            for (int t = 0; t < 20; t++)
            {
                int index = rnd.Next(0, size);

                arrTime += Time(() => arr.Add(index, 999));
                listTime += Time(() => list.Add(index, 999));

                // удаляем вставленное
                arr.Remove(index);
                list.Remove(index);
            }

            Console.WriteLine($"{size};arraylist;insert;{arrTime/20}");
            Console.WriteLine($"{size};linkedlist;insert;{listTime/20}");
        }


        static void TestRemove(MyArrayList<int> arr, MyLinkedList<int> list, int size)
        {
            Random rnd = new Random();
            double arrTime = 0, listTime = 0;

            for (int t = 0; t < 20; t++)
            {
                int index = rnd.Next(0, size);

                arrTime += Time(() => arr.Remove(index));
                listTime += Time(() => list.Remove(index));

                // вставляем обратно (для стабильности)
                arr.Add(index, 0);
                list.Add(index, 0);
            }

            Console.WriteLine($"{size};arraylist;remove;{arrTime/20}");
            Console.WriteLine($"{size};linkedlist;remove;{listTime/20}");
        }
    }
}
