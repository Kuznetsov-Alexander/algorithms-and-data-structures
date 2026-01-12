using System;

namespace MyCollections
{
    public class MyVector<T>
    {
        private T[] elementData;
        private int elementCount;
        private int capacityIncrement;

        public MyVector(int initialCapacity, int capacityIncrement)
        {
            if (initialCapacity < 0) throw new ArgumentOutOfRangeException(nameof(initialCapacity));
            elementData = new T[Math.Max(1, initialCapacity)];
            this.capacityIncrement = capacityIncrement;
            elementCount = 0;
        }

        public MyVector(int initialCapacity) : this(initialCapacity, 0) { }

        public MyVector() : this(10, 0) { }

        public MyVector(T[] a)
        {
            if (a == null) throw new ArgumentNullException(nameof(a));
            elementData = new T[Math.Max(10, a.Length)];
            Array.Copy(a, elementData, a.Length);
            elementCount = a.Length;
            capacityIncrement = 0;
        }

        private void EnsureCapacity(int minCapacity)
        {
            if (minCapacity <= elementData.Length) return;
            int newCapacity;
            if (capacityIncrement != 0)
            {
                newCapacity = elementData.Length + capacityIncrement;
                if (newCapacity < minCapacity) newCapacity = minCapacity;
            }
            else
            {
                newCapacity = elementData.Length * 2;
                if (newCapacity < minCapacity) newCapacity = minCapacity;
            }

            if (newCapacity < 0)
                newCapacity = int.MaxValue;

            T[] newArr = new T[newCapacity];
            Array.Copy(elementData, newArr, elementCount);
            elementData = newArr;
        }

        public void Add(T e)
        {
            EnsureCapacity(elementCount + 1);
            elementData[elementCount++] = e;
        }

        public void AddAll(T[] a)
        {
            if (a == null) throw new ArgumentNullException(nameof(a));
            EnsureCapacity(elementCount + a.Length);
            Array.Copy(a, 0, elementData, elementCount, a.Length);
            elementCount += a.Length;
        }

        public void Add(int index, T e)
        {
            if (index < 0 || index > elementCount) throw new ArgumentOutOfRangeException(nameof(index));
            EnsureCapacity(elementCount + 1);
            if (index < elementCount)
                Array.Copy(elementData, index, elementData, index + 1, elementCount - index);
            elementData[index] = e;
            elementCount++;
        }

        public void AddAll(int index, T[] a)
        {
            if (a == null) throw new ArgumentNullException(nameof(a));
            if (index < 0 || index > elementCount) throw new ArgumentOutOfRangeException(nameof(index));
            EnsureCapacity(elementCount + a.Length);
            if (index < elementCount)
                Array.Copy(elementData, index, elementData, index + a.Length, elementCount - index);
            Array.Copy(a, 0, elementData, index, a.Length);
            elementCount += a.Length;
        }

        public void Clear()
        {
            for (int i = 0; i < elementCount; i++) elementData[i] = default;
            elementCount = 0;
        }

        public bool Contains(object o)
        {
            return IndexOf(o) != -1;
        }

        public bool ContainsAll(T[] a)
        {
            if (a == null) throw new ArgumentNullException(nameof(a));
            foreach (var item in a)
            {
                if (!Contains(item)) return false;
            }
            return true;
        }

        public bool IsEmpty() => elementCount == 0;

        public bool Remove(object o)
        {
            int idx = IndexOf(o);
            if (idx == -1) return false;
            Remove(idx);
            return true;
        }

        public void RemoveAll(T[] a)
        {
            if (a == null) throw new ArgumentNullException(nameof(a));
            for (int i = 0; i < a.Length; i++)
            {
                while (Remove(a[i])) { }
            }
        }

        public void RetainAll(T[] a)
        {
            if (a == null) throw new ArgumentNullException(nameof(a));
            int dst = 0;
            for (int src = 0; src < elementCount; src++)
            {
                T val = elementData[src];
                bool keep = false;
                foreach (var item in a)
                {
                    if (Equals(val, item)) { keep = true; break; }
                }
                if (keep) elementData[dst++] = val;
            }
            // clear tail
            for (int i = dst; i < elementCount; i++) elementData[i] = default;
            elementCount = dst;
        }

        public int Size() => elementCount;

        public object[] ToArray()
        {
            object[] res = new object[elementCount];
            for (int i = 0; i < elementCount; i++) res[i] = elementData[i];
            return res;
        }

        public T[] ToArray(T[] a)
        {
            if (a == null)
            {
                T[] res = new T[elementCount];
                Array.Copy(elementData, 0, res, 0, elementCount);
                return res;
            }
            if (a.Length >= elementCount)
            {
                Array.Copy(elementData, 0, a, 0, elementCount);
                if (a.Length > elementCount) a[elementCount] = default;
                return a;
            }
            else
            {
                T[] res = new T[elementCount];
                Array.Copy(elementData, 0, res, 0, elementCount);
                return res;
            }
        }

        public T Get(int index)
        {
            if (index < 0 || index >= elementCount) throw new ArgumentOutOfRangeException(nameof(index));
            return elementData[index];
        }

        public int IndexOf(object o)
        {
            for (int i = 0; i < elementCount; i++)
            {
                if (Equals(elementData[i], o)) return i;
            }
            return -1;
        }

        public int LastIndexOf(object o)
        {
            for (int i = elementCount - 1; i >= 0; i--)
            {
                if (Equals(elementData[i], o)) return i;
            }
            return -1;
        }

        public T Remove(int index)
        {
            if (index < 0 || index >= elementCount) throw new ArgumentOutOfRangeException(nameof(index));
            T old = elementData[index];
            int move = elementCount - index - 1;
            if (move > 0) Array.Copy(elementData, index + 1, elementData, index, move);
            elementData[--elementCount] = default;
            return old;
        }

        public T Set(int index, T e)
        {
            if (index < 0 || index >= elementCount) throw new ArgumentOutOfRangeException(nameof(index));
            T old = elementData[index];
            elementData[index] = e;
            return old;
        }

        public MyVector<T> SubList(int fromIndex, int toIndex)
        {
            if (fromIndex < 0 || toIndex > elementCount || fromIndex > toIndex) throw new ArgumentOutOfRangeException();
            int len = toIndex - fromIndex;
            T[] arr = new T[len];
            Array.Copy(elementData, fromIndex, arr, 0, len);
            return new MyVector<T>(arr);
        }

        public T FirstElement()
        {
            if (elementCount == 0) throw new InvalidOperationException("Vector is empty");
            return elementData[0];
        }

        public T LastElement()
        {
            if (elementCount == 0) throw new InvalidOperationException("Vector is empty");
            return elementData[elementCount - 1];
        }

        public void RemoveElementAt(int pos) => Remove(pos);

        public void RemoveRange(int begin, int end)
        {
            if (begin < 0 || end > elementCount || begin > end) throw new ArgumentOutOfRangeException();
            int count = end - begin;
            if (count == 0) return;
            int move = elementCount - end;
            if (move > 0) Array.Copy(elementData, end, elementData, begin, move);
            for (int i = elementCount - count; i < elementCount; i++) elementData[i] = default;
            elementCount -= count;
        }

        public T this[int index]
        {
            get => Get(index);
            set => Set(index, value);
        }
    }
}
