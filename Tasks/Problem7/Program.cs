using System;
using System.Collections;
using System.Collections.Generic;
using Problem7;
namespace Problem7;

public class MyPriorityQueue<T>
{
    private T[] queue;
    private int size;
    private IComparer<T> comparator;

    public MyPriorityQueue() : this(11, Comparer<T>.Default) { }

    public MyPriorityQueue(T[] a) : this(a.Length, Comparer<T>.Default)
    {
        AddAll(a);
    }

    public MyPriorityQueue(int initialCapacity) : this(initialCapacity, Comparer<T>.Default) { }

    public MyPriorityQueue(int initialCapacity, IComparer<T> comparator)
    {
        if (initialCapacity < 1)
            throw new ArgumentException("Initial capacity must be at least 1");
        this.queue = new T[initialCapacity];
        this.size = 0;
        this.comparator = comparator ?? Comparer<T>.Default;
    }

    public void Add(T e)
    {
        if (size == queue.Length)
            Resize();
        queue[size] = e;
        size++;
        HeapifyUp(size - 1);
    }

    public void AddAll(T[] a)
    {
        foreach (var element in a)
        {
            Add(element);
        }
    }

    public void Clear()
    {
        size = 0;
    }

    public bool Contains(T item)
    {
        for (int i = 0; i < size; i++)
        {
            if (EqualityComparer<T>.Default.Equals(queue[i], item))
                return true;
        }
        return false;
    }

    public bool IsEmpty() => size == 0;

    public T Element()
    {
        if (IsEmpty())
            throw new InvalidOperationException("Priority queue is empty");
        return queue[0];
    }

    public T Peek() => IsEmpty() ? default : queue[0];

    public T Poll()
    {
        if (IsEmpty())
            return default;

        T result = queue[0];
        queue[0] = queue[size - 1]; 
        queue[size - 1] = default;   
        size--;
        HeapifyDown(0); 
        return result;
    }

    private void Resize()
    {
        int newCapacity = queue.Length < 64 ? queue.Length + 2 : (int)(queue.Length * 1.5);
        Array.Resize(ref queue, newCapacity);
    }

    private void HeapifyUp(int index)
    {
        while (index > 0)
        {
            int parentIndex = (index - 1) / 2;
            if (Compare(queue[index], queue[parentIndex]) >= 0)
                break;
            Swap(index, parentIndex);
            index = parentIndex;
        }
    }

    private void HeapifyDown(int index)
    {
        while (true)
        {
            int leftChildIndex = 2 * index + 1;
            int rightChildIndex = 2 * index + 2;
            int smallest = index;

            if (leftChildIndex < size && Compare(queue[leftChildIndex], queue[smallest]) < 0)
                smallest = leftChildIndex;
            if (rightChildIndex < size && Compare(queue[rightChildIndex], queue[smallest]) < 0)
                smallest = rightChildIndex;

            if (smallest == index)
                break;

            Swap(index, smallest);
            index = smallest;
        }
    }
    
    private void Swap(int i, int j)
    {
        T temp = queue[i];
        queue[i] = queue[j];
        queue[j] = temp;
    }

    private int Compare(T o1, T o2) => comparator.Compare(o1, o2);

    public int Size() => size;

    public T[] ToArray()
    {
        T[] result = new T[size];
        Array.Copy(queue, result, size);
        return result;
    }

    public T[] ToArray(T[] a)
    {
        if (a.Length < size)
            return ToArray();
        Array.Copy(queue, a, size);
        if (a.Length > size)
            a[size] = default;
        return a;
    }
}
public class Application
{
    public int RequestNumber { get; }
    public int Priority { get; }
    public int StepNumber { get; }
    public int WaitTime { get; set; }

    public Application(int requestNumber, int priority, int stepNumber)
    {
        RequestNumber = requestNumber;
        Priority = priority;
        StepNumber = stepNumber;
        WaitTime = 0;
    }
}
public class ApplicationComparer : IComparer<Application>
{
    public int Compare(Application x, Application y)
    {
        int result = y.Priority.CompareTo(x.Priority);
        if (result == 0)
        {
            result = x.RequestNumber.CompareTo(y.RequestNumber);
        }
        return result;
    }
}
class Program
{
    public static void Main(string[] args)
    {
        Random rand = new Random();
        MyPriorityQueue<Application> priorityQueue = new MyPriorityQueue<Application>(50, new ApplicationComparer());
        string path = "log.txt";
        int N;
        Console.WriteLine("Введите N - количество шагов добавления заявок в приоритетную очередь");
        N = Convert.ToInt32(Console.ReadLine());
        
        var lines = new List<string>();
        List<Application> requests = new List<Application>();
        int MaxTime = 0;
        Application MaxApp = null;
        for (int i = 1; i <= N; i++)
        {
            int k = rand.Next(1, 11);
            //int k = 9;
            for (int j = 0; j < k; j++)
            {
                int priority = rand.Next(1, 6);
                //int priority = 4;
                Application app = new Application(requests.Count + 1, priority, i);
                requests.Add(app);
                priorityQueue.Add(app);
                lines.Add($"ADD {app.RequestNumber} {app.Priority} {app.StepNumber}");
            }

            var removed = priorityQueue.Poll();
            removed.WaitTime = i - removed.StepNumber; 

            lines.Add($"REMOVE {removed.RequestNumber} {removed.Priority} {removed.StepNumber}");
                    
            if (removed.WaitTime > MaxTime)
            {
                MaxTime = removed.WaitTime;
                MaxApp = removed;
            }
            
        }
        while (!priorityQueue.IsEmpty())
        {
            var removed = priorityQueue.Poll();
            removed.WaitTime = N - removed.StepNumber; 
            
            lines.Add($"REMOVE {removed.RequestNumber} {removed.Priority} {removed.StepNumber}");
            
            if (removed.WaitTime > MaxTime)
            {
                MaxTime = removed.WaitTime;
                MaxApp = removed;
            }
        }
        File.WriteAllLines(path, lines);
        if (MaxApp != null)
        {
            Console.WriteLine($"Заявка с максимальным временем ожидания: Номер {MaxApp.RequestNumber}, Приоритет {MaxApp.Priority}, Время ожидания {MaxApp.WaitTime}");
        }
        else
        {
            Console.WriteLine("Очередь была пуста, нет заявок.");
        }
    }
}