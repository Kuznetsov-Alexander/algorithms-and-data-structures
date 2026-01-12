using System;

namespace MyCollectionsLinkedList
{
    public class MyLinkedList<T>
    {
        private class Node
        {
            public T Value;
            public Node Prev;
            public Node Next;

            public Node(T value)
            {
                Value = value;
            }
        }

        private Node first;
        private Node last;
        private int size;

        public MyLinkedList() { }

        public MyLinkedList(T[] a)
        {
            foreach (var x in a)
                Add(x);
        }

        public int Size() => size;

        public bool IsEmpty() => size == 0;

        private Node GetNode(int index)
        {
            if (index < 0 || index >= size)
                throw new ArgumentOutOfRangeException();

            Node cur;
            if (index < size / 2)
            {
                cur = first;
                for (int i = 0; i < index; i++) cur = cur.Next;
            }
            else
            {
                cur = last;
                for (int i = size - 1; i > index; i--) cur = cur.Prev;
            }
            return cur;
        }


        public void Add(T e)
        {
            Node n = new Node(e);
            if (first == null)
            {
                first = last = n;
            }
            else
            {
                last.Next = n;
                n.Prev = last;
                last = n;
            }
            size++;
        }

        public void AddFirst(T obj)
        {
            Node n = new Node(obj);
            if (first == null)
            {
                first = last = n;
            }
            else
            {
                n.Next = first;
                first.Prev = n;
                first = n;
            }
            size++;
        }

        public void AddLast(T obj) => Add(obj);

        public void AddAll(T[] a)
        {
            foreach (var x in a) Add(x);
        }

        public void Add(int index, T e)
        {
            if (index < 0 || index > size) throw new ArgumentOutOfRangeException();

            if (index == size)
            {
                Add(e);
                return;
            }
            if (index == 0)
            {
                AddFirst(e);
                return;
            }

            Node cur = GetNode(index);
            Node n = new Node(e);

            n.Prev = cur.Prev;
            n.Next = cur;
            cur.Prev.Next = n;
            cur.Prev = n;

            size++;
        }

        public void AddAll(int index, T[] a)
        {
            foreach (var x in a)
            {
                Add(index, x);
                index++;
            }
        }

        public T Get(int index)
        {
            return GetNode(index).Value;
        }

        public T Set(int index, T e)
        {
            Node n = GetNode(index);
            T old = n.Value;
            n.Value = e;
            return old;
        }

        public int IndexOf(object o)
        {
            Node cur = first;
            int i = 0;
            while (cur != null)
            {
                if (Equals(cur.Value, o)) return i;
                cur = cur.Next;
                i++;
            }
            return -1;
        }

        public int LastIndexOf(object o)
        {
            Node cur = last;
            int i = size - 1;
            while (cur != null)
            {
                if (Equals(cur.Value, o)) return i;
                cur = cur.Prev;
                i--;
            }
            return -1;
        }

        public bool Contains(object o) => IndexOf(o) != -1;

        public bool ContainsAll(T[] a)
        {
            foreach (var x in a)
                if (!Contains(x)) return false;
            return true;
        }

        public void Clear()
        {
            Node cur = first;
            while (cur != null)
            {
                Node nx = cur.Next;
                cur.Value = default;
                cur.Next = cur.Prev = null;
                cur = nx;
            }
            first = last = null;
            size = 0;
        }

        public bool Remove(object o)
        {
            int idx = IndexOf(o);
            if (idx == -1) return false;
            Remove(idx);
            return true;
        }

        public void RemoveAll(T[] a)
        {
            foreach (var x in a)
                while (Remove(x)) ;
        }

        public void RetainAll(T[] a)
        {
            MyLinkedList<T> temp = new MyLinkedList<T>();
            for (int i = 0; i < size; i++)
            {
                T val = Get(i);
                foreach (var z in a)
                    if (Equals(val, z))
                        temp.Add(val);
            }
            CopyFrom(temp);
        }

        private void CopyFrom(MyLinkedList<T> other)
        {
            this.Clear();
            Node cur = other.first;
            while (cur != null)
            {
                Add(cur.Value);
                cur = cur.Next;
            }
        }

        public T Remove(int index)
        {
            Node cur = GetNode(index);
            T old = cur.Value;

            if (cur.Prev != null) cur.Prev.Next = cur.Next;
            else first = cur.Next;

            if (cur.Next != null) cur.Next.Prev = cur.Prev;
            else last = cur.Prev;

            cur.Next = cur.Prev = null;
            size--;

            return old;
        }

        public T Element()
        {
            if (IsEmpty()) throw new InvalidOperationException("Empty list");
            return first.Value;
        }

        public bool Offer(T obj)
        {
            Add(obj);
            return true;
        }

        public T Peek()
        {
            if (IsEmpty()) return default;
            return first.Value;
        }

        public T Poll()
        {
            if (IsEmpty()) return default;
            return Remove(0);
        }

        public T GetFirst()
        {
            if (IsEmpty()) throw new InvalidOperationException("Empty list");
            return first.Value;
        }

        public T GetLast()
        {
            if (IsEmpty()) throw new InvalidOperationException("Empty list");
            return last.Value;
        }

        public bool OfferFirst(T obj)
        {
            AddFirst(obj);
            return true;
        }

        public bool OfferLast(T obj)
        {
            AddLast(obj);
            return true;
        }

        public T Pop() => Poll();

        public void Push(T obj) => AddFirst(obj);

        public T PeekFirst() => Peek();

        public T PeekLast()
        {
            if (IsEmpty()) return default;
            return last.Value;
        }

        public T PollFirst() => Poll();

        public T PollLast()
        {
            if (IsEmpty()) return default;
            return Remove(size - 1);
        }

        public void RemoveFirst() => Remove(0);

        public void RemoveLast() => Remove(size - 1);

        public bool RemoveFirstOccurrence(object obj)
        {
            return Remove(obj);
        }

        public bool RemoveLastOccurrence(object obj)
        {
            int idx = LastIndexOf(obj);
            if (idx == -1) return false;
            Remove(idx);
            return true;
        }

        public object[] ToArray()
        {
            object[] arr = new object[size];
            Node cur = first;
            int i = 0;
            while (cur != null)
            {
                arr[i++] = cur.Value;
                cur = cur.Next;
            }
            return arr;
        }

        public T[] ToArray(T[] a)
        {
            if (a == null || a.Length < size)
                a = new T[size];

            Node cur = first;
            int i = 0;
            while (cur != null)
            {
                a[i++] = cur.Value;
                cur = cur.Next;
            }

            if (a.Length > size) a[size] = default;
            return a;
        }

        public MyLinkedList<T> SubList(int fromIndex, int toIndex)
        {
            if (fromIndex < 0 || toIndex > size || fromIndex > toIndex)
                throw new ArgumentOutOfRangeException();

            MyLinkedList<T> res = new MyLinkedList<T>();
            for (int i = fromIndex; i < toIndex; i++)
                res.Add(Get(i));
            return res;
        }
    }
}
