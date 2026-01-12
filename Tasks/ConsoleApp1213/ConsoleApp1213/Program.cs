using System;
using MyCollections;

namespace MyCollectionsEx
{

    public class MyStack<T> : MyVector<T>
    {
        public MyStack() : base() { }
        public MyStack(int initialCapacity) : base(initialCapacity) { }
        public MyStack(T[] a) : base(a) { }


        public void Push(T item)
        {
            Add(item);
        }
        
        public T Pop()
        {
            if (IsEmpty()) throw new InvalidOperationException("Stack is empty");
            int idx = Size() - 1;
            return Remove(idx);
        }
        
        public T Peek()
        {
            if (IsEmpty()) throw new InvalidOperationException("Stack is empty");
            return Get(Size() - 1);
        }
        
        public bool Empty()
        {
            return IsEmpty();
        }
        
        public int Search(T item)
        {
            int sz = Size();
            for (int i = sz - 1, depth = 1; i >= 0; i--, depth++)
            {
                if (Equals(Get(i), item)) return depth;
            }
            return -1;
        }
    }
}