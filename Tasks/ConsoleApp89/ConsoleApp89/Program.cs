using System;

namespace Problem9;
public class MyArrayList<T>
{
    private T[] elements; 
    private int size; 
    public MyArrayList()
    {
        elements = new T[10]; 
        size = 0;
    }
    
    public MyArrayList(T[] a)
    {
        if (a == null) throw new ArgumentNullException(nameof(a), "Array cannot be null");
        
        size = a.Length;
        elements = new T[size];
        Array.Copy(a, elements, size);
    }
    
    public MyArrayList(int capacity)
    {
        if (capacity < 0) throw new ArgumentOutOfRangeException(nameof(capacity), "Capacity must be non-negative.");
        elements = new T[capacity];
        size = 0;
    }
    
    public void Add(T e)
    {
        if (size == elements.Length)
        {
            Resize(); 
        }
        elements[size++] = e; 
    }
    
    private void Resize()
    {
        int newCapacity = (int)(elements.Length * 1.5) + 1;
        T[] newArray = new T[newCapacity];
        Array.Copy(elements, newArray, size); 
        elements = newArray; 
    }
    
    public void AddAll(T[] a)
    {
        if (a == null) throw new ArgumentNullException(nameof(a), "Array cannot be null");

        foreach (var item in a)
        {
            Add(item); 
        }
    }
    
    public void Clear()
    {
        size = 0; 
        Array.Clear(elements, 0, elements.Length);
    }
    
    public bool Contains(object o)
    {
        for (int i = 0; i < size; i++)
        {
            if (Equals(elements[i], o)) return true; 
        }
        return false;
    }
    
    public bool ContainsAll(T[] a)
    {
        if (a == null) throw new ArgumentNullException(nameof(a), "Array cannot be null");

        foreach (var item in a)
        {
            if (!Contains(item)) return false; 
        }
        return true;
    }
    
    public bool IsEmpty() => size == 0;
    public bool Remove(object o)
    {
        for (int i = 0; i < size; i++)
        {
            if (Equals(elements[i], o))
            {
                RemoveAt(i);
                return true;
            }
        }
        return false;
    }
    
    public void RemoveAll(T[] a)
    {
        if (a == null) throw new ArgumentNullException(nameof(a), "Array cannot be null");

        foreach (var item in a)
        {
            Remove(item);
        }
    }

    public void RetainAll(T[] a)
    {
        if (a == null) throw new ArgumentNullException(nameof(a), "Array cannot be null");

        for (int i = size - 1; i >= 0; i--)
        {
            if (Array.IndexOf(a, elements[i]) == -1) 
            {
                RemoveAt(i);
            }
        }
    }
    
    public int Size() => size;
    
    public T[] ToArray()
    {
        T[] result = new T[size];
        Array.Copy(elements, result, size);
        return result; 
    }
    
    public T[] ToArray(T[] a)
    {
        if (a == null || a.Length < size)
        {
            return ToArray(); 
        }
        Array.Copy(elements, a, size); 
        return a; 
    }
    
    public void Add(int index, T e)
    {
        if (index < 0 || index > size) throw new ArgumentOutOfRangeException(nameof(index), "Index out of bounds.");

        if (size == elements.Length)
        {
            Resize(); 
        }

        for (int i = size; i > index; i--) 
        {
            elements[i] = elements[i - 1];
        }
        elements[index] = e; 
        size++; 
    }
    
    public void AddAll(int index, T[] a)
    {
        if (a == null) throw new ArgumentNullException(nameof(a), "Array cannot be null");

        for (int i = 0; i < a.Length; i++)
        {
            Add(index + i, a[i]); 
        }
    }
    
    public T Get(int index)
    {
        if (index < 0 || index >= size) throw new ArgumentOutOfRangeException(nameof(index), "Index out of bounds.");
        return elements[index]; 
    }
    
    public int IndexOf(object o)
    {
        for (int i = 0; i < size; i++)
        {
            if (Equals(elements[i], o)) return i; 
        }
        return -1; 
    }
    
    public int LastIndexOf(object o)
    {
        for (int i = size - 1; i >= 0; i--)
        {
            if (Equals(elements[i], o)) return i; 
        }
        return -1; 
    }
    
    public T RemoveAt(int index)
    {
        if (index < 0 || index >= size) throw new ArgumentOutOfRangeException(nameof(index), "Index out of bounds.");

        T removedElement = elements[index]; 
        for (int i = index; i < size - 1; i++) 
        {
            elements[i] = elements[i + 1];
        }
        elements[--size] = default; 
        return removedElement; 
    }
    
    public T Set(int index, T e)
    {
        if (index < 0 || index >= size) throw new ArgumentOutOfRangeException(nameof(index), "Index out of bounds.");

        T oldElement = elements[index]; 
        elements[index] = e;
        return oldElement; 
    }

   
    public MyArrayList<T> SubList(int fromIndex, int toIndex)
    {
        if (fromIndex < 0 ||  toIndex > size ||  fromIndex > toIndex)
            throw new ArgumentOutOfRangeException("Invalid range for sublist.");
        
        MyArrayList<T> subList = new MyArrayList<T>(toIndex - fromIndex); 
        for (int i = fromIndex; i < toIndex; i++)
        {
            subList.Add(elements[i]); 
        }
        return subList;
    }
}

class Program
{
    static void Main()
    {
        MyArrayList<int> list = new MyArrayList<int>();
        
        list.Add(1);
        list.Add(2);
        list.Add(3);
        Console.WriteLine($"After adding elements: {string.Join(", ", list.ToArray())}");
        
        Console.WriteLine($"Element at index 1: {list.Get(1)}"); 
        Console.WriteLine($"Size: {list.Size()}"); 
        
        Console.WriteLine($"Index of element 3: {list.IndexOf(3)}"); 
        Console.WriteLine($"Last index of element 2: {list.LastIndexOf(2)}"); 
        
        Console.WriteLine($"Contains 2: {list.Contains(2)}");
        Console.WriteLine($"Contains 5: {list.Contains(5)}");
        
        list.Remove(2); 
        Console.WriteLine($"After removing element 2: {string.Join(", ", list.ToArray())}"); 
        
        int[] newItems = { 4, 5, 6 };
        list.AddAll(newItems);
        Console.WriteLine($"After adding elements from array: {string.Join(", ", list.ToArray())}"); 
        
        MyArrayList<int> subList = list.SubList(1, 4);
        Console.WriteLine($"Sublist from index 1 to 4: {string.Join(", ", subList.ToArray())}");
        
        list.Clear();
        Console.WriteLine($"After clearing the list, is empty: {list.IsEmpty()}"); 
        
        list.Add(7);
        list.Add(8);
        list.Add(9);
        Console.WriteLine($"After adding more elements: {string.Join(", ", list.ToArray())}"); 
        
        list.Set(1, 10);
        Console.WriteLine($"After replacing element at index 1: {string.Join(", ", list.ToArray())}");
    }
}