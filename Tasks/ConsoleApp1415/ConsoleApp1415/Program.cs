using System;

namespace MyCollectionsDeque
{
    public class MyArrayDeque<T>
    {
        private T[] elements;
        private int head; 
        private int tail;   
        private int count;

        public MyArrayDeque()
        {
            elements = new T[16];
            head = 0;
            tail = 0;
            count = 0;
        }

        public MyArrayDeque(T[] a)
        {
            if (a == null) throw new ArgumentNullException(nameof(a));
            int cap = Math.Max(16, a.Length * 2);
            elements = new T[cap];
            head = 0;
            tail = a.Length;
            count = a.Length;
            Array.Copy(a, 0, elements, 0, a.Length);
        }

        public MyArrayDeque(int numElements)
        {
            if (numElements <= 0) numElements = 1;
            elements = new T[numElements];
            head = tail = count = 0;
        }

        private int Capacity => elements.Length;

        private int Inc(int i) => (i + 1) % Capacity;
        private int Dec(int i) => (i - 1 + Capacity) % Capacity;

        private void EnsureCapacity()
        {
            if (count < Capacity) return;

            int newCap = Capacity * 2;
            T[] newArr = new T[newCap];

            if (head <= tail)
            {
                Array.Copy(elements, head, newArr, 0, count);
            }
            else
            {
                int right = Capacity - head;
                Array.Copy(elements, head, newArr, 0, right);
                Array.Copy(elements, 0, newArr, right, tail);
            }

            elements = newArr;
            head = 0;
            tail = count;
        }

        public void Add(T e)
        {
            EnsureCapacity();
            elements[tail] = e;
            tail = Inc(tail);
            count++;
        }

        public void AddLast(T obj) => Add(obj);

        public void AddFirst(T obj)
        {
            EnsureCapacity();
            head = Dec(head);
            elements[head] = obj;
            count++;
        }

        public bool Offer(T obj)
        {
            Add(obj);
            return true;
        }

        public bool OfferLast(T obj) => Offer(obj);

        public bool OfferFirst(T obj)
        {
            AddFirst(obj);
            return true;
        }

        public T Element()
        {
            if (IsEmpty()) throw new InvalidOperationException("Deque is empty");
            return elements[head];
        }

        public T Peek()
        {
            if (IsEmpty()) return default;
            return elements[head];
        }

        public T PeekFirst() => Peek();

        public T PeekLast()
        {
            if (IsEmpty()) return default;
            return elements[Dec(tail)];
        }

        public T Poll()
        {
            if (IsEmpty()) return default;
            T val = elements[head];
            elements[head] = default;
            head = Inc(head);
            count--;
            return val;
        }

        public T PollFirst() => Poll();

        public T PollLast()
        {
            if (IsEmpty()) return default;
            tail = Dec(tail);
            T val = elements[tail];
            elements[tail] = default;
            count--;
            return val;
        }

        public T GetFirst() => Element();

        public T GetLast()
        {
            if (IsEmpty()) throw new InvalidOperationException("Deque is empty");
            return elements[Dec(tail)];
        }

        public void Clear()
        {
            for (int i = 0; i < Capacity; i++) elements[i] = default;
            head = tail = count = 0;
        }

        public bool Contains(object o)
        {
            return IndexOf(o) != -1;
        }

        public bool ContainsAll(T[] a)
        {
            foreach (var x in a)
                if (!Contains(x)) return false;
            return true;
        }

        public bool Remove(object o)
        {
            int idx = IndexOf(o);
            if (idx == -1) return false;
            RemoveAt(idx);
            return true;
        }

        public void RemoveFirst() => Poll();

        public void RemoveLast() => PollLast();

        public void RemoveAll(T[] a)
        {
            foreach (var x in a)
                while (Remove(x)) ;
        }

        public void RetainAll(T[] a)
        {
            MyArrayDeque<T> temp = new MyArrayDeque<T>();
            for (int i = 0; i < count; i++)
            {
                T val = GetAtIndex(i);
                foreach (var z in a)
                {
                    if (Equals(z, val))
                        temp.AddLast(val);
                }
            }
            CopyFrom(temp);
        }

        public int Size() => count;

        public bool IsEmpty() => count == 0;

        public object[] ToArray()
        {
            object[] arr = new object[count];
            for (int i = 0; i < count; i++) arr[i] = GetAtIndex(i);
            return arr;
        }

        public T[] ToArray(T[] a)
        {
            if (a == null) a = new T[count];
            if (a.Length < count) a = new T[count];
            for (int i = 0; i < count; i++) a[i] = GetAtIndex(i);
            if (a.Length > count) a[count] = default;
            return a;
        }

        public bool RemoveFirstOccurrence(object obj)
        {
            int idx = IndexOf(obj);
            if (idx == -1) return false;
            RemoveAt(idx);
            return true;
        }

        public bool RemoveLastOccurrence(object obj)
        {
            for (int i = count - 1; i >= 0; i--)
            {
                if (Equals(GetAtIndex(i), obj))
                {
                    RemoveAt(i);
                    return true;
                }
            }
            return false;
        }
        
        private T GetAtIndex(int i)
        {
            return elements[(head + i) % Capacity];
        }

        private int IndexOf(object o)
        {
            for (int i = 0; i < count; i++)
                if (Equals(GetAtIndex(i), o)) return i;
            return -1;
        }

        private void RemoveAt(int idx)
        {
            if (idx < 0 || idx >= count) return;

            if (idx < count / 2)
            {
                for (int i = idx; i > 0; i--)
                {
                    elements[(head + i) % Capacity] = elements[(head + i - 1) % Capacity];
                }
                elements[head] = default;
                head = Inc(head);
            }
            else
            {
                for (int i = idx; i < count - 1; i++)
                {
                    elements[(head + i) % Capacity] = elements[(head + i + 1) % Capacity];
                }
                tail = Dec(tail);
                elements[tail] = default;
            }
            count--;
        }

        private void CopyFrom(MyArrayDeque<T> other)
        {
            this.elements = new T[Math.Max(16, other.count * 2)];
            this.head = 0;
            this.tail = other.count;
            this.count = other.count;

            for (int i = 0; i < count; i++)
                this.elements[i] = other.GetAtIndex(i);
        }
    }
}
